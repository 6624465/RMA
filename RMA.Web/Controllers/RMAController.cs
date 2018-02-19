using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EZY.RMAS.Contract;
using EZY.RMAS.BusinessFactory;
using System.Threading.Tasks;
using System.IO;
using System.Web.Hosting;
using System.Globalization;
using LogiCon.Utilities;
using System.Text.RegularExpressions;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Web.Routing;
using iTextSharp.tool.xml;

namespace RMA.Web.Controllers
{
    [WebSsnFilter]
    public class RMAController : BaseController
    {
        public Task MessageServices { get; private set; }


        public ActionResult Inward()
        {
            ViewBag.countries = new BranchBO().GetList().Select(x => new
            {
                Value = x.BranchCode,
                Text = x.BranchName
            }).ToList();
            return View(new RMAHeader { IncidentDate = UTILITY.SINGAPORETIME });
        }
        [HttpGet]
        public ActionResult VendorRMA()
        {
            //var Branch = BRANCH_ID;
            //var list = new RMAHeaderBO().GetListByBranchIDForVendor(BRANCH_ID);
            return View("VendorRMA", new List<RMAHeader>());
        }
        [HttpPost]
        public JsonResult VendorRMA(DataTableObject Obj)
        {
            try
            {
                Obj.BranchID = BRANCH_ID;
                var vendorOutwardList = new VendorRMAHeaderBO()
                    .GetVendorOutwardList(Obj)
                    .Select(x => new
                    {
                        DocumentNo = x.DocumentNo
                    }).ToList();
                var totalRecords = new RMAHeaderBO().GetListByBranchIDForVendor(BRANCH_ID).Count();
                // var totalRecords = new InvoiceHeaderBO().GetList(BRANCH_ID).Where(x => x.InvoiceType == UTILITY.CONFIG_INVOICETYPE_VENDOR && x.Status == true).Count();
                return Json(new
                {
                    vendorOutwardList = vendorOutwardList,
                    totalRecords = totalRecords
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult VendorInvoiceList()
        {
            return View("VendorInvoiceList", new List<InvoiceHeader>());
        }
        [HttpPost]
        public ActionResult VendorRMAInvoiceSave(VendorRMAInvoiceHeader vendorRMAInvoiceHeader)
        {
            vendorRMAInvoiceHeader.BranchID = BRANCH_ID;
            vendorRMAInvoiceHeader.ReferenceNo = vendorRMAInvoiceHeader.VendorRMAInvoiceDetails.FirstOrDefault().RMANumber;
            vendorRMAInvoiceHeader.CreatedBy = USER_ID;
            vendorRMAInvoiceHeader.ModifiedBy = USER_ID;
            var result = new VendorRMAInvoiceHeaderBO().Save(vendorRMAInvoiceHeader);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult VendorOutwardRMADetailsByRMANo(string DocumentNo)
        {
            try
            {
                VendorRMAInvoiceHeader vendorRMADTO = new VendorRMAInvoiceHeader()
                {
                    VendorRMAHeader = new List<VendorRMAHeader>(),
                    VendorRMAInvoiceDetails = new List<VendorRMAInvoiceDetail>()
                };
                var VendorRmaInwardDTO = new InvoiceDetailBO().GetRMADetailByRMANo(BRANCH_ID, DocumentNo);
                if (VendorRmaInwardDTO != null)
                {
                    VendorRMAHeader vendorRmaDetailObj = null;
                    var VendorRMAInvoiceDetail = VendorRmaInwardDTO.ToList().GroupBy(x => new
                    {
                        x.ModelNo
                    }).Select(x => new VendorRMAInvoiceDetail
                    {
                        ModelNo = x.Select(y => y.ModelNo).FirstOrDefault(),
                        RMANumber = DocumentNo,
                        Qty = x.Select(a => a.SerialNo).Count(),
                        Description = x.Select(b => b.Description).FirstOrDefault()
                    }).ToList();
                    vendorRMADTO.VendorRMAInvoiceDetails = VendorRMAInvoiceDetail;

                    for (var i = 0; i < VendorRmaInwardDTO.Count; i++)
                    {
                        vendorRmaDetailObj = new VendorRMAHeader() { };
                        vendorRmaDetailObj.DocumentNo = DocumentNo;
                        vendorRmaDetailObj.VendorInvoiceNo = VendorRmaInwardDTO[i].VendorInvoiceNo;
                        vendorRmaDetailObj.VendorName = VendorRmaInwardDTO[i].VendorName;
                        vendorRmaDetailObj.SerialNo = VendorRmaInwardDTO[i].SerialNo;
                        vendorRmaDetailObj.CompanyName = VendorRmaInwardDTO[i].CompanyName;
                        vendorRmaDetailObj.CustomerAddress = VendorRmaInwardDTO[i].CustomerAddress;
                        if (VendorRmaInwardDTO[i].CustomerWarrantyExpiryDate != null)
                        {
                            vendorRmaDetailObj.WarrantyExpiryDate = VendorRmaInwardDTO[i].CustomerWarrantyExpiryDate;
                            vendorRmaDetailObj.IsValid = (UTILITY.SINGAPORETIME < VendorRmaInwardDTO[i].CustomerWarrantyExpiryDate);
                        }
                        if (VendorRmaInwardDTO[i].VendorWarrantyExpiryDate != null)
                        {
                            vendorRmaDetailObj.RMAIsValid = (UTILITY.SINGAPORETIME < VendorRmaInwardDTO[i].VendorWarrantyExpiryDate);
                            vendorRmaDetailObj.VendorWarrantyExpiryDate = VendorRmaInwardDTO[i].VendorWarrantyExpiryDate;
                        }
                        vendorRMADTO.VendorRMAHeader.Add(vendorRmaDetailObj);
                    }
                }
                else
                {
                    return RedirectToAction("VendorRMA");
                }
                return View(vendorRMADTO);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult VendorPdf(String DoNo)
        {
            string RMANO = new VendorRMAHeaderBO().GetListByBranchID(BRANCH_ID).Where(z => z.DoNo == DoNo).Select(y => y.DocumentNo).FirstOrDefault();
            string InvoiceNo = new VendorRMAInvoiceHeaderBO().GetList(BRANCH_ID).Where(x => x.ReferenceNo == RMANO).Select(y => y.InvoiceNo).FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(InvoiceNo))
            {
                VendorRMAInvoiceHeader vendorRMAInvoiceHeader = new VendorRMAInvoiceHeader();
                vendorRMAInvoiceHeader.BranchID = BRANCH_ID;
                vendorRMAInvoiceHeader.InvoiceNo = InvoiceNo;
                var InvoiceDetails = new VendorRMAInvoiceHeaderBO().GetItem(vendorRMAInvoiceHeader);
                InvoiceDetails.CompanyName = new RMAHeaderBO().GetListByBranchID(BRANCH_ID).Where(x => x.DocumentNo == RMANO).Select(x => x.CompanyName).FirstOrDefault();
                InvoiceDetails.CustomerAddress = new RMAHeaderBO().GetListByBranchID(BRANCH_ID).Where(x => x.DocumentNo == RMANO).Select(x => x.CustomerAddress).FirstOrDefault();
                foreach (var item in InvoiceDetails.VendorRMAInvoiceDetails)
                {
                    item.Description = new ProductBO().GetList().Where(x => x.ModelNo == item.ModelNo).Select(x => x.Description).FirstOrDefault();
                }
                string HTMLContent = this.RenderView<VendorRMAInvoiceHeader>("~/Views/RMA/VendorDO.cshtml", InvoiceDetails);
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=" + "VendorDO.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(GetPDF2(HTMLContent));
                Response.End();
            }
            return RedirectToAction("VendorRMAOutwardList");
        }
        public static T CreateController<T>(RouteData routeData = null) where T : Controller, new()
        {

            T controller = new T();
            HttpContextBase wrapper;
            if (System.Web.HttpContext.Current != null)
                wrapper = new HttpContextWrapper(System.Web.HttpContext.Current);
            else
                throw new InvalidOperationException(
                    "Can't create Controller Context if no " +
                    "active HttpContext instance is available.");

            if (routeData == null)
                routeData = new RouteData();

            if (!routeData.Values.ContainsKey("controller") &&
                !routeData.Values.ContainsKey("Controller"))
                routeData.Values.Add("controller",
                                     controller.GetType()
                                               .Name.ToLower().Replace("controller", ""));

            controller.ControllerContext = new ControllerContext(wrapper, routeData, controller);
            return controller;
        }
        private string RenderView<T>(string path, T model)
        {
            var controller = CreateController<RMA.Web.Controllers.TestController>();
            RouteData route = new RouteData();
            System.Web.Mvc.ControllerContext newContext = new System.Web.Mvc.ControllerContext(new HttpContextWrapper(System.Web.HttpContext.Current), route, controller);
            newContext.RouteData.Values.Add("controller", "Fault");
            var html = RenderViewToString(newContext, path, model, true);

            return html;
        }
        private byte[] GetPDF2(string pHTML)
        {
            byte[] result;
            try
            {
                MemoryStream memoryStream = new MemoryStream();
                TextReader reader = new StringReader(pHTML);
                Document expr_2B = new Document(PageSize.A4, 25f, 25f, 25f, 25f);
                PdfWriter.GetInstance(expr_2B, memoryStream);
                HTMLWorker hTMLWorker = new HTMLWorker(expr_2B);
                expr_2B.Open();
                hTMLWorker.StartDocument();
                hTMLWorker.Parse(reader);
                hTMLWorker.EndDocument();
                hTMLWorker.Close();
                expr_2B.Close();
                result = memoryStream.ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public byte[] GetPDF(string pHTML)
        {
            byte[] bPDF = null;

            MemoryStream ms = new MemoryStream();
            TextReader txtReader = new StringReader(pHTML);

            // 1: create object of a itextsharp document class
            Document doc = new Document(PageSize.A4, 25, 25, 25, 25);

            // 2: we create a itextsharp pdfwriter that listens to the document and directs a XML-stream to a file
            PdfWriter oPdfWriter = PdfWriter.GetInstance(doc, ms);

            // 3: we create a worker parse the document
            HTMLWorker htmlWorker = new HTMLWorker(doc);

            // 4: we open document and start the worker on the document
            doc.Open();
            htmlWorker.StartDocument();

            // 5: parse the html into the document
            htmlWorker.Parse(txtReader);

            // 6: close the document and the worker
            htmlWorker.EndDocument();
            htmlWorker.Close();
            doc.Close();

            bPDF = ms.ToArray();

            return bPDF;
        }
        public ActionResult ProcessRMAInward(RMAHeader rmaHeader)
        {
            if (rmaHeader.rmaDetails == null)
            {
                return RedirectToAction("Inward");
            }
            else
            {
                rmaHeader.rmaDetails.ForEach(x =>
                {
                    x.SerialNo = Regex.Replace(x.SerialNo, @"\t|\n|\r", "");
                });

                for (var i = 0; i < rmaHeader.rmaDetails.Count; i++)
                {
                    //Commented by krishna 31/08/2017 
                    //  var rmaInwardDTO = new InvoiceDetailBO().GetVendorInvoiceDetailBySerialNo(rmaHeader.rmaDetails[i].SerialNo, BRANCH_ID, UTILITY.CONFIG_INVOICETYPE_VENDOR);//CONFIG_INVOICETYPE_CUSTOMER
                    var rmaInwardDTO = new InvoiceDetailBO().GetVendorInvoiceDetailBySerialNo(rmaHeader.rmaDetails[i].SerialNo, rmaHeader.CountryCode, BRANCH_ID, UTILITY.CONFIG_INVOICETYPE_CUSTOMER);//CONFIG_INVOICETYPE_CUSTOMER
                    if (rmaInwardDTO != null)
                    {
                        rmaHeader.rmaDetails[i].DocumentNo = rmaInwardDTO.DocumentNo;
                        rmaHeader.rmaDetails[i].ModelNo = rmaInwardDTO.ModelNo;
                        rmaHeader.rmaDetails[i].WarrantyExpiryDate = rmaInwardDTO.InvoiceDate.AddMonths(rmaInwardDTO.WarrantyPeriod);
                        rmaHeader.rmaDetails[i].IsValid = (UTILITY.SINGAPORETIME < rmaHeader.rmaDetails[i].WarrantyExpiryDate);
                    }
                    else
                    {
                        rmaHeader.rmaDetails[i].IsValid = false;
                    }
                }
            }

            ViewBag.countries = new BranchBO().GetList().Select(x => new
            {
                Value = x.BranchCode,
                Text = x.BranchName
            }).ToList();

            return View("Inward", rmaHeader);
        }
        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<ActionResult> SendEmail(RMAHeader model)
        //{
        //    var message = await EMailTemplate("WelcomeEmail");
        //    message = message.Replace("@ViewBag.Name", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.EmailID));
        //    await EmailGenerator.SendEmailAsync(model.EmailID, "Welcome!", message);
        //    return View("EmailSent");
        //}
        public ActionResult EmailSent()
        {
            return View();
        }
        public static async Task<string> EMailTemplate(string template)
        {
            var templateFilePath = HostingEnvironment.MapPath("~/Css/templates/") + template + ".cshtml";
            StreamReader objstreamreaderfile = new StreamReader(templateFilePath);
            var body = await objstreamreaderfile.ReadToEndAsync();
            objstreamreaderfile.Close();
            return body;
        }

        public ActionResult GenerateRMA(RMAHeader rmaHeader)
        {
            rmaHeader.rmaDetails = rmaHeader.rmaDetails.Where(x => x.IsValid == true).ToList();
            rmaHeader.Status = true;
            rmaHeader.CreatedBy = USER_ID;
            rmaHeader.CreatedOn = UTILITY.SINGAPORETIME;
            rmaHeader.ModifiedBy = USER_ID;
            rmaHeader.ModifiedOn = UTILITY.SINGAPORETIME;
            rmaHeader.IncidentDate = UTILITY.SINGAPORETIME;
            var result = new RMAHeaderBO().SaveRMAHeader(rmaHeader, BRANCH_ID);
            string documentNo = rmaHeader.rmaDetails.FirstOrDefault().DocumentNo;
            List<RMADetail> rmaDetails = new RMADetailBO().GetListByDocumentNo(documentNo);
            if (rmaHeader.rmaDetails != null)
            {
                string RMAValues = "";
                StringBuilder htmlStr = new StringBuilder("");
                htmlStr.Append("<left><h2 class='headings'>RMA Validation List</h2>");
                htmlStr.Append("<table border='1' cellpadding='3'>");
                int i = 1;
                htmlStr.Append("<tr>");
                htmlStr.Append("<th width='35px'>");
                htmlStr.Append("Sl No.");
                htmlStr.Append("</th>");
                htmlStr.Append("<th width='50px'>");
                htmlStr.Append("Model No.");
                htmlStr.Append("</th>");
                htmlStr.Append("<th width='50px'>");
                htmlStr.Append("Product Serial No.");
                htmlStr.Append("</th>");
                htmlStr.Append("</tr>");
                foreach (var detail in rmaHeader.rmaDetails)
                {
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td width='35px'>" + i++ + "</td>");
                    htmlStr.Append("<td width='50px'>" + detail.ModelNo + "</td>");
                    htmlStr.Append("<td width='50px'>" + detail.SerialNo + "</td>");
                    htmlStr.Append("</tr>");
                }

                htmlStr.Append("</table></left><br/>");
                RMAValues = htmlStr.ToString();
                var Email = new EmailGenerator().ConfigMail(rmaHeader.EmailID, true, documentNo, RMAValues);
                TempData["isSaved"] = Email;
            }
            return RedirectToAction("Inward");
        }
        public ActionResult GenerateVendorRMA(VendorRMAInvoiceHeader vendorRMADTO)
        {
            var vendorRMADetails = vendorRMADTO.VendorRMAHeader.ToList().GroupBy(x => new
            {
                x.VendorInvoiceNo
            }).Select(x => new VendorRMAHeaderList
            {
                VendorInvoiceNo = x.Select(y => y.VendorInvoiceNo).FirstOrDefault(),
                SerialNo = x.Select(z => z.SerialNo).ToList(),
                VendorName = x.Select(y => y.VendorName).FirstOrDefault(),
                WarrantyExpiryDate = x.Select(a => a.WarrantyExpiryDate).FirstOrDefault(),
                IsValid = x.Select(b => b.IsValid).FirstOrDefault(),
                RMAIsValid = x.Select(c => c.RMAIsValid).FirstOrDefault(),
                VendorWarrantyExpiryDate = x.Select(d => d.VendorWarrantyExpiryDate).FirstOrDefault(),
                DocumentNo = x.Select(e => e.DocumentNo).FirstOrDefault(),
                BranchID = BRANCH_ID,
                Status = true,
                CreatedBy = USER_ID,
                CreatedOn = UTILITY.SINGAPORETIME,
                ModifiedBy = USER_ID,
                ModifiedOn = UTILITY.SINGAPORETIME
            }).ToList();
            var result = new VendorRMAHeaderBO().SaveVendorRMAHeader(vendorRMADetails);
            return RedirectToAction("VendorRMA");
        }
        [HttpGet]
        public ActionResult VendorRMAOutwardList()
        {
            var list = new VendorRMAHeaderBO().GetListByBranchID(BRANCH_ID);
            return View("VendorOutwardRMA", list);
        }

        [HttpGet]
        public ActionResult OutwardList()
        {
            var list = new RMAHeaderBO().GetListByBranchID(BRANCH_ID);
            return View("OutwardList", list);
        }

        [HttpGet]
        public ActionResult GetRMAByDocumentNo(string documentNo)
        {
            RMAOutwardHeader RMAOutwardHeader = new RMAOutwardHeader();
            RMAOutwardHeader.RmaDetails = new RMAOutwardDetailBO().RMAOutWardDetailListByRMAInWardDocumentNo(documentNo, BRANCH_ID);
            RMAOutwardHeader.ReferenceNo = RMAOutwardHeader.RmaDetails.FirstOrDefault() != null ?
                                                 RMAOutwardHeader.RmaDetails.FirstOrDefault().DocumentNo : string.Empty;
            return View(RMAOutwardHeader);
        }

        [HttpGet]
        public ActionResult GetVendorRMAByDocumentNo(string documentNo)
        {
            VendorRMAOutwardHeader vendorRMAOutwardHeader = new VendorRMAOutwardHeader();
            vendorRMAOutwardHeader.VendorRmaDetails = new VendorRMAOutwardDetailBO().VendorRMAOutWardDetailListByRMAInWardDocumentNo(documentNo, BRANCH_ID);
            vendorRMAOutwardHeader.ReferenceNo = vendorRMAOutwardHeader.VendorRmaDetails.FirstOrDefault() != null ?
                                                 vendorRMAOutwardHeader.VendorRmaDetails.FirstOrDefault().DocumentNo : string.Empty;
            ViewBag.vProducts = new ProductBO().GetList()
                   .Where(x => x.Status == true)
                   .Select(x => new
                   {
                       Value = x.ProductCode,
                       Text = x.ProductCode
                   })
                   .ToList();
            return View(vendorRMAOutwardHeader);
        }


        [HttpPost]
        public ActionResult SaveRMAOutWard(RMAOutwardHeader header)
        {
            Func<RMAOutwardDetail, bool> WhereFunc = delegate (RMAOutwardDetail detail)
            {
                var NewSerialNo = detail.NewSerialNo ?? "";
                var IsCreditNote = detail.IsCreditNote;
                return NewSerialNo.Trim().Length > 0 || IsCreditNote;
            };
            var IsRecordUpdated = false;
            IsRecordUpdated = header.RmaDetails.Where(WhereFunc).Count() > 0;
            if (IsRecordUpdated)
            {
                header.BranchID = BRANCH_ID;
                header.DocumentDate = UTILITY.SINGAPORETIME;
                header.Status = true;
                header.CreatedBy = USER_ID;
                header.CreatedOn = UTILITY.SINGAPORETIME;
                header.ModifiedBy = USER_ID;
                header.ModifiedOn = UTILITY.SINGAPORETIME;

                header.RmaDetails.ForEach(x =>
                {
                    x.BranchID = BRANCH_ID;
                    x.CreatedBy = USER_ID;
                    x.CreatedOn = UTILITY.SINGAPORETIME;
                    x.ModifiedBy = USER_ID;
                    x.ModifiedOn = UTILITY.SINGAPORETIME;
                });

                var result = new RMAOutwardHeaderBO().SaveRMAOutwardHeader(header);
            }
            return RedirectToAction("OutwardList", "RMA");
        }
        [HttpPost]
        public ActionResult SaveVendorRMAOutWard(VendorRMAOutwardHeader vendorRMAOutwardHeader)
        {
            Func<VendorRMAOutwardDetail, bool> WhereFunc = delegate (VendorRMAOutwardDetail detail)
            {
                var NewSerialNo = detail.NewSerialNo ?? "";
                var IsCreditNote = detail.IsCreditNote;
                return NewSerialNo.Trim().Length > 0 || IsCreditNote;
            };

            var IsRecordUpdated = false;
            IsRecordUpdated = vendorRMAOutwardHeader.VendorRmaDetails.Where(WhereFunc).Count() > 0;

            if (IsRecordUpdated)
            {

                vendorRMAOutwardHeader.BranchID = BRANCH_ID;
                vendorRMAOutwardHeader.DocumentDate = UTILITY.SINGAPORETIME;
                vendorRMAOutwardHeader.Status = true;
                vendorRMAOutwardHeader.CreatedBy = USER_ID;
                vendorRMAOutwardHeader.CreatedOn = UTILITY.SINGAPORETIME;
                vendorRMAOutwardHeader.ModifiedBy = USER_ID;
                vendorRMAOutwardHeader.ModifiedOn = UTILITY.SINGAPORETIME;

                vendorRMAOutwardHeader.VendorRmaDetails.ForEach(x =>
                {
                    x.BranchID = BRANCH_ID;
                    x.CreatedBy = USER_ID;
                    x.CreatedOn = UTILITY.SINGAPORETIME;
                    x.ModifiedBy = USER_ID;
                    x.ModifiedOn = UTILITY.SINGAPORETIME;
                });

                var result = new VendorRMAOutwardHeaderBO().SaveVendorRMAOutwardHeader(vendorRMAOutwardHeader);
                if (result)
                {
                    var Result = new VendorRMAHeaderBO().CloseVendorRMAHeader(vendorRMAOutwardHeader.BranchID, vendorRMAOutwardHeader.ReferenceNo, vendorRMAOutwardHeader.DocumentNo);
                }
            }

            return RedirectToAction("VendorRMAOutwardList", "RMA");
        }
        /*
        [HttpPost]
        public JsonResult RMAOutwordList(DataTableObject Obj)
        {
            try
            {
                Obj.BranchID = BRANCH_ID;               

                var RMAOutwordList = new RMAOutwardHeaderBO().GetList(BRANCH_ID);
                return Json(new
                {
                    RMAOutwordList = RMAOutwordList                   
                    
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { }, JsonRequestBehavior.AllowGet);
            }

        }*/

        [HttpGet]
        [EncryptedActionParameter]
        public ActionResult Outword(string ReferenceNo = null, string documentNo = null)
        {
            if (string.IsNullOrWhiteSpace(documentNo))
                return View(new RMAOutwardHeader { });
            else
            {
                var OutwordHeader = new RMAOutwardHeaderBO().GetRMAOutwardHeader(new RMAOutwardHeader { BranchID = BRANCH_ID, DocumentNo = documentNo });
                return View(OutwordHeader);
            }
        }

    }
    public class Table : IDisposable
    {
        private StringBuilder _sb;
        public Table(StringBuilder sb)
        {
            _sb = sb;
            _sb.Append("<table>");
        }
        public void Dispose()
        {
            _sb.Append("</table>");
        }
        public Row AddRow()
        {
            return new Row(_sb);
        }
    }
    public class Row : IDisposable
    {
        private StringBuilder _sb;
        public Row(StringBuilder sb)
        {
            _sb = sb;
            _sb.Append("<tr>");
        }
        public void Dispose()
        {
            _sb.Append("</tr>");
        }
        public void AddCell(string innerText)
        {
            _sb.Append("<td>");
            _sb.Append(innerText);
            _sb.Append("</td>");
        }
    }
}
using EZY.RMAS.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using EZY.RMAS.BusinessFactory;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using System.Net.Http.Headers;
using System.Configuration;
using System.Data.OleDb;
using System.Data;



namespace RMA.Web.Controllers
{
    [WebSsnFilter]
    public class COOController : BaseController
    {
        public static int PageNo = 1;


        public ActionResult COOHeaderList()
        {
            var cooHeaderList = new COOHeaderBO().GetList(BRANCH_ID).OrderByDescending(x => x.DocumentNo).ToList();
            return View(cooHeaderList);
        }

        public ActionResult COOHeader(string DocumentNo = "")
        {
            var cooHeader = new COOHeader();

            var branchCode = new BranchBO().GetList().Where(x => x.BranchID == BRANCH_ID).Select(x => x).FirstOrDefault();

            var defaultDeclarant = new DeclarantBO().GetList(BRANCH_ID).Where(x => x.DeclarantName == RMA.Web.UTILITY.DEFAULT_DECLARANT).FirstOrDefault();
             




            var branchprofile = new BranchBO().GetBranch(new Branch { BranchCode = branchCode.BranchCode });

            // var companyFullName = new CompanyBO().GetCompany(new Company { CompanyCode = branchprofile.CompanyCode }).CompanyName;
            //var countryName = new CountryBO().GetCountry(new Country { CountryCode = branchprofile.BranchAddress.CountryCode }).CountryName;
            cooHeader.ExporterName = "EZY INFOTECH PTE LTD";// companyFullName;
            cooHeader.ExporterAddress = "1 CHANGI NORTH STREET 1 \n SINGAPORE 498789";//branchprofile.BranchAddress.Address1 + "\n" + countryName + "   " + branchprofile.BranchAddress.ZipCode;
            //cooHeader.ExporterAddress = branchprofile.BranchAddress.FullAddress.Replace(",", "\n");
            ViewBag.FileName = "";
            if (DocumentNo == "")
            {
                cooHeader.COODetails = new List<COODetail>(); 
                cooHeader.InvoiceDate = DateTime.Now;
                cooHeader.DepartureDate = DateTime.Now;
                cooHeader.CreatedOn = DateTime.Now;
                cooHeader.IsInvoiceConfirm = true;
                cooHeader.IsCertified = false;
                cooHeader.DeclarantName = "";
                cooHeader.Designation = "";
                cooHeader.DeclarationDate = DateTime.Now;

                if (defaultDeclarant != null)
                {
                    cooHeader.DeclarantName = defaultDeclarant.DeclarantName;
                    cooHeader.Designation = defaultDeclarant.Designation;

                }


            }
            else
            {
                cooHeader.BranchID = BRANCH_ID;
                cooHeader.DocumentNo = DocumentNo;
                cooHeader = new COOHeaderBO().GetCOOHeader(cooHeader);
            }


            return View(cooHeader);
        }


        [HttpPost]
        public ActionResult COOHeader(COOHeader cooHeader)
        {
            try
            {
                var appSettingsPath = ConfigurationManager.AppSettings["CooFileSharePath"];
                var folderPath = Server.MapPath(appSettingsPath);
                CreateDirectory(folderPath);
                var fileName = "";
                var Message = "";
                if (Request.Files.Count > 0 && Request.Files[0] != null
                    && ValidateFile(Request.Files[0], ref Message))
                {
                    var file = Request.Files[0];
                    if (file != null && file.ContentLength > 0)
                    {
                        fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath(appSettingsPath), fileName);
                        file.SaveAs(path);
                        var lstcoo = ProcessFile(path);

                        List<string> lstSerialNos = new List<string>();
                        List<string> returnItems = new List<string>();
                        foreach (var NewModelNo in lstcoo)
                        {
                            lstSerialNos.Add(NewModelNo.ModelNo);
                        }


                        if (cooHeader.COODetails != null)
                        {
                            foreach (var item in cooHeader.COODetails)
                            {
                                lstcoo.Add(item);
                            }
                        }

                        List<string> DuplicateSNos = new List<string>();

                        var cooList = lstcoo;
                        cooHeader.COODetails = new List<COODetail>();
                        for (var i = 0; i < cooList.Count; i++)
                        {
                            var tempObj = cooHeader.COODetails
                                            .Where(x => x.ModelNo == cooList[i].ModelNo && x.Description == cooList[i].Description && x.Origin == cooList[i].Origin && x.Qty == cooList[i].Qty)
                                            .FirstOrDefault();

                            if (tempObj == null)
                            {
                                cooHeader.COODetails.Add(cooList[i]);
                            }
                            else
                            {
                                DuplicateSNos.Add(tempObj.ModelNo);
                            }
                        }
                        if (DuplicateSNos.Count != 0)
                        {
                            ViewData["DuplicateSNo"] = DuplicateSNos;
                        }

                    }
                }

                return View("CooHeader", cooHeader);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Invalid file content")
                {
                    TempData["IsUploadFileErr"] = true;
                    //return RedirectToRoute(new { controller = "Error", action = "FileContent" });
                    return View("CooHeader", cooHeader);
                }
                    
            }

            return View();
            
        }


        public PartialViewResult ADDCOODetails()
        {
            ViewBag.Title = "New COO Details";
            return PartialView();
        }


        private bool ValidateFile(HttpPostedFileBase fileControl, ref string message)
        {
            bool validationStatus = true;

            string fileName = Server.HtmlEncode(fileControl.FileName);
            string extension = System.IO.Path.GetExtension(fileName);

            if (extension.ToLower() == ".xls" || extension.ToLower() == ".xlsx")
            {

            }
            else
            {
                message = "Invalid File Format.";
                return !validationStatus;
            }

            return validationStatus;
        }


        private List<COODetail> ProcessFile(string fileName)
        {
            List<COODetail> lstInvoice = new List<COODetail>();

            var DA = new OleDbDataAdapter();
            var DS = new DataSet();
            var ext = Path.GetExtension(fileName);
            var _fileName = Path.GetFileName(fileName);
            var strxlsProvider = string.Empty;
            short rowCount;

            if (ext.ToLower() == ".xls")
                strxlsProvider = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties=Excel 8.0;";
            else if (ext.ToLower() == ".xlsx")
                strxlsProvider = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties=Excel 12.0;";


            var objXlsConnection = new OleDbConnection(strxlsProvider);

            try
            {

                objXlsConnection.Open();
                if (objXlsConnection.State == ConnectionState.Closed)
                    return null;
                DataTable dt = null;
                dt = objXlsConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                if (dt == null)
                {
                    return null;
                }

                String[] excelSheetNames = new String[dt.Rows.Count];
                int i = 0;

                foreach (DataRow row in dt.Rows)
                {
                    excelSheetNames[i] = row["TABLE_NAME"].ToString().Trim('\'');
                    i++;
                }

                foreach (var sheet in excelSheetNames)
                {
                    sheet.Trim('\'');

                    var objCmd = new OleDbCommand("Select * From [" + sheet + "]", objXlsConnection);
                    objCmd.CommandType = CommandType.Text;

                    DA.SelectCommand = objCmd;
                    DA.Fill(DS, "XLSData");

                    rowCount = Convert.ToInt16(DS.Tables[0].Rows.Count);

                    var tableHeaderValue = DS.Tables[0].Columns[0].ColumnName;
                    if (Convert.ToInt32(DS.Tables[0].Columns.Count) == 4)
                    {
                        if (DS.Tables[0].Columns[0].ColumnName.ToString().ToLower() == "model")//&& DS.Tables[0].Columns[1].ColumnName.ToString().ToLower() == "description" && DS.Tables[0].Columns[2].ColumnName.ToString().ToLower() == "quantity (in pcs)" && DS.Tables[0].Columns[3].ColumnName.ToString().ToLower() == "origin")
                        {
                            foreach (DataRow row in DS.Tables[0].Rows)
                            {
                                var currentItem = new COODetail();

                                if (row[0].ToString() != "")
                                {
                                    try
                                    {
                                        currentItem.ModelNo = row[0].ToString().Trim();
                                        currentItem.Description = row[1].ToString().Trim();
                                        currentItem.Qty = Convert.ToInt16(row[2].ToString().Trim());
                                        currentItem.Origin = row[3].ToString().Trim();

                                        lstInvoice.Add(currentItem);
                                    }
                                    catch (Exception)
                                    {
                                        var myEx = new Exception("Invalid file content");
                                        throw myEx;
                                    }
                                    
                                }
                            }
                            ViewBag.FileName = _fileName;
                        }
                        else
                        {
                            ViewBag.WrongFileContent = true;
                        }
                    }
                    else
                    {
                        ViewBag.WrongFileContent = true;
                    }
                }


                return lstInvoice;
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex.Message;
                throw ex;
            }
            finally
            {
                objXlsConnection.Close();
            }
        }


        [HttpPost]
        public ActionResult SaveCOODetails(COODetail cooDetails)
        {
            cooDetails.BranchID = BRANCH_ID;
            var result = new COODetailBO().SaveCOODetail(cooDetails);

            return RedirectToAction("COODetailsList", "COO");
        }

        [HttpPost]
        public ActionResult SaveCOOHeader(COOHeader cooHeader)
        {
            cooHeader.BranchID = BRANCH_ID;

            //if (cooHeader.IsInvoiceConfirm == true)
            //    cooHeader.IsCertified = false;
            //else if (cooHeader.IsCertified == true)
            //    cooHeader.IsInvoiceConfirm = false;

            var result = new COOHeaderBO().SaveCOOHeader(cooHeader);
            var latestDocumentNo = "";
            if (result)
            {
                TempData["COOSaved"] = result;
                var list = new COOHeaderBO().GetList(BRANCH_ID).OrderByDescending(x => x.DocumentNo).Select(x => x.DocumentNo).ToList().Take(1);
                latestDocumentNo = cooHeader.DocumentNo != null ? cooHeader.DocumentNo : list.ToList()[0];
            }
            return RedirectToAction("EditCOOHeader", "COO", new { DocumentNumber = latestDocumentNo });
        }

        [HttpPost]
        public JsonResult GetCOOHeaderbySearch(COOSearch coo)
        {
            var result = new COOHeaderBO().SearchList(BRANCH_ID, coo.VesselName, coo.ConsigneeName, coo.FromDate, coo.ToDate).OrderByDescending(x => x.DocumentNo).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditCOOHeader(string DocumentNumber)
        {
            var cooHeader = new COOHeader();
            cooHeader.BranchID = BRANCH_ID;
            cooHeader.DocumentNo = DocumentNumber;
            var cooHeaderDetails = new COOHeaderBO().GetCOOHeader(cooHeader);
            return View("COOHeader", cooHeaderDetails);
        }

        public ActionResult DeleteCOOHeader(string DocumentNumber)
        {
            var cooHeader = new COOHeader();
            cooHeader.BranchID = BRANCH_ID;
            cooHeader.DocumentNo = DocumentNumber;
            var result = new COOHeaderBO().DeleteCOOHeader(cooHeader);

            TempData["COODeleted"] = result;

            return RedirectToAction("COOHeaderList", "COO");
        }


        public ActionResult GetCOODetails(string DocumentNumber)
        {
            var cooDetail = new COODetail();
            cooDetail.BranchID = BRANCH_ID;
            cooDetail.DocumentNo = DocumentNumber;
            var result = new COODetailBO().GetCOODetail(cooDetail);
            return View(result);
        }



        [HttpPost]
        public JsonResult GetCOOs(DataTableObject Obj)
        {
            var cooList = new COOHeaderBO().GetList(BRANCH_ID).OrderByDescending(x => x.DocumentNo).ToList();
            var totalRecords = new COOHeaderBO().GetList(BRANCH_ID).Count();

            return Json(new { coos = cooList, totalRecords = totalRecords }, JsonRequestBehavior.AllowGet);
        }


        public FileResult PrintCOOHeaderReport(string branchID, string documentNo)
        {
            PageNo = 1;
            try
            {
                var outputPdfStream = new MemoryStream();
                using (Document document = new Document())
                {
                    using (PdfSmartCopy copy = new PdfSmartCopy(document, outputPdfStream))
                    {
                        document.Open();
                        AddDataSheets(copy, branchID, documentNo);
                    }
                }

                byte[] bytesInStream = outputPdfStream.ToArray(); // simpler way of converting to array
                outputPdfStream.Close();

                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=" + documentNo + ".pdf");
                Response.Buffer = true;
                Response.BinaryWrite(bytesInStream);
                Response.End();

                return File(bytesInStream, "application/pdf");
            }
            catch (Exception ex)
            {
                throw;
            }

        }


        public void AddDataSheets(PdfCopy copy, string branchID, string documentNo)
        {
            COOHeader cooHeader = new COOHeader();
            cooHeader.BranchID = BRANCH_ID;
            cooHeader.DocumentNo = documentNo;
            var cooHeaderItem = new COOHeaderBO().GetCOOHeader(cooHeader);


            var path = "";
            int cooDetailsCount = cooHeaderItem.COODetails.Count();
            int value = 0;
            for (int i = 0; i < cooHeaderItem.COODetails.Count();)
            {

                path = System.Web.Hosting.HostingEnvironment.MapPath("~/COOHeaderTemplate/COOPdf.pdf");

                PdfReader reader = new PdfReader(path);

                value = (cooDetailsCount - i) % 15;

                if (value > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        using (PdfStamper stamper = new PdfStamper(reader, ms))
                        {
                            Fill(stamper.AcroFields, cooHeaderItem.COODetails, i, 15, cooHeaderItem);
                            stamper.FormFlattening = true;
                        }
                        reader = new PdfReader(ms.ToArray());
                        copy.AddPage(copy.GetImportedPage(reader, 1));
                    }
                    i = i + 15;
                    PageNo = PageNo + 1;
                }
            }
        }

        public static void Fill(AcroFields pdfFormFields, List<COODetail> cOODetails, int dcount, int validcount, COOHeader cooHeader)
        {
            try
            {
                var IsConfirmation = "";
                if (cooHeader.IsInvoiceConfirm == true)
                    IsConfirmation = "THIS C/O IS APPLIED TO INVOICE NO.  " + cooHeader.InvoiceNo + "           Date:  " + cooHeader.InvoiceDate.ToShortDateString() + "";
                else
                    IsConfirmation = "WE HEREBY CERTIFY THAT THE ABOVE MENTIONED GOODS ARE ORIGIN OF ";

                pdfFormFields.SetField("ExporterName", cooHeader.ExporterName + "\n" + cooHeader.ExporterAddress);  //\nExch: " + declarationHeader.CurrencyExRate.ToString("#0.0000")
                pdfFormFields.SetField("ConsigneeName", cooHeader.ConsigneeName + (cooHeader.ConsigneeAddress != null ? "\n" + cooHeader.ConsigneeAddress : ""));

                pdfFormFields.SetField("DepartureDate", cooHeader.DepartureDate.ToShortDateString());
                pdfFormFields.SetField("VesselName", cooHeader.VesselName);
                pdfFormFields.SetField("PortOfDischarge", cooHeader.DestinationPort);
                pdfFormFields.SetField("CountryOfFinalDestination", cooHeader.DestinationCountry);
                pdfFormFields.SetField("CountryOfOriginOfGoods", " As Below ");
                pdfFormFields.SetField("Name", cooHeader.DeclarantName);
                pdfFormFields.SetField("Designation", cooHeader.Designation);
                pdfFormFields.SetField("Date", cooHeader.CreatedOn.ToShortDateString());
                //pdfFormFields.SetField("InvoiceNo", cooHeader.InvoiceNo != null ? cooHeader.InvoiceNo.ToString() : "");
                //pdfFormFields.SetField("InvoiceDate", (cooHeader.IsCertified != true && cooHeader.InvoiceDate != null) ? cooHeader.InvoiceDate.ToShortDateString() : "");
                pdfFormFields.SetField("MODEL", "MODEL");
                pdfFormFields.SetField("DESCRIPTION", "DESCRIPTION");
                pdfFormFields.SetField("QUANTITY", "QUANTITY(Pieces)");
                pdfFormFields.SetField("ORIGIN", "ORIGIN");
                pdfFormFields.SetField("PageNo", PageNo.ToString());

                var country1 = "";
                var country2 = "";

                if (cooHeader.IsInvoiceConfirm != true)
                {
                    if (cooHeader.Country1 != null)
                    {
                        country1 = cooHeader.Country1.ToString();
                    }
                    if (cooHeader.Country2 != null)
                    {
                        country1 = country1 + ", " + cooHeader.Country2.ToString() ;
                    }


                    if (cooHeader.Country3 != null)
                    {
                        country2 = cooHeader.Country3.ToString();
                    }
                    if (cooHeader.Country4 != null)
                    {
                        country2 = country2 + ", " + cooHeader.Country4.ToString();
                    }
                    if (cooHeader.Country5 != null)
                    {
                        country2 = country2 + ", " + cooHeader.Country5.ToString();
                    }
                    if (cooHeader.Country6 != null)
                    {
                        country2 = country2 + ", " + cooHeader.Country6.ToString();
                    }

                }

                pdfFormFields.SetField("IsConformation", IsConfirmation.ToString() + country1);
                pdfFormFields.SetField("Country2", cooHeader.IsInvoiceConfirm != true ? country2 : "");



                //pdfFormFields.SetField("Country1", cooHeader.IsInvoiceConfirm != true ? cooHeader.Country1 != null ? cooHeader.Country1.ToString() : "" : "");
                //pdfFormFields.SetField("Country2", cooHeader.IsInvoiceConfirm != true ? cooHeader.Country2 != null ? cooHeader.Country2.ToString() : "" : "");
                //pdfFormFields.SetField("Country3", cooHeader.IsInvoiceConfirm != true ? cooHeader.Country3 != null ? cooHeader.Country3.ToString() : "" : "");
                //pdfFormFields.SetField("Country4", cooHeader.IsInvoiceConfirm != true ? cooHeader.Country4 != null ? cooHeader.Country4.ToString() : "" : "");
                //pdfFormFields.SetField("Country5", cooHeader.IsInvoiceConfirm != true ? cooHeader.Country5 != null ? cooHeader.Country5.ToString() : "" : "");
                //pdfFormFields.SetField("Country6", cooHeader.IsInvoiceConfirm != true ? cooHeader.Country6 != null ? cooHeader.Country6.ToString() : "" : "");



                int count = dcount;
                for (int i = 0; i < validcount; i++)
                {
                    if (count < cooHeader.COODetails.Count)
                    {
                        pdfFormFields.SetField("model" + i + "", cooHeader.COODetails[count].ModelNo.ToString());
                        pdfFormFields.SetField("desc" + i + "", cooHeader.COODetails[count].Description.ToString());
                        pdfFormFields.SetField("qty" + i + "", cooHeader.COODetails[count].Qty.ToString());
                        pdfFormFields.SetField("org" + i + "", cooHeader.COODetails[count].Origin.ToString());
                    }
                    count++;
                }

            }
            catch (Exception ex)
            {
                throw;
            }

        }

        [HttpPost]
        public JsonResult GetConsigneeNameAddress(string consigneeName)
        {
            var customerList = new CustomerBO().GetList(BRANCH_ID).Where(x => x.CustomerName.Contains(consigneeName)).Select(x => x).FirstOrDefault();
            var customerDetails = new CustomerBO().GetCustomer(new Customer { BranchID = BRANCH_ID, CustomerCode = customerList.CustomerCode });
            var countryName = new CountryBO().GetCountry(new Country { CountryCode = customerDetails.CountryCode }).CountryName;
            //var fullAddress = customerDetails.Address1 + "\n" + customerDetails.Address2 + "\n" + customerDetails.State + "\n" + countryName + "\n" + customerDetails.PostCode;
            var consigneeAddress = new ConsigneeAddress() { CustomerName = customerDetails.CustomerName, Address1 = customerDetails.Address1, Address2 = customerDetails.Address2, State = customerDetails.State, Country = countryName, PostCode = (customerDetails.PostCode != "NA" && customerDetails.PostCode != "NP") ? customerDetails.PostCode : "" };
            return Json(consigneeAddress, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult GetConsigneeNames(string term)
        {
            //Note : you can bind same list from database  
            var customerNameList = new CustomerBO().GetList(BRANCH_ID).Where(x => x.CustomerType == 103).ToList();// .Where(x => x.CustomerName.StartsWith(term)).ToList();
            var customerNames = (from N in customerNameList
                                 where N.CustomerName.ToUpper().StartsWith(term.ToUpper())
                                 select new { CustomerName = N.CustomerName }).ToList();//customerNameList.Where(x => x.CustomerName.StartsWith(term)).ToList();

            return Json(customerNames, JsonRequestBehavior.AllowGet);
        }

    }

    public class COOSearch
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string VesselName { get; set; }
        public string ConsigneeName { get; set; }

    }

    public class ConsigneeAddress
    {
        public string CustomerName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostCode { get; set; }
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

using EZY.RMAS.Contract;
using EZY.RMAS.BusinessFactory;
using System.IO;
using System.Data.OleDb;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Text;

namespace RMA.Web.Controllers
{

    [WebSsnFilter]
    public class InvoiceController : BaseController
    {
        #region VendorInvoice
        [HttpGet]
        public ActionResult VendorInvoiceList()
        {
            return View("VendorInvoiceList", new List<InvoiceHeader>());
        }
        [HttpPost]
        public JsonResult CheckInvoiceNo(string invoiceNo)
        {
            if (Session["value"] != null)
            {
                string r = Session["value"].ToString();
                if (r == invoiceNo)
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var totalRecords = new InvoiceHeaderBO().GetList(BRANCH_ID)
                        .Where(x => x.InvoiceNo == invoiceNo).Count();
                    if (totalRecords > 0)
                    {
                        return Json(false, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            else
            {
                var totalRecords = new InvoiceHeaderBO().GetList(BRANCH_ID)
                       .Where(x => x.InvoiceNo == invoiceNo).Count();
                if (totalRecords > 0)
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }

        }
        [HttpPost]
        public JsonResult VendorInvoiceList(DataTableObject Obj)
        {
            try
            {
                Obj.BranchID = BRANCH_ID;
                var vendorInvoiceList = new InvoiceHeaderBO()
                    .GetVendorInvoiceDataTableList(Obj, UTILITY.CONFIG_INVOICETYPE_VENDOR)
                    .Select(x => new
                    {
                        DocumentNo = x.DocumentNo,
                        InvoiceNo = x.InvoiceNo,
                        CusInvoiceNo = x.CusInvoiceNo,
                        CustomerCode = x.CustomerCode,
                        CustomerName = x.CustomerName,
                        ProductCode = x.ProductCode,
                        ProductName = x.ProductName,
                        InvoiceDate = x.InvoiceDate.ToShortDateString(),
                        WarrantyExpiryDate = x.WarrantyExpiryDate.ToShortDateString(),
                        Quantity = x.Quantity,
                        UnitPrice = x.UnitPrice,
                        EncryptStr = UrlEncryptionHelper.Encrypt(string.Format("invoiceNo={0}?documentNo={1}", x.InvoiceNo, x.DocumentNo))
                    }).ToList();

                var totalRecords = new InvoiceHeaderBO().GetList(BRANCH_ID).Where(x => x.InvoiceType == UTILITY.CONFIG_INVOICETYPE_VENDOR && x.Status == true).Count();
                return Json(new
                {
                    vendorInvoiceList = vendorInvoiceList,
                    totalRecords = totalRecords
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { }, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpGet]
        [EncryptedActionParameter]
        public ActionResult VendorInvoice(string invoiceNo = null, string documentNo = null)
        {
            ViewBag.Currency = new LookupBO().GetList().Where(x => x.LookupCategory == UTILITY.CONFIG_LOOKUPCATEGORY_CURRENCY)
                                         .Select(x => new
                                         {
                                             Value = x.LookupCode,
                                             Text = x.LookupDescription
                                         }).ToList();
            ViewBag.productCategory = new LookupBO()
                                     .GetList()
                                     .Where(x => x.LookupCategory == "CategoryGroup" && x.Status == true)
                                     .Select(x => new
                                     {
                                         Value = x.LookupID,
                                         Text = x.LookupCode
                                     })
                                     .ToList();
            CustomerInvoiceDataSetUp(UTILITY.CONFIG_CUSTOMERTYPE_VENDOR);
            if (string.IsNullOrWhiteSpace(invoiceNo))

                return View(new InvoiceHeader { InvoiceDate = DateTime.Now });
            else {
                Session["value"] = invoiceNo;
                var invoiceHeader = new InvoiceHeaderBO().GetInvoiceHeader(new InvoiceHeader { BranchID = BRANCH_ID, InvoiceNo = invoiceNo, DocumentNo = documentNo, InvoiceType = UTILITY.CONFIG_INVOICETYPE_VENDOR });
                var expireDt = invoiceHeader.InvoiceDate.AddMonths(invoiceHeader.WarrantyPeriod);
                ViewBag.ExpireMsg = string.Format("Your invoice will expire on {0}", expireDt.ToString("dd/MM/yyyy"));

                ViewBag.vProducts = new ProductBO().GetList()
                    .Where(x => x.ProductCategory == invoiceHeader.ProductCategory && x.Status == true)
                    .Select(x => new
                    {
                        Value = x.ProductCode,
                        Text = x.ProductCode
                    })
                    .ToList();

                ViewBag.vModels = new ProductBO().GetList()
                        .Where(x => x.ProductCode == invoiceHeader.ProductCode && x.ProductCategory == invoiceHeader.ProductCategory && x.Status == true)
                        .Select(x => new
                        {
                            Value = x.ModelNo,
                            Text = x.Description
                        })
                        .ToList();
                return View(invoiceHeader);
            }
        }

        [HttpPost]
        public ActionResult VendorInvoice(InvoiceHeader invoiceHeader)
        { 
            invoiceHeader.InvoiceDate = UTILITY.SINGAPORETIME;
            invoiceHeader.InvoiceType = UTILITY.CONFIG_INVOICETYPE_VENDOR;

            var ProductCategory = invoiceHeader.ProductCategory;
            var ProductCode = invoiceHeader.ProductCode;

            var appSettingsPath = ConfigurationManager.AppSettings["VendorInputFileSharePath"];
            var folderPath = Server.MapPath(appSettingsPath);
            CreateDirectory(folderPath);
            var Message = "";
            if (Request.Files.Count > 0 && Request.Files[0] != null
                && ValidateFile(Request.Files[0], ref Message))
            {
                var file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath(appSettingsPath), fileName);
                    file.SaveAs(path);
                    var lstInvoice = ProcessFile(path);

                    List<string> lstSerialNos = new List<string>();
                    List<string> returnItems = new List<string>();
                    
                    foreach (var NewSerialNo in lstInvoice)
                    {
                        lstSerialNos.Add(NewSerialNo.SerialNo);
                    }

                    returnItems = new InvoiceHeaderBO().CheckSerailNumberForVendor(BRANCH_ID, ProductCategory, ProductCode, lstSerialNos);

                    if (returnItems.Count > 0)
                        {
                            foreach (var item in returnItems)
                            {
                                lstInvoice.RemoveAll(x => x.SerialNo == item);
                                ViewBag.Dupsno = true;
                            }
                        }
                    
                    if (invoiceHeader.InvoiceDetailItems != null)
                    {
                        foreach (var item in invoiceHeader.InvoiceDetailItems)
                        {
                            lstInvoice.Add(item);
                        }
                    }
                   // invoiceHeader.Quantity =Convert.ToInt16(lstInvoice.Count);
                    
                    invoiceHeader.FileName = fileName;

                    List<string> DuplicateSNos = new List<string>();
                    if (invoiceHeader.Quantity == lstInvoice.Count + returnItems.Count)
                    {
                        var invoiceList = lstInvoice;
                        invoiceHeader.InvoiceDetailItems = new List<InvoiceDetail>();
                        for (var i = 0; i < invoiceList.Count; i++)
                        {
                            var tempObj = invoiceHeader.InvoiceDetailItems
                                            .Where(x => x.SerialNo == invoiceList[i].SerialNo)
                                            .FirstOrDefault();

                            if (tempObj == null)
                            {
                                invoiceHeader.InvoiceDetailItems.Add(invoiceList[i]);
                            }
                            else
                            {
                                DuplicateSNos.Add(tempObj.SerialNo);
                            }
                        }
                        if (DuplicateSNos.Count != 0)
                        {
                            ViewData["DuplicateSNo"] = DuplicateSNos;
                        }
                    }
                    else
                        ViewBag.QuantityMismatched = true;

                }
            }
            else
            {
                invoiceHeader.FileName = "SCAN";
                if (invoiceHeader.InvoiceDetailItems == null || invoiceHeader.Quantity != invoiceHeader.InvoiceDetailItems.Count)
                {
                    ViewBag.QuantityMismatched = true;
                }
                List<string> lstSerialNos = new List<string>();
                List<string> returnItems = new List<string>();
                foreach(var NewSerialNo in invoiceHeader.InvoiceDetailItems.Where(x=>x.DocumentNo==null))
                {
                    lstSerialNos.Add(NewSerialNo.SerialNo);
                    //invoiceHeader.InvoiceDetailItems.ForEach(x => lstSerialNos.Add(x.SerialNo));
                }
                returnItems = new InvoiceHeaderBO().CheckSerailNumberForVendor(BRANCH_ID, ProductCategory, ProductCode, lstSerialNos);

                if (returnItems.Count > 0)
                {
                    foreach (var item in returnItems)
                    {
                        invoiceHeader.InvoiceDetailItems.RemoveAll(x => x.SerialNo == item);
                        ViewBag.Dupsno = true;
                    }
                }
                invoiceHeader.Quantity = Convert.ToInt16(invoiceHeader.InvoiceDetailItems.Count());
            }
            invoiceHeader.Status = true;
            invoiceHeader.CreatedBy = USER_ID;
            invoiceHeader.ModifiedBy = USER_ID;

            ViewBag.vProducts = new ProductBO().GetList()
                    .Where(x => x.ProductCategory == invoiceHeader.ProductCategory && x.Status == true)
        .Select(x => new
        {
            Value = x.ProductCode,
            Text = x.ProductCode
        })
                    .ToList();

            ViewBag.vModels = new ProductBO().GetList()
                    .Where(x => x.ProductCode == invoiceHeader.ProductCode && x.ProductCategory == invoiceHeader.ProductCategory && x.Status == true)
        .Select(x => new
        {
            Value = x.ModelNo,
            Text = x.Description
        })
                    .ToList();
            ViewBag.Currency = new LookupBO().GetList().Where(x => x.LookupCategory == UTILITY.CONFIG_LOOKUPCATEGORY_CURRENCY)
                                       .Select(x => new
                                       {
                                           Value = x.LookupCode,
                                           Text = x.LookupDescription
                                       }).ToList();
            CustomerInvoiceDataSetUp(UTILITY.CONFIG_CUSTOMERTYPE_VENDOR);
            return View("VendorInvoice", invoiceHeader);

        }

        [HttpPost]
        public ActionResult SaveVendorInvoice(InvoiceHeader invoiceHeader)
        {
            invoiceHeader.InvoiceDetailItems.ForEach(x =>
            {
                x.SerialNo = Regex.Replace(x.SerialNo, @"\t|\n|\r", "");
            });
            string nzTimeZoneKey = "SE Asia Standard Time";
            TimeZoneInfo nzTimeZone = TimeZoneInfo.FindSystemTimeZoneById(nzTimeZoneKey);
            //string formattedDate = invoiceHeader.InvoiceDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
            //DateTime invoiceDate = DateTime.ParseExact(invoiceHeader.InvoiceDate.ToString(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            invoiceHeader.InvoiceDate = invoiceHeader.InvoiceDate; //UTILITY.SINGAPORETIME;
            invoiceHeader.InvoiceType = UTILITY.CONFIG_INVOICETYPE_VENDOR;
            invoiceHeader.Status = true;
            invoiceHeader.CreatedBy = USER_ID;
            invoiceHeader.ModifiedBy = USER_ID;
            invoiceHeader.BranchID = BRANCH_ID;
            var warrantyexpirydate = invoiceHeader.InvoiceDate.AddMonths(invoiceHeader.WarrantyPeriod);
            invoiceHeader.InvoiceDetailItems.ForEach(dt =>
            {
                dt.InvoiceNo = invoiceHeader.InvoiceNo;
                dt.ModelNo = invoiceHeader.ModelNo;
                dt.Make = "";
                dt.ExpiryDate = warrantyexpirydate;
            });
            if(invoiceHeader.DocumentNo == null)
            {
                invoiceHeader.InvoiceDetailItems = invoiceHeader.InvoiceDetailItems.Where(x => x.RecordCount != 1).ToList();
            }
            else
            {
                foreach(var item in invoiceHeader.InvoiceDetailItems.Where(x=>x.RecordCount==2))
                {
                    var totalRecords = new InvoiceHeaderBO().DeleteSerialNo(item.SerialNo);
                }
                invoiceHeader.InvoiceDetailItems = invoiceHeader.InvoiceDetailItems.Where(x => x.RecordCount != 1 && x.RecordCount != 2).ToList();
            }
            var result = new InvoiceHeaderBO().SaveInvoiceHeader(invoiceHeader);
            TempData["VendorSaved"] = result;
            return RedirectToAction("VendorInvoiceList");
        }
        [HttpGet]
        [EncryptedActionParameter]
        public ActionResult DeleteVendorInvoice(string invoiceNo = null, string documentNo = null)
        {
            try
            {
                InvoiceHeader invoiceHeader = new InvoiceHeader()
                {
                    InvoiceNo = invoiceNo,
                    DocumentNo = documentNo,
                    InvoiceType = UTILITY.CONFIG_INVOICETYPE_VENDOR,
                    BranchID = BRANCH_ID
                };

                bool isDetaled = new InvoiceHeaderBO().DeleteInvoiceHeader(invoiceHeader);
                return RedirectToAction("VendorInvoiceList");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public JsonResult DeleteSerialNo(string serialNo)
        {
            var totalRecords = new InvoiceHeaderBO().DeleteSerialNo(serialNo);
            return Json(totalRecords, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region CustomerInvoice
        [HttpGet]
        public ActionResult CustomerInvoiceList()
        {
            //var i = TempData["ErrorMessage"];
            return View("CustomerInvoiceList", new List<InvoiceHeader>());
        }

        [HttpPost]
        public JsonResult CustomerInvoiceList(DataTableObject Obj)
        {
            Obj.BranchID = BRANCH_ID;
            var customerInvoiceList = new InvoiceHeaderBO()
                .GetVendorInvoiceDataTableList(Obj, UTILITY.CONFIG_INVOICETYPE_CUSTOMER)
                .Select(x => new
                {
                    DocumentNo = x.DocumentNo,
                    InvoiceNo = x.InvoiceNo,
                    CustomerCode = x.CustomerCode,
                    CustomerName = x.CustomerName,
                    ProductCode = x.ProductCode,
                    ProductName = x.ProductName,
                    InvoiceDate = x.InvoiceDate.ToShortDateString(),
                    WarrantyExpiryDate = x.WarrantyExpiryDate.ToShortDateString(),
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice,
                    EncryptStr = UrlEncryptionHelper.Encrypt(string.Format("invoiceNo={0}?documentNo={1}", x.InvoiceNo, x.DocumentNo))
                }).ToList();

            var totalRecords = new InvoiceHeaderBO().GetList(BRANCH_ID).Where(x => x.InvoiceType == UTILITY.CONFIG_INVOICETYPE_CUSTOMER).Count();
            return Json(new
            {
                customerInvoiceList = customerInvoiceList,
                totalRecords = totalRecords
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ProductsByCategory(short productCategory)
        {
            var products = new ProductBO().GetList()
                                .Where(x => x.ProductCategory == productCategory && x.Status == true)
                                .Select(x => new
                                {
                                    Value = x.ProductCode,
                                    Text = x.ProductCode
                                })
                                .ToList();

            return Json(products, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ModelsByProductCode(ProductDesc obj)
        {
            var models = new ProductBO().GetList()
                                .Where(x => x.ProductCode == obj.ProductCode && x.ProductCategory == obj.ProductCategory && x.Status == true)
                                .Select(x => new
                                {
                                    Value = x.ModelNo,
                                    Text = x.Description
                                })
                                .ToList();

            return Json(models, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CheckCustomerInvoiceNo(string invoiceNo)
        {
            var totalRecords = new InvoiceHeaderBO().GetList(BRANCH_ID).Where(x => x.InvoiceNo == invoiceNo).Count();
            if (totalRecords > 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpGet]
        [EncryptedActionParameter]
        public ActionResult CustomerInvoice(string invoiceNo = null, string documentNo = null)
        {
            ViewBag.Currency = new LookupBO().GetList().Where(x => x.LookupCategory == UTILITY.CONFIG_LOOKUPCATEGORY_CURRENCY && x.Status == true)
                                        .Select(x => new
                                        {
                                            Value = x.LookupCode,
                                            Text = x.LookupDescription
                                        }).ToList();
            CustomerInvoiceDataSetUp(UTILITY.CONFIG_CUSTOMERTYPE_CUSTOMER);
            if (string.IsNullOrWhiteSpace(invoiceNo))
                return View(new InvoiceHeader { InvoiceDate = DateTime.Now });
            else {
                var invoiceHeader = new InvoiceHeaderBO().GetInvoiceHeader(new InvoiceHeader { BranchID = BRANCH_ID, InvoiceNo = invoiceNo, DocumentNo = documentNo, InvoiceType = UTILITY.CONFIG_INVOICETYPE_CUSTOMER });
                var expireDt = invoiceHeader.InvoiceDate.AddMonths(invoiceHeader.WarrantyPeriod);
                ViewBag.ExpireMsg = string.Format("Your invoice will expire on {0}", expireDt.ToString("dd/MM/yyyy"));
                ViewBag.vProducts = new ProductBO().GetList()
                    .Where(x => x.ProductCategory == invoiceHeader.ProductCategory && x.Status == true)
                    .Select(x => new
                    {
                        Value = x.ProductCode,
                        Text = x.ProductCode
                    })
                    .ToList();

                ViewBag.vModels = new ProductBO().GetList()
                        .Where(x => x.ProductCode == invoiceHeader.ProductCode && x.ProductCategory == invoiceHeader.ProductCategory && x.Status == true)
                        .Select(x => new
                        {
                            Value = x.ModelNo,
                            Text = x.Description
                        })
                        .ToList();
                return View(invoiceHeader);
            }
        }
        [HttpPost]
        public ActionResult CustomerInvoice(InvoiceHeader invoiceHeader)
        {
            invoiceHeader.InvoiceDate = UTILITY.SINGAPORETIME;
            invoiceHeader.InvoiceType = UTILITY.CONFIG_INVOICETYPE_CUSTOMER;

            var appSettingsPath = ConfigurationManager.AppSettings["CustomerInputFileSharePath"];
            var folderPath = Server.MapPath(appSettingsPath);
            CreateDirectory(folderPath);
            var Message = "";

            if (Request.Files.Count > 0 && Request.Files[0] != null
            && invoiceHeader.InvoiceDetailItems == null
            && ValidateFile(Request.Files[0], ref Message))
            {
                var file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath(appSettingsPath), fileName);
                    file.SaveAs(path);

                    var lstInvoice = ProcessFile(path);
                    invoiceHeader.FileName = fileName;


                    if (invoiceHeader.Quantity == lstInvoice.Count)
                    {
                        invoiceHeader.InvoiceDetailItems = lstInvoice;
                        List<string> serialNoList = new List<string>();
                        List<string> returnItems = new List<string>();

                        invoiceHeader.InvoiceDetailItems.ForEach(x => serialNoList.Add(x.SerialNo));

                        returnItems = new InvoiceHeaderBO().CheckSerailNumber(BRANCH_ID, invoiceHeader.ProductCategory, invoiceHeader.ProductCode, serialNoList);

                        if (returnItems.Count > 0)
                        {
                            @TempData["IsNotExist"] = true;
                            ViewBag.SerialNos = returnItems;
                        }
                    }
                    else
                        ViewBag.QuantityMismatched = true;
                }
            }
            else
            {
                invoiceHeader.FileName = "SCAN";
                List<string> serialNoList = new List<string>();
                List<string> returnItems = new List<string>();

                invoiceHeader.InvoiceDetailItems.ForEach(x => serialNoList.Add(x.SerialNo));

                returnItems = new InvoiceHeaderBO().CheckSerailNumber(BRANCH_ID, invoiceHeader.ProductCategory, invoiceHeader.ProductCode, serialNoList);

                if (returnItems.Count > 0)
                {
                    @TempData["IsNotExist"] = true;
                    ViewBag.SerialNos = returnItems;
                }
                if (invoiceHeader.Quantity != invoiceHeader.InvoiceDetailItems.Count)
                {
                    ViewBag.QuantityMismatched = true;
                }
            }


            invoiceHeader.Status = true;
            invoiceHeader.CreatedBy = USER_ID;
            invoiceHeader.ModifiedBy = USER_ID;

            ViewBag.vProducts = new ProductBO().GetList()
                    .Where(x => x.ProductCategory == invoiceHeader.ProductCategory && x.Status == true)
                    .Select(x => new
                    {
                        Value = x.ProductCode,
                        Text = x.ProductCode
                    })
                    .ToList();

            ViewBag.vModels = new ProductBO().GetList()
                    .Where(x => x.ProductCode == invoiceHeader.ProductCode && x.ProductCategory == invoiceHeader.ProductCategory && x.Status == true)
                    .Select(x => new
                    {
                        Value = x.ModelNo,
                        Text = x.Description
                    })
                    .ToList();
            ViewBag.Currency = new LookupBO().GetList().Where(x => x.LookupCategory == UTILITY.CONFIG_LOOKUPCATEGORY_CURRENCY && x.Status == true)
                                        .Select(x => new
                                        {
                                            Value = x.LookupCode,
                                            Text = x.LookupDescription
                                        }).ToList();
            CustomerInvoiceDataSetUp(UTILITY.CONFIG_CUSTOMERTYPE_CUSTOMER);



            return View("CustomerInvoice", invoiceHeader);

        }

        [HttpPost]
        public ActionResult SaveCustomerInvoice(InvoiceHeader invoiceHeader)
        {
            invoiceHeader.InvoiceDetailItems.ForEach(x =>
            {
                x.SerialNo = Regex.Replace(x.SerialNo, @"\t|\n|\r", "");
            });

            invoiceHeader.InvoiceDate = invoiceHeader.InvoiceDate;//UTILITY.SINGAPORETIME;
            invoiceHeader.InvoiceType = UTILITY.CONFIG_INVOICETYPE_CUSTOMER;
            invoiceHeader.Status = true;
            invoiceHeader.CreatedBy = USER_ID;
            invoiceHeader.ModifiedBy = USER_ID;
            invoiceHeader.BranchID = BRANCH_ID;

            var warrantyexpirydate = invoiceHeader.InvoiceDate.AddMonths(invoiceHeader.WarrantyPeriod);
            invoiceHeader.InvoiceDetailItems.ForEach(dt =>
            {
                dt.InvoiceNo = invoiceHeader.InvoiceNo;
                dt.ModelNo = invoiceHeader.ModelNo;
                dt.Make = "";
                dt.ExpiryDate = warrantyexpirydate;
            });
            invoiceHeader.InvoiceDetailItems = invoiceHeader.InvoiceDetailItems.Where(x => x.SerialNo != "Deleted").ToList();

            //List<string> serialNoList = new List<string>();
            //List<string> returnItems = new List<string>();

            //invoiceHeader.InvoiceDetailItems.ForEach(x => serialNoList.Add(x.SerialNo));

            //returnItems = new InvoiceHeaderBO().CheckSerailNumber(invoiceHeader.BranchID, invoiceHeader.ProductCategory, invoiceHeader.ProductCode, serialNoList);

            //if (returnItems.Count > 0)
            //{
            //    @TempData["IsNotExist"] = true;
            //    return RedirectToAction("CustomerInvoice");
            //}
            //else
            //{
            var result = new InvoiceHeaderBO().SaveInvoiceHeader(invoiceHeader);
            TempData["isCustomerSave"] = result;
            //}
            return RedirectToAction("CustomerInvoiceList");
        }

        #endregion

        [HttpGet]
        public ActionResult SerialNoInvoice()
        {
            return View();
        }
        [HttpGet]
        public ActionResult MaterialsEnquiry()
        {
            List<MaterialsEnquiryDetailsDTO> materialsEnquiryDetails = new List<MaterialsEnquiryDetailsDTO>();
            return View(materialsEnquiryDetails);
        }
        public ActionResult FindSerialNoStatus(List<MaterialsEnquiryDetailsDTO> materialsEnquiryDetails)
        {
            var Message = "";
            if (Request.Files.Count > 0 && Request.Files[0] != null && ValidateFile(Request.Files[0], ref Message))
            {
                var file = Request.Files[0];
                var appSettingsPath = ConfigurationManager.AppSettings["VendorInputFileSharePath"];
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath(appSettingsPath), fileName);
                    file.SaveAs(path);
                }
            }
                    if (materialsEnquiryDetails == null)
            {
                return RedirectToAction("MaterialsEnquiry");
            }
            else
            {
                for (var i = 0; i < materialsEnquiryDetails.Count; i++)
                {
                    var MaterialSNoDetails = new InvoiceDetailBO().GetDetailBySerialNo(materialsEnquiryDetails[i].SerialNo, BRANCH_ID);

                    if (MaterialSNoDetails != null)
                    {
                        materialsEnquiryDetails[i].VendorExpiryDate = MaterialSNoDetails.VendorExpiryDate;
                        materialsEnquiryDetails[i].VendorIsValid = (UTILITY.SINGAPORETIME < MaterialSNoDetails.VendorExpiryDate);
                        materialsEnquiryDetails[i].CustomerExpiryDate = MaterialSNoDetails.CustomerExpiryDate;
                        materialsEnquiryDetails[i].CustomerIsvalid = (UTILITY.SINGAPORETIME < MaterialSNoDetails.CustomerExpiryDate);
                    }
                    else
                    {
                        materialsEnquiryDetails[i].VendorIsValid = false;
                    }
                }
                return View("MaterialsEnquiry", materialsEnquiryDetails);


            }
        }
        private List<InvoiceDetail> ProcessFile(string fileName)
        {
            List<InvoiceDetail> lstInvoice = new List<InvoiceDetail>();

            var DA = new OleDbDataAdapter();
            var DS = new DataSet();
            var ext = Path.GetExtension(fileName);
            var strxlsProvider = string.Empty;
            short rowCount;

            if (ext.ToLower() == ".xls")
                strxlsProvider = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties=Excel 8.0";
            else if (ext.ToLower() == ".xlsx")
                strxlsProvider = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties=Excel 12.0;";

            var objXlsConnection = new OleDbConnection(strxlsProvider);

            try
            {
                objXlsConnection.Open();
                if (objXlsConnection.State == ConnectionState.Closed)
                    return null;

                var objCmd = new OleDbCommand("Select * From [Sheet1$]", objXlsConnection);
                objCmd.CommandType = CommandType.Text;

                DA.SelectCommand = objCmd;
                DA.Fill(DS, "XLSData");

                rowCount = Convert.ToInt16(DS.Tables[0].Rows.Count);

                var tableHeaderValue = DS.Tables[0].Columns[0].ColumnName;
                lstInvoice.Add(new InvoiceDetail { SerialNo = tableHeaderValue.Trim() });
                foreach (DataRow row in DS.Tables[0].Rows)
                {
                    var currentItem = new InvoiceDetail();

                    currentItem.SerialNo = row[0].ToString().Trim();//row["Part #"].ToString();
                                                                    //currentItem.SerialNo = row["Serial Number"].ToString();
                    if(currentItem.SerialNo!="")
                    lstInvoice.Add(currentItem);
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

        private void CustomerInvoiceDataSetUp(short CustomerType)
        {
            ViewBag.productsList = new ProductBO()
                                    .GetList()
                                    .Where(x => x.Status == true)
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.ProductCode,
                                        Text = x.Description
                                    })
                                    .OrderBy(x => x.Text)
                                    .ToList();

            ViewBag.vendorsList = new CustomerBO()
                                    .GetList(BRANCH_ID)
                                    .Where(x => x.CustomerType == CustomerType && x.Status == true)
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.CustomerCode,
                                        Text = x.CustomerName
                                    })
                                    .OrderBy(x => x.Text)
                                    .ToList();

            ViewBag.productCategory = new LookupBO()
                                        .GetList()
                                        .Where(x => x.LookupCategory == "CategoryGroup" && x.Status == true)
                                        .Select(x => new
                                        {
                                            Value = x.LookupID,
                                            Text = x.LookupCode
                                        })
                                        .ToList();

            var WarentyPeriodList = new List<SelectListItem>();

            for (var i = 0; i < 6; i++)
            {
                var item = new SelectListItem
                {
                    Value = (i + 1).ToString(),
                    Text = (i + 1).ToString()
                };

                WarentyPeriodList.Add(item);
            }

            var item12 = new SelectListItem
            {
                Value = "12",
                Text = "12"
            };
            WarentyPeriodList.Add(item12);

            var item18 = new SelectListItem
            {
                Value = "18",
                Text = "18"
            };
            WarentyPeriodList.Add(item18);

            var item24 = new SelectListItem
            {
                Value = "24",
                Text = "24"
            };
            WarentyPeriodList.Add(item24);

            var item36 = new SelectListItem
            {
                Value = "36",
                Text = "36"
            };
            WarentyPeriodList.Add(item36);

            var item48 = new SelectListItem
            {
                Value = "48",
                Text = "48"
            };
            WarentyPeriodList.Add(item48);

            var item60 = new SelectListItem
            {
                Value = "60",
                Text = "60"
            };
            WarentyPeriodList.Add(item60);

            ViewBag.WarentyPeriodList = WarentyPeriodList;
        }
    }
    public class ProductDesc
    {
        public Int16 ProductCategory { get; set; }

        public string ProductCode { get; set; }
    }

    public class InvoiceVm
    {
        public string InvoiceNo { get; set; }

        public Int16 InvoiceType { get; set; }

        public DateTime InvoiceDate { get; set; }

        public string CustomerCode { get; set; }

        public string ProductCode { get; set; }

        public Int16 WarrantyPeriod { get; set; }

        public bool Status { get; set; }

        public HttpPostedFileBase file { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public List<InvoiceDetail> InvoiceDetailItems { get; set; }
    }
}
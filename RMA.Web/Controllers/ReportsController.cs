using EZY.RMAS.BusinessFactory;
using EZY.RMAS.Contract;
using RMA.Web;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace RMA.Web.Reports.Controllers
{

    [WebSsnFilter]
    public class ReportsController : BaseController
    {
            //string url = "MaterialInwardSSRSReport";
            //NetworkCredential nwc = new NetworkCredential("ragsarma-001", "n0ki@3310", "ifc");
            //WebClient client = new WebClient();
            //client.Credentials = nwc;
            //string reportURL = string.Format("{0}?{1}{2}&rs:Command=Render&rs:Format=PDF&rc:Toolbar=false&rc:Parameters=false&BranchID={3}&DateFrom={4}&DateTo={5}", "http://sql5002.smarterasp.net/ReportServer",
            //    "/ragsarma-001/RMAReports/", url, branchID, dateFrom, dateTo);
            //var cd = new System.Net.Mime.ContentDisposition
            //{
            //    FileName = "Report.pdf",
            //    Inline = false,
            //};
            //Response.AppendHeader("Content-Disposition", cd.ToString());
            //return File(client.DownloadData(reportURL), "application/pdf");
        public ActionResult MaterialInwardProductQtyMonthWise(ReportParams reportParams)
        {
            reportParams.BranchID = BRANCH_ID;
            reportParams.DateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month,1).AddMonths(-1);
            reportParams.DateTo = DateTime.Now.Date;
            reportParams.Year = Convert.ToInt16(DateTime.Now.Year);
            reportParams.ProductCode = 0;
            ViewBag.Year = new DashBoardBO().GetYear().Select(x => new { Value = x.Year, Text = x.Year });
            ViewBag.Branches = new BranchBO().GetList().Select(x => new { Value = x.BranchID, Text = x.BranchName });
            var ProductCategory = new LookupBO()
                                       .GetList()
                                       .Where(x => x.LookupCategory == "CategoryGroup" && x.Status == true)
                                       .Select(x => new SelectListItem
                                       {
                                           Value = x.LookupID.ToString(),
                                           Text = x.LookupCode
                                       })
                                       .ToList();
            ProductCategory.Add(new SelectListItem
            {
                Text = "ALL",
                Value = 0.ToString()
            });
            ViewBag.productCategory = ProductCategory;
            ViewBag.Login = BRANCH_ID;
            return View("MaterialInwardProductQty", reportParams);
        }
        public ActionResult ViewMaterialInwardProductQtyMonthWise(int? BranchID, string dateFrom , string dateTo,int? Year,int? ProductCode,string URL)
        {
            ViewBag.BranchID = BranchID;
            ViewBag.Year = Year;
            ViewBag.DateFrom = dateFrom;
            ViewBag.DateTo = dateTo;
            ViewBag.ProductCode = ProductCode;
            ViewBag.ReportSource = "MaterialInwardProductQty";
            ViewBag.Url = string.Format("{0}{1}", UTILITY.REPORTSUBFOLDER, URL);
            var path = "~/Views/Shared/_ReportViewerPartial.cshtml";
            return PartialView(path);
        }
        public ActionResult MaterialInwardProductCostMonthWise(ReportParams reportParams)
        {
            reportParams.BranchID = BRANCH_ID;
            reportParams.DateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);
            reportParams.DateTo = DateTime.Now.Date;
            reportParams.Year = Convert.ToInt16(DateTime.Now.Year);
            reportParams.ProductCode = 0;
            ViewBag.Year = new DashBoardBO().GetYear().Select(x => new { Value = x.Year, Text = x.Year });
            ViewBag.Branches = new BranchBO().GetList().Select(x => new { Value = x.BranchID, Text = x.BranchName });
            var ProductCategory = new LookupBO()
                                       .GetList()
                                       .Where(x => x.LookupCategory == "CategoryGroup" && x.Status == true)
                                       .Select(x => new SelectListItem
                                       {
                                           Value = x.LookupID.ToString(),
                                           Text = x.LookupCode
                                       })
                                       .ToList();
            ProductCategory.Add(new SelectListItem
            {
                Text = "ALL",
                Value = 0.ToString()
            });
            ViewBag.productCategory = ProductCategory;
            ViewBag.Login = BRANCH_ID;
            return View("MaterialInwardProductCost", reportParams);
        }
        public ActionResult ViewMaterialInwardProductCostMonthWise(int? BranchID, string dateFrom, string dateTo, int? Year, int? ProductCode, string URL)
        {
            ViewBag.BranchID = BranchID;
            ViewBag.Year = Year;
            ViewBag.DateFrom = dateFrom;
            ViewBag.DateTo = dateTo;
            ViewBag.ProductCode = ProductCode;
            ViewBag.ReportSource = "MaterialInwardProductCost";
            ViewBag.Url = string.Format("{0}{1}", UTILITY.REPORTSUBFOLDER, URL);
            var path = "~/Views/Shared/_ReportViewerPartial.cshtml";
            return PartialView(path);
        }
        public ActionResult MaterialInwardTotalInvoicesProductMonthWise(ReportParams reportParams)
        {
            reportParams.BranchID = BRANCH_ID;
            reportParams.DateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);
            reportParams.DateTo = DateTime.Now.Date;
            reportParams.Year = Convert.ToInt16(DateTime.Now.Year);
            reportParams.ProductCode = 0;
            ViewBag.Year = new DashBoardBO().GetYear().Select(x => new { Value = x.Year, Text = x.Year });
            ViewBag.Branches = new BranchBO().GetList().Select(x => new { Value = x.BranchID, Text = x.BranchName });
            var ProductCategory = new LookupBO()
                                       .GetList()
                                       .Where(x => x.LookupCategory == "CategoryGroup" && x.Status == true)
                                       .Select(x => new SelectListItem
                                       {
                                           Value = x.LookupID.ToString(),
                                           Text = x.LookupCode
                                       })
                                       .ToList();
            ProductCategory.Add(new SelectListItem
            {
                Text = "ALL",
                Value = 0.ToString()
            });
            ViewBag.productCategory = ProductCategory;
            ViewBag.Login = BRANCH_ID;
            return View("MaterialInwardTotalInvoicesProductWise", reportParams);
        }
        public ActionResult ViewMaterialInwardTotalInvoicesProductWise(int? BranchID, string dateFrom, string dateTo, int? Year, int? ProductCode, string URL)
        {
            ViewBag.BranchID = BranchID;
            ViewBag.Year = Year;
            ViewBag.DateFrom = dateFrom;
            ViewBag.DateTo = dateTo;
            ViewBag.ProductCode = ProductCode;
            ViewBag.ReportSource = "MaterialInwardTotalInvoicesProductWise";
            ViewBag.Url = string.Format("{0}{1}", UTILITY.REPORTSUBFOLDER, URL);
            var path = "~/Views/Shared/_ReportViewerPartial.cshtml";
            return PartialView(path);
        }

        //Outward Reports

        public ActionResult MaterialOutwardProductQtyMonthWise(ReportParams reportParams)
        {
            reportParams.BranchID = BRANCH_ID;
            reportParams.DateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);
            reportParams.DateTo = DateTime.Now.Date;
            reportParams.Year = Convert.ToInt16(DateTime.Now.Year);
            reportParams.ProductCode = 0;
            ViewBag.Year = new DashBoardBO().GetYear().Select(x => new { Value = x.Year, Text = x.Year });
            ViewBag.Branches = new BranchBO().GetList().Select(x => new { Value = x.BranchID, Text = x.BranchName });
            var ProductCategory = new LookupBO()
                                       .GetList()
                                       .Where(x => x.LookupCategory == "CategoryGroup" && x.Status == true)
                                       .Select(x => new SelectListItem
                                       {
                                           Value = x.LookupID.ToString(),
                                           Text = x.LookupCode
                                       })
                                       .ToList();
            ProductCategory.Add(new SelectListItem
            {
                Text = "ALL",
                Value = 0.ToString()
            });
            ViewBag.productCategory = ProductCategory;
            ViewBag.Login = BRANCH_ID;
            return View("MaterialOutwardProductQtyMonthWise", reportParams);
        }


        public ActionResult ViewMaterialOutwardProductQtyMonthWise(int? BranchID, string dateFrom, string dateTo, int? Year, int? ProductCode, string URL)
        {
            ViewBag.BranchID = BranchID;
            ViewBag.Year = Year;
            ViewBag.FromDate = dateFrom;
            ViewBag.ToDate = dateTo;
            ViewBag.ProductCode = ProductCode;
            ViewBag.ReportSource = "MaterialOutwardProductQty";
            ViewBag.Url = string.Format("{0}{1}", UTILITY.REPORTSUBFOLDER, URL);
            var path = "~/Views/Shared/_ReportViewerPartial.cshtml";
            return PartialView(path);
        }


        public ActionResult MaterialOutwardProductCostMonthWise(ReportParams reportParams)
        {
            reportParams.BranchID = BRANCH_ID;
            reportParams.DateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);
            reportParams.DateTo = DateTime.Now.Date;
            reportParams.Year = Convert.ToInt16(DateTime.Now.Year);
            reportParams.ProductCode = 0;
            ViewBag.Year = new DashBoardBO().GetYear().Select(x => new { Value = x.Year, Text = x.Year });
            ViewBag.Branches = new BranchBO().GetList().Select(x => new { Value = x.BranchID, Text = x.BranchName });
            var ProductCategory = new LookupBO()
                                       .GetList()
                                       .Where(x => x.LookupCategory == "CategoryGroup" && x.Status == true)
                                       .Select(x => new SelectListItem
                                       {
                                           Value = x.LookupID.ToString(),
                                           Text = x.LookupCode
                                       })
                                       .ToList();
            ProductCategory.Add(new SelectListItem
            {
                Text = "ALL",
                Value = 0.ToString()
            });
            ViewBag.productCategory = ProductCategory;
            ViewBag.Login = BRANCH_ID;
            return View("MaterialOutwardProductCostMonthWise", reportParams);
        }


        public ActionResult ViewMaterialOutwardProductCostMonthWise(int? BranchID, string dateFrom, string dateTo, int? Year, int? ProductCode, string URL)
        {
            ViewBag.BranchID = BranchID;
            ViewBag.Year = Year;
            ViewBag.FromDate = dateFrom;
            ViewBag.ToDate = dateTo;
            ViewBag.ProductCode = ProductCode;
            ViewBag.ReportSource = "MaterialOutwardProductCost";
            ViewBag.Url = string.Format("{0}{1}", UTILITY.REPORTSUBFOLDER, URL);
            var path = "~/Views/Shared/_ReportViewerPartial.cshtml";
            return PartialView(path);
        }

        public ActionResult MaterialOutwardTotalInvoicesProductMonthWise(ReportParams reportParams)
        {
            reportParams.BranchID = BRANCH_ID;
            reportParams.DateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);
            reportParams.DateTo = DateTime.Now.Date;
              reportParams.Year = Convert.ToInt16(DateTime.Now.Year);
            reportParams.ProductCode = 0;
            ViewBag.Year = new DashBoardBO().GetYear().Select(x => new { Value = x.Year, Text = x.Year });
            ViewBag.Branches = new BranchBO().GetList().Select(x => new { Value = x.BranchID, Text = x.BranchName });
            var ProductCategory = new LookupBO()
                                       .GetList()
                                       .Where(x => x.LookupCategory == "CategoryGroup" && x.Status == true)
                                       .Select(x => new SelectListItem
                                       {
                                           Value = x.LookupID.ToString(),
                                           Text = x.LookupCode
                                       })
                                       .ToList();
            ProductCategory.Add(new SelectListItem
            {
                Text = "ALL",
                Value = 0.ToString()
            });
            ViewBag.productCategory = ProductCategory;
            ViewBag.Login = BRANCH_ID;
            return View("MaterialOutwardTotalInvoicesProductMonthWise", reportParams);
        }


        public ActionResult ViewMaterialOutwardTotalInvoicesProductMonthWise(int? BranchID, string dateFrom, string dateTo, int? Year, int? ProductCode, string URL)
        {
            ViewBag.BranchID = BranchID;
            ViewBag.Year = Year;
            ViewBag.FromDate = dateFrom;
            ViewBag.ToDate = dateTo;
            ViewBag.ProductCode = ProductCode;
            ViewBag.ReportSource = "MaterialOutwardTotalInvoicesProductMonthWise";
            ViewBag.Url = string.Format("{0}{1}", UTILITY.REPORTSUBFOLDER, URL);
            var path = "~/Views/Shared/_ReportViewerPartial.cshtml";
            return PartialView(path);
        }

        // RMA Transactions
        public ActionResult RMATransactionReceived(RMAGenerationReceiveDTO rmaCreditNoteDTO)
        {
            rmaCreditNoteDTO.Branch = BRANCH_ID;
            rmaCreditNoteDTO.FromMonth = Convert.ToInt16(1);
            rmaCreditNoteDTO.ToMonth = Convert.ToInt16(DateTime.Now.Month);
            rmaCreditNoteDTO.Year = Convert.ToInt16(DateTime.Now.Year);
            rmaCreditNoteDTO.ProductCode = 0;
            ViewBag.Year = new DashBoardBO().GetYear().Select(x => new { Value = x.Year, Text = x.Year });
            ViewBag.Months = new SelectList(Enumerable.Range(1, 12).Select(x => new SelectListItem() { Text = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[x - 1], Value = x.ToString() }), "Value", "Text");
            ViewBag.Branches = new BranchBO().GetList().Select(x => new { Value = x.BranchID, Text = x.BranchName });
            var ProductCategory = new LookupBO()
                                       .GetList()
                                       .Where(x => x.LookupCategory == "CategoryGroup" && x.Status == true)
                                       .Select(x => new SelectListItem
                                       {
                                           Value = x.LookupID.ToString(),
                                           Text = x.LookupCode
                                       })
                                       .ToList();
            ProductCategory.Add(new SelectListItem
            {
                Text = "ALL",
                Value = 0.ToString()
            });
            ViewBag.productCategory = ProductCategory;
            ViewBag.Login = BRANCH_ID;
            return View("RMATransactionReceived", rmaCreditNoteDTO);
        }


        public ActionResult ViewRMATransactionReceived(int? BranchID, int? FromMonth, int? ToMonth, int? Year, int? ProductCode, string URL)
        {
            ViewBag.BranchID = BranchID;
            ViewBag.Year = Year;
            ViewBag.FromMonth = FromMonth;
            ViewBag.ToMonth = ToMonth;
            ViewBag.ProductCode = ProductCode;
            ViewBag.ReportSource = "RMATransactionReceived";
            ViewBag.Url = string.Format("{0}{1}", UTILITY.REPORTSUBFOLDER, URL);
            var path = "~/Views/Shared/_ReportViewerPartial.cshtml";
            return PartialView(path);
        }

        public ActionResult RMATransactionReturn(RMAGenerationReceiveDTO rmaCreditNoteDTO)
        {
            rmaCreditNoteDTO.Branch = BRANCH_ID;
            rmaCreditNoteDTO.FromMonth = Convert.ToInt16(1);
            rmaCreditNoteDTO.ToMonth = Convert.ToInt16(DateTime.Now.Month);
            rmaCreditNoteDTO.Year = Convert.ToInt16(DateTime.Now.Year);
            rmaCreditNoteDTO.ProductCode = 0;
            ViewBag.Year = new DashBoardBO().GetYear().Select(x => new { Value = x.Year, Text = x.Year });
            ViewBag.Months = new SelectList(Enumerable.Range(1, 12).Select(x => new SelectListItem() { Text = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[x - 1], Value = x.ToString() }), "Value", "Text");
            ViewBag.Branches = new BranchBO().GetList().Select(x => new { Value = x.BranchID, Text = x.BranchName });
            var ProductCategory = new LookupBO()
                                       .GetList()
                                       .Where(x => x.LookupCategory == "CategoryGroup" && x.Status == true)
                                       .Select(x => new SelectListItem
                                       {
                                           Value = x.LookupID.ToString(),
                                           Text = x.LookupCode
                                       })
                                       .ToList();
            ProductCategory.Add(new SelectListItem
            {
                Text = "ALL",
                Value = 0.ToString()
            });
            ViewBag.productCategory = ProductCategory;
            ViewBag.Login = BRANCH_ID;
            return View("RMATransactionReturn", rmaCreditNoteDTO);
        }


        public ActionResult ViewRMATransactionReturn(int? BranchID, int? FromMonth, int? ToMonth, int? Year, int? ProductCode, string URL)
        {
            ViewBag.BranchID = BranchID;
            ViewBag.Year = Year;
            ViewBag.FromMonth = FromMonth;
            ViewBag.ToMonth = ToMonth;
            ViewBag.ProductCode = ProductCode;
            ViewBag.ReportSource = "RMATransactionReturn";
            ViewBag.Url = string.Format("{0}{1}", UTILITY.REPORTSUBFOLDER, URL);
            var path = "~/Views/Shared/_ReportViewerPartial.cshtml";
            return PartialView(path);
        }

        // ProductsCategoryRMAReceiptQuantity and Value
        public ActionResult ProductsCategoryRMAReceiptQty(ProductCategoryRMAReceiptandValueDTO productCategoryRMAPriceandValueDTO)
        {
            productCategoryRMAPriceandValueDTO.Branch = BRANCH_ID;
            productCategoryRMAPriceandValueDTO.FromMonth = Convert.ToInt16(1);
            productCategoryRMAPriceandValueDTO.ToMonth = Convert.ToInt16(DateTime.Now.Month);
            productCategoryRMAPriceandValueDTO.Year = Convert.ToInt16(DateTime.Now.Year);
            productCategoryRMAPriceandValueDTO.ProductCode = 0;
            productCategoryRMAPriceandValueDTO.ProductCategoryRMAReceipt = new DashBoardBO().GetProductCategoryRMAReceipt(productCategoryRMAPriceandValueDTO);
            productCategoryRMAPriceandValueDTO.ProductCategoryRMAValue = new DashBoardBO().GetProductCategoryRMAValue(productCategoryRMAPriceandValueDTO);
            ViewBag.Branches = new BranchBO().GetList().Select(x => new { Value = x.BranchID, Text = x.BranchName });
            ViewBag.Year = new DashBoardBO().GetRMAYear().Select(x => new { Value = x.Year, Text = x.Year });
            ViewBag.Months = new SelectList(Enumerable.Range(1, 12).Select(x => new SelectListItem() { Text = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[x - 1], Value = x.ToString() }), "Value", "Text");
            var ProductCategory = new LookupBO()
                                        .GetList()
                                        .Where(x => x.LookupCategory == "CategoryGroup" && x.Status == true)
                                        .Select(x => new SelectListItem
                                        {
                                            Value = x.LookupID.ToString(),
                                            Text = x.LookupCode
                                        })
                                        .ToList();
            ProductCategory.Add(new SelectListItem
            {
                Text = "ALL",
                Value = 0.ToString()
            });
            ViewBag.productCategory = ProductCategory;
            ViewBag.Login = BRANCH_ID;
            return View(productCategoryRMAPriceandValueDTO);
        }


        public ActionResult ViewProductsCategoryRMAReceiptQty(int? BranchID, int? FromMonth, int? ToMonth, int? Year, int? ProductCode, string URL)
        {
            ViewBag.BranchID = BranchID;
            ViewBag.Year = Year;
            ViewBag.FromMonth = FromMonth;
            ViewBag.ToMonth = ToMonth;
            ViewBag.ProductCode = ProductCode;
            ViewBag.ReportSource = "ProductsCategoryRMAReceiptQty";
            ViewBag.Url = string.Format("{0}{1}", UTILITY.REPORTSUBFOLDER, URL);
            var path = "~/Views/Shared/_ReportViewerPartial.cshtml";
            return PartialView(path);
        }


        public ActionResult ProductsCategoryRMAReceiptValue(ProductCategoryRMAReceiptandValueDTO productCategoryRMAPriceandValueDTO)
        {
            productCategoryRMAPriceandValueDTO.Branch = BRANCH_ID;
            productCategoryRMAPriceandValueDTO.FromMonth = Convert.ToInt16(1);
            productCategoryRMAPriceandValueDTO.ToMonth = Convert.ToInt16(DateTime.Now.Month);
            productCategoryRMAPriceandValueDTO.Year = Convert.ToInt16(DateTime.Now.Year);
            productCategoryRMAPriceandValueDTO.ProductCode = 0;
            productCategoryRMAPriceandValueDTO.ProductCategoryRMAReceipt = new DashBoardBO().GetProductCategoryRMAReceipt(productCategoryRMAPriceandValueDTO);
            productCategoryRMAPriceandValueDTO.ProductCategoryRMAValue = new DashBoardBO().GetProductCategoryRMAValue(productCategoryRMAPriceandValueDTO);
            ViewBag.Branches = new BranchBO().GetList().Select(x => new { Value = x.BranchID, Text = x.BranchName });
            ViewBag.Year = new DashBoardBO().GetYear().Select(x => new { Value = x.Year, Text = x.Year });
            ViewBag.Months = new SelectList(Enumerable.Range(1, 12).Select(x => new SelectListItem() { Text = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[x - 1], Value = x.ToString() }), "Value", "Text");
            var ProductCategory = new LookupBO()
                                        .GetList()
                                        .Where(x => x.LookupCategory == "CategoryGroup" && x.Status == true)
                                        .Select(x => new SelectListItem
                                        {
                                            Value = x.LookupID.ToString(),
                                            Text = x.LookupCode
                                        })
                                        .ToList();
            ProductCategory.Add(new SelectListItem
            {
                Text = "ALL",
                Value = 0.ToString()
            });
            ViewBag.productCategory = ProductCategory;
            ViewBag.Login = BRANCH_ID;
            return View(productCategoryRMAPriceandValueDTO);
        }


        public ActionResult ViewProductsCategoryRMAReceiptValue(int? BranchID, int? FromMonth, int? ToMonth, int? Year, int? ProductCode, string URL)
        {
            ViewBag.BranchID = BranchID;
            ViewBag.Year = Year;
            ViewBag.FromMonth = FromMonth;
            ViewBag.ToMonth = ToMonth;
            ViewBag.ProductCode = ProductCode;
            ViewBag.ReportSource = "ProductsCategoryRMAReceiptValue";
            ViewBag.Url = string.Format("{0}{1}", UTILITY.REPORTSUBFOLDER, URL);
            var path = "~/Views/Shared/_ReportViewerPartial.cshtml";
            return PartialView(path);
        }

        //RMAReceiptQty and Value Month Wise
        public ActionResult RMAReceiptQty(ProductCategoryMonthWiseRMAReceiptandValueDTO productCategoryMonthWiseRMAPriceandValueDTO)
        {
            productCategoryMonthWiseRMAPriceandValueDTO.Branch = BRANCH_ID;
            productCategoryMonthWiseRMAPriceandValueDTO.FromMonth = Convert.ToInt16(1);
            productCategoryMonthWiseRMAPriceandValueDTO.ToMonth = Convert.ToInt16(DateTime.Now.Month);
            productCategoryMonthWiseRMAPriceandValueDTO.Year = Convert.ToInt16(DateTime.Now.Year);
            productCategoryMonthWiseRMAPriceandValueDTO.ProductCode = 0;
            productCategoryMonthWiseRMAPriceandValueDTO.RMAReceiptQty = new DashBoardBO().GetMonthWiseProductCategoryRMAReceipt(productCategoryMonthWiseRMAPriceandValueDTO);
            productCategoryMonthWiseRMAPriceandValueDTO.RMAReceiptValue = new DashBoardBO().GetMonthWiseProductCategoryRMAValue(productCategoryMonthWiseRMAPriceandValueDTO);
            ViewBag.Branches = new BranchBO().GetList().Select(x => new { Value = x.BranchID, Text = x.BranchName });
            ViewBag.Year = new DashBoardBO().GetRMAYear().Select(x => new { Value = x.Year, Text = x.Year });
            ViewBag.Months = new SelectList(Enumerable.Range(1, 12).Select(x => new SelectListItem() { Text = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[x - 1], Value = x.ToString() }), "Value", "Text");
            var ProductCategory = new LookupBO()
                                        .GetList()
                                        .Where(x => x.LookupCategory == "CategoryGroup" && x.Status == true)
                                        .Select(x => new SelectListItem
                                        {
                                            Value = x.LookupID.ToString(),
                                            Text = x.LookupCode
                                        })
                                        .ToList();
            ProductCategory.Add(new SelectListItem
            {
                Text = "ALL",
                Value = 0.ToString()
            });
            ViewBag.productCategory = ProductCategory;
            ViewBag.Login = BRANCH_ID;
            return View(productCategoryMonthWiseRMAPriceandValueDTO);
        }

        public ActionResult ViewRMAReceiptQty(int? BranchID, int? FromMonth, int? ToMonth, int? Year, int? ProductCode, string URL)
        {
            ViewBag.BranchID = BranchID;
            ViewBag.Year = Year;
            ViewBag.FromMonth = FromMonth;
            ViewBag.ToMonth = ToMonth;
            ViewBag.ProductCode = ProductCode;
            ViewBag.ReportSource = "ViewRMAReceiptQty";
            ViewBag.Url = string.Format("{0}{1}", UTILITY.REPORTSUBFOLDER, URL);
            var path = "~/Views/Shared/_ReportViewerPartial.cshtml";
            return PartialView(path);
        }


        public ActionResult RMAReceiptValue(ProductCategoryMonthWiseRMAReceiptandValueDTO productCategoryMonthWiseRMAPriceandValueDTO)
        {
            productCategoryMonthWiseRMAPriceandValueDTO.Branch = BRANCH_ID;
            productCategoryMonthWiseRMAPriceandValueDTO.FromMonth = Convert.ToInt16(1);
            productCategoryMonthWiseRMAPriceandValueDTO.ToMonth = Convert.ToInt16(DateTime.Now.Month);
            productCategoryMonthWiseRMAPriceandValueDTO.Year = Convert.ToInt16(DateTime.Now.Year);
            productCategoryMonthWiseRMAPriceandValueDTO.ProductCode = 0;
            productCategoryMonthWiseRMAPriceandValueDTO.RMAReceiptQty = new DashBoardBO().GetMonthWiseProductCategoryRMAReceipt(productCategoryMonthWiseRMAPriceandValueDTO);
            productCategoryMonthWiseRMAPriceandValueDTO.RMAReceiptValue = new DashBoardBO().GetMonthWiseProductCategoryRMAValue(productCategoryMonthWiseRMAPriceandValueDTO);
            ViewBag.Branches = new BranchBO().GetList().Select(x => new { Value = x.BranchID, Text = x.BranchName });
            ViewBag.Year = new DashBoardBO().GetRMAYear().Select(x => new { Value = x.Year, Text = x.Year });
            ViewBag.Months = new SelectList(Enumerable.Range(1, 12).Select(x => new SelectListItem() { Text = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[x - 1], Value = x.ToString() }), "Value", "Text");
            var ProductCategory = new LookupBO()
                                        .GetList()
                                        .Where(x => x.LookupCategory == "CategoryGroup" && x.Status == true)
                                        .Select(x => new SelectListItem
                                        {
                                            Value = x.LookupID.ToString(),
                                            Text = x.LookupCode
                                        })
                                        .ToList();
            ProductCategory.Add(new SelectListItem
            {
                Text = "ALL",
                Value = 0.ToString()
            });
            ViewBag.productCategory = ProductCategory;
            ViewBag.Login = BRANCH_ID;
            return View(productCategoryMonthWiseRMAPriceandValueDTO);
        }

        public ActionResult ViewRMAReceiptValue(int? BranchID, int? FromMonth, int? ToMonth, int? Year, int? ProductCode, string URL)
        {
            ViewBag.BranchID = BranchID;
            ViewBag.Year = Year;
            ViewBag.FromMonth = FromMonth;
            ViewBag.ToMonth = ToMonth;
            ViewBag.ProductCode = ProductCode;
            ViewBag.ReportSource = "ViewRMAReceiptValue";
            ViewBag.Url = string.Format("{0}{1}", UTILITY.REPORTSUBFOLDER, URL);
            var path = "~/Views/Shared/_ReportViewerPartial.cshtml";
            return PartialView(path);
        }

        //RMA CreditNote Category
        public ActionResult CreditNoteProductCategory(RMACreditNoteDTO rmaCreditNoteDTO)

        {
            rmaCreditNoteDTO.Branch = BRANCH_ID;
            rmaCreditNoteDTO.FromMonth = Convert.ToInt16(1);
            rmaCreditNoteDTO.ToMonth = Convert.ToInt16(DateTime.Now.Month);
            rmaCreditNoteDTO.Year = Convert.ToInt16(DateTime.Now.Year);
            rmaCreditNoteDTO.ProductCode = 0;
            rmaCreditNoteDTO.RMACreditNoteValue = new DashBoardBO().GetRMACreditNoteValue(rmaCreditNoteDTO);
            ViewBag.Branches = new BranchBO().GetList().Select(x => new { Value = x.BranchID, Text = x.BranchName });
            ViewBag.Months = new SelectList(Enumerable.Range(1, 12).Select(x => new SelectListItem() { Text = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[x - 1], Value = x.ToString() }), "Value", "Text");
            ViewBag.Year = new DashBoardBO().GetRMAOutwardYear().Select(x => new { Value = x.Year, Text = x.Year });
            var ProductCategory = new LookupBO()
                                        .GetList()
                                        .Where(x => x.LookupCategory == "CategoryGroup" && x.Status == true)
                                        .Select(x => new SelectListItem
                                        {
                                            Value = x.LookupID.ToString(),
                                            Text = x.LookupCode
                                        })
                                        .ToList();
            ProductCategory.Add(new SelectListItem
            {
                Text = "ALL",
                Value = 0.ToString()
            });
            ViewBag.productCategory = ProductCategory;
            ViewBag.Login = BRANCH_ID;
            return View(rmaCreditNoteDTO);
        }

        public ActionResult ViewCreditNoteProductCategory(int? BranchID, int? FromMonth, int? ToMonth, int? Year, int? ProductCode, string URL)
        {
            ViewBag.BranchID = BranchID;
            ViewBag.Year = Year;
            ViewBag.FromMonth = FromMonth;
            ViewBag.ToMonth = ToMonth;
            ViewBag.ProductCode = ProductCode;
            ViewBag.ReportSource = "ViewCreditNoteProductCategory";
            ViewBag.Url = string.Format("{0}{1}", UTILITY.REPORTSUBFOLDER, URL);
            var path = "~/Views/Shared/_ReportViewerPartial.cshtml";
            return PartialView(path);
        }


        public ActionResult CreditNoteValue(RMACreditNoteDTO rmaCreditNoteDTO)

        {
            rmaCreditNoteDTO.Branch = BRANCH_ID;
            rmaCreditNoteDTO.FromMonth = Convert.ToInt16(1);
            rmaCreditNoteDTO.ToMonth = Convert.ToInt16(DateTime.Now.Month);
            rmaCreditNoteDTO.Year = Convert.ToInt16(DateTime.Now.Year);
            rmaCreditNoteDTO.ProductCode = 0;
            rmaCreditNoteDTO.RMACreditNoteValue = new DashBoardBO().GetRMACreditNoteValue(rmaCreditNoteDTO);
            ViewBag.Branches = new BranchBO().GetList().Select(x => new { Value = x.BranchID, Text = x.BranchName });
            ViewBag.Months = new SelectList(Enumerable.Range(1, 12).Select(x => new SelectListItem() { Text = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[x - 1], Value = x.ToString() }), "Value", "Text");
            ViewBag.Year = new DashBoardBO().GetRMAOutwardYear().Select(x => new { Value = x.Year, Text = x.Year });
            var ProductCategory = new LookupBO()
                                        .GetList()
                                        .Where(x => x.LookupCategory == "CategoryGroup" && x.Status == true)
                                        .Select(x => new SelectListItem
                                        {
                                            Value = x.LookupID.ToString(),
                                            Text = x.LookupCode
                                        })
                                        .ToList();
            ProductCategory.Add(new SelectListItem
            {
                Text = "ALL",
                Value = 0.ToString()
            });
            ViewBag.productCategory = ProductCategory;
            ViewBag.Login = BRANCH_ID;
            return View(rmaCreditNoteDTO);
        }

        public ActionResult ViewCreditNoteValue(int? BranchID, int? FromMonth, int? ToMonth, int? Year, int? ProductCode, string URL)
        {
            ViewBag.BranchID = BranchID;
            ViewBag.Year = Year;
            ViewBag.FromMonth = FromMonth;
            ViewBag.ToMonth = ToMonth;
            ViewBag.ProductCode = ProductCode;
            ViewBag.ReportSource = "ViewCreditNoteValue";
            ViewBag.Url = string.Format("{0}{1}", UTILITY.REPORTSUBFOLDER, URL);
            var path = "~/Views/Shared/_ReportViewerPartial.cshtml";
            return PartialView(path);
        }
    }
}
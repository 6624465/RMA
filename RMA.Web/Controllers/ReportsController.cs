using EZY.RMAS.BusinessFactory;
using EZY.RMAS.Contract;
using RMA.Web;
using RMA.Web.ViewModels.Report;
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

            ViewBag.ReportMenuList = GetReportMenu();
            reportParams.BranchID = BRANCH_ID;
            reportParams.DateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);
            reportParams.DateTo = DateTime.Now.Date;
            reportParams.Year = Convert.ToInt16(DateTime.Now.Year);
            reportParams.ProductCode = 0;
            var YearList = new List<Year>();
            var Year = new DashBoardBO().GetRMAOutwardYear().Select(x => new Year() { Value = x.Year, Text = x.Year }).FirstOrDefault();
            YearList.Add(Year);
            if (new DashBoardBO().GetRMAOutwardYear().Any(x => x.Year != Convert.ToInt16(DateTime.Now.Year)))
            {
                YearList.Add(new Year { Value = Convert.ToInt16(DateTime.Now.Year), Text = Convert.ToInt16(DateTime.Now.Year) });
            }
            ViewBag.Year = YearList;
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

        private List<ReportMenu> GetReportMenu()
        {
            List<ReportMenu> all = new List<ReportMenu>();
            all.Add(new ReportMenu { MenuID = 1, MenuName = "Material Inward Product Qty", NavURL = "" });
            all.Add(new ReportMenu { MenuID = 2, MenuName = "Material Inward Product Cost", NavURL = "" });
            all.Add(new ReportMenu { MenuID = 3, MenuName = "Material Inward Total Invoice", NavURL = "" });
            all.Add(new ReportMenu { MenuID = 4, MenuName = "Material Outward Product Qty", NavURL = "" });
            all.Add(new ReportMenu { MenuID = 5, MenuName = "Material Outward Product Cost", NavURL = "" });
            all.Add(new ReportMenu { MenuID = 6, MenuName = "Material Outward Total Invoice", NavURL = "" });
            all.Add(new ReportMenu { MenuID = 7, MenuName = "RMA Transaction Received", NavURL = "" });
            all.Add(new ReportMenu { MenuID = 8, MenuName = "RMA Transaction Return", NavURL = "" });
            all.Add(new ReportMenu { MenuID = 9, MenuName = "Products Category RMA Receipt Qty", NavURL = "" });
            all.Add(new ReportMenu { MenuID = 10, MenuName = "Products Category RMA Receipt Value", NavURL = "" });
            all.Add(new ReportMenu { MenuID = 11, MenuName = "RMA Receipt Quantity", NavURL = "" });
            all.Add(new ReportMenu { MenuID = 12, MenuName = "RMA Receipt Value", NavURL = "" });
            all.Add(new ReportMenu { MenuID = 13, MenuName = "RMA CreditNote ProductCategory", NavURL = "" });
            all.Add(new ReportMenu { MenuID = 14, MenuName = "RMA CreditNote Value", NavURL = "" });
            //all.Add(new ReportMenu { MenuID = 6, MenuName = "", NavURL = "" });
            //all.Add(new ReportMenu { MenuID = 6, MenuName = "", NavURL = "" });
            return all;
        }

        public ActionResult ViewMaterialInwardProductQtyMonthWise(int? BranchID, string dateFrom, string dateTo, int? Year, int? ProductCode, string URL)
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
            ViewBag.ReportMenuList = GetReportMenu();
            reportParams.BranchID = BRANCH_ID;
            reportParams.DateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);
            reportParams.DateTo = DateTime.Now.Date;
            reportParams.Year = Convert.ToInt16(DateTime.Now.Year);
            reportParams.ProductCode = 0;
            var YearList = new List<Year>();
            var Year = new DashBoardBO().GetRMAOutwardYear().Select(x => new Year() { Value = x.Year, Text = x.Year }).FirstOrDefault();
            YearList.Add(Year);
            if (new DashBoardBO().GetRMAOutwardYear().Any(x => x.Year != Convert.ToInt16(DateTime.Now.Year)))
            {
                YearList.Add(new Year { Value = Convert.ToInt16(DateTime.Now.Year), Text = Convert.ToInt16(DateTime.Now.Year) });
            }
            ViewBag.Year = YearList;
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
            ViewBag.ReportMenuList = GetReportMenu();
            reportParams.BranchID = BRANCH_ID;
            reportParams.DateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);
            reportParams.DateTo = DateTime.Now.Date;
            reportParams.Year = Convert.ToInt16(DateTime.Now.Year);
            reportParams.ProductCode = 0;
            var YearList = new List<Year>();
            var Year = new DashBoardBO().GetRMAOutwardYear().Select(x => new Year() { Value = x.Year, Text = x.Year }).FirstOrDefault();
            YearList.Add(Year);
            if (new DashBoardBO().GetRMAOutwardYear().Any(x => x.Year != Convert.ToInt16(DateTime.Now.Year)))
            {
                YearList.Add(new Year { Value = Convert.ToInt16(DateTime.Now.Year), Text = Convert.ToInt16(DateTime.Now.Year) });
            }
            ViewBag.Year = YearList;
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
            ViewBag.ReportMenuList = GetReportMenu();
            reportParams.BranchID = BRANCH_ID;
            reportParams.DateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);
            reportParams.DateTo = DateTime.Now.Date;
            reportParams.Year = Convert.ToInt16(DateTime.Now.Year);
            reportParams.ProductCode = 0;
            var YearList = new List<Year>();
            var Year = new DashBoardBO().GetRMAOutwardYear().Select(x => new Year() { Value = x.Year, Text = x.Year }).FirstOrDefault();
            YearList.Add(Year);
            if (new DashBoardBO().GetRMAOutwardYear().Any(x => x.Year != Convert.ToInt16(DateTime.Now.Year)))
            {
                YearList.Add(new Year { Value = Convert.ToInt16(DateTime.Now.Year), Text = Convert.ToInt16(DateTime.Now.Year) });
            }
            ViewBag.Year = YearList;
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
            ViewBag.ReportSource = "MaterialOutwardProductQtyMonthWise";
            ViewBag.Url = string.Format("{0}{1}", UTILITY.REPORTSUBFOLDER, URL);
            var path = "~/Views/Shared/_ReportViewerPartial.cshtml";
            return PartialView(path);
        }


        public ActionResult MaterialOutwardProductCostMonthWise(ReportParams reportParams)
        {
            ViewBag.ReportMenuList = GetReportMenu();
            reportParams.BranchID = BRANCH_ID;
            reportParams.DateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);
            reportParams.DateTo = DateTime.Now.Date;
            reportParams.Year = Convert.ToInt16(DateTime.Now.Year);
            reportParams.ProductCode = 0;
            var YearList = new List<Year>();
            var Year = new DashBoardBO().GetRMAOutwardYear().Select(x => new Year() { Value = x.Year, Text = x.Year }).FirstOrDefault();
            YearList.Add(Year);
            if (new DashBoardBO().GetRMAOutwardYear().Any(x => x.Year != Convert.ToInt16(DateTime.Now.Year)))
            {
                YearList.Add(new Year { Value = Convert.ToInt16(DateTime.Now.Year), Text = Convert.ToInt16(DateTime.Now.Year) });
            }
            ViewBag.Year = YearList;
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
            ViewBag.ReportSource = "MaterialOutwardProductCostMonthWise";
            ViewBag.Url = string.Format("{0}{1}", UTILITY.REPORTSUBFOLDER, URL);
            var path = "~/Views/Shared/_ReportViewerPartial.cshtml";
            return PartialView(path);
        }

        public ActionResult MaterialOutwardTotalInvoicesProductMonthWise(ReportParams reportParams)
        {
            ViewBag.ReportMenuList = GetReportMenu();
            reportParams.BranchID = BRANCH_ID;
            reportParams.DateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);
            reportParams.DateTo = DateTime.Now.Date;
            reportParams.Year = Convert.ToInt16(DateTime.Now.Year);
            reportParams.ProductCode = 0;
            var YearList = new List<Year>();
            var Year = new DashBoardBO().GetRMAOutwardYear().Select(x => new Year() { Value = x.Year, Text = x.Year }).FirstOrDefault();
            YearList.Add(Year);
            if (new DashBoardBO().GetRMAOutwardYear().Any(x => x.Year != Convert.ToInt16(DateTime.Now.Year)))
            {
                YearList.Add(new Year { Value = Convert.ToInt16(DateTime.Now.Year), Text = Convert.ToInt16(DateTime.Now.Year) });
            }
            ViewBag.Year = YearList;
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
            ViewBag.ReportMenuList = GetReportMenu();
            rmaCreditNoteDTO.Branch = BRANCH_ID;
            rmaCreditNoteDTO.FromMonth = Convert.ToInt16(1);
            rmaCreditNoteDTO.ToMonth = Convert.ToInt16(DateTime.Now.Month);
            rmaCreditNoteDTO.Year = Convert.ToInt16(DateTime.Now.Year);
            rmaCreditNoteDTO.ProductCode = 0;
            var YearList = new List<Year>();
            var Year = new DashBoardBO().GetRMAOutwardYear().Select(x => new Year() { Value = x.Year, Text = x.Year }).FirstOrDefault();
            YearList.Add(Year);
            if (new DashBoardBO().GetRMAOutwardYear().Any(x => x.Year != Convert.ToInt16(DateTime.Now.Year)))
            {
                YearList.Add(new Year { Value = Convert.ToInt16(DateTime.Now.Year), Text = Convert.ToInt16(DateTime.Now.Year) });
            }
            ViewBag.Year = YearList;
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
            ViewBag.ReportMenuList = GetReportMenu();
            rmaCreditNoteDTO.Branch = BRANCH_ID;
            rmaCreditNoteDTO.FromMonth = Convert.ToInt16(1);
            rmaCreditNoteDTO.ToMonth = Convert.ToInt16(DateTime.Now.Month);
            rmaCreditNoteDTO.Year = Convert.ToInt16(DateTime.Now.Year);
            rmaCreditNoteDTO.ProductCode = 0;
            var YearList = new List<Year>();
            var Year = new DashBoardBO().GetRMAOutwardYear().Select(x => new Year() { Value = x.Year, Text = x.Year }).FirstOrDefault();
            YearList.Add(Year);
            if (new DashBoardBO().GetRMAOutwardYear().Any(x => x.Year != Convert.ToInt16(DateTime.Now.Year)))
            {
                YearList.Add(new Year { Value = Convert.ToInt16(DateTime.Now.Year), Text = Convert.ToInt16(DateTime.Now.Year) });
            }
            ViewBag.Year = YearList;
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
            ViewBag.ReportMenuList = GetReportMenu();
            productCategoryRMAPriceandValueDTO.Branch = BRANCH_ID;
            productCategoryRMAPriceandValueDTO.FromMonth = Convert.ToInt16(1);
            productCategoryRMAPriceandValueDTO.ToMonth = Convert.ToInt16(DateTime.Now.Month);
            productCategoryRMAPriceandValueDTO.Year = Convert.ToInt16(DateTime.Now.Year);
            productCategoryRMAPriceandValueDTO.ProductCode = 0;
            productCategoryRMAPriceandValueDTO.ProductCategoryRMAReceipt = new DashBoardBO().GetProductCategoryRMAReceipt(productCategoryRMAPriceandValueDTO);
            productCategoryRMAPriceandValueDTO.ProductCategoryRMAValue = new DashBoardBO().GetProductCategoryRMAValue(productCategoryRMAPriceandValueDTO);
            ViewBag.Branches = new BranchBO().GetList().Select(x => new { Value = x.BranchID, Text = x.BranchName });
            var YearList = new List<Year>();
            var Year = new DashBoardBO().GetRMAOutwardYear().Select(x => new Year() { Value = x.Year, Text = x.Year }).FirstOrDefault();
            YearList.Add(Year);
            if (new DashBoardBO().GetRMAOutwardYear().Any(x => x.Year != Convert.ToInt16(DateTime.Now.Year)))
            {
                YearList.Add(new Year { Value = Convert.ToInt16(DateTime.Now.Year), Text = Convert.ToInt16(DateTime.Now.Year) });
            }
            ViewBag.Year = YearList;
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
            return View("ProductsCategoryRMAReceiptQty", productCategoryRMAPriceandValueDTO);
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
            ViewBag.ReportMenuList = GetReportMenu();
            productCategoryRMAPriceandValueDTO.Branch = BRANCH_ID;
            productCategoryRMAPriceandValueDTO.FromMonth = Convert.ToInt16(1);
            productCategoryRMAPriceandValueDTO.ToMonth = Convert.ToInt16(DateTime.Now.Month);
            productCategoryRMAPriceandValueDTO.Year = Convert.ToInt16(DateTime.Now.Year);
            productCategoryRMAPriceandValueDTO.ProductCode = 0;
            productCategoryRMAPriceandValueDTO.ProductCategoryRMAReceipt = new DashBoardBO().GetProductCategoryRMAReceipt(productCategoryRMAPriceandValueDTO);
            productCategoryRMAPriceandValueDTO.ProductCategoryRMAValue = new DashBoardBO().GetProductCategoryRMAValue(productCategoryRMAPriceandValueDTO);
            ViewBag.Branches = new BranchBO().GetList().Select(x => new { Value = x.BranchID, Text = x.BranchName });
            var YearList = new List<Year>();
            var Year = new DashBoardBO().GetRMAOutwardYear().Select(x => new Year() { Value = x.Year, Text = x.Year }).FirstOrDefault();
            YearList.Add(Year);
            if (new DashBoardBO().GetRMAOutwardYear().Any(x => x.Year != Convert.ToInt16(DateTime.Now.Year)))
            {
                YearList.Add(new Year { Value = Convert.ToInt16(DateTime.Now.Year), Text = Convert.ToInt16(DateTime.Now.Year) });
            }
            ViewBag.Year = YearList;
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
            return View("ProductsCategoryRMAReceiptValue", productCategoryRMAPriceandValueDTO);
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
            ViewBag.ReportMenuList = GetReportMenu();
            productCategoryMonthWiseRMAPriceandValueDTO.Branch = BRANCH_ID;
            productCategoryMonthWiseRMAPriceandValueDTO.FromMonth = Convert.ToInt16(1);
            productCategoryMonthWiseRMAPriceandValueDTO.ToMonth = Convert.ToInt16(DateTime.Now.Month);
            productCategoryMonthWiseRMAPriceandValueDTO.Year = Convert.ToInt16(DateTime.Now.Year);
            productCategoryMonthWiseRMAPriceandValueDTO.ProductCode = 0;
            productCategoryMonthWiseRMAPriceandValueDTO.RMAReceiptQty = new DashBoardBO().GetMonthWiseProductCategoryRMAReceipt(productCategoryMonthWiseRMAPriceandValueDTO);
            productCategoryMonthWiseRMAPriceandValueDTO.RMAReceiptValue = new DashBoardBO().GetMonthWiseProductCategoryRMAValue(productCategoryMonthWiseRMAPriceandValueDTO);
            ViewBag.Branches = new BranchBO().GetList().Select(x => new { Value = x.BranchID, Text = x.BranchName });
            var YearList = new List<Year>();
            var Year = new DashBoardBO().GetRMAOutwardYear().Select(x => new Year() { Value = x.Year, Text = x.Year }).FirstOrDefault();
            YearList.Add(Year);
            if (new DashBoardBO().GetRMAOutwardYear().Any(x => x.Year != Convert.ToInt16(DateTime.Now.Year)))
            {
                YearList.Add(new Year { Value = Convert.ToInt16(DateTime.Now.Year), Text = Convert.ToInt16(DateTime.Now.Year) });
            }
            ViewBag.Year = YearList;
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
            return View("RMAReceiptQty", productCategoryMonthWiseRMAPriceandValueDTO);
        }

        public ActionResult ViewRMAReceiptQty(int? BranchID, int? FromMonth, int? ToMonth, int? Year, int? ProductCode, string URL)
        {
            ViewBag.BranchID = BranchID;
            ViewBag.Year = Year;
            ViewBag.FromMonth = FromMonth;
            ViewBag.ToMonth = ToMonth;
            ViewBag.ProductCode = ProductCode;
            ViewBag.ReportSource = "RMAReceiptQty";
            ViewBag.Url = string.Format("{0}{1}", UTILITY.REPORTSUBFOLDER, URL);
            var path = "~/Views/Shared/_ReportViewerPartial.cshtml";
            return PartialView(path);
        }


        public ActionResult RMAReceiptValue(ProductCategoryMonthWiseRMAReceiptandValueDTO productCategoryMonthWiseRMAPriceandValueDTO)
        {
            ViewBag.ReportMenuList = GetReportMenu();
            productCategoryMonthWiseRMAPriceandValueDTO.Branch = BRANCH_ID;
            productCategoryMonthWiseRMAPriceandValueDTO.FromMonth = Convert.ToInt16(1);
            productCategoryMonthWiseRMAPriceandValueDTO.ToMonth = Convert.ToInt16(DateTime.Now.Month);
            productCategoryMonthWiseRMAPriceandValueDTO.Year = Convert.ToInt16(DateTime.Now.Year);
            productCategoryMonthWiseRMAPriceandValueDTO.ProductCode = 0;
            productCategoryMonthWiseRMAPriceandValueDTO.RMAReceiptQty = new DashBoardBO().GetMonthWiseProductCategoryRMAReceipt(productCategoryMonthWiseRMAPriceandValueDTO);
            productCategoryMonthWiseRMAPriceandValueDTO.RMAReceiptValue = new DashBoardBO().GetMonthWiseProductCategoryRMAValue(productCategoryMonthWiseRMAPriceandValueDTO);
            ViewBag.Branches = new BranchBO().GetList().Select(x => new { Value = x.BranchID, Text = x.BranchName });
            var YearList = new List<Year>();
            var Year = new DashBoardBO().GetRMAOutwardYear().Select(x => new Year() { Value = x.Year, Text = x.Year }).FirstOrDefault();
            YearList.Add(Year);
            if (new DashBoardBO().GetRMAOutwardYear().Any(x => x.Year != Convert.ToInt16(DateTime.Now.Year)))
            {
                YearList.Add(new Year { Value = Convert.ToInt16(DateTime.Now.Year), Text = Convert.ToInt16(DateTime.Now.Year) });
            }
            ViewBag.Year = YearList;
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
            return View("RMAReceiptValue", productCategoryMonthWiseRMAPriceandValueDTO);
        }

        public ActionResult ViewRMAReceiptValue(int? BranchID, int? FromMonth, int? ToMonth, int? Year, int? ProductCode, string URL)
        {
            ViewBag.BranchID = BranchID;
            ViewBag.Year = Year;
            ViewBag.FromMonth = FromMonth;
            ViewBag.ToMonth = ToMonth;
            ViewBag.ProductCode = ProductCode;
            ViewBag.ReportSource = "RMAReceiptValue";
            ViewBag.Url = string.Format("{0}{1}", UTILITY.REPORTSUBFOLDER, URL);
            var path = "~/Views/Shared/_ReportViewerPartial.cshtml";
            return PartialView(path);
        }

        //RMA CreditNote Category
        public ActionResult CreditNoteProductCategory(RMACreditNoteDTO rmaCreditNoteDTO)

        {
            ViewBag.ReportMenuList = GetReportMenu();
            rmaCreditNoteDTO.Branch = BRANCH_ID;
            rmaCreditNoteDTO.FromMonth = Convert.ToInt16(1);
            rmaCreditNoteDTO.ToMonth = Convert.ToInt16(DateTime.Now.Month);
            rmaCreditNoteDTO.Year = Convert.ToInt16(DateTime.Now.Year);
            rmaCreditNoteDTO.ProductCode = 0;
            rmaCreditNoteDTO.RMACreditNoteValue = new DashBoardBO().GetRMACreditNoteValue(rmaCreditNoteDTO);
            ViewBag.Branches = new BranchBO().GetList().Select(x => new { Value = x.BranchID, Text = x.BranchName });
            ViewBag.Months = new SelectList(Enumerable.Range(1, 12).Select(x => new SelectListItem() { Text = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[x - 1], Value = x.ToString() }), "Value", "Text");
            var YearList = new List<Year>();
            var Year = new DashBoardBO().GetRMAOutwardYear().Select(x => new Year() { Value = x.Year, Text = x.Year }).FirstOrDefault();
            YearList.Add(Year);
            if (new DashBoardBO().GetRMAOutwardYear().Any(x => x.Year != Convert.ToInt16(DateTime.Now.Year)))
            {
                YearList.Add(new Year { Value = Convert.ToInt16(DateTime.Now.Year), Text = Convert.ToInt16(DateTime.Now.Year) });
            }
            ViewBag.Year = YearList;
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
            return View("CreditNoteProductCategory", rmaCreditNoteDTO);
        }

        public ActionResult ViewCreditNoteProductCategory(int? BranchID, int? FromMonth, int? ToMonth, int? Year, int? ProductCode, string URL)
        {
            ViewBag.BranchID = BranchID;
            ViewBag.Year = Year;
            ViewBag.FromMonth = FromMonth;
            ViewBag.ToMonth = ToMonth;
            ViewBag.ProductCode = ProductCode;
            ViewBag.ReportSource = "CreditNoteProductCategory";
            ViewBag.Url = string.Format("{0}{1}", UTILITY.REPORTSUBFOLDER, URL);
            var path = "~/Views/Shared/_ReportViewerPartial.cshtml";
            return PartialView(path);
        }


        public ActionResult CreditNoteValue(RMACreditNoteDTO rmaCreditNoteDTO)

        {
           

            ViewBag.ReportMenuList = GetReportMenu();
            rmaCreditNoteDTO.Branch = BRANCH_ID;
            rmaCreditNoteDTO.FromMonth = Convert.ToInt16(1);
            rmaCreditNoteDTO.ToMonth = Convert.ToInt16(DateTime.Now.Month);
            rmaCreditNoteDTO.Year = Convert.ToInt16(DateTime.Now.Year);
            rmaCreditNoteDTO.ProductCode = 0;
            rmaCreditNoteDTO.RMACreditNoteValue = new DashBoardBO().GetRMACreditNoteValue(rmaCreditNoteDTO);
            ViewBag.Branches = new BranchBO().GetList().Select(x => new { Value = x.BranchID, Text = x.BranchName });
            ViewBag.Months = new SelectList(Enumerable.Range(1, 12).Select(x => new SelectListItem() { Text = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[x - 1], Value = x.ToString() }), "Value", "Text");

            var YearList = new List<Year>();
            var Year = new DashBoardBO().GetRMAOutwardYear().Select(x => new Year() { Value = x.Year, Text = x.Year }).FirstOrDefault();
            YearList.Add(Year);
            if (new DashBoardBO().GetRMAOutwardYear().Any(x => x.Year != Convert.ToInt16(DateTime.Now.Year)))
            {
                YearList.Add(new Year { Value = Convert.ToInt16(DateTime.Now.Year), Text = Convert.ToInt16(DateTime.Now.Year) });
            }
            ViewBag.Year = YearList;

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
            return View("CreditNoteValue", rmaCreditNoteDTO);
        }

        public ActionResult ViewCreditNoteValue(int? BranchID, int? FromMonth, int? ToMonth, int? Year, int? ProductCode, string URL)
        {
            ViewBag.BranchID = BranchID;
            ViewBag.Year = Year;
            ViewBag.FromMonth = FromMonth;
            ViewBag.ToMonth = ToMonth;
            ViewBag.ProductCode = ProductCode;
            ViewBag.ReportSource = "CreditNoteValue";
            ViewBag.Url = string.Format("{0}{1}", UTILITY.REPORTSUBFOLDER, URL);
            var path = "~/Views/Shared/_ReportViewerPartial.cshtml";
            return PartialView(path);
        }
    }

    public class Year{
        public int Value { get; set; }
        public int Text { get; set; }
    }

}
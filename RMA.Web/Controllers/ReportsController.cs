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
using System.Web.Configuration;
using System.Web.Mvc;


namespace RMA.Web.Reports.Controllers
{

    [WebSsnFilter]
    public class ReportsController : BaseController
    {
        public static string REPORTSUBFOLDER = WebConfigurationManager.AppSettings["ReportPath"].ToString();

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
            all.Add(new ReportMenu { MenuID = 1, MenuName = "Material Inward Product Qty", NavURL = "MaterialInwardProductQty" });
            all.Add(new ReportMenu { MenuID = 2, MenuName = "Material Inward Product Cost", NavURL = "MaterialInwardProductCost" });
            all.Add(new ReportMenu { MenuID = 3, MenuName = "Material Inward Total Invoice", NavURL = "MaterialInwardTotalInvoice" });
            all.Add(new ReportMenu { MenuID = 4, MenuName = "Material Outward Product Qty", NavURL = "MaterialOutwardProductQty" });
            all.Add(new ReportMenu { MenuID = 5, MenuName = "Material Outward Product Cost", NavURL = "MaterialOutwardProductCost" });
            all.Add(new ReportMenu { MenuID = 6, MenuName = "Material Outward Total Invoice", NavURL = "MaterialOutwardTotalInvoice" });
            all.Add(new ReportMenu { MenuID = 7, MenuName = "RMA Transaction Received", NavURL = "RMATransactionReceived" });
            all.Add(new ReportMenu { MenuID = 8, MenuName = "RMA Transaction Return", NavURL = "RMATransactionReturn" });
            all.Add(new ReportMenu { MenuID = 9, MenuName = "Products Category RMA Receipt Qty", NavURL = "ProductsCategoryRMAReceiptQty" });
            all.Add(new ReportMenu { MenuID = 10, MenuName = "Products Category RMA Receipt Value", NavURL = "ProductsCategoryRMAReceiptValue" });
            all.Add(new ReportMenu { MenuID = 11, MenuName = "RMA Receipt Quantity", NavURL = "RMAReceiptQuantity" });
            all.Add(new ReportMenu { MenuID = 12, MenuName = "RMA Receipt Value", NavURL = "RMAReceiptValue" });
            all.Add(new ReportMenu { MenuID = 13, MenuName = "RMA CreditNote ProductCategory", NavURL = "RMACreditNoteProductCategory" });
            all.Add(new ReportMenu { MenuID = 14, MenuName = "RMA CreditNote Value", NavURL = "RMACreditNoteValue" });
            return all;
        }

        public PartialViewResult ViewMaterialInwardProductQtyMonthWise(int? BranchID, string dateFrom, string dateTo, int? Year, int? ProductCode, string URL)
        {
            ViewBag.Title = "Material Inward Product Qty";
            ViewBag.BranchID = BranchID;
            ViewBag.Year = Year;
            ViewBag.DateFrom = dateFrom;
            ViewBag.DateTo = dateTo;
            ViewBag.ProductCode = ProductCode;
            ViewBag.ReportSource = "MaterialInwardProductQty";
            ViewBag.Url = string.Format("{0}{1}", REPORTSUBFOLDER, URL);
            return PartialView("ViewReport");
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
            ViewBag.Title = "Material Inward Product Cost Report";
            ViewBag.BranchID = BranchID;
            ViewBag.Year = Year;
            ViewBag.DateFrom = dateFrom;
            ViewBag.DateTo = dateTo;
            ViewBag.ProductCode = ProductCode;
            ViewBag.ReportSource = "MaterialInwardProductCost";
            ViewBag.Url = string.Format("{0}{1}", REPORTSUBFOLDER, URL);
            //var path = "~/Views/Shared/_ReportViewerPartial.cshtml";
            return PartialView("ViewReport");
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
            ViewBag.Title = "Material Inward Total Invoices Product Wise Report";
            ViewBag.BranchID = BranchID;
            ViewBag.Year = Year;
            ViewBag.DateFrom = dateFrom;
            ViewBag.DateTo = dateTo;
            ViewBag.ProductCode = ProductCode;
            ViewBag.ReportSource = "MaterialInwardTotalInvoicesProductWise";
            ViewBag.Url = string.Format("{0}{1}", REPORTSUBFOLDER, URL);
            //var path = "~/Views/Shared/_ReportViewerPartial.cshtml";
            return PartialView("ViewReport");
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
            ViewBag.Title = "Material Outward Product Qty";
            ViewBag.BranchID = BranchID;
            ViewBag.Year = Year;
            ViewBag.DateFrom = dateFrom;
            ViewBag.DateTo = dateTo;
            ViewBag.ProductCode = ProductCode;
            ViewBag.ReportSource = "MaterialOutwardProductQty";
            ViewBag.Url = string.Format("{0}{1}", REPORTSUBFOLDER, URL);
            //var path = "~/Views/Shared/_ReportViewerPartial.cshtml";
            return PartialView("ViewReport");
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
            ViewBag.Title = "Material Outward Product Cost Report";
            ViewBag.BranchID = BranchID;
            ViewBag.Year = Year;
            ViewBag.DateFrom = dateFrom;
            ViewBag.DateTo = dateTo;
            ViewBag.ProductCode = ProductCode;
            ViewBag.ReportSource = "MaterialOutwardProductCost";
            ViewBag.Url = string.Format("{0}{1}", REPORTSUBFOLDER, URL);
            //var path = "~/Views/Shared/_ReportViewerPartial.cshtml";
            return PartialView("ViewReport");
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
            ViewBag.Title = "Material Outward Total Invoices Product Month Wise Report";
            ViewBag.BranchID = BranchID;
            ViewBag.Year = Year;
            ViewBag.DateFrom = dateFrom;
            ViewBag.DateTo = dateTo;
            ViewBag.ProductCode = ProductCode;
            ViewBag.ReportSource = "MaterialOutwardTotalInvoicesProductMonthWise";
            ViewBag.Url = string.Format("{0}{1}", REPORTSUBFOLDER, URL);
            //var path = "~/Views/Shared/_ReportViewerPartial.cshtml";
            return PartialView("ViewReport");
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
            ViewBag.Title = "RMA Transaction Received Report";
            ViewBag.BranchID = BranchID;
            ViewBag.Year = Year;
            ViewBag.FromMonth = FromMonth;
            ViewBag.ToMonth = ToMonth;
            ViewBag.ProductCode = ProductCode;
            ViewBag.ReportSource = "RMATransactionReceived";
            ViewBag.Url = string.Format("{0}{1}", REPORTSUBFOLDER, URL);
            //var path = "~/Views/Shared/_ReportViewerPartial.cshtml";
            return PartialView("ViewReport");
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
            ViewBag.Title = "RMA Transaction Return Report";
            ViewBag.BranchID = BranchID;
            ViewBag.Year = Year;
            ViewBag.FromMonth = FromMonth;
            ViewBag.ToMonth = ToMonth;
            ViewBag.ProductCode = ProductCode;
            ViewBag.ReportSource = "RMATransactionReturn";
            ViewBag.Url = string.Format("{0}{1}", REPORTSUBFOLDER, URL);
            //var path = "~/Views/Shared/_ReportViewerPartial.cshtml";
            return PartialView("ViewReport");
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
            ViewBag.Title = "Products Category RMA Receipt Qty Report";
            ViewBag.BranchID = BranchID;
            ViewBag.Year = Year;
            ViewBag.FromMonth = FromMonth;
            ViewBag.ToMonth = ToMonth;
            ViewBag.ProductCode = ProductCode;
            ViewBag.ReportSource = "ProductsCategoryRMAReceiptQty";
            ViewBag.Url = string.Format("{0}{1}", REPORTSUBFOLDER, URL);
            //var path = "~/Views/Shared/_ReportViewerPartial.cshtml";
            return PartialView("ViewReport");
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
            ViewBag.Title = "Products Category RMA Receipt Value Report";
            ViewBag.BranchID = BranchID;
            ViewBag.Year = Year;
            ViewBag.FromMonth = FromMonth;
            ViewBag.ToMonth = ToMonth;
            ViewBag.ProductCode = ProductCode;
            ViewBag.ReportSource = "ProductsCategoryRMAReceiptValue";
            ViewBag.Url = string.Format("{0}{1}", REPORTSUBFOLDER, URL);
            //var path = "~/Views/Shared/_ReportViewerPartial.cshtml";
            return PartialView("ViewReport");
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
            ViewBag.Title = "RMA Receipt Qty Report";
            ViewBag.BranchID = BranchID;
            ViewBag.Year = Year;
            ViewBag.FromMonth = FromMonth;
            ViewBag.ToMonth = ToMonth;
            ViewBag.ProductCode = ProductCode;
            ViewBag.ReportSource = "RMAReceiptQty";
            ViewBag.Url = string.Format("{0}{1}", REPORTSUBFOLDER, URL);
            //var path = "~/Views/Shared/_ReportViewerPartial.cshtml";
            return PartialView("ViewReport");
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
            ViewBag.Title = "RMA Receipt Value Report";
            ViewBag.BranchID = BranchID;
            ViewBag.Year = Year;
            ViewBag.FromMonth = FromMonth;
            ViewBag.ToMonth = ToMonth;
            ViewBag.ProductCode = ProductCode;
            ViewBag.ReportSource = "RMAReceiptValue";
            ViewBag.Url = string.Format("{0}{1}", REPORTSUBFOLDER, URL);
            //var path = "~/Views/Shared/_ReportViewerPartial.cshtml";
            return PartialView("ViewReport");
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
            ViewBag.Title = "Credit Note Product Category Report";
            ViewBag.BranchID = BranchID;
            ViewBag.Year = Year;
            ViewBag.FromMonth = FromMonth;
            ViewBag.ToMonth = ToMonth;
            ViewBag.ProductCode = ProductCode;
            ViewBag.ReportSource = "CreditNoteProductCategory";
            ViewBag.Url = string.Format("{0}{1}", REPORTSUBFOLDER, URL);
            //var path = "~/Views/Shared/_ReportViewerPartial.cshtml";
            return PartialView("ViewReport");
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
            ViewBag.Title = "Credit Note Value Report";
            ViewBag.BranchID = BranchID;
            ViewBag.Year = Year;
            ViewBag.FromMonth = FromMonth;
            ViewBag.ToMonth = ToMonth;
            ViewBag.ProductCode = ProductCode;
            ViewBag.ReportSource = "CreditNoteValue";
            ViewBag.Url = string.Format("{0}{1}", REPORTSUBFOLDER, URL);
            //var path = "~/Views/Shared/_ReportViewerPartial.cshtml";
            return PartialView("ViewReport");
        }
    }

    public class Year{
        public int Value { get; set; }
        public int Text { get; set; }
    }

}
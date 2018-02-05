using EZY.RMAS.BusinessFactory;
using EZY.RMAS.Contract;
using RMA.Web;
using System;
using System.Collections.Generic;
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
    }
}
using EZY.RMAS.Contract;
using EZY.RMAS.BusinessFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;

namespace RMA.Web.Controllers
{
    [WebSsnFilter]
    public class DashboardController : BaseController
    {
        // GET: Dashboard
        #region Material Inward
        [HttpGet]
        public ActionResult MaterialInwardDashboards(MaterialInwardDashboardDTO materialInwardDashboardDTO)
        {
            materialInwardDashboardDTO.Branch = BRANCH_ID;
            materialInwardDashboardDTO.FromMonth = Convert.ToInt16(DateTime.Now.Month);
            materialInwardDashboardDTO.ToMonth = Convert.ToInt16(DateTime.Now.Month);
            materialInwardDashboardDTO.Year = Convert.ToInt16(DateTime.Now.Year);
            materialInwardDashboardDTO.ProductCode = 0;
            materialInwardDashboardDTO.MaterialInwardProductQty = new DashBoardBO().GetMaterialInwardProductQtyMonthWise(materialInwardDashboardDTO);
            materialInwardDashboardDTO.MaterialInwardInvoices = new DashBoardBO().GetMaterialInwardInvoicesMonthWise(materialInwardDashboardDTO);
            ViewBag.Months = new SelectList(Enumerable.Range(1, 12).Select(x => new SelectListItem() { Text = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[x - 1], Value = x.ToString() }), "Value", "Text");
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
            return View(materialInwardDashboardDTO);
        }
        [HttpPost]
        public ActionResult GetMaterialInwardProductCost(MaterialInwardDashboardDTO dashBoardParams)
        {
            if (dashBoardParams.Branch == 0)
            {
                dashBoardParams.Branch = BRANCH_ID;
            }
            var MaterialInwardProductCost = new DashBoardBO().GetMaterialInwardProductCostMonthWise(dashBoardParams);
            return Json(MaterialInwardProductCost, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetMaterialInwardDashboards(MaterialInwardDashboardDTO materialInwardDashboardDTO)
        {
            if (materialInwardDashboardDTO.Branch == 0)
            {
                materialInwardDashboardDTO.Branch = BRANCH_ID;
            }
            materialInwardDashboardDTO.MaterialInwardProductQty = new DashBoardBO().GetMaterialInwardProductQtyMonthWise(materialInwardDashboardDTO);
            materialInwardDashboardDTO.MaterialInwardInvoices = new DashBoardBO().GetMaterialInwardInvoicesMonthWise(materialInwardDashboardDTO);
            ViewBag.Months = new SelectList(Enumerable.Range(1, 12).Select(x => new SelectListItem() { Text = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[x - 1], Value = x.ToString() }), "Value", "Text");
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
            return View("MaterialInwardDashboards", materialInwardDashboardDTO);
        }

        #endregion

        #region Material Outward

        [HttpGet]
        public ActionResult MaterialOutwardDashboards(MaterialInwardDashboardDTO materialInwardDashboardDTO)
        {
            materialInwardDashboardDTO.Branch = BRANCH_ID;
            materialInwardDashboardDTO.FromMonth = Convert.ToInt16(DateTime.Now.Month);
            materialInwardDashboardDTO.ToMonth = Convert.ToInt16(DateTime.Now.Month);
            materialInwardDashboardDTO.Year = Convert.ToInt16(DateTime.Now.Year);
            materialInwardDashboardDTO.ProductCode = 0;
            materialInwardDashboardDTO.MaterialInwardProductQty = new DashBoardBO().GetMaterialOutwardProductQtyMonthWise(materialInwardDashboardDTO);
            materialInwardDashboardDTO.MaterialInwardInvoices = new DashBoardBO().GetMaterialOutwardInvoicesMonthWise(materialInwardDashboardDTO);
            ViewBag.Months = new SelectList(Enumerable.Range(1, 12).Select(x => new SelectListItem() { Text = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[x - 1], Value = x.ToString() }), "Value", "Text");
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
            return View(materialInwardDashboardDTO);
        }
        [HttpPost]
        public ActionResult GetMaterialOutwardProductCost(MaterialInwardDashboardDTO dashBoardParams)
        {
            if(dashBoardParams.Branch==0)
            {
                dashBoardParams.Branch = BRANCH_ID;
            }
            var MaterialOutwardProductCost = new DashBoardBO().GetMaterialOutwardProductCostMonthWise(dashBoardParams);
            return Json(MaterialOutwardProductCost, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetMaterialOutwardDashboards(MaterialInwardDashboardDTO materialOutwardDashboardDTO)
        {
            materialOutwardDashboardDTO.MaterialInwardProductQty = new DashBoardBO().GetMaterialOutwardProductQtyMonthWise(materialOutwardDashboardDTO);
            materialOutwardDashboardDTO.MaterialInwardInvoices = new DashBoardBO().GetMaterialOutwardInvoicesMonthWise(materialOutwardDashboardDTO);
            ViewBag.Months = new SelectList(Enumerable.Range(1, 12).Select(x => new SelectListItem() { Text = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[x - 1], Value = x.ToString() }), "Value", "Text");
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
            return View("MaterialOutwardDashboards", materialOutwardDashboardDTO);
        }
        #endregion

        #region RMA

        [HttpGet]
        public ActionResult GetRMAReceivedReturnedDashboards(RMAGenerationReceiveDTO rmaGenerationReceiveDTO)
        {
            rmaGenerationReceiveDTO.Branch = BRANCH_ID;
            rmaGenerationReceiveDTO.FromMonth = Convert.ToInt16(1);
            rmaGenerationReceiveDTO.ToMonth = Convert.ToInt16(DateTime.Now.Month);
            rmaGenerationReceiveDTO.Year = Convert.ToInt16(DateTime.Now.Year);
            rmaGenerationReceiveDTO.ProductCode = 0;
            rmaGenerationReceiveDTO.RMAGeneration = new DashBoardBO().GetRMAGenerationValues(rmaGenerationReceiveDTO);
            rmaGenerationReceiveDTO.RMAReturned = new DashBoardBO().GetRMAReturnedValues(rmaGenerationReceiveDTO);
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
            return View("RMAReceivedReturnedDashboards", rmaGenerationReceiveDTO);
        }
        [HttpPost]
        public ActionResult RMAReceivedReturnedDashboards(RMAGenerationReceiveDTO rmaGenerationReceiveDTO)
        {
            if (rmaGenerationReceiveDTO.Branch == 0)
            {
                rmaGenerationReceiveDTO.Branch = BRANCH_ID;
            }
            rmaGenerationReceiveDTO.RMAGeneration = new DashBoardBO().GetRMAGenerationValues(rmaGenerationReceiveDTO);
            rmaGenerationReceiveDTO.RMAReturned = new DashBoardBO().GetRMAReturnedValues(rmaGenerationReceiveDTO);
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
            return View(rmaGenerationReceiveDTO);
        }
        [HttpGet]
        public ActionResult RMACreditNoteandValue(RMACreditNoteDTO rmaCreditNoteDTO)
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
        [HttpPost]
        public ActionResult RMACreditNoteProductCategory(RMACreditNoteDTO rmaCreditNoteDTO)
        {
            if (rmaCreditNoteDTO.Branch == 0)
            {
                rmaCreditNoteDTO.Branch = BRANCH_ID;
            }
            var RMACreditNoteProductCategory = new DashBoardBO().GetRMACreditNoteProductCategory(rmaCreditNoteDTO);
            return Json(RMACreditNoteProductCategory, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetRMACreditNoteandValue(RMACreditNoteDTO rmaCreditNoteDTO)
        {
            if (rmaCreditNoteDTO.Branch == 0)
            {
                rmaCreditNoteDTO.Branch = BRANCH_ID;
            }
            rmaCreditNoteDTO.RMACreditNoteValue = new DashBoardBO().GetRMACreditNoteValue(rmaCreditNoteDTO);
            ViewBag.Branches = new BranchBO().GetList().Select(x => new { Value = x.BranchID, Text = x.BranchName });
            ViewBag.Year = new DashBoardBO().GetRMAOutwardYear().Select(x => new { Value = x.Year, Text = x.Year });
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
            return View("RMACreditNoteandValue", rmaCreditNoteDTO);
        }

        [HttpGet]
        public ActionResult RMAProductCategoryPriceandValue(ProductCategoryRMAReceiptandValueDTO productCategoryRMAPriceandValueDTO)
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
        [HttpPost]
        public ActionResult GetRMAProductCategoryReceiptandValue(ProductCategoryRMAReceiptandValueDTO ProductCategoryRMAPriceandValueDTO)
        {
            if (ProductCategoryRMAPriceandValueDTO.Branch == 0)
            {
                ProductCategoryRMAPriceandValueDTO.Branch = BRANCH_ID;
            }
            ProductCategoryRMAPriceandValueDTO.ProductCategoryRMAReceipt = new DashBoardBO().GetProductCategoryRMAReceipt(ProductCategoryRMAPriceandValueDTO);
            ProductCategoryRMAPriceandValueDTO.ProductCategoryRMAValue = new DashBoardBO().GetProductCategoryRMAValue(ProductCategoryRMAPriceandValueDTO);
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
            return View("RMAProductCategoryPriceandValue", ProductCategoryRMAPriceandValueDTO);
        }
        [HttpGet]
        public ActionResult RMAProductCategoryPriceandValueMonthWise(ProductCategoryMonthWiseRMAReceiptandValueDTO productCategoryMonthWiseRMAPriceandValueDTO)
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
        [HttpPost]
        public ActionResult GetRMAProductCategoryPriceandValueMonthWise(ProductCategoryMonthWiseRMAReceiptandValueDTO productCategoryMonthWiseRMAPriceandValueDTO)
        {
            if (productCategoryMonthWiseRMAPriceandValueDTO.Branch == 0)
            {
                productCategoryMonthWiseRMAPriceandValueDTO.Branch = BRANCH_ID;
            }
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
            return View("RMAProductCategoryPriceandValueMonthWise", productCategoryMonthWiseRMAPriceandValueDTO);
        }
        #endregion
    }
}

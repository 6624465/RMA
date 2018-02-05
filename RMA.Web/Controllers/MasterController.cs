using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using EZY.RMAS.Contract;
using EZY.RMAS.BusinessFactory;

namespace RMA.Web.Controllers
{
    [WebSsnFilter]
    public class MasterController : BaseController
    {
        /* Product Category Start */
        [HttpGet]
        public ActionResult ProductCategoryList()
        {
            var productCategoryList = new ProductCategoryBO()
                        .GetList()
                        .ToList();
            return View(productCategoryList);
        }

        [HttpGet]
        public PartialViewResult ProductCategoryGroup(short? productCategory)
        {
            var lookupBO = new LookupBO();
            ViewBag.ProductCategoryList = lookupBO.GetList()
                                       .Where(x => x.LookupCategory == "CategoryGroup")
                                       .Select(x => new LookUpSelect
                                       {
                                           Value = x.LookupID,
                                           Text = x.LookupDescription
                                       }).ToList<LookUpSelect>();
            if (productCategory == null)
            {
                ViewBag.Title = "New Product Category";
                return PartialView("ProductCategory", new ProductCategoryMaster());
            }
            else
            {
                ViewBag.Title = "Update Product Category";
                return PartialView("ProductCategory", new ProductCategoryBO()
                                        .GetList()
                                        .Where(x => x.ProductCategory == productCategory)
                                        .FirstOrDefault());

            }
        }
        [HttpPost]
        public ActionResult ProductCategory(ProductCategoryMaster productCategoryMaster)
        {
            //productCategoryMaster.LookupCategory = UTILITY.CONFIG_LOOKUPCATEGORY_PRODUCTCATEGORY;
            var lookUpBO = new ProductCategoryBO();
            var result = lookUpBO.SaveProduct(productCategoryMaster);

            return RedirectToAction("ProductCategoryList");
        }
        /*  Product Category End */

        /* Category Group Start */
        [HttpGet]
        public ActionResult CategoryList()
        {
            var CategoryList = new LookupBO()
                        .GetList()
                        .Where(x => x.LookupCategory == UTILITY.CONFIG_LOOKUPCATEGORY_CATEGORY)
                        .ToList();

            return View(CategoryList);
        }

        public PartialViewResult CategoryGroup(short? lookupID)
        {
            if (lookupID == null)
            {
                ViewBag.Title = "New Category Group";
                return PartialView(new Lookup());
            }
            else
            {
                ViewBag.Title = "Update Category Group";
                return PartialView(new LookupBO()
                                        .GetList()
                                        .Where(x => x.LookupID == lookupID)
                                        .FirstOrDefault());

            }
        }
        [HttpPost]
        public ActionResult SaveCategoryGroup(Lookup lookUp)
        {
            lookUp.LookupCategory = UTILITY.CONFIG_LOOKUPCATEGORY_CATEGORY;
            lookUp.Status = true;
            lookUp.ISOCode = "";
            lookUp.MappingCode = "";
            lookUp.CreatedBy = USER_ID;
            lookUp.CreatedOn = UTILITY.SINGAPORETIME;
            lookUp.ModifiedBy = USER_ID;
            lookUp.ModifiedOn = UTILITY.SINGAPORETIME;

            var lookUpBO = new LookupBO();
            var result = lookUpBO.SaveLookup(lookUp);

            return RedirectToAction("CategoryList");
        }
        [HttpPost]
        public JsonResult SaveCategory(string ProductCategory)
        {
            Lookup lookUp = new Lookup();
            lookUp.LookupCategory = UTILITY.CONFIG_LOOKUPCATEGORY_CATEGORY;
            lookUp.Status = true;
            lookUp.ISOCode = "";
            lookUp.MappingCode = "";
            lookUp.CreatedBy = USER_ID;
            lookUp.CreatedOn = UTILITY.SINGAPORETIME;
            lookUp.ModifiedBy = USER_ID;
            lookUp.ModifiedOn = UTILITY.SINGAPORETIME;
            lookUp.LookupDescription = "Desc";
            lookUp.LookupCode = ProductCategory;
            var lookUpBO = new LookupBO();
            var result = lookUpBO.SaveLookup(lookUp);
            ViewBag.ProductCategoryList = lookUpBO.GetList()
                                    .Where(x => x.LookupCategory == "CategoryGroup")
                                    .Select(x => new LookUpSelect
                                    {
                                        Value = x.LookupID,
                                        Text = x.LookupCode
                                    }).ToList<LookUpSelect>();
            return Json(ViewBag.ProductCategoryList, JsonRequestBehavior.AllowGet);
        }
        /* Category Group End */

        /* Currency Start */
        [HttpGet]
        public ActionResult CurrencyList()
        {
            var currencyCategoryList = new LookupBO()
                        .GetList()
                        .Where(x => x.LookupCategory == UTILITY.CONFIG_LOOKUPCATEGORY_CURRENCY)
                        .ToList();

            return View(currencyCategoryList);
        }

        public PartialViewResult Currency(short? lookupID)
        {
            if (lookupID == null)
            {
                ViewBag.Title = "New Currency";
                return PartialView(new Lookup());
            }
            else
            {
                ViewBag.Title = "Update Currency";
                return PartialView(new LookupBO()
                                        .GetList()
                                        .Where(x => x.LookupID == lookupID)
                                        .FirstOrDefault());

            }
        }

        [HttpPost]
        public ActionResult Currency(Lookup lookUp)
        {
            lookUp.LookupCategory = UTILITY.CONFIG_LOOKUPCATEGORY_CURRENCY;
            lookUp.Status = true;
            lookUp.ISOCode = "";
            lookUp.MappingCode = "";
            lookUp.CreatedBy = USER_ID;
            lookUp.CreatedOn = UTILITY.SINGAPORETIME;
            lookUp.ModifiedBy = USER_ID;
            lookUp.ModifiedOn = UTILITY.SINGAPORETIME;

            var lookUpBO = new LookupBO();
            var result = lookUpBO.SaveLookup(lookUp);

            return RedirectToAction("CurrencyList");
        }
        /* Currency End */

        [HttpGet]
        public ActionResult Products()
        {
            return View("Products", new List<Product>());
        }

        public PartialViewResult Product(string productCode = "", string modelNo = "")
        {
            var lookupBO = new LookupBO();
            ViewBag.ProductCategoryList = lookupBO.GetList()
                                    .Where(x => x.LookupCategory == "CategoryGroup")
                                    .Select(x => new LookUpSelect
                                    {
                                        Value = x.LookupID,
                                        Text = x.LookupCode
                                    }).ToList<LookUpSelect>();
            if (string.IsNullOrWhiteSpace(productCode))
            {

                ViewBag.Title = "New Product";
                return PartialView(new Product { Status = true });
            }
            else
            {
                ViewBag.Title = "Update Product";
                return PartialView(new ProductBO().GetProduct(new Product { ProductCode = productCode, ModelNo = modelNo }));

            }
        }

        [HttpPost]
        public ActionResult Product(Product product)
        {
            var productBO = new ProductBO();

            product.ProductCode = product.ModelNo;
            product.CreatedBy = USER_ID;
            product.CreatedOn = UTILITY.SINGAPORETIME;
            product.ModifiedBy = USER_ID;
            product.ModifiedOn = UTILITY.SINGAPORETIME;
            var result = productBO.SaveProduct(product);

            return RedirectToAction("Products");
        }

        [HttpPost]
        public ActionResult DeleteProduct(string productCode, string modelNo)
        {
            var productBO = new ProductBO();
            var result = productBO.DeleteProduct(new Product { ProductCode = productCode, ModelNo = modelNo });
            return View("Products", productBO.GetList());
        }

        [HttpPost]
        public JsonResult GetProducts(DataTableObject Obj)
        {
            var products = new ProductBO().GetProductTableDataList(Obj);
            var totalRecords = new ProductBO().GetList().Count();

            return Json(new { products = products, totalRecords = totalRecords }, JsonRequestBehavior.AllowGet);
        }
    }
}
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
    public class CustomerController : BaseController
    {
        [HttpGet]
        public ActionResult Customers()
        {
            return View("Customers", new List<Customer>());
        }

        private void SetUpLookupData()
        {
            var lookupBO = new LookupBO();

            ViewBag.CustomerType = lookupBO.GetList()
                                        .Where(x => x.LookupCategory == "CustomerType")
                                        .Select(x => new LookUpSelect
                                        {
                                            Value = x.LookupID,
                                            Text = x.LookupDescription
                                        }).ToList<LookUpSelect>();

            ViewBag.BranchList = new BranchBO().GetList()
                                      .Select(x => new Branch
                                      {
                                          BranchID = x.BranchID,
                                          BranchName = x.BranchName
                                      }).ToList<Branch>();

            ViewBag.Country = new CountryBO().GetList().Select(x => new
            {
                Value = x.CountryCode,
                Text = x.CountryName
            }).ToList();


            // ViewBag.Country = countryList;
        }

        [HttpGet]
        [EncryptedActionParameter]
        public ActionResult Customer(string customerCode = "")
        {
            SetUpLookupData();
            if (string.IsNullOrWhiteSpace(customerCode))
            {
                ViewBag.HeaderTitle = "New Contact";
                return View("Customer", new Customer { Status = true });
            }
            else
            {
                ViewBag.HeaderTitle = "Update Contact";
                return View("Customer", new CustomerBO().GetCustomer(new Customer { BranchID = BRANCH_ID, CustomerCode = customerCode }));
            }

        }

        [HttpPost]
        public ActionResult SaveCustomer(Customer customer)
        {
            customer.CreatedBy = USER_ID;
            customer.CreatedOn = UTILITY.SINGAPORETIME;
            customer.ModifiedBy = USER_ID;
            customer.ModifiedOn = UTILITY.SINGAPORETIME;
            customer.BranchID = BRANCH_ID;

            var result = new CustomerBO().SaveCustomer(customer);

            SetUpLookupData();
            return View("Customers", new CustomerBO().GetList(BRANCH_ID));
        }

        [HttpPost]
        public ActionResult DeleteCustomer(string CustomerCode)
        {
            var result = new CustomerBO().DeleteCustomer(new Customer { CustomerCode = CustomerCode, BranchID = BRANCH_ID });

            var customers = new CustomerBO().GetList(BRANCH_ID);
            return View("Customers", customers);
        }

        [HttpPost]
        public JsonResult GetCustomers(DataTableObject Obj)
        {
            Obj.BranchID = BRANCH_ID;
            var customers = new CustomerBO().GetCustomerTableDataList(Obj);
            var totalRecords = new CustomerBO().GetList(BRANCH_ID).Count();

            return Json(new
            {
                customers = customers.Select(x => new
                {
                    CustomerTypeDescription = x.CustomerTypeDescription,
                    CustomerCode = x.CustomerCode,
                    CustomerName = x.CustomerName,
                    Status = x.Status,
                    EncryptStr = UrlEncryptionHelper.Encrypt(string.Format("customerCode={0}", x.CustomerCode))
                }),
                totalRecords = totalRecords
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using EZY.RMAS.Contract;
using EZY.RMAS.BusinessFactory;

namespace RMA.Web.Controllers
{
    public class ServicesController : BaseController
    {
        // GET: Services
       public ActionResult ProductInquiry()
        {
            return View();
        }
        public ActionResult CreateJob(string serialNo)
        {
            var jobheader = new JobFormHeaderBO().GetJobFormHeader(BRANCH_ID, serialNo);

            jobheader.EngineerList = new UsersBO().GetList().Where(x => x.RoleCode == "Engineer").Select(x => new SelectListItem
            {
                Value = x.RoleCode,
                Text = x.UserName
            }).ToList();
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
            jobheader.ProductCategoryList = ProductCategory;
            jobheader.ServiceTypeList = new LookupBO()
                                    .GetList()
                                    .Where(x => x.LookupCategory == "Service Type" && x.Status == true)
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.LookupID.ToString(),
                                        Text = x.LookupCode
                                    })
                                    .ToList();
            return View(jobheader);
        }

        [HttpPost]
        public ActionResult CreateJob(JobFormHeader jobheader)
        {
            JobFormDetail jobdetail = new JobFormDetail();
            jobheader.EngineerList = new UsersBO().GetList().Where(x => x.RoleCode == "Engineer").Select(x => new SelectListItem
            {
                Value = x.RoleCode,
                Text = x.UserName
            }).ToList();
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
            jobheader.ProductCategoryList = ProductCategory;
            jobheader.ServiceTypeList = new LookupBO()
                                    .GetList()
                                    .Where(x => x.LookupCategory == "Service Type" && x.Status == true)
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.LookupID.ToString(),
                                        Text = x.LookupCode
                                    })
                                    .ToList();

            if (jobheader.JobFormDetails == null)
                jobheader.JobFormDetails = new List<JobFormDetail>();
            jobheader.JobFormDetails.Add(jobdetail);
            return View(jobheader);
        }

        [HttpPost]
        public ActionResult JobFormSave(JobFormHeader jobheader)
        {
            jobheader.BranchID = BRANCH_ID;
            jobheader.CreatedBy = USER_ID;
            jobheader.CreatedOn = DateTime.Now;
            var jobsaved = new JobFormHeaderBO().SaveJobFormHeader(jobheader);

            return RedirectToAction("ProductInquiry");
        }
        public ActionResult JobSheetList()
        {
            var jobheaderlist = new JobFormHeaderBO().GetList(BRANCH_ID);
            return View(jobheaderlist);
        }
        public ActionResult ServiceStore()
        {
            return View();
        }
        public ActionResult CSM()
        {
            return View();
        }
        public ActionResult VendorClaimProcess()
        {
            return View();
        }
        public ActionResult CreateServiceQuote()
        {
            return View();
        }
        public ActionResult StoresBranch()
        {
            return View();
        }
        public ActionResult Reports()
        {
            return View();
        }
    }
}
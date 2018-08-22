using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMA.Web.Controllers
{
    public class ServicesController : Controller
    {
        // GET: Services
       public ActionResult ProductInquiry()
        {
            return View();
        }
        public ActionResult CreateJob()
        {
            return View();
        }
        public ActionResult JobSheetList()
        {
            return View();
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
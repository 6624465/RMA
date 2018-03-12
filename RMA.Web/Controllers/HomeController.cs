using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EZY.RMAS.BusinessFactory;
using EZY.RMAS.Contract;
namespace RMA.Web.Controllers
{
    public class HomeVm
    {
        public bool IsMaster { get; set; } = false;
        public bool IsTransactions { get; set; } = false;
        public bool IsAdministration { get; set; } = false;
        public bool IsReports { get; set; } = false;
    }

    [WebSsnFilter]
    public class HomeController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            var userSecurables = USER_SECURABLES;
            var homeVm = new HomeVm();
            if (userSecurables != null && userSecurables.Count > 0)
            {
                homeVm.IsMaster = userSecurables
                                    .Where(x => x.GroupID == "Master")
                                    .Count() > 0;

                homeVm.IsTransactions = userSecurables
                                        .Where(x => x.GroupID == "Transaction")
                                        .Count() > 0;

                homeVm.IsAdministration = userSecurables
                                        .Where(x => x.GroupID == "Administration")
                                        .Count() > 0;

                homeVm.IsReports = userSecurables
                                        .Where(x => x.GroupID == "Reports")
                                        .Count() > 0;
            }


            return View(homeVm);
        }
        public ActionResult Administration()
        {
            return View();
        }
        public ActionResult Reports()
        {
            return View();
        }
        public ActionResult Transactions()
        {
            return View();
        }
        public ActionResult Masters()
        {
            return View();
        }
        public ActionResult COO()
        {
            return View();
        }
        public class test
        {
            public string name { get; set; }
            public List<tets1> list { get; set; }
        }

        public class tets1
        {
            public int count { get; set; }
        }

        public class Title
        {
            public string text { get; set; }
        }

        public class XAxis
        {
            public List<string> categories { get; set; }
        }

        public class Series
        {
            public List<double> data { get; set; }
        }

        public class RootObject
        {
            public Title title { get; set; }
            public XAxis xAxis { get; set; }
            public List<Series> series { get; set; }
        }
    }
}

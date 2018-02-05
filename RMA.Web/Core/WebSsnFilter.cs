using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMA.Web
{
    public class WebSsnFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var ssnObj = (SessionObject)HttpContext.Current.Session[UTILITY.SSN_USER_OBJECT];
            if (ssnObj == null)
            {
                filterContext.Result = new RedirectResult("~/Account/Index");
            }
        }
    }
}
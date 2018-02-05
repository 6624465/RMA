using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using EZY.RMAS.Contract;
using System.IO;

namespace RMA.Web
{
    public class BaseController : Controller
    {
        public string RenderViewToString(ControllerContext context,
                                string viewPath,
                                object model = null,
                                bool partial = false)
        {
            ViewEngineResult viewEngineResult = null;
            if (partial)
                viewEngineResult = ViewEngines.Engines.FindPartialView(context, viewPath);
            else
                viewEngineResult = ViewEngines.Engines.FindView(context, viewPath, null);

            if (viewEngineResult == null)
                throw new FileNotFoundException("View cannot be found.");


            var view = viewEngineResult.View;
            context.Controller.ViewData.Model = model;

            string result = null;

            using (var sw = new StringWriter())
            {
                var ctx = new ViewContext(context, view,
                                            context.Controller.ViewData,
                                            context.Controller.TempData,
                                            sw);
                view.Render(ctx, sw);
                result = sw.ToString();
            }

            return result;
        }
    public SessionObject USER_OBJECT
        {
            get
            {
                return (SessionObject)System.Web.HttpContext.Current.Session[UTILITY.SSN_USER_OBJECT];
            }
            set
            {
                Session[UTILITY.SSN_USER_OBJECT] = value;
            }
        }

        public List<Securables> USER_SECURABLES
        {
            get
            {
                return (List<Securables>)System.Web.HttpContext.Current.Session[UTILITY.SSN_USER_SECURABLES];
            }
            set
            {
                Session[UTILITY.SSN_USER_SECURABLES] = value;
            }
        }

        public string USER_EMAIL
        {
            get
            {
                return USER_OBJECT.Email;
            }
        }

        public string USER_NAME
        {
            get
            {
                return USER_OBJECT.UserName;
            }
        }

        public string USER_ID
        {
            get
            {
                return USER_OBJECT.UserID;
            }
        }

        public short BRANCH_ID
        {
            get
            {
                return USER_OBJECT.BranchId;
            }
        }

        public string BRANCH_NAME
        {
            get
            {
                return USER_OBJECT.BranchName;
            }
        }

        public string COMPANY_ID
        {
            get
            {
                return USER_OBJECT.CompanyId;
            }
        }

        public string COMPANY_NAME
        {
            get
            {
                return USER_OBJECT.CompanyName;
            }
        }

        public string ROLE_CODE
        {
            get
            {
                return USER_OBJECT.RoleCode;
            }
        }

        public void CreateDirectory(string path)
        {
            var dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
                dirInfo.Create();
        }
    }

    public class SessionObject
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string RoleCode { get; set; }
        public short BranchId { get; set; }
        public string BranchName { get; set; }

        public string CompanyId { get; set; }

        public string CompanyName { get; set; }
    }
}
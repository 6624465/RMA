
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using EZY.RMAS.BusinessFactory;

namespace RMA.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            GetAllSecurables();
        }

        protected void GetAllSecurables()
        {
            var securablesAll = new RoleRightsBO()
                                    .GetSecurableItemsList();

            Application["AppSecurables"] = securablesAll;
        }
    }
}

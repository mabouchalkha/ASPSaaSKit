using StarterKit.Utils;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;

namespace StarterKit
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //if (!HttpContext.Current.IsDebuggingEnabled)
            //{
            //    Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
            //}
        }

        void Application_Error(object sender, EventArgs e)
        {
            Response.Clear();

            Exception ex = Server.GetLastError();

            Response.ContentType = "application/json";
            Response.StatusCode = 500;

            Response.Write(new JavaScriptSerializer().Serialize(new { success = false, message = ErrorUtil.GetInnerMessage(ex) }));

            Server.ClearError();
        }
    }
}

using StarterKit.Architecture;
using StarterKit.Utils;
using System;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using Stripe;
using System.Configuration;
using StarterKit.Architecture.MEF;
using System.Threading;
using System.Globalization;
using FluentValidation.Mvc;

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
            FluentValidationModelValidatorProvider.Configure();

            StripeConfiguration.SetApiKey(ConfigurationManager.AppSettings["stripeSecretKey"]);

            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
            CompositionContainer container = MEFLoader.Init(catalog.Catalogs);

            DependencyResolver.SetResolver(new MefDependencyResolver(container));
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

        protected void Application_BeginRequest()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("fr");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr");
        }
    }
}

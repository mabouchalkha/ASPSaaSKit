using System.Web.Optimization;

namespace StarterKit
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/style.css",
                      "~/Content/custom.css",
                      "~/Content/font-awesome.css",
                      "~/Lib/angular-toastr.css",
                      "~/Lib/ui-grid.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/app")
                .Include("~/Scripts/app.js")
                .Include("~/Scripts/home_ctrl.js")
                .Include("~/Lib/angular-toastr.tpls.js")
                .Include("~/Lib/slim_scroll.js")
                .Include("~/Lib/ui-grid.min.js")
                 .Include("~/Lib/bootstrap.min.js")
                .IncludeDirectory("~/Scripts/Factories", "*.js", true)
                .IncludeDirectory("~/Scripts/Directives", "*.js", true)
                .IncludeDirectory("~/Scripts/Pages/One", "*.js", true)
                .IncludeDirectory("~/Scripts/Pages/Two", "*.js", true)
                .IncludeDirectory("~/Scripts/Pages/Login", "*.js", true)
                .IncludeDirectory("~/Scripts/Pages/Account", "*.js", true)
                .IncludeDirectory("~/Scripts/Pages/ResetPassword", "*.js", true)
                .IncludeDirectory("~/Scripts/Pages/ConfirmEmail", "*.js", true)
                .IncludeDirectory("~/Scripts/Pages/TwoFactor", "*.js", true)
                .IncludeDirectory("~/Scripts/Pages/Dashboard", "*.js", true)
                .IncludeDirectory("~/Scripts/Pages/User", "*.js", true)
                .IncludeDirectory("~/Scripts/Pages/Index", "*.js", true));
                

            BundleTable.EnableOptimizations = false;
        }
    }
}
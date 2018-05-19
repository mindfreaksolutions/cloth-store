using System.Web;
using System.Web.Optimization;

namespace clothStoreAdmin
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/adminJs").Include(
                       "~/Scripts/jquery-1.10.2.min.js",
                       "~/Scripts/adminModernizr-respond.min.js",
                       "~/Scripts/adminBootstrap.min.js",
                       "~/Scripts/adminPlugins.js",
                       "~/Scripts/adminMain.js"));
            bundles.Add(new StyleBundle("~/Content/adminCss").Include(
                      "~/Content/adminBootstrap.css",
                      "~/Content/adminPlugins.css",
                      "~/Content/adminMain.css",
                      "~/Content/adminThemes.css"
                    ));
        }
    }
}

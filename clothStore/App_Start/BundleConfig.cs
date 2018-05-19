using System.Web;
using System.Web.Optimization;

namespace clothStore
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-1.10.2.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/webJs").Include(
                      //"~/Scripts/bootstrap.js",
                      "~/Scripts/webBootstrap.min.js",
                      "~/Scripts/own-menu.js",
                      "~/Scripts/jquery.lighter.js",
                      "~/Scripts/owl.carousel.min.js",
                      "~/Scripts/jquery.tp.t.min.js",
                      "~/Scripts/jquery.tp.min.js",
                      "~/Scripts/main.js",
                      "~/Scripts/respond.js"));
            bundles.Add(new StyleBundle("~/Content/webCss").Include(
                      "~/Content/settings.css",
                      "~/Content/webBootstrap.min.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/ionicons.min.css",
                      "~/Content/main.css",
                      "~/Content/style.css",
                       "~/Content/site.css",
                       "~/Content/responsive.css"));
        }
    }
}

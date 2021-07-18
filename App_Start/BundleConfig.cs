using System.Web;
using System.Web.Optimization;

namespace Couponat
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/Admin/jquery").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/Admin/jquery-ui.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/Public/Scripts").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/Admin/jquery-ui.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/Public/js").Include(
                "~/Scripts/jquery-3.4.1.min.js",
                "~/Scripts/Public/bootstrap.min.js",
                "~/Scripts/modernizr-2.8.3.js",
                "~/Scripts/Public/owl.carousel.min.js",
                "~/Scripts/Public/jquery.flexslider-min.js",
                "~/Scripts/Public/jquery.countdown.js",
                "~/Scripts/Public/main.js"));

            bundles.Add(new StyleBundle("~/Public/css").Include(
                "~/Content/Public/bootstrap.min.css",
                "~/Content/Public/bootstrap-rtl.css",
                "~/Content/Public/font-awesome.min.css",
                "~/Content/Public/linearicons.css",
                "~/Content/Public/owl.carousel.min.css",
                "~/Content/Public/owl.theme.min.css",
                "~/Content/Public/flexslider.css",
                "~/Content/Public/base.css",
                "~/Content/Public/style.css"));
        }
    }
}

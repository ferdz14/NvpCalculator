using System.Web;
using System.Web.Optimization;

namespace NpvCalculator
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterScriptBundle(bundles);
            RegisterStyleBundle(bundles);
        }

        private static void RegisterScriptBundle(BundleCollection bundles)
        {
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

            bundles.Add(new ScriptBundle("~/bundles/charts").Include(
                      "~/Scripts/charts.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/gridmvc").Include(
                      "~/Scripts/ladda-bootstrap/*.min.js",
                      "~/Scripts/URI.js",
                      "~/Scripts/gridmvc.min.js",
                      "~/Scripts/gridmvc-ext.js"));

            bundles.Add(new ScriptBundle("~/bundles/npvcalculator").Include(
                      "~/Scripts/application/npvcalculator.js"));

            bundles.Add(new ScriptBundle("~/bundles/npvhistory").Include(
                      "~/Scripts/application/npvhistory.js"));
        }

        private static void RegisterStyleBundle(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}

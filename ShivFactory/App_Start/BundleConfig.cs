using System.Web;
using System.Web.Optimization;

namespace ShivFactory
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
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

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            //Added for toaster
            bundles.Add(new ScriptBundle("~/bundles/toastr").Include(
                       "~/Scripts/toastr.js*",
                       "~/Scripts/toastrImp.js"));
            //Modify for toastr
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css",
                                                                "~/Content/toastr.css"));


            // Bundling For WebSite Layout           
            //On Top page
           
            bundles.Add(new StyleBundle("~/_WebSiteLayout/css").Include(
               // "~/Content/Website/css/bootstrap.css",
                "~/Content/Website/fonts/fontawesome/css/all.min.css",
                "~/Content/Website/fonts/Pe-icon-7-stroke.css"
                //"~/Content/Website/css/ui.css",
                //"~/Content/Website/css/stylecode.css",
                //"~/Content/Website/css/responsive.css"
                ));

            var CssCdnPath = "https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css";
            bundles.Add(new ScriptBundle("~/_WebSiteLayout/css", CssCdnPath).Include("~/Scripts/jquery-{version}.js"
               // "~/Content/Website/css/style.css"
                ));
            //the following creates bundles in debug mode;
            //BundleTable.EnableOptimizations = true;



        }
    }
}

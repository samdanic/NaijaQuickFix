using System.Web;
using System.Web.Optimization;

namespace NaijaQuickFix
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                     "~/Scripts/jquery-{version}.js",
                      "~/Scripts/bootstrap.js",
                      "~/ Scripts/bootstrap.min.js",
                      "~/Scripts/bootstrap-dialog.min.js",
                   //"~/Scripts/bootbox.js",
                   //"~/Scripts/datatables/jquery.datatables.js",
                   //"~/Scripts/datatables/datatables.bootstrap.js",
                   //"~/Scripts/typeahead.bundle.js",
                   //"~/Scripts/toastr.js",
                   "~/Scripts/Global/form.js",
                   "~/Scripts/Global/layout.js",
                    "~/Scripts/Global/ManageArtisan.js"));


            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"
                       //"~/Scripts/Global/form.js"
                       ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/signalR").Include(
                        "~/Scripts/jquery-3.4.1.js",
                        "~/Scripts/jquery.signalR-2.4.3.min.js"
                        ));
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap.min.js",
                     "~/Scripts/bootstrap-dialog.min.js",
                      "~/Scripts/respond.js",                      "~/Scripts/respond.js",
                     "~/Scripts/jquery-ui.js"
                      ));
            bundles.Add(new ScriptBundle("~/bundles/javaScript").Include(
                   "~/Scripts/login.js",
                   "~/Scripts/home.js",
                   "~/Scripts/header.js",
                   "~/Scripts/register.js",
                   "~/Scripts/dashboard.js",
                   "~/Scripts/table.js",
                   "~/Scripts/profile.js",
                   "~/Scripts/DataTables/jquery.dataTables.js",
                      "~/Scripts/DataTables/dataTables.bootstrap.js"
                   ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/content/bootstrap.css",
                      "~/Content/root.css",
                      "~/Content/home.css",
                      "~/Content/login.css",
                      "~/Content/register.css",
                      "~/Content/profile.css"
                      //"~/Content/DataTables/css/dataTables.bootstrap.css"
                      ));
        }
    }
}

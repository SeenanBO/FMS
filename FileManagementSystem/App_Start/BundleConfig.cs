using System.Web;
using System.Web.Optimization;

namespace FileManagementSystem
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                      "~/Scripts/jquery.min.js",
                       "~/Scripts/popper.js",
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/jquery.dataTables.js",
                      "~/Scripts/dataTables.bootstrap4.js",
                      "~/Scripts/perfect-scrollbar.min.js",
                      "~/Scripts/select2.min.js",
                      "~/Scripts/sweetalert2.min.js",
                      "~/Scripts/coreui.min.js",
                      "~/Scripts/jquery.easy-ticker.min.js",
                      "~/Scripts/barcode.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/scriptsval").Include(
                      "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new StyleBundle("~/bundles/styles").Include(
                      "~/Content/coreui-icons.min.css",
                      "~/Content/dataTables.bootstrap4.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/select2.min.css",
                      "~/Content/simple-line-icons.css",
                      "~/Content/style.css",
                      "~/Content/sweetalert2.min.css"));
        }
    }
}

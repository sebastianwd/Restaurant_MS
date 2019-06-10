using System.Web;
using System.Web.Optimization;

namespace Restaurant_MS
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
     
            bundles.Add(new ScriptBundle("~/bundles/formsJs").Include(
            "~/Scripts/jquery.form.min.js",
              "~/Scripts/jquery.validate.min.js",
                "~/Scripts/jquery.validate.unobtrusive.min.js",
"~/Content/cleavejs/cleave.js"
           ));

    
            bundles.Add(new StyleBundle("~/bundles/loginCss").Include(
                "~/Content/Main/login/main.css",
                "~/Content/core-ui/icons/font-awesome.min.css"
             ));

            bundles.Add(new ScriptBundle("~/bundles/tablesJs").Include(
                 "~/Content/DataTables/datatables.min.js",
                 "~/Content/DataTables/js/dataTables.bootstrap4.min.js",
                 "~/Content/DataTables/Buttons-1.5.4/js/dataTables.buttons.min.js",
                 "~/Content/DataTables/Buttons-1.5.4/js/buttons.bootstrap4.min.js",
                 "~/Content/DataTables/Select-1.2.6/js/dataTables.select.min.js",
                 "~/Content/DataTables/Select-1.2.6/js/select.bootstrap4.min.js",
                 "~/Content/DataTables/CellEdit-1.0.19/dataTables.cellEdit.js",
                 "~/Content/DataTables/Responsive-2.2.2/js/dataTables.responsive.min.js",
                 "~/Content/DataTables/js/Responsive-2.2.2/js/responsive.bootstrap4.min.js",
                 "~/Content/DataTables/KeyTable-2.5.0/js/dataTables.keyTable.min.js",
                 "~/Content/DataTables/KeyTable-2.5.0/js/keyTable.bootstrap4.min.js"
                        ));

            bundles.Add(new StyleBundle("~/bundles/tablesCss").Include(
                 "~/Content/DataTables/DataTables-1.10.18/css/dataTables.bootstrap4.css",
                 "~/Content/DataTables/Buttons-1.5.4/css/buttons.bootstrap4.css",
                 "~/Content/DataTables/Select-1.2.6/css/select.bootstrap4.css",
                 "~/Content/DataTables/Responsive-2.2.2/css/responsive.bootstrap4.css",
                 "~/Content/DataTables/KeyTable-2.5.0/css/keyTable.bootstrap4.css"
           ));

            BundleTable.EnableOptimizations = true;
        }
    }
}

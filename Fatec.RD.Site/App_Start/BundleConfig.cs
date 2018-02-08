using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization;

namespace Fatec.RD.Site
{
    // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.unobtrusive*",
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                "~/Scripts/knockout-{version}.js",
                "~/Scripts/knockout.validation.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/Scripts/sammy-{version}.js",
                "~/Scripts/app/common.js",
                "~/Scripts/app/app.datamodel.js",
                "~/Scripts/app/app.viewmodel.js",
                "~/Scripts/app/home.viewmodel.js",
                "~/Scripts/app/_run.js"));

            bundles.Add(new ScriptBundle("~/bundles/metronic").Include(
                "~/Assets/vendors/base/vendors.bundle.js",
                "~/Assets/demo/default/base/scripts.bundle.js",
                "~/Assets/vendors/custom/fullcalendar/fullcalendar.bundle.js",
                "~/Assets/app/js/dashboard.js"));

            bundles.Add(new ScriptBundle("~/bundles/site").Include(
                 "~/Scripts/site.js",
                 "~/Scripts/jquery.mask.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/views/despesas").Include(
               "~/Scripts/views/despesas.js"));

            bundles.Add(new ScriptBundle("~/views/relatorios").Include(
               "~/Scripts/views/relatorios.js"));

            bundles.Add(new ScriptBundle("~/views/tipoDespesa").Include(
               "~/Scripts/views/tipoDespesa.js"));

            bundles.Add(new ScriptBundle("~/views/tipoRelatorio").Include(
               "~/Scripts/views/tipoRelatorio.js"));

            bundles.Add(new ScriptBundle("~/views/tipoPagamento").Include(
               "~/Scripts/views/tipoPagamento.js"));

            bundles.Add(new ScriptBundle("~/views/login").Include(
               "~/Scripts/views/login.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                 "~/Content/Site.css",
                 "~/Content/Login.css",
                 "~/Content/Modal.css"));

            bundles.Add(new StyleBundle("~/Metronic/css").Include(
                "~/Assets/vendors/custom/fullcalendar/fullcalendar.bundle.css",
                "~/Assets/vendors/base/vendors.bundle.css",
                "~/Assets/demo/default/base/style.bundle.css"));
        }
    }
}

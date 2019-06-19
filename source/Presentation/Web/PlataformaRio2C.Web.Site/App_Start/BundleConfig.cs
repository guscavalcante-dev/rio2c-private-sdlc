using System.Web.Optimization;

namespace PlataformaRio2C.Web.Site
{
    public static class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            #region scripts

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-1.x.min.js"));


            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                       "~/Scripts/jquery.validate.min.js",
                       "~/Scripts/jquery.validate-vsdoc.min.js",
                       "~/Scripts/jquery.validate.unobtrusive.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/scripts/modernizr").Include(
                        "~/Scripts/modernizr-2.8.3.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/popper.min.js",
                        "~/Scripts/bootstrap.min.js",
                        "~/Scripts/respond.min.js"));

            bundles.Add(new ScriptBundle("~/Content/assets/js").Include(
                  "~/Content/assets/bootstrap-colorpicker/js/bootstrap-colorpicker.js",
                  "~/Content/assets/bootstrap-datepicker/js/bootstrap-datepicker.min.js",
                   "~/Content/assets/bootstrap-fileupload/bootstrap-fileupload.min.js"));


            bundles.Add(new ScriptBundle("~/Content/js/bundle").Include(
                "~/Content/js/jquery.dcjqaccordion.2.7.min.js",
                "~/Content/js/jquery.nicescroll.min.js",
                "~/Content/js/jquery.scrollTo.min.js",
                "~/Content/js/jquery.customSelect.min.js",
                "~/Content/js/jquery.tagsinput.min.js",               
                "~/Content/js/bootstrap-switch.js",
                "~/Content/js/ga.js",
                "~/Content/js/common-scripts.js",
                "~/Content/js/form-component.js",
                "~/Content/js/advanced-form-components.js",
                "~/Content/js/toucheffects.min.js",
                "~/Content/js/fabric.js",
                "~/Content/js/darkroom.js"));


            #region bundlesAngular

            var bundlesAngular = new ScriptBundle("~/Scripts/angular/bundles");
            bundlesAngular.Include("~/Scripts/angular/angular.min.js");
            bundlesAngular.Include("~/Scripts/angular/angular-aria.min.js");
            bundlesAngular.Include("~/Scripts/angular/angular-animate.min.js");
            bundlesAngular.Include("~/Scripts/angular/angular-messages.min.js");
            bundlesAngular.Include("~/Scripts/angular/angular-sanitize.min.js");
            bundlesAngular.Include("~/Scripts/angular/angular-route.min.js");
            bundlesAngular.Include("~/Scripts/angular/angular-cookies.min.js");
            bundlesAngular.Include("~/Scripts/angular-ui/ui-bootstrap.min.js");            
            bundlesAngular.Include("~/Scripts/angular-ui/ui-bootstrap-tpls.min.js");
            bundlesAngular.Include("~/Scripts/ngToast/ngToast.min.js");
            bundlesAngular.Include("~/Scripts/MarlinToolKit/MarlinAlert/MarlinAlert.module.js");
            bundlesAngular.Include("~/Scripts/MarlinToolKit/MarlinAlert/MarlinAlert.config.js");
            bundlesAngular.Include("~/Scripts/MarlinToolKit/MarlinAlert/*.js");
            bundlesAngular.Include("~/Scripts/MarlinToolKit/MarlinToolKit.module.js");
            bundlesAngular.Include("~/Scripts/angular-vs-repeat-master/angular-vs-repeat.min.js");
            bundlesAngular.Include("~/Scripts/Moment/moment.min.js");
            bundlesAngular.Include("~/Scripts/Moment/moment-with-locales.min.js");
            

            bundles.Add(bundlesAngular);

            #endregion

            #region bundlesAngularExtensions

            var bundlesAngularExtensions = new ScriptBundle("~/Content/js/Rio2C/bundles");
            bundlesAngularExtensions.Include("~/Content/js/Rio2C/Rio2C.module.js");
            bundlesAngularExtensions.Include("~/Content/js/Rio2C/Rio2C.config.js");
            bundlesAngularExtensions.Include("~/Content/js/Rio2C/Rio2C.loadimage.directive.js");
            bundlesAngularExtensions.Include("~/Content/js/Rio2C/Rio2C.pluralize.filter.js");
            bundlesAngularExtensions.Include("~/Content/js/Rio2C/Project/Project.module.js");
            bundlesAngularExtensions.Include("~/Content/js/Rio2C/Project/*.js");
            bundlesAngularExtensions.Include("~/Content/js/Rio2C/Player/Player.module.js");
            bundlesAngularExtensions.Include("~/Content/js/Rio2C/Player/*.js");
            bundlesAngularExtensions.Include("~/Content/js/Rio2C/Conference/Conference.module.js");
            bundlesAngularExtensions.Include("~/Content/js/Rio2C/Conference/*.js");
            bundlesAngularExtensions.Include("~/Content/js/Rio2C/Message/Message.module.js");
            bundlesAngularExtensions.Include("~/Content/js/Rio2C/Message/*.js");
            bundlesAngularExtensions.Include("~/Content/js/Rio2C/Schedule/Schedule.module.js");
            bundlesAngularExtensions.Include("~/Content/js/Rio2C/Schedule/*.js");
            bundles.Add(bundlesAngularExtensions);

            #endregion

            #endregion

            #region styles


            bundles.Add(new StyleBundle("~/Content/assets/css").Include(
                  "~/Content/assets/bootstrap-colorpicker/css/colorpicker.css",
                  "~/Content/assets/bootstrap-datepicker/css/datepicker.css",
                  "~/Content/assets/bootstrap-fileupload/bootstrap-fileupload.min.css",
                  "~/Content/darkroom.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/font-awesome.css",
                      "~/Content/style.css"));

            #endregion

            BundleTable.EnableOptimizations = true;
        }
    }
}


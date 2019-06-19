using System.Web;
using System.Web.Optimization;

namespace PlataformaRio2C.Web.Admin
{
    public static class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
           

            #region Scripts

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                     "~/Scripts/modernizr-2.6.2.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-1.x.min.js"));


            var bundlesJqueryval = new ScriptBundle("~/bundles/jqueryval");
            bundlesJqueryval.Include("~/Scripts/jquery.validate.min.js");
            bundlesJqueryval.Include("~/Scripts/jquery.validate-vsdoc.min.js");
            bundlesJqueryval.Include("~/Scripts/jquery.validate.unobtrusive.min.js");
            //bundlesJqueryval.Include("~/Scripts/globalize.js");            
            //bundlesJqueryval.Include("~/Scripts/jquery.validate.globalize.min.js");       
            bundles.Add(bundlesJqueryval);

          

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/popper.min.js",
                        "~/Scripts/bootstrap.min.js",
                        "~/Scripts/respond.min.js"));

            bundles.Add(new ScriptBundle("~/Content/assets/js").Include(
                      "~/Content/assets/bootstrap-colorpicker/js/bootstrap-colorpicker.js",
                      "~/Content/assets/bootstrap-datepicker/js/bootstrap-datepicker.min.js",
                       "~/Content/assets/bootstrap-fileupload/bootstrap-fileupload.min.js"));


            bundles.Add(new ScriptBundle("~/Content/js/bundle").Include(
                "~/Content/js/jquery.cookie.js",
                "~/Content/js/jquery.dcjqaccordion.2.7.min.js",
                "~/Content/js/jquery.scrollTo.min.js",
                "~/Content/js/jquery.nicescroll.min.js",
                "~/Content/js/bootstrap-switch.js",
                "~/Content/js/jquery.tagsinput.min.js",
                "~/Content/js/ga.js",
                "~/Content/js/fabric.js",
                "~/Content/js/darkroom.js",
                "~/Content/js/form-component.js",
                "~/Content/js/advanced-form-components.js",
                "~/Content/js/common-scripts.js"));


            #region bundlesAngular

            var bundlesAngular = new ScriptBundle("~/Scripts/angular/bundles");
            bundlesAngular.Include("~/Scripts/angular/angular.min.js");
            bundlesAngular.Include("~/Scripts/angular/angular-aria.min.js");
            bundlesAngular.Include("~/Scripts/angular/angular-animate.min.js");
            bundlesAngular.Include("~/Scripts/angular/angular-messages.min.js");
            bundlesAngular.Include("~/Scripts/angular/angular-sanitize.min.js");
            bundlesAngular.Include("~/Scripts/angular/angular-route.min.js");
            bundlesAngular.Include("~/Scripts/angular/angular-resource.min.js");
            bundlesAngular.Include("~/Scripts/angular/angular-cookies.min.js");
            bundlesAngular.Include("~/Scripts/angular/i18n/angular-locale_pt-br.js");            
            bundlesAngular.Include("~/Scripts/angular-ui/ui-bootstrap.min.js");
            bundlesAngular.Include("~/Scripts/angular-ui/ui-bootstrap-tpls.min.js");
            bundlesAngular.Include("~/Scripts/ngToast/ngToast.min.js");
            bundlesAngular.Include("~/Scripts/MarlinToolKit/MarlinAlert/MarlinAlert.module.js");
            bundlesAngular.Include("~/Scripts/MarlinToolKit/MarlinAlert/MarlinAlert.config.js");
            bundlesAngular.Include("~/Scripts/MarlinToolKit/MarlinAlert/*.js");
            bundlesAngular.Include("~/Scripts/MarlinToolKit/MarlinToolKit.module.js");
            bundlesAngular.Include("~/Scripts/angular-chart/Chart.min.js");
            bundlesAngular.Include("~/Scripts/angular-chart/angular-chart.min.js");
            bundlesAngular.Include("~/Scripts/angular-count-to/angular-count-to.min.js");

            bundlesAngular.Include("~/Scripts/Moment/moment.min.js");
            bundlesAngular.Include("~/Scripts/Moment/moment-with-locales.min.js");

            bundles.Add(bundlesAngular);

            #endregion

            #region bundlesAngularExtensions

            var bundlesAngularExtensions = new ScriptBundle("~/Content/js/Rio2CAdmin/bundles");
            bundlesAngularExtensions.Include("~/Content/js/MarlinValidate/MarlinValidate.module.js");
            bundlesAngularExtensions.Include("~/Content/js/MarlinValidate/*.js");
            bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/Rio2CAdmin.module.js");
            bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/Rio2CAdmin.loadImage.directive.js");
            bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/Collaborator/Collaborator.module.js");
            bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/Collaborator/*.js");
            bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/Logistics/Logistics.module.js");
            bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/Logistics/*.js");
            bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/Conference/Conference.module.js");
            bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/Conference/*.js");
            bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/Producer/Producer.module.js");
            bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/Producer/*.js");
            bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/Project/Project.module.js");
            bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/Project/*.js");
            bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/Room/Room.module.js");
            bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/Room/*.js");
            bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/RoleLecturer/RoleLecturer.module.js");
            bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/RoleLecturer/*.js");
            bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/Dashboard/Dashboard.module.js");
            bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/Dashboard/*.js");
            bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/OneToOneMeetings/OneToOneMeetings.module.js");
            bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/OneToOneMeetings/OneToOneMeetings.service.js");
            bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/OneToOneMeetings/*.js");
            bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/Holding/Holding.module.js");
            bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/Holding/*.js");
            bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/CollaboratorProducer/CollaboratorProducer.module.js");
            bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/CollaboratorProducer/*.js");
            bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/Schedule/Schedule.module.js");
            bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/Schedule/*.js");
            bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/FinancialReport/FinancialReport.module.js");
            bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/FinancialReport/*.js");


            bundles.Add(bundlesAngularExtensions);

            #endregion

            #endregion


            #region Styles

            bundles.Add(new StyleBundle("~/Content/assets/css").Include(
               "~/Content/assets/bootstrap-colorpicker/css/colorpicker.css",
               "~/Content/assets/bootstrap-datepicker/css/datepicker.css",
               "~/Content/assets/bootstrap-fileupload/bootstrap-fileupload.min.css"));

            bundles.Add(new StyleBundle("~/Content/css/bundle").Include(
                      "~/Content/css/bootstrap.min.css",
                      "~/Content/css/bootstrap-reset.min.css",
                      "~/Content/css/font-awesome.css",
                      "~/Content/css/darkroom.css",
                      "~/Content/css/style.css",
                      "~/Content/css/style-responsive.css"));

            #endregion


            BundleTable.EnableOptimizations = false;
        }
    }
}

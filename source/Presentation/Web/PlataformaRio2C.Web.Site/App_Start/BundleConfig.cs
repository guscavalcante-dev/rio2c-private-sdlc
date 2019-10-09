// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-09-2019
// ***********************************************************************
// <copyright file="BundleConfig.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Web.Optimization;

namespace PlataformaRio2C.Web.Site
{
    /// <summary>BundleConfig</summary>
    public static class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region Metronic Bundles

            #region Scripts

            #region Global Mandatory Vendors

            bundles.Add(new ScriptBundle("~/bundles/js/layoutGlobalMandatoryVendors.js").Include(
                "~/Assets/themes/metronic/vendors/general/jquery/dist/jquery.js",
                "~/Assets/themes/metronic/vendors/general/popper.js/dist/umd/popper.js",
                "~/Assets/themes/metronic/vendors/general/bootstrap/dist/js/bootstrap.min.js",
                "~/Assets/themes/metronic/vendors/general/js-cookie/src/js.cookie.js",
                "~/Assets/themes/metronic/vendors/general/moment/min/moment-with-locales.min.js",
                "~/Assets/themes/metronic/vendors/general/tooltip.js/dist/umd/tooltip.min.js",
                "~/Assets/themes/metronic/vendors/general/perfect-scrollbar/dist/perfect-scrollbar.js",
                "~/Assets/themes/metronic/vendors/general/toastr/build/toastr.min.js",
                "~/Assets/themes/metronic/vendors/general/sticky-js/dist/sticky.min.js",
                "~/Assets/themes/metronic/vendors/general/wnumb/wNumb.js"));

            #endregion

            #region Global Optional Vendors

            bundles.Add(new ScriptBundle("~/bundles/js/layoutGlobalOptionalVendors.js").Include(
                "~/Assets/themes/metronic/vendors/general/block-ui/jquery.blockUI.js",
                "~/Assets/themes/metronic/vendors/general/owl.carousel/dist/owl.carousel.js"));

            #endregion

            #region Global Theme Bundle

            bundles.Add(new ScriptBundle("~/bundles/js/layoutGlobalThemeBundle.js").Include(
                "~/Assets/themes/metronic/js/demo4/scripts.bundle.js"));

            #endregion

            #region Login

            bundles.Add(new ScriptBundle("~/bundles/js/loginCustomScripts.js").Include(
                "~/Assets/themes/metronic/js/demo4/pages/login/login-1.js"));

            #endregion

            #endregion

            #region Styles

            #region Global Mandatory Vendors

            bundles.Add(new StyleBundle("~/bundles/css/layoutGlobalMandatoryVendors.css").Include(
                "~/Assets/themes/metronic/vendors/general/perfect-scrollbar/css/perfect-scrollbar.css",
                "~/Assets/themes/metronic/vendors/general/toastr/build/toastr.css"));

            #endregion

            #region Global Optional Vendors

            bundles.Add(new StyleBundle("~/bundles/css/layoutGlobalOptionalVendors.css").Include(
                "~/Assets/themes/metronic/vendors/general/owl.carousel/dist/assets/owl.carousel.css",
                "~/Assets/themes/metronic/vendors/general/owl.carousel/dist/assets/owl.theme.default.css"));

            bundles.Add(new StyleBundle("~/bundles/css/layoutFontOptionalVendors.css")
                .Include("~/Assets/themes/metronic/vendors/custom/vendors/flaticon/flaticon.css", new CssRewriteUrlTransform())
                .Include("~/Assets/themes/metronic/vendors/custom/vendors/flaticon2/flaticon.css", new CssRewriteUrlTransform())
                .Include("~/Assets/themes/metronic/vendors/general/socicon/css/socicon.css", new CssRewriteUrlTransform())
                .Include("~/Assets/themes/metronic/vendors/custom/vendors/line-awesome/css/line-awesome.css", new CssRewriteUrlTransform())
                .Include("~/Assets/themes/metronic/vendors/general/fortawesome/fontawesome-free/css/all.min.css", new CssRewriteUrlTransform()));

            #endregion

            #region Global Theme Styles

            bundles.Add(new StyleBundle("~/bundles/css/layoutGlobalThemeStyles.css").Include(
                "~/Assets/themes/metronic/css/demo4/style.bundle.css"));

            #endregion

            #region Login

            bundles.Add(new StyleBundle("~/bundles/css/loginCustomStyles.css").Include(
                "~/Assets/themes/metronic/css/demo4/pages/login/login-1.css"));

            #endregion

            #region Error

            bundles.Add(new StyleBundle("~/bundles/css/errorCustomStyles.css").Include(
                "~/Assets/themes/metronic/css/demo4/pages/error/error-1.css"));

            #endregion

            #endregion

            #endregion

            #region MyRio2C Bundles

            bundles.Add(new StyleBundle("~/bundles/css/layoutGlobalCustomizedStyles.css").Include(
                "~/Assets/css/myrio2c.common.css",
                "~/Assets/css/myrio2c.common.responsive.css"));

            bundles.Add(new ScriptBundle("~/bundles/js/layoutGlobalCustomized.js").Include(
                "~/Assets/js/myrio2c.common.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/onboardingIndex").Include(
                "~/Assets/js/pages/onboarding/wizard-2.js",
                "~/Assets/themes/metronic/vendors/general/jquery-validation/dist/jquery.validate.js",
                "~/Assets/themes/metronic/vendors/general/sweetalert2/dist/sweetalert2.all.js"));

            #endregion

            #region Components Bundles

            #region JQuery Validation

            bundles.Add(new ScriptBundle("~/bundles/js/jqueryval.js").Include(
                "~/Scripts/jquery.validate.min.js",
                "~/Scripts/jquery.validate-vsdoc.min.js",
                "~/Scripts/jquery.validate.unobtrusive.min.js",
                "~/Client Scripts/mvcfoolproof.unobtrusive.js"
            ));

            #endregion

            #region JQuery Form

            bundles.Add(new ScriptBundle("~/bundles/js/jqueryform.js").Include(
                "~/Assets/components/jquery.form/jquery.form.js"));

            #endregion

            #region Datatables

            bundles.Add(new StyleBundle("~/bundles/css/dataTables.css")
                .Include("~/Assets/themes/metronic/vendors/custom/datatables/datatables.bundle.css", new CssRewriteUrlTransform()));

            bundles.Add(new ScriptBundle("~/bundles/js/dataTables.js").Include(
                "~/Assets/themes/metronic/vendors/custom/datatables/datatables.bundle.js"));

            #endregion

            #region Chart.js

            bundles.Add(new ScriptBundle("~/bundles/js/chart.js").Include(
                "~/Assets/themes/metronic/vendors/general/chart.js/dist/Chart.bundle.js"));

            #endregion

            #region CKEditor

            bundles.Add(new ScriptBundle("~/bundles/js/ckEditor.js").Include(
                "~/Scripts/ckeditor/ckeditor.js",
                "~/Content/js/ckeditor_config.js"));

            #endregion

            #region Cropperjs

            bundles.Add(new StyleBundle("~/bundles/css/cropper.css")
                .Include("~/Assets/components/cropper/dist/cropper.css", new CssRewriteUrlTransform()));

            bundles.Add(new ScriptBundle("~/bundles/js/cropper.js").Include(
                "~/Assets/components/cropper/dist/cropper.js",
                "~/Assets/js/myrio2c.cropper.js"));

            #endregion

            #region Hide Show Password

            bundles.Add(new StyleBundle("~/bundles/css/hideshowpassword.css")
                .Include("~/Assets/components/hideshowpassword/css/example.wink.css", new CssRewriteUrlTransform()));

            bundles.Add(new ScriptBundle("~/bundles/js/hideshowpassword.js").Include(
                "~/Assets/components/hideshowpassword/hideShowPassword.js",
                "~/Assets/js/myrio2c.showhidepassword.js"));

            #endregion

            #region Bootbox

            bundles.Add(new ScriptBundle("~/bundles/js/bootbox.js").Include(
                "~/Scripts/bootbox.min.js"));

            #endregion

            #region Select2

            bundles.Add(new StyleBundle("~/bundles/css/select2.css")
                .Include("~/Assets/themes/metronic/vendors/general/select2/dist/css/select2.css"/*, new CssRewriteUrlTransform()*/));

            bundles.Add(new ScriptBundle("~/bundles/js/select2.js").Include(
                "~/Assets/themes/metronic/vendors/general/select2/dist/js/select2.js",
                "~/Assets/themes/metronic/vendors/general/select2/dist/js/i18n/pt-BR.js"));

            #endregion

            #region Input Mask

            bundles.Add(new ScriptBundle("~/bundles/js/inputmask.js").Include(
                "~/Assets/themes/metronic/vendors/general/inputmask/dist/jquery.inputmask.bundle.js",
                "~/Assets/js/myrio2c.inputmask.js"));

            #endregion

            #endregion

            #region Pages Bundles

            #region Onboarding Wizard

            bundles.Add(new ScriptBundle("~/bundles/js/onboarding.wizard.js").Include(
                "~/Assets/js/onboarding/onboarding.wizard.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/onboarding.collaboratordata.js").Include(
                "~/Assets/js/onboarding/onboarding.collaboratordata.js",
                "~/Assets/js/myrio2c.publicemail.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/onboarding.organizationdata.js").Include(
                "~/Assets/js/onboarding/onboarding.organizationdata.js",
                "~/Assets/js/myrio2c.companynumber.js",
                "~/Assets/js/myrio2c.activity.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/onboarding.interests.js").Include(
                "~/Assets/js/onboarding/onboarding.interests.js"));

            #endregion

            #region Companies

            bundles.Add(new ScriptBundle("~/bundles/js/companies.widget.js").Include(
                "~/Assets/js/companies/companies.maininformation.widget.js",
                "~/Assets/js/companies/companies.executive.widget.js",
                "~/Assets/js/companies/companies.address.widget.js",
                "~/Assets/js/companies/companies.activity.widget.js",
                "~/Assets/js/companies/companies.targetaudience.widget.js",
                "~/Assets/js/companies/companies.interest.widget.js"));

            #endregion

            #region Addresses

            bundles.Add(new ScriptBundle("~/bundles/js/addresses.form.js").Include(
                "~/Assets/js/addresses/addresses.form.js"));

            #endregion

            #endregion

            #region Scripts

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-1.x.min.js"));


            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                       "~/Scripts/jquery.validate.min.js",
                       //"~/Scripts/jquery.validate-vsdoc.min.js",
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

            bundles.Add(new ScriptBundle("~/bundles/js/scheduleCustomScripts").Include(
                "~/Assets/themes/metronic/vendors/custom/fullcalendar/fullcalendar.bundle.js",
                "~/Assets/js/pages/schedule/components/calendar/basic.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/projectSubmitCustomScripts").Include(
"~/Assets/themes/metronic/js/demo4/pages/wizard/wizard-3.js"));

            #endregion

            #region Styles

            bundles.Add(new StyleBundle("~/Content/assets/css").Include(
                  "~/Content/assets/bootstrap-colorpicker/css/colorpicker.css",
                  "~/Content/assets/bootstrap-datepicker/css/datepicker.css",
                  "~/Content/assets/bootstrap-fileupload/bootstrap-fileupload.min.css",
                  "~/Content/darkroom.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/font-awesome.css",
                      "~/Content/style.css"));

            bundles.Add(new StyleBundle("~/bundles/css/scheduleCustomStyles").Include(
                "~/Assets/themes/metronic/vendors/custom/fullcalendar/fullcalendar.bundle.css"));

            bundles.Add(new StyleBundle("~/bundles/css/projectSubmitCustomStyles").Include(
                "~/Assets/themes/metronic/css/demo4/pages/wizard/wizard-3.css"));

            bundles.Add(new StyleBundle("~/bundles/css/onboardingIndex").Include(
                "~/Assets/css/pages/onboarding/wizard-2.css",
                "~/Assets/css/pages/onboarding/onboarding.css"));
            #endregion

            // Required to generate bundles on release running in visual studio
            #if !DEBUG
            BundleTable.EnableOptimizations = true;
            #endif
        }
    }
}


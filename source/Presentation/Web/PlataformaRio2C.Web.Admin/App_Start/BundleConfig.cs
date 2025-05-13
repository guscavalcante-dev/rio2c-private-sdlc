// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Ribeiro
// Last Modified On : 21-02-2025
// ***********************************************************************
// <copyright file="BundleConfig.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Web.Optimization;

namespace PlataformaRio2C.Web.Admin
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
                "~/Assets/components/moment-timezone/moment-timezone-with-data.js",
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

            bundles.Add(new StyleBundle("~/bundles/css/layoutGlobalCustomizedStyles.css")
                .Include("~/Assets/css/myrio2c.common.css")
                .Include("~/Assets/css/myrio2c.common.responsive.css")
                .Include("~/Assets/components/hideshowpassword/css/example.wink.css", new CssRewriteUrlTransform()));

            bundles.Add(new ScriptBundle("~/bundles/js/layoutGlobalCustomized.js").Include(
                "~/Assets/js/myrio2c.common.js",
                "~/Assets/components/jquery.form/jquery.form.js",
                "~/Assets/components/hideshowpassword/hideShowPassword.js",
                "~/Assets/js/myrio2c.showhidepassword.js",
                "~/Assets/js/accounts/accounts.password.js",
                "~/Assets/components/bootstrap-maxlength/src/bootstrap-maxlength.js"));

            #endregion

            #region Components Bundles

            #region JQuery Validation

            bundles.Add(new ScriptBundle("~/bundles/js/jqueryval.js").Include(
                "~/Scripts/jquery.validate.min.js",
                "~/Scripts/jquery.validate-vsdoc.min.js",
                "~/Scripts/jquery.validate.unobtrusive.min.js",
                "~/Client Scripts/mvcfoolproof.unobtrusive.20240307151400.js"
                ));

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

            #region Cropperjs

            bundles.Add(new StyleBundle("~/bundles/css/cropper.css")
                .Include("~/Assets/components/cropper/dist/cropper.css", new CssRewriteUrlTransform()));

            bundles.Add(new ScriptBundle("~/bundles/js/cropper.js").Include(
                "~/Assets/components/cropper/dist/cropper.js",
                "~/Assets/js/myrio2c.cropper.js"));

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

            #region Autocomplete

            bundles.Add(new StyleBundle("~/bundles/css/jquery.autocomplete.css")
                .Include("~/Assets/components/jquery.autocomplete/content/styles.css"/*, new CssRewriteUrlTransform()*/));

            bundles.Add(new ScriptBundle("~/bundles/js/jquery.autocomplete.js").Include(
                "~/Assets/components/jquery.autocomplete/dist/jquery.autocomplete.js"));

            #endregion

            #region Dynamic List

            bundles.Add(new ScriptBundle("~/bundles/js/dynamic.list.js").Include(
                "~/Assets/js/dynamic.list.js"));

            #endregion

            #region Bootstrap Datetimepicker

            bundles.Add(new StyleBundle("~/bundles/css/bootstrap-datetimepicker.css")
                .Include("~/Assets/themes/metronic/vendors/general/bootstrap-datetime-picker/css/bootstrap-datetimepicker.css", new CssRewriteUrlTransform()));

            bundles.Add(new ScriptBundle("~/bundles/js/bootstrap-datetimepicker.js").Include(
                "~/Assets/themes/metronic/vendors/general/bootstrap-datetime-picker/js/bootstrap-datetimepicker.js",
                "~/Assets/themes/metronic/vendors/general/bootstrap-datetime-picker/js/locales/bootstrap-datetimepicker.pt-BR.js"));

            #endregion

            #region Bootstrap Datepicker

            bundles.Add(new StyleBundle("~/bundles/css/bootstrap-datepicker.css")
                .Include("~/Assets/themes/metronic/vendors/general/bootstrap-datepicker/dist/css/bootstrap-datepicker.css", new CssRewriteUrlTransform()));

            bundles.Add(new ScriptBundle("~/bundles/js/bootstrap-datepicker.js").Include(
                "~/Assets/themes/metronic/vendors/general/inputmask/dist/jquery.inputmask.bundle.js",
                //"~/Assets/themes/metronic/vendors/general/moment/min/moment-with-locales.min.js",
                "~/Assets/themes/metronic/vendors/general/bootstrap-datepicker/dist/js/bootstrap-datepicker.js",
                "~/Assets/components/bootstrap-timepicker/bootstrap-datepicker.en-us.js",
                "~/Assets/components/bootstrap-timepicker/bootstrap-datepicker.pt-br.js"));

            #endregion

            #region Bootstrap Timepicker

            bundles.Add(new StyleBundle("~/bundles/css/bootstrap-timepicker.css")
                .Include("~/Assets/themes/metronic/vendors/general/bootstrap-timepicker/css/bootstrap-timepicker.css", new CssRewriteUrlTransform()));

            bundles.Add(new ScriptBundle("~/bundles/js/bootstrap-timepicker.js").Include(
                "~/Assets/themes/metronic/vendors/general/bootstrap-timepicker/js/bootstrap-timepicker.js"));

            #endregion

            #region Jquery MiniColors

            bundles.Add(new StyleBundle("~/bundles/css/jquery-minicolors.css")
                .Include("~/Assets/components/jquery-minicolors/jquery.minicolors.css", new CssRewriteUrlTransform()));

            bundles.Add(new ScriptBundle("~/bundles/js/jquery-minicolors.js").Include(
                "~/Assets/components/jquery-minicolors/jquery.minicolors.js"));

            #endregion

            #region AmCharts

            bundles.Add(new ScriptBundle("~/bundles/js/amcharts.js").Include(
                "~/Assets/components/amcharts4/core.js",
                "~/Assets/components/amcharts4/charts.js",
                "~/Assets/components/amcharts4/themes/animated.js",
                "~/Assets/components/amcharts4/lang/pt_BR.js",
                "~/Assets/components/amcharts4/lang/en_US.js"));

            #endregion

            #region Odometer

            bundles.Add(new ScriptBundle("~/bundles/js/odometer.js").Include(
                "~/Assets/components/odometer/odometer.js"));

            bundles.Add(new StyleBundle("~/bundles/css/odometer.css")
                .Include("~/Assets/components/odometer/themes/odometer-theme-car.css"));

            #endregion

            #region Globalize

            bundles.Add(new ScriptBundle("~/bundles/js/globalize.js").Include(
                "~/Assets/components/globalize/globalize.js",
                "~/Assets/components/globalize/cultures/globalize.culture.pt-BR.js"
                , "~/Assets/components/globalize/cultures/globalize.culture.en-US.js"
                ));

            #endregion

            #endregion

            #region Common Page Bundles

            bundles.Add(new ScriptBundle("~/bundles/js/salesplatforms.export.js").Include(
                "~/Assets/js/salesplatforms/salesplatforms.export.js"));

            #endregion

            #region Accounts Page Bundles

            bundles.Add(new ScriptBundle("~/bundles/js/accounts.update.userstatus.js").Include(
               "~/Assets/js/accounts/accounts.update.userstatus.js"));

            #endregion

            #region Administration Page Bundles

            #region Administrator

            bundles.Add(new ScriptBundle("~/bundles/js/administrators.list.js").Include(
                "~/Assets/js/administrators/administrators.totalcount.widget.js",
                "~/Assets/js/administrators/administrators.datatable.widget.js",
                "~/Assets/js/salesplatforms/salesplatforms.export.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/administrators.editioncount.js").Include(
                "~/Assets/js/administrators/administrators.editioncount.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/administrators.create.js").Include(
                "~/Assets/js/administrators/administrators.create.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/administrators.update.js").Include(
                "~/Assets/js/administrators/administrators.update.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/administrators.delete.js").Include(
                "~/Assets/js/administrators/administrators.delete.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/administrators.maininformation.widget.js").Include(
                "~/Assets/js/administrators/administrators.maininformation.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/administrators.form.js").Include(
                "~/Assets/js/administrators/administrators.form.js"));

            #endregion

            #region Editions

            bundles.Add(new ScriptBundle("~/bundles/js/editions.list.js").Include(
                "~/Assets/js/editions/editions.totalcount.widget.js",
                "~/Assets/js/editions/editions.datatable.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/editions.editioncount.js").Include(
                "~/Assets/js/editions/editions.editioncount.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/editions.create.js").Include(
                "~/Assets/js/editions/editions.create.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/editions.details.js").Include(
                "~/Assets/js/editions/editions.maininformation.widget.js",
                "~/Assets/js/editions/editions.datesinformation.widget.js",
                "~/Assets/js/editions/editions.events.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/editions.delete.js").Include(
                "~/Assets/js/editions/editions.delete.js"));

            #endregion

            #endregion

            #region Audiovisual Pages Bundles

            #region Holdings

            bundles.Add(new ScriptBundle("~/bundles/js/holdings.list.js").Include(
                "~/Assets/js/holdings/holdings.totalcount.widget.js",
                "~/Assets/js/holdings/holdings.datatable.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/holdings.editioncount.js").Include(
                "~/Assets/js/holdings/holdings.editioncount.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/holdings.create.js").Include(
                "~/Assets/js/holdings/holdings.create.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/holdings.update.js").Include(
                "~/Assets/js/holdings/holdings.update.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/holdings.delete.js").Include(
                "~/Assets/js/holdings/holdings.delete.js"));

            #endregion

            #region Organizations

            bundles.Add(new StyleBundle("~/bundles/css/organizations.apiconfiguration.css").Include(
                "~/Assets/themes/metronic/css/demo4/pages/pricing/pricing-1.css"));

            bundles.Add(new ScriptBundle("~/bundles/js/organizations.widget.js").Include(
                 "~/Assets/js/organizations/organizations.maininformation.widget.js",
                 "~/Assets/js/organizations/organizations.socialnetworks.widget.js",
                 "~/Assets/js/organizations/organizations.address.widget.js",
                 "~/Assets/js/organizations/organizations.activity.widget.js",
                 "~/Assets/js/organizations/organizations.targetaudience.widget.js",
                 "~/Assets/js/organizations/organizations.interest.widget.js",
                 "~/Assets/js/organizations/organizations.executives.widget.js",
                 "~/Assets/js/organizations/organizations.apiconfiguration.widget.js",
                 "~/Assets/js/myrio2c.companynumber.js",
                 "~/Assets/js/myrio2c.additionalinfo.js"));

            #endregion

            #region Collaborators 

            bundles.Add(new ScriptBundle("~/bundles/js/collaborators.details.js").Include(
                "~/Assets/js/collaborators/collaborators.maininformation.widget.js",
                "~/Assets/js/collaborators/collaborators.socialnetworks.widget.js",
                "~/Assets/js/collaborators/collaborators.onboardinginfo.widget.js",
                "~/Assets/js/collaborators/collaborators.company.widget.js",
                "~/Assets/js/companies/companyinfo.autocomplete.js",
                "~/Assets/js/myrio2c.companynumber.js"
                ));

            #endregion

            #region Players

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.players.list.js").Include(
                "~/Assets/js/audiovisual/players/audiovisual.players.totalcount.widget.js",
                "~/Assets/js/audiovisual/players/audiovisual.players.datatable.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.players.editioncount.js").Include(
                "~/Assets/js/audiovisual/players/audiovisual.players.editioncount.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.players.editioncountodometer.widget.js").Include(
                "~/Assets/js/audiovisual/players/audiovisual.players.editioncountodometer.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.players.create.js").Include(
                "~/Assets/js/audiovisual/players/audiovisual.players.create.js",
                "~/Assets/js/myrio2c.companynumber.js",
                "~/Assets/js/myrio2c.additionalinfo.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.players.update.js").Include(
                "~/Assets/js/audiovisual/players/audiovisual.players.update.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.players.delete.js").Include(
                "~/Assets/js/audiovisual/players/audiovisual.players.delete.js"));

            #endregion

            #region Players - Executives

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.players.executives.list.js").Include(
                 "~/Assets/js/audiovisual/playersexecutives/audiovisual.players.executives.totalcount.widget.js",
                 "~/Assets/js/audiovisual/playersexecutives/audiovisual.players.executives.datatable.widget.js",
                 "~/Assets/js/salesplatforms/salesplatforms.export.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.players.executives.editioncount.js").Include(
                "~/Assets/js/audiovisual/playersexecutives/audiovisual.players.executives.editioncount.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.players.executives.create.js").Include(
                "~/Assets/js/audiovisual/playersexecutives/audiovisual.players.executives.create.js",
                "~/Assets/js/myrio2c.publicemail.js",
                "~/Assets/js/attendeeorganizations/attendeeorganizations.form.js",
                "~/Assets/js/dynamic.list.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.players.executives.update.js").Include(
                "~/Assets/js/audiovisual/playersexecutives/audiovisual.players.executives.update.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.players.executives.delete.js").Include(
                "~/Assets/js/audiovisual/playersexecutives/audiovisual.players.executives.delete.js"));

            #endregion

            #region Producers

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.producers.list.js").Include(
                "~/Assets/js/audiovisual/producers/audiovisual.producers.totalcount.widget.js",
                "~/Assets/js/audiovisual/producers/audiovisual.producers.datatable.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.producers.editioncount.js").Include(
                "~/Assets/js/audiovisual/producers/audiovisual.producers.editioncount.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.producers.delete.js").Include(
                "~/Assets/js/audiovisual/producers/audiovisual.producers.delete.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.producers.editioncountodometer.widget.js").Include(
                "~/Assets/js/audiovisual/producers/audiovisual.producers.editioncountodometer.widget.js"));

            #endregion

            #region Producers - Executives

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.producers.executives.list.js").Include(
                 "~/Assets/js/audiovisual/producersexecutives/audiovisual.producers.executives.totalcount.widget.js",
                 "~/Assets/js/audiovisual/producersexecutives/audiovisual.producers.executives.datatable.widget.js",
                 "~/Assets/js/salesplatforms/salesplatforms.export.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.producers.executives.editioncount.js").Include(
                "~/Assets/js/audiovisual/producersexecutives/audiovisual.producers.executives.editioncount.widget.js"));

            #endregion

            #region Projects - Negotiations

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.projects.list.js").Include(
                "~/Assets/js/audiovisual/projects/audiovisual.projects.datatable.widget.js",
                "~/Assets/js/audiovisual/projects/audiovisual.projects.totalcount.widget.js",
                "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.projects.editioncount.widget.js").Include(
                "~/Assets/js/audiovisual/projects/audiovisual.projects.editioncount.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.projects.editioncount.gauge.widget.js").Include(
                "~/Assets/js/audiovisual/projects/audiovisual.projects.editioncount.gauge.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.projects.widget.js").Include(
                "~/Assets/js/audiovisual/projects/audiovisual.projects.maininformation.widget.js",
                "~/Assets/js/audiovisual/projects/audiovisual.projects.interest.widget.js",
                "~/Assets/js/audiovisual/projects/audiovisual.projects.links.widget.js",
                "~/Assets/js/audiovisual/projects/audiovisual.projects.buyercompany.widget.js",
                "~/Assets/js/audiovisual/projects/audiovisual.projects.evaluators.widget.js",
                "~/Assets/js/audiovisual/projects/audiovisual.projects.commission.evaluation.widget.js",
                "~/Assets/js/myrio2c.additionalinfo.js",
                "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.projects.delete.js").Include(
                "~/Assets/js/audiovisual/projects/audiovisual.projects.delete.js"));

            #endregion

            #region Speakers

            bundles.Add(new ScriptBundle("~/bundles/js/speakers.list.js").Include(
                "~/Assets/js/speakers/speakers.totalcount.widget.js",
                "~/Assets/js/speakers/speakers.datatable.widget.js",
                "~/Assets/js/salesplatforms/salesplatforms.export.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/speakers.editioncount.js").Include(
                "~/Assets/js/speakers/speakers.editioncount.widget.js"));

            bundles.Add(new StyleBundle("~/bundles/css/speakers.details.css").Include(
                "~/Assets/themes/metronic/css/demo4/pages/pricing/pricing-1.css"));

            bundles.Add(new ScriptBundle("~/bundles/js/speakers.details.js").Include(
                "~/Assets/js/collaborators/collaborators.maininformation.widget.js",
                "~/Assets/js/collaborators/collaborators.socialnetworks.widget.js",
                "~/Assets/js/collaborators/collaborators.onboardinginfo.widget.js",
                "~/Assets/js/collaborators/collaborators.company.widget.js",
                "~/Assets/js/collaborators/collaborators.images.widget.js",
                "~/Assets/js/speakers/speakers.conferences.widget.js",
                "~/Assets/js/speakers/speakers.participants.widget.js",
                "~/Assets/js/companies/companyinfo.autocomplete.js",
                "~/Assets/js/myrio2c.companynumber.js",
                "~/Assets/js/speakers/speakers.apiconfiguration.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/speakers.create.js").Include(
                "~/Assets/js/speakers/speakers.create.js",
                "~/Assets/js/myrio2c.publicemail.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/speakers.update.js").Include(
                "~/Assets/js/speakers/speakers.update.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/speakers.delete.js").Include(
                "~/Assets/js/speakers/speakers.delete.js"));

            #endregion

            #region Attendee Organizations

            bundles.Add(new ScriptBundle("~/bundles/js/attendeeorganizations.form.js").Include(
                "~/Assets/js/attendeeorganizations/attendeeorganizations.form.js"));

            #endregion

            #region Addresses

            bundles.Add(new ScriptBundle("~/bundles/js/addresses.form.js").Include(
                "~/Assets/js/addresses/addresses.form.js"));

            #endregion

            #region Conferences

            bundles.Add(new ScriptBundle("~/bundles/js/conferences.list.js").Include(
                "~/Assets/js/conferences/conferences.totalcount.widget.js",
                "~/Assets/js/conferences/conferences.datatable.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/conferences.editioncount.js").Include(
                "~/Assets/js/conferences/conferences.editioncount.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/conferences.create.js").Include(
                "~/Assets/js/conferences/conferences.create.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/conferences.editionevents.js").Include(
                "~/Assets/js/conferences/conferences.editionevents.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/conferences.details.js").Include(
                "~/Assets/js/conferences/conferences.maininformation.widget.js",
                "~/Assets/js/conferences/conferences.tracksandformats.widget.js",
                "~/Assets/js/conferences/conferences.participants.widget.js",
                "~/Assets/js/conferences/conferences.apiconfiguration.widget.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/js/conferences.delete.js").Include(
                "~/Assets/js/conferences/conferences.delete.js")
            );

            bundles.Add(new StyleBundle("~/bundles/css/conferences.details.css").Include(
                "~/Assets/themes/metronic/css/demo4/pages/pricing/pricing-1.css")
            );

            #endregion

            #region Events

            bundles.Add(new ScriptBundle("~/bundles/js/events.list.js").Include(
                "~/Assets/js/events/events.totalcount.widget.js",
                "~/Assets/js/events/events.datatable.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/events.editioncount.js").Include(
                "~/Assets/js/events/events.editioncount.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/events.create.js").Include(
                "~/Assets/js/events/events.create.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/events.details.js").Include(
                "~/Assets/js/events/events.maininformation.widget.js",
                "~/Assets/js/events/events.conferences.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/events.delete.js").Include(
                "~/Assets/js/events/events.delete.js"));

            #endregion

            #region Rooms

            bundles.Add(new ScriptBundle("~/bundles/js/rooms.list.js").Include(
                "~/Assets/js/rooms/rooms.totalcount.widget.js",
                "~/Assets/js/rooms/rooms.datatable.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/rooms.editioncount.js").Include(
                "~/Assets/js/rooms/rooms.editioncount.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/rooms.create.js").Include(
                "~/Assets/js/rooms/rooms.create.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/rooms.details.js").Include(
                "~/Assets/js/rooms/rooms.maininformation.widget.js",
                "~/Assets/js/rooms/rooms.conferences.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/rooms.delete.js").Include(
                "~/Assets/js/rooms/rooms.delete.js"));

            #endregion

            #region Conference Participant Roles

            bundles.Add(new ScriptBundle("~/bundles/js/conferenceparticipantroles.list.js").Include(
                "~/Assets/js/conferenceparticipantroles/conferenceparticipantroles.totalcount.widget.js",
                "~/Assets/js/conferenceparticipantroles/conferenceparticipantroles.datatable.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/conferenceparticipantroles.editioncount.js").Include(
                "~/Assets/js/conferenceparticipantroles/conferenceparticipantroles.editioncount.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/conferenceparticipantroles.create.js").Include(
                "~/Assets/js/conferenceparticipantroles/conferenceparticipantroles.create.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/conferenceparticipantroles.details.js").Include(
                "~/Assets/js/conferenceparticipantroles/conferenceparticipantroles.maininformation.widget.js",
                "~/Assets/js/conferenceparticipantroles/conferenceparticipantroles.conferences.widget.js",
                "~/Assets/js/conferenceparticipantroles/conferenceparticipantroles.participants.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/conferenceparticipantroles.delete.js").Include(
                "~/Assets/js/conferenceparticipantroles/conferenceparticipantroles.delete.js"));

            #endregion

            #region Tracks

            bundles.Add(new ScriptBundle("~/bundles/js/tracks.list.js").Include(
                "~/Assets/js/tracks/tracks.totalcount.widget.js",
                "~/Assets/js/tracks/tracks.datatable.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/tracks.editioncount.js").Include(
                "~/Assets/js/tracks/tracks.editioncount.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/tracks.create.js").Include(
                "~/Assets/js/tracks/tracks.create.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/tracks.details.js").Include(
                "~/Assets/js/tracks/tracks.maininformation.widget.js",
                "~/Assets/js/tracks/tracks.conferences.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/tracks.delete.js").Include(
                "~/Assets/js/tracks/tracks.delete.js"));

            #endregion

            #region Pillars

            bundles.Add(new ScriptBundle("~/bundles/js/pillars.list.js").Include(
                "~/Assets/js/pillars/pillars.totalcount.widget.js",
                "~/Assets/js/pillars/pillars.datatable.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/pillars.editioncount.js").Include(
                "~/Assets/js/pillars/pillars.editioncount.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/pillars.create.js").Include(
                "~/Assets/js/pillars/pillars.create.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/pillars.details.js").Include(
                "~/Assets/js/pillars/pillars.maininformation.widget.js",
                "~/Assets/js/pillars/pillars.conferences.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/pillars.delete.js").Include(
                "~/Assets/js/pillars/pillars.delete.js"));

            #endregion

            #region Presentation Formats

            bundles.Add(new ScriptBundle("~/bundles/js/presentationformats.list.js").Include(
                "~/Assets/js/presentationformats/presentationformats.totalcount.widget.js",
                "~/Assets/js/presentationformats/presentationformats.datatable.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/presentationformats.editioncount.js").Include(
                "~/Assets/js/presentationformats/presentationformats.editioncount.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/presentationformats.create.js").Include(
                "~/Assets/js/presentationformats/presentationformats.create.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/presentationformats.details.js").Include(
                "~/Assets/js/presentationformats/presentationformats.maininformation.widget.js",
                "~/Assets/js/presentationformats/presentationformats.conferences.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/presentationformats.delete.js").Include(
                "~/Assets/js/presentationformats/presentationformats.delete.js"));

            #endregion

            #region Reports

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.reports.projectssubmissions.widget.js").Include(
                "~/Assets/js/audiovisual/reports/audiovisual.reports.projectssubmissions.widget.js",
                "~/Scripts/jquery.unobtrusive-ajax.js"));

            #endregion

            #region Meeting Parameters - AudioVisual

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.meetingparameters.list.widget.js").Include(
                "~/Assets/js/audiovisual/meetingparameters/audiovisual.meetingparameters.datatable.widget.js",
                "~/Assets/js/audiovisual/meetingparameters/audiovisual.meetingparameters.totalcount.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.meetingparameters.editioncount.widget.js").Include(
                "~/Assets/js/audiovisual/meetingparameters/audiovisual.meetingparameters.editioncount.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.meetingparameters.create.js").Include(
                "~/Assets/js/audiovisual/meetingparameters/audiovisual.meetingparameters.create.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.meetingparameters.delete.js").Include(
                "~/Assets/js/audiovisual/meetingparameters/audiovisual.meetingparameters.delete.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.meetingparameters.details.js").Include(
                "~/Assets/js/audiovisual/meetingparameters/audiovisual.meetingparameters.maininformation.widget.js",
                "~/Assets/js/audiovisual/meetingparameters/audiovisual.meetingparameters.rooms.widget.js"));

            #endregion

            #region Meetings Scheduled

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.meetings.editionscheduledcount.widget.js").Include(
                "~/Assets/js/audiovisual/meetings/audiovisual.meetings.editionscheduledcount.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.meetings.editionunscheduledcount.widget.js").Include(
                "~/Assets/js/audiovisual/meetings/audiovisual.meetings.editionunscheduledcount.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.meetings.editionscheduledcount.gauge.widget.js").Include(
                "~/Assets/js/audiovisual/meetings/audiovisual.meetings.editionscheduledcount.gauge.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.meetings.generate.widget.js").Include(
                "~/Assets/js/audiovisual/meetings/audiovisual.meetings.status.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.meetings.scheduled.widget.js").Include(
                "~/Assets/js/audiovisual/meetings/audiovisual.meetings.scheduled.widget.js",
                "~/Assets/js/audiovisual/meetings/audiovisual.meetings.scheduled.delete.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.meetings.unscheduled.widget.js").Include(
                "~/Assets/js/audiovisual/meetings/audiovisual.meetings.unscheduled.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.meetings.scheduled.create.js").Include(
                "~/Assets/js/audiovisual/meetings/audiovisual.meetings.scheduled.create.js",
                "~/Assets/js/audiovisual/meetings/audiovisual.meetings.logistics.info.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.meetings.scheduled.update.js").Include(
                "~/Assets/js/audiovisual/meetings/audiovisual.meetings.scheduled.update.js",
                "~/Assets/js/audiovisual/meetings/audiovisual.meetings.logistics.info.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.meetings.report.widget.js").Include(
                "~/Assets/js/audiovisual/meetings/audiovisual.meetings.report.widget.js"));

            #endregion

            #region Meetings Unscheduled

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.meetings.unscheduled.manualschedule.js").Include(
                "~/Assets/js/audiovisual/meetings/audiovisual.meetings.unscheduled.manualschedule.js",
                "~/Assets/js/audiovisual/meetings/audiovisual.meetings.logistics.info.widget.js"));

            #endregion

            #region Meetings - Send Email

            #region Players

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.meetings.sendemailtoplayers.list.js").Include(
                "~/Assets/js/audiovisual/meetings/audiovisual.meetings.sendemailtoplayers.totalcount.widget.js",
                "~/Assets/js/audiovisual/meetings/audiovisual.meetings.sendemailtoplayers.datatable.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.meetings.sendemailtoplayers.editioncount.js").Include(
                "~/Assets/js/audiovisual/meetings/audiovisual.meetings.sendemailtoplayers.editioncount.widget.js"));

            #endregion

            #region Producers

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.meetings.sendemailtoproducers.list.js").Include(
                "~/Assets/js/audiovisual/meetings/audiovisual.meetings.sendemailtoproducers.totalcount.widget.js",
                "~/Assets/js/audiovisual/meetings/audiovisual.meetings.sendemailtoproducers.datatable.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.meetings.sendemailtoproducers.editioncount.js").Include(
                "~/Assets/js/audiovisual/meetings/audiovisual.meetings.sendemailtoproducers.editioncount.widget.js"));

            #endregion

            #endregion

            #region Commissions

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.commissions.list.js").Include(
                "~/Assets/js/audiovisual/commissions/audiovisual.commissions.totalcount.widget.js",
                "~/Assets/js/audiovisual/commissions/audiovisual.commissions.datatable.widget.js",
                "~/Assets/js/salesplatforms/salesplatforms.export.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.commissions.editioncount.js").Include(
                "~/Assets/js/audiovisual/commissions/audiovisual.commissions.editioncount.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.commissions.create.js").Include(
                "~/Assets/js/audiovisual/commissions/audiovisual.commissions.create.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.commissions.details.js").Include(
                "~/Assets/js/collaborators/collaborators.maininformation.widget.js",
                "~/Assets/js/collaborators/collaborators.socialnetworks.widget.js",
                "~/Assets/js/collaborators/collaborators.onboardinginfo.widget.js",
                "~/Assets/js/audiovisual/commissions/audiovisual.commissions.interests.widget.js",
                "~/Assets/js/audiovisual/commissions/audiovisual.commissions.evaluations.widget.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.commissions.update.js").Include(
                "~/Assets/js/audiovisual/commissions/audiovisual.commissions.update.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.commissions.delete.js").Include(
                "~/Assets/js/audiovisual/commissions/audiovisual.commissions.delete.js"));

            #endregion

            #endregion

            #region Music Page Bundles

            #region Meeting Parameters - Music

            bundles.Add(new ScriptBundle("~/bundles/js/music.meetingparameters.list.widget.js").Include(
                "~/Assets/js/music/meetingparameters/music.meetingparameters.datatable.widget.js",
                "~/Assets/js/music/meetingparameters/music.meetingparameters.totalcount.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/music.meetingparameters.editioncount.widget.js").Include(
                "~/Assets/js/music/meetingparameters/music.meetingparameters.editioncount.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/music.meetingparameters.create.js").Include(
                "~/Assets/js/music/meetingparameters/music.meetingparameters.create.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/music.meetingparameters.delete.js").Include(
                "~/Assets/js/music/meetingparameters/music.meetingparameters.delete.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/music.meetingparameters.details.js").Include(
                "~/Assets/js/music/meetingparameters/music.meetingparameters.maininformation.widget.js",
                "~/Assets/js/music/meetingparameters/music.meetingparameters.rooms.widget.js"));

            #endregion

            #region Meetings Scheduled - Music

            bundles.Add(new ScriptBundle("~/bundles/js/music.meetings.editionscheduledcount.widget.js").Include(
                "~/Assets/js/music/meetings/music.meetings.editionscheduledcount.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/music.meetings.editionunscheduledcount.widget.js").Include(
                "~/Assets/js/music/meetings/music.meetings.editionunscheduledcount.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/music.meetings.editionscheduledcount.gauge.widget.js").Include(
                "~/Assets/js/music/meetings/music.meetings.editionscheduledcount.gauge.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/music.meetings.generate.widget.js").Include(
                "~/Assets/js/music/meetings/music.meetings.status.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/music.meetings.scheduled.widget.js").Include(
                "~/Assets/js/music/meetings/music.meetings.scheduled.widget.js",
                "~/Assets/js/music/meetings/music.meetings.scheduled.delete.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/music.meetings.unscheduled.widget.js").Include(
                "~/Assets/js/music/meetings/music.meetings.unscheduled.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/music.meetings.scheduled.create.js").Include(
                "~/Assets/js/music/meetings/music.meetings.scheduled.create.js",
                "~/Assets/js/music/meetings/music.meetings.logistics.info.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/music.meetings.scheduled.update.js").Include(
                "~/Assets/js/music/meetings/music.meetings.scheduled.update.js",
                "~/Assets/js/music/meetings/music.meetings.logistics.info.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/music.meetings.report.widget.js").Include(
                "~/Assets/js/music/meetings/music.meetings.report.widget.js"));

            #endregion

            #region Commissions

            bundles.Add(new ScriptBundle("~/bundles/js/music.commissions.list.js").Include(
                "~/Assets/js/music/commissions/music.commissions.totalcount.widget.js",
                "~/Assets/js/music/commissions/music.commissions.datatable.widget.js",
                "~/Assets/js/salesplatforms/salesplatforms.export.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/music.commissions.editioncount.js").Include(
                "~/Assets/js/music/commissions/music.commissions.editioncount.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/music.commissions.create.js").Include(
                "~/Assets/js/music/commissions/music.commissions.create.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/music.commissions.details.js").Include(
                "~/Assets/js/collaborators/collaborators.maininformation.widget.js",
                "~/Assets/js/collaborators/collaborators.socialnetworks.widget.js",
                "~/Assets/js/collaborators/collaborators.onboardinginfo.widget.js",
                "~/Assets/js/music/commissions/music.commissions.evaluations.widget.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/js/music.commissions.update.js").Include(
                "~/Assets/js/music/commissions/music.commissions.update.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/music.commissions.delete.js").Include(
                "~/Assets/js/music/commissions/music.commissions.delete.js"));

            #endregion

            #region Projects

            bundles.Add(new ScriptBundle("~/bundles/js/music.projects.list.widget.js").Include(
                "~/Assets/js/music/projects/music.projects.datatable.widget.js",
                "~/Assets/js/music/projects/music.projects.totalcount.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/music.projects.editioncount.widget.js").Include(
                "~/Assets/js/music/projects/music.projects.editioncount.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/music.projects.editioncount.pie.widget.js").Include(
                "~/Assets/js/music/projects/music.projects.editioncount.pie.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/music.projects.widget.js").Include(
                "~/Assets/js/music/projects/music.projects.maininformation.widget.js",
                "~/Assets/js/music/projects/music.projects.members.widget.js",
                "~/Assets/js/music/projects/music.projects.teammembers.widget.js",
                "~/Assets/js/music/projects/music.projects.releasedprojects.widget.js",
                "~/Assets/js/music/projects/music.projects.evaluators.widget.js",
                "~/Assets/js/music/projects/music.projects.responsible.widget.js",
                "~/Assets/js/music/projects/music.projects.clipping.widget.js",
                "~/Assets/js/music/projects/music.projects.videoandmusic.widget.js",
                "~/Assets/js/music/projects/music.projects.socialnetworks.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/music.projects.evaluation.widget.js").Include(
                "~/Assets/js/music/projects/music.projects.evaluation.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/music.projects.delete.js").Include(
                "~/Assets/js/music/projects/music.projects.delete.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/music.pitching.generate.widget.js").Include(
                "~/Assets/js/music/pitching/music.pitching.status.widget.js"));
            bundles.Add(new ScriptBundle("~/bundles/js/music.pitching.editionscheduledcount.widget.js").Include(
                "~/Assets/js/music/pitching/music.pitching.editionscheduledcount.widget.js"));
            bundles.Add(new ScriptBundle("~/bundles/js/music.pitching.editionunscheduledcount.widget.js").Include(
                "~/Assets/js/music/pitching/music.pitching.editionunscheduledcount.widget.js"));

            #endregion

            #region Players

            bundles.Add(new ScriptBundle("~/bundles/js/music.players.list.js").Include(
                "~/Assets/js/music/players/music.players.totalcount.widget.js",
                "~/Assets/js/music/players/music.players.datatable.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/music.players.editioncount.js").Include(
                "~/Assets/js/music/players/music.players.editioncount.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/music.players.editioncountodometer.widget.js").Include(
                "~/Assets/js/music/players/music.players.editioncountodometer.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/music.players.create.js").Include(
                "~/Assets/js/music/players/music.players.create.js",
                "~/Assets/js/myrio2c.companynumber.js",
                "~/Assets/js/myrio2c.additionalinfo.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/music.players.update.js").Include(
                "~/Assets/js/music/players/music.players.update.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/music.players.delete.js").Include(
                "~/Assets/js/music/players/music.players.delete.js"));

            #endregion

            #region Players - Executives
            bundles.Add(new ScriptBundle("~/bundles/js/music.players.executives.list.js").Include(
                "~/Assets/js/music/playersexecutives/music.players.executives.totalcount.widget.js",
                "~/Assets/js/music/playersexecutives/music.players.executives.datatable.widget.js",
                "~/Assets/js/salesplatforms/salesplatforms.export.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/music.players.executives.editioncount.js").Include(
                "~/Assets/js/music/playersexecutives/music.players.executives.editioncount.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/music.players.executives.create.js").Include(
               "~/Assets/js/music/playersexecutives/music.players.executives.create.js",
               "~/Assets/js/myrio2c.publicemail.js",
               "~/Assets/js/dynamic.list.js",
               "~/Assets/js/myrio2c.additionalinfo.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/music.players.executives.update.js").Include(
               "~/Assets/js/music/playersexecutives/music.players.executives.update.js",
               "~/Assets/js/myrio2c.publicemail.js",
               "~/Assets/js/dynamic.list.js",
               "~/Assets/js/myrio2c.additionalinfo.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/music.players.executives.delete.js").Include(
               "~/Assets/js/music/playersexecutives/music.players.executives.delete.js"));

            #endregion

            #region Meeting Parameters - Music

            bundles.Add(new ScriptBundle("~/bundles/js/music.meetingparameters.list.widget.js").Include(
                "~/Assets/js/music/meetingparameters/music.meetingparameters.datatable.widget.js",
                "~/Assets/js/music/meetingparameters/music.meetingparameters.totalcount.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/music.meetingparameters.editioncount.widget.js").Include(
                "~/Assets/js/music/meetingparameters/music.meetingparameters.editioncount.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/music.meetingparameters.create.js").Include(
                "~/Assets/js/music/meetingparameters/music.meetingparameters.create.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/music.meetingparameters.delete.js").Include(
                "~/Assets/js/music/meetingparameters/music.meetingparameters.delete.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/music.meetingparameters.details.js").Include(
                "~/Assets/js/music/meetingparameters/music.meetingparameters.maininformation.widget.js",
                "~/Assets/js/music/meetingparameters/music.meetingparameters.rooms.widget.js"));

            #endregion

            #region Meetings Scheduled - Music

            bundles.Add(new ScriptBundle("~/bundles/js/music.meetings.editionscheduledcount.widget.js").Include(
                "~/Assets/js/music/meetings/music.meetings.editionscheduledcount.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/music.meetings.editionunscheduledcount.widget.js").Include(
                "~/Assets/js/music/meetings/music.meetings.editionunscheduledcount.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/music.meetings.editionscheduledcount.gauge.widget.js").Include(
                "~/Assets/js/music/meetings/music.meetings.editionscheduledcount.gauge.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/music.meetings.generate.widget.js").Include(
                "~/Assets/js/music/meetings/music.meetings.status.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/music.meetings.scheduled.widget.js").Include(
                "~/Assets/js/music/meetings/music.meetings.scheduled.widget.js",
                "~/Assets/js/music/meetings/music.meetings.scheduled.delete.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/music.meetings.unscheduled.widget.js").Include(
                "~/Assets/js/music/meetings/music.meetings.unscheduled.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/music.meetings.scheduled.create.js").Include(
                "~/Assets/js/music/meetings/music.meetings.scheduled.create.js",
                "~/Assets/js/music/meetings/music.meetings.logistics.info.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/music.meetings.scheduled.update.js").Include(
                "~/Assets/js/music/meetings/music.meetings.scheduled.update.js",
                "~/Assets/js/music/meetings/music.meetings.logistics.info.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/music.meetings.report.widget.js").Include(
                "~/Assets/js/music/meetings/music.meetings.report.widget.js"));

            #endregion

            #region Meetings Unscheduled - Music

            bundles.Add(new ScriptBundle("~/bundles/js/music.meetings.unscheduled.manualschedule.js").Include(
                "~/Assets/js/music/meetings/music.meetings.unscheduled.manualschedule.js",
                "~/Assets/js/music/meetings/music.meetings.logistics.info.widget.js"));

            #endregion

            #region   Meetings Send Email Players

            bundles.Add(new ScriptBundle("~/bundles/js/music.meetings.sendemailtoplayers.list.js").Include(
                "~/Assets/js/music/meetings/music.meetings.sendemailtoplayers.totalcount.widget.js",
                "~/Assets/js/music/meetings/music.meetings.sendemailtoplayers.datatable.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/music.meetings.sendemailtoplayers.editioncount.js").Include(
                "~/Assets/js/music/meetings/music.meetings.sendemailtoplayers.editioncount.widget.js"));

            #endregion

            #region Meeting Send Email Producers

            bundles.Add(new ScriptBundle("~/bundles/js/music.meetings.sendemailtoproducers.list.js").Include(
              "~/Assets/js/music/meetings/music.meetings.sendemailtoproducers.totalcount.widget.js",
              "~/Assets/js/music/meetings/music.meetings.sendemailtoproducers.datatable.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/music.meetings.sendemailtoproducers.editioncount.js").Include(
                "~/Assets/js/music/meetings/music.meetings.sendemailtoproducers.editioncount.widget.js"));

            #endregion

            #endregion

            #region Innovation Page Bundles

            #region Commissions

            bundles.Add(new ScriptBundle("~/bundles/js/innovation.commissions.list.js").Include(
                "~/Assets/js/innovation/commissions/innovation.commissions.totalcount.widget.js",
                "~/Assets/js/innovation/commissions/innovation.commissions.datatable.widget.js",
                "~/Assets/js/salesplatforms/salesplatforms.export.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/innovation.commissions.editioncount.js").Include(
                "~/Assets/js/innovation/commissions/innovation.commissions.editioncount.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/innovation.commissions.create.js").Include(
                "~/Assets/js/innovation/commissions/innovation.commissions.create.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/innovation.commissions.details.js").Include(
                "~/Assets/js/collaborators/collaborators.maininformation.widget.js",
                "~/Assets/js/collaborators/collaborators.socialnetworks.widget.js",
                "~/Assets/js/collaborators/collaborators.onboardinginfo.widget.js",
                "~/Assets/js/innovation/commissions/innovation.commissions.tracks.widget.js",
                "~/Assets/js/innovation/commissions/innovation.commissions.evaluations.widget.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/js/innovation.commissions.update.js").Include(
                "~/Assets/js/innovation/commissions/innovation.commissions.update.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/innovation.commissions.delete.js").Include(
                "~/Assets/js/innovation/commissions/innovation.commissions.delete.js"));

            #endregion

            #region Projects

            bundles.Add(new ScriptBundle("~/bundles/js/innovation.projects.list.widget.js").Include(
                "~/Assets/js/innovation/projects/innovation.projects.datatable.widget.js",
                "~/Assets/js/innovation/projects/innovation.projects.totalcount.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/innovation.projects.editioncount.widget.js").Include(
                "~/Assets/js/innovation/projects/innovation.projects.editioncount.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/innovation.projects.editioncount.pie.widget.js").Include(
                "~/Assets/js/innovation/projects/innovation.projects.editioncount.pie.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/innovation.projects.widget.js").Include(
                "~/Assets/js/innovation/projects/innovation.projects.maininformation.widget.js",
                "~/Assets/js/innovation/projects/innovation.projects.tracks.widget.js",
                "~/Assets/js/innovation/projects/innovation.projects.objectives.widget.js",
                "~/Assets/js/innovation/projects/innovation.projects.experiences.widget.js",
                "~/Assets/js/innovation/projects/innovation.projects.technologies.widget.js",
                "~/Assets/js/innovation/projects/innovation.projects.evaluators.widget.js",
                "~/Assets/js/innovation/projects/innovation.projects.founders.widget.js",
                "~/Assets/js/innovation/projects/innovation.projects.businessinformation.widget.js",
                "~/Assets/js/innovation/projects/innovation.projects.presentation.widget.js",
                 "~/Assets/js/innovation/projects/innovation.projects.sustainable.widget.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/js/innovation.projects.evaluation.widget.js").Include(
                "~/Assets/js/innovation/projects/innovation.projects.evaluation.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/innovation.projects.delete.js").Include(
                "~/Assets/js/innovation/projects/innovation.projects.delete.js"));

            #endregion

            #region Players - Executives

            bundles.Add(new ScriptBundle("~/bundles/js/innovation.players.executives.list.js").Include(
                 "~/Assets/js/innovation/playersexecutives/innovation.players.executives.totalcount.widget.js",
                 "~/Assets/js/innovation/playersexecutives/innovation.players.executives.datatable.widget.js",
                 "~/Assets/js/salesplatforms/salesplatforms.export.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/innovation.players.executives.editioncount.js").Include(
                "~/Assets/js/innovation/playersexecutives/innovation.players.executives.editioncount.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/innovation.players.executives.create.js").Include(
                "~/Assets/js/innovation/playersexecutives/innovation.players.executives.create.js",
                "~/Assets/js/myrio2c.publicemail.js",
                "~/Assets/js/attendeeorganizations/attendeeorganizations.form.js",
                "~/Assets/js/dynamic.list.js",
                "~/Assets/js/myrio2c.additionalinfo.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/innovation.players.executives.update.js").Include(
                "~/Assets/js/innovation/playersexecutives/innovation.players.executives.update.js",
                "~/Assets/js/myrio2c.publicemail.js",
                "~/Assets/js/attendeeorganizations/attendeeorganizations.form.js",
                "~/Assets/js/dynamic.list.js",
                "~/Assets/js/myrio2c.additionalinfo.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/innovation.players.executives.delete.js").Include(
                "~/Assets/js/innovation/playersexecutives/innovation.players.executives.delete.js"));

            #endregion

            #region Track Options

            bundles.Add(new ScriptBundle("~/bundles/js/innovation.trackoptions.list.js").Include(
                "~/Assets/js/innovation/trackoptions/innovation.trackoptions.editioncount.widget.js",
                "~/Assets/js/innovation/trackoptions/innovation.trackoptions.totalcount.widget.js",
                "~/Assets/js/innovation/trackoptions/innovation.trackoptions.datatable.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/innovation.trackoptions.details.js").Include(
                "~/Assets/js/innovation/trackoptions/innovation.trackoptions.maininformation.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/innovation.trackoptions.create.js").Include(
                "~/Assets/js/innovation/trackoptions/innovation.trackoptions.create.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/innovation.trackoptions.delete.js").Include(
                "~/Assets/js/innovation/trackoptions/innovation.trackoptions.delete.js"));

            #endregion

            #region Track Option Groups

            bundles.Add(new ScriptBundle("~/bundles/js/innovation.trackoption.groups.list.js").Include(
                "~/Assets/js/innovation/trackoptiongroups/innovation.trackoption.groups.editioncount.widget.js",
                "~/Assets/js/innovation/trackoptiongroups/innovation.trackoption.groups.totalcount.widget.js",
                "~/Assets/js/innovation/trackoptiongroups/innovation.trackoption.groups.datatable.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/innovation.trackoption.groups.details.js").Include(
                "~/Assets/js/innovation/trackoptiongroups/innovation.trackoption.groups.maininformation.widget.js",
                "~/Assets/js/innovation/trackoptiongroups/innovation.trackoption.groups.trackoptions.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/innovation.trackoption.groups.create.js").Include(
                "~/Assets/js/innovation/trackoptiongroups/innovation.trackoption.groups.create.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/innovation.trackoption.groups.delete.js").Include(
                "~/Assets/js/innovation/trackoptiongroups/innovation.trackoption.groups.delete.js"));

            #endregion

            #region Players

            bundles.Add(new ScriptBundle("~/bundles/js/innovation.players.list.js").Include(
                "~/Assets/js/innovation/players/innovation.players.totalcount.widget.js",
                "~/Assets/js/innovation/players/innovation.players.datatable.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/innovation.players.editioncount.js").Include(
                "~/Assets/js/innovation/players/innovation.players.editioncount.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/innovation.players.editioncountodometer.widget.js").Include(
                "~/Assets/js/innovation/players/innovation.players.editioncountodometer.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/innovation.players.create.js").Include(
                "~/Assets/js/innovation/players/innovation.players.create.js",
                "~/Assets/js/myrio2c.companynumber.js",
                "~/Assets/js/myrio2c.additionalinfo.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/innovation.players.update.js").Include(
                "~/Assets/js/innovation/players/innovation.players.update.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/innovation.players.delete.js").Include(
                "~/Assets/js/innovation/players/innovation.players.delete.js"));

            #endregion

            #endregion

            #region Creator Page Bundles

            #region Commissions

            bundles.Add(new ScriptBundle("~/bundles/js/creator.commissions.list.js").Include(
                "~/Assets/js/creator/commissions/creator.commissions.totalcount.widget.js",
                "~/Assets/js/creator/commissions/creator.commissions.datatable.widget.js",
                "~/Assets/js/salesplatforms/salesplatforms.export.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/creator.commissions.editioncount.js").Include(
                "~/Assets/js/creator/commissions/creator.commissions.editioncount.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/creator.commissions.create.js").Include(
                "~/Assets/js/creator/commissions/creator.commissions.create.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/creator.commissions.details.js").Include(
                "~/Assets/js/collaborators/collaborators.maininformation.widget.js",
                "~/Assets/js/collaborators/collaborators.socialnetworks.widget.js",
                "~/Assets/js/collaborators/collaborators.onboardinginfo.widget.js",
                "~/Assets/js/creator/commissions/creator.commissions.evaluations.widget.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/js/creator.commissions.update.js").Include(
                "~/Assets/js/creator/commissions/creator.commissions.update.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/creator.commissions.delete.js").Include(
                "~/Assets/js/creator/commissions/creator.commissions.delete.js"));

            #endregion

            #endregion

            #region Cartoon Page Bundles

            #region Commissions

            bundles.Add(new ScriptBundle("~/bundles/js/cartoon.commissions.list.js").Include(
                "~/Assets/js/cartoon/commissions/cartoon.commissions.totalcount.widget.js",
                "~/Assets/js/cartoon/commissions/cartoon.commissions.datatable.widget.js",
                "~/Assets/js/salesplatforms/salesplatforms.export.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/cartoon.commissions.editioncount.js").Include(
                "~/Assets/js/cartoon/commissions/cartoon.commissions.editioncount.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/cartoon.commissions.create.js").Include(
                "~/Assets/js/cartoon/commissions/cartoon.commissions.create.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/cartoon.commissions.details.js").Include(
                "~/Assets/js/collaborators/collaborators.maininformation.widget.js",
                "~/Assets/js/collaborators/collaborators.socialnetworks.widget.js",
                "~/Assets/js/collaborators/collaborators.onboardinginfo.widget.js",
                "~/Assets/js/cartoon/commissions/cartoon.commissions.evaluations.widget.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/js/cartoon.commissions.update.js").Include(
                "~/Assets/js/cartoon/commissions/cartoon.commissions.update.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/cartoon.commissions.delete.js").Include(
                "~/Assets/js/cartoon/commissions/cartoon.commissions.delete.js"));

            #endregion

            #region Projects

            bundles.Add(new ScriptBundle("~/bundles/js/cartoon.projects.list.widget.js").Include(
                "~/Assets/js/cartoon/projects/cartoon.projects.datatable.widget.js",
                "~/Assets/js/cartoon/projects/cartoon.projects.totalcount.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/cartoon.projects.editioncount.widget.js").Include(
                "~/Assets/js/cartoon/projects/cartoon.projects.editioncount.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/cartoon.projects.editioncount.pie.widget.js").Include(
                "~/Assets/js/cartoon/projects/cartoon.projects.editioncount.pie.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/cartoon.projects.widget.js").Include(
                "~/Assets/js/cartoon/projects/cartoon.projects.maininformation.widget.js",
                "~/Assets/js/cartoon/projects/cartoon.projects.evaluators.widget.js",
                "~/Assets/js/cartoon/projects/cartoon.projects.creators.widget.js",
                "~/Assets/js/cartoon/projects/cartoon.projects.organization.widget.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/js/cartoon.projects.evaluation.widget.js").Include(
                "~/Assets/js/cartoon/projects/cartoon.projects.evaluation.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/cartoon.projects.delete.js").Include(
                "~/Assets/js/cartoon/projects/cartoon.projects.delete.js"));

            #endregion

            #endregion

            #region Agendas - Executives

            bundles.Add(new ScriptBundle("~/bundles/js/agendas.executives.list.js").Include(
                 "~/Assets/js/agendas/executives/agendas.executives.totalcount.widget.js",
                 "~/Assets/js/agendas/executives/agendas.executives.datatable.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/agendas.executives.editioncount.js").Include(
                "~/Assets/js/agendas/executives/agendas.executives.editioncount.widget.js"));

            #endregion

            #region Logistics - Sponsors

            bundles.Add(new ScriptBundle("~/bundles/js/logisticsponsors.list.js").Include(
                "~/Assets/js/logisticsponsors/logisticsponsors.totalcount.widget.js",
                "~/Assets/js/logisticsponsors/logisticsponsors.datatable.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/logisticsponsors.editioncount.js").Include(
                "~/Assets/js/logisticsponsors/logisticsponsors.editioncount.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/logisticsponsors.create.js").Include(
                "~/Assets/js/logisticsponsors/logisticsponsors.create.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/logisticsponsors.details.js").Include(
                "~/Assets/js/logisticsponsors/logisticsponsors.maininformation.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/logisticsponsors.delete.js").Include(
                "~/Assets/js/logisticsponsors/logisticsponsors.delete.js"));

            #endregion

            #region Logistics - Requests

            bundles.Add(new ScriptBundle("~/bundles/js/logistics.list.js").Include(
                "~/Assets/js/logistics/logistics.datatable.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/logistics.create.js").Include(
                "~/Assets/js/logistics/logistics.create.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/logistics.update.js").Include(
                "~/Assets/js/logistics/logistics.update.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/logistics.delete.js").Include(
                "~/Assets/js/logistics/logistics.delete.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/logistics.details.js").Include(
                "~/Assets/js/logistics/logistics.maininformation.widget.js",
                "~/Assets/js/logistics/logistics.airfare.create.js",
                "~/Assets/js/logistics/logistics.airfare.update.js",
                "~/Assets/js/logistics/logistics.airfare.widget.js",
                "~/Assets/js/logistics/logistics.airfare.delete.js",
                "~/Assets/js/logistics/logistics.accommodation.create.js",
                "~/Assets/js/logistics/logistics.accommodation.update.js",
                "~/Assets/js/logistics/logistics.accommodation.widget.js",
                "~/Assets/js/logistics/logistics.accommodation.delete.js",
                "~/Assets/js/logistics/logistics.transfer.create.js",
                "~/Assets/js/logistics/logistics.transfer.update.js",
                "~/Assets/js/logistics/logistics.transfer.widget.js",
                "~/Assets/js/logistics/logistics.transfer.delete.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/logistics.info.js").Include(
                "~/Assets/js/logistics/logistics.info.widget.js"));

            #endregion

            #region Logistics - Availabilities

            bundles.Add(new ScriptBundle("~/bundles/js/availabilities.list.js").Include(
                "~/Assets/js/availabilities/availabilities.totalcount.widget.js",
                "~/Assets/js/availabilities/availabilities.datatable.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/availabilities.editioncount.js").Include(
                "~/Assets/js/availabilities/availabilities.editioncount.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/availabilities.create.js").Include(
                "~/Assets/js/availabilities/availabilities.create.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/availabilities.update.js").Include(
                "~/Assets/js/availabilities/availabilities.update.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/availabilities.delete.js").Include(
                "~/Assets/js/availabilities/availabilities.delete.js"));

            #endregion

            #region Places

            bundles.Add(new ScriptBundle("~/bundles/js/places.list.js").Include(
                "~/Assets/js/places/places.totalcount.widget.js",
                "~/Assets/js/places/places.datatable.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/places.editioncount.js").Include(
                "~/Assets/js/places/places.editioncount.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/places.create.js").Include(
                "~/Assets/js/places/places.create.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/places.details.js").Include(
                "~/Assets/js/places/places.maininformation.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/places.delete.js").Include(
                "~/Assets/js/places/places.delete.js"));

            #endregion

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

            //#region bundlesAngular

            //var bundlesAngular = new ScriptBundle("~/Scripts/angular/bundles");
            //bundlesAngular.Include("~/Scripts/angular/angular.min.js");
            //bundlesAngular.Include("~/Scripts/angular/angular-aria.min.js");
            //bundlesAngular.Include("~/Scripts/angular/angular-animate.min.js");
            //bundlesAngular.Include("~/Scripts/angular/angular-messages.min.js");
            //bundlesAngular.Include("~/Scripts/angular/angular-sanitize.min.js");
            //bundlesAngular.Include("~/Scripts/angular/angular-route.min.js");
            //bundlesAngular.Include("~/Scripts/angular/angular-resource.min.js");
            //bundlesAngular.Include("~/Scripts/angular/angular-cookies.min.js");
            //bundlesAngular.Include("~/Scripts/angular/i18n/angular-locale_pt-br.js");            
            //bundlesAngular.Include("~/Scripts/angular-ui/ui-bootstrap.min.js");
            //bundlesAngular.Include("~/Scripts/angular-ui/ui-bootstrap-tpls.min.js");
            //bundlesAngular.Include("~/Scripts/ngToast/ngToast.min.js");
            //bundlesAngular.Include("~/Scripts/MarlinToolKit/MarlinAlert/MarlinAlert.module.js");
            //bundlesAngular.Include("~/Scripts/MarlinToolKit/MarlinAlert/MarlinAlert.config.js");
            //bundlesAngular.Include("~/Scripts/MarlinToolKit/MarlinAlert/*.js");
            //bundlesAngular.Include("~/Scripts/MarlinToolKit/MarlinToolKit.module.js");
            //bundlesAngular.Include("~/Scripts/angular-chart/Chart.min.js");
            //bundlesAngular.Include("~/Scripts/angular-chart/angular-chart.min.js");
            //bundlesAngular.Include("~/Scripts/angular-count-to/angular-count-to.min.js");

            //bundlesAngular.Include("~/Scripts/Moment/moment.min.js");
            //bundlesAngular.Include("~/Scripts/Moment/moment-with-locales.min.js");

            //bundles.Add(bundlesAngular);

            //#endregion

            //#region bundlesAngularExtensions

            //var bundlesAngularExtensions = new ScriptBundle("~/Content/js/Rio2CAdmin/bundles");
            //bundlesAngularExtensions.Include("~/Content/js/MarlinValidate/MarlinValidate.module.js");
            //bundlesAngularExtensions.Include("~/Content/js/MarlinValidate/*.js");
            //bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/Rio2CAdmin.module.js");
            //bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/Rio2CAdmin.loadImage.directive.js");
            //bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/Collaborator/Collaborator.module.js");
            //bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/Collaborator/*.js");
            //bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/Logistics/Logistics.module.js");
            //bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/Logistics/*.js");
            //bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/Conference/Conference.module.js");
            //bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/Conference/*.js");
            //bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/Producer/Producer.module.js");
            //bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/Producer/*.js");
            //bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/Project/Project.module.js");
            //bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/Project/*.js");
            //bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/Room/Room.module.js");
            //bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/Room/*.js");
            //bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/RoleLecturer/RoleLecturer.module.js");
            //bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/RoleLecturer/*.js");
            //bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/Dashboard/Dashboard.module.js");
            //bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/Dashboard/*.js");
            //bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/OneToOneMeetings/OneToOneMeetings.module.js");
            //bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/OneToOneMeetings/OneToOneMeetings.service.js");
            //bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/OneToOneMeetings/*.js");
            //bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/Holding/Holding.module.js");
            //bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/Holding/*.js");
            //bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/CollaboratorProducer/CollaboratorProducer.module.js");
            //bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/CollaboratorProducer/*.js");
            //bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/Schedule/Schedule.module.js");
            //bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/Schedule/*.js");
            //bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/FinancialReport/FinancialReport.module.js");
            //bundlesAngularExtensions.Include("~/Content/js/Rio2CAdmin/FinancialReport/*.js");


            //bundles.Add(bundlesAngularExtensions);

            //#endregion

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

            // Required to generate bundles on release running in visual studio
#if !DEBUG
            BundleTable.EnableOptimizations = true;
#endif
        }
    }
}
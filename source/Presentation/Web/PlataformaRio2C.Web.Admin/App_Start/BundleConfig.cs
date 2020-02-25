// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-25-2020
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
                "~/Client Scripts/mvcfoolproof.unobtrusive.js"
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

            //#region CKEditor

            //bundles.Add(new ScriptBundle("~/bundles/js/ckEditor.js").Include(
            //    "~/Scripts/ckeditor/ckeditor.js",
            //    "~/Content/js/ckeditor_config.js"));

            //#endregion

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
                "~/Assets/themes/metronic/vendors/general/bootstrap-datetime-picker/js/bootstrap-datetimepicker.js"));

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

            bundles.Add(new ScriptBundle("~/bundles/js/organizations.list.js").Include(
                "~/Assets/js/organizations/organizations.totalcount.widget.js",
                "~/Assets/js/organizations/organizations.datatable.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/organizations.editioncount.js").Include(
                "~/Assets/js/organizations/organizations.editioncount.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/organizations.create.js").Include(
                "~/Assets/js/organizations/organizations.create.js",
                "~/Assets/js/myrio2c.companynumber.js",
                "~/Assets/js/myrio2c.additionalinfo.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/organizations.update.js").Include(
                "~/Assets/js/organizations/organizations.update.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/organizations.delete.js").Include(
                "~/Assets/js/organizations/organizations.delete.js"));

            #endregion

            #region Collaborators

            bundles.Add(new ScriptBundle("~/bundles/js/collaborators.list.js").Include(
                "~/Assets/js/collaborators/collaborators.totalcount.widget.js",
                "~/Assets/js/collaborators/collaborators.datatable.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/collaborators.editioncount.js").Include(
                "~/Assets/js/collaborators/collaborators.editioncount.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/collaborators.create.js").Include(
                "~/Assets/js/collaborators/collaborators.create.js",
                "~/Assets/js/myrio2c.publicemail.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/collaborators.update.js").Include(
                "~/Assets/js/collaborators/collaborators.update.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/collaborators.delete.js").Include(
                "~/Assets/js/collaborators/collaborators.delete.js"));

            #endregion

            #region Collaborators - Speakers

            bundles.Add(new ScriptBundle("~/bundles/js/speakers.list.js").Include(
                "~/Assets/js/speakers/speakers.totalcount.widget.js",
                "~/Assets/js/speakers/speakers.datatable.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/speakers.editioncount.js").Include(
                "~/Assets/js/speakers/speakers.editioncount.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/speakers.create.js").Include(
                "~/Assets/js/speakers/speakers.create.js",
                "~/Assets/js/myrio2c.publicemail.js"));

            bundles.Add(new StyleBundle("~/bundles/css/speakers.details.css").Include(
                "~/Assets/themes/metronic/css/demo4/pages/pricing/pricing-1.css"));

            bundles.Add(new ScriptBundle("~/bundles/js/speakers.details.js").Include(
                "~/Assets/js/speakers/speakers.maininformation.widget.js",
                "~/Assets/js/speakers/speakers.socialnetworks.widget.js",
                "~/Assets/js/speakers/speakers.company.widget.js",
                "~/Assets/js/speakers/speakers.conferences.widget.js",
                "~/Assets/js/speakers/speakers.participants.widget.js",
                "~/Assets/js/companies/companyinfo.autocomplete.js",
                "~/Assets/js/myrio2c.companynumber.js",
                "~/Assets/js/speakers/speakers.apiconfiguration.widget.js"));

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
                "~/Assets/js/conferences/conferences.participants.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/conferences.delete.js").Include(
                "~/Assets/js/conferences/conferences.delete.js"));

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

            #region Projects

            bundles.Add(new ScriptBundle("~/bundles/js/projects.pitching.widget.js").Include(
                "~/Assets/js/projects/projects.pitching.datatable.widget.js",
                "~/Assets/js/projects/projects.pitching.totalcount.widget.js",
                "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/projects.pitching.editioncount.widget.js").Include(
                "~/Assets/js/projects/projects.pitching.editioncount.widget.js"));

            #endregion

            #region Reports
            #region Audiovisual
            bundles.Add(new ScriptBundle("~/bundles/js/reports.audiovisual.subscriptions.widget.js").Include(
                "~/Assets/js/reports/reports.audiovisual.subscriptions.widget.js",
                "~/Scripts/jquery.unobtrusive-ajax.js"));

            #endregion
            #endregion

            #endregion

            #region Music Page Bundles

            #region Collaborators - Commissions

            bundles.Add(new ScriptBundle("~/bundles/js/music.commissions.list.js").Include(
                "~/Assets/js/music/commissions/music.commissions.totalcount.widget.js",
                "~/Assets/js/music/commissions/music.commissions.datatable.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/music.commissions.editioncount.js").Include(
                "~/Assets/js/music/commissions/music.commissions.editioncount.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/music.commissions.create.js").Include(
                "~/Assets/js/music/commissions/music.commissions.create.js"));

            //bundles.Add(new StyleBundle("~/bundles/css/speakers.details.css").Include(
            //    "~/Assets/themes/metronic/css/demo4/pages/pricing/pricing-1.css"));

            //bundles.Add(new ScriptBundle("~/bundles/js/speakers.details.js").Include(
            //    "~/Assets/js/speakers/speakers.maininformation.widget.js",
            //    "~/Assets/js/speakers/speakers.socialnetworks.widget.js",
            //    "~/Assets/js/speakers/speakers.company.widget.js",
            //    "~/Assets/js/speakers/speakers.conferences.widget.js",
            //    "~/Assets/js/speakers/speakers.participants.widget.js",
            //    "~/Assets/js/companies/companyinfo.autocomplete.js",
            //    "~/Assets/js/myrio2c.companynumber.js",
            //    "~/Assets/js/speakers/speakers.apiconfiguration.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/music.commissions.update.js").Include(
                "~/Assets/js/music/commissions/music.commissions.update.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/music.commissions.delete.js").Include(
                "~/Assets/js/music/commissions/music.commissions.delete.js"));

            #endregion

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
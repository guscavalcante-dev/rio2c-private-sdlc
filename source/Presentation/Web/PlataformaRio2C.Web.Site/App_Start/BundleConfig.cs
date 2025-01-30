// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-13-2025
// ***********************************************************************
// <copyright file="BundleConfig.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Presentation;
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
                "~/Assets/components/globalize/globalize.js",
                "~/Assets/components/globalize/cultures/globalize.culture.pt-BR.js",
                "~/Assets/components/jquery.form/jquery.form.js",
                "~/Assets/components/hideshowpassword/hideShowPassword.js",
                "~/Assets/js/myrio2c.showhidepassword.js",
                "~/Assets/js/accounts/accounts.emailsettings.js",
                "~/Assets/js/accounts/accounts.password.js",
                "~/Assets/components/bootstrap-maxlength/src/bootstrap-maxlength.js",
                "~/Assets/js/networks/networks.notification.common.js"));

            #endregion

            #region Components Bundles

            #region SignalR

            bundles.Add(new ScriptBundle("~/bundles/js/jquery.signalR.js").Include(
                "~/Scripts/jquery.signalR-2.4.1.js"));

            #endregion

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

            #region TimeAgo

            bundles.Add(new ScriptBundle("~/bundles/js/jquery.timeago.js").Include(
                "~/Assets/components/jquery.timeago/jquery.timeago.js"));

            #endregion

            #region FullCalendar

            bundles.Add(new StyleBundle("~/bundles/css/fullcalendar.css").Include(
                "~/Assets/themes/metronic/vendors/custom/fullcalendar/fullcalendar.bundle.css"));

            bundles.Add(new ScriptBundle("~/bundles/js/fullcalendar.js").Include(
                "~/Assets/themes/metronic/vendors/custom/fullcalendar/fullcalendar.bundle.js",
                "~/Assets/components/fullcalendar/locales-all.js"));

            #endregion

            #region Dynamic List

            bundles.Add(new ScriptBundle("~/bundles/js/dynamic.list.js").Include(
                "~/Assets/js/dynamic.list.js"));

            #endregion

            #region Chronograph

            bundles.Add(new StyleBundle("~/bundles/css/chronograph.css").Include(
                "~/Assets/components/chronograph/fonts.css",
                "~/Assets/components/chronograph/chronograph.css"));

            bundles.Add(new ScriptBundle("~/bundles/js/chronograph.js").Include(
                "~/Assets/components/chronograph/chronograph.js"));

            #endregion

            #endregion

            #region Common Pages Bundles

            #region Agenda

            bundles.Add(new ScriptBundle("~/bundles/js/agendas.widget.js").Include(
                "~/Assets/js/agendas/agendas.widget.js"));

            #endregion

            #region WeConnect

            bundles.Add(new ScriptBundle("~/bundles/js/weconnect.widget.js").Include(
              "~/Assets/js/weconnect/weconnect.widget.js"));

            #endregion

            #endregion

            #region Audiovisual Pages Bundles

            #region Onboarding Wizard

            bundles.Add(new StyleBundle("~/bundles/css/onboardingIndex").Include(
                "~/Assets/css/pages/onboarding/wizard-2.css"));

            bundles.Add(new ScriptBundle("~/bundles/js/onboarding.collaboratordata.js").Include(
                "~/Assets/js/onboarding/onboarding.collaboratordata.js",
                "~/Assets/js/myrio2c.publicemail.js",
                "~/Assets/js/myrio2c.additionalinfo.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/onboarding.playerinfo.js").Include(
                "~/Assets/js/onboarding/onboarding.playerinfo.js",
                "~/Assets/js/myrio2c.companynumber.js",
                "~/Assets/js/myrio2c.additionalinfo.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/onboarding.interests.js").Include(
                "~/Assets/js/onboarding/onboarding.interests.js",
                "~/Assets/js/myrio2c.additionalinfo.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/onboarding.companyinfo.js").Include(
                "~/Assets/js/onboarding/onboarding.companyinfo.js",
                "~/Assets/js/companies/companyinfo.autocomplete.js",
                "~/Assets/js/myrio2c.companynumber.js",
                "~/Assets/js/myrio2c.additionalinfo.js"));

            bundles.Add(new StyleBundle("~/bundles/css/onboarding.projects.css").Include(
                "~/Assets/css/pages/projects/projects.wizard-3.css"));

            #endregion

            #region Projects - Business Rounds

            bundles.Add(new ScriptBundle("~/bundles/js/businessrounds.producerinfo.js").Include(
                "~/Assets/js/projects/businessrounds/businessrounds.producerinfo.js",
                "~/Assets/js/companies/companyinfo.autocomplete.js",
                "~/Assets/js/myrio2c.companynumber.js",
                "~/Assets/js/myrio2c.additionalinfo.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/businessrounds.projectinfo.js").Include(
                "~/Assets/js/projects/businessrounds/businessrounds.projectinfo.js",
                "~/Assets/js/myrio2c.additionalinfo.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/businessrounds.buyercompany.widget.js").Include(
                "~/Assets/js/projects/businessrounds/businessrounds.buyercompany.widget.js",
                "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/businessrounds.widget.js").Include(
                "~/Assets/js/projects/businessrounds/businessrounds.maininformation.widget.js",
                "~/Assets/js/projects/businessrounds/businessrounds.interest.widget.js",
                "~/Assets/js/projects/businessrounds/businessrounds.links.widget.js",
                "~/Assets/js/myrio2c.additionalinfo.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/businessrounds.buyerevaluation.list.widget.js").Include(
                "~/Assets/js/projects/businessrounds/businessrounds.buyerevaluation.list.widget.js",
                "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/businessrounds.buyerevaluation.update.widget.js").Include(
                "~/Assets/js/projects/businessrounds/businessrounds.buyerevaluation.update.widget.js"));

            #endregion

            #region Projects - Pitching

            bundles.Add(new ScriptBundle("~/bundles/js/pitching.widget.js").Include(
               "~/Assets/js/projects/pitching/pitching.maininformation.widget.js",
               "~/Assets/js/projects/pitching/pitching.interest.widget.js",
               "~/Assets/js/projects/pitching/pitching.links.widget.js",
               "~/Assets/js/myrio2c.additionalinfo.js"));

            #endregion

            #region Projects - Commissions

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.projects.widget.js").Include(
               "~/Assets/js/audiovisual/projects/audiovisual.projects.maininformation.widget.js",
               "~/Assets/js/audiovisual/projects/audiovisual.projects.commission.evaluation.widget.js",
               "~/Assets/js/audiovisual/projects/audiovisual.projects.evaluators.widget.js",
               "~/Assets/js/audiovisual/projects/audiovisual.projects.interest.widget.js",
               "~/Assets/js/audiovisual/projects/audiovisual.projects.links.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.projects.evaluation.list.widget.js").Include(
                "~/Assets/js/audiovisual/projects/audiovisual.projects.evaluation.list.widget.js",
                "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.projects.evaluation.widget.js").Include(
                "~/Assets/js/audiovisual/projects/audiovisual.projects.evaluation.widget.js"));

            #endregion

            #region Companies

            bundles.Add(new ScriptBundle("~/bundles/js/companies.widget.js").Include(
                "~/Assets/js/companies/companies.maininformation.widget.js",
                "~/Assets/js/companies/companies.socialnetworks.widget.js",
                "~/Assets/js/companies/companies.address.widget.js",
                "~/Assets/js/companies/companies.activity.widget.js",
                "~/Assets/js/companies/companies.targetaudience.widget.js",
                "~/Assets/js/companies/companies.interest.widget.js",
                "~/Assets/js/companies/companies.executive.widget.js",
                "~/Assets/js/myrio2c.companynumber.js",
                "~/Assets/js/myrio2c.additionalinfo.js"));

            #endregion

            #region Executives

            bundles.Add(new ScriptBundle("~/bundles/js/executives.widget.js").Include(
                "~/Assets/js/executives/executives.company.widget.js",
                "~/Assets/js/executives/executives.maininformation.widget.js",
                "~/Assets/js/executives/executives.socialnetworks.widget.js",
                "~/Assets/js/executives/executives.logisticinfo.widget.js",
                "~/Assets/js/companies/companyinfo.autocomplete.js",
                "~/Assets/js/myrio2c.companynumber.js",
                "~/Assets/js/myrio2c.publicemail.js",
                "~/Assets/js/myrio2c.additionalinfo.js"));

            #endregion

            #region Addresses

            bundles.Add(new ScriptBundle("~/bundles/js/addresses.form.js").Include(
                "~/Assets/js/addresses/addresses.form.js"));

            #endregion

            #region Attendee Organizations

            bundles.Add(new ScriptBundle("~/bundles/js/attendeeorganizations.form.js").Include(
                "~/Assets/js/attendeeorganizations/attendeeorganizations.form.js"));

            #endregion

            #region Networks

            bundles.Add(new ScriptBundle("~/bundles/js/networks.contacts.js").Include(
                "~/Assets/js/networks/networks.contacts.js",
                "~/Assets/js/networks/networks.contacts.list.widget.js",
                "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/networks.messages.js").Include(
                "~/Assets/js/networks/networks.messages.conversations.widget.js",
                "~/Assets/js/networks/networks.messages.conversation.widget.js",
                "~/Assets/js/networks/chat.js"));

            #endregion

            #region Meetings

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.meetings.list.widget.js").Include(
                "~/Assets/js/audiovisual/meetings/audiovisual.meetings.scheduled.widget.js",
                "~/Assets/js/audiovisual/meetings/audiovisual.meetings.scheduled.delete.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/audiovisual.meetings.widget.js").Include(
                "~/Assets/js/audiovisual/meetings/audiovisual.meetings.maininformation.widget.js",
                "~/Assets/js/audiovisual/meetings/audiovisual.meetings.virtualmeeting.widget.js"));

            #endregion

            #endregion

            #region Music Page Bundles

            #region Projects - Commissions

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

            bundles.Add(new ScriptBundle("~/bundles/js/music.projects.evaluation.list.widget.js").Include(
                "~/Assets/js/music/projects/music.projects.evaluation.list.widget.js",
                "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/music.projects.evaluation.widget.js").Include(
                "~/Assets/js/music/projects/music.projects.evaluation.widget.js"));

            #endregion

            #region Projects - Business Rounds

            bundles.Add(new ScriptBundle("~/bundles/js/music.businessrounds.buyerevaluation.list.widget.js").Include(
                "~/Assets/js/music/businessrounds/music.businessrounds.buyerevaluation.list.widget.js",
                "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/music.businessrounds.buyerevaluation.update.widget.js").Include(
                "~/Assets/js/music/businessrounds/music.businessrounds.buyerevaluation.update.widget.js"));

              bundles.Add(new ScriptBundle("~/bundles/js/music.businessrounds.buyercompany.widget.js").Include(
                "~/Assets/js/music/businessrounds/music.businessrounds.buyercompany.widget.js", 
                "~/Scripts/jquery.unobtrusive-ajax.js"));


            bundles.Add(new ScriptBundle("~/bundles/js/music.businessrounds.widget.js").Include(
               "~/Assets/js/music/businessrounds/music.businessrounds.maininformation.widget.js",
               "~/Assets/js/myrio2c.additionalinfo.js"));

            #endregion

            #endregion

            #region Cartoon Page Bundles

            #region Projects - Commissions

            bundles.Add(new ScriptBundle("~/bundles/js/cartoon.projects.widget.js").Include(
                "~/Assets/js/cartoon/projects/cartoon.projects.maininformation.widget.js",
                "~/Assets/js/cartoon/projects/cartoon.projects.creators.widget.js",
                "~/Assets/js/cartoon/projects/cartoon.projects.evaluators.widget.js",
                "~/Assets/js/cartoon/projects/cartoon.projects.organization.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/cartoon.projects.evaluation.list.widget.js").Include(
                "~/Assets/js/cartoon/projects/cartoon.projects.evaluation.list.widget.js",
                "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/cartoon.projects.evaluation.widget.js").Include(
                "~/Assets/js/cartoon/projects/cartoon.projects.evaluation.widget.js"));

            #endregion

            #endregion

            #region Innovation Page Bundles

            #region Projects - Commissions

            bundles.Add(new ScriptBundle("~/bundles/js/innovation.projects.widget.js").Include(
               "~/Assets/js/innovation/projects/innovation.projects.maininformation.widget.js",
               "~/Assets/js/innovation/projects/innovation.projects.tracks.widget.js",
               "~/Assets/js/innovation/projects/innovation.projects.objectives.widget.js",
               "~/Assets/js/innovation/projects/innovation.projects.experiences.widget.js",
               "~/Assets/js/innovation/projects/innovation.projects.technologies.widget.js",
               "~/Assets/js/innovation/projects/innovation.projects.evaluators.widget.js",
               "~/Assets/js/innovation/projects/innovation.projects.founders.widget.js",
               "~/Assets/js/innovation/projects/innovation.projects.businessinformation.widget.js",
               "~/Assets/js/innovation/projects/innovation.projects.presentation.widget.js"
               ));

            bundles.Add(new ScriptBundle("~/bundles/js/innovation.projects.evaluation.list.widget.js").Include(
                "~/Assets/js/innovation/projects/innovation.projects.evaluation.list.widget.js",
                "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/innovation.projects.evaluation.widget.js").Include(
                "~/Assets/js/innovation/projects/innovation.projects.evaluation.widget.js"));

            #endregion

            #endregion

            #region Creator Page Bundles

            #region Projects - Commissions

            bundles.Add(new ScriptBundle("~/bundles/js/creator.projects.widget.js").Include(
                "~/Assets/js/creator/projects/creator.projects.maininformation.widget.js",
                "~/Assets/js/creator/projects/creator.projects.projectinformation.widget.js",
                "~/Assets/js/creator/projects/creator.projects.attachments.widget.js",
                "~/Assets/js/creator/projects/creator.projects.evaluators.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/creator.projects.evaluation.list.widget.js").Include(
                "~/Assets/js/creator/projects/creator.projects.evaluation.list.widget.js",
                "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/creator.projects.evaluation.widget.js").Include(
                "~/Assets/js/creator/projects/creator.projects.evaluation.widget.js"));

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

            //bundles.Add(new StyleBundle("~/bundles/css/projectSubmitCustomStyles").Include(
            //    "~/Assets/themes/metronic/css/demo4/pages/wizard/wizard-3.css"));

            #endregion

            #region Bootstrap Datepicker

            bundles.Add(new StyleBundle("~/bundles/css/bootstrap-datepicker.css")
                .Include("~/Assets/themes/metronic/vendors/general/bootstrap-datepicker/dist/css/bootstrap-datepicker.css", new CssRewriteUrlTransform()));

            bundles.Add(new ScriptBundle("~/bundles/js/bootstrap-datepicker.js").Include(
                //"~/Assets/themes/metronic/vendors/general/moment/min/moment-with-locales.min.js",
                "~/Assets/themes/metronic/vendors/general/bootstrap-datepicker/dist/js/bootstrap-datepicker.js",
                "~/Assets/components/bootstrap-timepicker/bootstrap-datepicker.en-us.js",
                "~/Assets/components/bootstrap-timepicker/bootstrap-datepicker.pt-br.js"));

            #endregion

            // Required to generate bundles on release running in visual studio
#if !DEBUG
            BundleTable.EnableOptimizations = true;
#endif
        }
    }
}


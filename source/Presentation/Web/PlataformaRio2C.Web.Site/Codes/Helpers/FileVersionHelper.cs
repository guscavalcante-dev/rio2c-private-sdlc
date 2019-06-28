//// ***********************************************************************
//// Assembly         : PlataformaRio2C.Web.Site
//// Author           : Rafael Dantas Ruiz
//// Created          : 06-28-2019
////
//// Last Modified By : Rafael Dantas Ruiz
//// Last Modified On : 06-28-2019
//// ***********************************************************************
//// <copyright file="FileVersionHelper.cs" company="Softo">
////     Copyright (c) Softo. All rights reserved.
//// </copyright>
//// <summary></summary>
//// ***********************************************************************
//namespace PlataformaRio2C.Web.Site.Codes.Helpers
//{
//    /// <summary>FileVersionHelper</summary>
//    public class FileVersionHelper
//    {
//        public string Filename { get; private set; }
//        public string Directory { get; private set; }
//        public string Version { get; private set; }
//        public string ExternalUrl { get; private set; }

//        /// <summary>Initializes a new instance of the <see cref="FileVersionHelper"/> class.</summary>
//        /// <param name="filename">The filename.</param>
//        /// <param name="directory">The directory.</param>
//        /// <param name="version">The version.</param>
//        /// <param name="externalUrl">The external URL.</param>
//        public FileVersionHelper(string filename, string directory, string version, string externalUrl = null)
//        {
//            this.Filename = filename;
//            this.Directory = directory;
//            this.Version = version;
//            this.ExternalUrl = externalUrl;
//        }

//        /// <summary>
//        /// Gets the URL.
//        /// </summary>
//        /// <returns>System.String.</returns>
//        public string GetUrl()
//        {
//            if (this.ExternalUrl != null)
//            {
//                return this.ExternalUrl;
//            }

//            return this.Directory + this.Filename + "?v=" + this.Version;
//        }

//        /// <summary>
//        /// Gets the URL.
//        /// </summary>
//        /// <param name="param">The parameter.</param>
//        /// <param name="extension">The extension.</param>
//        /// <param name="showEnglish">The show english.</param>
//        /// <returns></returns>
//        public string GetUrl(string param, string extension, bool showEnglish = false)
//        {
//            if (param != "en" || showEnglish)
//            {
//                return this.Directory + this.Filename + param + "." + extension + "?v=" + this.Version;
//            }

//            return null;
//        }

//        #region Directories

//        public static FileVersionHelper DirAssets = new FileVersionHelper(null, "/assets/", null);
//        public static FileVersionHelper DirAssetsJs = new FileVersionHelper(null, DirAssets.Directory + "js/", null);
//        public static FileVersionHelper DirAssetsCss = new FileVersionHelper(null, DirAssets.Directory + "css/", null);
//        public static FileVersionHelper DirAssetsComponents = new FileVersionHelper(null, DirAssets.Directory + "components/", null);
//        public static FileVersionHelper DirMetronicAssets = new FileVersionHelper(null, DirAssets.Directory + "themes/metronic/", null);
//        public static FileVersionHelper DirMetronicGlobalImg = new FileVersionHelper(null, DirMetronicAssets.Directory + "global/img/", null);
//        public static FileVersionHelper DirMetronicGlobalPlugins = new FileVersionHelper(null, DirMetronicAssets.Directory + "global/plugins/", null);
//        public static FileVersionHelper DirMetronicGlobalCss = new FileVersionHelper(null, DirMetronicAssets.Directory + "global/css/", null);
//        public static FileVersionHelper DirMetronicGlobalScripts = new FileVersionHelper(null, DirMetronicAssets.Directory + "global/scripts/", null);
//        public static FileVersionHelper DirMetronicPagesCss = new FileVersionHelper(null, DirMetronicAssets.Directory + "pages/css/", null);
//        public static FileVersionHelper DirMetronicPagesScripts = new FileVersionHelper(null, DirMetronicAssets.Directory + "pages/scripts/", null);
//        public static FileVersionHelper DirMetronicAdminCss = new FileVersionHelper(null, DirMetronicAssets.Directory + "layouts/layout4/css/", null);
//        public static FileVersionHelper DirMetronicAdminScripts = new FileVersionHelper(null, DirMetronicAssets.Directory + "layouts/layout4/scripts/", null);
//        public static FileVersionHelper DirMetronicLayoutsGlobalScripts = new FileVersionHelper(null, DirMetronicAssets.Directory + "layouts/global/scripts/", null);

//        #endregion

//        #region Metronic Template Components

//        // Layout
//        public static FileVersionHelper CssMetronicLayout = new FileVersionHelper("layout.min.css", DirMetronicAdminCss.Directory, "20160205");
//        public static FileVersionHelper JsMetronicLayout = new FileVersionHelper("layout.js", DirMetronicAdminScripts.Directory, "20160205");
//        // App
//        public static FileVersionHelper JsMetronic = new FileVersionHelper("app.js", DirMetronicGlobalScripts.Directory, "20160205");
//        // Demo
//        public static FileVersionHelper JsMetronicDemo = new FileVersionHelper("demo.js", DirMetronicAdminScripts.Directory, "20160205");
//        // Components
//        public static FileVersionHelper CssMetronicComponents = new FileVersionHelper("components.css", DirMetronicGlobalCss.Directory, "20160205");
//        public static FileVersionHelper CssMetronicComponentsRounded = new FileVersionHelper("components-rounded.css", DirMetronicGlobalCss.Directory, "20160205");
//        public static FileVersionHelper CssMetronicComponentsMd = new FileVersionHelper("components-md.min.css", DirMetronicGlobalCss.Directory, "20160205");
//        // Plugins
//        public static FileVersionHelper CssMetronicPlugins = new FileVersionHelper("plugins.css", DirMetronicGlobalCss.Directory, "20160205");
//        public static FileVersionHelper CssMetronicPluginsMd = new FileVersionHelper("plugins-md.min.css", DirMetronicGlobalCss.Directory, "20160205");
//        // Themes
//        public static FileVersionHelper CssMetronicThemesDefault = new FileVersionHelper("default.css", DirMetronicAdminCss.Directory + "themes/", "20160205");
//        public static FileVersionHelper CssMetronicThemesDarkblue = new FileVersionHelper("darkblue.css", DirMetronicAdminCss.Directory + "themes/", "20160205");
//        public static FileVersionHelper CssMetronicThemesLight = new FileVersionHelper("light.css", DirMetronicAdminCss.Directory + "themes/", "20160205");
//        public static FileVersionHelper CssMetronicCustom = new FileVersionHelper("custom.css", DirMetronicAdminCss.Directory, "20160205");
//        // Quick sidebar
//        public static FileVersionHelper JsMetronicQuickSidebar = new FileVersionHelper("quick-sidebar.js", DirMetronicLayoutsGlobalScripts.Directory, "20160205");
//        // Component pickers
//        public static FileVersionHelper JsMetronicComponentsPickers = new FileVersionHelper("components-pickers.js", DirMetronicPagesScripts.Directory, "20160205");
//        //public static FileVersionHelper JsMetronicComponentsDateTimePickers = new FileVersionHelper("components-date-time-pickers.min.js", DirMetronicPagesScripts.Directory, "20160205");

//        #endregion

//        #region Metronic Pages Css

//        // Invoice 
//        public static FileVersionHelper CssMetronicInvoice = new FileVersionHelper("invoice-2.min.css", DirMetronicPagesCss.Directory, "20160205");
//        // Princing
//        public static FileVersionHelper CssMetronicPricing = new FileVersionHelper("pricing.min.css", DirMetronicPagesCss.Directory, "20160922");

//        #endregion

//        #region Metronic Other Components

//        // JQuery
//        public static FileVersionHelper JsMetronicJquery = new FileVersionHelper("jquery.min.js", DirMetronicGlobalPlugins.Directory, "20150212");
//        // JqueryUi
//        public static FileVersionHelper CssMetronicJqueryUi = new FileVersionHelper("jquery-ui.min.css", DirMetronicGlobalPlugins.Directory + "jquery-ui/", "20160205");
//        public static FileVersionHelper JsMetronicJqueryUi = new FileVersionHelper("jquery-ui.min.js", DirMetronicGlobalPlugins.Directory + "jquery-ui/", "20160205");
//        // JQuery Validate
//        public static FileVersionHelper JsMetronicJqueryValidate = new FileVersionHelper("jquery.validate.min.js", DirMetronicGlobalPlugins.Directory + "jquery-validation/js/", "20160205");
//        public static FileVersionHelper JsMetronicJqueryValidateAdditionalMethods = new FileVersionHelper("additional-methods.min.js", DirMetronicGlobalPlugins.Directory + "jquery-validation/js/", "20160714");
//        // JQuery Migrate
//        public static FileVersionHelper JsMetronicJqueryMigrate = new FileVersionHelper("jquery-migrate.min.js", DirMetronicGlobalPlugins.Directory, "20160205");
//        // JQuery SlimpScroll
//        public static FileVersionHelper JsMetronicJquerySlimScroll = new FileVersionHelper("jquery.slimscroll.min.js", DirMetronicGlobalPlugins.Directory + "jquery-slimscroll/", "20160205");
//        // JQuery BlockUi
//        public static FileVersionHelper JsMetronicJqueryBlockUi = new FileVersionHelper("jquery.blockui.min.js", DirMetronicGlobalPlugins.Directory, "20160205");
//        // JQuery Cookie
//        public static FileVersionHelper JsMetronicJqueryCookie = new FileVersionHelper("jquery.cokie.min.js", DirMetronicGlobalPlugins.Directory, "20160205");
//        // Simple line icons
//        public static FileVersionHelper CssMetronicSimpleLineIcons = new FileVersionHelper("simple-line-icons.min.css", DirMetronicGlobalPlugins.Directory + "simple-line-icons/", "20160205");
//        // Jquery minicolors
//        public static FileVersionHelper CssMetronicJqueryMiniColors = new FileVersionHelper("jquery.minicolors.css", DirMetronicGlobalPlugins.Directory + "jquery-minicolors/", "20160205");
//        public static FileVersionHelper JsMetronicJqueryMiniColors = new FileVersionHelper("jquery.minicolors.min.js", DirMetronicGlobalPlugins.Directory + "jquery-minicolors/", "20160205");
//        // Color picker
//        public static FileVersionHelper CssMetronicColorsPicker = new FileVersionHelper("colorpicker.css", DirMetronicGlobalPlugins.Directory + "bootstrap-colorpicker/css/", "20160205");
//        // Tags input
//        public static FileVersionHelper CssMetronicTagsInput = new FileVersionHelper("bootstrap-tagsinput.css", DirMetronicGlobalPlugins.Directory + "bootstrap-tagsinput/", "20160205");
//        public static FileVersionHelper JsMetronicTagsInput = new FileVersionHelper("bootstrap-tagsinput.js", DirMetronicGlobalPlugins.Directory + "bootstrap-tagsinput/", "20160205");
//        public static FileVersionHelper JsMetronicTagsInputMin = new FileVersionHelper("bootstrap-tagsinput.min.js", DirMetronicGlobalPlugins.Directory + "bootstrap-tagsinput/", "20160205");
//        // Bootstrap
//        public static FileVersionHelper CssMetronicBootstrap = new FileVersionHelper("bootstrap.min.css", DirMetronicGlobalPlugins.Directory + "bootstrap/css/", "20160205");
//        public static FileVersionHelper JsMetronicBootstrap = new FileVersionHelper("bootstrap.min.js", DirMetronicGlobalPlugins.Directory + "bootstrap/js/", "20160205");
//        // Select2
//        public static FileVersionHelper CssMetronicSelect2 = new FileVersionHelper("select2.min.css", DirMetronicGlobalPlugins.Directory + "select2/css/", "20160205");
//        public static FileVersionHelper CssMetronicBootstrapSelect2 = new FileVersionHelper("select2-bootstrap.min.css", DirMetronicGlobalPlugins.Directory + "select2/css/", "20160205");
//        public static FileVersionHelper JsMetronicSelect2 = new FileVersionHelper("select2.min.js", DirMetronicGlobalPlugins.Directory + "select2/js/", "20160205");
//        public static FileVersionHelper JsMetronicSelect2Locale = new FileVersionHelper("", DirMetronicGlobalPlugins.Directory + "select2/js/i18n/", "20160205");
//        // Bootstrap Select
//        public static FileVersionHelper CssMetronicBootstrapSelect = new FileVersionHelper("bootstrap-select.min.css", DirMetronicGlobalPlugins.Directory + "bootstrap-select/", "20160205");
//        public static FileVersionHelper JsMetronicBootstrapSelect = new FileVersionHelper("bootstrap-select.min.js", DirMetronicGlobalPlugins.Directory + "bootstrap-select/js/", "20160205");
//        // Uniform
//        public static FileVersionHelper CssMetronicUniform = new FileVersionHelper("uniform.default.css", DirMetronicGlobalPlugins.Directory + "uniform/css/", "20160205");
//        public static FileVersionHelper JsMetronicJqueryUniform = new FileVersionHelper("jquery.uniform.min.js", DirMetronicGlobalPlugins.Directory + "uniform/", "20160205");
//        // Bootstrap Datepicker
//        public static FileVersionHelper CssMetronicBootstrapDatepicker = new FileVersionHelper("bootstrap-datepicker.css", DirMetronicGlobalPlugins.Directory + "bootstrap-datepicker/css/", "20160205");
//        public static FileVersionHelper JsMetronicBootstrapDatePicker = new FileVersionHelper("bootstrap-datepicker.min.js", DirMetronicGlobalPlugins.Directory + "bootstrap-datepicker/js/", "20160205");
//        public static FileVersionHelper JsMetronicBootstrapDatePickerLocale = new FileVersionHelper("bootstrap-datepicker.", DirMetronicGlobalPlugins.Directory + "bootstrap-datepicker/locales/", "20160205");
//        // Bootstrap Datetimepicker
//        public static FileVersionHelper CssMetronicBootstrapDatetimepicker = new FileVersionHelper("bootstrap-datetimepicker.min.css", DirMetronicGlobalPlugins.Directory + "bootstrap-datetimepicker/css/", "20160205");
//        public static FileVersionHelper JsMetronicBootstrapDateTimePicker = new FileVersionHelper("bootstrap-datetimepicker.min.js", DirMetronicGlobalPlugins.Directory + "bootstrap-datetimepicker/js/", "20160205");
//        public static FileVersionHelper JsMetronicBootstrapDatetimePickerLocale = new FileVersionHelper("bootstrap-datetimepicker.js", DirMetronicGlobalPlugins.Directory + "bootstrap-datetimepicker/js/locales/", "20160205");
//        // DateRangePicker
//        public static FileVersionHelper CssMetronicDateRangePicker = new FileVersionHelper("daterangepicker.min.css", DirMetronicGlobalPlugins.Directory + "bootstrap-daterangepicker/", "20160205");
//        public static FileVersionHelper JsMetronicDateRangePicker = new FileVersionHelper("daterangepicker.min.js", DirMetronicGlobalPlugins.Directory + "bootstrap-daterangepicker/", "20160205");
//        public static FileVersionHelper JsMetronicDateRangePickerMoment = new FileVersionHelper("moment.min.js", DirMetronicGlobalPlugins.Directory + "bootstrap-daterangepicker/", "20160205");
//        // Bootstrap DataTables
//        public static FileVersionHelper CssMetronicBootstrapDataTables = new FileVersionHelper("dataTables.bootstrap.css", DirMetronicGlobalPlugins.Directory + "datatables/plugins/bootstrap/", "20160205");
//        public static FileVersionHelper JsMetronicBootstrapDataTables = new FileVersionHelper("dataTables.bootstrap.js", DirMetronicGlobalPlugins.Directory + "datatables/plugins/bootstrap/", "20160205");
//        // Jquery DataTables
//        public static FileVersionHelper CssMetronicDataTables = new FileVersionHelper("jquery.dataTables.min.css", DirMetronicGlobalPlugins.Directory + "datatables/media/css/", "20160205");
//        public static FileVersionHelper JsMetronicJqueryDataTables = new FileVersionHelper("jquery.dataTables.min.js", DirMetronicGlobalPlugins.Directory + "datatables/media/js/", "20160205");
//        // JSTree
//        public static FileVersionHelper CssMetronicJsTree = new FileVersionHelper("style.min.css", DirMetronicGlobalPlugins.Directory + "jstree/dist/themes/default/", "20160205");
//        // Bootstrap Switch
//        public static FileVersionHelper CssMetronicBootstrapSwitch = new FileVersionHelper("bootstrap-switch.min.css", DirMetronicGlobalPlugins.Directory + "bootstrap-switch/css/", "20160205");
//        public static FileVersionHelper JsMetronicBootstrapSwitch = new FileVersionHelper("bootstrap-switch.min.js", DirMetronicGlobalPlugins.Directory + "bootstrap-switch/js/", "20160205");
//        // Bootstrap Fileinput
//        public static FileVersionHelper CssMetronicBootstrapFileinput = new FileVersionHelper("bootstrap-fileinput.css", DirMetronicGlobalPlugins.Directory + "bootstrap-fileinput/", "20160205");
//        public static FileVersionHelper JsMetronicBootstrapFileInput = new FileVersionHelper("bootstrap-fileinput.js", DirMetronicGlobalPlugins.Directory + "bootstrap-fileinput/", "20160205");
//        // Typeahed
//        public static FileVersionHelper CssMetronicTypeAhead = new FileVersionHelper("typeahead.css", DirMetronicGlobalPlugins.Directory + "typeahead/", "20160205");
//        public static FileVersionHelper JsMetronicTypeAhead = new FileVersionHelper("typeahead.bundle.min.js", DirMetronicGlobalPlugins.Directory + "typeahead/", "20160205");
//        // Pace
//        public static FileVersionHelper CssMetronicPace = new FileVersionHelper("pace-theme-minimal.css", DirMetronicGlobalPlugins.Directory + "pace/themes/", "20160205");
//        public static FileVersionHelper JsMetronicPace = new FileVersionHelper("pace.min.js", DirMetronicGlobalPlugins.Directory + "pace/", "20160205");
//        // Toastr
//        public static FileVersionHelper CssMetronicToastr = new FileVersionHelper("toastr.min.css", DirMetronicGlobalPlugins.Directory + "bootstrap-toastr/", "20160205");
//        public static FileVersionHelper JsMetronicBootstrapToastr = new FileVersionHelper("toastr.min.js", DirMetronicGlobalPlugins.Directory + "bootstrap-toastr/", "20160205");
//        // JQuery Jcrop
//        public static FileVersionHelper CssMetronicJqueryJCrop = new FileVersionHelper("jquery.Jcrop.min.css", DirMetronicGlobalPlugins.Directory + "jcrop/css/", "20160205");
//        public static FileVersionHelper JsMetronicJqueryJCrop = new FileVersionHelper("jquery.Jcrop.min.js", DirMetronicGlobalPlugins.Directory + "jcrop/js/", "20160205");
//        // Dropzone
//        public static FileVersionHelper CssMetronicDropzone = new FileVersionHelper("dropzone.css", DirMetronicGlobalPlugins.Directory + "dropzone/css/", "20160205");
//        public static FileVersionHelper JsMetronicDropzone = new FileVersionHelper("dropzone.js", DirMetronicGlobalPlugins.Directory + "dropzone/", "20160205");
//        // BootBox
//        public static FileVersionHelper JsMetronicBootBox = new FileVersionHelper("bootbox.min.js", DirMetronicGlobalPlugins.Directory + "bootbox/", "20160205");
//        // AmCharts
//        public static FileVersionHelper JsMetronicAmCharts = new FileVersionHelper("amcharts.js", DirMetronicGlobalPlugins.Directory + "amcharts/amcharts/", "20160205");
//        public static FileVersionHelper JsMetronicAmChartsSerial = new FileVersionHelper("serial.js", DirMetronicGlobalPlugins.Directory + "amcharts/amcharts/", "20160205");
//        public static FileVersionHelper JsMetronicAmChartsPie = new FileVersionHelper("pie.js", DirMetronicGlobalPlugins.Directory + "amcharts/amcharts/", "20160205");
//        public static FileVersionHelper JsMetronicAmChartsThemeLight = new FileVersionHelper("light.js", DirMetronicGlobalPlugins.Directory + "amcharts/amcharts/themes/", "20160205");
//        public static FileVersionHelper JsMetronicAmChartsLocale = new FileVersionHelper("", DirMetronicGlobalPlugins.Directory + "amcharts/amcharts/lang/", "20160205");
//        // Full callendar
//        public static FileVersionHelper JsMetronicFullCalendar = new FileVersionHelper("fullcalendar.min.js", DirMetronicGlobalPlugins.Directory + "fullcalendar/", "20160205");
//        // UI Tree
//        public static FileVersionHelper JsMetronicUiTree = new FileVersionHelper("ui-tree.js", DirMetronicPagesScripts.Directory, "20160205");
//        // Moment
//        public static FileVersionHelper JsMetronicMoment = new FileVersionHelper("moment.min.js", DirMetronicGlobalPlugins.Directory + "", "20160205");
//        // Bootstrap hover dropdown
//        public static FileVersionHelper JsMetronicBootstrapHoverDropdown = new FileVersionHelper("bootstrap-hover-dropdown.min.js", DirMetronicGlobalPlugins.Directory + "bootstrap-hover-dropdown/", "20160205");
//        // Respond
//        public static FileVersionHelper JsMetronicResponde = new FileVersionHelper("respond.min.js", DirMetronicGlobalPlugins.Directory, "20160205");
//        // Excanvas
//        public static FileVersionHelper JsMetronicExcanvas = new FileVersionHelper("excanvas.min.js", DirMetronicGlobalPlugins.Directory, "20160205");
//        // Counterup
//        public static FileVersionHelper JsMetronicJqueryWaypoints = new FileVersionHelper("jquery.waypoints.min.js", DirMetronicGlobalPlugins.Directory + "counterup/", "20160205");
//        public static FileVersionHelper JsMetronicJqueryCounterup = new FileVersionHelper("jquery.counterup.js", DirMetronicGlobalPlugins.Directory + "counterup/", "20160324");
//        // Font Awesome
//        public static FileVersionHelper CssFontAwesome = new FileVersionHelper("font-awesome.min.css", DirMetronicGlobalPlugins.Directory + "font-awesome/css/", "20160205");
//        // JQuery Backstretch
//        public static FileVersionHelper JsMetronicJqueryBackstretch = new FileVersionHelper("jquery.backstretch.min.js", DirMetronicGlobalPlugins.Directory + "backstretch/", "20160205");
//        // Prettify
//        public static FileVersionHelper JsMetronicRunPrettify = new FileVersionHelper("run_prettify.js", DirMetronicGlobalPlugins.Directory + "owl-carousel/assets/js/google-code-prettify/", "20160307");
//        //Jquery Multi-Selecty
//        public static FileVersionHelper CssMetronicJqueryMultiSelecty = new FileVersionHelper("multi-select.css", DirMetronicGlobalPlugins.Directory + "jquery-multi-select/css/", "20160329");
//        public static FileVersionHelper JsMetronicJqueryMultiSelecty = new FileVersionHelper("jquery.multi-select.js", DirMetronicGlobalPlugins.Directory + "jquery-multi-select/js/", "20160329");
//        //Jquery QuickSearch
//        public static FileVersionHelper JsMetronicJqueryQuickSearch = new FileVersionHelper("jquery.quicksearch.js", DirAssetsComponents.Directory + "quicksearch-master/", "20160329");

//        #endregion

//        #region Other Components (/assets/components)

//        // JQuery Unobtrusive Ajax
//        public static FileVersionHelper JsJqueryUnobtrusiveAjax = new FileVersionHelper("jquery.unobtrusive-ajax.min.js", "/assets/js/", "20160205");
//        public static FileVersionHelper JsJqueryValidateUnobtrusive = new FileVersionHelper("jquery.validate.unobtrusive.min.js", "/Scripts/", "20160205");
//        //public static FileVersionHelper JsJqueryMvcFoolProofUnobtrusive = new FileVersionHelper("mvcfoolproof.unobtrusive.min.js", "/Scripts/", "20160205"); //TODO: Use always the JsMvcFoolProofUnobtrusiveFixedDecimalMin
//        public static FileVersionHelper JsMvcFoolProofUnobtrusiveFixedDecimalMin = new FileVersionHelper("mvcfoolproof.unobtrusive.fixed-decimal.min.js", "/Scripts/", "20160205");
//        // Globalize
//        public static FileVersionHelper JsJqueryGlobalize = new FileVersionHelper("globalize.js", DirAssetsComponents.Directory + "globalize/", "20150212");
//        public static FileVersionHelper JsJqueryGlobalizeLocale = new FileVersionHelper("globalize.culture.", DirAssetsComponents.Directory + "globalize/cultures/", "20160205");
//        // Moment
//        public static FileVersionHelper MomentJs = new FileVersionHelper("moment.min.js", DirAssetsComponents.Directory + "moment/", "20140805");
//        public static FileVersionHelper MomentWithLangsJs = new FileVersionHelper("moment-with-langs.min.js", DirAssetsComponents.Directory + "moment/", "20160205");
//        // Timeago
//        public static FileVersionHelper JQueryTimeago = new FileVersionHelper("jquery.timeago.js", DirAssetsComponents.Directory + "timeago/", "20160205");
//        public static FileVersionHelper JQueryTimeagoLang = new FileVersionHelper("jquery.timeago", DirAssetsComponents.Directory + "timeago/", "20160205");
//        // Jquery Form
//        public static FileVersionHelper JsJQueryForm = new FileVersionHelper("jquery.form.js", DirAssetsComponents.Directory + "jquery-form/", "3.51.0-2014.06.20");
//        // Input mask
//        public static FileVersionHelper JsJQueryInputMaskBundle = new FileVersionHelper("jquery.inputmask.bundle.min.js", DirAssetsComponents.Directory + "jquery-inputmask/dist/min/", "20160205");
//        // Cropper
//        public static FileVersionHelper JsCropper = new FileVersionHelper("cropper.js", DirAssetsComponents.Directory + "cropper/dist/", "20160714");
//        public static FileVersionHelper CssCropper = new FileVersionHelper("cropper.min.css", DirAssetsComponents.Directory + "cropper/dist/", "20160714");
//        public static FileVersionHelper CssCropperMain = new FileVersionHelper("main.css", DirAssetsComponents.Directory + "cropper/demo/css/", "20160714");
//        // Color picker (check which is being used)
//        public static FileVersionHelper JsComponentsColorPickersMin = new FileVersionHelper("components-color-pickers.min.js", DirAssetsComponents.Directory + "color-pickers/", "20160205");
//        // Bootstrap color picker
//        public static FileVersionHelper CssColorpicker = new FileVersionHelper("docs.css", DirAssetsComponents.Directory + "bootstrap-colorpicker/css/", "20160205");
//        public static FileVersionHelper JsColorpickerColor = new FileVersionHelper("colorpicker-color.js", DirAssetsComponents.Directory + "bootstrap-colorpicker/js/", "20160205");
//        public static FileVersionHelper JsColorpicker = new FileVersionHelper("colorpicker.js", DirAssetsComponents.Directory + "bootstrap-colorpicker/js/", "20160205");
//        public static FileVersionHelper JsColorpickerDocs = new FileVersionHelper("docs.js", DirAssetsComponents.Directory + "bootstrap-colorpicker/js/", "20160205");
//        // Particle
//        public static FileVersionHelper CssParticles = new FileVersionHelper("particlesjs.css", DirAssetsComponents.Directory + "particles/", "20160205");
//        public static FileVersionHelper JsParticles = new FileVersionHelper("particles.min.js", DirAssetsComponents.Directory + "particles/", "20160205");
//        //no more tables
//        public static FileVersionHelper CssNoMoreTables = new FileVersionHelper("no-more-tables.css", DirAssetsComponents.Directory + "no-more-tables/", "20160205");
//        //css browser window
//        public static FileVersionHelper CssBrowserWindowDesktop = new FileVersionHelper("desktop-browser-window.css", DirAssetsComponents.Directory + "browser-window/desktop/", "20160205");
//        public static FileVersionHelper CssBrowserWindowMobile = new FileVersionHelper("mobile-browser-window.css", DirAssetsComponents.Directory + "browser-window/mobile/", "20160205");
//        // Prettify
//        public static FileVersionHelper JsRunPrettify = new FileVersionHelper(null, null, null, "https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js");
//        //Ion RangeSliders
//        public static FileVersionHelper JsIonRangeslider = new FileVersionHelper("ion.rangeSlider.min.js", DirAssetsComponents.Directory + "ion-rangeslider/js/", "20160205");
//        public static FileVersionHelper CssIonRangeslider = new FileVersionHelper("ion.rangeSlider.css", DirAssetsComponents.Directory + "ion-rangeslider/css/", "20160205");
//        public static FileVersionHelper CssIonRangesliderSkinNice = new FileVersionHelper("ion.rangeSlider.skinNice.css", DirAssetsComponents.Directory + "ion-rangeslider/css/", "20160205");
//        public static FileVersionHelper CssIonRangesliderSkinLeadGrid = new FileVersionHelper("ion.rangeSlider.skinLeadGrid.css", DirAssetsComponents.Directory + "ion-rangeslider/css/", "20160205");
//        //Jquery alphanum
//        public static FileVersionHelper JsJqueryAlphanum = new FileVersionHelper("jquery.alphanum.js", DirAssetsComponents.Directory + "jquery.alphanum/", "20161026");
//        // intro.js
//        public static FileVersionHelper JsIntrojs = new FileVersionHelper("intro.min.js", DirAssetsComponents.Directory + "intro.js/js/", "20161216");
//        public static FileVersionHelper CssIntrojs = new FileVersionHelper("introjs.min.css", DirAssetsComponents.Directory + "intro.js/css/", "20161216");
//        public static FileVersionHelper CssIntrojsTheme = new FileVersionHelper("introjs-custom.css", DirAssetsComponents.Directory + "intro.js/css/themes/", "20161216");
//        // JSTree
//        public static FileVersionHelper JsMetronicJsTreeMin = new FileVersionHelper("jstree.min.js", DirAssetsComponents.Directory + "vakata-jstree-3.3.8/dist/", "20190515");
//        public static FileVersionHelper JsMetronicJsTree = new FileVersionHelper("jstree.js", DirAssetsComponents.Directory + "vakata-jstree-3.3.8/dist/", "20190515");

//        #endregion

//        #region Jack Components (assets/js/common)

//        public static FileVersionHelper JsSolutionCommon = new FileVersionHelper("solution-common.js", DirAssetsJs.Directory + "common/", "20161026");
//        public static FileVersionHelper JqueryValidationFixes = new FileVersionHelper("jquery-validation-fixes.js", DirAssetsJs.Directory + "common/", "20170914");
//        public static FileVersionHelper JsEnableDecimal = new FileVersionHelper("enable-decimal.js", DirAssetsJs.Directory + "common/", "20160205");
//        public static FileVersionHelper JsEnableCurrency = new FileVersionHelper("enable-currency.js", DirAssetsJs.Directory + "common/", "20170122");
//        public static FileVersionHelper JsEnableDate = new FileVersionHelper("enable-date.js", DirAssetsJs.Directory + "common/", "20190504");
//        public static FileVersionHelper JsEnableDatetime = new FileVersionHelper("enable-datetime.js", DirAssetsJs.Directory + "common/", "20160205");
//        public static FileVersionHelper JsEnableInt = new FileVersionHelper("enable-int.js", DirAssetsJs.Directory + "common/", "20160205");

//        public static FileVersionHelper JsEnableCpf = new FileVersionHelper("enable-cpf.js", DirAssetsJs.Directory + "common/", "20160205");
//        public static FileVersionHelper JsEnableCnpj = new FileVersionHelper("enable-cnpj.js", DirAssetsJs.Directory + "common/", "20160205");
//        public static FileVersionHelper JsEnablePhone = new FileVersionHelper("enable-phone.js", DirAssetsJs.Directory + "common/", "20160205");
//        public static FileVersionHelper JsEnableZipcode = new FileVersionHelper("enable-zipcode.js", DirAssetsJs.Directory + "common/", "20160205");

//        public static FileVersionHelper JsMaxLengthAutoValidation = new FileVersionHelper("max-length-auto-validation.js", DirAssetsJs.Directory + "common/", "20160205");
//        public static FileVersionHelper JsShowAlert = new FileVersionHelper("show-alert.js", DirAssetsJs.Directory + "common/", "20160711");
//        public static FileVersionHelper JsSelectText = new FileVersionHelper("select-text.js", DirAssetsJs.Directory + "common/", "20160229");

//        #endregion

//        #region LeadGrid Pages (assets/js and assets/css)

//        // Layout
//        public static FileVersionHelper CssMetronicJack = new FileVersionHelper("metronic-jack.css", DirAssetsCss.Directory, "20180914");
//        // Login / Register / Remember Password
//        public static FileVersionHelper CssAccountsLoginSoft = new FileVersionHelper("login-soft.css", DirAssetsCss.Directory + "accounts/", "20160712");
//        public static FileVersionHelper CssAccountsSignUp = new FileVersionHelper("sign-up.css", DirAssetsCss.Directory + "accounts/", "20160223");
//        public static FileVersionHelper JsAccountsRegister = new FileVersionHelper("register.js", DirAssetsJs.Directory + "accounts/", "20181113");
//        public static FileVersionHelper JsAccountsLogin = new FileVersionHelper("login.js", DirAssetsJs.Directory + "accounts/", "20160317");
//        public static FileVersionHelper JsAccountsForgotPassword = new FileVersionHelper("forgot-password.js", DirAssetsJs.Directory + "accounts/", "20160317");
//        public static FileVersionHelper JsAccountsResetPassword = new FileVersionHelper("reset-password.js", DirAssetsJs.Directory + "accounts/", "20160317");
//        // Setup wizard
//        public static FileVersionHelper CssSetupWizard = new FileVersionHelper("setup-wizard.css", DirAssetsCss.Directory + "setup-wizard/", "20160205");
//        public static FileVersionHelper CssStepFormWizardThemeSea = new FileVersionHelper("step-form-wizard-sea.css", DirAssetsCss.Directory + "setup-wizard/", "20160205");
//        public static FileVersionHelper JsSetupWizardStep1 = new FileVersionHelper("wizard-step1.js", DirAssetsJs.Directory + "setup-wizard/", "20160712");
//        // Get started
//        public static FileVersionHelper JsGetStarted = new FileVersionHelper("getstarted.js", DirAssetsJs.Directory + "getstarted/", "20170608");
//        // Sites
//        public static FileVersionHelper JsSiteBlock = new FileVersionHelper("site-block.js", DirAssetsJs.Directory + "sites/", "20160220");
//        public static FileVersionHelper JsSiteLogo = new FileVersionHelper("site-logo.js", DirAssetsJs.Directory + "sites/", "20160715");
//        public static FileVersionHelper JsSiteCodes = new FileVersionHelper("site-codes.js", DirAssetsJs.Directory + "sites/", "20170718");
//        public static FileVersionHelper JsSiteSettings = new FileVersionHelper("site-settings.js", DirAssetsJs.Directory + "sites/", "20190517");
//        public static FileVersionHelper JsSiteCategories = new FileVersionHelper("site-categories.js", DirAssetsJs.Directory + "sites/", "20160902");
//        public static FileVersionHelper JsSiteBillingInfo = new FileVersionHelper("site-billinginfo.js", DirAssetsJs.Directory + "sites/", "20170110");
//        public static FileVersionHelper JsSiteInvoice = new FileVersionHelper("site-invoice.js", DirAssetsJs.Directory + "sites/", "20161116");
//        public static FileVersionHelper JsTour = new FileVersionHelper("site-tour-index.js", DirAssetsJs.Directory + "sites/", "20170608");
//        // Site & organizations management
//        public static FileVersionHelper JsOrganizationsSites = new FileVersionHelper("sites.js", DirAssetsJs.Directory + "organizations/", "20170112");
//        // Organization Invite User
//        public static FileVersionHelper JsOrganizationsUsers = new FileVersionHelper("users.js", DirAssetsJs.Directory + "organizations/", "20161113");
//        // Leads
//        public static FileVersionHelper CssLeads = new FileVersionHelper("leads.css", DirAssetsCss.Directory + "leads/", "20160205");
//        public static FileVersionHelper LeadsList = new FileVersionHelper("leads-list.js", DirAssetsJs.Directory + "leads/", "20170419");
//        // Buy Lead Credits
//        public static FileVersionHelper JsLeadsBuyCredit = new FileVersionHelper("leads-buy-credit.js", DirAssetsJs.Directory + "leads/", "20170608");
//        public static FileVersionHelper JsLeadsBuyCreditRecurrent = new FileVersionHelper("leads-buy-credit-recurrent.js", DirAssetsJs.Directory + "leads/", "20170207");
//        public static FileVersionHelper JsLeadsBuyCreditTransactions = new FileVersionHelper("leads-buy-credit-transactions.js", DirAssetsJs.Directory + "leads/", "20170608");
//        // Ofers
//        public static FileVersionHelper JsOffers = new FileVersionHelper("offers.js", DirAssetsJs.Directory + "offers/", "20170116");
//        // Widgets
//        public static FileVersionHelper JsWidgetList = new FileVersionHelper("widgets-list.js", DirAssetsJs.Directory + "widgets/", "20161013");
//        public static FileVersionHelper JsWidgetCustomization = new FileVersionHelper("widget-customization.js", DirAssetsJs.Directory + "widgets/", "20160712");
//        public static FileVersionHelper JsWidgetSelectTemplate = new FileVersionHelper("widget-select-template.js", DirAssetsJs.Directory + "widgets/", "20161013");
//        public static FileVersionHelper CssWidgetTemplateSelection = new FileVersionHelper("widget-template-selection.css", DirAssetsCss.Directory + "widgets/", "20160309");
//        // Errors
//        public static FileVersionHelper CssMetronicError = new FileVersionHelper("error.css", DirMetronicPagesCss.Directory, "20160205");
//        // Third party services
//        public static FileVersionHelper JsDataIntegration = new FileVersionHelper("data-integration.js", DirAssetsJs.Directory + "data-integration/", "20160314");
//        // User
//        public static FileVersionHelper JsEmailPreferences = new FileVersionHelper("email-preferences.js", DirAssetsJs.Directory + "user/", "20160803");
//        // Plans
//        public static FileVersionHelper CssSitePlans = new FileVersionHelper("plans.css", DirAssetsCss.Directory + "plans/", "20161207");
//        public static FileVersionHelper JsSitePlans = new FileVersionHelper("site-plans.js", DirAssetsJs.Directory + "sites/", "20161206");
//        // Functionality Measurements
//        public static FileVersionHelper JsFunctionalityMeasurements = new FileVersionHelper("functionality-measurements.js", DirAssetsJs.Directory + "functionality-measurements/", "20161221");
//        // Payment methods
//        public static FileVersionHelper JsPaymentMethods = new FileVersionHelper("paymentmethods.js", DirAssetsJs.Directory + "paymentmethods/", "20170207");
//        public static FileVersionHelper CssPaymentMethods = new FileVersionHelper("paymentmethods.css", DirAssetsCss.Directory + "billing/", "20170207");
//        // Badges
//        public static FileVersionHelper JsSiteBadges = new FileVersionHelper("site-badges.js", DirAssetsJs.Directory + "settings/", "20170307");
//        #endregion
//    }
//}
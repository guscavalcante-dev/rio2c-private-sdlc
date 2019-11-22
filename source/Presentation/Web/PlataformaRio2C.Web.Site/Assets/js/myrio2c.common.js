// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 08-09-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-22-2019
// ***********************************************************************
// <copyright file="myrio2c.common.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var MyRio2cCommon = function () {

    // Global Variables --------------------------------------------------------------------------
    var globalVariables = new Object();
    globalVariables.userInterfaceLanguage = '';
    globalVariables.userInterfaceLanguageUppercase = '';
    globalVariables.editionUrlCode = '';
    globalVariables.bucket = '';

    var setGlobalVariables = function (userInterfaceLanguage, editionUrlCode, bucket) {
        globalVariables.userInterfaceLanguage = userInterfaceLanguage;
        globalVariables.userInterfaceLanguageUppercade = MyRio2cCommon.getCultureUppercase(userInterfaceLanguage);
        globalVariables.editionUrlCode = editionUrlCode;
        globalVariables.bucket = bucket;
    };

    var getGlobalVariables = function () {
        return globalVariables;
    };

    var getGlobalVariable = function (key) {
        if (!MyRio2cCommon.hasProperty(globalVariables, key)) {
            return null;
        }

        return globalVariables[key];
    };

    // Layout -------------------------------------------------------------------------------------
    var enableAjaxForbiddenCatch = function () {
        $(document).ajaxError(function (e, xhr) {
            if (xhr.status === 401) {
                window.location.reload();
            }
            else if (xhr.status === 403) {
                var response = $.parseJSON(xhr.responseText);

                if (!MyRio2cCommon.isNullOrEmpty(response.redirect)) {
                    window.location = response.redirect;
                }
            }
        });
    };

    var disableMetronicScripts = function () {
        $('#kt_aside_menu, #kt_header_menu').unbind('click');
    };

    var fixSelect2Modal = function () {
        $.fn.modal.Constructor.prototype.enforceFocus = function () { };
    };

    var enablePrototypes = function () {
        Array.prototype.distinct = function () {
            var unique = (value, index, self) => {
                return self.indexOf(value) === index;
            };

            return this.filter(unique);
        };
    };

    //var fixCkEditorValidation = function () {
    //    if (typeof (CKEDITOR) === "undefined") {
    //        return;
    //    }

    //    CKEDITOR.on('instanceReady', function () {
    //        $.each(CKEDITOR.instances, function (instance) {
    //            CKEDITOR.instances[instance].on("change", function (e) {
    //                for (instance in CKEDITOR.instances) {
    //                    CKEDITOR.instances[instance].updateElement();
    //                }
    //            });
    //        });
    //    });
    //};

    var initScroll = function () {
        $('.rio2c-scroll').not('.rio2c-scroll-enabled').each(function () {
            var el = $(this);
            KTUtil.scrollInit(this, {
                //mobileNativeScroll: true,
                handleWindowResize: true,
                windowScroll: false,
                rememberPosition: (el.data('remember-position') === 'true' ? true : false),
                height: function () {
                    if (KTUtil.isInResponsiveRange('tablet-and-mobile') && el.data('mobile-height')) {
                        return el.data('mobile-height');
                    } else {
                        return el.data('height');
                    }
                }
            });

            el.addClass('rio2c-scroll-enabled');
        });
    };

    var enablePaginationBlockUi = function () {
        $(document).on('click', '.kt-pagination .kt-pagination__links a', function() {
            MyRio2cCommon.block();
        });
    };

    var extendGlobalValidations = function () {
        if (typeof ($.validator) === 'undefined') {
            return;
        }

        // extend jquery range validator to work for required checkboxes
        var defaultRangeValidator = $.validator.methods.range;
        $.validator.methods.range = function (value, element, param) {
            if (element.type === 'checkbox') {
                // if it's a checkbox return true if it is checked
                return element.checked;
            }
            else {
                // otherwise run the default validation function
                return defaultRangeValidator.call(this, value, element, param);
            }
        };

        // Required if one not empty and other empty
        jQuery.validator.addMethod("requiredifonenotemptyandotherempty", function (value, element, params) {
            var dependentPropertyNotEmpty = foolproof.getId(element, params["dependentpropertynotempty"]);
            var dependentPropertyNotEmptyValue = $('#' + dependentPropertyNotEmpty).val();
            if (MyRio2cCommon.isNullOrEmpty(dependentPropertyNotEmptyValue)) {
                return true;
            }

            var dependentPropertyEmpty = foolproof.getId(element, params["dependentpropertyempty"]);
            var dependentPropertyEmptyValue = $('#' + dependentPropertyEmpty).val();
            if (!MyRio2cCommon.isNullOrEmpty(dependentPropertyEmptyValue) || !MyRio2cCommon.isNullOrEmpty(value)) {
                return true;
            }

            return false;
        });

        $.validator.unobtrusive.adapters.add("requiredifonenotemptyandotherempty", ["dependentpropertynotempty", "dependentpropertyempty"], function (options) {
            var value = {
                dependentpropertynotempty: options.params.dependentpropertynotempty,
                dependentpropertyempty: options.params.dependentpropertyempty
            };

            options.rules["requiredifonenotemptyandotherempty"] = value;
            if (options.message) {
                options.messages["requiredifonenotemptyandotherempty"] = options.message;
            }
        });

        // Required if one with value and other empty
        jQuery.validator.addMethod("requiredifonewithvalueandotherempty", function (value, element, params) {
            var dependentPropertyWithValue = foolproof.getId(element, params["dependentpropertywithvalue"]);
            var dependentPropertyValue = $('#' + dependentPropertyWithValue).val();
            if (MyRio2cCommon.isNullOrEmpty(dependentPropertyValue)) {
                return true;
            }

            if (dependentPropertyValue != params["dependentpropertyvalue"]) {
                return true;
            }

            var dependentPropertyEmpty = foolproof.getId(element, params["dependentpropertyempty"]);
            var dependentPropertyEmptyValue = $('#' + dependentPropertyEmpty).val();
            if (!MyRio2cCommon.isNullOrEmpty(dependentPropertyEmptyValue) || !MyRio2cCommon.isNullOrEmpty(value)) {
                return true;
            }

            return false;
        });

        $.validator.unobtrusive.adapters.add("requiredifonewithvalueandotherempty", ["dependentpropertywithvalue", "dependentpropertyvalue", "dependentpropertyempty"], function (options) {
            var value = {
                dependentpropertywithvalue: options.params.dependentpropertywithvalue,
                dependentpropertyvalue: options.params.dependentpropertyvalue,
                dependentpropertyempty: options.params.dependentpropertyempty
            };

            options.rules["requiredifonewithvalueandotherempty"] = value;
            if (options.message) {
                options.messages["requiredifonewithvalueandotherempty"] = options.message;
            }
        });

        // Required if one with value and other with value
        jQuery.validator.addMethod("requiredimageoptional", function (value, element, params) {
            var isRequired = foolproof.getId(element, "IsRequired");
            var isRequiredValue = $('#' + isRequired).val();
            if (isRequiredValue !== "True") {
                return true;
            }

            var imageFile = foolproof.getId(element, "ImageFile");
            var imageFileValue = $('#' + imageFile).val();
            if (!MyRio2cCommon.isNullOrEmpty(imageFileValue)) {
                return true;
            }

            var imageUploadDate = foolproof.getId(element, "ImageUploadDate");
            var imageUploadDateValue = $('#' + imageUploadDate).val();
            if (MyRio2cCommon.isNullOrEmpty(imageUploadDateValue)) {
                return false;
            }

            var isImageDeleted = foolproof.getId(element, "IsImageDeleted");
            var isImageDeletedValue = $('#' + isImageDeleted).val();
            if (isImageDeletedValue === "True") {
                return false;
            }

            return true;
        });

        $.validator.unobtrusive.adapters.add("requiredimageoptional", [], function (options) {
            var value = {
                //dependentproperty1withvalue: options.params.dependentproperty1withvalue,
                //dependentproperty1value: options.params.dependentproperty1value,
                //dependentproperty2withvalue: options.params.dependentproperty2withvalue,
                //dependentproperty2value: options.params.dependentproperty2value
            };

            options.rules["requiredimageoptional"] = value;
            if (options.message) {
                options.messages["requiredimageoptional"] = options.message;
            }
        });

        // Check if is a valid company number
        jQuery.validator.addMethod("validcompanynumber", function (value, element, params) {
            if (typeof (MyRio2cCompanyDocument) === 'undefined') {
                return true;
            }

            return MyRio2cCompanyDocument.validate($('#CountryUid').find(":selected").data("country-code"), $('#Document').val());
        });

        $.validator.unobtrusive.adapters.add("validcompanynumber", [], function (options) {
            var value = {
                //dependentproperty1withvalue: options.params.dependentproperty1withvalue,
                //dependentproperty1value: options.params.dependentproperty1value,
                //dependentproperty2withvalue: options.params.dependentproperty2withvalue,
                //dependentproperty2value: options.params.dependentproperty2value
            };

            options.rules["validcompanynumber"] = value;
            if (options.message) {
                options.messages["validcompanynumber"] = options.message;
            }
        });

        //// Validate ckeditor max characters
        //jQuery.validator.addMethod("ckeditormaxchars", function (value, element, params) {
        //    if (typeof (CKEDITOR) === 'undefined') {
        //        return true;
        //    }

        //    var ckeditorInstance = CKEDITOR.instances[element.id];
        //    if (typeof (ckeditorInstance) === 'undefined') {
        //        return true;
        //    }

        //    var maxChars = params["maxchars"];
        //    if (MyRio2cCommon.isNullOrEmpty(maxChars)) {
        //        return true;
        //    }

        //    if (ckeditorInstance.wordCount.charCount <= maxChars) {
        //        return true;
        //    }

        //    return false;
        //});

        //$.validator.unobtrusive.adapters.add("ckeditormaxchars", ["maxchars"], function (options) {
        //    var value = {
        //        maxchars: options.params.maxchars
        //    };

        //    options.rules["ckeditormaxchars"] = value;
        //    if (options.message) {
        //        options.messages["ckeditormaxchars"] = options.message;
        //    }
        //});
    };

    var enableAtLeastOnCheckboxByNameValidation = function (formIdOrClass) {
        $.validator.addMethod('require-one',
            function (value, element) {

                // Get the name without array[]
                var name = element.name;
                if (name.includes('[')) {
                    var nameSplit = name.split('[');
                    name = nameSplit[0];
                }

                var dataId = $(element).attr("data-id");
                var dataValMsgFor = $('[data-valmsg-for="' + name + '"]');

                if (!MyRio2cCommon.isNullOrEmpty(dataId)) {
                    var dataValMsgForDataId = $('[data-valmsg-for="' + dataId + '"]');
                    if (dataValMsgFor.length <= 0 && dataValMsgForDataId.length > 0) {
                        dataValMsgFor = dataValMsgForDataId;
                    }

                    if ($('[data-id="' + $(element).attr("data-id") + '"].require-one:checked').length > 0) {
                        dataValMsgFor.html('');
                        dataValMsgFor.addClass('field-validation-valid');
                        dataValMsgFor.removeClass('field-validation-error');

                        return true;
                    }
                    else {
                        dataValMsgFor.html('<span for="' + name + '" generated="true" class="">' + labels.selectAtLeastOneOption + '</span>');
                        dataValMsgFor.removeClass('field-validation-valid');
                        dataValMsgFor.addClass('field-validation-error');

                        return false;
                    }
                }
                else {
                    if ($('.require-one:checked').length > 0) {
                        dataValMsgFor.html('');
                        dataValMsgFor.addClass('field-validation-valid');
                        dataValMsgFor.removeClass('field-validation-error');

                        return true;
                    }
                    else {
                        dataValMsgFor.html('<span for="' + name + '" generated="true" class="">' + labels.selectAtLeastOneOption + '</span>');
                        dataValMsgFor.removeClass('field-validation-valid');
                        dataValMsgFor.addClass('field-validation-error');

                        return false;
                    }
                }
            }, labels.selectAtLeastOneOption);
    };

    var validateRequireOneGroup = function () {
        var isValid = true;

        $(".require-one-group").each(function (index, element) {

            var dataId = $(element).data("id");
            if (MyRio2cCommon.isNullOrEmpty(dataId)) {
                return;
            }

            if ($('[data-id="' + dataId + '"].require-one-item:checked').length > 0 === false) {
                $('[data-valmsg-for="' + dataId + '"]').html('<span for="' + dataId + '" generated="true" class="">' + labels.selectAtLeastOneOption + '</span>');
                $('[data-valmsg-for="' + dataId + '"]').removeClass('field-validation-valid');
                $('[data-valmsg-for="' + dataId + '"]').addClass('field-validation-error');

                isValid = false;
            }
            else {
                $('[data-valmsg-for="' + dataId + '"]').html('');
                $('[data-valmsg-for="' + dataId + '"]').addClass('field-validation-valid');
                $('[data-valmsg-for="' + dataId + '"]').removeClass('field-validation-error');
            }
        });

        // Enable checkbox change on first submit
        $(".require-one-item").not('.change-event-enabled').on('change', function () {
            validateRequireOneGroup();
        });

        $(".require-one-item").addClass('change-event-enabled');

        return isValid;
    };

    // General ------------------------------------------------------------------------------------
    var hasProperty = function (obj, key) {
        return key.split(".").every(function (x) {
            if (typeof obj !== "object" || obj === null || !(x in obj)) {
                return false;
            }

            obj = obj[x];

            return true;
        });
    };

    var isNullOrEmpty = function (value) {
        if (typeof (value) === 'undefined' || value == null || value === '') {
            return true;
        }

        return false;
    };

    var getUrlWithCultureAndEdition = function (url) {
        var urlPrefix = '';

        if (!isNullOrEmpty(globalVariables.userInterfaceLanguage)) {
            if (url.includes(globalVariables.userInterfaceLanguage)) {
                url.replace(globalVariables.userInterfaceLanguage + '/', '');
            }

            urlPrefix = '/' + globalVariables.userInterfaceLanguage;
        }

        if (!isNullOrEmpty(globalVariables.editionUrlCode)) {
            if (url.includes(globalVariables.editionUrlCode)) {
                url.replace(globalVariables.editionUrlCode + '/', '');
            }

            urlPrefix += '/' + globalVariables.editionUrlCode;
        }

        return urlPrefix + url;
    };

    var getCultureUppercase = function (culture) {
        if (isNullOrEmpty(culture)) {
            return '';
        }

        var split = culture.split('-');
        if (split.length === 1) {
            return culture;
        }
        else {
            return split[0] + '-' + split[1].toUpperCase();
        }
    };

    var convertJsonToCsv = function (json) {
        if (isNullOrEmpty(json)) {
            return '';
        }

        if (json.length === 0) {
            return '';
        }

        var fields = Object.keys(json[0]);
        var replacer = function (key, value) { return value === null ? '' : value }
        var csv = json.map(function (row) {
            return fields.map(function (fieldName) {
                return JSON.stringify(row[fieldName], replacer);
            }).join(',');
        });

        csv.unshift(fields.join(',')); // add header column
        return csv.join('\r\n');
    };

    // Forms --------------------------------------------------------------------------------------
    var enableFormValidation = function (options) {
        extendGlobalValidations();

        if (!hasProperty(options, 'formIdOrClass') || isNullOrEmpty(options.formIdOrClass)) {
            return;
        }

        var enableHiddenInputsValidation = false;
        if (hasProperty(options, 'enableHiddenInputsValidation') && options.enableHiddenInputsValidation) {
            enableHiddenInputsValidation = options.enableHiddenInputsValidation;
        }

        var enableMaxlength = false;
        if (hasProperty(options, 'enableMaxlength') && options.enableMaxlength) {
            enableMaxlength = options.enableMaxlength;
        }

        $(options.formIdOrClass).removeData('validator');
        $(options.formIdOrClass).removeData('unobtrusiveValidation');
        $.validator.unobtrusive.parse(options.formIdOrClass);

        if (enableHiddenInputsValidation === true) {
            var validator = $(options.formIdOrClass).data('validator');
            if (undefined != validator) {
                validator.settings.ignore = "";
            }
        }

        if (enableMaxlength === true) {
            enableInputMaxlength();
        }
    };

    var enableSelect2 = function (options) {
        if (isNullOrEmpty(options)) {
            options = new Object();
        }

        if (!hasProperty(options, 'inputIdOrClass') || isNullOrEmpty(options.inputIdOrClass)) {
            options.inputIdOrClass = '.enable-select2';
        }

        $(options.inputIdOrClass).select2({
            language: globalVariables.userInterfaceLanguageUppercade,
            width: '100%'
        });
    };

    var submitForm = function (formIdOrClass) {
        if (MyRio2cCommon.isNullOrEmpty(formIdOrClass)) {
            return;
        }

        var validator = $(formIdOrClass).validate();

        if ($(formIdOrClass).valid()) {
            MyRio2cCommon.block();
            $(formIdOrClass).submit();
        }
        else {
            validator.focusInvalid();
        }
    };

    // Enable input maxlength ---------------------------------------------------------------------
    var enableInputMaxlength = function () {
        $('input[data-val-length-max],textarea[data-val-length-max]').not('.maxlength-enabled').each(function () {

            var maxlength = $(this).data('val-length-max');
            var allowOverMax = $(this).hasClass('maxlength-allowovermax');
            if (MyRio2cCommon.isNullOrEmpty(allowOverMax)) {
                allowOverMax = false;
            }

            if (!MyRio2cCommon.isNullOrEmpty(maxlength)) {
                $(this).maxlength({
                    customMaxAttribute: maxlength + '',
                    alwaysShow: true,
                    validate: !allowOverMax,
                    appendToParent: true,
                    message: labels.maxlengthCounterMessage,
                    warningClass: "kt-badge kt-badge--success kt-badge--rounded kt-badge--inline",
                    limitReachedClass: "kt-badge kt-badge--danger kt-badge--rounded kt-badge--inline",
                });

                $(this).addClass('maxlength-enabled');
            }
        });
    };

    // Hide/Show Element --------------------------------------------------------------------------
    var hide = function (element) {
        if (isNullOrEmpty(element)) {
            return;
        }

        element.addClass('d-none');
    };

    var show = function (element) {
        if (isNullOrEmpty(element)) {
            return;
        }

        element.removeClass('d-none');
    };

    var changeElementsVisibilityByDataId = function (options) {
        if (!hasProperty(options, 'dataId') || isNullOrEmpty(options.dataId)) {
            return false;
        }

        var dataIdElement = $('[data-id="' + options.dataId + '"]');
        if (isNullOrEmpty(dataIdElement)) {
            return false;
        }

        if (!hasProperty(options, 'hideElementIdOrClass') || !isNullOrEmpty(options.hideElementIdOrClass)) {
            var hideElement = dataIdElement.find(options.hideElementIdOrClass);
            if (!isNullOrEmpty(hideElement)) {
                hide(hideElement);
            }
        }

        if (!hasProperty(options, 'showElementIdOrClass') || !isNullOrEmpty(options.showElementIdOrClass)) {
            var showElement = dataIdElement.find(options.showElementIdOrClass);
            if (!isNullOrEmpty(showElement)) {
                show(showElement);
            }
        }

        return false;
    };

    var enableFieldEdit = function (options) {
        if (!hasProperty(options, 'dataId') || isNullOrEmpty(options.dataId)) {
            return false;
        }

        changeElementsVisibilityByDataId({ dataId: options.dataId, hideElementIdOrClass: '.view', showElementIdOrClass: '.edit' });

        return false;
    };

    var disableFieldEdit = function (options) {
        if (!hasProperty(options, 'dataId') || isNullOrEmpty(options.dataId)) {
            return false;
        }

        changeElementsVisibilityByDataId({ dataId: options.dataId, hideElementIdOrClass: '.edit', showElementIdOrClass: '.view' });

        return false;
    };

    // Block/unblock UI ---------------------------------------------------------------------------
    var block = function (options) {
        var idOrClass = 'body';

        if (hasProperty(options, 'idOrClass')) {
            idOrClass = options.idOrClass;
        }

        KTApp.block(idOrClass);

        if (hasProperty(options, 'isModal') && options.isModal) {
            $(".blockUI.blockOverlay").addClass("blockUIModal");
            $(".blockUI.blockMsg.blockPage").addClass("blockUIModal");
        }
    };

    var unblock = function (options) {
        var idOrClass = 'body';

        if (hasProperty(options, 'idOrClass')) {
            idOrClass = options.idOrClass;
        }

        KTApp.unblock(idOrClass);
    };

    // Handle ajax return -------------------------------------------------------------------------
    var handleAjaxReturn = function (options) {

        if (!hasProperty(options, 'data')) {
            return null;
        }

        var data = options.data;

        // Undefined Error (no data.status)
        if (!hasProperty(data, 'status') || isNullOrEmpty(data.status)) {
            data.status = 'error';
        }

        if (data.status === "success") {
            // Redirect without alert message
            if (hasProperty(data, 'redirectOnly') && !isNullOrEmpty(data.redirectOnly)) {
                window.location.replace(data.redirectOnly);
                return null;
            }

            // Redirect but first show alert message
            if (hasProperty(data, 'redirect') && !isNullOrEmpty(data.redirect)) {
                MyRio2cCommon.showAlert({
                    message: data.message + " " + redirectMessage,
                    messageType: data.status,
                    isFixed: data.isFixed,
                    callbackOnHidden: function () {
                        window.location.replace(data.redirect);
                    }
                });

                return null;
            }

            // Submit search form
            if (hasProperty(options, 'searchFormIdOrClass') && !isNullOrEmpty(options.searchFormIdOrClass)) {
                MyRio2cCommon.showAlert({
                    message: data.message + " " + redirectMessage,
                    messageType: data.status,
                    isFixed: data.isFixed,
                    callbackOnHidden: function () {
                        $(searchFormIdOrClass).submit();
                    }
                });

                return null;
            }

            //// Hide modal
            //if (typeof modalDivIdOrClass !== "undefined" && modalDivIdOrClass != null && modalDivIdOrClass != '') {
            //    MyRio2cCommon.showAlert(data.message, data.status, data.isFixed);
            //    $(modalDivIdOrClass).modal('hide');

            //    if (typeof callbackSuccess !== "undefined" && callbackSuccess != null && callbackSuccess != '') {
            //        callbackSuccess();
            //    }
            //    return;
            //}

            // Replace pages
            if (hasProperty(data, 'pages') && !isNullOrEmpty(data.pages)) {
                MyRio2cCommon.showAlert({ message: data.message, messageType: data.status, isFixed: data.isFixed });

                $.each(data.pages, function (key, value) {
                    $(value.divIdOrClass).html(value.page);
                });

                //if (typeof callbackSuccess !== "undefined" && callbackSuccess != null && callbackSuccess !== '') {
                if (hasProperty(options, 'onSuccess')) {
                    return options.onSuccess(data);
                }

                return null;
            }

            if (hasProperty(options, 'formDivIdOrClass') && !isNullOrEmpty(options.formDivIdOrClass)) {
                MyRio2cCommon.showAlert({ message: data.message, messageType: data.status, isFixed: data.isFixed });

                if (typeof data.page !== "undefined" && data.page != null && data.page !== '') {
                    $(options.formDivIdOrClass).html(data.page);
                }
                //else {
                //    $(formDivIdOrClass).html('');
                //}

                if (hasProperty(options, 'onSuccess')) {
                    return options.onSuccess(data);
                }

                return null;
            }

            if (hasProperty(options, 'onSuccess')) {
                MyRio2cCommon.showAlert({ message: data.message, messageType: data.status, isFixed: data.isFixed });

                return options.onSuccess();
            }

            MyRio2cCommon.showAlert({ message: data.message, messageType: data.status, isFixed: data.isFixed });

            return null;
        }

        // Defined Error (data.status == "error)
        if (data.status === "error") {
            // User is not logged in
            if (hasProperty(data, 'redirect') && !isNullOrEmpty(data.redirect)) {
                MyRio2cCommon.showAlert({
                    message: data.message + " " + redirectMessage,
                    messageType: data.status,
                    isFixed: data.isFixed,
                    callbackOnHidden: function () {
                        if (data.redirect.toLowerCase() === "/accounts/login")
                            window.location.replace(data.redirect +
                                "?ReturnUrl=" +
                                encodeURIComponent(window.location.pathname) +
                                encodeURIComponent(window.location.search));
                        else
                            window.location.replace(data.redirect);
                    }
                });

                return null;
            }

            if (hasProperty(data, 'pages') && !isNullOrEmpty(data.pages)) {
                MyRio2cCommon.showAlert({ message: data.message, messageType: data.status, isFixed: data.isFixed });

                $.each(data.pages, function (key, value) {
                    $(value.divIdOrClass).html(value.page);
                });

                //if (typeof callbackSuccess !== "undefined" && callbackSuccess != null && callbackSuccess !== '') {
                if (hasProperty(options, 'onError')) {
                    return options.onError(data);
                }

                return null;
            }

            // Form with error
            if (hasProperty(options, 'formDivIdOrClass') && !isNullOrEmpty(options.formDivIdOrClass) && hasProperty(data, 'page') && !isNullOrEmpty(data.page)) {
                MyRio2cCommon.showAlert({ message: data.message, messageType: data.status, isFixed: data.isFixed });

                $(options.formDivIdOrClass).html(data.page);

                if (hasProperty(options, 'onError')) {
                    return options.onError();
                }

                return null;
            }

            if (hasProperty(options, 'onError')) {
                MyRio2cCommon.showAlert({ message: data.message, messageType: data.status, isFixed: data.isFixed });

                return options.onError();
            }

            MyRio2cCommon.showAlert({ message: data.message, messageType: data.status, isFixed: data.isFixed });

            return null;
        }

        return null;
        //showAlert();
    };

    //// CKEDITOR -----------------------------------------------------------------------------------
    //var enableCkEditor = function (options) {

    //    if (!hasProperty(options, 'idOrClass')) {
    //        return;
    //    }

    //    if (isNullOrEmpty(window.CKEDITOR)) {
    //        return;
    //    }

    //    if (!hasProperty(options, 'maxCharCount')) {
    //        options.maxCharCount = 8000;
    //    }

    //    $(options.idOrClass).each(function () {
    //        var elementName = $(this).attr('name');

    //        CKEDITOR.replace($(this)[0], {
    //            customConfig: '/Content/js/ckeditor_config.js',
    //            language: globalVariables.userInterfaceLanguage,
    //            wordcount: {
    //                countBytesAsChars: false,
    //                countLineBreaks: false,
    //                // Whether or not you want to show the Paragraphs Count
    //                showParagraphs: false,

    //                // Whether or not you want to show the Word Count
    //                showWordCount: false,

    //                // Whether or not you want to show the Char Count
    //                showCharCount: true,

    //                // Whether or not you want to count Spaces as Chars
    //                countSpacesAsChars: true,

    //                // Whether or not to include Html chars in the Char Count
    //                countHTML: false,

    //                // Maximum allowed Word Count, -1 is default for unlimited
    //                maxWordCount: -1,

    //                // Maximum allowed Char Count, -1 is default for unlimited
    //                maxCharCount: options.maxCharCount,

    //                // Disable hardlimit to allow user to write more than the limit
    //                hardLimit: false,

    //                // Add filter to add or remove element before counting (see CKEDITOR.htmlParser.filter), Default value : null (no filter)
    //                filter: new CKEDITOR.htmlParser.filter({
    //                    elements: {
    //                        div: function (element) {
    //                            if (element.attributes.class == 'mediaembed') {
    //                                return false;
    //                            }
    //                        }
    //                    }
    //                }),

    //                // Show error when the max length limit is reached
    //                charCountGreaterThanMaxLengthEvent: function (currentLength, maxLength) {
    //                    $('[data-valmsg-for="' + elementName + '"]').html('<span for="' + name + '" generated="true" class="">' + labels.propertyBetweenLengths.replace('{0} ', '').replace('{2}', 1).replace('{1}', maxLength) + '</span>');
    //                    $('[data-valmsg-for="' + elementName + '"]').removeClass('field-validation-valid');
    //                    $('[data-valmsg-for="' + elementName + '"]').addClass('field-validation-error');
    //                },
    //                charCountLessThanMaxLengthEvent: function (currentLength, maxLength) {
    //                    if (currentLength > 0 && $('[data-valmsg-for="' + elementName + '"]').hasClass('field-validation-error')) {
    //                        $('[data-valmsg-for="' + elementName + '"]').html('');
    //                        $('[data-valmsg-for="' + elementName + '"]').addClass('field-validation-valid');
    //                        $('[data-valmsg-for="' + elementName + '"]').removeClass('field-validation-error');
    //                    }
    //                }
    //            }
    //        });
    //    });
    //};

    //var updateCkEditorElements = function () {
    //    if (isNullOrEmpty(window.CKEDITOR)) {
    //        return;
    //    }

    //    for (var instance in CKEDITOR.instances) {
    //        if (CKEDITOR.instances.hasOwnProperty(instance)) {
    //            CKEDITOR.instances[instance].updateElement();
    //        }
    //    }
    //};

    // Ajax Form ----------------------------------------------------------------------------------
    var enableAjaxForm = function (options) {

        if (!hasProperty(options, 'idOrClass')) {
            return;
        }

        var uploadFormElement = $(options.idOrClass);

        uploadFormElement.ajaxForm({
            beforeSerialize: function (form, options) {
                //MyRio2cCommon.updateCkEditorElements();
            },
            beforeSubmit: function () {
                return uploadFormElement.valid(); // TRUE when form is valid, FALSE will cancel submit
            },
            beforeSend: function () {
                MyRio2cCommon.block({ isModal: true });
            },
            uploadProgress: function (event, position, total, percentComplete) {
                //if (progressBarElement.length) {
                //    var percentVal = percentComplete + '%';
                //    bar.width(percentVal);
                //    percent.html(percentVal);
                //}
            },
            success: function (data) {
                MyRio2cCommon.handleAjaxReturn({
                    data: data,
                    // Success
                    onSuccess: function () {
                        if (!hasProperty(options, 'onSuccess')) {
                            return;
                        }

                        options.onSuccess(data);
                    },
                    // Error
                    onError: function () {
                        if (!hasProperty(options, 'onError')) {
                            return;
                        }

                        options.onError(data);
                    }
                });

                //if (progressBarElement.length) {
                //    var percentVal = '100%';
                //    bar.width(percentVal);
                //    percent.html(percentVal);
                //}
                //if (typeof data.imageLink !== 'undefined' && data.imageLink != null && data.imageLink != '' && $(imgFinalElement).length) {
                //    imgFinalElement.attr('src', data.imageLink);
                //}
                //showAlert(data.message, data.status);
                //if (typeof getPersonActivities !== 'undefined' && getPersonActivities != null) {
                //    getPersonActivities(true);
                //}
            },
            complete: function () {
                MyRio2cCommon.unblock();

                if (!hasProperty(options, 'onComplete')) {
                    return;
                }

                options.onComplete();

                //if (progressBarElement.length) {
                //    progressBarElement.addClass('hide');
                //    var percentVal = '0%';
                //    bar.width(percentVal);
                //    percent.html(percentVal);
                //}
            }
        });
    };

    // Toastr -------------------------------------------------------------------------------------
    var showAlert = function (options) {

        if (isNullOrEmpty(options)) {
            options = new Object();
        }

        if (!hasProperty(options, 'messageType')
            || (options.messageType.toLowerCase() !== 'error'
                && options.messageType.toLowerCase() !== 'success'
                && options.messageType.toLowerCase() !== 'info'
                && options.messageType.toLowerCase() !== 'warning')) {
            options.messageType = "error";
        }

        if (options.messageType.toLowerCase() === 'error' && (!hasProperty(options, 'message') || isNullOrEmpty(options.message))) {
            options.message = alertUndefinedMessage;
        }

        if (!hasProperty(options, 'message') || isNullOrEmpty(options.message)) {
            return;
        }

        if (hasProperty(options, 'message')) {
            toastr.options = {
                "closeButton": true,
                "debug": false,
                "positionClass": "toast-top-right",
                "onclick": null,
                "showDuration": "1000",
                "hideDuration": "1000",
                "timeOut": "10000",
                "extendedTimeOut": "1000",
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeIn",
                "hideMethod": "fadeOut"
            };

            if (hasProperty(options, 'callbackOnHidden') && !isNullOrEmpty(options.callbackOnHidden)) {
                toastr.options.onHidden = options.callbackOnHidden;
            }

            if (hasProperty(options, 'isFixed') && options.isFixed === true) {
                toastr.options.timeOut = 'none';
            }

            toastr[options.messageType.toLowerCase()](options.message, alertMessageTypeTranslated[options.messageType.toLowerCase()]);
        }
    };

    return {
        init: function (userInterfaceLanguage, editionUrlCode, bucket) {
            setGlobalVariables(userInterfaceLanguage, editionUrlCode, bucket);

            // Functions that need jquery
            $(function () {
                disableMetronicScripts();
                enableAjaxForbiddenCatch();
                fixSelect2Modal();
                enablePrototypes();
                //fixCkEditorValidation();
                initScroll();
                extendGlobalValidations();
                enableInputMaxlength();
            });
        },
        getGlobalVariables: function () {
            return getGlobalVariables();
        },
        getGlobalVariable: function (key) {
            return getGlobalVariable(key);
        },
        initScroll: function () {
            initScroll();
        },
        enablePaginationBlockUi: function () {
            enablePaginationBlockUi();
        },
        enableAtLeastOnCheckboxByNameValidation: function (formIdOrClass) {
            enableAtLeastOnCheckboxByNameValidation(formIdOrClass);
        },
        validateRequireOneGroup: function () {
            return validateRequireOneGroup();
        },
        hasProperty: function (obj, key) {
            return hasProperty(obj, key);
        },
        isNullOrEmpty: function (value) {
            return isNullOrEmpty(value);
        },
        enableFormValidation: function (options) {
            enableFormValidation(options);
        },
        enableSelect2: function (options) {
            enableSelect2(options);
        },
        submitForm: function (formIdOrClass) {
            submitForm(formIdOrClass);
        },
        hide: function (element) {
            hide(element);
        },
        show: function (element) {
            show(element);
        },
        changeElementsVisibilityByDataId: function (options) {
            return changeElementsVisibilityByDataId(options);
        },
        enableFieldEdit: function (options) {
            return enableFieldEdit(options);
        },
        disableFieldEdit: function (options) {
            return disableFieldEdit(options);
        },
        getUrlWithCultureAndEdition: function (url) {
            return getUrlWithCultureAndEdition(url);
        },
        getCultureUppercase: function (culture) {
            return getCultureUppercase(culture);
        },
        convertJsonToCsv: function (json) {
            return convertJsonToCsv(json);
        },
        block: function (options) {
            block(options);
        },
        unblock: function (options) {
            unblock(options);
        },
        handleAjaxReturn: function (options) {
            return handleAjaxReturn(options);
        },
        //enableCkEditor: function (options) {
        //    enableCkEditor(options);
        //},
        //updateCkEditorElements: function () {
        //    updateCkEditorElements();
        //},
        enableAjaxForm: function (options) {
            enableAjaxForm(options);
        },
        showAlert: function (options) {
            showAlert(options);
        }
    };
}();
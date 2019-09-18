// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 08-09-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-18-2019
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
                window.location = response.redirect;
            }
        });
    };

    var disableMetronicScripts = function () {
        $('#kt_aside_menu, #kt_header_menu').unbind('click');
    };

    var fixSelect2Modal = function () {
        $.fn.modal.Constructor.prototype.enforceFocus = function () { };
    };

    var fixCkEditorValidation = function () {
        if (typeof (CKEDITOR) === "undefined") {
            return;
        }

        CKEDITOR.on('instanceReady', function () {
            $.each(CKEDITOR.instances, function (instance) {
                CKEDITOR.instances[instance].on("change", function (e) {
                    for (instance in CKEDITOR.instances) {
                        CKEDITOR.instances[instance].updateElement();
                    }
                });
            });
        });
    };

    var initScroll = function () {
        $('.rio2c-scroll').each(function () {
            var el = $(this);
            KTUtil.scrollInit(this, {
                mobileNativeScroll: true,
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
    };

    var enableAtLeastOnCheckboxByNameValidation = function (formIdOrClass) {
        $.validator.addMethod('require-one', function (value, element) {
            var dataId = $(element).attr("data-id");
            if (!MyRio2cCommon.isNullOrEmpty(dataId)) {
                return $('[data-id="' + $(element).attr("data-id") + '"].require-one:checked').length > 0;
            }
            else {
                return $('.require-one:checked').length > 0;
            }
            //return $('[name="' + element.name + '"].require-one:checked').length > 0;
            //return $('.require-one:checked').length > 0;
        }, labels.selectAtLeastOneOption);

        var checkboxes = $('.require-one');
        var checkbox_names = $.map(checkboxes, function (e, i) {
            return $(e).attr("name");
        }).join(" ");

        $(formIdOrClass).validate({
            groups: {
                checks: checkbox_names
            },
            errorPlacement: function (error, element) {
                if (element.attr("type") == "checkbox") {
                    error.insertAfter(checkboxes.last());
                } 
                else {
                    error.insertAfter(element);
                }
            }
        });
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

    var enableCompanyNumberMask = function (originDropdownIdOrClass, targetInputIdOrClass) {
        if (typeof (MyRio2cInputMask) === 'undefined') {
            return;
        }

        if (isNullOrEmpty(originDropdownIdOrClass) || isNullOrEmpty(targetInputIdOrClass)) {
            return;
        }

        var element = $(originDropdownIdOrClass);
        if (typeof (element) === 'undefined') {
            return;
        }

        var companyNumberMask = element.find(":selected").data("companynumber-mask");
        if (!MyRio2cCommon.isNullOrEmpty(companyNumberMask)) {
            MyRio2cInputMask.enableMask(targetInputIdOrClass, companyNumberMask);
        }
        else {
            MyRio2cInputMask.removeMask(targetInputIdOrClass);
        }
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

        $(options.formIdOrClass).removeData('validator');
        $(options.formIdOrClass).removeData('unobtrusiveValidation');
        $.validator.unobtrusive.parse(options.formIdOrClass);

        if (enableHiddenInputsValidation === true) {
            var validator = $(options.formIdOrClass).data('validator');
            if (undefined != validator) {
                validator.settings.ignore = "";
            }
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
            // Redirect
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

    // CKEDITOR -----------------------------------------------------------------------------------
    var enableCkEditor = function (options) {

        if (!hasProperty(options, 'idOrClass')) {
            return;
        }

        if (isNullOrEmpty(window.CKEDITOR)) {
            return;
        }

        if (!hasProperty(options, 'maxCharCount')) {
            options.maxCharCount = 8000;
        }

        $(options.idOrClass).each(function () {
            CKEDITOR.replace($(this)[0], {
                customConfig: '/Content/js/ckeditor_config.js',
                language: globalVariables.userInterfaceLanguage,
                wordcount: {
                    countBytesAsChars: false,
                    countLineBreaks: false,
                    // Whether or not you want to show the Paragraphs Count
                    showParagraphs: false,

                    // Whether or not you want to show the Word Count
                    showWordCount: false,

                    // Whether or not you want to show the Char Count
                    showCharCount: true,

                    // Whether or not you want to count Spaces as Chars
                    countSpacesAsChars: true,

                    // Whether or not to include Html chars in the Char Count
                    countHTML: false,

                    // Maximum allowed Word Count, -1 is default for unlimited
                    maxWordCount: -1,

                    // Maximum allowed Char Count, -1 is default for unlimited
                    maxCharCount: options.maxCharCount,

                    // Add filter to add or remove element before counting (see CKEDITOR.htmlParser.filter), Default value : null (no filter)
                    filter: new CKEDITOR.htmlParser.filter({
                        elements: {
                            div: function (element) {
                                if (element.attributes.class == 'mediaembed') {
                                    return false;
                                }
                            }
                        }
                    })
                }
            });
        });
    };

    var updateCkEditorElements = function () {
        if (isNullOrEmpty(window.CKEDITOR)) {
            return;
        }

        for (var instance in CKEDITOR.instances) {
            if (CKEDITOR.instances.hasOwnProperty(instance)) {
                CKEDITOR.instances[instance].updateElement();
            }
        }
    };

    // Ajax Form ----------------------------------------------------------------------------------
    var enableAjaxForm = function (options) {

        if (!hasProperty(options, 'idOrClass')) {
            return;
        }

        var uploadFormElement = $(options.idOrClass);

        uploadFormElement.ajaxForm({
            beforeSerialize: function (form, options) {
                MyRio2cCommon.updateCkEditorElements();
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
                fixCkEditorValidation();
                initScroll();
                extendGlobalValidations();
            });
        },
        getGlobalVariables: function () {
            return getGlobalVariables();
        },
        getGlobalVariable: function (key) {
            return getGlobalVariable(key);
        },
        enableAtLeastOnCheckboxByNameValidation: function (formIdOrClass) {
            enableAtLeastOnCheckboxByNameValidation(formIdOrClass);
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
        enableCompanyNumberMask: function (originDropdownIdOrClass, targetInputIdOrClass) {
            enableCompanyNumberMask(originDropdownIdOrClass, targetInputIdOrClass);
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
        enableCkEditor: function (options) {
            enableCkEditor(options);
        },
        updateCkEditorElements: function () {
            updateCkEditorElements();
        },
        enableAjaxForm: function (options) {
            enableAjaxForm(options);
        },
        showAlert: function (options) {
            showAlert(options);
        }
    };
}();
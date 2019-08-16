// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 08-09-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-16-2019
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
    globalVariables.editionUrlCode = '';

    var setGlobalVariables = function (userInterfaceLanguage, editionUrlCode) {
        globalVariables.userInterfaceLanguage = userInterfaceLanguage;
        globalVariables.editionUrlCode = editionUrlCode;
    };

    var getGlobalVariables = function () {
        return globalVariables;
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

    // Forms --------------------------------------------------------------------------------------
    var enableFormValidation = function (options) {

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

        if (enableHiddenInputsValidation == true) {
            var validator = $(options.formIdOrClass).data('validator');
            if (undefined != validator)
                validator.settings.ignore = "";
        }
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
            //showAlert(data.message, data.status, data.isFixed);

            return null;
        }

        var data = options.data;
        
        // Undefined Error (no data.status)
        if (typeof data.status === "undefined" || data.status == null || data.status === '') {
            //showAlert(data.message, data.status, data.isFixed);

            return null;
        }

        if (data.status === "success") {
            // Redirect
            //if (typeof data.redirect !== "undefined" && data.redirect != null && data.redirect !== '') {
            if (hasProperty(data, 'redirect')) {
                //showAlert(data.message + " " + redirectMessage, data.status, data.isFixed, function () {
                //    window.location.replace(data.redirect);
                //});

                return null;
            }

            // Submit search form
            //if (typeof searchFormIdOrClass !== "undefined" && searchFormIdOrClass != null && searchFormIdOrClass !== '') {
            if (hasProperty(options, 'searchFormIdOrClass')) {
                //showAlert(data.message + " " + redirectMessage, data.status, data.isFixed, function () {
                //    $(searchFormIdOrClass).submit();
                //});

                return null;
            }

            //// Hide modal
            //if (typeof modalDivIdOrClass !== "undefined" && modalDivIdOrClass != null && modalDivIdOrClass != '') {
            //    showAlert(data.message, data.status, data.isFixed);
            //    $(modalDivIdOrClass).modal('hide');

            //    if (typeof callbackSuccess !== "undefined" && callbackSuccess != null && callbackSuccess != '') {
            //        callbackSuccess();
            //    }
            //    return;
            //}

            // Replace pages
            //if (data.pages !== "undefined" && data.pages != null && data.pages !== '') {
            if (hasProperty(data, 'pages')) {
                //showAlert(data.message, data.status, data.isFixed);

                $.each(data.pages, function (key, value) {
                    $(value.divIdOrClass).html(value.page);
                });

                //if (typeof callbackSuccess !== "undefined" && callbackSuccess != null && callbackSuccess !== '') {
                if (hasProperty(options, 'onSuccess')) {
                    return options.onSuccess(data);
                }

                return null;
            }

            //if (typeof formDivIdOrClass !== "undefined" && formDivIdOrClass != null && formDivIdOrClass !== '') {
            if (hasProperty(options, 'formDivIdOrClass')) {
                //showAlert(data.message, data.status, data.isFixed);

                if (typeof data.page !== "undefined" && data.page != null && data.page !== '') {
                    $(options.formDivIdOrClass).html(data.page);
                }
                //else {
                //    $(formDivIdOrClass).html('');
                //}

                //if (typeof callbackSuccess !== "undefined" && callbackSuccess != null && callbackSuccess !== '') {
                if (hasProperty(options, 'onSuccess')) {
                    return options.onSuccess(data);
                }

                return null;
            }

            //if (typeof callbackSuccess !== "undefined" && callbackSuccess != null && callbackSuccess !== '') {
            if (hasProperty(options, 'onSuccess')) {
                if (typeof data.message !== "undefined" && data.message != null && data.message !== '') {
                    //showAlert(data.message, data.status, data.isFixed);
                }

                return options.onSuccess();
            }

            //if (typeof data.message !== "undefined" && data.message != null && data.message !== '') {
            if (hasProperty(data, 'message')) {
                //showAlert(data.message, data.status, data.isFixed);
            }

            return null;
        }

        // Defined Error (data.status == "error)
        if (data.status === "error") {
            // User is not logged in
            //if (typeof data.redirect !== "undefined" && data.redirect != null && data.redirect !== '') {
            if (hasProperty(data, 'redirect')) {
                //showAlert(data.message + " " + redirectMessage, data.status, data.isFixed, function () {
                //    if (data.redirect.toLowerCase() == "/accounts/login")
                //        window.location.replace(data.redirect + "?ReturnUrl=" + encodeURIComponent(window.location.pathname) + encodeURIComponent(window.location.search));
                //    else
                //        window.location.replace(data.redirect);
                //});

                return null;
            }
            // Form with error
            //if (typeof formDivIdOrClass !== "undefined" && formDivIdOrClass != null && formDivIdOrClass !== '' && typeof data.page !== "undefined" && data.page !== null && data.page !== '') {
            if (hasProperty(options, 'formDivIdOrClass') && hasProperty(data, 'page')) {
                //showAlert(data.message, data.status, data.isFixed);
                $(options.formDivIdOrClass).html(data.page);

                //if (typeof callbackError !== "undefined" && callbackError != null && callbackError !== '') {
                if (hasProperty(options, 'onError')) {
                    return options.onError();
                }

                return;
            }

            //if (typeof callbackError !== "undefined" && callbackError != null && callbackError !== '') {
            if (hasProperty(options, 'onError')) {
                //showAlert(data.message, data.status, data.isFixed);
                return options.onError();
            }

            //showAlert(data.message, data.status, data.isFixed);

            return null;
        }

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

        $(options.idOrClass).each(function () {
            var ck = CKEDITOR.replace($(this)[0], {
                customConfig: '/Content/js/ckeditor_config.js'
            });
        });
    };

    var updateCkEditorElements = function () {
        if (isNullOrEmpty(window.CKEDITOR)) {
            return;
        }

        for (instance in CKEDITOR.instances) {
            CKEDITOR.instances[instance].updateElement();
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

                        options.onSuccess();
                    },
                    // Error
                    onError: function () {
                        if (!hasProperty(options, 'onError')) {
                            return;
                        }

                        options.onError();
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

    return {
        init: function (userInterfaceLanguage, editionUrlCode) {
            setGlobalVariables(userInterfaceLanguage, editionUrlCode);

            // Functions that need jquery
            $(function () {
                enableAjaxForbiddenCatch();
            });
        },
        getGlobalVariables: function() {
            return getGlobalVariables();
        },
        hasProperty: function (obj, key) {
            return hasProperty(obj, key);
        },
        isNullOrEmpty: function (value) {
            return isNullOrEmpty(value);
        },
        show: function (element) {
            show(element);
        },
        enableFormValidation: function (options) {
            enableFormValidation(options);
        },
        hide: function (element) {
            hide(element);
        },
        getUrlWithCultureAndEdition: function (url) {
            return getUrlWithCultureAndEdition(url);
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
        enableCkEditor: function(options) {
            enableCkEditor(options);
        },
        updateCkEditorElements: function() {
            updateCkEditorElements();
        },
        enableAjaxForm: function (options) {
            enableAjaxForm(options);
        }
    };
}();
// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 08-09-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-13-2019
// ***********************************************************************
// <copyright file="myrio2c.common.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var MyRio2cCommon = function () {

    // Layout -------------------------------------------------------------------------------------
    var enableAjaxForbiddenCatch = function() {
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

    // Block/unblock UI ---------------------------------------------------------------------------
    var block = function (options) {
        var idOrClass = 'body';

        if (hasProperty(options, 'idOrClass')) {
            idOrClass = options.idOrClass;
        }

        KTApp.block(idOrClass);
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

    return {
        init: function () {
            enableAjaxForbiddenCatch();
        },
        hasProperty: function (obj, key) {
            return hasProperty(obj, key);
        },
        isNullOrEmpty: function (value) {
            return isNullOrEmpty(value);
        },
        block: function (options) {
            block(options);
        },
        unblock: function (options) {
            unblock(options);
        },
        handleAjaxReturn: function (options) {
            return handleAjaxReturn(options);
        }
    };
}();
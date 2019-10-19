// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 08-16-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-18-2019
// ***********************************************************************
// <copyright file="holdings.update.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var HoldingsUpdate = function () {

    var modalId = '#UpdateHoldingModal';
    var formId = '#UpdateHoldingForm';

    // Enable form validation ---------------------------------------------------------------------
    var enableFormValidation = function () {
        MyRio2cCommon.enableFormValidation({
            formIdOrClass: formId,
            enableHiddenInputsValidation: true
        });
    };

    // Enable plugins -----------------------------------------------------------------------------
    var enablelugins = function () {
        MyRio2cCropper.init({ formIdOrClass: formId });
        //MyRio2cCommon.enableCkEditor({ idOrClass: '.ckeditor-rio2c', maxCharCount: 710 });
        enableAjaxForm();
        enableFormValidation();
    };

    // Show modal ---------------------------------------------------------------------------------
    var showModal = function (holdingUid) {
        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();
        jsonParameters.holdingUid = holdingUid;

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Holdings/ShowUpdateModal'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    enablelugins();
                    $(modalId).modal();
                },
                // Error
                onError: function() {
                }
            });
        })
        .fail(function () {
        })
        .always(function () {
            MyRio2cCommon.unblock();
        });
    };

    // Enable ajax form ---------------------------------------------------------------------------
    var enableAjaxForm = function () {
        MyRio2cCommon.enableAjaxForm({
            idOrClass: formId,
            onSuccess: function (data) {
                $(modalId).modal('hide');

                if (typeof (HoldingsDataTableWidget) !== 'undefined') {
                    HoldingsDataTableWidget.refreshData();
                }
            },
            onError: function (data) {
                if (MyRio2cCommon.hasProperty(data, 'pages')) {
                    enablelugins();
                }
            }
        });
    };

    return {
        showModal: function (holdingUid) {
            showModal(holdingUid);
        }
    };
}();
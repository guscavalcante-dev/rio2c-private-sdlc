// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 08-13-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-18-2019
// ***********************************************************************
// <copyright file="holdings.create.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var HoldingsCreate = function () {

    var modalId = '#CreateHoldingModal';
    var formId = '#CreateHoldingForm';

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
        MyRio2cCommon.enableCkEditor({ idOrClass: '.ckeditor-rio2c' });
        enableAjaxForm();
        enableFormValidation();
    };

    // Show modal ---------------------------------------------------------------------------------
    var showModal = function () {
        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Holdings/ShowCreateModal'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    enablelugins();
                    $(modalId).modal();
                    MyRio2cCommon.unblock();
                },
                // Error
                onError: function() {
                }
            });
        })
        .fail(function () {
            MyRio2cCommon.unblock();
            //showAlert();
            //MyRio2cCommon.unblock(widgetElementId);
        });
    };

    // Enable ajax form ---------------------------------------------------------------------------
    var enableAjaxForm = function () {
        MyRio2cCommon.enableAjaxForm({
            idOrClass: formId,
            onSuccess: function () {
                $(modalId).modal('hide');

                if (!MyRio2cCommon.isNullOrEmpty(HoldingsDataTableWidget)) {
                    HoldingsDataTableWidget.refreshData();
                }
            },
            onError: function () {
                enablelugins();
            }
        });
    };

    return {
        showModal: function () {
            showModal();
        }
    };
}();
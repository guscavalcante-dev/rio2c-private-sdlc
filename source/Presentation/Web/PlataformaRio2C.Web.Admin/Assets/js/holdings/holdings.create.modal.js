// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 08-13-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-16-2019
// ***********************************************************************
// <copyright file="holdings.create.modal.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var HoldingsCreateModal = function () {

    var modalId = '#CreateHoldingModal';
    var uploadFormId = '#CreateHoldingForm';

    // Enable form validation ---------------------------------------------------------------------
    var enableFormValidation = function () {
        MyRio2cCommon.enableFormValidation({
            formIdOrClass: uploadFormId,
            enableHiddenInputsValidation: true
        });
    };

    // Enable plugins -----------------------------------------------------------------------------
    var enablelugins = function () {
        MyRio2cCropper.init({ formIdOrClass: uploadFormId });
        MyRio2cCommon.enableCkEditor({ idOrClass: '.ckeditor-rio2c' });
        enableAjaxForm();
        enableFormValidation();
    };

    // Show modal ---------------------------------------------------------------------------------
    var show = function () {
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
            idOrClass: '#CreateHoldingForm',
            onSuccess: function () {
                //enableCreatePlugins();
                $(modalId).modal('hide');
            },
            onError: function () {
                enablelugins();
            }
        });
    };

    return {
        show: function () {
            show();
        }
    };
}();
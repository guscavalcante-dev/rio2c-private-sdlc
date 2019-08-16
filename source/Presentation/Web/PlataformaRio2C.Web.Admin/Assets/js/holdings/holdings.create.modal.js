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

    // Enable ajax form ---------------------------------------------------------------------------
    var enableAjaxForm = function () {
        var uploadFormElement = $(uploadFormId);

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
                        //enableCreatePlugins();
                        $(modalId).modal('hide');
                    },
                    // Error
                    onError: function () {
                        enableCreatePlugins();
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

                //if (progressBarElement.length) {
                //    progressBarElement.addClass('hide');
                //    var percentVal = '0%';
                //    bar.width(percentVal);
                //    percent.html(percentVal);
                //}
            }
        });
    };

    // Enable form validation ---------------------------------------------------------------------
    var enableFormValidation = function () {
        MyRio2cCommon.enableFormValidation({
            formIdOrClass: uploadFormId,
            enableHiddenInputsValidation: true
        });
    };

    // Create -------------------------------------------------------------------------------------
    var enableCreatePlugins = function () {
        MyRio2cCropper.init({ formIdOrClass: uploadFormId });
        MyRio2cCommon.enableCkEditor({ idOrClass: '.ckeditor-rio2c' });
        enableAjaxForm();
        enableFormValidation();
    };

    var show = function () {
        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Holdings/ShowCreateModal'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    enableCreatePlugins();
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

    return {
        show: function () {
            show();
        }
    };
}();
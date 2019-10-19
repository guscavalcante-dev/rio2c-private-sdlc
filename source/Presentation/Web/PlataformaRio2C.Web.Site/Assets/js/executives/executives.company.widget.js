// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 10-09-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-09-2019
// ***********************************************************************
// <copyright file="executives.company.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var ExecutivesCompanyWidget = function () {

    var widgetElementId = '#ExecutiveCompanyWidget';
    var widgetElement = $(widgetElementId);

    var createModalId = '#CreateCompanyInfoModal';
    var createFormId = '#CreateCompanyInfoForm';

    // Show ---------------------------------------------------------------------------------------
    var enableShowPlugins = function () {
        KTApp.initTooltips();
    };

    var show = function () {
        if (widgetElement.length <= 0) {
            return;
        }

        var jsonParameters = new Object();
        jsonParameters.collaboratorUid = $('#AggregateId').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Executives/ShowCompanyWidget'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    enableShowPlugins();
                },
                // Error
                onError: function () {
                }
            });
        })
            .fail(function () {
                //showAlert();
                //MyRio2cCommon.unblock(widgetElementId);
            })
            .always(function () {
                MyRio2cCommon.unblock({ idOrClass: widgetElementId });
            });
    };

    var enableAjaxForm = function () {
        MyRio2cCommon.enableAjaxForm({
            idOrClass: createFormId,
            onSuccess: function (data) {
                $(createModalId).modal('hide');

                if (typeof (ExecutivesCompanyWidget) !== 'undefined') {
                    ExecutivesCompanyWidget.init();
                }
            },
            onError: function (data) {
                if (MyRio2cCommon.hasProperty(data, 'pages')) {
                    enableUpdatePlugins();
                }
            }
        });
    };

    var enableUpdatePlugins = function () {
        MyRio2cCommon.enableSelect2({ inputIdOrClass: createFormId + ' .enable-select2' });
        AddressesForm.init();

        MyRio2cCommon.enableCkEditor({ idOrClass: '.ckeditor-rio2c', maxCharCount: 710 });
        MyRio2cCropper.init({ formIdOrClass: createFormId });
        enableAjaxForm();
        MyRio2cCommon.enableFormValidation({ formIdOrClass: createFormId, enableHiddenInputsValidation: true });
    };

    var showCreateModal = function () {
        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Executives/ShowCreateCompanyInfoModal'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    enableUpdatePlugins();
                    $(createModalId).modal();
                },
                // Error
                onError: function () {
                }
            });
        })
            .fail(function () {
            })
            .always(function () {
                MyRio2cCommon.unblock();
            });
    };


    return {
        init: function () {
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            show();
        },
        showCreateModal: function () {
            showCreateModal();
        }
    };
}();
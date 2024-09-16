// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 06-03-2021
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-19-2021
// ***********************************************************************
// <copyright file="audiovisual.organizations.maininformation.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var OrganizationsMainInformationWidget = function () {

    var widgetElementId = '#CompanyMainInformationWidget';
    var widgetElement = $(widgetElementId);

    var updateModalId = '#UpdateMainInformationModal';
    var updateFormId = '#UpdateMainInformationForm';
    var countryUid = '#CountryUid';

    // Show ---------------------------------------------------------------------------------------
    var enableShowPlugins = function () {
        KTApp.initTooltips();
        MyRio2cCommon.enableTooltips();
    };

    var show = function () {
        if (widgetElement.length <= 0) {
            return;
        }

        var jsonParameters = new Object();
        jsonParameters.organizationUid = $('#AggregateId').val();
        jsonParameters.organizationTypeUid = $('#OrganizationTypeUid').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Organizations/ShowMainInformationWidget'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    enableShowPlugins();
                },
                // Error
                onError: function() {
                }
            });
        })
        .fail(function () {
            //showAlert();
            //MyRio2cCommon.unblock(widgetElementId);
        })
        .always(function() {
            MyRio2cCommon.unblock({ idOrClass: widgetElementId });
        });
    };

    // Update -------------------------------------------------------------------------------------
    var enableAjaxForm = function () {
        MyRio2cCommon.enableAjaxForm({
            idOrClass: updateFormId,
            onSuccess: function (data) {
                $(updateModalId).modal('hide');

                if (typeof (OrganizationsMainInformationWidget) !== 'undefined') {
                    OrganizationsMainInformationWidget.init();
                }
            },
            onError: function (data) {
                if (MyRio2cCommon.hasProperty(data, 'pages')) {
                    enableUpdatePlugins();
                }

                $(updateFormId).find(":input.input-validation-error:first").focus();
            }
        });
    };

    var enableUpdatePlugins = function () {
        MyRio2cCropper.init({ formIdOrClass: updateFormId });
        MyRio2cCommon.enableSelect2({ inputIdOrClass: updateFormId + ' .enable-select2' });
        /*MyRio2cCommon.enableCkEditor({ idOrClass: '.ckeditor-rio2c', maxCharCount: 710 });*/

        MyRio2cCompanyDocument.enableCompanyNumberMask(countryUid, '#Document');
        enableAjaxForm();
        MyRio2cCommon.enableFormValidation({ formIdOrClass: updateFormId, enableHiddenInputsValidation: true, enableMaxlength: true });
    };

    var showUpdateModal = function () {
        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();
        jsonParameters.organizationUid = $('#AggregateId').val();
        jsonParameters.organizationTypeUid = $('#OrganizationTypeUid').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Organizations/ShowUpdateMainInformationModal'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
            data: data,
            // Success
            onSuccess: function () {
                enableUpdatePlugins();
                $(updateModalId).modal();
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
        showUpdateModal: function () {
            showUpdateModal();
        }
    };
}();
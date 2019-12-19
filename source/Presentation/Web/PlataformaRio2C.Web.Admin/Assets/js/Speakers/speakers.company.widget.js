// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 12-16-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-19-2019
// ***********************************************************************
// <copyright file="speakers.company.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var SpeakersCompanyWidget = function () {

    var widgetElementId = '#SpeakerCompanyWidget';
    var widgetElement = $(widgetElementId);

    var createModalId = '#UpdateCompanyInfoModal';
    var createFormId = '#UpdateCompanyInfoForm';

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

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Speakers/ShowCompanyWidget'), jsonParameters, function (data) {
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
            })
            .always(function () {
                MyRio2cCommon.unblock({ idOrClass: widgetElementId });
            });
    };

    // Update -------------------------------------------------------------------------------------
    var enableAjaxForm = function () {
        MyRio2cCommon.enableAjaxForm({
            idOrClass: createFormId,
            onSuccess: function (data) {
                $(createModalId).modal('hide');

                if (typeof (SpeakersCompanyWidget) !== 'undefined') {
                    SpeakersCompanyWidget.init();
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
        if (typeof (MyRio2cCropper) !== 'undefined') {
            MyRio2cCropper.init({ formIdOrClass: createFormId });
        }
        
        if (typeof (AddressesForm) !== 'undefined') {
            AddressesForm.init();
        }

        if (typeof (CompanyInfoAutocomplete) !== 'undefined' && $('#OrganizationUid').val() === '') {
            CompanyInfoAutocomplete.init('/Companies/ShowTicketBuyerFilledForm', enableUpdatePlugins);
        }

        enableAjaxForm();
        MyRio2cCommon.enableFormValidation({ formIdOrClass: createFormId, enableHiddenInputsValidation: true, enableMaxlength: true });
    };

    var showUpdateModal = function (organizationUid) {
        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();
        jsonParameters.collaboratorUid = $('#AggregateId').val();
        jsonParameters.organizationUid = organizationUid;

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Speakers/ShowUpdateCompanyInfoModal'), jsonParameters, function (data) {
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
        showUpdateModal: function (organizationUid) {
            showUpdateModal(organizationUid);
        }
    };
}();
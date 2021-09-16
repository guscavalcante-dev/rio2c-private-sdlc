// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 08-31-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 09-16-2021
// ***********************************************************************
// <copyright file="collaborators.company.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var CollaboratorsCompanyWidget = function () {

    var widgetElementId = '#CollaboratorCompanyWidget';
    var widgetElement = $(widgetElementId);

    var createModalId = '#UpdateCompanyInfoModal';
    var createFormId = '#UpdateCompanyInfoForm';

    var _organizationTypeName = '';

    // Show ---------------------------------------------------------------------------------------
    var enableShowPlugins = function () {
        KTApp.initTooltips();
        MyRio2cCommon.initScroll();
    };

    var show = function (organizationTypeName) {
        if (widgetElement.length <= 0) {
            return;
        }

        _organizationTypeName = organizationTypeName;

        var jsonParameters = new Object();
        jsonParameters.collaboratorUid = $('#AggregateId').val();
        jsonParameters.organizationTypeName = organizationTypeName;

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Collaborators/ShowCompanyWidget'), jsonParameters, function (data) {
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

                if (typeof (CollaboratorsCompanyWidget) !== 'undefined') {
                    CollaboratorsCompanyWidget.init(_organizationTypeName);
                }
            },
            onError: function (data) {
                if (MyRio2cCommon.hasProperty(data, 'pages')) {
                    enableUpdatePlugins();
                }

                $(createFormId).find(":input.input-validation-error:first").focus();
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

        if (typeof (CompanyInfoAutocomplete) !== 'undefined' && $('#IsUpdate').val() !== 'True') {
            CompanyInfoAutocomplete.init('/Companies/ShowTicketBuyerFilledForm', enableUpdatePlugins, _organizationTypeName);
        }

        enableAjaxForm();
        MyRio2cCommon.enableFormValidation({ formIdOrClass: createFormId, enableHiddenInputsValidation: true, enableMaxlength: true });
    };

    var showUpdateModal = function (organizationUid) {
        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();
        jsonParameters.collaboratorUid = $('#AggregateId').val();
        jsonParameters.organizationUid = organizationUid;

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Collaborators/ShowUpdateCompanyInfoModal'), jsonParameters, function (data) {
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

    // Delete -------------------------------------------------------------------------------------
    var executeDelete = function (organizationUid) {
        MyRio2cCommon.block();

        var jsonParameters = new Object();
        jsonParameters.collaboratorUid = $('#AggregateId').val();
        jsonParameters.organizationUid = organizationUid;

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Collaborators/DeleteOrganization'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    if (typeof (CollaboratorsCompanyWidget) !== 'undefined') {
                        CollaboratorsCompanyWidget.init(_organizationTypeName);
                    }
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

    var showDeleteModal = function (organizationUid, isDeletingFromCurrentEdition) {
        var message = labels.deleteConfirmationMessage;

        if (isDeletingFromCurrentEdition) {
            message = labels.deleteCurrentEditionConfirmationMessage;
        }

        bootbox.dialog({
            message: message,
            buttons: {
                cancel: {
                    label: labels.cancel,
                    className: "btn btn-secondary mr-auto",
                    callback: function () {
                    }
                },
                confirm: {
                    label: labels.remove,
                    className: "btn btn-danger",
                    callback: function () {
                        executeDelete(organizationUid);
                    }
                }
            }
        });
    };

    return {
        init: function (organizationTypeName) {
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            show(organizationTypeName);
        },
        showUpdateModal: function (organizationUid) {
            showUpdateModal(organizationUid);
        },
        showDeleteModal: function (organizationUid) {
            showDeleteModal(organizationUid);
        }
    };
}();
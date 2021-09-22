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

    var createModalId = '#AssociateCompanyModal';
    var createFormId = '#AssociateCompanyForm';

    var organizationTypeElementId = '#OrganizationTypeUid';
    var organizationTypeForDropdownSearchId = '#OrganizationTypeForDropdownSearch';

    // Show ---------------------------------------------------------------------------------------
    var enableShowPlugins = function () {
        KTApp.initTooltips();
        MyRio2cCommon.initScroll();
    };

    var show = function (organizationTypeUid) {
        if (widgetElement.length <= 0) {
            return;
        }

        var jsonParameters = new Object();
        jsonParameters.collaboratorUid = $('#AggregateId').val();
        jsonParameters.organizationTypeUid = organizationTypeUid;

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

    // Associate --------------------------------------------------------------------------------------
    var enableAssociateAjaxForm = function () {
        MyRio2cCommon.enableAjaxForm({
            idOrClass: createFormId,
            onSuccess: function (data) {
                $(createModalId).modal('hide');

                if (typeof (CollaboratorsCompanyWidget) !== 'undefined') {
                    CollaboratorsCompanyWidget.init($(organizationTypeElementId).val());
                }
            },
            onError: function (data) {
                if (MyRio2cCommon.hasProperty(data, 'pages')) {
                    enableAssociatePlugins();
                }

                $(createFormId).find(":input.input-validation-error:first").focus();
            }
        });
    };

    var enableAssociatePlugins = function () {
        enableAssociateAjaxForm();
        MyRio2cCommon.enableOrganizationSelect2({
            inputIdOrClass: '#OrganizationUid',
            url: '/' + $(organizationTypeForDropdownSearchId).val() + '/FindAllByFilters',
            placeholder: labels.selectPlaceholder,
            //customFilter: 'HasProjectNegotiationNotScheduled',
            //selectedOption: {
            //    id: $('#InitialBuyerOrganizationUid').val(),
            //    text: $('#InitialBuyerOrganizationName').val()
            //}
        });
        MyRio2cCommon.enableFormValidation({ formIdOrClass: createFormId, enableHiddenInputsValidation: true, enableMaxlength: true });
    };

    var showAssociateModal = function (collaboratorUid) {
        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();
        jsonParameters.collaboratorUid = collaboratorUid;
        jsonParameters.organizationTypeUid = $(organizationTypeElementId).val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Collaborators/ShowAssociateCompanyModal'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    enableAssociatePlugins();
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

    // Delete --------------------------------------------------------------------------------------
    var executeDisassociate = function (collaboratorUid, organizationUid) {
        MyRio2cCommon.block();

        var jsonParameters = new Object();
        jsonParameters.collaboratorUid = collaboratorUid;
        jsonParameters.organizationUid = organizationUid;
        jsonParameters.organizationTypeUid = $(organizationTypeElementId).val();

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Collaborators/DisassociateCompany'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    if (typeof (CollaboratorsCompanyWidget) !== 'undefined') {
                        CollaboratorsCompanyWidget.init($(organizationTypeElementId).val());
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

    var showDisassociateModal = function (collaboratorUid, organizationUid) {
        var message = labels.deleteConfirmationMessage;

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
                        executeDisassociate(collaboratorUid, organizationUid);
                    }
                }
            }
        });
    };

    return {
        init: function (organizationTypeUid) {
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            show(organizationTypeUid);
        },
        showAssociateModal: function (collaboratorUid) {
            showAssociateModal(collaboratorUid);
        },
        showDisassociateModal: function (collaboratorUid, organizationUid) {
            showDisassociateModal(collaboratorUid, organizationUid);
        },
    };
}();
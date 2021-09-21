// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 06-03-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 09-20-2021
// ***********************************************************************
// <copyright file="audiovisual.organizations.executives.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var AudiovisualOrganizationsExecutivesWidget = function () {

    var widgetElementId = '#OrganizationExecutivesWidget';
    var widgetElement = $(widgetElementId);

    var createModalId = '#AssociateExecutiveModal';
    var createFormId = '#AssociateExecutiveForm';

    var organizationTypeElementId = '#OrganizationTypeUid';
    var collaboratorTypeForDropdownSearchId = '#CollaboratorTypeForDropdownSearch';

    // Show ---------------------------------------------------------------------------------------
    var enableShowPlugins = function () {
        KTApp.initTooltips();
        MyRio2cCommon.initScroll();
    };

    var show = function () {
        if (widgetElement.length <= 0) {
            return;
        }

        var jsonParameters = new Object();
        jsonParameters.organizationUid = $('#AggregateId').val();
        jsonParameters.organizationTypeUid = $(organizationTypeElementId).val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/Organizations/ShowExecutivesWidget'), jsonParameters, function (data) {
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
        })
        .always(function() {
            MyRio2cCommon.unblock({ idOrClass: widgetElementId });
        });
    };

    // Associate --------------------------------------------------------------------------------------
    var enableAssociateAjaxForm = function () {
        MyRio2cCommon.enableAjaxForm({
            idOrClass: createFormId,
            onSuccess: function (data) {
                $(createModalId).modal('hide');

                if (typeof (OrganizationExecutivesWidget) !== 'undefined') {
                    AudiovisualOrganizationsExecutivesWidget.init($(organizationTypeElementId).val());
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
        MyRio2cCommon.enableCollaboratorSelect2({
            url: '/' + $(collaboratorTypeForDropdownSearchId).val() + '/FindAllByFilters',
            placeholder: labels.selectPlaceholder,
            //customFilter: 'HasProjectNegotiationNotScheduled',
            //selectedOption: {
            //    id: $('#InitialBuyerOrganizationUid').val(),
            //    text: $('#InitialBuyerOrganizationName').val()
            //}
        });


        MyRio2cCommon.enableSelect2({ inputIdOrClass: createFormId + ' .enable-select2', allowClear: true });
        MyRio2cCommon.enableFormValidation({ formIdOrClass: createFormId, enableHiddenInputsValidation: true, enableMaxlength: true });
    };

    var showAssociateModal = function (organizationUid) {
        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();
        jsonParameters.organizationUid = organizationUid;
        jsonParameters.organizationTypeUid = $(organizationTypeElementId).val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/Organizations/ShowAssociateExecutiveModal'), jsonParameters, function (data) {
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

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/Organizations/DisassociateExecutive'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    if (typeof (OrganizationExecutivesWidget) !== 'undefined') {
                        AudiovisualOrganizationsExecutivesWidget.init($(organizationTypeElementId).val());
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
        init: function () {
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            show();
        },
        showAssociateModal: function (organizationUid) {
            showAssociateModal(organizationUid);
        },
        showDisassociateModal: function (collaboratorUid, organizationUid) {
            showDisassociateModal(collaboratorUid, organizationUid);
        },
    };
}();
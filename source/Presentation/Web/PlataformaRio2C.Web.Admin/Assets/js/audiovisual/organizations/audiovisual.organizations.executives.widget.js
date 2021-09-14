// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 06-03-2021
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-22-2021
// ***********************************************************************
// <copyright file="audiovisual.organizations.executives.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var AudiovisualOrganizationsExecutivesWidget = function () {

    var widgetElementId = '#OrganizationExecutivesWidget';
    var widgetElement = $(widgetElementId);

    var createModalId = '#CreateOrganizationExecutiveModal';
    var createFormId = '#CreateOrganizationExecutiveForm';

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
        jsonParameters.organizationTypeUid = $('#OrganizationTypeUid').val();

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

    // Create --------------------------------------------------------------------------------------
    var enableCreateAjaxForm = function () {
        MyRio2cCommon.enableAjaxForm({
            idOrClass: createFormId,
            onSuccess: function (data) {
                $(createModalId).modal('hide');

                if (typeof (OrganizationExecutivesWidget) !== 'undefined') {
                    AudiovisualOrganizationsExecutivesWidget.init();
                }
            },
            onError: function (data) {
                if (MyRio2cCommon.hasProperty(data, 'pages')) {
                    enableCreatePlugins();
                }

                $(createFormId).find(":input.input-validation-error:first").focus();
            }
        });
    };

    var enableCreatePlugins = function () {
        enableCreateAjaxForm();
        MyRio2cCommon.enableCollaboratorSelect2({ url: '/Collaborators/FindAllExecutivesByFilters' });
        MyRio2cCommon.enableSelect2({ inputIdOrClass: createFormId + ' .enable-select2', allowClear: true });
        MyRio2cCommon.enableFormValidation({ formIdOrClass: createFormId, enableHiddenInputsValidation: true, enableMaxlength: true });
    };

    var showCreateModal = function (attendeeOrganizationUid) {
        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();
        jsonParameters.attendeeOrganizationUid = attendeeOrganizationUid;

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/Organizations/ShowCreateExecutiveModal'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    enableCreatePlugins();
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
    var executeDelete = function (collaboratorUid, organizationUid) {
        MyRio2cCommon.block();

        var jsonParameters = new Object();
        jsonParameters.collaboratorUid = collaboratorUid;
        jsonParameters.organizationUid = organizationUid;

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/Organizations/DeleteExecutive'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    if (typeof (OrganizationExecutivesWidget) !== 'undefined') {
                        AudiovisualOrganizationsExecutivesWidget.init();
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

    var showDeleteModal = function (collaboratorUid, organizationUid, isDeletingFromCurrentEdition) {
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
                        executeDelete(collaboratorUid, organizationUid);
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
        showCreateModal: function (attendeeOrganizationUid) {
            showCreateModal(attendeeOrganizationUid);
        },
        showDeleteModal: function (collaboratorUid, organizationUid, isDeletingFromCurrentEdition) {
            showDeleteModal(collaboratorUid, organizationUid, isDeletingFromCurrentEdition);
        },
    };
}();
// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 01-02-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-21-2020
// ***********************************************************************
// <copyright file="conferences.participants.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var ConferencesParticipantsWidget = function () {

    var widgetElementId = '#ConferenceParticipantsWidget';
    var widgetElement = $(widgetElementId);

    var createModalId = '#CreateParticipantModal';
    var createFormId = '#CreateParticipantForm';

    var updateModalId = '#UpdateParticipantModal';
    var updateFormId = '#UpdateParticipantForm';

    // Show ---------------------------------------------------------------------------------------
    var enableShowPlugins = function () {
        KTApp.initTooltips();
        MyRio2cCommon.initScroll();
        //enableSpeakerSelect2();
    };

    var show = function () {
        if (widgetElement.length <= 0) {
            return;
        }

        var jsonParameters = new Object();
        jsonParameters.conferenceUid = $('#AggregateId').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Conferences/ShowParticipantsWidget'), jsonParameters, function (data) {
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

    // Create -------------------------------------------------------------------------------------
    var enableCreateAjaxForm = function () {
        MyRio2cCommon.enableAjaxForm({
            idOrClass: createFormId,
            onSuccess: function (data) {
                $(createModalId).modal('hide');

                if (typeof (ConferencesParticipantsWidget) !== 'undefined') {
                    ConferencesParticipantsWidget.init();
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
        MyRio2cCommon.enableCollaboratorSelect2({ url: '/Speakers/FindAllByFilters' });
        MyRio2cCommon.enableSelect2({ inputIdOrClass: createFormId + ' .enable-select2', allowClear: true });
        MyRio2cCommon.enableFormValidation({ formIdOrClass: createFormId, enableHiddenInputsValidation: true, enableMaxlength: true });
    };

    var showCreateModal = function () {
        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();
        jsonParameters.conferenceUid = $('#AggregateId').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Conferences/ShowCreateParticipantModal'), jsonParameters, function (data) {
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

    // Update -------------------------------------------------------------------------------------
    var enableUpdateAjaxForm = function () {
        MyRio2cCommon.enableAjaxForm({
            idOrClass: updateFormId,
            onSuccess: function (data) {
                $(updateModalId).modal('hide');

                if (typeof (ConferencesParticipantsWidget) !== 'undefined') {
                    ConferencesParticipantsWidget.init();
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
        enableUpdateAjaxForm();
        //MyRio2cCommon.enableCollaboratorSelect2({ url: '/Speakers/FindAllByFilters', selectedOption: { id: $('#InitialCollaboratorUid').val(), text: $('#InitialCollaboratorName').val() }});
        MyRio2cCommon.enableSelect2({ inputIdOrClass: updateFormId + ' .enable-select2', allowClear: true });
        MyRio2cCommon.enableFormValidation({ formIdOrClass: updateFormId, enableHiddenInputsValidation: true, enableMaxlength: true });
    };

    var showUpdateModal = function (collaboratorUid) {
        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();
        jsonParameters.conferenceUid = $('#AggregateId').val();
        jsonParameters.collaboratorUid = collaboratorUid;

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Conferences/ShowUpdateParticipantModal'), jsonParameters, function (data) {
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

    // Delete -------------------------------------------------------------------------------------
    var executeDelete = function (collaboratorUid) {
        MyRio2cCommon.block();

        var jsonParameters = new Object();
        jsonParameters.conferenceUid = $('#AggregateId').val();
        jsonParameters.collaboratorUid = collaboratorUid;

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Conferences/DeleteParticipant'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    if (typeof (ConferencesParticipantsWidget) !== 'undefined') {
                        ConferencesParticipantsWidget.init();
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

    var showDeleteModal = function (collaboratorUid) {
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
                        executeDelete(collaboratorUid);
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
        showCreateModal: function () {
            showCreateModal();
        },
        showUpdateModal: function (collaboratorUid) {
            showUpdateModal(collaboratorUid);
        },
        showDeleteModal: function (collaboratorUid) {
            showDeleteModal(collaboratorUid);
        }
    };
}();
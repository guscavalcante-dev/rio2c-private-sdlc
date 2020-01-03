// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 01-02-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-03-2020
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

    // Show ---------------------------------------------------------------------------------------
    var enableShowPlugins = function () {
        KTApp.initTooltips();
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
    var enableAjaxForm = function () {
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
            }
        });
    };

    var enableCreatePlugins = function () {
        enableAjaxForm();
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

    //// Delete -------------------------------------------------------------------------------------
    //var executeDelete = function (organizationUid) {
    //    MyRio2cCommon.block();

    //    var jsonParameters = new Object();
    //    jsonParameters.collaboratorUid = $('#AggregateId').val();
    //    jsonParameters.organizationUid = organizationUid;

    //    $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Speakers/DeleteOrganization'), jsonParameters, function (data) {
    //        MyRio2cCommon.handleAjaxReturn({
    //            data: data,
    //            // Success
    //            onSuccess: function () {
    //                if (typeof (SpeakersCompanyWidget) !== 'undefined') {
    //                    SpeakersCompanyWidget.init();
    //                }
    //            },
    //            // Error
    //            onError: function () {
    //            }
    //        });
    //    })
    //    .fail(function () {
    //    })
    //    .always(function () {
    //        MyRio2cCommon.unblock();
    //    });
    //};

    //var showDeleteModal = function (organizationUid, isDeletingFromCurrentEdition) {
    //    var message = labels.deleteConfirmationMessage;

    //    if (isDeletingFromCurrentEdition) {
    //        message = labels.deleteCurrentEditionConfirmationMessage;
    //    }

    //    bootbox.dialog({
    //        message: message,
    //        buttons: {
    //            cancel: {
    //                label: labels.cancel,
    //                className: "btn btn-secondary mr-auto",
    //                callback: function () {
    //                }
    //            },
    //            confirm: {
    //                label: labels.remove,
    //                className: "btn btn-danger",
    //                callback: function () {
    //                    executeDelete(organizationUid);
    //                }
    //            }
    //        }
    //    });
    //};

    return {
        init: function () {
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            show();
        },
        showCreateModal: function () {
            showCreateModal();
        },
        //showDeleteModal: function (organizationUid) {
        //    showDeleteModal(organizationUid);
        //}
    };
}();
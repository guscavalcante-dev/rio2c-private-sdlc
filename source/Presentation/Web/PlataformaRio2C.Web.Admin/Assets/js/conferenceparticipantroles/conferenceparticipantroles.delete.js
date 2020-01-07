// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 01-07-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="conferenceparticipantroles.delete.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var ConferenceParticipantRolesDelete = function () {

    // Delete -------------------------------------------------------------------------------------
    var executeDelete = function (conferenceParticipantRoleUid) {
        MyRio2cCommon.block();

        var jsonParameters = new Object();
        jsonParameters.conferenceParticipantRoleUid = conferenceParticipantRoleUid;

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/ParticipantRoles/Delete'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    if (typeof (ConferenceParticipantRolesDataTableWidget) !== 'undefined') {
                        ConferenceParticipantRolesDataTableWidget.refreshData();
                    }

                    if (typeof (ConferenceParticipantRolesTotalCountWidget) !== 'undefined') {
                        ConferenceParticipantRolesTotalCountWidget.init();
                    }

                    if (typeof (ConferenceParticipantRolesEditionCountWidget) !== 'undefined') {
                        ConferenceParticipantRolesEditionCountWidget.init();
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

    var showModal = function (conferenceParticipantRoleUid) {
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
                        executeDelete(conferenceParticipantRoleUid);
                    }
                }
            }
        });
    };

    return {
        showModal: function (conferenceParticipantRoleUid) {
            showModal(conferenceParticipantRoleUid);
        }
    };
}();
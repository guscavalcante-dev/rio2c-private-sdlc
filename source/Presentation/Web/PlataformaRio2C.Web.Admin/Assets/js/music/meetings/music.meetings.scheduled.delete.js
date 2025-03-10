// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Ribeiro
// Created          : 21-02-2025
//
// Last Modified By : Rafael Ribeiro
// Last Modified On : 21-02-2025
// ***********************************************************************
// <copyright file="Music.meetings.scheduled.delete.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var MusicMeetingsScheduledDelete = function () {

    // Delete -------------------------------------------------------------------------------------
    var executeDelete = function (negotiationUid) {
        MyRio2cCommon.block();

        var jsonParameters = new Object();
        jsonParameters.negotiationUid = negotiationUid;

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Music/Meetings/Delete'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    if (typeof (MusicMeetingsScheduledWidget) !== 'undefined') {
	                    MusicMeetingsScheduledWidget.search();
	                }

                    if (typeof (MusicMeetingsEditionScheduledCountWidget) !== 'undefined') {
	                    MusicMeetingsEditionScheduledCountWidget.init();
	                }

                    if (typeof (MusicMeetingsEditionUnscheduledCountWidget) !== 'undefined') {
	                    MusicMeetingsEditionUnscheduledCountWidget.init();
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

    var showModal = function (negotiationUid) {
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
                        executeDelete(negotiationUid);
                    }
                }
            }
        });
    };

    return {
        showModal: function (negotiationUid) {
            showModal(negotiationUid);
        }
    };
}();
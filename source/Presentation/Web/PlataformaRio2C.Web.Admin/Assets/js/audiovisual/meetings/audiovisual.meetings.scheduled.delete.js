// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 03-08-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-08-2020
// ***********************************************************************
// <copyright file="audiovisual.meetings.scheduled.delete.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var AudiovisualMeetingsScheduledDelete = function () {

    // Delete -------------------------------------------------------------------------------------
    var executeDelete = function (negotiationUid) {
        MyRio2cCommon.block();

        var jsonParameters = new Object();
        jsonParameters.negotiationUid = negotiationUid;

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/Meetings/Delete'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    if (typeof (AudiovisualMeetingsScheduledWidget) !== 'undefined') {
	                    AudiovisualMeetingsScheduledWidget.search();
	                }

                    if (typeof (AudiovisualMeetingsEditionScheduledCountWidget) !== 'undefined') {
	                    AudiovisualMeetingsEditionScheduledCountWidget.init();
	                }

                    if (typeof (AudiovisualMeetingsEditionUnscheduledCountWidget) !== 'undefined') {
	                    AudiovisualMeetingsEditionUnscheduledCountWidget.init();
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
// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 03-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-04-2020
// ***********************************************************************
// <copyright file="audiovisual.meetingparameters.delete.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var AudiovisualMeetingParametersDelete = function () {

    // Delete -------------------------------------------------------------------------------------
    var executeDelete = function (negotiationConfigUid) {
        MyRio2cCommon.block();

        var jsonParameters = new Object();
        jsonParameters.negotiationConfigUid = negotiationConfigUid;

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/MeetingParameters/Delete'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
	                if (typeof (AudiovisualMeetingParametersDataTableWidget) !== 'undefined') {
		                AudiovisualMeetingParametersDataTableWidget.refreshData();
	                }

	                if (typeof (AudiovisualMeetingParametersTotalCountWidget) !== 'undefined') {
		                AudiovisualMeetingParametersTotalCountWidget.init();
	                }

	                if (typeof (AudiovisualMeetingParametersEditionCountWidget) !== 'undefined') {
		                AudiovisualMeetingParametersEditionCountWidget.init();
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

    var showModal = function (negotiationConfigUid) {
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
                        executeDelete(negotiationConfigUid);
                    }
                }
            }
        });
    };

    return {
        showModal: function (negotiationConfigUid) {
            showModal(negotiationConfigUid);
        }
    };
}();
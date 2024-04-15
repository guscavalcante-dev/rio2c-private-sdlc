// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 12-04-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-04-2024
// ***********************************************************************
// <copyright file="availabilities.delete.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var AvailabilitiesDelete = function () {

    // Delete -------------------------------------------------------------------------------------
    var executeDelete = function (uid) {
        MyRio2cCommon.block();

        var jsonParameters = new Object();
        jsonParameters.attendeeCollaboratorUid = uid;

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Logistics/Availability/Delete'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    if (typeof (AvailabilitiesDataTableWidget) !== 'undefined') {
	                    AvailabilitiesDataTableWidget.refreshData();
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

    var showModal = function (uid) {
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
					    executeDelete(uid);
				    }
			    }
		    }
	    });
    };

    return {
	    showModal: function (uid) {
		    showModal(uid);
	    }
    };
}();
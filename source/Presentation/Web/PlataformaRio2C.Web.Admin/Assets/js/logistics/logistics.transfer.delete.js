﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 08-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-26-2019
// ***********************************************************************
// <copyright file="logistics.transfer.delete.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var LogisticTransferDelete = function () {

    // Delete -------------------------------------------------------------------------------------
    var executeDelete = function (uid) {
        MyRio2cCommon.block();

        var jsonParameters = new Object();
        jsonParameters.uid = uid;

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Logistics/Requests/DeleteLogisticTransfer'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    if (typeof (LogisticsTransferWidget) !== 'undefined') {
	                    LogisticsTransferWidget.init();
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
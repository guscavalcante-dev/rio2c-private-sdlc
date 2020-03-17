// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 03-17-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-17-2020
// ***********************************************************************
// <copyright file="places.delete.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var PlacesDelete = function () {

    // Delete -------------------------------------------------------------------------------------
    var executeDelete = function (placeUid) {
        MyRio2cCommon.block();

        var jsonParameters = new Object();
        jsonParameters.placeUid = placeUid;

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Places/Delete'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
	                if (typeof (PlacesDataTableWidget) !== 'undefined') {
		                PlacesDataTableWidget.refreshData();
	                }

	                if (typeof (PlacesTotalCountWidget) !== 'undefined') {
		                PlacesTotalCountWidget.init();
	                }

	                if (typeof (PlacesEditionCountWidget) !== 'undefined') {
		                PlacesEditionCountWidget.init();
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

    var showModal = function (placeUid) {
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
                        executeDelete(placeUid);
                    }
                }
            }
        });
    };

    return {
        showModal: function (placeUid) {
            showModal(placeUid);
        }
    };
}();
// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-06-2020
// ***********************************************************************
// <copyright file="rooms.delete.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var RoomsDelete = function () {

    // Delete -------------------------------------------------------------------------------------
    var executeDelete = function (roomUid) {
        MyRio2cCommon.block();

        var jsonParameters = new Object();
        jsonParameters.roomUid = roomUid;

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Rooms/Delete'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    if (typeof (RoomsDataTableWidget) !== 'undefined') {
                        RoomsDataTableWidget.refreshData();
                    }

                    if (typeof (RoomsTotalCountWidget) !== 'undefined') {
                        RoomsTotalCountWidget.init();
                    }

                    if (typeof (RoomsEditionCountWidget) !== 'undefined') {
                        RoomsEditionCountWidget.init();
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

    var showModal = function (roomUid) {
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
                        executeDelete(roomUid);
                    }
                }
            }
        });
    };

    return {
        showModal: function (roomUid) {
            showModal(roomUid);
        }
    };
}();
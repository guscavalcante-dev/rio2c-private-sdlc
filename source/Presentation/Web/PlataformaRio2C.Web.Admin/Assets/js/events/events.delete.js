// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-06-2020
// ***********************************************************************
// <copyright file="events.delete.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var EventsDelete = function () {

    // Delete -------------------------------------------------------------------------------------
    var executeDelete = function (editionEventUid) {
        MyRio2cCommon.block();

        var jsonParameters = new Object();
        jsonParameters.editionEventUid = editionEventUid;

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Events/Delete'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    if (typeof (EventsDataTableWidget) !== 'undefined') {
                        EventsDataTableWidget.refreshData();
                    }

                    if (typeof (EventsTotalCountWidget) !== 'undefined') {
                        EventsTotalCountWidget.init();
                    }

                    if (typeof (EventsEditionCountWidget) !== 'undefined') {
                        EventsEditionCountWidget.init();
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

    var showModal = function (editionEventUid) {
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
                        executeDelete(editionEventUid);
                    }
                }
            }
        });
    };

    return {
        showModal: function (editionEventUid) {
            showModal(editionEventUid);
        }
    };
}();
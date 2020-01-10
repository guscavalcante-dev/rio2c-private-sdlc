// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="tracks.delete.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var TracksDelete = function () {

    // Delete -------------------------------------------------------------------------------------
    var executeDelete = function (trackUid) {
        MyRio2cCommon.block();

        var jsonParameters = new Object();
        jsonParameters.trackUid = trackUid;

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Tracks/Delete'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    if (typeof (TracksDataTableWidget) !== 'undefined') {
                        TracksDataTableWidget.refreshData();
                    }

                    if (typeof (TracksTotalCountWidget) !== 'undefined') {
                        TracksTotalCountWidget.init();
                    }

                    if (typeof (TracksEditionCountWidget) !== 'undefined') {
                        TracksEditionCountWidget.init();
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

    var showModal = function (trackUid) {
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
                        executeDelete(trackUid);
                    }
                }
            }
        });
    };

    return {
        showModal: function (trackUid) {
            showModal(trackUid);
        }
    };
}();
// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 01-07-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="horizontaltracks.delete.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var HorizontalTracksDelete = function () {

    // Delete -------------------------------------------------------------------------------------
    var executeDelete = function (horizontalTrackUid) {
        MyRio2cCommon.block();

        var jsonParameters = new Object();
        jsonParameters.horizontalTrackUid = horizontalTrackUid;

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/HorizontalTracks/Delete'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    if (typeof (HorizontalTracksDataTableWidget) !== 'undefined') {
                        HorizontalTracksDataTableWidget.refreshData();
                    }

                    if (typeof (HorizontalTracksTotalCountWidget) !== 'undefined') {
                        HorizontalTracksTotalCountWidget.init();
                    }

                    if (typeof (HorizontalTracksEditionCountWidget) !== 'undefined') {
                        HorizontalTracksEditionCountWidget.init();
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

    var showModal = function (horizontalTrackUid) {
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
                        executeDelete(horizontalTrackUid);
                    }
                }
            }
        });
    };

    return {
        showModal: function (horizontalTrackUid) {
            showModal(horizontalTrackUid);
        }
    };
}();
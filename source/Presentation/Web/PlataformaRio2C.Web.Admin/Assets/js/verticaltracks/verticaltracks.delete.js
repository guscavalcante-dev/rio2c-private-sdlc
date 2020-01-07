// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-06-2020
// ***********************************************************************
// <copyright file="verticaltracks.delete.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var VerticalTracksDelete = function () {

    // Delete -------------------------------------------------------------------------------------
    var executeDelete = function (verticalTrackUid) {
        MyRio2cCommon.block();

        var jsonParameters = new Object();
        jsonParameters.verticalTrackUid = verticalTrackUid;

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/VerticalTracks/Delete'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    if (typeof (VerticalTracksDataTableWidget) !== 'undefined') {
                        VerticalTracksDataTableWidget.refreshData();
                    }

                    if (typeof (VerticalTracksTotalCountWidget) !== 'undefined') {
                        VerticalTracksTotalCountWidget.init();
                    }

                    if (typeof (VerticalTracksEditionCountWidget) !== 'undefined') {
                        VerticalTracksEditionCountWidget.init();
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

    var showModal = function (verticalTrackUid) {
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
                        executeDelete(verticalTrackUid);
                    }
                }
            }
        });
    };

    return {
        showModal: function (verticalTrackUid) {
            showModal(verticalTrackUid);
        }
    };
}();
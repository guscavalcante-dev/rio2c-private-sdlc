// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 12-21-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-21-2023
// ***********************************************************************
// <copyright file="music.players.delete.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var MusicPlayersDelete = function () {

    // Delete -------------------------------------------------------------------------------------
    var executeDelete = function (organizationUid) {
        MyRio2cCommon.block();

        var jsonParameters = new Object();
        jsonParameters.organizationUid = organizationUid;

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Music/Players/Delete'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    if (typeof (MusicPlayersDataTableWidget) !== 'undefined') {
                        MusicPlayersDataTableWidget.refreshData();
                    }

                    if (typeof (MusicPlayersTotalCountWidget) !== 'undefined') {
                        MusicPlayersTotalCountWidget.init();
                    }

                    if (typeof (MusicPlayersEditionCountWidget) !== 'undefined') {
                        MusicPlayersEditionCountWidget.init();
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

    var showModal = function (organizationUid, isDeletingFromCurrentEdition) {
        var message = labels.deleteConfirmationMessage;

        if (isDeletingFromCurrentEdition) {
            message = labels.deleteCurrentEditionConfirmationMessage;
        }

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
                        executeDelete(organizationUid);
                    }
                }
            }
        });
    };

    return {
        showModal: function (organizationUid, isDeletingFromCurrentEdition) {
            showModal(organizationUid, isDeletingFromCurrentEdition);
        }
    };
}();
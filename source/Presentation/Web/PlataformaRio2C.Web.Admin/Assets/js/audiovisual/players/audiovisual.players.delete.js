// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-20-2021
// ***********************************************************************
// <copyright file="audiovisual.players.delete.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var AudiovisualPlayersDelete = function () {

    // Delete -------------------------------------------------------------------------------------
    var executeDelete = function (organizationUid) {
        MyRio2cCommon.block();

        var jsonParameters = new Object();
        jsonParameters.organizationUid = organizationUid;

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/Players/Delete'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    if (typeof (AudiovisualPlayersDataTableWidget) !== 'undefined') {
                        AudiovisualPlayersDataTableWidget.refreshData();
                    }

                    if (typeof (AudiovisualPlayersTotalCountWidget) !== 'undefined') {
                        AudiovisualPlayersTotalCountWidget.init();
                    }

                    if (typeof (AudiovisualPlayersEditionCountWidget) !== 'undefined') {
                        AudiovisualPlayersEditionCountWidget.init();
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
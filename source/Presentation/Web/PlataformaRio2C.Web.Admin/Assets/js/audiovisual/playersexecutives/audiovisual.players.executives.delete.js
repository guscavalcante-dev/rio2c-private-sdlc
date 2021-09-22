// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 08-26-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 09-14-2021
// ***********************************************************************
// <copyright file="audiovisual.players.executives.delete.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var PlayersExecutivesDelete = function () {

    // Delete -------------------------------------------------------------------------------------
    var executeDelete = function (collaboratorUid) {
        MyRio2cCommon.block();

        var jsonParameters = new Object();
        jsonParameters.collaboratorUid = collaboratorUid;

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/PlayersExecutives/Delete'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    if (typeof (PlayersExecutivesDataTableWidget) !== 'undefined') {
                        PlayersExecutivesDataTableWidget.refreshData();
                    }

                    if (typeof (PlayersExecutivesTotalCountWidget) !== 'undefined') {
                        PlayersExecutivesTotalCountWidget.init();
                    }

                    if (typeof (PlayersExecutivesEditionCountWidget) !== 'undefined') {
                        PlayersExecutivesEditionCountWidget.init();
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

    var showModal = function (collaboratorUid, isDeletingFromCurrentEdition) {
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
                        executeDelete(collaboratorUid);
                    }
                }
            }
        });
    };

    return {
        showModal: function (collaboratorUid, isDeletingFromCurrentEdition) {
            showModal(collaboratorUid, isDeletingFromCurrentEdition);
        }
    };
}();
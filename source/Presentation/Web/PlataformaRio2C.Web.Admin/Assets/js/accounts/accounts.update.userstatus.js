// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 09-10-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 09-10-2021
// ***********************************************************************
// <copyright file="accounts.update.userstatus.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var AccountsUpdateUserStatus = function () {

    //Update -------------------------------------------------------------
    var executeUpdateUserStatus = function (userUid, active) {
        var jsonParameters = new Object();
        jsonParameters.userUid = userUid;
        jsonParameters.active = active;

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Account/UpdateUserStatus'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                },
                // Error
                onError: function () {
                }
            });
        })
            .fail(function () {
            })
            .always(function () {
                if (typeof (AdministratorsDataTableWidget) !== 'undefined') {
                    AdministratorsDataTableWidget.refreshData();
                }
                if (typeof (PlayersExecutivesDataTableWidget) !== 'undefined') {
                    PlayersExecutivesDataTableWidget.refreshData();
                }
                MyRio2cCommon.unblock();
            });
    }

    var showModal = function (userUid, active) {
        var message = labels.blockConfirmationMessage;
        var label = labels.block;

        if (active == "true") {
            message = labels.unblockConfirmationMessage;
            label = labels.unblock;
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
                    label: label,
                    className: "btn btn-danger",
                    callback: function () {
                        executeUpdateUserStatus(userUid, active);
                    }
                }
            }
        });
    }

    return {
        showModal: function (userUid, active) {
            showModal(userUid, active);
        }
    };
}();
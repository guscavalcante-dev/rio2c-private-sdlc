// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 04-20-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 04-20-2021
// ***********************************************************************
// <copyright file="managers.delete.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var ManagersDelete = function () {

    // Delete -------------------------------------------------------------------------------------
    var executeDelete = function (collaboratorUid, collaboratorTypeName) {
        MyRio2cCommon.block();

        var jsonParameters = new Object();
        jsonParameters.CollaboratorUid = collaboratorUid;
        jsonParameters.CollaboratorTypeName = collaboratorTypeName;

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Collaborators/Managers/Delete'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    if (typeof (ManagersDataTableWidget) !== 'undefined') {
                        ManagersDataTableWidget.refreshData();
                    }

                    if (typeof (ManagersTotalCountWidget) !== 'undefined') {
                        ManagersTotalCountWidget.init();
                    }

                    if (typeof (ManagersEditionCountWidget) !== 'undefined') {
                        ManagersEditionCountWidget.init();
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

    var showModal = function (collaboratorUid, collaboratorTypeName, isDeletingFromCurrentEdition) {
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
                        executeDelete(collaboratorUid, collaboratorTypeName);
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
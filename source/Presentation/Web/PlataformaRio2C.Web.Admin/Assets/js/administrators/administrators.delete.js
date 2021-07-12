// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 04-20-2021
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-09-2021
// ***********************************************************************
// <copyright file="administrators.delete.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var AdministratorsDelete = function () {

    // Delete -------------------------------------------------------------------------------------
    var executeDelete = function (collaboratorUid, isDeletingFromCurrentEdition) {
        MyRio2cCommon.block();

        var jsonParameters = new Object();
        jsonParameters.CollaboratorUid = collaboratorUid;
        jsonParameters.IsDeletingFromCurrentEdition = isDeletingFromCurrentEdition;

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Administrators/Delete'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    if (typeof (AdministratorsDataTableWidget) !== 'undefined') {
                        AdministratorsDataTableWidget.refreshData();
                    }

                    if (typeof (AdministratorsTotalCountWidget) !== 'undefined') {
                        AdministratorsTotalCountWidget.init();
                    }

                    if (typeof (AdministratorsEditionCountWidget) !== 'undefined') {
                        AdministratorsEditionCountWidget.init();
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
                        executeDelete(collaboratorUid, isDeletingFromCurrentEdition);
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
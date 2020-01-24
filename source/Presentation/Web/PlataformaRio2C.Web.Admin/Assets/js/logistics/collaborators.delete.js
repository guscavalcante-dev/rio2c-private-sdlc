// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 08-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-26-2019
// ***********************************************************************
// <copyright file="collaborators.delete.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var CollaboratorsDelete = function () {

    // Delete -------------------------------------------------------------------------------------
    var executeDelete = function (collaboratorUid) {
        MyRio2cCommon.block();

        var jsonParameters = new Object();
        jsonParameters.collaboratorUid = collaboratorUid;

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/PlayersExecutives/Delete'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    if (typeof (CollaboratorsDataTableWidget) !== 'undefined') {
                        CollaboratorsDataTableWidget.refreshData();
                    }

                    if (typeof (CollaboratorsTotalCountWidget) !== 'undefined') {
                        CollaboratorsTotalCountWidget.init();
                    }

                    if (typeof (CollaboratorsEditionCountWidget) !== 'undefined') {
                        CollaboratorsEditionCountWidget.init();
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
// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 04-04-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 04-04-2021
// ***********************************************************************
// <copyright file="editions.delete.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var EditionsDelete = function () {

    // Delete -------------------------------------------------------------------------------------
    var executeDelete = function (editionUid) {
        MyRio2cCommon.block();

        var jsonParameters = new Object();
        jsonParameters.editionUid = editionUid;

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Editions/Delete'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    if (typeof (EditionsDataTableWidget) !== 'undefined') {
                        EditionsDataTableWidget.refreshData();
                    }

                    if (typeof (EditionsTotalCountWidget) !== 'undefined') {
                        EditionsTotalCountWidget.init();
                    }

                    if (typeof (EditionsCountWidget) !== 'undefined') {
                        EditionsCountWidget.init();
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

    var showModal = function (editionUid) {
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
                        executeDelete(editionUid);
                    }
                }
            }
        });
    };

    return {
        showModal: function (editionUid) {
            showModal(editionUid);
        }
    };
}();
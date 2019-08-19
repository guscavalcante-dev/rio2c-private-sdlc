// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="organizations.delete.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var OrganizationsDelete = function () {

    // Delete -------------------------------------------------------------------------------------
    var executeDelete = function (holdingUid) {
        MyRio2cCommon.block();

        var jsonParameters = new Object();
        jsonParameters.holdingUid = holdingUid;

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Players/Delete'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    if (!MyRio2cCommon.isNullOrEmpty(HoldingsDataTableWidget)) {
                        HoldingsDataTableWidget.refreshData();
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

    var showModal = function (holdingUid) {
        bootbox.dialog({
            message: labels.deleteConfirmationMessage,
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
                        executeDelete(holdingUid);
                    }
                }
            }
        });
    };

    return {
        showModal: function (holdingUid) {
            showModal(holdingUid);
        }
    };
}();
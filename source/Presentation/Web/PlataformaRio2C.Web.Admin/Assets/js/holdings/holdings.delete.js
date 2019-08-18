// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 08-16-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-18-2019
// ***********************************************************************
// <copyright file="holdings.delete.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var HoldingsDelete = function () {

    // Delete -------------------------------------------------------------------------------------
    var executeDelete = function (holdingUid) {
        MyRio2cCommon.block();

        var jsonParameters = new Object();
        jsonParameters.holdingUid = holdingUid;

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Holdings/Delete'), jsonParameters, function (data) {
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
            title: labels.deleteConfirmationTitle,
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
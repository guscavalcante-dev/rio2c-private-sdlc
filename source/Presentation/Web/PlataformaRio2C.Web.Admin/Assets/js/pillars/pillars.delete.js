﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-17-2020
// ***********************************************************************
// <copyright file="pillars.delete.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var PillarsDelete = function () {

    // Delete -------------------------------------------------------------------------------------
    var executeDelete = function (pillarUid) {
        MyRio2cCommon.block();

        var jsonParameters = new Object();
        jsonParameters.pillarUid = pillarUid;

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Pillars/Delete'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    if (typeof (PillarsDataTableWidget) !== 'undefined') {
                        PillarsDataTableWidget.refreshData();
                    }

                    if (typeof (PillarsTotalCountWidget) !== 'undefined') {
                        PillarsTotalCountWidget.init();
                    }

                    if (typeof (PillarsEditionCountWidget) !== 'undefined') {
                        PillarsEditionCountWidget.init();
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

    var showModal = function (pillarUid) {
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
                        executeDelete(pillarUid);
                    }
                }
            }
        });
    };

    return {
        showModal: function (pillarUid) {
            showModal(pillarUid);
        }
    };
}();
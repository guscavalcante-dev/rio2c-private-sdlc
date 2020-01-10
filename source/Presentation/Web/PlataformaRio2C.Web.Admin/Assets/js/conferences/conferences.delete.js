// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 01-02-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-02-2020
// ***********************************************************************
// <copyright file="conferences.delete.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var ConferencesDelete = function () {

    // Delete -------------------------------------------------------------------------------------
    var executeDelete = function (conferenceUid) {
        MyRio2cCommon.block();

        var jsonParameters = new Object();
        jsonParameters.conferenceUid = conferenceUid;

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Conferences/Delete'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    if (typeof (ConferencesDataTableWidget) !== 'undefined') {
                        ConferencesDataTableWidget.refreshData();
                    }

                    if (typeof (ConferencesTotalCountWidget) !== 'undefined') {
                        ConferencesTotalCountWidget.init();
                    }

                    if (typeof (ConferencesEditionCountWidget) !== 'undefined') {
                        ConferencesEditionCountWidget.init();
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

    var showModal = function (conferenceUid) {
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
                        executeDelete(conferenceUid);
                    }
                }
            }
        });
    };

    return {
        showModal: function (conferenceUid) {
            showModal(conferenceUid);
        }
    };
}();
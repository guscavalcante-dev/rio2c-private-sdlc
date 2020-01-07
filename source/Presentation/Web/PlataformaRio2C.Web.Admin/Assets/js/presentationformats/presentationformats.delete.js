// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 01-07-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="horizontaltracks.delete.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var PresentationFormatsDelete = function () {

    // Delete -------------------------------------------------------------------------------------
    var executeDelete = function (presentationFormatUid) {
        MyRio2cCommon.block();

        var jsonParameters = new Object();
        jsonParameters.presentationFormatUid = presentationFormatUid;

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/PresentationFormats/Delete'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    if (typeof (PresentationFormatsDataTableWidget) !== 'undefined') {
                        PresentationFormatsDataTableWidget.refreshData();
                    }

                    if (typeof (PresentationFormatsTotalCountWidget) !== 'undefined') {
                        PresentationFormatsTotalCountWidget.init();
                    }

                    if (typeof (PresentationFormatsEditionCountWidget) !== 'undefined') {
                        PresentationFormatsEditionCountWidget.init();
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

    var showModal = function (presentationFormatUid) {
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
                        executeDelete(presentationFormatUid);
                    }
                }
            }
        });
    };

    return {
        showModal: function (presentationFormatUid) {
            showModal(presentationFormatUid);
        }
    };
}();
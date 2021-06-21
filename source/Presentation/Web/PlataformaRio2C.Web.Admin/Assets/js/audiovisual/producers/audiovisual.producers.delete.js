// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 06-21-2021
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-21-2021
// ***********************************************************************
// <copyright file="audiovisual.producers.delete.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var AudiovisualProducersDelete = function () {

    // Delete -------------------------------------------------------------------------------------
    var executeDelete = function (organizationUid) {
        MyRio2cCommon.block();

        var jsonParameters = new Object();
        jsonParameters.organizationUid = organizationUid;

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/Producers/Delete'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    if (typeof (AudiovisualProducersDataTableWidget) !== 'undefined') {
                        AudiovisualProducersDataTableWidget.refreshData();
                    }

                    if (typeof (AudiovisualProducersTotalCountWidget) !== 'undefined') {
                        AudiovisualProducersTotalCountWidget.init();
                    }

                    if (typeof (AudiovisualProducersEditionCountWidget) !== 'undefined') {
                        AudiovisualProducersEditionCountWidget.init();
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

    var showModal = function (organizationUid, isDeletingFromCurrentEdition) {
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
                        executeDelete(organizationUid);
                    }
                }
            }
        });
    };

    return {
        showModal: function (organizationUid, isDeletingFromCurrentEdition) {
            showModal(organizationUid, isDeletingFromCurrentEdition);
        }
    };
}();
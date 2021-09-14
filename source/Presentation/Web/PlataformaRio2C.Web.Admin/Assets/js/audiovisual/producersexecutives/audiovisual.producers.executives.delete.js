// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 09-14-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 09-14-2021
// ***********************************************************************
// <copyright file="audiovisual.producers.executives.delete.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var ProducersExecutivesDelete = function () {

    // Delete -------------------------------------------------------------------------------------
    var executeDelete = function (collaboratorUid) {
        MyRio2cCommon.block();

        var jsonParameters = new Object();
        jsonParameters.collaboratorUid = collaboratorUid;

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/ProducersExecutives/Delete'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    if (typeof (ProducersExecutivesDataTableWidget) !== 'undefined') {
                        ProducersExecutivesDataTableWidget.refreshData();
                    }

                    if (typeof (ProducersExecutivesTotalCountWidget) !== 'undefined') {
                        ProducersExecutivesTotalCountWidget.init();
                    }

                    if (typeof (ProducersExecutivesEditionCountWidget) !== 'undefined') {
                        ProducersExecutivesEditionCountWidget.init();
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
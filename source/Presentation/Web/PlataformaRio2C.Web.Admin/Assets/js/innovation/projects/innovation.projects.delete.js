// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 07-24-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-24-2021
// ***********************************************************************
// <copyright file="innovation.projects.delete.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var InnovationProjectsDelete = function () {

    // Delete -------------------------------------------------------------------------------------
    var executeDelete = function (innovationProjectUid) {
        MyRio2cCommon.block();

        var jsonParameters = new Object();
        jsonParameters.innovationProjectUid = innovationProjectUid;

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Innovation/Projects/Delete'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    if (typeof (InnovationProjectsDataTableWidget) !== 'undefined') {
	                    InnovationProjectsDataTableWidget.refreshData();
                    }

                    if (typeof (InnovationProjectsTotalCountWidget) !== 'undefined') {
	                    InnovationProjectsTotalCountWidget.init();
                    }

                    if (typeof (InnovationProjectsEditionCountWidget) !== 'undefined') {
	                    InnovationProjectsEditionCountWidget.init();
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

    var showModal = function (innovationProjectUid) {
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
                        executeDelete(innovationProjectUid);
                    }
                }
            }
        });
    };

    return {
        showModal: function (innovationProjectUid) {
            showModal(innovationProjectUid);
        }
    };
}();
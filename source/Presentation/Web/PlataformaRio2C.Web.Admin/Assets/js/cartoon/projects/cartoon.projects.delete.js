// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 02-10-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-10-2022
// ***********************************************************************
// <copyright file="cartoon.projects.delete.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var CartoonProjectsDelete = function () {

    // Delete -------------------------------------------------------------------------------------
    var executeDelete = function (attendeeCartoonProjectUid) {
        MyRio2cCommon.block();

        var jsonParameters = new Object();
        jsonParameters.attendeeCartoonProjectUid = attendeeCartoonProjectUid;

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Cartoon/Projects/Delete'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    if (typeof (CartoonProjectsDataTableWidget) !== 'undefined') {
	                    CartoonProjectsDataTableWidget.refreshData();
                    }

                    if (typeof (CartoonProjectsTotalCountWidget) !== 'undefined') {
	                    CartoonProjectsTotalCountWidget.init();
                    }

                    if (typeof (CartoonProjectsEditionCountWidget) !== 'undefined') {
	                    CartoonProjectsEditionCountWidget.init();
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

    var showModal = function (attendeeCartoonProjectUid) {
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
                        executeDelete(attendeeCartoonProjectUid);
                    }
                }
            }
        });
    };

    return {
        showModal: function (attendeeCartoonProjectUid) {
            showModal(attendeeCartoonProjectUid);
        }
    };
}();
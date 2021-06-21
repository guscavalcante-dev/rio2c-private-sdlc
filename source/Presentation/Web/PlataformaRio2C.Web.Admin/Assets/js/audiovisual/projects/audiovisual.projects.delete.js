// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 06-21-2021
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-21-2021
// ***********************************************************************
// <copyright file="audiovisual.projects.delete.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var AudiovisualProjectsDelete = function () {

    // Delete -------------------------------------------------------------------------------------
    var executeDelete = function (projectUid) {
        MyRio2cCommon.block();

        var jsonParameters = new Object();
        jsonParameters.projectUid = projectUid;

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/Projects/Delete'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    if (typeof (AudiovisualProjectsDataTableWidget) !== 'undefined') {
                        AudiovisualProjectsDataTableWidget.refreshData();
                    }

                    if (typeof (AudiovisualProjectsTotalCountWidget) !== 'undefined') {
                        AudiovisualProjectsTotalCountWidget.init();
                    }

                    if (typeof (AudiovisualProjectsEditionCountWidget) !== 'undefined') {
                        AudiovisualProjectsEditionCountWidget.init();
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

    var showModal = function (projectUid) {
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
                        executeDelete(projectUid);
                    }
                }
            }
        });
    };

    return {
        showModal: function (projectUid) {
            showModal(projectUid);
        }
    };
}();
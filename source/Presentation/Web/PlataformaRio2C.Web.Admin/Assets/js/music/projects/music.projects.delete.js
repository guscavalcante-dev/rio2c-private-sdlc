// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 03-01-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-01-2020
// ***********************************************************************
// <copyright file="music.projects.delete.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var MusicProjectsDelete = function () {

    // Delete -------------------------------------------------------------------------------------
    var executeDelete = function (musicProjectUid) {
        MyRio2cCommon.block();

        var jsonParameters = new Object();
        jsonParameters.musicProjectUid = musicProjectUid;

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Music/Projects/Delete'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    if (typeof (MusicProjectsDataTableWidget) !== 'undefined') {
	                    MusicProjectsDataTableWidget.refreshData();
                    }

                    if (typeof (MusicProjectsTotalCountWidget) !== 'undefined') {
	                    MusicProjectsTotalCountWidget.init();
                    }

                    if (typeof (MusicProjectsEditionCountWidget) !== 'undefined') {
	                    MusicProjectsEditionCountWidget.init();
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

    var showModal = function (musicProjectUid) {
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
                        executeDelete(musicProjectUid);
                    }
                }
            }
        });
    };

    return {
        showModal: function (musicProjectUid) {
            showModal(musicProjectUid);
        }
    };
}();
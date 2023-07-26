// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 07-26-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="innovation.trackoptions.delete.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var InnovationTrackOptionsDelete = function () {

    // Delete -------------------------------------------------------------------------------------
    var executeDelete = function (innovationOrganizationTrackOptionUid) {
        MyRio2cCommon.block();

        var jsonParameters = new Object();
        jsonParameters.innovationOrganizationTrackOptionUid = innovationOrganizationTrackOptionUid;

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Innovation/TrackOptions/Delete'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    if (typeof (InnovationTrackOptionsDataTableWidget) !== 'undefined') {
                        InnovationTrackOptionsDataTableWidget.refreshData();
	                }

                    if (typeof (InnovationTrackOptionsEditionCountWidget) !== 'undefined') {
                        InnovationTrackOptionsEditionCountWidget.init();
	                }

                    if (typeof (InnovationTrackOptionsTotalCountWidget) !== 'undefined') {
                        InnovationTrackOptionsTotalCountWidget.init();
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

    var showModal = function (innovationOrganizationTrackOptionUid) {
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
                        executeDelete(innovationOrganizationTrackOptionUid);
                    }
                }
            }
        });
    };

    return {
        showModal: function (innovationOrganizationTrackOptionUid) {
            showModal(innovationOrganizationTrackOptionUid);
        }
    };
}();
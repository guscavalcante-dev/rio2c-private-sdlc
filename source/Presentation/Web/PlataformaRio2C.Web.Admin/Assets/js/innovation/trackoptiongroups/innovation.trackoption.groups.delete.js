// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 07-03-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-17-2023
// ***********************************************************************
// <copyright file="innovation.trackoption.groups.delete.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var InnovationTrackOptionGroupsDelete = function () {

    // Delete -------------------------------------------------------------------------------------
    var executeDelete = function (innovationOrganizationTrackOptionGroupUid) {
        MyRio2cCommon.block();

        var jsonParameters = new Object();
        jsonParameters.innovationOrganizationTrackOptionGroupUid = innovationOrganizationTrackOptionGroupUid;

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Innovation/TrackOptionGroups/Delete'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    if (typeof (InnovationTrackOptionGroupsDataTableWidget) !== 'undefined') {
		                InnovationTrackOptionGroupsDataTableWidget.refreshData();
	                }

                    if (typeof (InnovationTrackOptionGroupsTotalCountWidget) !== 'undefined') {
		                InnovationTrackOptionGroupsTotalCountWidget.init();
	                }

                    if (typeof (InnovationTrackOptionGroupsEditionCountWidget) !== 'undefined') {
		                InnovationTrackOptionGroupsEditionCountWidget.init();
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
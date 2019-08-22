// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-21-2019
// ***********************************************************************
// <copyright file="organizations.delete.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var OrganizationsDelete = function () {

    // Delete -------------------------------------------------------------------------------------
    var executeDelete = function (organizationUid) {
        MyRio2cCommon.block();

        var jsonParameters = new Object();
        jsonParameters.organizationUid = organizationUid;

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Players/Delete'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    if (typeof (OrganizationsDataTableWidget) !== 'undefined') {
                        OrganizationsDataTableWidget.refreshData();
                    }

                    if (typeof (OrganizationsTotalCountWidget) !== 'undefined') {
                        OrganizationsTotalCountWidget.init();
                    }

                    if (typeof (OrganizationsEditionCountWidget) !== 'undefined') {
                        OrganizationsEditionCountWidget.init();
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

    var showModal = function (organizationUid) {
        bootbox.dialog({
            message: labels.deleteConfirmationMessage,
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
        showModal: function (organizationUid) {
            showModal(organizationUid);
        }
    };
}();
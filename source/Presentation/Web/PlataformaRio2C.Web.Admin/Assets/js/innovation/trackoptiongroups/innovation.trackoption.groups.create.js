// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 07-03-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-13-2023
// ***********************************************************************
// <copyright file="innovation.trackoptions.groups.create.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var InnovationTrackOptionGroupsCreate = function () {

    var modalId = '#CreateTrackOptionGroupModal';
    var formId = '#CreateTrackOptionGroupForm';

    // Enable form validation ---------------------------------------------------------------------
    var enableFormValidation = function () {
        MyRio2cCommon.enableFormValidation({ formIdOrClass: formId, enableHiddenInputsValidation: true, enableMaxlength: true });
    };

    // Enable plugins -----------------------------------------------------------------------------
    var enablePlugins = function () {
        enableAjaxForm();
        enableFormValidation();
    };

    // Show modal ---------------------------------------------------------------------------------
    var showModal = function () {
        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Innovation/TrackOptionGroups/ShowCreateModal'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    enablePlugins();
                    $(modalId).modal();
                },
                // Error
                onError: function() {
                }
            });
        })
        .fail(function () {
        })
        .always(function () {
            MyRio2cCommon.unblock();
        });
    };

    // Enable ajax form ---------------------------------------------------------------------------
    var enableAjaxForm = function () {
        MyRio2cCommon.enableAjaxForm({
            idOrClass: formId,
            onSuccess: function (data) {
                $(modalId).modal('hide');

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
            onError: function (data) {
                if (MyRio2cCommon.hasProperty(data, 'pages')) {
                    enablePlugins();
                }

                $(formId).find(":input.input-validation-error:first").focus();
            }
        });
    };

    return {
        showModal: function () {
            showModal();
        }
    };
}();
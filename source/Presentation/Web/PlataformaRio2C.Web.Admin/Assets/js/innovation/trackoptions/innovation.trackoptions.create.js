// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 07-03-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-25-2023
// ***********************************************************************
// <copyright file="innovation.trackoptions.create.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var InnovationTrackOptionsCreate = function () {

    var modalId = '#CreateTrackOptionModal';
    var formId = '#CreateTrackOptionForm';

    // Enable plugins -----------------------------------------------------------------------------
    var enablePlugins = function () {
        MyRio2cCommon.enableSelect2({ inputIdOrClass: formId + ' .enable-select2' });
        enableAjaxForm();
        MyRio2cCommon.enableFormValidation({ formIdOrClass: formId, enableHiddenInputsValidation: true, enableMaxlength: true });
    };

    // Show modal ---------------------------------------------------------------------------------
    var showModal = function () {
        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Innovation/TrackOptions/ShowCreateModal'), jsonParameters, function (data) {
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

                if (typeof (InnovationTrackOptionsDataTableWidget) !== 'undefined') {
                    InnovationTrackOptionsDataTableWidget.refreshData();
                }

                if (typeof (InnovationTrackOptionsTotalCountWidget) !== 'undefined') {
	                InnovationTrackOptionsTotalCountWidget.init();
                }

                if (typeof (InnovationTrackOptionsEditionCountWidget) !== 'undefined') {
	                InnovationTrackOptionsEditionCountWidget.init();
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
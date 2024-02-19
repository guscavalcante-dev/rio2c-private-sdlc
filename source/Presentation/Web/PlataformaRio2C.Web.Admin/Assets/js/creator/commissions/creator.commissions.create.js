// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 02-14-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-14-2024
// ***********************************************************************
// <copyright file="creator.commissions.create.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var CreatorCommissionsCreate = function () {

    var modalId = '#CreateCommissionModal';
    var formId = '#CreateCommissionForm';

    // Enable form validation ---------------------------------------------------------------------
    var enableFormValidation = function () {
        MyRio2cCommon.enableFormValidation({ formIdOrClass: formId, enableHiddenInputsValidation: true, enableMaxlength: true });
    };

    // Enable plugins -----------------------------------------------------------------------------
    var enablePlugins = function () {
        enableAjaxForm();
        MyRio2cCommon.enableAtLeastOnCheckboxByNameValidation(formId);
        enableFormValidation();
    };

    // Show modal ---------------------------------------------------------------------------------
    var showModal = function () {
        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Creator/Commissions/ShowCreateModal'), jsonParameters, function (data) {
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

                if (typeof (CreatorCommissionsDataTableWidget) !== 'undefined') {
	                CreatorCommissionsDataTableWidget.refreshData();
                }

                if (typeof (CreatorCommissionsTotalCountWidget) !== 'undefined') {
	                CreatorCommissionsTotalCountWidget.init();
                }

                if (typeof (CreatorCommissionsEditionCountWidget) !== 'undefined') {
	                CreatorCommissionsEditionCountWidget.init();
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
// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 02-25-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-25-2020
// ***********************************************************************
// <copyright file="music.commissions.update.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var MusicCommissionsUpdate = function () {

    var modalId = '#UpdateCommissionModal';
    var formId = '#UpdateCommissionForm';

    // Enable form validation ---------------------------------------------------------------------
    var enableFormValidation = function () {
        MyRio2cCommon.enableFormValidation({ formIdOrClass: formId, enableHiddenInputsValidation: true, enableMaxlength: true });
    };

    // Enable plugins -----------------------------------------------------------------------------
    var enablePlugins = function () {
        enableAjaxForm();
        enableFormValidation();
        MyRio2cCommon.enableSelect2({ inputIdOrClass: formId + ' .enable-select2' });
    };

    // Show modal ---------------------------------------------------------------------------------
    var showModal = function (collaboratorUid, isAddingToCurrentEdition) {
        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();
        jsonParameters.collaboratorUid = collaboratorUid;
        jsonParameters.isAddingToCurrentEdition = isAddingToCurrentEdition;

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Music/Commissions/ShowUpdateModal'), jsonParameters, function (data) {
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

                if (typeof (MusicCommissionsDataTableWidget) !== 'undefined') {
	                MusicCommissionsDataTableWidget.refreshData();
                }

                if (typeof (MusicCommissionsTotalCountWidget) !== 'undefined') {
	                MusicCommissionsTotalCountWidget.init();
                }

                if (typeof (MusicCommissionsEditionCountWidget) !== 'undefined') {
	                MusicCommissionsEditionCountWidget.init();
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
        showModal: function (collaboratorUid, isAddingToCurrentEdition) {
            showModal(collaboratorUid, isAddingToCurrentEdition);
        }
    };
}();
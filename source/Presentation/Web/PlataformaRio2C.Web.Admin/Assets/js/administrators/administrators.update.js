// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 04-20-2021
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-09-2021
// ***********************************************************************
// <copyright file="administrators.update.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var AdministratorsUpdate = function () {

    var modalId = '#UpdateAdministratorModal';
    var formId = '#UpdateAdministratorForm';

    // Enable form validation ---------------------------------------------------------------------
    var enableFormValidation = function () {
        MyRio2cCommon.enableFormValidation({ formIdOrClass: formId, enableHiddenInputsValidation: true, enableMaxlength: true });
    };

    // Enable plugins -----------------------------------------------------------------------------
    var enablePlugins = function () {
        if (typeof (MyRio2cPublicEmail) !== 'undefined') {
            MyRio2cPublicEmail.init();
        }

        MyRio2cCropper.init({ formIdOrClass: formId });
        MyRio2cCommon.enableSelect2({ inputIdOrClass: formId + ' .enable-select2' });
        MyRio2cCommon.enableDropdownChangeEvent("RoleName", "Role");
        enableAjaxForm();
        enableFormValidation();
    };

    // Show modal ---------------------------------------------------------------------------------
    var showModal = function (collaboratorUid, isAddingToCurrentEdition) {
        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();
        jsonParameters.collaboratorUid = collaboratorUid;
        jsonParameters.isAddingToCurrentEdition = isAddingToCurrentEdition;

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Administrators/ShowUpdateModal'), jsonParameters, function (data) {
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

                if (typeof (AdministratorsDataTableWidget) !== 'undefined') {
                    AdministratorsDataTableWidget.refreshData();
                }

                if (typeof (AdministratorsTotalCountWidget) !== 'undefined') {
                    AdministratorsTotalCountWidget.init();
                }

                if (typeof (AdministratorsEditionCountWidget) !== 'undefined') {
                    AdministratorsEditionCountWidget.init();
                }

                if (typeof (AdministratorsMainInformationWidget) !== 'undefined') {
                    AdministratorsMainInformationWidget.init();
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
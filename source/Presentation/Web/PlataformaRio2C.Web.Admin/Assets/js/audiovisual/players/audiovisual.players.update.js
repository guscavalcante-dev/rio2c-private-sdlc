// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-20-2021
// ***********************************************************************
// <copyright file="audiovisual.players.update.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var AudiovisualPlayersUpdate = function () {

    var modalId = '#UpdatePlayerModal';
    var formId = '#UpdatePlayerForm';

    // Enable form validation ---------------------------------------------------------------------
    var enableFormValidation = function () {
        MyRio2cCommon.enableFormValidation({ formIdOrClass: formId, enableHiddenInputsValidation: true, enableMaxlength: true });
    };

    // Enable plugins -----------------------------------------------------------------------------
    var enablePlugins = function () {
        MyRio2cCropper.init({ formIdOrClass: formId });
        MyRio2cCommon.enableSelect2({ inputIdOrClass: formId + ' .enable-select2' });
        AddressesForm.init();
        //MyRio2cCommon.enableCkEditor({ idOrClass: '.ckeditor-rio2c', maxCharCount: 710 });
        //MyRio2cCommon.enableCkEditor({ idOrClass: '.ckeditor-rio2c-restrictions', maxCharCount: 270 });
        enableAjaxForm();
        enableFormValidation();

        // Enable additional info textbox
        if (typeof (MyRio2cCommonAdditionalInfo) !== 'undefined') {
            MyRio2cCommonAdditionalInfo.init();
        }
    };

    // Show modal ---------------------------------------------------------------------------------
    var showModal = function (organizationUid, isAddingToCurrentEdition) {
        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();
        jsonParameters.organizationUid = organizationUid;
        jsonParameters.isAddingToCurrentEdition = isAddingToCurrentEdition;

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/Players/ShowUpdateModal'), jsonParameters, function (data) {
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

                if (typeof (AudiovisualPlayersDataTableWidget) !== 'undefined') {
                    AudiovisualPlayersDataTableWidget.refreshData();
                }

                if (typeof (AudiovisualPlayersTotalCountWidget) !== 'undefined') {
                    AudiovisualPlayersTotalCountWidget.init();
                }

                if (typeof (AudiovisualPlayersEditionCountWidget) !== 'undefined') {
                    AudiovisualPlayersEditionCountWidget.init();
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
        showModal: function (organizationUid, isAddingToCurrentEdition) {
            showModal(organizationUid, isAddingToCurrentEdition);
        }
    };
}();
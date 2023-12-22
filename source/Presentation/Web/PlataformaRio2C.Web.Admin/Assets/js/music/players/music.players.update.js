// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 12-21-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-21-2023
// ***********************************************************************
// <copyright file="music.players.update.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var MusicPlayersUpdate = function () {

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

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Music/Players/ShowUpdateModal'), jsonParameters, function (data) {
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

                if (typeof (MusicPlayersDataTableWidget) !== 'undefined') {
                    MusicPlayersDataTableWidget.refreshData();
                }

                if (typeof (MusicPlayersTotalCountWidget) !== 'undefined') {
                    MusicPlayersTotalCountWidget.init();
                }

                if (typeof (MusicPlayersEditionCountWidget) !== 'undefined') {
                    MusicPlayersEditionCountWidget.init();
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
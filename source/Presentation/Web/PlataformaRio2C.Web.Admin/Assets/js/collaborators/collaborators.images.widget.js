// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 04-15-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 04-15-2023
// ***********************************************************************
// <copyright file="collaborators.images.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var CollaboratorsImagesWidget = function () {

    var widgetElementId = '#CollaboratorImagesWidget';
    var widgetElement = $(widgetElementId);

    // Show ---------------------------------------------------------------------------------------
    var enableShowPlugins = function () {
        KTApp.initTooltips();
    };

    var show = function () {
        if (widgetElement.length <= 0) {
            return;
        }

        var jsonParameters = new Object();
        jsonParameters.collaboratorUid = $('#AggregateId').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Collaborators/ShowCollaboratorImagesWidget'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    enableShowPlugins();
                },
                // Error
                onError: function () {
                }
            });
        })
        .fail(function () {
        })
        .always(function () {
            MyRio2cCommon.unblock({ idOrClass: widgetElementId });
        });
    };

    // Update -------------------------------------------------------------------------------------
    var enableAjaxForm = function () {
        MyRio2cCommon.enableAjaxForm({
            idOrClass: updateFormId,
            onSuccess: function (data) {
                $(updateModalId).modal('hide');

                if (typeof (CollaboratorImagesWidget) !== 'undefined') {
                    CollaboratorImagesWidget.init();
                }
            },
            onError: function (data) {
                if (MyRio2cCommon.hasProperty(data, 'pages')) {
                    enableUpdatePlugins();
                }

                $(updateFormId).find(":input.input-validation-error:first").focus();
            }
        });
    };

    var enableUpdatePlugins = function () {
        enableAjaxForm();
        MyRio2cCommon.enableFormValidation({ formIdOrClass: updateFormId, enableHiddenInputsValidation: true, enableMaxlength: true });
    };

    return {
        init: function () {
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            show();
        }
    };
}();
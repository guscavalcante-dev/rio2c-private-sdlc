// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 08-14-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-14-2021
// ***********************************************************************
// <copyright file="audiovisual.commissions.evaluations.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var AudiovisualCommissionsEvaluationsWidget = function () {

    var widgetElementId = '#AudiovisualCommissionEvaluationsWidget';
    var widgetElement = $(widgetElementId);

    var updateModalId = '#UpdateEvaluationsModal';
    var updateFormId = '#UpdateEvaluationsForm';

    // Show ---------------------------------------------------------------------------------------
    var enableShowPlugins = function () {
        KTApp.initTooltips();
        MyRio2cCommon.initScroll();
    };

    var show = function () {
        if (widgetElement.length <= 0) {
            return;
        }

        var jsonParameters = new Object();
        jsonParameters.collaboratorUid = $('#AggregateId').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/Commissions/ShowEvaluationsWidget'), jsonParameters, function (data) {
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

                if (typeof (AudiovisualCommissionsEvaluationsWidget) !== 'undefined') {
                    AudiovisualCommissionsEvaluationsWidget.init();
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
        MyRio2cCommon.enableAtLeastOnCheckboxByNameValidation(updateFormId);
        MyRio2cCommon.enableFormValidation({ formIdOrClass: updateFormId, enableHiddenInputsValidation: true, enableMaxlength: true });
    };

    return {
        init: function () {
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            show();
        }
    };
}();
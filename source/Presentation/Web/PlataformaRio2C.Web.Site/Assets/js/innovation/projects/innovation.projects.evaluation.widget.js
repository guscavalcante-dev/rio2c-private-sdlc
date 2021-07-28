// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Renan Valentim
// Created          : 07-28-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-28-2021
// ***********************************************************************
// <copyright file="innovation.projects.evaluation.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var InnovationProjectsEvaluationWidget = function () {

    var widgetElementId = '#ProjectEvaluationWidget';
    var widgetElement;

    var acceptModalId = '#AcceptEvaluationModal';
    var acceptFormId = '#AcceptEvaluationForm';
    var refuseModalId = '#RefuseEvaluationModal';
    var refuseFormId = '#RefuseEvaluationForm';

    // Initialize Elements ------------------------------------------------------------------------
    var initElements = function () {
        widgetElement = $(widgetElementId);
    };

    // Show ---------------------------------------------------------------------------------------
    var enableShowPlugins = function () {
        KTApp.initTooltips();
        MyRio2cCommon.initScroll();
        MyRio2cCommon.enableDecimal('decimal-globalize-mask', 15);
    };

    var show = function () {
        if (widgetElement.length <= 0) {
            return;
        }

        var jsonParameters = new Object();
        jsonParameters.attendeeInnovationOrganizationUid = $('#AggregateId').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Innovation/Projects/ShowEvaluationGradeWidget'), jsonParameters, function (data) {
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
                //showAlert();
                //MyRio2cCommon.unblock(widgetElementId);
            })
            .always(function () {
                MyRio2cCommon.unblock({ idOrClass: widgetElementId });
            });
    };

    // Evaluation Grade ---------------------------------------------------------------------------
    var submitEvaluationGrade = function (innovationOrganizationId) {
        var jsonParameters = new Object();
        jsonParameters.innovationOrganizationId = innovationOrganizationId;
        jsonParameters.grade = $('#AttendeeInnovationOrganizationEvaluationGrade').val();

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Innovation/Projects/Evaluate'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
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
                InnovationProjectsEvaluationWidget.init();
                InnovationProjectsEvaluatorsWidget.init();
                InnovationProjectsMainInformationWidget.init();
            });
    };

    return {
        init: function () {
            initElements();
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            show();
        },
        submitEvaluationGrade: function (innovationOrganizationId) {
            submitEvaluationGrade(innovationOrganizationId);
        }
    };
}();
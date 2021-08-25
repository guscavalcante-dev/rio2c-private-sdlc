// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 07-24-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-24-2021
// ***********************************************************************
// <copyright file="audiovisual.projects.evaluation.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var AudiovisualProjectsEvaluationWidget = function () {

    var widgetElementId = '#ProjectEvaluationWidget';
    var widgetElement;

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
        jsonParameters.projectUid = $('#AggregateId').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/Projects/ShowEvaluationGradeWidget'), jsonParameters, function (data) {
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

    // Evaluation Grade ---------------------------------------------------------------------------
    var submitEvaluationGrade = function (projectId) {
        var jsonParameters = new Object();
        jsonParameters.projectId = projectId;
        jsonParameters.grade = $('#ProjectCommissionEvaluationGrade').val();

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/Projects/Evaluate'), jsonParameters, function (data) {
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
                AudiovisualProjectsEvaluationWidget.init();
                AudiovisualProjectsEvaluatorsWidget.init();
                AudiovisualProjectsMainInformationWidget.init();
            });
    };

    return {
        init: function () {
            initElements();
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            show();
        },
        submitEvaluationGrade: function (projectId) {
            submitEvaluationGrade(projectId);
        }
    };
}();
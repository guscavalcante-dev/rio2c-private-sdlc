// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Renan Valentim
// Created          : 02-27-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-27-2024
// ***********************************************************************
// <copyright file="creator.projects.evaluation.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var CreatorProjectsEvaluationWidget = function () {

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
        jsonParameters.attendeeCreatorProjectUid = $('#AggregateId').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Creator/Projects/ShowEvaluationGradeWidget'), jsonParameters, function (data) {
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
    var submitEvaluationGrade = function (creatorProjectId) {
        var jsonParameters = new Object();
        jsonParameters.creatorProjectId = creatorProjectId;
        jsonParameters.grade = $('#AttendeeCreatorProjectEvaluationGrade').val();

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Creator/Projects/Evaluate'), jsonParameters, function (data) {
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
                CreatorProjectsEvaluationWidget.init();
                CreatorProjectsEvaluatorsWidget.init();
                CreatorProjectsMainInformationWidget.init();
            });
    };

    return {
        init: function () {
            initElements();
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            show();
        },
        submitEvaluationGrade: function (creatorProjectId) {
            submitEvaluationGrade(creatorProjectId);
        }
    };
}();
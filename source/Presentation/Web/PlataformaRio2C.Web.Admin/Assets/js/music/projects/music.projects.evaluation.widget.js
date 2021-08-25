// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 03-01-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-01-2020
// ***********************************************************************
// <copyright file="music.projects.evaluation.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var MusicProjectsEvaluationWidget = function () {

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

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Music/Projects/ShowEvaluationGradeWidget'), jsonParameters, function (data) {
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
    var submitEvaluationGrade = function (musicBandId) {
        var jsonParameters = new Object();
        jsonParameters.musicBandId = musicBandId;
        jsonParameters.grade = $('#AttendeeMusicBandEvaluationGrade').val();

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Music/Projects/Evaluate'), jsonParameters, function (data) {
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
                MusicProjectsEvaluationWidget.init();
                MusicProjectsEvaluatorsWidget.init();
                MusicProjectsMainInformationWidget.init();
            });
    };

    return {
        init: function () {
            initElements();
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            show();
        },
        submitEvaluationGrade: function (musicBandId) {
            submitEvaluationGrade(musicBandId);
        }
    };
}();
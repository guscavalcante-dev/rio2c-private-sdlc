 // ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 02-28-2020
//
// Last Modified By : Daniel Giese Rodrigues
// Last Modified On : 01-07-2025
// ***********************************************************************
// <copyright file="music.projects.maininformation.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var MusicProjectsMainInformationWidget = function () {

    var widgetElementId = '#ProjectMainInformationWidget';
    var widgetElement = $(widgetElementId);

    var updateModalId = '#UpdateMainInformationModal';
    var updateFormId = '#UpdateMainInformationForm';

    // Show ---------------------------------------------------------------------------------------
    var enableShowPlugins = function () {
        KTApp.initTooltips();
        MyRio2cCommon.initScroll();
        widgetElement.on('click', '.clickable-band-image', function () {
            var fullImageUrl = $(this).data('full-image');
            $('#FullBandImage').attr('src', fullImageUrl);
            $('#ImageFullModal').modal('show');
        });
    };

    var show = function () {
        if (widgetElement.length <= 0) {
            return;
        }

        var jsonParameters = new Object();
        jsonParameters.projectUid = $('#AggregateId').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Music/PitchingProjects/ShowMainInformationWidget'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    enableShowPlugins();
                },
                // Error
                onError: function() {
                }
            });
        })
        .fail(function () {
            //showAlert();
            //MyRio2cCommon.unblock(widgetElementId);
        })
        .always(function() {
            MyRio2cCommon.unblock({ idOrClass: widgetElementId });
        });
    };

    // Evaluation Grade ---------------------------------------------------------------------------
    var submitEvaluationGrade = function (musicBandId) {
        var jsonParameters = new Object();
        jsonParameters.musicBandId = musicBandId;
        jsonParameters.grade = $('#AttendeeMusicBandEvaluationGradeMain').val();

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Music/PitchingProjects/Evaluate'), jsonParameters, function (data) {
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
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            show();
        },
        submitEvaluationGrade: function (musicBandId) {
            submitEvaluationGrade(musicBandId);
        },
    };
}();
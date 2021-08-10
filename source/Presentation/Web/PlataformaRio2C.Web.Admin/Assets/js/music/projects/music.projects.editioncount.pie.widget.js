// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 08-06-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-06-2021
// ***********************************************************************
// <copyright file="music.projects.editioncount.pie.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var MusicProjectsEditionCountPieWidget = function () {

    var widgetElementId = '#MusicProjectsEditionCountPieWidget';
    var chartElementId = 'MusicProjectsEditionCountPieChart';
    var widgetElement = $(widgetElementId);

    // Show ---------------------------------------------------------------------------------------
    var initChart = function (data, musicBandsTotalCount) {
        if ($('#' + chartElementId).length === 0) {
            return;
        }

        am4core.ready(function () {
            am4core.useTheme(am4themes_animated);

            var totalCountLabelId = "TotalCountLabel";
            var observationMessageLabelId = "ObservationMessageLabel";

            var chart = am4core.create(chartElementId, am4charts.PieChart);
            chart.language.locale = am4lang_pt_BR;
            //chart.radius = am4core.percent(70);
            chart.innerRadius = am4core.percent(50);
            chart.data = data;
            chart.paddingLeft = 5;
            chart.paddingRight = 5;
            chart.paddingBottom = 20;
            chart.responsive.enabled = true;
            chart.responsive.rules.push({
                relevant: function (target) {
                    if (target.pixelWidth <= 600) {
                        return true;
                    }
                    return false;
                },
                state: function (target, stateId) {
                    if (target instanceof am4charts.PieSeries) {
                        var state = target.states.create(stateId);

                        var labelState = target.labels.template.states.create(stateId);
                        labelState.properties.disabled = true;

                        var tickState = target.ticks.template.states.create(stateId);
                        tickState.properties.disabled = true;
                        return state;
                    }

                    if (target instanceof am4core.Label) {
                        if (!MyRio2cCommon.isNullOrEmpty(target.html)) {

                            //ObservationMessageLabel
                            if (target.id == observationMessageLabelId) {
                                var state = target.states.create(stateId);
                                state.properties.visible = false;
                                return state;
                            }

                            //TotalCountLabel
                            if (target.id == totalCountLabelId) {
                            }
                        }
                    }

                    return null;
                }
            });

            // Label - Music Bands Total Count         
            var label = chart.seriesContainer.createChild(am4core.Label);
            label.isMeasured = false;
            label.horizontalCenter = "middle";
            label.verticalCenter = "bottom";
            label.fontSize = 30;
            label.html = musicBandsTotalCount;
            label.id = totalCountLabelId;

            // Label2 - Music Projects
            var label2 = chart.seriesContainer.createChild(am4core.Label);
            label2.isMeasured = false;
            label2.horizontalCenter = "middle";
            label2.verticalCenter = "top";
            label2.fontSize = 15;
            label2.html = musicProjects;
            label2.wrap = true;
            label2.maxWidth = 120;
            label2.textAlign = "middle";

             //Label3 - Additional Info
            let label3 = chart.createChild(am4core.Label);
            label3.fontSize = 12;
            label3.isMeasured = false;
            label3.x = am4core.percent(10);
            label3.y = am4core.percent(100);
            label3.html = musicProjectsObservationMessage;
            label3.id = observationMessageLabelId;

            // Add and configure Series
            var pieSeries = chart.series.push(new am4charts.PieSeries());
            pieSeries.dataFields.value = "MusicBandsTotalCount";
            pieSeries.dataFields.category = "MusicGenreName";
            pieSeries.alignLabels = false;
            pieSeries.labels.template.horizontalCenter = "middle";
            pieSeries.labels.template.verticalCenter = "bottom";
            //pieSeries.labels.template.fontSize = 10;

            // Hide label with values less than minimun configured
            pieSeries.ticks.template.events.on("ready", hideSmall);
            pieSeries.ticks.template.events.on("visibilitychanged", hideSmall);
            pieSeries.labels.template.events.on("ready", hideSmall);
            pieSeries.labels.template.events.on("visibilitychanged", hideSmall);
            pieSeries.labels.template.maxWidth = 130;
            pieSeries.labels.template.wrap = true;
            function hideSmall(ev) {
                if (ev.target.dataItem && (ev.target.dataItem.values.value.percent < 2)) {
                    ev.target.hide();
                }
                else {
                    ev.target.show();
                }
            }
        });
    };

    var enableShowPlugins = function (data, musicBandsTotalCount) {
        initChart(data, musicBandsTotalCount);
    };

    var show = function () {
        var jsonParameters = new Object();
        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Music/Projects/ShowEditionCountPieWidget'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    enableShowPlugins(data.musicBandGroupedByGenreDtos, data.musicBandsTotalCount);
                },
                // Error
                onError: function () {
                }
            });
        })
        .fail(function () {
        })
        .always(function() {
            MyRio2cCommon.unblock({ idOrClass: widgetElementId });
        });
    };

    return {
        init: function () {
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            show();
        }
    };
}();
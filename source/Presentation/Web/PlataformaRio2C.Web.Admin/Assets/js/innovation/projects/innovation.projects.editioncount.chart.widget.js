// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 08-07-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-07-2021
// ***********************************************************************
// <copyright file="innovation.projects.editioncount.chart.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var InnovationProjectsEditionCountChartWidget = function () {

    var widgetElementId = '#InnovationProjectsEditionCountChartWidget';
    var chartElementId = 'InnovationProjectsEditionCountChart';
    var widgetElement = $(widgetElementId);

    // Show ---------------------------------------------------------------------------------------
    var initChart = function (data, innovationProjectsTotalCount) {
        if ($('#' + chartElementId).length === 0) {
            return;
        }

        am4core.ready(function () {
            am4core.useTheme(am4themes_animated);

            // Create chart instance
            var chart = am4core.create(chartElementId, am4charts.PieChart);
            //chart.radius = am4core.percent(70);
            chart.innerRadius = am4core.percent(50);
            chart.data = data;
            chart.paddingLeft = 5;
            chart.paddingRight = 5;
            chart.paddingBottom = 20;

            // Add labels         
            var label = chart.seriesContainer.createChild(am4core.Label);
            label.isMeasured = false;
            label.horizontalCenter = "middle";
            label.verticalCenter = "bottom";
            label.fontSize = 30;
            label.text = innovationProjectsTotalCount;

            var label2 = chart.seriesContainer.createChild(am4core.Label);
            label2.isMeasured = false;
            label2.horizontalCenter = "middle";
            label2.verticalCenter = "top";
            label2.fontSize = 15;
            label2.text = innovationProjects;
            label2.wrap = true;
            label2.maxWidth = 120;
            label2.textAlign = "middle";

            // Add and configure Series
            var pieSeries = chart.series.push(new am4charts.PieSeries());
            pieSeries.dataFields.value = "InnovationProjectsTotalCount";
            pieSeries.dataFields.category = "TrackName";
            pieSeries.alignLabels = false;
            pieSeries.labels.template.horizontalCenter = "middle";
            pieSeries.labels.template.verticalCenter = "middle";
            //pieSeries.labels.template.fontSize = 10;

            // Hide label with values less than minimun configured
            pieSeries.ticks.template.events.on("ready", hideSmall);
            pieSeries.ticks.template.events.on("visibilitychanged", hideSmall);
            pieSeries.labels.template.events.on("ready", hideSmall);
            pieSeries.labels.template.events.on("visibilitychanged", hideSmall);
            pieSeries.labels.template.maxWidth = 150;
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

    var enableShowPlugins = function (data, innovationProjectsTotalCount) {
        initChart(data, innovationProjectsTotalCount);
    };

    var show = function () {
        var jsonParameters = new Object();
        //jsonParameters.keyword = $('#Search').val();
        //jsonParameters.interestUid = $('#InterestUid').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Innovation/Projects/ShowEditionCountChartWidget'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    enableShowPlugins(data.innovationProjectsGroupedByTrackDtos, data.innovationProjectsTotalCount);
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
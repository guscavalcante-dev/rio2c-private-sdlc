// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 08-06-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-06-2021
// ***********************************************************************
// <copyright file="music.projects.editioncount.gauge.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var MusicProjectsEditionCountGaugeWidget = function () {

    var widgetElementId = '#MusicProjectsEditionCountGaugeWidget';
    var chartElementId = 'MusicProjectsEditionCountGaugeChart';
    var widgetElement = $(widgetElementId);

    // Show ---------------------------------------------------------------------------------------
    var initChart = function (data, musicBandsTotalCount) {
        if ($('#' + chartElementId).length === 0) {
            return;
        }

        am4core.ready(function () {
            am4core.useTheme(am4themes_animated);

            // Create chart instance
            var chart = am4core.create(chartElementId, am4charts.PieChart);
            chart.innerRadius = 70;
            chart.data = data;
            chart.marginTop = 200;

            // Add labels         
            var label = chart.seriesContainer.createChild(am4core.Label);
            label.isMeasured = false;
            label.horizontalCenter = "middle";
            label.verticalCenter = "bottom";
            label.fontSize = 30;
            label.text = musicBandsTotalCount;

            var label2 = chart.seriesContainer.createChild(am4core.Label);
            label2.isMeasured = false;
            label2.horizontalCenter = "middle";
            label2.verticalCenter = "top";
            label2.fontSize = 15;
            label2.text = musicProjects;
            label2.wrap = true;
            label2.maxWidth = 120;
            label2.textAlign = "middle";

            // Add and configure Series
            var pieSeries = chart.series.push(new am4charts.PieSeries());
            pieSeries.dataFields.value = "MusicBandsTotalCount";
            pieSeries.dataFields.category = "MusicGenreName";
            //pieSeries.labels.template.fontSize = 10;
            //pieSeries.alignLabels = false;

            // Animate chart data
            //var chartData = data;
            //var currentYear = 1995;
            //function getCurrentData() {
            //    label.text = currentYear;
            //    var data = chartData[currentYear];
            //    currentYear++;
            //    if (currentYear > 2014)
            //        currentYear = 1995;
            //    return data;
            //}

            //function loop() {
            //    //chart.allLabels[0].text = currentYear;
            //    var data = getCurrentData();
            //    for (var i = 0; i < data.length; i++) {
            //        chart.data[i].MusicBandsTotalCount = data[i].MusicBandsTotalCount;
            //    }
            //    chart.invalidateRawData();
            //    chart.setTimeout(loop, 4000);
            //}

            //loop();

        }); // end am4core.ready()
    };

    var enableShowPlugins = function (data, musicBandsTotalCount) {
        initChart(data, musicBandsTotalCount);
    };

    var show = function () {
        var jsonParameters = new Object();
        jsonParameters.keyword = $('#Search').val();
        jsonParameters.interestUid = $('#InterestUid').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Music/Projects/ShowEditionCountGaugeWidget'), jsonParameters, function (data) {
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
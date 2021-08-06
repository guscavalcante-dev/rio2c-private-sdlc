// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 03-07-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-08-2020
// ***********************************************************************
// <copyright file="audiovisual.meetings.editionscheduledcount.gauge.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var AudiovisualMeetingsEditionScheduledCountGaugeWidget = function () {

    var widgetElementId = '#AudiovisualMeetingsEditionScheduledCountGaugeWidget';
    var chartElementId = 'AudiovisualMeetingsEditionScheduledCountGaugeChart';
    var widgetElement = $(widgetElementId);

    // Show ---------------------------------------------------------------------------------------
    var initChart = function (chartData) {
        if ($('#' + chartElementId).length === 0) {
            return;
        }

        am4core.ready(function () {
            // Themes begin
            am4core.useTheme(am4themes_animated);
            // Themes end

            var chartMin = 200;
            var chartMax = 2000;

            var data = {
                score: chartData,
                gradingData: [
                    {
                        title: "",
                        color: "#ee1f25",
                        lowScore: 200,
                        highScore: 600
                    },
                    {
                        title: "",
                        color: "#f04922",
                        lowScore: 600,
                        highScore: 800
                    },
                    {
                        title: "",
                        color: "#fdae19",
                        lowScore: 800,
                        highScore: 1000
                    },
                    {
                        title: "",
                        color: "#f3eb0c",
                        lowScore: 1000,
                        highScore: 1200
                    },
                    {
                        title: "",
                        color: "#b0d136",
                        lowScore: 1200,
                        highScore: 1400
                    },
                    {
                        title: "",
                        color: "#54b947",
                        lowScore: 1400,
                        highScore: 1600
                    },
                    {
                        title: "",
                        color: "#0f9747",
                        lowScore: 1600,
                        highScore: 2000
                    }
                ]
            };

            /**
            Grading Lookup
             */
            function lookUpGrade(lookupScore, grades) {
                // Only change code below this line
                for (var i = 0; i < grades.length; i++) {
                    if (
                        grades[i].lowScore < lookupScore &&
                        grades[i].highScore >= lookupScore
                    ) {
                        return grades[i];
                    }
                }
                return null;
            }

            // create chart
            var chart = am4core.create(chartElementId, am4charts.GaugeChart);
            chart.innerRadius = am4core.percent(80);
            chart.hiddenState.properties.opacity = 0;
            chart.fontSize = 11;
            chart.resizable = true;
            chart.paddingTop = 0;

            /**
             * Normal axis
             */
            var axis = chart.xAxes.push(new am4charts.ValueAxis());
            axis.min = chartMin;
            axis.max = chartMax;
            axis.strictMinMax = true;
            axis.renderer.radius = am4core.percent(80);
            axis.renderer.inside = true;
            axis.renderer.line.strokeOpacity = 0.1;
            axis.renderer.ticks.template.disabled = false;
            axis.renderer.ticks.template.strokeOpacity = 1;
            axis.renderer.ticks.template.strokeWidth = 0.5;
            axis.renderer.ticks.template.length = 5;
            axis.renderer.grid.template.disabled = true;
            axis.renderer.labels.template.radius = 35;

            /**
             * Axis for ranges
             */
            var axis2 = chart.xAxes.push(new am4charts.ValueAxis());
            axis2.min = chartMin;
            axis2.max = chartMax;
            axis2.strictMinMax = true;
            axis2.renderer.labels.template.disabled = true;
            axis2.renderer.ticks.template.disabled = true;
            axis2.renderer.grid.template.disabled = false;
            axis2.renderer.grid.template.opacity = 0.5;
            axis2.renderer.labels.template.bent = true;
            axis2.renderer.labels.template.fill = am4core.color("#000");
            axis2.renderer.labels.template.fontWeight = "bold";
            axis2.renderer.labels.template.fillOpacity = 0.3;

            /**
            Ranges
            */
            for (let grading of data.gradingData) {
                var range = axis2.axisRanges.create();
                range.value = grading.lowScore > chartMin ? grading.lowScore : chartMin;
                range.endValue = grading.highScore < chartMax ? grading.highScore : chartMax;
                range.axisFill.fillOpacity = 0.8;
                range.axisFill.fill = am4core.color(grading.color);          
                range.axisFill.zIndex = -1;
                range.grid.strokeOpacity = 0;
                range.stroke = am4core.color(grading.color).lighten(-0.1);
                range.label.inside = true;
                range.label.text = grading.title.toUpperCase();
                range.label.inside = true;
                range.label.location = 0.5;
                range.label.inside = true;
                range.label.radius = am4core.percent(10);
                range.label.paddingBottom = -5; // ~half font size
                range.label.fontSize = "0.9em";
            }

            var matchingGrade = lookUpGrade(data.score, data.gradingData);

            /**
             * Label
             */
            var label = chart.radarContainer.createChild(am4core.Label);
            label.isMeasured = false;
            label.fontSize = 45;
            label.x = am4core.percent(50);
            label.y = am4core.percent(100);
            label.horizontalCenter = "middle";
            label.verticalCenter = "bottom";
            label.text = "50%";
            label.fill = am4core.color(matchingGrade.color);


            /**
             * Hand (Speedometer pointer)
             */
            var hand = chart.hands.push(new am4charts.ClockHand());
            hand.axis = axis2;
            hand.innerRadius = am4core.percent(55);
            hand.startWidth = 8;
            hand.pin.disabled = true;
            hand.value = chartMin;
            hand.fill = am4core.color("#444");
            hand.stroke = am4core.color("#000");

            hand.events.on("propertychanged", function () {
                label.text = axis2.positionToValue(hand.currentPosition).toFixed(); //Pass tofixed(1) to enable decimal cases
                axis2.invalidate();
            })

            var animation = new am4core.Animation(hand, {
                property: "value",
                to: chartData
            }, 2000, am4core.ease.cubicOut);

            animation.start();
        });
    };

    var enableShowPlugins = function (data) {
        initChart(data);
    };

    var show = function () {
        var jsonParameters = new Object();
        jsonParameters.keyword = $('#Search').val();
        jsonParameters.interestUid = $('#InterestUid').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/Meetings/ShowEditionScheduledCountGaugeWidget'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    enableShowPlugins(data.chartData);
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

    return {
        init: function () {
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            show();
        }
    };
}();
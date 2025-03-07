// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Ribeiro
// Created          : 21-02-2025
//
// Last Modified By : Rafael Ribeiro
// Last Modified On : 21-02-2025
// ***********************************************************************
// <copyright file="Music.meetings.editionscheduledcount.gauge.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var MusicMeetingsEditionScheduledCountGaugeWidget = function () {

    var widgetElementId = '#MusicMeetingsEditionScheduledCountGaugeWidget';
    var chartElementId = 'MusicMeetingsEditionScheduledCountGaugeChart';
    var widgetElement = $(widgetElementId);

    // Show ---------------------------------------------------------------------------------------
    var initChart = function (chartData, totalAvailablSlotsByEdition) {
        if ($('#' + chartElementId).length === 0) {
            return;
        }

        am4core.ready(function () {
            am4core.useTheme(am4themes_animated);

            var totalCountLabelId = "TotalCountLabel";
            var chartMin = 0;
            var chartMax = totalAvailablSlotsByEdition < 100 ? 100 : totalAvailablSlotsByEdition;

            const grading = [
                {
                    title: "",
                    color: "#ee1f25",
                    lowScore: 0,
                    highScore: 0
                },
                {
                    title: "",
                    color: "#f04922",
                    lowScore: 0,
                    highScore: 0
                },
                {
                    title: "",
                    color: "#fdae19",
                    lowScore: 0,
                    highScore: 0
                },
                {
                    title: "",
                    color: "#f3eb0c",
                    lowScore: 0,
                    highScore: 0
                },
                {
                    title: "",
                    color: "#b0d136",
                    lowScore: 0,
                    highScore: 0
                },
                {
                    title: "",
                    color: "#54b947",
                    lowScore: 0,
                    highScore: 0
                },
                {
                    title: "",
                    color: "#0f9747",
                    lowScore: 0,
                    highScore: 0
                }
            ];

            grading[0].lowScore = 0;
            for (let multipler = 0; multipler < grading.length; multipler++) {
                score = Number((chartMax / grading.length * (multipler + 1)).toString().split('.')[0]);
                grading[multipler].highScore = score;
                if (grading[multipler + 1]) {
                    grading[multipler + 1].lowScore = score;
                }
            }

            var data = {
                score: chartData,
                gradingData: grading
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
            chart.paddingTop = 0;
            chart.hiddenState.properties.opacity = 0;
            chart.fontSize = 11;
            chart.responsive.enabled = true;
            chart.responsive.rules.push({
                relevant: function (target) {
                    if (target.pixelWidth <= 600) {
                        return true;
                    }
                    return false;
                },
                state: function (target, stateId) {
                    // Labels States
                    if (target instanceof am4core.Label) {
                        if (!MyRio2cCommon.isNullOrEmpty(target.text)) {

                            // TotalCountLabel
                            if (target.id == totalCountLabelId) {

                                var state = target.states.create(stateId);
                                state.properties.fontSize = "1.5em";
                                state.properties.fontWeight = "bold";
                                return state;
                            }
                        }
                    }

                    return null;
                }
            });

            /**
             * Normal axis
             */
            var axis = chart.xAxes.push(new am4charts.ValueAxis());
            axis.min = chartMin;
            axis.max = chartMax;
            axis.strictMinMax = true;
            axis.renderer.radius = am4core.percent(80);
            axis.renderer.inside = true;
            axis.renderer.line.strokeOpacity = 0.5;
            axis.renderer.ticks.template.disabled = false;
            axis.renderer.ticks.template.strokeOpacity = 0.5;
            axis.renderer.ticks.template.strokeWidth = 0.5;
            axis.renderer.ticks.template.length = 5;
            axis.renderer.grid.template.disabled = true;
            axis.renderer.minGridDistance = 100; //Change the space between labels (range)
            axis.renderer.labels.template.radius = 40;
            axis.renderer.labels.template.adapter.add("text", function (text) {
                return text
            })

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
                //range.label.fontSize = "0.9em";
            }

            var matchingGrade = lookUpGrade(data.score, data.gradingData);

            /**
             * Label
             */
            var label = chart.radarContainer.createChild(am4core.Label);
            label.isMeasured = false;
            label.fontSize = "4em";
            label.x = am4core.percent(50);
            label.y = am4core.percent(100);
            label.horizontalCenter = "middle";
            label.verticalCenter = "bottom";
            label.text = chartData;
            label.fill = matchingGrade == null ? am4core.color("#000") : am4core.color(matchingGrade.color);
            label.id = totalCountLabelId;

            //Only trigger animation when the chartData is greather than charMinValue. Otherwise, visual bug occurs.
            if (chartData > chartMin) {
                /**
                 * Hand (Speedometer pointer)
                 */
                var hand = chart.hands.push(new am4charts.ClockHand());
                hand.axis = axis2;
                hand.innerRadius = am4core.percent(55);
                hand.startWidth = 5;
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
            }
        });
    };

    var enableShowPlugins = function (data, totalAvailablSlotsByEdition) {
        initChart(data, totalAvailablSlotsByEdition);
    };

    var show = function () {
        var jsonParameters = new Object();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Music/Meetings/ShowEditionScheduledCountGaugeWidget'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    enableShowPlugins(data.chartData, data.maximumAvailableSlots.TotalAvailablSlotsByEdition);
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
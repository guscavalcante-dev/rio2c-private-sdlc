// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 08-05-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-05-2021
// ***********************************************************************
// <copyright file="audiovisual.projects.editioncount.gauge.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var AudiovisualProjectsEditionCountGaugeWidget = function () {

    var widgetElementId = '#AudiovisualProjectsEditionCountGaugeWidget';
    var chartElementId = 'AudiovisualProjectsEditionCountGaugeChart';
    var widgetElement = $(widgetElementId);

    // Show ---------------------------------------------------------------------------------------
    var initChart = function (chartData) {
        if ($('#' + chartElementId).length === 0) {
            return;
        }

        am4core.ready(function () {
            am4core.useTheme(am4themes_animated);

            var totalCountLabelId = "TotalCountLabel";
            var chartMin = 200;
            var chartMax = 2000;

            // create chart
            var chart = am4core.create(chartElementId, am4charts.GaugeChart);
            chart.innerRadius = am4core.percent(82);
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
            axis.renderer.ticks.template.disabled = false
            axis.renderer.ticks.template.strokeOpacity = 0.5;
            axis.renderer.ticks.template.length = 10;
            axis.renderer.grid.template.disabled = true;
            axis.renderer.minGridDistance = 100; //Change the space between labels (range)
            axis.renderer.labels.template.radius = 40;
            axis.renderer.labels.template.adapter.add("text", function (text) {
                return text
            })

            /**
             * Axis for ranges
             */
            var colorSet = new am4core.ColorSet();

            var axis2 = chart.xAxes.push(new am4charts.ValueAxis());
            axis2.min = chartMin;
            axis2.max = chartMax;
            axis2.strictMinMax = true;
            axis2.renderer.labels.template.disabled = true;
            axis2.renderer.ticks.template.disabled = true;
            axis2.renderer.grid.template.disabled = true;

            var range0 = axis2.axisRanges.create();
            range0.value = chartMin;
            range0.endValue = 1100;
            range0.axisFill.fillOpacity = 1;
            range0.axisFill.fill = colorSet.getIndex(0);

            var range1 = axis2.axisRanges.create();
            range1.value = 1100;
            range1.endValue = 2000;
            range1.axisFill.fillOpacity = 1;
            range1.axisFill.fill = colorSet.getIndex(2);

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
            label.id = totalCountLabelId;

            //Only trigger animation when the chartData is greather than charMinValue. Otherwise, visual bug occurs.
            if (chartData > chartMin) {
                /**
                 * Hand (Speedometer pointer)
                 */
                var hand = chart.hands.push(new am4charts.ClockHand());
                hand.axis = axis2;
                hand.innerRadius = am4core.percent(50);
                hand.startWidth = 5;
                hand.pin.disabled = true;
                hand.value = chartMin;
                hand.fill = am4core.color("#444");
                hand.stroke = am4core.color("#000");

                hand.events.on("propertychanged", function (ev) {
                    label.text = axis2.positionToValue(hand.currentPosition).toFixed(); //Pass tofixed(1) to enable decimal cases
                    axis2.invalidate();
                    range0.endValue = ev.target.value;
                    range1.value = ev.target.value;
                });

                var animation = new am4core.Animation(hand, {
                    property: "value",
                    to: chartData
                }, 2000, am4core.ease.cubicOut);

                animation.start();
            }
        });
    };

    var enableShowPlugins = function (chartData) {
        initChart(chartData);
    };

    var show = function () {
        var jsonParameters = new Object();
        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/Projects/ShowEditionCountGaugeWidget'), jsonParameters, function (data) {
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
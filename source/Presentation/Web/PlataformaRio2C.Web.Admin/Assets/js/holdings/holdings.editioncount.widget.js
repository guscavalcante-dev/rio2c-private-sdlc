// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 08-07-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-12-2019
// ***********************************************************************
// <copyright file="holdings.editioncount.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var HoldingsEditionCountWidget = function () {

    var widgetElementId = '#HoldingEditionCountWidget';
    var widgetElement = $(widgetElementId);

    var getCounter = function (countUrl) {
        var jsonParameters = new Object();
        jsonParameters.showAllEditions = false;

        $.get(countUrl, jsonParameters, function (data) {
            if (data.status === 'success') {
                widgetElement.find('.counter').html(data.count);
            }
            //solutionCommonHandleFormReturn(data, null, null,
            //    // Success
            //    function () {
            //        if (data.Email !== null && data.Email !== '') {
            //            $('#Email').val(data.Email);
            //            disableEmailField();
            //        }
            //        else {
            //            enableEmailField();
            //        }
            //    },
            //    // Error
            //    function () {
            //        //console.log('erro');
            //    });
            MyRio2cCommon.unblock({ idOrClass: widgetElementId });
        })
        .fail(function () {
            //showAlert();
            //MyRio2cCommon.block(widgetElementId);
        });
    };

    var initChart = function () {
        if ($('#kt_chart_bandwidth2').length == 0) {
            return;
        }

        var ctx = document.getElementById("kt_chart_bandwidth2").getContext("2d");

        var gradient = ctx.createLinearGradient(0, 0, 0, 240);
        gradient.addColorStop(0, Chart.helpers.color('#ffefce').alpha(1).rgbString());
        gradient.addColorStop(1, Chart.helpers.color('#ffefce').alpha(0.3).rgbString());

        var config = {
            type: 'line',
            data: {
                labels: ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October"],
                datasets: [{
                    //label: "Bandwidth Stats",
                    backgroundColor: gradient,
                    borderColor: KTApp.getStateColor('warning'),
                    pointBackgroundColor: Chart.helpers.color('#000000').alpha(0).rgbString(),
                    pointBorderColor: Chart.helpers.color('#000000').alpha(0).rgbString(),
                    pointHoverBackgroundColor: KTApp.getStateColor('danger'),
                    pointHoverBorderColor: Chart.helpers.color('#000000').alpha(0.1).rgbString(),

                    //fill: 'start',
                    data: [
                        10, 14, 12, 16, 9, 11, 13, 9, 13, 15
                    ]
                }]
            },
            options: {
                title: {
                    display: false
                },
                tooltips: {
                    enabled: false
                    //mode: 'nearest',
                    //intersect: false,
                    //position: 'nearest',
                    //xPadding: 10,
                    //yPadding: 10,
                    //caretPadding: 10
                },
                legend: {
                    display: false
                },
                responsive: true,
                maintainAspectRatio: false,
                scales: {
                    xAxes: [{
                        display: false,
                        gridLines: false,
                        scaleLabel: {
                            display: true,
                            labelString: 'Month'
                        }
                    }],
                    yAxes: [{
                        display: false,
                        gridLines: false,
                        scaleLabel: {
                            display: true,
                            labelString: 'Value'
                        },
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                },
                elements: {
                    line: {
                        tension: 0.0000001
                    },
                    point: {
                        radius: 4,
                        borderWidth: 12
                    }
                },
                layout: {
                    padding: {
                        left: 0,
                        right: 0,
                        top: 10,
                        bottom: 0
                    }
                }
            }
        };

        var chart = new Chart(ctx, config);
    };

    return {
        init: function (countUrl) {
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            getCounter(countUrl);
            initChart();
        }
    };
}();
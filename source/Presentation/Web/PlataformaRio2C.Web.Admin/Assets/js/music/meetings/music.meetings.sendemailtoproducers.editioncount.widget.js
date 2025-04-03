// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Daniel Giese
// Created          : 03-17-2025
//
// Last Modified By : Daniel Giese
// Last Modified On : 03-17-2025
// ***********************************************************************
// <copyright file="music.meetings.sendemailtoproducers.editioncount.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var MusicMeetingsSendEmailToProducersEditionCountWidget = function () {

    var widgetElementId = '#MusicMeetingsSendEmailToProducersEditionCountWidget';
    var chartElementId = 'MusicMeetingsSendEmailToProducersEditionCountChart';
    var widgetElement = $(widgetElementId);

    // Show ---------------------------------------------------------------------------------------
    var initChart = function () {
        if ($('#' + chartElementId).length === 0) {
            return;
        }

        var ctx = document.getElementById(chartElementId).getContext("2d");

        var gradient = ctx.createLinearGradient(0, 0, 0, 240);
        gradient.addColorStop(0, Chart.helpers.color('#d1f1ec').alpha(1).rgbString());
        gradient.addColorStop(1, Chart.helpers.color('#d1f1ec').alpha(0.3).rgbString());

        var config = {
            type: 'line',
            data: {
                labels: ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October"],
                datasets: [{
                    backgroundColor: gradient,
                    borderColor: KTApp.getStateColor('success'),
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

    var enableShowPlugins = function () {
        initChart();
    };

    var show = function () {
        var jsonParameters = new Object();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Music/Meetings/ShowSendEmailToProducersEditionCountWidget'), jsonParameters, function (data) {
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
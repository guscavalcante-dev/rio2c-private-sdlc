// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Carlos Pereira
// Created          : 08-05-2021
//
// Last Modified By : Carlos Pereira
// Last Modified On : 08-05-2021
// ***********************************************************************
// <copyright file="audiovisual.producers.editioncountodometer.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
var AudiovisualProducersEditionCountOdometerWidget = function () {
    var widgetElementId = '#AudiovisualProducersEditionCountOdometerWidget';
    var chartElementId = '.odometer_audiovisualproducerseditioncountodometerchart';
    var widgetElement = $(widgetElementId);
    // Show ---------------------------------------------------------------------------------------
    var initChart = function (odometerCount) {
        var el = document.querySelector(chartElementId);
        od = new Odometer({
            el: el,
            format: '',
            theme: 'car'
        });
        $(chartElementId).html(odometerCount);
    };
    var enableShowPlugins = function (odometerCount) {
        initChart(odometerCount);
    };
    var show = function () {
        var jsonParameters = new Object();
        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/Producers/ShowEditionCountOdometerWidget'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    enableShowPlugins(data.odometerCount);
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

// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 12-21-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-21-2023
// ***********************************************************************
// <copyright file="music.players.editioncountodometer.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
var MusicPlayersEditionCountOdometerWidget = function () {
    var widgetElementId = '#MusicPlayersEditionCountOdometerWidget';
    var chartElementId = '.odometer_musicplayerseditioncountodometerchart';
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
        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Music/Players/ShowEditionCountOdometerWidget'), jsonParameters, function (data) {
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
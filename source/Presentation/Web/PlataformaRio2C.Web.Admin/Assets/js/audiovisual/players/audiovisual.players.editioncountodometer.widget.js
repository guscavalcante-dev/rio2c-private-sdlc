// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Carlos Pereira
// Created          : 08-05-2021
//
// Last Modified By : Carlos Pereira
// Last Modified On : 08-05-2021
// ***********************************************************************
// <copyright file="audiovisual.players.editioncountodometer.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var AudiovisualPlayersEditionCountOdometerWidget = function () {

    var widgetElementId = '#AudiovisualPlayersEditionCountOdometerWidget';
    var chartElementId = '.odometer';
    var widgetElement = $(widgetElementId);

    // Show ---------------------------------------------------------------------------------------
    var initChart = function () {

        var el = document.querySelector(chartElementId);

        od = new Odometer({
            el: el,
            format: '',
            theme: 'car'
        });

        $(chartElementId).html(300);

    };

    var enableShowPlugins = function (data) {
        initChart();
        console.log(data);
    };

    var show = function () {
        var jsonParameters = new Object();
        jsonParameters.keyword = $('#Search').val();
        jsonParameters.interestUid = $('#InterestUid').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/Projects/ShowEditionCountOdometerWidget'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    enableShowPlugins(data);
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

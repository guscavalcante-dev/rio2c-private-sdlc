// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 06-10-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 06-10-2021
// ***********************************************************************
// <copyright file="audiovisual.meetings.logistics.info.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var AudiovisualMeetingsLogisticsInfoWidget = function () {
    var widgetElementId = '#AudiovisualMeetingsLogisticsInfoWidget';
    var widgetElement = $(widgetElementId);

    // Show ---------------------------------------------------------------------------------------
    var enableShowPlugins = function () {
        KTApp.initTooltips();
        MyRio2cCommon.initScroll();

        var element = $(widgetElementId);
        if (!MyRio2cCommon.isNullOrEmpty(element)) {
            element.removeClass('d-none');
        }
    };

    var show = function (organizationUids) {
        var jsonParameters = new Object();
        jsonParameters.organizationsUids = organizationUids.join(',');;

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Meetings/ShowLogisticsInfoWidget'), jsonParameters, function (data) {
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
        })
        .always(function () {
            MyRio2cCommon.unblock({ idOrClass: widgetElementId });
        });
    };

    return {
        init: function (organizationUids) {
            show(organizationUids);
        }
    };
}();
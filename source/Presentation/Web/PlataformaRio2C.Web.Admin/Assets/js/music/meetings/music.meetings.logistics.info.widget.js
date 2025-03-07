// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Ribeiro
// Created          : 21-02-2025
//
// Last Modified By : Rafael Ribeiro
// Last Modified On : 21-02-2025
// ***********************************************************************
// <copyright file="Music.meetings.logistics.info.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var MusicMeetingsLogisticsInfoWidget = function () {
    var widgetElementId = '#MusicMeetingsLogisticsInfoWidget';
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
        if (!MyRio2cCommon.isNullOrEmpty(organizationUids)) {
            jsonParameters.organizationsUids = organizationUids.join(',');
        }
        else {
            jsonParameters.organizationsUids = '';
        }

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
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            show(organizationUids);
        }
    };
}();
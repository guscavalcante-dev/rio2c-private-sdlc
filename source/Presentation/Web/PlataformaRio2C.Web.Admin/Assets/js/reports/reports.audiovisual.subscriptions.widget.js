// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : William Sergio Almado Junior
// Created          : 01-13-2019
//
// Last Modified By : William Sergio Almado Junior
// Last Modified On : 01-13-2019
// ***********************************************************************
// <copyright file="reports.audiovisual.subscription.widget.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var ReportsAudiovisualSubscriptionWidget = function () {

    var widgetElementId = '#ReportAudiovisualSubscriptionWidget';
    var widgetElement = $(widgetElementId);

    //var updateModalId = '#UpdateMainInformationModal';
    //var updateFormId = '#UpdateMainInformationForm';

    // Show ---------------------------------------------------------------------------------------
    var enableShowPlugins = function () {
        KTApp.initTooltips();
        MyRio2cCommon.initScroll();
    };

    var show = function () {
        if (widgetElement.length <= 0) {
            return;
        }

        var jsonParameters = new Object();
        //jsonParameters.roomUid = $('#AggregateId').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Reports/ShowAudiovisualSubscriptionsWidget'), jsonParameters, function (data) {
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
        init: function () {
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            show();
        },
    };
}();
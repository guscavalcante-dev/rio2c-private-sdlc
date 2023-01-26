// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Elton Assunção
// Created          : 01-20-2023
//
// Last Modified By : Elton Assunção
// Last Modified On : 01-20-2023
// ***********************************************************************
// <copyright file="innovation.projects.objectives.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var InnovationProjectsSustainableDevelopmentWidget = function () {

    var widgetElementId = '#ProjectsSustainableDevelopmentWidget';
    var widgetElement = $(widgetElementId);

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
        jsonParameters.attendeeInnovationOrganizationUid = $('#AggregateId').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Innovation/Projects/ShowSustainableDevelopmentWidget'), jsonParameters, function (data) {
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
            debugger;
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            show();
        },
    };
}();
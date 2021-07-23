// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 04-20-2021
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-22-2021
// ***********************************************************************
// <copyright file="administrators.maininformation.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var AdministratorsMainInformationWidget = function () {

    var widgetElementId = '#AdministratorsMainInformationWidget';
    var widgetElement = $(widgetElementId);

    // Show ---------------------------------------------------------------------------------------
    var show = function () {
        if (widgetElement.length <= 0) {
            return;
        }

        var jsonParameters = new Object();
        jsonParameters.collaboratorUid = $('#AggregateId').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Administrators/ShowMainInformationWidget'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    //enableShowPlugins();
                },
                // Error
                onError: function () {
                }
            });
        })
        .fail(function () {
        })
        .always(function () {                
        });
    };

    return {
        init: function () {
            //MyRio2cCommon.block({ idOrClass: widgetElementId });
            show();
        },
        show: function () {
            show();
        }
    };
}();
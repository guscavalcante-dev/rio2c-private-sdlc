// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 08-13-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-13-2019
// ***********************************************************************
// <copyright file="holdings.create.modal.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var HoldingsCreateModal = function () {

    var modalId = '#CreateHoldingModal';

    // Create -------------------------------------------------------------------------------------
    var enableCreatePlugins = function () {
        //initChart();
    };

    var show = function () {
        MyRio2cCommon.block();

        var jsonParameters = new Object();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Holdings/ShowCreateModal'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    enableCreatePlugins();
                    $(modalId).modal();
                    MyRio2cCommon.unblock();
                },
                // Error
                onError: function() {
                }
            });
        })
        .fail(function () {
            //showAlert();
            //MyRio2cCommon.unblock(widgetElementId);
        });
    };

    return {
        show: function () {
            show();
        }
    };
}();
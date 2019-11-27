// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 11-27-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-27-2019
// ***********************************************************************
// <copyright file="networks.messages.conversation.widget" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var NetworksMessagesConversationWidget = function () {

    var widgetElementId = '#MessagesConversationWidget';
    var widgetElement = $(widgetElementId);

    // Show ---------------------------------------------------------------------------------------
    var enableShowPlugins = function () {
        $("time.timeago").timeago();
        MyRio2cCommon.initScroll();
    };

    var show = function () {
        if (widgetElement.length <= 0) {
            return;
        }

        var chatSelectedElement = $('.chat-selected');
        if (chatSelectedElement.length <= 0) {
            return;
        }

        var jsonParameters = new Object();
        jsonParameters.recipientId = chatSelectedElement.data('recipient-id');
        jsonParameters.recipientUid = chatSelectedElement.data('recipient-uid');

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Networks/ShowConversationWidget'), jsonParameters, function (data) {
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
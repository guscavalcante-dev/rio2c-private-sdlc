// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 11-27-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-28-2019
// ***********************************************************************
// <copyright file="networks.messages.conversation.widget" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var NetworksMessagesConversationWidget = function () {

    //int editionId, Guid editionUid, int senderId, Guid senderUid, int recipientId, Guid recipientUid, string recipientEmail, string text
    var widgetElementId = '#MessagesConversationWidget';
    var widgetElement = $(widgetElementId);

    var recipientId;
    var recipientUid;
    var recipientEmail;

    // Signalr ------------------------------------------------------------------------------------
    var enableChat = function () {
        $.connection.hub.start().done(function () {
            $('#SendMessage').click(function () {
                // Call the Send method on the hub.
                window.messageHub.server.sendMessage(
                    messagesConfig.editionId,
                    messagesConfig.editionUid,
                    messagesConfig.senderId,
                    messagesConfig.senderUid,
                    recipientId,
                    recipientUid,
                    recipientEmail,
                    $('#Text').val());

                var html =
                    '<div class="kt-chat__message kt-chat__message--right">' +
                        '   <div class="kt-chat__user">' +
                        '       <span class="kt-chat__datetime">30 Seconds</span>' +
                        '       <a href="#" class="kt-chat__username">You</a>' +
                        '       <span class="kt-userpic kt-userpic--circle kt-userpic--sm">' +
                        '           <img src="https://dev.assets.my.rio2c.com/img/users/1d42aa30-7498-41e9-9e4e-66bd1d416314_thumbnail.png?v=20191125134854" alt="image">' +
                        '       </span>' +
                        '   </div>' +
                        '   <div class="kt-chat__text kt-bg-light-brand">' + htmlEncode($('#Text').val()) + '</div>' +
                    '</div>';

                $('#Messages').append(html);
                $('#Messages').scrollTop($('#Messages')[0].scrollHeight);
                // Clear text box and reset focus for next comment.
                $('#Text').val('').focus();
            });
        });

        $('#Text').focus();
    };

    // Show ---------------------------------------------------------------------------------------
    var enableShowPlugins = function () {
        $("time.timeago").timeago();
        MyRio2cCommon.initScroll();
        enableChat();
    };

    var show = function () {
        if (widgetElement.length <= 0) {
            return;
        }

        var chatSelectedElement = $('.chat-selected');
        if (chatSelectedElement.length <= 0) {
            return;
        }

        recipientId = chatSelectedElement.data('recipient-id');
        recipientUid = chatSelectedElement.data('recipient-uid');
        recipientEmail = chatSelectedElement.data('recipient-email');

        var jsonParameters = new Object();
        jsonParameters.recipientId = recipientId;
        jsonParameters.recipientUid = recipientUid;

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


jQuery(document).ready(function () {
    if (typeof (window.messageHub) === 'undefined') {
        window.messageHub = $.connection.messageHub;
    }

    messageHub.client.receiveMessage = function (name, message) {
        var html =
            '<div class="kt-chat__message">' +
                '   <div class="kt-chat__user">' +
                '       <span class="kt-userpic kt-userpic--circle kt-userpic--sm">' +
                '           <img src="https://dev.assets.my.rio2c.com/img/users/1d42aa30-7498-41e9-9e4e-66bd1d416314_thumbnail.png?v=20191125134854" alt="image">' +
                '       </span>' +
                '       <a href="#" class="kt-chat__username">' + htmlEncode(name) + '</a>' +
                '       <span class="kt-chat__datetime">2 Hours</span>' +
                '   </div>' +
                '   <div class="kt-chat__text kt-bg-light-success text-wrap">' + htmlEncode(message) + '</div>' +
                '</div>';
        $('#Messages').append(html);
    };
});

function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}
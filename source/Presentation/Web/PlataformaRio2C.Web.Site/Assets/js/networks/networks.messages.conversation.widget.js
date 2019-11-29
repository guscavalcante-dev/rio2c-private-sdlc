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
                    messagesConfig.senderEmail,
                    recipientId,
                    recipientUid,
                    $('#Text').val());

                //var html =
                //    '<div class="kt-chat__message kt-chat__message--right">' +
                //        '   <div class="kt-chat__user">' +
                //        '       <span class="kt-chat__datetime">30 Seconds</span>' +
                //        '       <a href="#" class="kt-chat__username">You</a>' +
                //        '       <span class="kt-userpic kt-userpic--circle kt-userpic--sm">' +
                //        '           <img src="https://dev.assets.my.rio2c.com/img/users/1d42aa30-7498-41e9-9e4e-66bd1d416314_thumbnail.png?v=20191125134854" alt="image">' +
                //        '       </span>' +
                //        '   </div>' +
                //        '   <div class="kt-chat__text kt-bg-light-brand">' + htmlEncode($('#Text').val()) + '</div>' +
                //    '</div>';

                //$('#Messages').append(html);
                //$('#Messages').scrollTop($('#Messages')[0].scrollHeight);
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

    messageHub.client.receiveSenderMessage = function (message) {
        var messageHubDto = JSON.parse(message);

        MyRio2cCommon.handleAjaxReturn({
            data: messageHubDto,
            // Success
            onSuccess: function () {
                var html =
                    '<div class="kt-chat__message kt-chat__message--right">' +
                        '   <div class="kt-chat__user">' +
                        '       <time class="timeago kt-widget__date" datetime="' + messageHubDto.data.sendDate + '">' + messageHubDto.data.sendDateFormatted  + '</time>' +
                        '       <a href="#" class="kt-chat__username">' + messagesConfig.you + '</a>';

                if (!MyRio2cCommon.isNullOrEmpty(messageHubDto.data.senderImageUrl)) {
                    html +=
                        '       <span class="kt-userpic kt-userpic--circle kt-userpic--sm">' +
                        '           <img src="' + messageHubDto.data.senderImageUrl + '" alt="image">' +
                        '       </span>';
                }
                else {
                    html +=
                        '       <span class="kt-media kt-media--circle kt-media--brand kt-media--sm d-inline-block">' +
                        '           <span>' + messageHubDto.data.senderNameInitials  + '</span>' +
                        '       </span>';
                }

                html +=
                        '   </div>' +
                        '   <div class="kt-chat__text kt-bg-light-brand">' + messageHubDto.data.text.replace(/(?:\r\n|\r|\n)/g, '<br>') + '</div>' +
                        '</div>';

                $('#Messages').append(html);
                $("time.timeago").timeago();
            },
            // Error
            onError: function () {
            }
        });
    };

    messageHub.client.receiveRecipientMessage = function (message) {
        var messageHubDto = JSON.parse(message);

        MyRio2cCommon.handleAjaxReturn({
            data: messageHubDto,
            // Success
            onSuccess: function () {
                var html =
                    '<div class="kt-chat__message">' +
                        '   <div class="kt-chat__user">';

                if (!MyRio2cCommon.isNullOrEmpty(messageHubDto.data.senderImageUrl)) {
                    html +=
                        '       <span class="kt-userpic kt-userpic--circle kt-userpic--sm">' +
                        '           <img src="' + messageHubDto.data.senderImageUrl + '" alt="image">' +
                        '       </span>';
                }
                else {
                    html +=
                        '       <span class="kt-media kt-media--circle kt-media--brand kt-media--sm d-inline-block">' +
                        '           <span>' + messageHubDto.data.senderNameInitials  + '</span>' +
                        '       </span>';
                }

                html +=
                        '       <a href="#" class="kt-chat__username">' + messageHubDto.data.senderName + '</a>' +
                        '       <time class="timeago kt-widget__date" datetime="' + messageHubDto.data.sendDate + '">' + messageHubDto.data.sendDateFormatted  + '</time>' +
                        '   </div>' +
                        '   <div class="kt-chat__text kt-bg-light-success text-wrap">' + messageHubDto.data.text.replace(/(?:\r\n|\r|\n)/g, '<br>') + '</div>' +
                    '</div>';

                $('#Messages').append(html);
                $("time.timeago").timeago();
            },
            // Error
            onError: function () {
            }
        });
    };
});

//function htmlEncode(value) {
//    var encodedValue = $('<div />').text(value).html();
//    return encodedValue;
//}
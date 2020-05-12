// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 11-27-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 05-12-2020
// ***********************************************************************
// <copyright file="networks.messages.conversation.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var otherUserId;
var otherUserUid;

var NetworksMessagesConversationWidget = function () {

    var widgetElementId = '#MessagesConversationWidget';
    var widgetElement = $(widgetElementId);

    // Signalr ------------------------------------------------------------------------------------
    var enableChat = function () {
        startHub();

        $('#SendMessage').click(function () {
            // Call the Send method on the hub.
            window.messageHub.server.sendMessage(
                messagesConfig.editionId,
                messagesConfig.editionUid,
                messagesConfig.currentUserId,
                messagesConfig.currentUserUid,
                messagesConfig.currentUserEmail,
                otherUserId,
                otherUserUid,
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

        $('#Text').focus();
    };

    // Read messages ------------------------------------------------------------------------------
    var readMessages = function (isChatOpen) {
        if (isChatOpen || !$('.chat-selected .unread-messages-count').hasClass('d-none')) {
            window.messageHub.server.readMessages(
                otherUserId,
                otherUserUid,
                messagesConfig.currentUserId,
                messagesConfig.currentUserUid
            )
            .done(function () {
                $('.chat-selected .unread-messages-count')
                    .html(0)
                    .addClass('d-none');

                    NetworksMessagesConversationsWidget.disableAlertPulse();
             });
        }
    };

    // Show ---------------------------------------------------------------------------------------
    var enableShowPlugins = function () {
        MyRio2cKTAppChat.init();
        $("time.timeago").timeago();
        NetworksMessagesConversationWidget.handleMessaging();
        //MyRio2cCommon.initScroll();
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

        var newOtherUserId = chatSelectedElement.data('otheruser-id');
        var newOtherUserUid = chatSelectedElement.data('otheruser-uid');

        if (newOtherUserUid === otherUserUid) {
            return;
        }

        MyRio2cCommon.block({ idOrClass: widgetElementId });

        otherUserId = newOtherUserId;
        otherUserUid = newOtherUserUid;

        var jsonParameters = new Object();
        jsonParameters.recipientId = otherUserId;
        jsonParameters.recipientUid = otherUserUid;

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Networks/ShowConversationWidget'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    enableShowPlugins();
                    readMessages();
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

    var handleMessaging = function () {
        var parentEl = KTUtil.getByID('kt_chat_content');
        var scrollEl = KTUtil.find(parentEl, '.kt-scroll');
        var messagesEl = KTUtil.find(parentEl, '.kt-chat__messages');

        var node = document.createElement("DIV");
        KTUtil.addClass(node, 'kt-chat__message kt-chat__message--brand kt-chat__message--right');

        if (!MyRio2cCommon.isNullOrEmpty(KTUtil.css(messagesEl, 'height'))) {
            scrollEl.scrollTop = parseInt(KTUtil.css(messagesEl, 'height'));
        }

        var ps;
        if (ps = KTUtil.data(scrollEl).get('ps')) {
            ps.update();
        }

        KTUtil.addClass(node, 'kt-chat__message kt-chat__message--success');

        if (!MyRio2cCommon.isNullOrEmpty(KTUtil.css(messagesEl, 'height'))) {
            scrollEl.scrollTop = parseInt(KTUtil.css(messagesEl, 'height'));
        }

        var ps;
        if (ps = KTUtil.data(scrollEl).get('ps')) {
            ps.update();
        }
    };

    // Toggle conversation ------------------------------------------------------------------------
    var disableConversation = function () {
        $('#Text').prop('disabled', true);
        $('#SendMessage').prop('disabled', true);
    };

    var enableConversation = function () {
        $('#Text').prop('disabled', false);
        $('#SendMessage').prop('disabled', false);
    };

    return {
        init: function () {
            show();
        },
        handleMessaging: function() {
            handleMessaging();
        },
        readMessages: function (isChatOpen) {
            readMessages(isChatOpen);
        },
        disableConversation: function () {
            disableConversation();
        },
        enableConversation: function () {
            enableConversation();
        }
    };
}();

var startHub = function () {
    $('.connection-status').addClass('d-none');
    $('#StatusConnecting').removeClass('d-none');

    $.connection.hub.start()
        .done(function () {
            NetworksMessagesConversationWidget.enableConversation();
            $('.connection-status').addClass('d-none');
            $('#StatusConnected').removeClass('d-none');
        })
        .fail(function () {
            NetworksMessagesConversationWidget.disableConversation();
            $('.connection-status').addClass('d-none');
            $('#StatusDisconnected').removeClass('d-none');
        });
};

jQuery(document).ready(function () {
    if (typeof (window.messageHub) === 'undefined') {
        window.messageHub = $.connection.messageHub;
    }

    messageHub.client.receiveSenderMessage = function (message) {
        var globalVariables = MyRio2cCommon.getGlobalVariables();
        var messageHubDto = JSON.parse(message);

        MyRio2cCommon.handleAjaxReturn({
            data: messageHubDto,
            // Success
            onSuccess: function () {
                if (otherUserUid === messageHubDto.data.recipientUserUid) {
                    var sendDateFormatted = moment(messageHubDto.data.sendDate).tz(globalVariables.momentTimeZone).locale(globalVariables.userInterfaceLanguage).format('L LTS');

                    var html =
                        '<div class="kt-chat__message kt-chat__message--right">' +
                            '   <div class="kt-chat__user">' +
                        '       <time class="timeago kt-widget__date" datetime="' + messageHubDto.data.sendDate + '">' + sendDateFormatted + '</time>' +
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
                            '           <span>' + messageHubDto.data.senderNameInitials + '</span>' +
                            '       </span>';
                    }

                    html +=
                        '   </div>' +
                        '   <div class="kt-chat__text kt-bg-light-brand">' + messageHubDto.data.text.replace(/(?:\r\n|\r|\n)/g, '<br>') + '</div>' +
                        '</div>';

                    $('#Messages').append(html);

                    if (typeof (NetworksMessagesConversationsWidget) !== 'undefined') {
                        NetworksMessagesConversationsWidget.changeLastMessageDateTime(messageHubDto.data.sendDate, sendDateFormatted);
                    }

                    $("time.timeago").timeago();
                    NetworksMessagesConversationWidget.handleMessaging();
                }
            },
            // Error
            onError: function () {
            }
        });
    };

    messageHub.client.receiveRecipientMessage = function (message) {
        var globalVariables = MyRio2cCommon.getGlobalVariables();
        var messageHubDto = JSON.parse(message);

        MyRio2cCommon.handleAjaxReturn({
            data: messageHubDto,
            // Success
            onSuccess: function () {

                if (otherUserUid === messageHubDto.data.senderUserUid) {
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
                            '           <span>' + messageHubDto.data.senderNameInitials + '</span>' +
                            '       </span>';
                    }

                    html +=
                        '       <a href="#" class="kt-chat__username">' + messageHubDto.data.senderName + '</a>' +
                    '       <time class="timeago kt-widget__date" datetime="' + messageHubDto.data.sendDate + '">' + moment(messageHubDto.data.sendDate).tz(globalVariables.momentTimeZone).locale(globalVariables.userInterfaceLanguage).format('L LTS') + '</time>' +
                        '   </div>' +
                        '   <div class="kt-chat__text kt-bg-light-success text-wrap">' + messageHubDto.data.text.replace(/(?:\r\n|\r|\n)/g, '<br>') + '</div>' +
                        '</div>';

                    $('#Messages').append(html);
                    $("time.timeago").timeago();
                    NetworksMessagesConversationWidget.handleMessaging();
                    NetworksMessagesConversationWidget.readMessages(true);
                }
            },
            // Error
            onError: function () {
            }
        });
    };

    //$.connection.hub.logging = true;

    $.connection.hub.connectionSlow(function () {
        $('.connection-status').addClass('d-none');
        $('#StatusConnectionSlow').removeClass('d-none');
    });

    $.connection.hub.reconnecting(function () {
        NetworksMessagesConversationWidget.disableConversation();
        $('.connection-status').addClass('d-none');
        $('#StatusConnecting').removeClass('d-none');
    });

    $.connection.hub.reconnected(function () {
        NetworksMessagesConversationWidget.enableConversation();
        $('.connection-status').addClass('d-none');
        $('#StatusConnected').removeClass('d-none');
    });

    $.connection.hub.disconnected(function () {
        NetworksMessagesConversationWidget.disableConversation();
        $('.connection-status').addClass('d-none');
        $('#StatusDisconnected').removeClass('d-none');

        // Try to reconnect
        setTimeout(function () {
            startHub();
        }, 10000); // Restart connection after 5 seconds.
    });

    startHub();
});
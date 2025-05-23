﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 11-27-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-17-2020
// ***********************************************************************
// <copyright file="networks.messages.conversations.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var NetworksMessagesConversationsWidget = function () {

    var widgetElementId = '#MessagesConversationsWidget';
    var widgetElement = $(widgetElementId);

    // Search -------------------------------------------------------------------------------------
    var search = function () {
        if (typeof (NetworksMessagesConversationsWidget) !== 'undefined') {
            NetworksMessagesConversationsWidget.init();
        }
    };

    var enableSearchEvent = function () {
        $('#ConversationsSearchKeywords').not('.search-event-enabled').on('search', function () {
            search();
        });

        $('#ConversationsSearchKeywords').addClass('search-event-enabled');
    };

    // Show ---------------------------------------------------------------------------------------
    var disableAlertPulse = function () {
        var newMessagePulseElement = $('#NewMessagePulse');
        if (newMessagePulseElement.length > 0 && $('.unread-messages-count:not(".d-none")').length === 0) {
            newMessagePulseElement.addClass('d-none');
        }
    };

    var enableShowPlugins = function () {
        $("time.timeago").timeago();
        enableSearchEvent();
        disableAlertPulse();
    };

    var show = function (otherUserUid) {
        if (widgetElement.length <= 0) {
            return;
        }

        var jsonParameters = new Object();

        var initialOtherUserIdElement = $('#InitialUserUid');
        if (!MyRio2cCommon.isNullOrEmpty(otherUserUid)) {
            jsonParameters.userUid = otherUserUid;
        }
        else if (!MyRio2cCommon.isNullOrEmpty(initialOtherUserIdElement.val())) {
            jsonParameters.userUid = $('#InitialUserUid').val();
            $('#InitialUserUid').val('');
        }

        jsonParameters.searchKeywords = $('#ConversationsSearchKeywords').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Networks/ShowConversationsWidget'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    enableShowPlugins();

                    if (typeof (NetworksMessagesConversationWidget) !== 'undefined') {
                        NetworksMessagesConversationWidget.init();
                    }
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

    // Reload ------------------------------------------------------------------------------------
    var reload = function () {
        var otherUserUid = '';

        var chatSelectedElement = $('.chat-selected');
        if (chatSelectedElement.length > 0) {
            otherUserUid = chatSelectedElement.data('otheruser-uid');
        }

        show(otherUserUid);
    };

    // Change -------------------------------------------------------------------------------------
    var change = function (element) {
        if (MyRio2cCommon.isNullOrEmpty(element)) {
            return;
        }

        $('.kt-widget__item.chat-selected').each(function() {
            $(this).removeClass('chat-selected');
        });

        element.addClass('chat-selected');

        if (typeof (NetworksMessagesConversationWidget) !== 'undefined') {
            NetworksMessagesConversationWidget.init();
        }
    };

    // Change last message date/time -----------------------------------------------------------------
    var changeLastMessageDateTime = function (dateTimeOffset, dateTime) {
        $('.chat-selected .kt-widget__date').timeago('update', dateTimeOffset);
        $('.chat-selected .kt-widget__date').attr('title', dateTime);
    };

    return {
        init: function () {
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            show();
        },
        show: function (otherUserUid) {
            show(otherUserUid);
        },
        disableAlertPulse: function () {
            disableAlertPulse();
        },
        search: function () {
            search();
        },
        change: function (element) {
            change(element);
        },
        reload: function() {
            reload();
        },
        changeLastMessageDateTime: function (isChatOpen, dateTime) {
            changeLastMessageDateTime(isChatOpen, dateTime);
        }
    };
}();
// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 11-27-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-03-2019
// ***********************************************************************
// <copyright file="networks.messages.conversations.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var NetworksMessagesConversationsWidget = function () {

    var widgetElementId = '#MessagesConversationsWidget';
    var widgetElement = $(widgetElementId);

    // Show ---------------------------------------------------------------------------------------
    var enableShowPlugins = function () {
        $("time.timeago").timeago();
    };

    var show = function () {
        if (widgetElement.length <= 0) {
            return;
        }

        var jsonParameters = new Object();
        jsonParameters.userUid = $('#UserUid').val();

        $('#UserUid').val('');

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

    return {
        init: function () {
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            show();
        },
        change: function (element) {
            change(element);
        }
    };
}();
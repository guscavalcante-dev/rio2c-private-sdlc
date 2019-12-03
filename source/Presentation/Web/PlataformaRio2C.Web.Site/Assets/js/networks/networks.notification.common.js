// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 12-03-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-03-2019
// ***********************************************************************
// <copyright file="networks.notification.common.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var NetworksNotificationCommon = function () {

    return {
        init: function () {
        }
    };
}();

jQuery(document).ready(function () {
    if (typeof (window.messageHub) === 'undefined') {
        window.messageHub = $.connection.messageHub;
    }

    messageHub.client.receiveNotification = function (message) {
        var messageHubDto = JSON.parse(message);

        MyRio2cCommon.handleAjaxReturn({
            data: messageHubDto,
            // Success
            onSuccess: function () {

                // The user is in the messages page
                if (typeof (NetworksMessagesConversationsWidget) !== 'undefined') {
                    NetworksMessagesConversationsWidget.reload();
                }
                // The user is not in the messages page
                else {
                    MyRio2cCommon.showAlert({
                        message: labels.youReceivedMessageFrom.replace('{0}', messageHubDto.data.senderName),
                        messageType: 'info',
                        isFixed: false,
                        callbackOnClick: function () {
                            window.location.replace(MyRio2cCommon.getUrlWithCultureAndEdition('/Networks/Messages'));
                        }
                    });
                }
            },
            // Error
            onError: function () {
            }
        });
    };

    $.connection.hub.start();
});
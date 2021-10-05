// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 09-29-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-04-2021
// ***********************************************************************
// <copyright file="audiovisual.meetings.maininformation.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var AudiovisualMeetingsMainInformationWidget = function () {

    var widgetElementId = '#AudiovisualMeetingsMainInformationWidget';
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
        jsonParameters.negotiationUid = $('#AggregateId').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/Meetings/ShowMainInformationWidget'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    enableShowPlugins();
                },
                // Error
                onError: function() {
                }
            });
        })
        .fail(function () {
        })
        .always(function() {
            MyRio2cCommon.unblock({ idOrClass: widgetElementId });
        });
    };


    // Exit Room ----------------------------------------------------------------------------------
    var showExitRoomModal = function () {
        bootbox.dialog({
            message: translations.leaveMeetingconfirmationMessage,
            buttons: {
                cancel: {
                    label: labels.cancel,
                    className: "btn btn-secondary btn-elevate mr-auto",
                    callback: function () {
                    }
                },
                confirm: {
                    label: labels.yes,
                    className: "btn btn-brand btn-elevate",
                    callback: function () {
                        exitRoom();
                    }
                }
            }
        });
    };

    var exitRoom = function () {
        //Disconnect from Jitsi Meet API, because it stills connected when closes the page.
        if (typeof (AudiovisualMeetingsVirtualMeetingWidget) !== 'undefined') {
            AudiovisualMeetingsVirtualMeetingWidget.hangUpParticipant();
        }

        //Redirect to meetings list
        window.location.href = MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/Meetings/Index/');
    }

    return {
        init: function () {
            show();
        },
        exitRoom: function () {
            showExitRoomModal();
        }
    };
}();
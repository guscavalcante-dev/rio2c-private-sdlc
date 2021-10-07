// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 09-29-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-04-2021
// ***********************************************************************
// <copyright file="audiovisual.meetings.virtualmeeting.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var AudiovisualMeetingsVirtualMeetingWidget = function () {

    var widgetElementId = '#AudiovisualMeetingsVirtualMeetingWidget';
    var widgetElement = $(widgetElementId);

    var meetingIframeWidgetElementId = '#MeetingIframeWidget';
    var meetingIframeWidgetElement = $(widgetElementId);
    var api;

    var _negotiationStartDate = $('#NegotiationStartDate').val();
    var _negotiationEndDate = $('#NegotiationEndDate').val();
    var _roomLiberationDate = $('#RoomLiberationDate').val();

    var globalVariables = MyRio2cCommon.getGlobalVariables();

    // Show ----------------------------------------------------------------------------------------------
    var enableShowPlugins = function () {
        KTApp.initTooltips();

        var refreshIdMeeting = setInterval(function () {
            var ret = validateVirtualMeeting();
            if (ret === true) {
                clearInterval(refreshIdMeeting);
            }
        }, 1000);

        var refreshIdTimer = setInterval(function () {
            var ret = validateCountdownTimer();
            if (ret === true) {
                clearInterval(refreshIdTimer);
            }
        }, 1000);

        window.onbeforeunload = function (evt) {
            hangUpParticipant();
            return null;
        };

        //window.addEventListener("beforeunload", function (e) {
        //    hangUpParticipant();
        //}, false);

    };

    var validateVirtualMeeting = function () {

        if (!MyRio2cCommon.isNullOrEmpty(_negotiationStartDate)) {
            const currentDate = new Date();
            const roomLiberationDate = new Date(_roomLiberationDate);
            const negotiationEndDate = new Date(_negotiationEndDate);

            if (currentDate > negotiationEndDate) {
                // Business round has been finished
                $('#FinishedNotice').removeClass('d-none').addClass('d-flex');
                return true;
            }
            else if (currentDate < roomLiberationDate) {
                // Business round has not started yet
                $('#LiberationNotice').removeClass('d-none').addClass('d-flex');
                return false;
            }
            else if (currentDate >= roomLiberationDate && currentDate <= negotiationEndDate) {
                // Business round in progress
                try {
                    $('#LiberationNotice').removeClass('d-flex').addClass('d-none');
                    $('#FinishedNotice').removeClass('d-flex').addClass('d-none');
                    $('#MeetingIframeWidget').removeClass('d-none');

                    showJitsiMeet();
                }
                finally {
                    // finally is important to avoid looping! showJitsiMeet() can throw errors!
                    return true;
                }
            }
            else {
                return true;
            }
        }
    }

    var validateCountdownTimer = function () {
        if (!MyRio2cCommon.isNullOrEmpty(_negotiationStartDate)) {
            const currentDate = new Date();
            const negotiationStartDate = new Date(_negotiationStartDate);
            const negotiationEndDate = new Date(_negotiationEndDate);

            if (currentDate > negotiationEndDate) {
                // Business round has been finished
                return true;
            }
            else if (currentDate < negotiationStartDate) {
                // Business round has not started yet
                //$('#LiberationNotice').removeClass('d-none').addClass('d-flex');
                return false;
            }
            else if (currentDate >= negotiationStartDate && currentDate <= negotiationEndDate) {
                // Business round in progress
                try {
                    if (typeof (Chronograph) !== 'undefined') {
                        Chronograph.init(negotiationEndDate);
                    }
                }
                finally {
                    // finally is important to avoid looping! Chronograph.init() can throw errors!
                    return true;
                }
            }
            else {
                return true;
            }
        }
    }

    var show = function () {
        if (widgetElement.length <= 0) {
            return;
        }

        var jsonParameters = new Object();
        jsonParameters.negotiationUid = $('#AggregateId').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/Meetings/ShowVirtualMeetingWidget'), jsonParameters, function (data) {
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
            })
            .always(function () {
                MyRio2cCommon.unblock({ idOrClass: widgetElementId });
            });
    };

    // Jitsi Meet ----------------------------------------------------------------------------------------------
    var showJitsiMeet = function () {
        var domain = 'meet.jit.si';
        var options = {
            roomName: 'myrio2c-' + $('#AggregateId').val(),
            width: '100%',
            height: '600px',
            parentNode: document.querySelector(meetingIframeWidgetElementId),
            userInfo: {
                email: $('#UserEmail').val(),
                displayName: $('#DisplayName').val()
            },
            configOverwrite: {
                subject: translations.meetings,
                defaultLanguage: 'en',
                //disablePolls: true,
                disableThirdPartyRequests: true,
                toolbarButtons: [
                    'camera',
                    'chat',
                    'closedcaptions',
                    'desktop',
                    'download',
                    'etherpad',
                    'filmstrip',
                    'fullscreen',
                    'hangup',
                    'invite',
                    'microphone',
                    'participants-pane',
                    'profile',
                    'raisehand',
                    'recording',
                    'select-background',
                    'settings',
                    'shortcuts',
                    'stats',
                    'tileview',
                    'toggle-camera',
                    'videoquality',
                    '__end'
                ]
            }
        }

        api = new JitsiMeetExternalAPI(domain, options);
        //api.addEventListener('participantRoleChanged', function(event) {
        //    if (event.role === "moderator") {
        //        api.executeCommand('password', 'teste123');
        //    }
        //});
        //api.on('passwordRequired', function ()
        //{
        //    api.executeCommand('password', 'teste123');
        //});

        //api.dispose();
    }

    var kickAllParticipants = function () {
        if (!MyRio2cCommon.isNullOrEmpty(api)) {
            var allParticipantsInfo = api.getParticipantsInfo();

            allParticipantsInfo.forEach(p => {
                api.executeCommand('kickParticipant',
                    p.participantId
                );
            });
        }
    }

    var hangUpParticipant = function () {
        if (!MyRio2cCommon.isNullOrEmpty(api)) {
            api.executeCommand('hangup'); //Disconnect current user
            //api.dispose(); //Removes the embedded Jitsi Meet conference:
        }
    }

    return {
        init: function () {
            show();
        },
        kickAllParticipants: function () {
            kickAllParticipants();
        },
        hangUpParticipant: function () {
            hangUpParticipant();
        }
    };
}();
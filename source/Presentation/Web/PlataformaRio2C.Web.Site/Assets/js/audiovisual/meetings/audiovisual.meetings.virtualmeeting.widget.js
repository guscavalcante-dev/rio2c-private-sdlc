// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 09-29-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-23-2021
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

    var _negotiationStartDate;
    var _negotiationEndDate;
    var _roomLiberationDate;
    var _userEmail;
    var _userDisplayName;

    // Show ----------------------------------------------------------------------------------------------
    var enableShowPlugins = function () {
        KTApp.initTooltips();

        _negotiationStartDate = meeting.negotiationStartDate;
        _negotiationEndDate = meeting.negotiationEndDate;
        _roomLiberationDate = meeting.roomLiberationDate;

        _userEmail = meeting.userEmail;
        _userDisplayName = meeting.userDisplayName;

        var refreshIdMeeting = setInterval(function () {
            var ret = validateVirtualMeetingStart();
            if (ret === true) {
                clearInterval(refreshIdMeeting);
            }
        }, 1000);

        var refreshIdTimer = setInterval(function () {
            var ret = validateCountdownTimerStart();
            if (ret === true) {
                clearInterval(refreshIdTimer);
            }
        }, 1000);

        window.onbeforeunload = function (evt) {
            hangUpParticipant();
            return null;
        };
    };

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
                email: _userEmail,
                displayName: _userDisplayName
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

    var validateVirtualMeetingStart = function () {

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

    // Countdown timer ------------------------------------------------------------------------------------
    var everyTick = function () {
        // Shows the timer!
        $('#chronographTimer').removeClass('d-none');
    }

    var almostOverCountDown = function () {
        if (typeof (MyRio2cCommon) !== 'undefined') {
            MyRio2cCommon.showAlert({ message: translations.isAlmostOver, messageType: 'info', isFixed: true });
        }

        if (!MyRio2cCommon.isNullOrEmpty(meeting.meetingIsAlmostOverAudioFile)) {
            var audio = new Audio(meeting.meetingIsAlmostOverAudioFile);
            audio.play();
        }
    }

    var almostOverOneMinuteCountDown = function () {
        if (typeof (MyRio2cCommon) !== 'undefined') {
            MyRio2cCommon.showAlert({ message: translations.isAlmostOverOneMinute, messageType: 'warning', isFixed: true });
        }
    }

    var finishedCountdown = function () {
        // Clear countdown labels
        document.getElementById('time').innerText = "-";
        document.getElementById('description').innerText = translations.finished;

        // Disconnect from Jitsi Meet API.
        if (typeof (AudiovisualMeetingsVirtualMeetingWidget) !== 'undefined') {
            AudiovisualMeetingsVirtualMeetingWidget.hangUpParticipant();
        }

        // Show bootbox with "The meeting is over!"
        bootbox.dialog({
            message: translations.isOver,
            buttons: {
                confirm: {
                    label: labels.confirm,
                    className: "btn btn-brand btn-elevate",
                    callback: function () {
                        //Redirect to meetings list
                        window.location.href = MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/Meetings/Index/');
                    }
                }
            }
        });
    }

    var validateCountdownTimerStart = function () {
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
                return false;
            }
            else if (currentDate >= negotiationStartDate && currentDate <= negotiationEndDate) {
                // Business round in progress
                try {
                    if (typeof (Chronograph) !== 'undefined') {
                        Chronograph.init(negotiationEndDate, translations, everyTick, meeting.almostOverMinutes, almostOverCountDown, almostOverOneMinuteCountDown, finishedCountdown);
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
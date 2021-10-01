// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 09-29-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 09-29-2021
// ***********************************************************************
// <copyright file="audiovisual.meetings.virtualmeeting.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var AudiovisualMeetingsVirtualMeetingWidget = function () {

    var widgetElementId = '#AudiovisualMeetingsVirtualMeetingWidget';
    var widgetElement = $(widgetElementId);
    var api;

    var show = function(){
        var domain = 'meet.jit.si';
        var options = {
            roomName: 'myrio2c-' + $('#AggregateId').val(),
            width: '100%',
            height: '600px',
            parentNode: document.querySelector(widgetElementId),
            userInfo: {
                email: $('#UserEmail').val(),
                displayName: $('#DisplayName').val()
            },
            configOverwrite: {
                subject: translations.meetings,
                defaultLanguage: 'en',
                //disablePolls: true,
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

    var kickAllParticipants = function(){
        var allParticipantsInfo = api.getParticipantsInfo();

        allParticipantsInfo.forEach(p => {
            api.executeCommand('kickParticipant',
                p.participantId
            );
        });
    }

    var hangUpParticipant = function () {
        api.executeCommand('hangup'); //Disconnect current user
        //api.dispose(); //Removes the embedded Jitsi Meet conference:
    }

    return {
        init: function () {
            //show();
        },
        kickAllParticipants: function () {
            kickAllParticipants();
        },
        hangUpParticipant: function () {
            hangUpParticipant();
        }
    };
}();
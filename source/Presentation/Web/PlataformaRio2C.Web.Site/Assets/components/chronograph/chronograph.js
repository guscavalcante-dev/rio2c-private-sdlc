// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Renan Valentim
// Created          : 09-30-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-06-2021
// ***********************************************************************
// <copyright file="chronograph.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var Chronograph = function () {
    var one_second = 1000;
    var one_minute = one_second * 60;
    var one_hour = one_minute * 60;
    var one_day = one_hour * 24;
    var countDownDate;
    var showMeetingAlmostOverAlert = true;
    var _messages = {
        inProgress: 'In progress',
        isAlmostOver: 'Meeting is almost over',
        finished: 'Finished'
    };

    var startCountDown = function (endDate, messages, almostFinishedMinutes, almostFinishedCallback, finishedCallback) {

        enablePlugins(messages);
        countDownDate = new Date(endDate).getTime();

        // Before start countDown
        const seconds = ((countDownDate - new Date().getTime()) / 1000);
        var animationDuration = seconds.toString() + "s";
        var almosFinishedSeconds = Math.floor(almostFinishedMinutes * one_minute);

        if (seconds > 0) {
            document.getElementById('hourHand1').style.animationDuration = animationDuration;
            document.getElementById('hourHand2').style.animationDuration = animationDuration;

            document.getElementById('hourHand1').style.animationPlayState = 'running';
            document.getElementById('hourHand2').style.animationPlayState = 'running';

            document.getElementById('description').innerText = _messages.inProgress;

            // Start countDown
            //tick(almostFinishedMinutes, almostFinishedCallback, finishedCallback);

            var refreshId = setInterval(function () {
                var ret = tick(almosFinishedSeconds, almostFinishedCallback, finishedCallback);
                if (ret === true) {
                    clearInterval(refreshId);
                }
            }, 1000);
        }
    }

    var enablePlugins = function (messages) {
        if (messages !== 'undefined') {
            _messages = messages;
        }
    }

    var tick = function (almostFinishedSeconds, almostFinishedCallback, finishedCallback) {
        // Get today's date and time
        var now = new Date().getTime();

        // Find the distance between now and the count down date
        var elapsed = countDownDate - now;

        // Time calculations for days, hours, minutes and seconds
        var parts = [];
        parts[0] = '' + Math.floor(elapsed / one_hour);
        parts[1] = '' + Math.floor((elapsed % one_hour) / one_minute);
        parts[2] = '' + Math.floor(((elapsed % one_hour) % one_minute) / one_second);

        parts[0] = (parts[0].length == 1) ? '0' + parts[0] : parts[0];
        parts[1] = (parts[1].length == 1) ? '0' + parts[1] : parts[1];
        parts[2] = (parts[2].length == 1) ? '0' + parts[2] : parts[2];

        document.getElementById('time').innerText = parts.join(':');

        if (elapsed <= almostFinishedSeconds && showMeetingAlmostOverAlert) {
            if (typeof (almostFinishedCallback) === 'function') {
                almostFinishedCallback();
                showMeetingAlmostOverAlert = false;
                return false;
            }
        }

        // If the count down is finished
        if (elapsed < 0) {
            if (typeof (finishedCallback) === 'function') {
                finishedCallback();
                return true;
            }

            // Clear countdown labels
            document.getElementById('time').innerText = "-";
            document.getElementById('description').innerText = _messages.finished;
            return true;
        }

        return false;
    }

    return {
        init: function (endDate, messages, almostFinishedMinutes, almostFinishedCallback, finishedCallback) {
            startCountDown(endDate, messages, almostFinishedMinutes, almostFinishedCallback, finishedCallback);
        }
    };
}();
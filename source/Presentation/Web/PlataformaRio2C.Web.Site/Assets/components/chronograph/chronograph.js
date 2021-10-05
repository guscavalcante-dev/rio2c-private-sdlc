// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Renan Valentim
// Created          : 09-30-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-04-2021
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

    // http://paulirish.com/2011/requestanimationframe-for-smart-animating/
    var requestAnimationFrame = (function () {
        return window.requestAnimationFrame ||
            window.webkitRequestAnimationFrame ||
            window.mozRequestAnimationFrame ||
            window.oRequestAnimationFrame ||
            window.msRequestAnimationFrame ||
            function (callback) {
                window.setTimeout(callback, 1000 / 60);
            };
    }());

    var tick = function() {
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

        // If the count down is finished, write some text
        if (elapsed < 0) {
            document.getElementById('time').innerText = "-";
            document.getElementById('description').innerText = translations.finished;
            return;
        }

        requestAnimationFrame(tick);
    }

    var startCountDown = function (endDate) {
        countDownDate = new Date(endDate).getTime();

        //before start countDown
        const seconds = ((countDownDate - new Date().getTime()) / 1000);
        var animationDuration = seconds.toString() + "s";

        if (seconds > 0) {
            document.getElementById('hourHand1').style.animationDuration = animationDuration;
            document.getElementById('hourHand2').style.animationDuration = animationDuration;

            document.getElementById('hourHand1').style.animationPlayState = 'running';
            document.getElementById('hourHand2').style.animationPlayState = 'running';

            document.getElementById('description').innerText = translations.inProgress;

            //start countDown
            tick();
        }
    }

    return {
        init: function (endDate) {
            startCountDown(endDate);
        }
    };
}();
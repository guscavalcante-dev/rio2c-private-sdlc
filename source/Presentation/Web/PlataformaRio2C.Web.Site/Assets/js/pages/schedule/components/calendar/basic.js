"use strict";

var KTCalendarBasic = function() {

    return {
        //main function to initiate the module
        init: function() {
            var todayDate = moment().startOf('day');
            var YM = todayDate.format('YYYY-MM');
            var YESTERDAY = todayDate.clone().subtract(1, 'day').format('YYYY-MM-DD');
            var TODAY = todayDate.format('YYYY-MM-DD');
            var TOMORROW = todayDate.clone().add(1, 'day').format('YYYY-MM-DD');

            var TODAYm1 = todayDate.clone().subtract(1, 'day').format('YYYY-MM-DD');
            var TODAYm2 = todayDate.clone().subtract(2, 'day').format('YYYY-MM-DD');
            var TODAYm3 = todayDate.clone().subtract(3, 'day').format('YYYY-MM-DD');
            var TODAYm4 = todayDate.clone().subtract(4, 'day').format('YYYY-MM-DD');

            var TODAYp1 = todayDate.clone().add(1, 'day').format('YYYY-MM-DD');
            var TODAYp2 = todayDate.clone().add(2, 'day').format('YYYY-MM-DD');
            var TODAYp3 = todayDate.clone().add(3, 'day').format('YYYY-MM-DD');
            var TODAYp4 = todayDate.clone().add(4, 'day').format('YYYY-MM-DD');

            var calendarEl = document.getElementById('kt_calendar');
            var calendar = new FullCalendar.Calendar(calendarEl, {
                plugins: [ 'interaction', 'dayGrid', 'timeGrid', 'list' ],

                isRTL: KTUtil.isRTL(),
                header: {
                    left: 'prev,next today',
                    center: 'title',
                    right: 'dayGridMonth,timeGridWeek,timeGridDay'
                },

                height: 800,
                contentHeight: 780,
                aspectRatio: 1.35,  // see: https://fullcalendar.io/docs/aspectRatio

                nowIndicator: true,
                now: TODAY + 'T09:25:00', // just for demo

                views: {
                    dayGridMonth: { buttonText: 'month' },
                    timeGridWeek: { buttonText: 'week' },
                    timeGridDay: { buttonText: 'day' },
                    timeGridFourDay: {
                        type: 'timeGrid',
                        duration: { days: 4 },
                        buttonText: '4 day'
                    }
                },

                

                defaultView: 'timeGridFourDay',
                defaultDate: TODAY,

                editable: true,
                eventLimit: true, // allow "more" link when too many events
                navLinks: true,
                events: [
                    {
                        title: 'MULHERES NO AUDIOVISUAL BRAZILIAN CONTENT',
                        start: TODAYp3 + 'T10:30:00',
                        end: TODAYp3 + 'T12:30:00',
                        className: "fc-event-solid-danger fc-event-light"
                    },
                    {
                        title: 'DOC & IMPACTO SOCIAL SALA AUDIOVISUAL',
                        start: TODAYp2 + 'T13:30:00',
                        end: TODAYp2 + 'T15:30:00',
                        className: "fc-event-solid-danger fc-event-light"
                    },
                    {
                        title: 'E quem se importa? Rodadas de Negócios(Sala 2)',
                        start: TODAYp1 + 'T13:00:00',
                        end: TODAYp1 + 'T14:00:00',
                        className: "fc-event-solid-info fc-event-light"
                    },
                    {
                        title: '“Palco História – Mulheres Brasileiras”_3 Rodadas de Negócios(Sala 2)',
                        start: TODAYp1 + 'T14:00:00',
                        end: TODAYp1 + 'T15:00:00',
                        className: "fc-event-solid-info fc-event-light"
                    },
                    {
                        title: 'UM OUTRO FRANCISCO Rodadas de Negócios(Sala 2)',
                        start: TODAYp1 + 'T15:00:00',
                        end: TODAYp1 + 'T16:00:00',
                        className: "fc-event-solid-info fc-event-light"
                    }
                    
                ],

                eventRender: function(info) {
                    var element = $(info.el);

                    if (info.event.extendedProps && info.event.extendedProps.description) {
                        if (element.hasClass('fc-day-grid-event')) {
                            element.data('content', info.event.extendedProps.description);
                            element.data('placement', 'top');
                            KTApp.initPopover(element);
                        } else if (element.hasClass('fc-time-grid-event')) {
                            element.find('.fc-title').append('<div class="fc-description">' + info.event.extendedProps.description + '</div>'); 
                        } else if (element.find('.fc-list-item-title').lenght !== 0) {
                            element.find('.fc-list-item-title').append('<div class="fc-description">' + info.event.extendedProps.description + '</div>'); 
                        }
                    } 
                }
            });

            calendar.render();
        }
    };
}();

jQuery(document).ready(function() {
    KTCalendarBasic.init();
});
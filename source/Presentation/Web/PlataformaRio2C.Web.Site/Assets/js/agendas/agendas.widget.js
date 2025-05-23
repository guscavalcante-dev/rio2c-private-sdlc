// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 03-27-2020
//
// Last Modified By : Renan Valentim
// Last Modified On : 05-12-2025
// ***********************************************************************
// <copyright file="schedules.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var AgendasWidget = function () {

    var widgetElementName = 'AgendaWidget';
    var widgetElementId = '#' + widgetElementName;
    var widgetElement = $(widgetElementId);
    var calendar;

    var globalVariables = MyRio2cCommon.getGlobalVariables();

    //Print PDF -----------------------------------------------------------------------------------
    var printToPdf = function (info) {
        if (widgetElement.length <= 0) {
            return;
        }

        var jsonParameters = new Object();
        jsonParameters.startDate = moment(calendar.view.activeStart).unix();
        jsonParameters.endDate = moment(calendar.view.activeEnd).unix();
        jsonParameters.showOneToOneMeetings = $('#ShowOneToOneMeetings').is(':checked');

        window.open(
            MyRio2cCommon.getUrlWithCultureAndEdition('/Agendas/PrintAudiovisualMeetingsToPdfAsync') +
            '?startDate=' + jsonParameters.startDate +
            '&endDate=' + jsonParameters.endDate +
            '&showOneToOneMeetings=' + jsonParameters.showOneToOneMeetings,
            '_blank')
    }

    // Calendar Data ------------------------------------------------------------------------------
    var getFirstDate = function () {
        return editionStartDate;
    };

    // Enable calendar ----------------------------------------------------------------------------
    var formatPopupDate = function (startDate, endDate) {
        startDate = moment(startDate).tz(globalVariables.momentTimeZone).locale(globalVariables.userInterfaceLanguage);//.format('L LTS');
        endDate = moment(endDate).tz(globalVariables.momentTimeZone).locale(globalVariables.userInterfaceLanguage);
        var date = startDate.format("ddd, D") + " of " + startDate.format("MMMM");

        if (endDate == null
            || endDate === ''
            || (startDate.year() === endDate.year()
                && startDate.month() === endDate.month()
                && startDate.day() === endDate.day()
                && startDate.hours() === endDate.hours()
                && startDate.minutes() === endDate.minutes())) {
            date += ", " + startDate.format("HH:mm");
        }
        else if (startDate.day() === endDate.day()) {
            date += ", " + startDate.format("HH:mm") + " &#8210 " + endDate.format("HH:mm");
        }
        else {
            date += ", " + startDate.format("HH:mm") + " &#8210 " + endDate.format("ddd, D") + " of " + startDate.format("MMMM") + ", " + endDate.format("HH:mm");
        }

        return date;
    }

    var enableCalendar = function () {
        if (widgetElement.length <= 0) {
            return;
        }

        var calendarEl = document.getElementById(widgetElementName);
        calendar = new FullCalendar.Calendar(calendarEl, {
            plugins: ['interaction', 'dayGrid', 'timeGrid', 'list'],
            header: {
                left: 'prev,next today',
                center: 'title',
                right: 'dayGridMonth,timeGridWeek,timeGridDay,listMonth,timeGridFourDay printButton'
            },
            views: {
                timeGridFourDay: {
                    type: 'timeGrid',
                    duration: { days: 4 },
                    buttonText: translations.fourDaysButton
                }
            },
            //customButtons: {
            //    printButton: {
            //        text: translations.printButton,
            //        click: function (info) {
            //            printToPdf(info);
            //        }
            //    }
            //},
            viewSkeletonRender: function (info) {
                calendarEl.querySelectorAll('.fc-button').forEach((button) => {
                    if (button.innerText === translations.printButton) {
                        button.classList.add('fc-button-active')
                    }
                })
            },
            defaultView: 'timeGridFourDay',
            defaultDate: getFirstDate(),
            locale: globalVariables.userInterfaceLanguage,
            buttonIcons: true,
            weekNumbers: false,
            navLinks: true,
            editable: false,
            eventLimit: false,
            displayEventTime: false,
            eventSources: [
                {
                    events: function (info, successCallback, failureCallback) {
                        var jsonParameters = new Object();
                        jsonParameters.startDate = moment(info.start).unix();
                        jsonParameters.endDate = moment(info.end).unix();
                        jsonParameters.showMyConferences = $('#ShowMyConferences').is(':checked');
                        jsonParameters.showAllConferences = $('#ShowAllConferences').is(':checked');

                        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Agendas/GetConferencesData'), jsonParameters, function (data) {
                            MyRio2cCommon.handleAjaxReturn({
                                data: data,
                                // Success
                                onSuccess: function () {
                                    if (data.events === null) {
                                        successCallback([]);
                                        return;
                                    }

                                    successCallback(
                                        Array.prototype.slice.call(data.events).map(function (eventEl) {
                                            return {
                                                id: eventEl.Id,
                                                title: eventEl.Title,
                                                start: moment(eventEl.Start).tz(globalVariables.momentTimeZone).format(),
                                                end: moment(eventEl.End).tz(globalVariables.momentTimeZone).format(),
                                                allDay: eventEl.AllDay || false,
                                                type: eventEl.Type,
                                                className: eventEl.Css,
                                                editionEvent: eventEl.EditionEvent,
                                                synopsis: eventEl.Synopsis,
                                                room: eventEl.Room
                                            }
                                        })
                                    );
                                },
                                // Error
                                onError: function () {
                                    failureCallback('error');
                                }
                            });
                        })
                            .fail(function () {
                            })
                            .always(function () {
                                //MyRio2cCommon.unblock();
                            });
                    }
                },
                {
                    events: function (info, successCallback, failureCallback) {
                        var jsonParameters = new Object();
                        jsonParameters.startDate = moment(info.start).unix();
                        jsonParameters.endDate = moment(info.end).unix();
                        jsonParameters.showOneToOneMeetings = $('#ShowOneToOneMeetings').is(':checked');

                        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Agendas/GetAudiovisualMeetingsData'), jsonParameters, function (data) {
                            MyRio2cCommon.handleAjaxReturn({
                                data: data,
                                // Success
                                onSuccess: function () {
                                    if (data.events === null) {
                                        successCallback([]);
                                        return;
                                    }

                                    successCallback(
                                        Array.prototype.slice.call(data.events).map(function (eventEl) {
                                            return {
                                                id: eventEl.Id,
                                                title: eventEl.Title,
                                                start: moment(eventEl.Start).tz(globalVariables.momentTimeZone).format(),
                                                end: moment(eventEl.End).tz(globalVariables.momentTimeZone).format(),
                                                allDay: eventEl.AllDay || false,
                                                type: eventEl.Type,
                                                className: eventEl.Css,
                                                projectLogLine: eventEl.ProjectLogLine,
                                                producer: eventEl.Producer,
                                                player: eventEl.Player,
                                                room: eventEl.Room,
                                                tableNumber: eventEl.TableNumber,
                                                roundNumber: eventEl.RoundNumber
                                            }
                                        })
                                    );

                                    changePrintButtonVisibility(data.events.length > 0);
                                },
                                // Error
                                onError: function () {
                                    failureCallback('error');
                                }
                            });
                        })
                            .fail(function () {
                            })
                            .always(function () {
                                //MyRio2cCommon.unblock();
                            });
                    }
                },
                {
                    events: function (info, successCallback, failureCallback) {
                        var jsonParameters = new Object();
                        jsonParameters.startDate = moment(info.start).unix();
                        jsonParameters.endDate = moment(info.end).unix();
                        jsonParameters.showOneToOneMeetings = $('#ShowOneToOneMeetings').is(':checked');

                        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Agendas/GetMusicMeetingsData'), jsonParameters, function (data) {
                            MyRio2cCommon.handleAjaxReturn({
                                data: data,
                                // Success
                                onSuccess: function () {
                                    if (data.events === null) {
                                        successCallback([]);
                                        return;
                                    }

                                    successCallback(
                                        Array.prototype.slice.call(data.events).map(function (eventEl) {
                                            return {
                                                id: eventEl.Id,
                                                title: eventEl.Title,
                                                start: moment(eventEl.Start).tz(globalVariables.momentTimeZone).format(),
                                                end: moment(eventEl.End).tz(globalVariables.momentTimeZone).format(),
                                                allDay: eventEl.AllDay || false,
                                                type: eventEl.Type,
                                                className: eventEl.Css,
                                                participant: eventEl.Participant,
                                                player: eventEl.Player,
                                                room: eventEl.Room,
                                                tableNumber: eventEl.TableNumber,
                                                roundNumber: eventEl.RoundNumber
                                            }
                                        })
                                    );

                                    changePrintButtonVisibility(data.events.length > 0);
                                },
                                // Error
                                onError: function () {
                                    failureCallback('error');
                                }
                            });
                        })
                            .fail(function () {
                            })
                            .always(function () {
                                //MyRio2cCommon.unblock();
                            });
                    }
                },
                {
                    events: function (info, successCallback, failureCallback) {
                        var jsonParameters = new Object();
                        jsonParameters.startDate = moment(info.start).unix();
                        jsonParameters.endDate = moment(info.end).unix();
                        jsonParameters.showFlights = $('#ShowFlights').is(':checked');

                        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Agendas/GetLogisticAirfaresData'), jsonParameters, function (data) {
                            MyRio2cCommon.handleAjaxReturn({
                                data: data,
                                // Success
                                onSuccess: function () {
                                    if (data.events === null) {
                                        successCallback([]);
                                        return;
                                    }

                                    successCallback(
                                        Array.prototype.slice.call(data.events).map(function (eventEl) {
                                            return {
                                                id: eventEl.Id,
                                                title: eventEl.Title,
                                                start: moment(eventEl.Start).tz(globalVariables.momentTimeZone).format(),
                                                end: moment(eventEl.End).tz(globalVariables.momentTimeZone).format(),
                                                allDay: eventEl.AllDay || false,
                                                type: eventEl.Type,
                                                className: eventEl.Css,
                                                flightType: eventEl.FlightType,
                                                fromPlace: eventEl.FromPlace,
                                                toPlace: eventEl.ToPlace,
                                                ticketNumber: eventEl.TicketNumber,
                                            }
                                        })
                                    );
                                },
                                // Error
                                onError: function () {
                                    failureCallback('error');
                                }
                            });
                        })
                            .fail(function () {
                            })
                            .always(function () {
                                //MyRio2cCommon.unblock();
                            });
                    }
                },
                {
                    events: function (info, successCallback, failureCallback) {
                        var jsonParameters = new Object();
                        jsonParameters.startDate = moment(info.start).unix();
                        jsonParameters.endDate = moment(info.end).unix();
                        jsonParameters.showAccommodations = $('#ShowAccommodations').is(':checked');

                        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Agendas/GetLogisticAccommodationsData'), jsonParameters, function (data) {
                            MyRio2cCommon.handleAjaxReturn({
                                data: data,
                                // Success
                                onSuccess: function () {
                                    if (data.events === null) {
                                        successCallback([]);
                                        return;
                                    }

                                    successCallback(
                                        Array.prototype.slice.call(data.events).map(function (eventEl) {
                                            return {
                                                id: eventEl.Id,
                                                title: eventEl.Title,
                                                start: moment(eventEl.Start).tz(globalVariables.momentTimeZone).format(),
                                                end: moment(eventEl.End).tz(globalVariables.momentTimeZone).format(),
                                                allDay: eventEl.AllDay || false,
                                                type: eventEl.Type,
                                                className: eventEl.Css,
                                                subType: eventEl.SubType,
                                                checkInDate: eventEl.CheckInDate,
                                                checkOutDate: eventEl.CheckOutDate
                                            }
                                        })
                                    );
                                },
                                // Error
                                onError: function () {
                                    failureCallback('error');
                                }
                            });
                        })
                            .fail(function () {
                            })
                            .always(function () {
                                //MyRio2cCommon.unblock();
                            });
                    }
                },
                {
                    events: function (info, successCallback, failureCallback) {
                        var jsonParameters = new Object();
                        jsonParameters.startDate = moment(info.start).unix();
                        jsonParameters.endDate = moment(info.end).unix();
                        jsonParameters.showTransfers = $('#ShowTransfers').is(':checked');

                        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Agendas/GetLogisticTransfersData'), jsonParameters, function (data) {
                            MyRio2cCommon.handleAjaxReturn({
                                data: data,
                                // Success
                                onSuccess: function () {
                                    if (data.events === null) {
                                        successCallback([]);
                                        return;
                                    }

                                    successCallback(
                                        Array.prototype.slice.call(data.events).map(function (eventEl) {
                                            return {
                                                id: eventEl.Id,
                                                title: eventEl.Title,
                                                start: moment(eventEl.Start).tz(globalVariables.momentTimeZone).format(),
                                                end: moment(eventEl.End).tz(globalVariables.momentTimeZone).format(),
                                                allDay: eventEl.AllDay || false,
                                                type: eventEl.Type,
                                                className: eventEl.Css
                                            }
                                        })
                                    );
                                },
                                // Error
                                onError: function () {
                                    failureCallback('error');
                                }
                            });
                        })
                            .fail(function () {
                            })
                            .always(function () {
                                //MyRio2cCommon.unblock();
                            });
                    }
                }
            ],
            eventRender: function (info) {
                var element = $(info.el);

                // Close other open popovers
                element.on('click', function (e) {
                    $('.popover').popover('hide');
                });

                if (info.event.extendedProps.type === 'Conference') {
                    showConferencePopover(element, info);
                }
                else if (info.event.extendedProps.type === 'AudiovisualMeeting') {
                    showAudiovisualMeetingPopover(element, info);
                }
                else if (info.event.extendedProps.type === 'MusicMeeting') {
                    showMusicMeetingPopover(element, info);
                }
                else if (info.event.extendedProps.type === 'LogisticAirfare') {
                    showLogisticAirfarePopover(element, info);
                }
                else if (info.event.extendedProps.type === 'LogisticAccommodation') {
                    showLogisticAccommodationPopover(element, info);
                }
                else if (info.event.extendedProps.type === 'LogisticTransfer') {
                    showLogisticTransferPopover(element, info);
                }
            },
            loading: function (isLoading, view) {
                if (isLoading) {
                    MyRio2cCommon.block({ idOrClass: widgetElementId });
                }
                else {
                    MyRio2cCommon.unblock({ idOrClass: widgetElementId });
                }
            },
            select: function (start, end, jsEvent) {
                $('.popover').popover('hide');
            }
        });

        calendar.render();
    };

    var changePrintButtonVisibility = function (hasScheduledAudiovisualMeetings) {
        if (hasScheduledAudiovisualMeetings && (isBuyer || isSeller)) {
            calendar.setOption("customButtons",
                {
                    printButton: {
                        text: translations.printButton,
                        click: function (info) {
                            printToPdf(info);
                        }
                    }
                });
        }
    }

    var reload = function () {
        calendar.refetchEvents();
    }

    var enableChangeEvents = function () {
        $('.enable-calendar-reload').not('.change-event-enabled').on('change', function () {
            reload();
        });
        $('.enable-calendar-reload').addClass('change-event-enabled');

        $('body').on('click', function (e) {
            if ($(e.target).parents('.fc-event-container').length === 0 && $(e.target).parents('.fc-list-item').length === 0 && $(e.target).parents('.popover.in').length === 0) {
                $('.popover').popover('hide');
            }
            //if (!$(e.target).hasClass('fc-content') && !$(e.target).hasClass('fc-title') && !$(e.target).hasClass('fc-event') 
            //    && !$(e.target).hasClass('fc-event-dot') && !$(e.target).hasClass('fc-list-item-marker') && !$(e.target).hasClass('fc-list-item-title') && !$(e.target).parent().hasClass('fc-list-item-title')
            //    && $(e.target).parents('.popover.in').length === 0) {
            //    $('.popover-enabled').popover('hide');
            //}
        });
    };

    // Popovers -----------------------------------------------------------------------------------
    var showConferencePopover = function (element, info) {
        var popoverHtml = $("#conference-popover-event-content").html();
        var startDate = info.event.start;
        var endDate = info.event.end;

        element.popover({
            html: true,
            placement: 'top',
            content: function () {
                return popoverHtml
                    .replace("popoverDate", formatPopupDate(startDate, endDate))
                    .replace("popoverEditionEvent", info.event.extendedProps.editionEvent)
                    .replace("popoverSynopsis", info.event.extendedProps.synopsis)
                    .replace("popoverRoom", info.event.extendedProps.room);
            },
            template: '<div class="fullcalendar-popover popover" role="tooltip"><div class="arrow"></div><h3 class="popover-header"></h3><div class="popover-body"></div></div>',
            title: '<span class="text-info">' + info.event.title + '</span>',
            container: 'body'
        });
    }

    var showAudiovisualMeetingPopover = function (element, info) {
        var popoverHtml = $("#audiovisual-meeting-popover-event-content").html();
        var startDate = info.event.start;
        var endDate = info.event.end;

        element.popover({
            html: true,
            placement: 'top',
            content: function () {
                var popover = popoverHtml
                    .replace("popoverDate", formatPopupDate(startDate, endDate))
                    .replace("popoverProjectLogLine", info.event.extendedProps.projectLogLine)
                    .replace("popoverProducer", info.event.extendedProps.producer)
                    .replace("popoverPlayer", info.event.extendedProps.player)
                    .replace("popoverRoom", info.event.extendedProps.room)
                    .replace("popoverTableNumber", info.event.extendedProps.tableNumber)
                    .replace("popoverRoundNumber", info.event.extendedProps.roundNumber);

                return popover;
            },
            template: '<div class="fullcalendar-popover popover" role="tooltip"><div class="arrow"></div><h3 class="popover-header"></h3><div class="popover-body"></div></div>',
            title: '<span class="text-info">' + info.event.title + '</span>',
            container: 'body'
        });
    }

    var showMusicMeetingPopover = function (element, info) {
        var popoverHtml = $("#music-meeting-popover-event-content").html();
        var startDate = info.event.start;
        var endDate = info.event.end;

        element.popover({
            html: true,
            placement: 'top',
            content: function () {
                var popover = popoverHtml
                    .replace("popoverDate", formatPopupDate(startDate, endDate))
                    .replace("popoverParticipant", info.event.extendedProps.participant)
                    .replace("popoverPlayer", info.event.extendedProps.player)
                    .replace("popoverRoom", info.event.extendedProps.room)
                    .replace("popoverTableNumber", info.event.extendedProps.tableNumber)
                    .replace("popoverRoundNumber", info.event.extendedProps.roundNumber);

                return popover;
            },
            template: '<div class="fullcalendar-popover popover" role="tooltip"><div class="arrow"></div><h3 class="popover-header"></h3><div class="popover-body"></div></div>',
            title: '<span class="text-info">' + info.event.title + '</span>',
            container: 'body'
        });
    }

    var showLogisticAirfarePopover = function (element, info) {
        var popoverHtml = $("#airfare-popover-event-content").html();
        var startDate = info.event.start;
        var endDate = info.event.end;

        element.popover({
            html: true,
            placement: 'top',
            content: function () {
                return popoverHtml
                    .replace("popoverDate", formatPopupDate(startDate, endDate))
                    .replace("popoverFlightType", info.event.extendedProps.flightType)
                    .replace("popoverFromPlace", info.event.extendedProps.fromPlace)
                    .replace("popoverToPlace", info.event.extendedProps.toPlace)
                    .replace("popoverTicketNumber", info.event.extendedProps.ticketNumber);
            },
            template: '<div class="fullcalendar-popover popover" role="tooltip"><div class="arrow"></div><h3 class="popover-header"></h3><div class="popover-body"></div></div>',
            title: '<span class="text-info">' + info.event.title + '</span>',
            container: 'body'
        });
    }

    var showLogisticAccommodationPopover = function (element, info) {
        var popoverHtml = $("#accommodation-popover-event-content").html();
        var startDate = info.event.start;

        element.popover({
            html: true,
            placement: 'top',
            content: function () {
                return popoverHtml
                    .replace("popoverDate", formatPopupDate(
                        info.event.extendedProps.subType === 'AllDay' ? info.event.extendedProps.checkInDate : startDate,
                        info.event.extendedProps.subType === 'AllDay' ? info.event.extendedProps.checkOutDate : startDate));
            },
            template: '<div class="fullcalendar-popover popover" role="tooltip"><div class="arrow"></div><h3 class="popover-header"></h3><div class="popover-body"></div></div>',
            title: '<span class="text-info">' + info.event.title + '</span>',
            container: 'body'
        });
    }

    var showLogisticTransferPopover = function (element, info) {
        var popoverHtml = $("#transfer-popover-event-content").html();
        var startDate = info.event.start;

        element.popover({
            html: true,
            placement: 'top',
            content: function () {
                return popoverHtml
                    .replace("popoverDate", formatPopupDate(startDate, startDate));
            },
            template: '<div class="fullcalendar-popover popover" role="tooltip"><div class="arrow"></div><h3 class="popover-header"></h3><div class="popover-body"></div></div>',
            title: '<span class="text-info">' + info.event.title + '</span>',
            container: 'body'
        });
    }

    return {
        init: function () {
            enableChangeEvents();
            enableCalendar();
        }
    };
}();
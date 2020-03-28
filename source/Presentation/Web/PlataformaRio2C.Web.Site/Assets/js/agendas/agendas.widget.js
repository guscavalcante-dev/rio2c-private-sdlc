// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 03-27-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-28-2020
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
                right: 'dayGridMonth,timeGridWeek,timeGridDay,listMonth,timeGridFourDay'
            },
            views: {
	             timeGridFourDay: {
		             type: 'timeGrid',
		             duration: { days: 4 },
		             buttonText: translations.fourDaysButton
	             }
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
					events: function(info, successCallback, failureCallback) {
						var jsonParameters = new Object();
                        jsonParameters.startDate = moment(info.start).unix();
                        jsonParameters.endDate = moment(info.end).unix();
                        jsonParameters.showMyConferences = $('#ShowMyConferences').is(':checked');
                        jsonParameters.showAllConferences = $('#ShowAllConferences').is(':checked');

                        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Agendas/GetConferencesData'), jsonParameters, function(data) {
							MyRio2cCommon.handleAjaxReturn({
								data: data,
								// Success
                                onSuccess: function () {
                                    if (data.events === null) {
                                        successCallback([]);
                                        return;
                                    }

                                    successCallback(
										Array.prototype.slice.call(data.events).map(function(eventEl) {
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
								onError: function() {
								failureCallback('error');
								}
							});
						})
						.fail(function() {
						})
						.always(function() {
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
                    $('.popover-enabled').popover('hide');
                });

                if (info.event.extendedProps.type === 'Conference') {
	                showConferencePopover(element, info);
                }
                else if (info.event.extendedProps.type === 'AudiovisualMeeting') {
	                showMeetingPopover(element, info);
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
            }
        });

        calendar.render();
    };

    var reload = function () {
	    calendar.refetchEvents();
    }

    var enableChangeEvents = function () {
        $('.enable-calendar-reload').not('.change-event-enabled').on('change', function () {
	        reload();
	    });
        $('.enable-calendar-reload').addClass('change-event-enabled');

        $('body').on('click', function (e) {
	        //did not click a popover toggle or popover
            if (!$(e.target).hasClass('fc-content') && !$(e.target).hasClass('fc-title') && !$(e.target).hasClass('fc-event') 
	            && !$(e.target).hasClass('fc-event-dot') && !$(e.target).hasClass('fc-list-item-marker') && !$(e.target).hasClass('fc-list-item-title') && !$(e.target).parent().hasClass('fc-list-item-title')
		        && $(e.target).parents('.popover.in').length === 0) {
                $('.popover-enabled').popover('hide');
	        }
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

    var showMeetingPopover = function (element, info) {
	    var popoverHtml = $("#meeting-popover-event-content").html();
	    var startDate = info.event.start;
	    var endDate = info.event.end;

	    element.popover({
		    html: true,
		    placement: 'top',
		    content: function () {
			    return popoverHtml
						    .replace("popoverDate", formatPopupDate(startDate, endDate))
						    .replace("popoverProjectLogLine", info.event.extendedProps.projectLogLine)
						    .replace("popoverProducer", info.event.extendedProps.producer)
						    .replace("popoverPlayer", info.event.extendedProps.player)
						    .replace("popoverRoom", info.event.extendedProps.room)
						    .replace("popoverTableNumber", info.event.extendedProps.tableNumber)
							.replace("popoverRoundNumber", info.event.extendedProps.roundNumber);
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
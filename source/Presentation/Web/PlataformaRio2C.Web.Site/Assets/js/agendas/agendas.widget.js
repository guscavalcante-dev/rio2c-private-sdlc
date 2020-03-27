// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 03-27-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-27-2020
// ***********************************************************************
// <copyright file="schedules.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
//"use strict";

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
                                                className: eventEl.Css
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
                },
                {
                    events: function (info, successCallback, failureCallback) {
                        var jsonParameters = new Object();
                        jsonParameters.startDate = moment(info.start).unix();
                        jsonParameters.endDate = moment(info.end).unix();
                        jsonParameters.showFlights = $('#ShowFlights').is(':checked');
                        jsonParameters.showAccommodations = $('#ShowAccommodations').is(':checked');
                        jsonParameters.showTransfers = $('#ShowTransfers').is(':checked');

                        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Agendas/GetLogisticsData'), jsonParameters, function (data) {
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
                var tooltip = new Tooltip(info.el, {
                    title: 'Teste',
                    placement: 'top',
                    trigger: 'hover',
                    container: 'body'
                });
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
    };

    return {
        //main function to initiate the module
        init: function () {
	        enableChangeEvents();
            enableCalendar();
        }
    };
}();
// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 01-24-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-25-2020
// ***********************************************************************
// <copyright file="audiovisual.meetings.create.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var AudiovisualMeetingsCreate = function () {

    var modalId = '#CreateAudiovisualMeetingModal';
    var formId = '#CreateAudiovisualMeetingForm';
    var buyerOrganizationId = '#BuyerOrganizationUid';
    var projectId = '#ProjectUid';
    var negotiationConfigId = '#NegotiationConfigUid';
    var negotiationRoomConfigId = '#NegotiationRoomConfigUid';
    var startTimeId = '#StartTime';
    var roundNumberId = '#RoundNumber';
    var globalVariables = MyRio2cCommon.getGlobalVariables();

    // Buyer organization select2 ----------------------------------------------------------------
    var enableBuyerOrganizationChangeEvent = function () {
        var element = $(buyerOrganizationId);

        element.not('.change-event-enabled').on('change', function () {
		    toogleProjectSelect2();
	    });
	    element.addClass('change-event-enabled');
    };

    // Project select2 ---------------------------------------------------------------------------
    var toogleProjectSelect2 = function () {
	    var element = $(projectId);

	    if ($(buyerOrganizationId).val() !== '') {
		    element.removeClass('disabled');
		    element.prop("disabled", false);
	    }
	    else {
		    element.addClass('disabled');
		    element.prop("disabled", true);
		    $(projectId).val('').trigger('change');
	    }
    };

    var enableProjectChangeEvent = function () {
        var element = $(projectId);

	    element.not('.change-event-enabled').on('change', function () {
		    toogleDateSelect2();
	    });
	    element.addClass('change-event-enabled');
    };

    // Date select2 ------------------------------------------------------------------------------
    var toogleDateSelect2 = function () {
        var element = $(negotiationConfigId);

        if ($(projectId).val() !== '') {
	        enableDateSelect2();
		    element.removeClass('disabled');
		    element.prop("disabled", false);
	    }
	    else {
		    element.addClass('disabled');
		    element.prop("disabled", true);
            $(negotiationConfigId).val('').trigger('change');
	    }
    };

    var emptyStateSelect2 = function () {
        $(negotiationConfigId).val('').trigger('change');

        $(negotiationConfigId).select2({
            language: globalVariables.userInterfaceLanguage,
            width: '100%',
            placeholder: translations.dateDropdownPlaceholder,
            data: []
        });

        $(negotiationConfigId).empty();
    };

    var enableDateSelect2 = function () {
        var projectUid = $(projectId).val();

        if (MyRio2cCommon.isNullOrEmpty(projectUid)) {
            emptyStateSelect2();
        }
        else {
	        var jsonParameters = new Object();
	        jsonParameters.customFilter = 'HasManualTables';
            jsonParameters.buyerOrganizationUid = $(buyerOrganizationId).val();

	        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/MeetingParameters/FindAllDates'), jsonParameters, function (data) {
	            MyRio2cCommon.handleAjaxReturn({
	                data: data,
	                // Success
	                onSuccess: function () {
                        if (data.negotiationConfigs.length <= 0) {
	                        emptyStateSelect2();
	                    }

	                    var negotiationConfigsData = new Array();

	                    // Placeholder
	                    var negotiationConfigObject = new Object();
	                    negotiationConfigObject.id = '';
	                    negotiationConfigObject.text = labels.selectPlaceholder;
	                    negotiationConfigsData.push(negotiationConfigObject);

	                    for (var i in data.negotiationConfigs) {
	                        if (data.negotiationConfigs.hasOwnProperty(i)) {
	                            negotiationConfigObject = new Object();
	                            negotiationConfigObject.id = data.negotiationConfigs[i].Uid;
                                negotiationConfigObject.text = moment(data.negotiationConfigs[i].StartDate).tz(globalVariables.momentTimeZone).locale(globalVariables.userInterfaceLanguage).format('L');
	                            negotiationConfigsData.push(negotiationConfigObject);
	                        }
	                    }

	                    $(negotiationConfigId).empty().select2({
                            language: globalVariables.userInterfaceLanguage,
	                        width: '100%',
                            placeholder: translations.dateDropdownPlaceholder,
	                        triggerChange: true,
	                        allowClear: true,
	                        data: negotiationConfigsData
	                    });
	                },
	                // Error
	                onError: function () {
	                    emptyStateSelect2();
	                }
	            });
	        })
	        .fail(function () {
	            emptyStateSelect2();
	        })
	        .always(function () {
	            MyRio2cCommon.unblock();
	        });
        }
    };

    var enableDateChangeEvent = function () {
	    var element = $(negotiationConfigId);

	    element.not('.change-event-enabled').on('change', function () {
		    toogleRoomSelect2();
	    });
	    element.addClass('change-event-enabled');
    };

    // Room select2 ------------------------------------------------------------------------------
    var toogleRoomSelect2 = function () {
        var element = $(negotiationRoomConfigId);

        if ($(negotiationConfigId).val() !== '') {
            enableRoomSelect2();
            element.removeClass('disabled');
            element.prop("disabled", false);
        }
        else {
            element.addClass('disabled');
            element.prop("disabled", true);
            $(negotiationRoomConfigId).val('').trigger('change');
        }
    };

    var emptyRoomSelect2 = function () {
        $(negotiationRoomConfigId).val('').trigger('change');

        $(negotiationRoomConfigId).select2({
            language: globalVariables.userInterfaceLanguage,
            width: '100%',
            placeholder: translations.roomDropdownPlaceholder,
            data: []
        });

        $(negotiationRoomConfigId).empty();
    };

    var enableRoomSelect2 = function () {
        var projectUid = $(negotiationConfigId).val();

        if (MyRio2cCommon.isNullOrEmpty(projectUid)) {
	        emptyRoomSelect2();
        }
        else {
            var jsonParameters = new Object();
            jsonParameters.customFilter = 'HasManualTables';
            jsonParameters.negotiationConfigUid = $(negotiationConfigId).val();
            jsonParameters.buyerOrganizationUid = $(buyerOrganizationId).val();

            $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/MeetingParameters/FindAllRooms'), jsonParameters, function (data) {
                MyRio2cCommon.handleAjaxReturn({
                    data: data,
                    // Success
                    onSuccess: function () {
                        if (data.rooms.length <= 0) {
	                        emptyRoomSelect2();
                        }

                        var roomsData = new Array();

                        // Placeholder
                        var roomObject = new Object();
                        roomObject.id = '';
                        roomObject.text = labels.selectPlaceholder;
                        roomsData.push(roomObject);

                        for (var i in data.rooms) {
                            if (data.rooms.hasOwnProperty(i)) {
                                roomObject = new Object();
                                roomObject.id = data.rooms[i].NegotiationRoomConfigUid;
                                roomObject.text = data.rooms[i].RoomName;
                                roomsData.push(roomObject);
                            }
                        }

                        $(negotiationRoomConfigId).empty().select2({
                            language: globalVariables.userInterfaceLanguage,
                            width: '100%',
                            placeholder: translations.roomDropdownPlaceholder,
                            triggerChange: true,
                            allowClear: true,
                            data: roomsData
                        });
                    },
                    // Error
                    onError: function () {
	                    emptyRoomSelect2();
                    }
                });
            })
            .fail(function () {
                emptyRoomSelect2();
            })
            .always(function () {
                MyRio2cCommon.unblock();
            });
        }
    };

    var enableRoomChangeEvent = function () {
	    var element = $(negotiationRoomConfigId);

	    element.not('.change-event-enabled').on('change', function () {
		    toogleStartTimeSelect2();
	    });
	    element.addClass('change-event-enabled');
    };

    // Start time select2 -------------------------------------------------------------------------
    var toogleStartTimeSelect2 = function () {
        var element = $(startTimeId);

        if ($(negotiationRoomConfigId).val() !== '') {
            enableStartTimeSelect2();
            element.removeClass('disabled');
            element.prop("disabled", false);
        }
        else {
            element.addClass('disabled');
            element.prop("disabled", true);
            $(startTimeId).val('').trigger('change');
        }
    };

    var emptyStartTimeSelect2 = function () {
        $(startTimeId).val('').trigger('change');

        $(startTimeId).select2({
            language: globalVariables.userInterfaceLanguage,
            width: '100%',
            placeholder: translations.startTimeDropdownPlaceholder,
            data: []
        });

        $(startTimeId).empty();
    };

    var enableStartTimeSelect2 = function () {
        var negotiationRoomConfigUid = $(negotiationRoomConfigId).val();

        if (MyRio2cCommon.isNullOrEmpty(negotiationRoomConfigUid)) {
	        emptyStartTimeSelect2();
        }
        else {
            var jsonParameters = new Object();
            jsonParameters.customFilter = 'HasManualTables';
            jsonParameters.negotiationRoomConfigUid = negotiationRoomConfigUid;
            jsonParameters.buyerOrganizationUid = $(buyerOrganizationId).val();

            $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/MeetingParameters/FindAllTimes'), jsonParameters, function (data) {
                MyRio2cCommon.handleAjaxReturn({
                    data: data,
                    // Success
                    onSuccess: function () {
                        if (data.times.length <= 0) {
	                        emptyStartTimeSelect2();
                        }

                        var timesData = new Array();

                        // Placeholder
                        var timesObject = new Object();
                        timesObject.id = '';
                        timesObject.text = labels.selectPlaceholder;
                        timesData.push(timesObject);

                        for (var i in data.times) {
                            if (data.times.hasOwnProperty(i)) {
                                timesObject = new Object();
                                timesObject.id = moment(data.times[i].StartTime).tz(globalVariables.momentTimeZone).locale(globalVariables.userInterfaceLanguage).format('LTS');
                                timesObject.text = moment(data.times[i].StartTime).tz(globalVariables.momentTimeZone).locale(globalVariables.userInterfaceLanguage).format('LTS')
												   + ' - ' + moment(data.times[i].EndTime).tz(globalVariables.momentTimeZone).locale(globalVariables.userInterfaceLanguage).format('LTS');
                                timesObject.roundNumber = data.times[i].RoundNumber;
                                timesData.push(timesObject);
                            }
                        }

                        $(startTimeId).empty().select2({
                            language: globalVariables.userInterfaceLanguage,
                            width: '100%',
                            placeholder: translations.startTimeDropdownPlaceholder,
                            triggerChange: true,
                            allowClear: true,
                            data: timesData
                        });
                    },
                    // Error
                    onError: function () {
	                    emptyStartTimeSelect2();
                    }
                });
            })
                .fail(function () {
	                emptyStartTimeSelect2();
                })
                .always(function () {
                    MyRio2cCommon.unblock();
                });
        }
    };

    var enableStartTimeChangeEvent = function () {
        var element = $(startTimeId);

	    element.not('.change-event-enabled').on('change', function () {
            $(roundNumberId).val(element.select2('data')[0].roundNumber || '');
	    });
	    element.addClass('change-event-enabled');
    };

    // Enable plugins -----------------------------------------------------------------------------
    var enablePlugins = function () {
        // Select2
        MyRio2cCommon.enableOrganizationSelect2({ inputIdOrClass: buyerOrganizationId, url: '/Players/FindAllByFilters', customFilter: 'HasProjectNegotiationNotScheduled', placeholder: translations.playerDropdownPlaceholder });
        MyRio2cCommon.enableProjectSelect2({ inputIdOrClass: projectId, url: '/Projects/FindAllByFilters', customFilter: 'HasNegotiationNotScheduled', buyerOrganizationId: buyerOrganizationId, placeholder: translations.projectDropdownPlaceholder });
        enableDateSelect2();
        enableRoomSelect2();
        enableStartTimeSelect2();

        // Change events
        enableBuyerOrganizationChangeEvent();
        enableProjectChangeEvent();
        enableDateChangeEvent();
        enableRoomChangeEvent();
        enableStartTimeChangeEvent();

        enableAjaxForm();
        MyRio2cCommon.enableFormValidation({ formIdOrClass: formId, enableHiddenInputsValidation: true, enableMaxlength: true });
    };

    // Show modal ---------------------------------------------------------------------------------
    var showModal = function () {
        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/Meetings/ShowCreateModal'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    enablePlugins();
                    $(modalId).modal();
                },
                // Error
                onError: function() {
                }
            });
        })
        .fail(function () {
        })
        .always(function () {
            MyRio2cCommon.unblock();
        });
    };

    // Enable ajax form ---------------------------------------------------------------------------
    var enableAjaxForm = function () {
        MyRio2cCommon.enableAjaxForm({
            idOrClass: formId,
            onSuccess: function (data) {
                $(modalId).modal('hide');

                if (typeof (AudiovisualMeetingsScheduledWidget) !== 'undefined') {
	                AudiovisualMeetingsScheduledWidget.search();
                }

                if (typeof (AudiovisualMeetingsEditionScheduledCountWidget) !== 'undefined') {
	                AudiovisualMeetingsEditionScheduledCountWidget.init();
                }

                if (typeof (AudiovisualMeetingsEditionUnscheduledCountWidget) !== 'undefined') {
	                AudiovisualMeetingsEditionUnscheduledCountWidget.init();
                }
            },
            onError: function (data) {
                if (MyRio2cCommon.hasProperty(data, 'pages')) {
                    enablePlugins();
                }

                $(formId).find(":input.input-validation-error:first").focus();
            }
        });
    };

    return {
        showModal: function () {
            showModal();
        }
    };
}();
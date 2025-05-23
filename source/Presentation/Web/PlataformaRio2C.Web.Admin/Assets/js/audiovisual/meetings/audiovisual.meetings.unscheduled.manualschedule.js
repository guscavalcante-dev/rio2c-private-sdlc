﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 01-24-2020
//
// Last Modified By : Renan Valentim
// Last Modified On : 04-18-2024
// ***********************************************************************
// <copyright file="audiovisual.meetings.unscheduled.manualschedule.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var AudiovisualMeetingsManualSchedule = function () {

    var modalId = '#ManualScheduleAudiovisualMeetingModal';
    var formId = '#ManualScheduleAudiovisualMeetingForm';

    var buyerOrganizationUid = '#BuyerOrganizationUid';
    var projectUid = '#ProjectUid';
    var negotiationConfigUid = '#NegotiationConfigUid';
    var negotiationRoomConfigUid = '#NegotiationRoomConfigUid';
    var startTimeId = '#StartTime';
    var roundNumberId = '#RoundNumber';
    var sellerOrganizationUid = '#SellerOrganizationUid';

    var initialBuyerOrganizationUid = '#InitialBuyerOrganizationUid';
    var initialBuyerOrganizationName = '#InitialBuyerOrganizationName';
    var initialProjectUid = '#InitialProjectUid';
    var initialProjectName = '#InitialProjectName';

    var initialNegotiationConfigUid = '#InitialNegotiationConfigUid';
    var initialNegotiationRoomConfigUid = '#InitialNegotiationRoomConfigUid';
    var initialStartTime = '#InitialStartTime';

    var globalVariables = MyRio2cCommon.getGlobalVariables();

    var buyerOrganizationUidElement;
    var projectUidElement;
    var negotiationConfigUidElement;
    var negotiationRoomConfigUidElement;
    var startTimeIdElement;
    var roundNumberIdElement;
    var sellerOrganizationUidElement;

    var initialBuyerOrganizationUidElement;
    var initialBuyerOrganizationNameElement;
    var initialProjectUidElement;
    var initialProjectNameElement;

    var initialNegotiationConfigUidElement;
    var initialNegotiationRoomConfigUidElement;
    var initialStartTimeElement;

    // Init elements ------------------------------------------------------------------------------
    var initElements = function () {
        buyerOrganizationUidElement = $(buyerOrganizationUid);
        projectUidElement = $(projectUid);
        negotiationConfigUidElement = $(negotiationConfigUid);
        negotiationRoomConfigUidElement = $(negotiationRoomConfigUid);
        startTimeIdElement = $(startTimeId);
        roundNumberIdElement = $(roundNumberId);
        sellerOrganizationUidElement = $(sellerOrganizationUid);

        initialBuyerOrganizationUidElement = $(initialBuyerOrganizationUid);
        initialBuyerOrganizationNameElement = $(initialBuyerOrganizationName);
        initialProjectUidElement = $(initialProjectUid);
        initialProjectNameElement = $(initialProjectName);

        initialNegotiationConfigUidElement = $(initialNegotiationConfigUid);
        initialNegotiationRoomConfigUidElement = $(initialNegotiationRoomConfigUid);
        initialStartTimeElement = $(initialStartTime);
    };

    // Buyer organization select2 ----------------------------------------------------------------
    var enableBuyerOrganizationChangeEvent = function () {
        var element = $(buyerOrganizationUid);

        element.not('.change-event-enabled').on('change', function () {
            AudiovisualMeetingsLogisticsInfoWidget.init([this.value, sellerOrganizationUidElement.val()]);
        });
        element.addClass('change-event-enabled');

        element.change();
    };

    // Date select2 ------------------------------------------------------------------------------
    var emptyDateSelect2 = function () {
        negotiationConfigUidElement.val('').trigger('change');

        negotiationConfigUidElement.select2({
            language: globalVariables.userInterfaceLanguage,
            width: '100%',
            placeholder: translations.dateDropdownPlaceholder,
            data: []
        });

        negotiationConfigUidElement.empty();
    };

    var enableDateSelect2 = function (isParentChanged) {
        if (!MyRio2cCommon.isNullOrEmpty(isParentChanged) && isParentChanged === true) {
            if (!MyRio2cCommon.isNullOrEmpty(negotiationConfigUidElement.val())) {
                negotiationConfigUidElement.val('').trigger('change');
            }
            else {
                negotiationConfigUidElement.val('');
            }
        }

        var projectUid = projectUidElement.val();

        if (MyRio2cCommon.isNullOrEmpty(projectUid)) {
            emptyDateSelect2();
        }
        else {
            var jsonParameters = new Object();
            jsonParameters.customFilter = 'HasManualTables';
            jsonParameters.buyerOrganizationUid = buyerOrganizationUidElement.val();
            jsonParameters.projectUid = projectUidElement.val();

            $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/MeetingParameters/FindAllDatesAvailables'), jsonParameters, function (data) {
                MyRio2cCommon.handleAjaxReturn({
                    data: data,
                    // Success
                    onSuccess: function () {
                        if (data.negotiationConfigs.length <= 0) {
                            emptyDateSelect2();
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

                        negotiationConfigUidElement.empty().select2({
                            language: globalVariables.userInterfaceLanguage,
                            width: '100%',
                            placeholder: translations.dateDropdownPlaceholder,
                            triggerChange: true,
                            allowClear: true,
                            data: negotiationConfigsData
                        });

                        var initialNegotiationConfigUidValue = initialNegotiationConfigUidElement.val();
                        if (!MyRio2cCommon.isNullOrEmpty(initialNegotiationConfigUidValue)) {
                            negotiationConfigUidElement.val(initialNegotiationConfigUidValue).trigger('change');
                            initialNegotiationConfigUidElement.val('');
                        }
                    },
                    // Error
                    onError: function () {
                        emptyDateSelect2();
                    }
                });
            })
                .fail(function () {
                    emptyDateSelect2();
                })
                .always(function () {
                    MyRio2cCommon.unblock();
                });
        }
    };

    var enableDateChangeEvent = function () {
        var element = negotiationConfigUidElement;

        element.not('.change-event-enabled').on('change', function () {
            toogleRoomSelect2();
        });
        element.addClass('change-event-enabled');
    };

    // Room select2 ------------------------------------------------------------------------------
    var toogleRoomSelect2 = function () {
        var element = negotiationRoomConfigUidElement;

        if (!MyRio2cCommon.isNullOrEmpty(negotiationConfigUidElement.val())) {
            enableRoomSelect2(true);
            element.removeClass('disabled');
            element.prop("disabled", false);
        }
        else {
            element.addClass('disabled');
            element.prop("disabled", true);
            negotiationRoomConfigUidElement.val('').trigger('change');
        }
    };

    var emptyRoomSelect2 = function () {
        negotiationRoomConfigUidElement.val('').trigger('change');

        negotiationRoomConfigUidElement.select2({
            language: globalVariables.userInterfaceLanguage,
            width: '100%',
            placeholder: translations.roomDropdownPlaceholder,
            data: []
        });

        negotiationRoomConfigUidElement.empty();
    };

    var enableRoomSelect2 = function (isParentChanged) {
        if (!MyRio2cCommon.isNullOrEmpty(isParentChanged) && isParentChanged === true) {
            if (!MyRio2cCommon.isNullOrEmpty(negotiationRoomConfigUidElement.val())) {
                negotiationRoomConfigUidElement.val('').trigger('change');
            }
            else {
                negotiationRoomConfigUidElement.val('');
            }
        }

        var projectUid = negotiationConfigUidElement.val();

        if (MyRio2cCommon.isNullOrEmpty(projectUid)) {
            emptyRoomSelect2();
        }
        else {
            var jsonParameters = new Object();
            jsonParameters.customFilter = 'HasManualTables';
            jsonParameters.negotiationConfigUid = negotiationConfigUidElement.val();
            jsonParameters.buyerOrganizationUid = buyerOrganizationUidElement.val();

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

                        negotiationRoomConfigUidElement.empty().select2({
                            language: globalVariables.userInterfaceLanguage,
                            width: '100%',
                            placeholder: translations.roomDropdownPlaceholder,
                            triggerChange: true,
                            allowClear: true,
                            data: roomsData
                        });

                        var initialNegotiationRoomConfigUidValue = initialNegotiationRoomConfigUidElement.val();
                        if (!MyRio2cCommon.isNullOrEmpty(initialNegotiationRoomConfigUidValue)) {
                            negotiationRoomConfigUidElement.val(initialNegotiationRoomConfigUidValue).trigger('change');
                            initialNegotiationRoomConfigUidElement.val('');
                        }
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
        var element = negotiationRoomConfigUidElement;

        element.not('.change-event-enabled').on('change', function () {
            toogleStartTimeSelect2();
        });
        element.addClass('change-event-enabled');
    };

    // Start time select2 -------------------------------------------------------------------------
    var toogleStartTimeSelect2 = function () {
        var element = startTimeIdElement;

        if (!MyRio2cCommon.isNullOrEmpty(negotiationRoomConfigUidElement.val())) {
            enableStartTimeSelect2(true);
            element.removeClass('disabled');
            element.prop("disabled", false);
        }
        else {
            element.addClass('disabled');
            element.prop("disabled", true);
            startTimeIdElement.val('').trigger('change');
        }
    };

    var emptyStartTimeSelect2 = function () {
        startTimeIdElement.val('').trigger('change');

        startTimeIdElement.select2({
            language: globalVariables.userInterfaceLanguage,
            width: '100%',
            placeholder: translations.startTimeDropdownPlaceholder,
            data: []
        });

        startTimeIdElement.empty();
    };

    var enableStartTimeSelect2 = function (isParentChanged) {
        if (!MyRio2cCommon.isNullOrEmpty(isParentChanged) && isParentChanged === true) {
            if (!MyRio2cCommon.isNullOrEmpty(startTimeIdElement.val())) {
                startTimeIdElement.val('').trigger('change');
            }
            else {
                startTimeIdElement.val('');
            }
        }

        var negotiationRoomConfigUid = negotiationRoomConfigUidElement.val();

        if (MyRio2cCommon.isNullOrEmpty(negotiationRoomConfigUid)) {
            emptyStartTimeSelect2();
        }
        else {
            var jsonParameters = new Object();
            jsonParameters.customFilter = 'HasManualTables';
            jsonParameters.negotiationRoomConfigUid = negotiationRoomConfigUid;
            jsonParameters.buyerOrganizationUid = buyerOrganizationUidElement.val();
            jsonParameters.projectUid = projectUidElement.val();

            $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/MeetingParameters/FindAllTimesAvailables'), jsonParameters, function (data) {
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

                        startTimeIdElement.empty().select2({
                            language: globalVariables.userInterfaceLanguage,
                            width: '100%',
                            placeholder: translations.startTimeDropdownPlaceholder,
                            triggerChange: true,
                            allowClear: true,
                            data: timesData
                        });


                        var initialStartTimeValue = initialStartTimeElement.val();
                        if (!MyRio2cCommon.isNullOrEmpty(initialStartTimeValue)) {
                            startTimeIdElement.val(initialStartTimeValue).trigger('change');
                            initialStartTimeElement.val('');
                        }
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
        var element = startTimeIdElement;

        element.not('.change-event-enabled').on('change', function () {

            var val = '';
            if (!MyRio2cCommon.isNullOrEmpty(element.select2('data')[0]))
            {
                val = element.select2('data')[0].roundNumber;
            }

            roundNumberIdElement.val(val);
        });
        element.addClass('change-event-enabled');
    };

    // Enable plugins -----------------------------------------------------------------------------
    var enablePlugins = function () {
        initElements();

        KTApp.initTooltips();
        MyRio2cCommon.initScroll();

        // Select2
        MyRio2cCommon.enableOrganizationSelect2({
            inputIdOrClass: buyerOrganizationUid,
            url: '/Audiovisual/Players/FindAllByFilters',
            customFilter: 'HasProjectNegotiationNotScheduled',
            placeholder: translations.playerDropdownPlaceholder,
            selectedOption: {
                id: $('#InitialBuyerOrganizationUid').val(),
                text: $('#InitialBuyerOrganizationName').val()
            }
        });
        MyRio2cCommon.enableProjectSelect2({
            inputIdOrClass: projectUid,
            url: '/Audiovisual/Projects/FindAllByFilters',
            customFilter: 'HasNegotiationNotScheduled',
            buyerOrganizationUid: buyerOrganizationUid,
            placeholder: translations.projectDropdownPlaceholder,
            selectedOption: {
                id: $('#InitialProjectUid').val(),
                text: $('#InitialProjectName').val()
            }
        });
        enableDateSelect2();
        enableRoomSelect2();
        enableStartTimeSelect2();

        // Change events
        enableBuyerOrganizationChangeEvent();
        enableDateChangeEvent();
        enableRoomChangeEvent();
        enableStartTimeChangeEvent();

        enableAjaxForm();
        MyRio2cCommon.enableFormValidation({ formIdOrClass: formId, enableHiddenInputsValidation: true, enableMaxlength: true });
    };

    // Show modal ---------------------------------------------------------------------------------
    var showModal = function (projectBuyerEvaluationUid) {
        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();

        if (!MyRio2cCommon.isNullOrEmpty(projectBuyerEvaluationUid))
            jsonParameters.projectBuyerEvaluationUid = projectBuyerEvaluationUid;

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/Meetings/ShowManualScheduleModal'), jsonParameters, function (data) {
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

                if (typeof (AudiovisualMeetingsUnscheduledWidget) !== 'undefined') {
                    AudiovisualMeetingsUnscheduledWidget.init();
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
        showModal: function (projectBuyerEvaluationUid) {
            showModal(projectBuyerEvaluationUid);
        }
    };
}();
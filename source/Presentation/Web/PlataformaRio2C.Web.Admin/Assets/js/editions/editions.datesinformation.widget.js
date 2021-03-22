// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 19-03-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 19-03-2021
// ***********************************************************************
// <copyright file="editions.datesinformation.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var EditionsDatesInformationWidget = function () {

    var widgetElementId = '#DatesInformationWidget';
    var widgetElement = $(widgetElementId);

    var updateModalId = '#UpdateDatesInformationModal';
    var updateFormId = '#UpdateDatesInformationForm';

    var enableDatePickerDateRangeChangeEvent = function () {
        $("#StartDate").datepicker({
            todayBtn: 1,
            autoclose: true
        }).on('changeDate', function (selected) {
            setStartDateMinDate(selected.date.valueOf());
        });

        $("#EndDate").datepicker()
            .on('changeDate', function (selected) {
                setEndDateMaxDate(selected.date.valueOf());
            });

        var selectedStarDate = $('#StartDate').datepicker("getDate");
        if (!MyRio2cCommon.isNullOrEmpty(selectedStarDate)) {
            setStartDateMinDate(selectedStarDate);
        }

        var selectedEndDate = $('#EndDate').datepicker("getDate");
        if (!MyRio2cCommon.isNullOrEmpty(selectedEndDate)) {
            setEndDateMaxDate(selectedEndDate);
        }
    }

    var setStartDateMinDate = function (el, selectedDate) {
        var minDate = new Date(selectedDate);
        var element = el;
        var elementId = element.attr('id');
        element.datepicker('setStartDate', minDate);
        checkIfDateIsInRange(elementId);
    }

    var setGeneralDates = function (selectedDate) {
        $(".enable-datepicker").each(function (el) {
            if (el != 'StartDate' && el != 'EndDate')
                setStartDateMinDate(el, selectedDate);
        });
    }

    var setEndDateMaxDate = function (selectedDate) {
        var maxDate = new Date(selectedDate);

        $(".enable-datepicker").each(function () {
            var element = $(this);
            var elementId = element.attr('id');

            if (elementId != 'StartDate' && elementId != 'EndDate') {
                element.datepicker('setEndDate', maxDate);
                checkIfDateIsInRange(elementId);
            }
        });
    }

    var checkIfDateIsInRange = function (datePickerElementId) {

        if (MyRio2cCommon.isNullOrEmpty(datePickerElementId)) {
            return;
        }

        var minDate = $('#StartDate').datepicker("getDate");
        var maxDate = $('#EndDate').datepicker("getDate");

        if (!MyRio2cCommon.isNullOrEmpty(minDate) && !MyRio2cCommon.isNullOrEmpty(maxDate) ) {

            var datePickerElement = $('#' + datePickerElementId);
            var datePickerSelectedDate = datePickerElement.datepicker("getDate");

            if (datePickerSelectedDate > maxDate || datePickerSelectedDate < minDate) {
                //datePickerElement.datepicker('setDate', null);
                datePickerElement.valid();

                //const diffTime = Math.abs(minDate - new Date());
                //const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));
                //datePickerElement.datepicker({ defaultDate: -diffDays });
            }
        }
    }

    // Show ---------------------------------------------------------------------------------------
    var enableShowPlugins = function () {
        KTApp.initTooltips();
    };

    var show = function () {
        if (widgetElement.length <= 0) {
            return;
        }

        var jsonParameters = new Object();
        jsonParameters.editionUid = $('#AggregateId').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Editions/ShowDatesInformationWidget'), jsonParameters, function (data) {
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

    // Update -------------------------------------------------------------------------------------
    var enableAjaxForm = function () {
        MyRio2cCommon.enableAjaxForm({
            idOrClass: updateFormId,
            onSuccess: function (data) {
                $(updateModalId).modal('hide');

                //if (typeof (EditionsMainInformationWidget) !== 'undefined') {
                //    EditionsMainInformationWidget.init();
                //}

                //if (typeof (EditionsEventsWidget) !== 'undefined') {
                //    EditionsEventsWidget.init();
                //}

                if (typeof (EditionsDatesInformationWidget) !== 'undefined') {
                    EditionsDatesInformationWidget.init();
                }
            },
            onError: function (data) {
                if (MyRio2cCommon.hasProperty(data, 'pages')) {
                    enableUpdatePlugins();
                }

                $(updateFormId).find(":input.input-validation-error:first").focus();
            }
        });
    };

    var enableUpdatePlugins = function () {
        MyRio2cCommon.enableDatePicker({ inputIdOrClass: updateFormId + ' .enable-datepicker' });
        enableAjaxForm();
        MyRio2cCommon.enableFormValidation({ formIdOrClass: updateFormId, enableHiddenInputsValidation: true, enableMaxlength: true });
        //enableDatePickerDateRangeChangeEvent();
    };

    var showUpdateModal = function () {
        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();
        jsonParameters.editionUid = $('#AggregateId').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Editions/ShowUpdateDatesInformationModal'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    $(updateModalId).modal();
                    enableUpdatePlugins();
                },
                // Error
                onError: function () {
                }
            });
        })
            .fail(function () {
            })
            .always(function () {
                MyRio2cCommon.unblock();
            });
    };

    return {
        init: function () {
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            show();
        },
        showUpdateModal: function () {
            showUpdateModal();
        }
    };
}();
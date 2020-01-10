// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 01-05-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-05-2020
// ***********************************************************************
// <copyright file="conferences.editionevents.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var ConferencesEditionEvents = function () {

    var editionEventName = 'EditionEventUid';
    var editionEventId = '#' + editionEventName;
    var editionEventElement;

    var dateName = 'Date';

    var dataPeriodContainerId = '#data-period-container';
    var dataPeriodContainerElement;

    var dataPeriodMessage;

    // Initi elements -----------------------------------------------------------------------------
    var initElements = function() {
        editionEventElement = $(editionEventId);
        dataPeriodContainerElement = $(dataPeriodContainerId);
        dataPeriodMessage = translations.dateBetweenDates;
    };

    // Update message -----------------------------------------------------------------------------
    var updateMessage = function () {
        var selectedOptionElement = editionEventElement.find(":selected");
        var startDate = selectedOptionElement.data("start-date");
        var endDate = selectedOptionElement.data("end-date");

        if (!MyRio2cCommon.isNullOrEmpty(startDate) && !MyRio2cCommon.isNullOrEmpty(endDate)) {
            dataPeriodContainerElement.html(dataPeriodMessage.replace('{1}', startDate).replace('{0}', endDate));
            dataPeriodContainerElement.removeClass('d-none');
        }
        else {
            dataPeriodContainerElement.html('');
            dataPeriodContainerElement.addClass('d-none');
        }

        var eventValMsgElement = $('[data-valmsg-for="' + editionEventName + '"]');
        eventValMsgElement.html('');
        eventValMsgElement.addClass('field-validation-valid');
        eventValMsgElement.removeClass('field-validation-error');

        var dateValMsgElement = $('[data-valmsg-for="' + dateName + '"]');
        if (dateValMsgElement.html().indexOf("O campo Data deve ser entre") >= 0 || dateValMsgElement.html().indexOf("The field Date must be between") >= 0) {
            dateValMsgElement.html('');
            dateValMsgElement.addClass('field-validation-valid');
            dateValMsgElement.removeClass('field-validation-error');
        }
    };

    // Enable change event ------------------------------------------------------------------------
    var enableChangeEvent = function () {
        if (typeof (dataPeriodContainerElement) === 'undefined' || MyRio2cCommon.isNullOrEmpty(dataPeriodMessage)) {
            return;
        }

        editionEventElement.not('.change-event-enabled').on('change', function () {
            updateMessage();
        });
        editionEventElement.addClass('change-event-enabled');
    };

    return {
        init: function () {
            initElements();
            enableChangeEvent();
        }
    };
}();
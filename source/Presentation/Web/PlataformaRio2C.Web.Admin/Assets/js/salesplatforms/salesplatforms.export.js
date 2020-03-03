// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 02-26-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-26-2020
// ***********************************************************************
// <copyright file="salesplatforms.export.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var SalesPlatformsExport = function () {

    var modalId = '#ExportEventbriteCsvModal';
    var eventbriteCsvExport;

    // Export csv ---------------------------------------------------------------------------------
    var enableExportEventbriteCsvPlugins = function () {
        MyRio2cCommon.enableSelect2({ inputIdOrClass: modalId + ' .enable-select2' });
    };

    var showExportEventbriteCsvModal = function (eventbriteCsvExportParam) {
	    eventbriteCsvExport = eventbriteCsvExportParam;

	    MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/SalesPlatforms/ShowExportEventbriteCsvModal'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    enableExportEventbriteCsvPlugins();
                    $(modalId).modal();
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

    var exportEventbriteCsv = function () {
        MyRio2cCommon.block({ isModal: true });

        var ticketTypeNameId = 'AttendeeSalesPlatformTicketTypeName';
        var ticketTypeName = $('#' + ticketTypeNameId).val();

        if (MyRio2cCommon.isNullOrEmpty(ticketTypeName)) {
            $('[data-valmsg-for="' + ticketTypeNameId + '"]').html('<span for="' + ticketTypeNameId + '" generated="true" class="">' + theFieldIsRequired.replace('{0}', ticketType) + '</span>');
            $('[data-valmsg-for="' + ticketTypeNameId + '"]').removeClass('field-validation-valid');
            $('[data-valmsg-for="' + ticketTypeNameId + '"]').addClass('field-validation-error');

            MyRio2cCommon.unblock({ isModal: true });
            return;
        }
        else {
            $('[data-valmsg-for="' + ticketTypeNameId + '"]').html('');
            $('[data-valmsg-for="' + ticketTypeNameId + '"]').addClass('field-validation-valid');
            $('[data-valmsg-for="' + ticketTypeNameId + '"]').removeClass('field-validation-error');
        }

        eventbriteCsvExport.ticketClassName = ticketTypeName;

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/SalesPlatforms/ExportEventbriteCsv'), eventbriteCsvExport, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    var json = jQuery.parseJSON(data.data);
                    var csv = MyRio2cCommon.convertJsonToCsv(json);
                    var bom = "\uFEFF";
                    var csvContent = bom + csv;
                    var csvData = new Blob([csvContent], { type: 'text/csv;charset=utf-8;' });
                    var csvUrl = window.URL.createObjectURL(csvData);
                    var tempLink = document.createElement('a');
                    tempLink.href = csvUrl;
                    tempLink.setAttribute('download', 'eventbrite.csv');
                    tempLink.click();
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
        },
        showExportEventbriteCsvModal: function (eventbriteCsvExport) {
            showExportEventbriteCsvModal(eventbriteCsvExport);
        },
        exportEventbriteCsv: function () {
            exportEventbriteCsv();
        }
    };
}();
// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 03-30-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-30-2020
// ***********************************************************************
// <copyright file="audiovisual.meetings.report.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var AudiovisualMeetingsReportWidget = function () {

    var widgetElementId = '#AudiovisualMeetingsReportWidget';
    var widgetElement = $(widgetElementId);

    //var updateModalId = '#UpdateMainInformationModal';
    //var updateFormId = '#UpdateMainInformationForm';

    // Search form  -------------------------------------------------------------------------------
    var enableSearchEvents = function () {
	    $('#SearchBuyerOrganizationUid').not('.change-event-enabled').on('change', function () {
		    AudiovisualMeetingsReportWidget.search();
	    });
	    $('#SearchBuyerOrganizationUid').addClass('change-event-enabled');

	    $('#SearchSellerOrganizationUid').not('.change-event-enabled').on('change', function () {
		    AudiovisualMeetingsReportWidget.search();
	    });
        $('#SearchSellerOrganizationUid').addClass('change-event-enabled');

	    $('#SearchProjectKeywords').not('.search-event-enabled').on('search', function () {
		    AudiovisualMeetingsReportWidget.search();
	    });
        $('#SearchProjectKeywords').addClass('search-event-enabled');

        $('#SearchDate').not('.change-event-enabled').on('change', function () {
	        AudiovisualMeetingsReportWidget.search();
        });
        $('#SearchDate').addClass('change-event-enabled');

        $('#SearchRoomUid').not('.change-event-enabled').on('change', function () {
	        AudiovisualMeetingsReportWidget.search();
        });
        $('#SearchRoomUid').addClass('change-event-enabled');
    };

    var enableSearchForm = function () {
        enableSearchEvents();

        MyRio2cCommon.enableOrganizationSelect2({ inputIdOrClass: '#SearchBuyerOrganizationUid', url: '/Players/FindAllByFilters', customFilter: 'HasProjectNegotiationScheduled', placeholder: translations.playerDropdownPlaceholder });
        MyRio2cCommon.enableOrganizationSelect2({ inputIdOrClass: '#SearchSellerOrganizationUid', url: '/Audiovisual/Producers/FindAllByFilters', customFilter: 'HasProjectNegotiationScheduled', placeholder: translations.producerDropdownPlaceholder });
	    MyRio2cCommon.enableDatePicker({ inputIdOrClass: '.enable-datepicker' });
        MyRio2cCommon.enableSelect2({ inputIdOrClass: '#SearchRoomUid', allowClear: true, placeholder: translations.roomDropdownPlaceholder });
    }

    // Show ---------------------------------------------------------------------------------------
    var enableShowPlugins = function () {
        KTApp.initTooltips();
        MyRio2cCommon.initScroll();
    };

    var show = function () {
        if (widgetElement.length <= 0) {
            return;
        }

        var jsonParameters = new Object();
        jsonParameters.buyerOrganizationUid = $('#SearchBuyerOrganizationUid').val();
        jsonParameters.sellerOrganizationUid = $('#SearchSellerOrganizationUid').val();
        jsonParameters.projectKeywords = $('#SearchProjectKeywords').val();
        jsonParameters.roomUid = $('#SearchRoomUid').val();

        var date = $('#SearchDate').val();
        if (!MyRio2cCommon.isNullOrEmpty(date)) {
            jsonParameters.date = moment(date, "L", MyRio2cCommon.getGlobalVariable('userInterfaceLanguageUppercase')).format('YYYY-MM-DD');
        }

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/Meetings/ShowReportDataWidget'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    enableShowPlugins();
                },
                // Error
                onError: function() {
                }
            });
        })
        .fail(function () {
            //showAlert();
            //MyRio2cCommon.unblock(widgetElementId);
        })
        .always(function() {
            MyRio2cCommon.unblock({ idOrClass: widgetElementId });
        });
    };

    var exportExcel = function () {
	    var jsonParameters = new Object();
	    jsonParameters.buyerOrganizationUid = $('#SearchBuyerOrganizationUid').val();
	    jsonParameters.sellerOrganizationUid = $('#SearchSellerOrganizationUid').val();
	    jsonParameters.projectKeywords = $('#SearchProjectKeywords').val();
	    jsonParameters.roomUid = $('#SearchRoomUid').val();

	    var date = $('#SearchDate').val();
	    if (!MyRio2cCommon.isNullOrEmpty(date)) {
		    jsonParameters.date = moment(date, "L", MyRio2cCommon.getGlobalVariable('userInterfaceLanguageUppercase')).format('YYYY-MM-DD');
	    }

	    //$('#btnExportToExcel').addClass('disabled');
        location.href = '/Audiovisual/Meetings/ExportReportExcel?' + jQuery.param(jsonParameters);
    }

    return {
        init: function () {
	        enableSearchForm();
	        AudiovisualMeetingsReportWidget.search();
        },
        search: function() {
	        MyRio2cCommon.block({ idOrClass: widgetElementId });
	        show();
        },
        exportExcel: function() {
	        exportExcel();
        }
    };
}();
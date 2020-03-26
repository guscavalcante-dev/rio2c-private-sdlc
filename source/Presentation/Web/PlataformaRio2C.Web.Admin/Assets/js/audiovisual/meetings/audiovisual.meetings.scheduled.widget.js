// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 03-07-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-25-2020
// ***********************************************************************
// <copyright file="audiovisual.meetings.scheduled.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var AudiovisualMeetingsScheduledWidget = function () {

    var widgetElementId = '#AudiovisualMeetingsScheduledWidget';
    var widgetElement = $(widgetElementId);

    //var updateModalId = '#UpdateMainInformationModal';
    //var updateFormId = '#UpdateMainInformationForm';

    // Search form  -------------------------------------------------------------------------------
    var enableSearchEvents = function () {
	    $('#SearchBuyerOrganizationUid').not('.change-event-enabled').on('change', function () {
		    AudiovisualMeetingsScheduledWidget.search();
	    });
	    $('#SearchBuyerOrganizationUid').addClass('change-event-enabled');

	    $('#SearchSellerOrganizationUid').not('.change-event-enabled').on('change', function () {
		    AudiovisualMeetingsScheduledWidget.search();
	    });
        $('#SearchSellerOrganizationUid').addClass('change-event-enabled');

	    $('#SearchProjectKeywords').not('.search-event-enabled').on('search', function () {
	        AudiovisualMeetingsScheduledWidget.search();
	    });
        $('#SearchProjectKeywords').addClass('search-event-enabled');

        $('#SearchDate').not('.change-event-enabled').on('change', function () {
	        AudiovisualMeetingsScheduledWidget.search();
        });
        $('#SearchDate').addClass('change-event-enabled');

        $('#SearchRoomUid').not('.change-event-enabled').on('change', function () {
	        AudiovisualMeetingsScheduledWidget.search();
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

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/Meetings/ShowScheduledDataWidget'), jsonParameters, function (data) {
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

    return {
        init: function () {
	        enableSearchForm();
	        AudiovisualMeetingsScheduledWidget.search();
        },
        search: function() {
	        MyRio2cCommon.block({ idOrClass: widgetElementId });
	        show();
        }
    };
}();
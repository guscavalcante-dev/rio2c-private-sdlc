// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 09-27-2021
//
// Last Modified By : Rafael Ribeiro
// Last Modified On : 02-31-2025
// ***********************************************************************
// <copyright file="music.meetings.scheduled.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var MusicMeetingsScheduledWidget = function () {

    var widgetElementId = '#MusicMeetingsScheduledWidget';
    var widgetElement = $(widgetElementId);

    // Search form  -------------------------------------------------------------------------------
    var enableSearchEvents = function () {
	    $('#SearchBuyerOrganizationUid').not('.change-event-enabled').on('change', function () {
		    MusicMeetingsScheduledWidget.search();
	    });
	    $('#SearchBuyerOrganizationUid').addClass('change-event-enabled');

	    $('#SearchSellerOrganizationUid').not('.change-event-enabled').on('change', function () {
		   MusicMeetingsScheduledWidget.search();
	    });
        $('#SearchSellerOrganizationUid').addClass('change-event-enabled');

	    $('#SearchProjectKeywords').not('.search-event-enabled').on('search', function () {
	        MusicMeetingsScheduledWidget.search();
	    });
        $('#SearchProjectKeywords').addClass('search-event-enabled');

        $('#SearchDate').not('.change-event-enabled').on('change', function () {
	        MusicMeetingsScheduledWidget.search();
        });
        $('#SearchDate').addClass('change-event-enabled');
    };

    var enableSearchForm = function () {
        enableSearchEvents();

        if (isPlayerExecutive) {
            MyRio2cCommon.enableOrganizationSelect2({ inputIdOrClass: '#SearchSellerOrganizationUid', url: '/Companies/FindAllProducersByFilters', customFilter: 'HasProjectNegotiationScheduled', placeholder: translations.producerDropdownPlaceholder });
        }
        else {
            MyRio2cCommon.enableOrganizationSelect2({ inputIdOrClass: '#SearchBuyerOrganizationUid', url: '/Companies/FindAllPlayersByFilters', customFilter: 'HasProjectNegotiationScheduled', placeholder: translations.playerDropdownPlaceholder });
        }

	    MyRio2cCommon.enableDatePicker({ inputIdOrClass: '.enable-datepicker' });
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
        jsonParameters.collaboratorTypeUid = $('#CollaboratorTypeUid').val();

        var date = $('#SearchDate').val();
        if (!MyRio2cCommon.isNullOrEmpty(date)) {
            jsonParameters.date = moment(date, "L", MyRio2cCommon.getGlobalVariable('userInterfaceLanguageUppercase')).format('YYYY-MM-DD');
        }

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Music/Meetings/ShowScheduledDataWidget'), jsonParameters, function (data) {
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
        })
        .always(function() {
            MyRio2cCommon.unblock({ idOrClass: widgetElementId });
        });
    };

    return {
        init: function () {
	        enableSearchForm();
	        MusicMeetingsScheduledWidget.search();
        },
        search: function() {
	        MyRio2cCommon.block({ idOrClass: widgetElementId });
	        show();
        }
    };
}();
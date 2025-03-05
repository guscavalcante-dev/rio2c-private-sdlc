// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Ribeiro
// Created          : 21-02-2025
//
// Last Modified By : Rafael Ribeiro
// Last Modified On : 21-02-2025
// ***********************************************************************
// <copyright file="Music.meetings.scheduled.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var MusicMeetingsScheduledWidget = function () {

    var widgetElementId = '#MusicMeetingsScheduledWidget';
    var widgetElement = $(widgetElementId);

    // Search form  -------------------------------------------------------------------------------
    var enableSearchEvents = function () {
        $('#BuyerOrganizationUid').not('.change-event-enabled').on('change', function () {
            MusicMeetingsScheduledWidget.search();
        });
        $('#BuyerOrganizationUid').addClass('change-event-enabled');

        $('#SellerOrganizationUid').not('.change-event-enabled').on('change', function () {
            MusicMeetingsScheduledWidget.search();
        });
        $('#SellerOrganizationUid').addClass('change-event-enabled');

        $('#ProjectKeywords').not('.search-event-enabled').on('search', function () {
            MusicMeetingsScheduledWidget.search();
        });
        $('#ProjectKeywords').addClass('search-event-enabled');

        $('#Date').not('.change-event-enabled').on('change', function () {
            MusicMeetingsScheduledWidget.search();
        });
        $('#Date').addClass('change-event-enabled');

        $('#RoomUid').not('.change-event-enabled').on('change', function () {
            MusicMeetingsScheduledWidget.search();
        });
        $('#RoomUid').addClass('change-event-enabled');

        $('#ShowParticipants').not('.change-event-enabled').on('change', function () {
            MusicMeetingsScheduledWidget.search();
        });
        $('#ShowParticipants').addClass('change-event-enabled');
    };

    var enableSearchForm = function () {
        enableSearchEvents();

        MyRio2cCommon.enableOrganizationSelect2({ inputIdOrClass: '#BuyerOrganizationUid', url: '/Music/Players/FindAllByFilters', customFilter: 'HasProjectNegotiationScheduled', placeholder: translations.playerDropdownPlaceholder });
        MyRio2cCommon.enableOrganizationSelect2({ inputIdOrClass: '#SellerOrganizationUid', url: '/Music/StageName/FindAllByFilters', customFilter: 'HasProjectNegotiationScheduled', placeholder: translations.stagenameDropdownPlaceholder });
        MyRio2cCommon.enableDatePicker({ inputIdOrClass: '.enable-datepicker' });
        MyRio2cCommon.enableSelect2({ inputIdOrClass: '#RoomUid', allowClear: true, placeholder: translations.roomDropdownPlaceholder });
    }

    var getJsonParameters = function () {

        var jsonParameters = new Object();
        jsonParameters.buyerOrganizationUid = $('#BuyerOrganizationUid').val();
        jsonParameters.sellerOrganizationUid = $('#SellerOrganizationUid').val();
        jsonParameters.projectKeywords = $('#ProjectKeywords').val();
        jsonParameters.roomUid = $('#RoomUid').val();
        jsonParameters.showParticipants = $('#ShowParticipants').prop('checked');

        var date = $('#Date').val();
        if (!MyRio2cCommon.isNullOrEmpty(date)) {
            jsonParameters.date = moment(date, "L", MyRio2cCommon.getGlobalVariable('userInterfaceLanguageUppercase')).format('YYYY-MM-DD');
        }

        return jsonParameters;
    }

    // Show ---------------------------------------------------------------------------------------
    var enableShowPlugins = function () {
        $('[data-toggle="tooltip"]').tooltip({
            html: true,
            placement: 'bottom'
        });
        MyRio2cCommon.initScroll();
    };

    var show = function () {
        if (widgetElement.length <= 0) {
            return;
        }

        var jsonParameters = getJsonParameters();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Music/Meetings/ShowScheduledDataWidget'), jsonParameters, function (data) {
            console.log(data)
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
                //showAlert();
                //MyRio2cCommon.unblock(widgetElementId);
            })
            .always(function () {
                MyRio2cCommon.unblock({ idOrClass: widgetElementId });
            });
    };

    // Export to PDF ------------------------------------------------------------------------------
    var exportToPdf = function () {
        var jsonParameters = getJsonParameters();
        location.href = '/Music/Meetings/ExportReportToPdf?' + jQuery.param(jsonParameters);
    }

    return {
        init: function () {
            enableSearchForm();
            MusicMeetingsScheduledWidget.search();
        },
        search: function () {
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            show();
        },
        exportToPdf: function () {
            exportToPdf();
        }
    };
}();
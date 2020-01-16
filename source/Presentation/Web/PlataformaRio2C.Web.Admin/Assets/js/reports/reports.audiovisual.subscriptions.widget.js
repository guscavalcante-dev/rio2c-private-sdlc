// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : William Sergio Almado Junior
// Created          : 01-13-2019
//
// Last Modified By : William Sergio Almado Junior
// Last Modified On : 01-13-2019
// ***********************************************************************
// <copyright file="reports.audiovisual.subscription.widget.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var ReportsAudiovisualSubscriptionWidget = function () {

    var widgetElementId = '#ReportAudiovisualSubscriptionWidget';
    var widgetElement = $(widgetElementId);

    // Show ---------------------------------------------------------------------------------------
    var enableShowPlugins = function () {
        KTApp.initTooltips();
        MyRio2cCommon.initScroll();
        MyRio2cCommon.enableSelect2({ inputIdOrClass: '#InterestUid', allowClear: true, placeholder: translations.selectPlaceholder.replace('{0}', translations.genre) + '...' });
        MyRio2cCommon.enableSelect2({ inputIdOrClass: '#TargetAudienceUid', allowClear: true, placeholder: translations.selectPlaceholder.replace('{0}', translations.targetAudience) + '...' });
        MyRio2cCommon.enableDatePicker({ inputIdOrClass: '#StartDate', allowClear: true, placeholder: translations.selectPlaceholder.replace('{0}', translations.startDate) + '...' });
        MyRio2cCommon.enableDatePicker({ inputIdOrClass: '#EndDate', allowClear: true, placeholder: translations.selectPlaceholder.replace('{0}', translations.endDate) + '...' });
        enablePageSizeChangeEvent();
    };

    var show = function () {
        if (widgetElement.length <= 0) {
            return;
        }

        var jsonParameters = new Object();
        jsonParameters.Search = $('#SearchKeywords').val();
        jsonParameters.InterestUid = $('#InterestUid').val();
        jsonParameters.Page = $('#Page').val();
        jsonParameters.PageSize = $('#PageSize').val();
        jsonParameters.IsPitching = $('#IsPitching').prop("checked");
        jsonParameters.TargetAudienceUid = $('#TargetAudienceUid').val();
        jsonParameters.StartDate = $('#StartDate').val();
        jsonParameters.EndDate = $('#EndDate').val();


        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Reports/ShowAudiovisualSubscriptionsWidget'), jsonParameters, function (data) {
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

    // Search -------------------------------------------------------------------------------------
    var search = function () {
        $('#Page').val('1');
        ReportsAudiovisualSubscriptionWidget.init();
    };

    var enableSearchEvents = function () {
        $('#SearchKeywords').not('.search-event-enabled').on('search', function () {
            search();
        });
        $('#SearchKeywords').addClass('search-event-enabled');

        $('#InterestUid').not('.change-event-enabled').on('change', function () {
            search();
        });
        $('#InterestUid').addClass('change-event-enabled');

        $('#IsPitching').not('.change-event-enabled').on('change', function () {
            search();
        });
        $('#IsPitching').addClass('change-event-enabled');

        $('#TargetAudienceUid').not('.change-event-enabled').on('change', function () {
            search();
        });
        $('#TargetAudienceUid').addClass('change-event-enabled');

        $('#StartDate').not('.search-event-enabled').on('search', function () {
            search();
        });
        $('#StartDate').addClass('search-event-enabled');

        $('#EndDate').not('.search-event-enabled').on('search', function () {
            search();
        });
        $('#EndDate').addClass('search-event-enabled');


    };

    // Pagination ---------------------------------------------------------------------------------
    var enablePageSizeChangeEvent = function () {
        $('#PageSizeDropdown').not('.change-event-enabled').on('change', function () {
            $('#PageSize').val($(this).val());
            ReportsAudiovisualSubscriptionWidget.search();
        });

        $('#PageSizeDropdown').addClass('change-event-enabled');
    };

    var changePage = function () {
        MyRio2cCommon.block({ idOrClass: widgetElementId });
    };

    var handlePaginationReturn = function (data) {
        MyRio2cCommon.handleAjaxReturn({
            data: data,
            // Success
            onSuccess: function () {
                enableShowPlugins();
                MyRio2cCommon.unblock({ idOrClass: widgetElementId });
                $('#SearchKeywords').focus();
            },
            // Error
            onError: function () {
                MyRio2cCommon.unblock({ idOrClass: widgetElementId });
            }
        });
    };

    return {
        init: function () {
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            enableSearchEvents();
            show();
        },
        search: function () {
            search();
        },
        changePage: function () {
            changePage();
        },
        handlePaginationReturn: function (data) {
            handlePaginationReturn(data);
        }
    };
}();
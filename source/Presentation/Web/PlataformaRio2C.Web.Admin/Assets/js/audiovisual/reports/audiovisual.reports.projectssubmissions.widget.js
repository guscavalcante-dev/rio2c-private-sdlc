﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : William Sergio Almado Junior
// Created          : 01-13-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-02-2021
// ***********************************************************************
// <copyright file="audiovisual.reports.projectssubmissions.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var AudiovisualReportsProjectsSubmissionsWidget = function () {

    var widgetElementId = '#AudiovisualReportsProjectsSubmissionsWidget';
    var widgetElement = $(widgetElementId);

    // Search Form --------------------------------------------------------------------------------
    var eanbelSearchFormPlugins = function () {
        MyRio2cCommon.enableSelect2({ inputIdOrClass: '#InterestUid', allowClear: true, placeholder: translations.selectPlaceholder.replace('{0}', translations.genre) + '...' });
        MyRio2cCommon.enableSelect2({ inputIdOrClass: '#TargetAudienceUid', allowClear: true, placeholder: translations.selectPlaceholder.replace('{0}', translations.targetAudience) + '...' });
        MyRio2cCommon.enableDatePicker({ inputIdOrClass: '#StartDate', allowClear: true, placeholder: translations.selectPlaceholder.replace('{0}', translations.startDate) + '...' });
        MyRio2cCommon.enableDatePicker({ inputIdOrClass: '#EndDate', allowClear: true, placeholder: translations.selectPlaceholder.replace('{0}', translations.endDate) + '...' });
    };

    // Show ---------------------------------------------------------------------------------------
    var enableShowPlugins = function () {
        KTApp.initTooltips();
        MyRio2cCommon.initScroll();
        enablePageSizeChangeEvent();
    };

    var show = function () {
        if (widgetElement.length <= 0) {
            return;
        }

        MyRio2cCommon.block();

        var jsonParameters = new Object();
        jsonParameters.Search = $('#SearchKeywords').val();
        jsonParameters.InterestUids = $('#InterestUid').val().join(',');
        jsonParameters.Page = $('#Page').val();
        jsonParameters.PageSize = $('#PageSize').val();
        jsonParameters.IsPitching = $('#IsPitching').prop("checked");
        jsonParameters.TargetAudienceUids = $('#TargetAudienceUid').val();
        jsonParameters.StartDate = $('#StartDate').val();
        jsonParameters.EndDate = $('#EndDate').val();

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/Reports/ShowProjectsSubmissionsWidget'), jsonParameters, function (data) {
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
            MyRio2cCommon.unblock();
        });
    };

    // Search -------------------------------------------------------------------------------------
    var search = function () {
        $('#Page').val('1');
        show();
    };

    var enableSearchEvents = function () {
        //$('#SearchKeywords').not('.search-event-enabled').on('search', function () {
        //    search();
        //});
        //$('#SearchKeywords').addClass('search-event-enabled');

        //$('#InterestUid').not('.change-event-enabled').on('change', function () {
        //    search();
        //});
        //$('#InterestUid').addClass('change-event-enabled');

        //$('#IsPitching').not('.change-event-enabled').on('change', function () {
        //    search();
        //});
        //$('#IsPitching').addClass('change-event-enabled');

        //$('#TargetAudienceUid').not('.change-event-enabled').on('change', function () {
        //    search();
        //});
        //$('#TargetAudienceUid').addClass('change-event-enabled');

        //$('#StartDate').not('.search-event-enabled').on('search', function () {
        //    search();
        //});
        //$('#StartDate').addClass('search-event-enabled');

        //$('#EndDate').not('.search-event-enabled').on('search', function () {
        //    search();
        //});
        //$('#EndDate').addClass('search-event-enabled');
    };

    // Excel -------------------------------------------------------------------------------------
    var exportToExcel = function () {
        if (widgetElement.length <= 0) {
            return;
        }

        var jsonParameters = new Object();
        jsonParameters.Search = $('#SearchKeywords').val();
        jsonParameters.InterestUids = $('#InterestUid').val().join(',');
        jsonParameters.IsPitching = $('#IsPitching').prop("checked");
        jsonParameters.TargetAudienceUids = $('#TargetAudienceUid').val().join(',');
        jsonParameters.StartDate = $('#StartDate').val();
        jsonParameters.EndDate = $('#EndDate').val();

        window.open(MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/Reports/GenerateProjectsSubmissionsExcelAsync') +
            '?search=' + jsonParameters.Search +
            '&interestUids=' + jsonParameters.InterestUids +
            '&isPitching=' + jsonParameters.IsPitching +
            '&targetAudienceUids=' + jsonParameters.TargetAudienceUids +
            '&startDate=' + jsonParameters.StartDate +
            '&endDate=' + jsonParameters.EndDate
        );

        MyRio2cCommon.unblock();
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
        MyRio2cCommon.block();
    };

    var handlePaginationReturn = function (data) {
        MyRio2cCommon.handleAjaxReturn({
            data: data,
            // Success
            onSuccess: function () {
                enableShowPlugins();
                MyRio2cCommon.unblock();
                $('#SearchKeywords').focus();
            },
            // Error
            onError: function () {
                MyRio2cCommon.unblock();
            }
        });
    };

    return {
        init: function () {
            eanbelSearchFormPlugins();
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
        },
        exportToExcel: function () {
            exportToExcel();
        }
    };
}();
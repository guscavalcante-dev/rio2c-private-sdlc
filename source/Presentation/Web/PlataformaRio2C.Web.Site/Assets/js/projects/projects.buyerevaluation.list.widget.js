// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 12-09-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-10-2019
// ***********************************************************************
// <copyright file="projects.buyerevaluation.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var ProjectsBuyerEvaluationListWidget = function () {

    var widgetElementId = '#ProjectBuyerEvaluationListWidget';
    var widgetElement;

    // Initialize Elements ------------------------------------------------------------------------
    var initElements = function () {
        widgetElement = $(widgetElementId);
    };

    // Show ---------------------------------------------------------------------------------------
    var enableShowPlugins = function () {
        KTApp.initTooltips();
        MyRio2cCommon.initScroll();
        MyRio2cCommon.enableSelect2({ inputIdOrClass: '.enable-select2' });
        enablePageSizeChangeEvent();
    };

    var show = function () {
        if (widgetElement.length <= 0) {
            return;
        }

        var jsonParameters = new Object();
        jsonParameters.searchKeywords = $('#SearchKeywords').val();
        jsonParameters.interestUid = $('#InterestUid').val();
        jsonParameters.evaluationStatusUid = $('#EvaluationStatusUid').val();
        jsonParameters.page = $('#Page').val();
        jsonParameters.pageSize = $('#PageSize').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Projects/ShowEvaluationListWidget'), jsonParameters, function (data) {
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
        ProjectsBuyerEvaluationListWidget.init();
    };

    var enableSearchEvent = function () {
        $('#SearchKeywords').not('.search-event-enabled').on('search', function () {
            search();
        });

        $('#SearchKeywords').addClass('search-event-enabled');
    };

    // Pagination ---------------------------------------------------------------------------------
    var enablePageSizeChangeEvent = function () {
        $('#PageSizeDropdown').not('.change-event-enabled').on('change', function () {
            $('#PageSize').val($(this).val());
            ProjectsBuyerEvaluationListWidget.search();
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
                $('#ContactsSearchKeywords').focus();
            },
            // Error
            onError: function () {
                MyRio2cCommon.unblock();
            }
        });
    };

    return {
        init: function () {
            initElements();
            MyRio2cCommon.block();
            enableSearchEvent();
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
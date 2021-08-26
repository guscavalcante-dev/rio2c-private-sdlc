// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Renan Valentim
// Created          : 07-28-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-28-2021
// ***********************************************************************
// <copyright file="audiovisual.projects.evaluation.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var AudiovisualProjectsEvaluationListWidget = function () {

    var widgetElementId = '#ProjectEvaluationListWidget';
    var widgetElement;

    // Initialize Elements ------------------------------------------------------------------------
    var initElements = function () {
        widgetElement = $(widgetElementId);
    };

    // Show ---------------------------------------------------------------------------------------
    var enableShowPlugins = function () {
        KTApp.initTooltips();
        MyRio2cCommon.initScroll();
        MyRio2cCommon.enableSelect2({ inputIdOrClass: '#InterestUid', allowClear: true, placeholder: translations.selectFPlaceholder.replace('{0}', translations.track) + '...' });
        MyRio2cCommon.enableSelect2({ inputIdOrClass: '#EvaluationStatusUid', allowClear: true, placeholder: translations.selectPlaceholder.replace('{0}', translations.status) + '...' });
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

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/Projects/ShowEvaluationListWidget'), jsonParameters, function (data) {
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
        AudiovisualProjectsEvaluationListWidget.init();
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

        $('#EvaluationStatusUid').not('.change-event-enabled').on('change', function () {
            search();
        });
        $('#EvaluationStatusUid').addClass('change-event-enabled');
    };

    // Pagination ---------------------------------------------------------------------------------
    var enablePageSizeChangeEvent = function () {
        $('#PageSizeDropdown').not('.change-event-enabled').on('change', function () {
            $('#PageSize').val($(this).val());
            AudiovisualProjectsEvaluationListWidget.search();
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

    // Refresh project ----------------------------------------------------------------------------
    var refreshProject = function (projectUid) {
        if (MyRio2cCommon.isNullOrEmpty(projectUid)) {
	        return;
        }

        var projectWidgetName = '#project-' + projectUid;
        if ($(projectWidgetName).length <= 0) {
		    return;
        }

        MyRio2cCommon.block({ idOrClass: projectWidgetName });

	    var jsonParameters = new Object();
        jsonParameters.projectUid = projectUid;

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/Projects/ShowEvaluationListItemWidget'), jsonParameters, function (data) {
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
            MyRio2cCommon.unblock({ idOrClass: projectWidgetName });
	    });
    };

    return {
        init: function () {
            initElements();
            MyRio2cCommon.block();
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
        refreshProject: function(projectUid) {
	        refreshProject(projectUid);
        }
    };
}();
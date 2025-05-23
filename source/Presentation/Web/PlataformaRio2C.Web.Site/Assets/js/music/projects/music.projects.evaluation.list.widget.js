﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 02-26-2020
//
// Last Modified By : Daniel Giese Rodrigues
// Last Modified On : 01-07-2025
// ***********************************************************************
// <copyright file="music.projects.evaluation.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var MusicProjectsEvaluationListWidget = function () {

    var widgetElementId = '#MusicProjectEvaluationListWidget';
    var widgetElement;

    // Initialize Elements ------------------------------------------------------------------------
    var initElements = function () {
        widgetElement = $(widgetElementId);
    };

    // Show ---------------------------------------------------------------------------------------
    var enableShowPlugins = function () {
        KTApp.initTooltips();
        MyRio2cCommon.initScroll();
        MyRio2cCommon.enableSelect2({ inputIdOrClass: '#MusicGenreUid', allowClear: true, placeholder: translations.selectPlaceholder.replace('{0}', translations.genre) + '...' });
        MyRio2cCommon.enableSelect2({ inputIdOrClass: '#EvaluationStatusUid', allowClear: true, placeholder: translations.selectPlaceholder.replace('{0}', translations.status) + '...' });
        enablePageSizeChangeEvent();
    };

    var show = function () {
        if (widgetElement.length <= 0) {
            return;
        }

        var jsonParameters = new Object();
        jsonParameters.searchKeywords = $('#SearchKeywords').val();
        jsonParameters.musicGenreUid = $('#MusicGenreUid').val();
        jsonParameters.evaluationStatusUid = $('#EvaluationStatusUid').val();
        jsonParameters.showBusinessRounds = $('#ShowBusinessRounds').prop('checked');
        jsonParameters.page = $('#Page').val();
        jsonParameters.pageSize = $('#PageSize').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Music/PitchingProjects/ShowEvaluationListWidget'), jsonParameters, function (data) {
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
        MusicProjectsEvaluationListWidget.init();
    };

    var enableSearchEvents = function () {
        $('#SearchKeywords').not('.search-event-enabled').on('search', function () {
            search();
        });
        $('#SearchKeywords').addClass('search-event-enabled');

        $('#MusicGenreUid').not('.change-event-enabled').on('change', function () {
            search();
        });
        $('#MusicGenreUid').addClass('change-event-enabled');

        $('#EvaluationStatusUid').not('.change-event-enabled').on('change', function () {
            search();
        });
        $('#EvaluationStatusUid').addClass('change-event-enabled');

        $('#ShowBusinessRounds').not('.change-event-enabled').on('change', function () {
            search();
        });
        $('#ShowBusinessRounds').addClass('change-event-enabled');
    };

    // Pagination ---------------------------------------------------------------------------------
    var enablePageSizeChangeEvent = function () {
        $('#PageSizeDropdown').not('.change-event-enabled').on('change', function () {
            $('#PageSize').val($(this).val());
            MusicProjectsEvaluationListWidget.search();
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

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Music/PitchingProjects/ShowEvaluationListItemWidget'), jsonParameters, function (data) {
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
// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 12-09-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-20-2020
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
        MyRio2cCommon.enableSelect2({ inputIdOrClass: '#EvaluationStatusUid', allowClear: true, placeholder: translations.selectPlaceholder.replace('{0}', translations.status) + '...' });
        MyRio2cCommon.enableSelect2({ inputIdOrClass: '#TargetAudienceUid', allowClear: true, placeholder: translations.selectPlaceholder.replace('{0}', translations.participantProfile) + '...' });
        MyRio2cCommon.enableSelect2({ inputIdOrClass: '#InterestAreaInterestUid', allowClear: true, placeholder: translations.selectPlaceholder.replace('{0}', translations.interest) + '...' });
        MyRio2cCommon.enableSelect2({ inputIdOrClass: '#BusinessRoundObjetiveInterestsUid', allowClear: true, placeholder: translations.selectPlaceholder.replace('{0}', translations.objective) + '...' });        
        enablePageSizeChangeEvent();
    };

    var show = function () {
        if (widgetElement.length <= 0) {
            return;
        }

        var jsonParameters = new Object();
        jsonParameters.searchKeywords = $('#SearchKeywords').val();
        jsonParameters.evaluationStatusUid = $('#EvaluationStatusUid').val();
        jsonParameters.targetAudienceUid = $('#TargetAudienceUid').val();
        jsonParameters.interestAreaInterestUid = $('#InterestAreaInterestUid').val();
        jsonParameters.businessRoundObjetiveInterestsUid = $('#BusinessRoundObjetiveInterestsUid').val();
        jsonParameters.page = $('#Page').val();
        jsonParameters.pageSize = $('#PageSize').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Music/BusinessRoundProjects/ShowEvaluationListWidget'), jsonParameters, function (data) {
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

    var enableSearchEvents = function () {
        $('#SearchKeywords').not('.search-event-enabled').on('search', function () {
            search();
        });
        $('#SearchKeywords').addClass('search-event-enabled');

        // Evaluation Status
        $('#EvaluationStatusUid').not('.change-event-enabled').on('change', function () {
            search();
        });
        $('#EvaluationStatusUid').addClass('change-event-enabled');

        // Target Audience
        $('#TargetAudienceUid').not('.change-event-enabled').on('change', function () {
            search();
        });
        $('#TargetAudienceUid').addClass('change-event-enabled');

        // Interest - Interest Area
        $('#InterestAreaInterestUid').not('.change-event-enabled').on('change', function () {
            search();
        });
        $('#InterestAreaInterestUid').addClass('change-event-enabled');

        // Interest - Business Round Objetive
        $('#BusinessRoundObjetiveInterestsUid').not('.change-event-enabled').on('change', function () {
            search();
        });
        $('#BusinessRoundObjetiveInterestsUid').addClass('change-event-enabled');
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

	    $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Music/BusinessRoundProjects/ShowEvaluationListItemWidget'), jsonParameters, function (data) {
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
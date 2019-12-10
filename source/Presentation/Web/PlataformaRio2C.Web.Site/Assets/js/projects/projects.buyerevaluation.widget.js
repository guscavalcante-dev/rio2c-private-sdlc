// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
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

var ProjectsBuyerEvaluationWidget = function () {

    var widgetElementId = '#ProjectBuyerEvaluationWidget';
    var widgetElement;

    var acceptModalId = '#AcceptEvaluationModal';
    var acceptFormId = '#AcceptEvaluationForm';
    var refuseModalId = '#RefuseEvaluationModal';
    var refuseFormId = '#RefuseEvaluationForm';

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
        ProjectsBuyerEvaluationWidget.init();
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
            ProjectsBuyerEvaluationWidget.search();
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

    // Accept -------------------------------------------------------------------------------------
    var enableAcceptAjaxForm = function () {
        MyRio2cCommon.enableAjaxForm({
            idOrClass: acceptFormId,
            onSuccess: function (data) {
                $(acceptModalId).modal('hide');

                if (typeof (ProjectsBuyerEvaluationWidget) !== 'undefined') {
                    ProjectsBuyerEvaluationWidget.init();
                }
            },
            onError: function (data) {
                if (MyRio2cCommon.hasProperty(data, 'pages')) {
                    enableAcceptPlugins();
                }
            }
        });
    };

    var enableAcceptPlugins = function () {
        MyRio2cCommon.enableSelect2({ inputIdOrClass: acceptFormId + ' .enable-select2' });
        enableAcceptAjaxForm();
        MyRio2cCommon.enableFormValidation({ formIdOrClass: acceptFormId, enableHiddenInputsValidation: true, enableMaxlength: true });
    };

    var showAcceptModal = function (projectUid) {
        if (MyRio2cCommon.isNullOrEmpty(projectUid)) {
            return;
        }

        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();
        jsonParameters.projectUid = projectUid;

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Projects/ShowAcceptEvaluationModal'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    enableAcceptPlugins();
                    $(acceptModalId).modal();
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

    // Refuse -------------------------------------------------------------------------------------
    var changeIsRequired = function () {
        var hasAdditionalInfoElement = $('#HasAdditionalInfo');

        var hasAdditionalInfo = $('#ProjectEvaluationRefuseReasonUid').find(":selected").data("hasadditionalinfo");
        if (!MyRio2cCommon.isNullOrEmpty(hasAdditionalInfo)) {
            hasAdditionalInfoElement.val(hasAdditionalInfo);
        }
        else {
            hasAdditionalInfoElement.val('False');
        }

        if (hasAdditionalInfoElement.val() === 'True') {
            $('#AdditionalReasonContainer').removeClass('d-none');
        }
        else {
            $('#AdditionalReasonContainer').addClass('d-none');
        }
    };

    var enableProjectEvaluationRefuseReasonChangeEvent = function () {
        $('#ProjectEvaluationRefuseReasonUid').not('.change-event-enabled').on('change', function () {
            changeIsRequired();
        });

        $('#ProjectEvaluationRefuseReasonUid').addClass('change-event-enabled');
    };

    var enableRefuseAjaxForm = function () {
        MyRio2cCommon.enableAjaxForm({
            idOrClass: refuseFormId,
            onSuccess: function (data) {
                $(refuseModalId).modal('hide');

                if (typeof (ProjectsBuyerEvaluationWidget) !== 'undefined') {
                    ProjectsBuyerEvaluationWidget.init();
                }
            },
            onError: function (data) {
                if (MyRio2cCommon.hasProperty(data, 'pages')) {
                    enableRefusePlugins();
                }
            }
        });
    };

    var enableRefusePlugins = function () {
        enableProjectEvaluationRefuseReasonChangeEvent();
        MyRio2cCommon.enableSelect2({ inputIdOrClass: refuseFormId + ' .enable-select2' });
        enableRefuseAjaxForm();
        MyRio2cCommon.enableFormValidation({ formIdOrClass: refuseFormId, enableHiddenInputsValidation: true, enableMaxlength: true });
    };

    var showRefuseModal = function (projectUid) {
        if (MyRio2cCommon.isNullOrEmpty(projectUid)) {
            return;
        }

        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();
        jsonParameters.projectUid = projectUid;

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Projects/ShowRefuseEvaluationModal'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    enableRefusePlugins();
                    $(refuseModalId).modal();
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
        },
        showAcceptModal: function (projectUid) {
            showAcceptModal(projectUid);
        },
        showRefuseModal: function (projectUid) {
            showRefuseModal(projectUid);
        }
    };
}();
// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 12-10-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-21-2020
// ***********************************************************************
// <copyright file="music.businessrounds.buyerevaluation.update.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var ProjectsBuyerEvaluationUpdateWidget = function () {

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
    };

    var show = function () {
        if (widgetElement.length <= 0) {
            return;
        }

        var jsonParameters = new Object();
        jsonParameters.projectUid = $('#AggregateId').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Music/BusinessRoundProjects/ShowBuyerEvaluationWidget'), jsonParameters, function (data) {
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

    // Accept -------------------------------------------------------------------------------------
    var enableAcceptAjaxForm = function () {
        MyRio2cCommon.enableAjaxForm({
            idOrClass: acceptFormId,
            onSuccess: function (data) {
                $(acceptModalId).modal('hide');

                if (typeof (ProjectsBuyerEvaluationUpdateWidget) !== 'undefined') {
                    ProjectsBuyerEvaluationUpdateWidget.init();
                }

                if (typeof (ProjectsBuyerEvaluationListWidget) !== 'undefined' && !MyRio2cCommon.isNullOrEmpty(data.projectUid)) {
                    ProjectsBuyerEvaluationListWidget.refreshProject(data.projectUid);
                }
            },
            onError: function (data) {
                if (MyRio2cCommon.hasProperty(data, 'pages')) {
                    enableAcceptPlugins();
                }

                $(acceptFormId).find(":input.input-validation-error:first").focus();
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
        jsonParameters.buyerAttendeeOrganizationUid = $('#AttendeeOrganizationUid').val()

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Music/BusinessRoundProjects/ShowAcceptEvaluationModal'), jsonParameters, function (data) {
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

                if (typeof (ProjectsBuyerEvaluationUpdateWidget) !== 'undefined') {
                    ProjectsBuyerEvaluationUpdateWidget.init();
                }

                if (typeof (ProjectsBuyerEvaluationListWidget) !== 'undefined' && !MyRio2cCommon.isNullOrEmpty(data.projectUid)) {
	                ProjectsBuyerEvaluationListWidget.refreshProject(data.projectUid);
                }
            },
            onError: function (data) {
                if (MyRio2cCommon.hasProperty(data, 'pages')) {
                    enableRefusePlugins();
                }

                $(refuseFormId).find(":input.input-validation-error:first").focus();
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

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Music/BusinessRoundProjects/ShowRefuseEvaluationModal'), jsonParameters, function (data) {
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
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            show();
        },
        showAcceptModal: function (projectUid) {
            showAcceptModal(projectUid);
        },
        showRefuseModal: function (projectUid) {
            showRefuseModal(projectUid);
        }
    };
}();
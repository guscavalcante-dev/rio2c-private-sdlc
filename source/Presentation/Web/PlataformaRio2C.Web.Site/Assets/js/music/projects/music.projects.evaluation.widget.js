// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 02-28-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-29-2020
// ***********************************************************************
// <copyright file="music.projects.evaluation.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var MusicProjectsEvaluationWidget = function () {

    var widgetElementId = '#ProjectEvaluationWidget';
    var widgetElement;

    var acceptModalId = '#AcceptMusicPitchingEvaluationModal';
    var acceptFormId = '#AcceptMusicPitchingEvaluationForm';
    var refuseModalId = '#RefuseMusicPitchingEvaluationModal';
    var refuseFormId = '#RefuseMusicPitchingEvaluationForm';

    // Initialize Elements ------------------------------------------------------------------------
    var initElements = function () {
        widgetElement = $(widgetElementId);
    };

    // Show ---------------------------------------------------------------------------------------
    var enableShowPlugins = function () {
        KTApp.initTooltips();
        MyRio2cCommon.initScroll();
        MyRio2cCommon.enableDecimal('decimal-globalize-mask', 15);
    };

    var show = function () {
        if (widgetElement.length <= 0) {
            return;
        }

        var jsonParameters = new Object();
        jsonParameters.projectUid = $('#AggregateId').val();

        //$.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Music/Projects/ShowEvaluationWidget'), jsonParameters, function (data) {
        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Music/Projects/ShowEvaluationGradeWidget'), jsonParameters, function (data) {
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

    var enableAcceptAjaxForm = function () {
        MyRio2cCommon.enableAjaxForm({
            idOrClass: acceptFormId,
            onSuccess: function (data) {
                $(acceptModalId).modal('hide');
                if (typeof (MusicProjectsEvaluationWidget) !== 'undefined') {
                    MusicProjectsEvaluationWidget.init();
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

    var enableRefuseAjaxForm = function () {
        MyRio2cCommon.enableAjaxForm({
            idOrClass: refuseFormId,
            onSuccess: function (data) {
                $(refuseModalId).modal('hide');
                if (typeof (MusicProjectsEvaluationWidget) !== 'undefined') {
                    MusicProjectsEvaluationWidget.init();
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

    var enableAcceptPlugins = function () {
        MyRio2cCommon.enableSelect2({ inputIdOrClass: acceptFormId + ' .enable-select2' });
        enableAcceptAjaxForm();
        MyRio2cCommon.enableFormValidation({ formIdOrClass: acceptFormId, enableHiddenInputsValidation: true, enableMaxlength: true });
    };

    var showAcceptModal = function (musicProjectUid) {
        if (MyRio2cCommon.isNullOrEmpty(musicProjectUid)) {
            return;
        }

        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();
        jsonParameters.musicProjectUid = musicProjectUid;

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Music/Projects/ShowAcceptEvaluationModal'), jsonParameters, function (data) {
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

    var enableRefusePlugins = function () {
        MyRio2cCommon.enableSelect2({ inputIdOrClass: refuseFormId + ' .enable-select2' });
        enableRefuseAjaxForm();
        MyRio2cCommon.enableFormValidation({ formIdOrClass: refuseFormId, enableHiddenInputsValidation: true, enableMaxlength: true });
    };

    var showRefuseModal = function (musicProjectUid) {
        if (MyRio2cCommon.isNullOrEmpty(musicProjectUid)) {
            return;
        }

        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();
        jsonParameters.musicProjectUid = musicProjectUid;

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Music/Projects/ShowRefuseEvaluationModal'), jsonParameters, function (data) {
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

    // Evaluation Grade ---------------------------------------------------------------------------
    var submitEvaluationGrade = function (musicBandId) {
        var jsonParameters = new Object();
        jsonParameters.musicBandId = musicBandId;
        jsonParameters.grade = $('#AttendeeMusicBandEvaluationGrade').val();

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Music/Projects/Evaluate'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
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
                MusicProjectsEvaluationWidget.init();
                MusicProjectsEvaluatorsWidget.init();
                MusicProjectsMainInformationWidget.init();
            });
    };
    
    var submitAcceptEvaluation = function () {
        const form = $('#AcceptMusicPitchingEvaluationForm');
        $.post(form.attr('action'), form.serialize(), function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    $(acceptModalId).modal('hide');
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
                MusicProjectsEvaluationWidget.init();
                MusicProjectsEvaluatorsWidget.init();
                MusicProjectsMainInformationWidget.init();
            });
    };

    var submitRefuseEvaluation = function () {
        const form = $('#RefuseMusicPitchingEvaluationForm');
        $.post(form.attr('action'), form.serialize(), function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    $(refuseModalId).modal('hide');
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
                MusicProjectsEvaluationWidget.init();
                MusicProjectsEvaluatorsWidget.init();
                MusicProjectsMainInformationWidget.init();
            });
    };

    return {
        init: function () {
            initElements();
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            show();
        },
        submitEvaluationGrade: function (musicBandId) {
            submitEvaluationGrade(musicBandId);
        },     
        showAcceptModal: function (musicProjectUid) {
            showAcceptModal(musicProjectUid);
        },
        showRefuseModal: function (musicProjectUid) {
            showRefuseModal(musicProjectUid);
        },
        submitAcceptEvaluation: submitAcceptEvaluation,
        submitRefuseEvaluation: submitRefuseEvaluation,
    };
}();
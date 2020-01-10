// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 01-04-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="conferences.tracks.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var ConferencesTracksAndPresentationFormatsWidget = function () {

    var widgetElementId = '#ConferenceTracksAndPresentationFormatsWidget';
    var widgetElement = $(widgetElementId);

    var updateModalId = '#UpdateTracksAndPresentationFormatsModal';
    var updateFormId = '#UpdateTracksAndPresentationFormatsForm';

    // Show ---------------------------------------------------------------------------------------
    var enableShowPlugins = function () {
        KTApp.initTooltips();
    };

    var show = function () {
        if (widgetElement.length <= 0) {
            return;
        }

        var jsonParameters = new Object();
        jsonParameters.conferenceUid = $('#AggregateId').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Conferences/ShowTracksAndPresentationFormatsWidget'), jsonParameters, function (data) {
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

    // Update -------------------------------------------------------------------------------------
    var enableAjaxForm = function () {
        MyRio2cCommon.enableAjaxForm({
            idOrClass: updateFormId,
            onSuccess: function (data) {
                $(updateModalId).modal('hide');

                if (typeof (ConferencesTracksAndPresentationFormatsWidget) !== 'undefined') {
                    ConferencesTracksAndPresentationFormatsWidget.init();
                }
            },
            onError: function (data) {
                if (MyRio2cCommon.hasProperty(data, 'pages')) {
                    enableUpdatePlugins();
                }
            }
        });
    };

    var enableUpdatePlugins = function () {
        enableAjaxForm();
        MyRio2cCommon.enableFormValidation({ formIdOrClass: updateFormId, enableHiddenInputsValidation: true, enableMaxlength: true });
    };

    var showUpdateModal = function () {
        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();
        jsonParameters.conferenceUid = $('#AggregateId').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Conferences/ShowUpdateTracksAndPresentationFormatsModal'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    enableUpdatePlugins();
                    $(updateModalId).modal();
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
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            show();
        },
        showUpdateModal: function () {
            showUpdateModal();
        }
    };
}();
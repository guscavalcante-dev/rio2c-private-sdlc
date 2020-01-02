// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 12-27-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-02-2020
// ***********************************************************************
// <copyright file="conferences.create.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var ConferencesCreate = function () {

    var modalId = '#CreateConferenceModal';
    var formId = '#CreateConferenceForm';

    // Enable form validation ---------------------------------------------------------------------
    var enableFormValidation = function () {
        MyRio2cCommon.enableFormValidation({ formIdOrClass: formId, enableHiddenInputsValidation: true, enableMaxlength: true });
    };

    // Enable plugins -----------------------------------------------------------------------------
    var enableDatePickers = function () {
        $('#Date').datepicker({
            todayHighlight: true,
            orientation: "bottom left",
            autoclose: true,
            language: MyRio2cCommon.getGlobalVariable('userInterfaceLanguage')
        });
    };

    var enableTimePickers = function () {
        $('#StartTime, #EndTime').timepicker({
            defaultTime: false,
            minuteStep: 1,
            showSeconds: false,
            showMeridian: false,
            icons: {
                up: 'la la-angle-up',
                down: 'la la-angle-down'
            }
        });
    };

    var enablePlugins = function () {
        MyRio2cCommon.enableSelect2({ inputIdOrClass: formId + ' .enable-select2' });
        enableDatePickers();
        enableTimePickers();
        enableAjaxForm();
        enableFormValidation();
    };

    // Show modal ---------------------------------------------------------------------------------
    var showModal = function () {
        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Conferences/ShowCreateModal'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    enablePlugins();
                    $(modalId).modal();
                },
                // Error
                onError: function() {
                }
            });
        })
        .fail(function () {
        })
        .always(function () {
            MyRio2cCommon.unblock();
        });
    };

    // Enable ajax form ---------------------------------------------------------------------------
    var enableAjaxForm = function () {
        MyRio2cCommon.enableAjaxForm({
            idOrClass: formId,
            onSuccess: function (data) {
                $(modalId).modal('hide');

                if (typeof (ConferencesDataTableWidget) !== 'undefined') {
                    ConferencesDataTableWidget.refreshData();
                }

                if (typeof (ConferencesTotalCountWidget) !== 'undefined') {
                    ConferencesTotalCountWidget.init();
                }

                if (typeof (ConferencesEditionCountWidget) !== 'undefined') {
                    ConferencesEditionCountWidget.init();
                }
            },
            onError: function (data) {
                if (MyRio2cCommon.hasProperty(data, 'pages')) {
                    enablePlugins();
                }
            }
        });
    };

    return {
        showModal: function () {
            showModal();
        }
    };
}();
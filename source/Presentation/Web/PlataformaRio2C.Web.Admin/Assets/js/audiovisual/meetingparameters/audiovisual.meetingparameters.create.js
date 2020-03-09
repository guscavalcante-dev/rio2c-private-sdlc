// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 03-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-05-2020
// ***********************************************************************
// <copyright file="audiovisual.meetingparameters.create.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var AudiovisualMeetingParametersCreate = function () {

    var modalId = '#CreateAudiovisualMeetingParameterModal';
    var formId = '#CreateCAudiovisualMeetingParameterForm';

    // Enable form validation ---------------------------------------------------------------------
    var enableFormValidation = function () {
        MyRio2cCommon.enableFormValidation({ formIdOrClass: formId, enableHiddenInputsValidation: true, enableMaxlength: true });
    };

    // Enable plugins -----------------------------------------------------------------------------
    var enablePlugins = function () {
        MyRio2cCommon.enableDatePicker({ inputIdOrClass: formId + ' .enable-datepicker' });
        MyRio2cCommon.enableTimePicker({ inputIdOrClass: formId + ' .enable-timepicker' });
        enableAjaxForm();
        enableFormValidation();
    };

    // Show modal ---------------------------------------------------------------------------------
    var showModal = function () {
        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/MeetingParameters/ShowCreateModal'), jsonParameters, function (data) {
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

                if (typeof (AudiovisualMeetingParametersDataTableWidget) !== 'undefined') {
	                AudiovisualMeetingParametersDataTableWidget.refreshData();
                }

                if (typeof (AudiovisualMeetingParametersTotalCountWidget) !== 'undefined') {
	                AudiovisualMeetingParametersTotalCountWidget.init();
                }

                if (typeof (AudiovisualMeetingParametersEditionCountWidget) !== 'undefined') {
	                AudiovisualMeetingParametersEditionCountWidget.init();
                }
            },
            onError: function (data) {
                if (MyRio2cCommon.hasProperty(data, 'pages')) {
                    enablePlugins();
                }

                $(formId).find(":input.input-validation-error:first").focus();
            }
        });
    };

    return {
        showModal: function () {
            showModal();
        }
    };
}();
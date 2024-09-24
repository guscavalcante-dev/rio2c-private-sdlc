// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 08-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-16-2020
// ***********************************************************************
// <copyright file="logistics.airfare.update.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var LogisticAirfareUpdate = function () {

    var modalId = '#UpdateLogisticAirfareModal';
    var formId = '#UpdateLogisticAirfareForm';

    // Enable plugins -----------------------------------------------------------------------------
    var enablePlugins = function () {
	    MyRio2cCommon.enableSelect2({ inputIdOrClass: formId + ' .enable-select2', allowClear: true });
        MyRio2cCommon.enableDateTimePicker({ inputIdOrClass: formId + ' .enable-datetimepicker', allowClear: true });
        MyRio2cCommon.enableCustomFile();
        enableAjaxForm();
        MyRio2cCommon.enableFormValidation({ formIdOrClass: formId, enableHiddenInputsValidation: true, enableMaxlength: true });
    };

    // Show modal ---------------------------------------------------------------------------------
    var showModal = function (uid) {
        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();
        jsonParameters.uid = uid;

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Logistics/Requests/ShowUpdateAirfareModal'), jsonParameters, function (data) {
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

                if (typeof (LogisticsAirfareWidget) !== 'undefined') {
	                LogisticsAirfareWidget.init();
                }
            },
            onError: function (data) {
                if (MyRio2cCommon.hasProperty(data, 'pages')) {
                    enablePlugins();
                }
            }
        });
    };

    // Ticket pdf ---------------------------------------------------------------------------------
    var changeFile = function () {
        $('#file-preview-container').addClass('d-none');
        $('#file-container').removeClass('d-none');
        $('#IsTicketFileDeleted').val('True');
    }

    return {
        showModal: function (uid) {
            showModal(uid);
        },
        changeFile: function () {
	        changeFile();
        }
    };
}();
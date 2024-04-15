// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 12-04-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-04-2024
// ***********************************************************************
// <copyright file="availabilities.create.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var AvailabilitiesCreate = function () {

    var modalId = '#CreateAvailabilityModal';
    var formId = '#CreateAvailabilityForm';

    // Show modal ---------------------------------------------------------------------------------
    var enableAjaxForm = function () {
        MyRio2cCommon.enableAjaxForm({
            idOrClass: formId,
            onSuccess: function (data) {
                $(modalId).modal('hide');

                if (typeof (AvailabilitiesDataTableWidget) !== 'undefined') {
                    AvailabilitiesDataTableWidget.refreshData();
                }
            },
            onError: function (data) {
                if (MyRio2cCommon.hasProperty(data, 'pages')) {
                    enablePlugins();
                }
            }
        });
    };

    var enableFormValidation = function () {
        MyRio2cCommon.enableFormValidation({ formIdOrClass: formId, enableHiddenInputsValidation: true, enableMaxlength: true });
    };

    var enablePlugins = function () {
        MyRio2cCommon.enableCollaboratorSelect2({ url: '/Collaborators/FindAllByFilters' });
        MyRio2cCommon.enableSelect2({ inputIdOrClass: formId + ' .enable-select2', allowClear: true });
        MyRio2cCommon.enableDatePicker({ inputIdOrClass: formId + ' .enable-datepicker' });
        enableAjaxForm();
        enableFormValidation();
    };

    var showModal = function () {
        MyRio2cCommon.block({ isModal: true });

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Logistics/Availability/ShowCreateModal'), function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    enablePlugins();
                    $(modalId).modal();
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
        showModal: function () {
            showModal();
        }
    };
}();
// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 03-17-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-17-2020
// ***********************************************************************
// <copyright file="places.create.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var PlacesCreate = function () {

    var modalId = '#CreatePlaceModal';
    var formId = '#CreatePlaceForm';

    // Enable plugins -----------------------------------------------------------------------------
    var enablePlugins = function () {
        enableAjaxForm();
        MyRio2cCommon.enableFormValidation({ formIdOrClass: formId, enableHiddenInputsValidation: true, enableMaxlength: true });
    };

    // Show modal ---------------------------------------------------------------------------------
    var showModal = function (placeUid) {
        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();
        jsonParameters.placeUid = placeUid;

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Places/ShowCreateModal'), jsonParameters, function (data) {
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

                if (typeof (PlacesDataTableWidget) !== 'undefined') {
                    PlacesDataTableWidget.refreshData();
                }

                if (typeof (PlacesTotalCountWidget) !== 'undefined') {
                    PlacesTotalCountWidget.init();
                }

                if (typeof (PlacesEditionCountWidget) !== 'undefined') {
                    PlacesEditionCountWidget.init();
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
        showModal: function (placeUid) {
            showModal(placeUid);
        }
    };
}();
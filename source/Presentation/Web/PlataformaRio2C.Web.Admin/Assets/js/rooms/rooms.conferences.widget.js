﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-06-2020
// ***********************************************************************
// <copyright file="rooms.conferences.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var RoomsConferencesWidget = function () {

    var widgetElementId = '#RoomConferencesWidget';
    var widgetElement = $(widgetElementId);

    //var updateModalId = '#UpdateMainInformationModal';
    //var updateFormId = '#UpdateMainInformationForm';

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
        jsonParameters.roomUid = $('#AggregateId').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Rooms/ShowConferencesWidget'), jsonParameters, function (data) {
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

    //// Update -------------------------------------------------------------------------------------
    //var enableAjaxForm = function () {
    //    MyRio2cCommon.enableAjaxForm({
    //        idOrClass: updateFormId,
    //        onSuccess: function (data) {
    //            $(updateModalId).modal('hide');

    //            if (typeof (RoomsMainInformationWidget) !== 'undefined') {
    //                RoomsMainInformationWidget.init();
    //            }
    //        },
    //        onError: function (data) {
    //            if (MyRio2cCommon.hasProperty(data, 'pages')) {
    //                enableUpdatePlugins();
    //            }
    //        }
    //    });
    //};

    //var enableUpdatePlugins = function () {
    //    enableAjaxForm();
    //    MyRio2cCommon.enableFormValidation({ formIdOrClass: updateFormId, enableHiddenInputsValidation: true, enableMaxlength: true });
    //};

    //var showUpdateModal = function () {
    //    MyRio2cCommon.block({ isModal: true });

    //    var jsonParameters = new Object();
    //    jsonParameters.roomUid = $('#AggregateId').val();

    //    $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Rooms/ShowUpdateMainInformationModal'), jsonParameters, function (data) {
    //        MyRio2cCommon.handleAjaxReturn({
    //            data: data,
    //            // Success
    //            onSuccess: function () {
    //                enableUpdatePlugins();
    //                $(updateModalId).modal();
    //            },
    //            // Error
    //            onError: function () {
    //            }
    //        });
    //    })
    //    .fail(function () {
    //    })
    //    .always(function () {
    //        MyRio2cCommon.unblock();
    //    });
    //};

    return {
        init: function () {
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            show();
        },
        //showUpdateModal: function () {
        //    showUpdateModal();
        //}
    };
}();
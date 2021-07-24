// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 07-24-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-24-2021
// ***********************************************************************
// <copyright file="innovation.projects.releasedprojects.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var InnovationProjectsReleasedWidget = function () {

    var widgetElementId = '#ReleasedProjectsWidget';
    var widgetElement = $(widgetElementId);

    var updateModalId = '#UpdateReleasedProjectsModal';
    var updateFormId = '#UpdateReleasedProjectsForm';

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

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Innovation/Projects/ShowReleasedProjectsWidget'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    enableShowPlugins();
                },
                // Error
                onError: function() {
                }
            });
        })
        .fail(function () {
            //showAlert();
            //MyRio2cCommon.unblock(widgetElementId);
        })
        .always(function() {
            MyRio2cCommon.unblock({ idOrClass: widgetElementId });
        });
    };

    //// Update -------------------------------------------------------------------------------------
    //var enableAjaxForm = function () {
    //    MyRio2cCommon.enableAjaxForm({
    //        idOrClass: updateFormId,
    //        onSuccess: function (data) {
    //            $(updateModalId).modal('hide');

    //            if (typeof (InnovationProjectsMainInformationWidget) !== 'undefined') {
	   //             InnovationProjectsMainInformationWidget.init();
    //            }
    //        },
    //        onError: function (data) {
    //            if (MyRio2cCommon.hasProperty(data, 'pages')) {
    //                enableUpdatePlugins();
    //            }

    //            $(updateFormId).find(":input.input-validation-error:first").focus();
    //        }
    //    });
    //};

    //var enableUpdatePlugins = function () {
    //    //MyRio2cCommon.enableSelect2({ inputIdOrClass: updateFormId + ' .enable-select2' });
    //    MyRio2cInputMask.enableMask('#TotalPlayingTime', '99:99:99');
    //    MyRio2cInputMask.enableMask('#EachEpisodePlayingTime', '99:99:99');
    //    enableAjaxForm();
    //    MyRio2cCommon.enableFormValidation({ formIdOrClass: updateFormId, enableHiddenInputsValidation: true, enableMaxlength: true });
    //};

    //var showUpdateModal = function () {
    //    MyRio2cCommon.block({ isModal: true });

    //    var jsonParameters = new Object();
    //    jsonParameters.projectUid = $('#AggregateId').val();

    //    $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Innovation/Projects/ShowUpdateMainInformationModal'), jsonParameters, function (data) {
    //        MyRio2cCommon.handleAjaxReturn({
    //        data: data,
    //        // Success
    //        onSuccess: function () {
    //            enableUpdatePlugins();
    //            $(updateModalId).modal();
    //        },
    //        // Error
    //        onError: function () {
    //        }
    //    });
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
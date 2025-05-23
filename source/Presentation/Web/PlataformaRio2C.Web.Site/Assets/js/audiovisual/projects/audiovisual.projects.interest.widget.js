﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-20-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-21-2021
// ***********************************************************************
// <copyright file="audiovisual.projects.interest.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var AudiovisualProjectsInterestWidget = function () {

    var widgetElementId = '#ProjectInterestWidget';
    var widgetElement = $(widgetElementId);

    var updateModalId = '#UpdateInterestModal';
    var updateFormId = '#UpdateInterestForm';

    // Show ---------------------------------------------------------------------------------------
    var enableShowPlugins = function () {
        KTApp.initTooltips();
    };

    var show = function () {
        if (widgetElement.length <= 0) {
            return;
        }

        var jsonParameters = new Object();
        jsonParameters.projectUid = $('#AggregateId').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/Projects/ShowInterestWidget'), jsonParameters, function (data) {
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

    // Update -------------------------------------------------------------------------------------
    var enableAjaxForm = function () {
        MyRio2cCommon.enableAjaxForm({
            idOrClass: updateFormId,
            onSuccess: function (data) {
                $(updateModalId).modal('hide');

                if (typeof (AudiovisualProjectsInterestWidget) !== 'undefined') {
                    AudiovisualProjectsInterestWidget.init();
                }
            },
            onError: function (data) {
                if (MyRio2cCommon.hasProperty(data, 'pages')) {
                    enableUpdatePlugins();
                }

                $(updateFormId).find(":input.input-validation-error:first").focus();
            }
        });
    };

    var enableUpdatePlugins = function () {
        MyRio2cCommon.enableAtLeastOnCheckboxByNameValidation(updateFormId);
        enableAjaxForm();
        MyRio2cCommon.enableFormValidation({ formIdOrClass: updateFormId, enableHiddenInputsValidation: true, enableMaxlength: true });

        // Enable additional info textbox
        if (typeof (MyRio2cCommonAdditionalInfo) !== 'undefined') {
            MyRio2cCommonAdditionalInfo.init();
        }
    };

    var showUpdateModal = function () {
        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();
        jsonParameters.projectUid = $('#AggregateId').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/Projects/ShowUpdateInterestModal'), jsonParameters, function (data) {
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

    //// Form submit --------------------------------------------------------------------------------
    //var submit = function () {
    //    var validator = $(updateFormId).validate();
    //    var formValidation = $(updateFormId).valid();
    //    //var interestsValidation = MyRio2cCommon.validateRequireOneGroup();

    //    if (formValidation/* && interestsValidation*/) {
    //        MyRio2cCommon.submitForm(updateFormId);
    //    }
    //    else {
    //        validator.focusInvalid();
    //    }
    //};

    return {
        init: function () {
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            show();
        },
        showUpdateModal: function () {
            showUpdateModal();
        },
        //submit: function () {
        //    submit();
        //}
    };
}();
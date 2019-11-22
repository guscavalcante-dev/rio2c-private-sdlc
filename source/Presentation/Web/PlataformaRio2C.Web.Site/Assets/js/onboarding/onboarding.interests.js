// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 09-13-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-22-2019
// ***********************************************************************
// <copyright file="onboarding.interests.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var OnboardingInterests = function () {

    //var modalId = '#UpdatePlayerExecutiveModal';
    var formId = '#PlayerInterestssDataForm';

    // Enable form validation ---------------------------------------------------------------------
    //var enableFormValidation = function () {
    //    MyRio2cCommon.enableFormValidation({ formIdOrClass: formId, enableHiddenInputsValidation: true, enableMaxlength: true });
    //};

    // Enable plugins -----------------------------------------------------------------------------
    var enablePlugins = function () {
        //MyRio2cCommon.enableCkEditor({ idOrClass: '.ckeditor-rio2c-restrictions', maxCharCount: 270 });
        MyRio2cCommon.enableAtLeastOnCheckboxByNameValidation(formId);

        // Enable additional info textbox
        if (typeof (MyRio2cCommonAdditionalInfo) !== 'undefined') {
            MyRio2cCommonAdditionalInfo.init();
        }
    };

    // Form submit --------------------------------------------------------------------------------
    var submit = function () {
        var validator = $(formId).validate();
        var formValidation = $(formId).valid();
        //var interestsValidation = MyRio2cCommon.validateRequireOneGroup();

        if (formValidation/* && interestsValidation*/) {
            MyRio2cCommon.submitForm(formId);
        }
        else {
            validator.focusInvalid();
        }
    };

    return {
        init: function () {
            enablePlugins();
        },
        validateInterests: function () {
            return validateInterests();
        },
        submit: function () {
            submit();
        }
    };
}();
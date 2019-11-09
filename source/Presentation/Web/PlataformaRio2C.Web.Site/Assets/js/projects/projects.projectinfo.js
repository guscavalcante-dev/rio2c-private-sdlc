// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 11-08-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-08-2019
// ***********************************************************************
// <copyright file="projects.projectinfo.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var ProjectsProjectInfo = function () {

    //var modalId = '#UpdatePlayerExecutiveModal';
    var formId = '#CreateProjectForm';

    // Enable form validation ---------------------------------------------------------------------
    //var enableFormValidation = function () {
    //    MyRio2cCommon.enableFormValidation({ formIdOrClass: formId, enableHiddenInputsValidation: true, enableMaxlength: true });
    //};

    // Enable plugins -----------------------------------------------------------------------------
    var enablePlugins = function () {
        //MyRio2cCommon.enableCkEditor({ idOrClass: '.ckeditor-rio2c-restrictions', maxCharCount: 270 });
        MyRio2cCommon.enableAtLeastOnCheckboxByNameValidation(formId);
    };

    // Form submit --------------------------------------------------------------------------------
    var submit = function () {
        var validator = $(formId).validate();
        var formValidation = $(formId).valid();
        var interestsValidation = MyRio2cCommon.validateRequireOneGroup();

        if (formValidation && interestsValidation) {
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
        submit: function () {
            submit();
        }
    };
}();
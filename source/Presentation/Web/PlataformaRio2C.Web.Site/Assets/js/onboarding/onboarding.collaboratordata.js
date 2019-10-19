// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 09-06-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-18-2019
// ***********************************************************************
// <copyright file="onboarding.collaboratordata.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var OnboardingCollaboratorData = function () {

    //var modalId = '#UpdatePlayerExecutiveModal';
    var formId = '#CollaboratorDataForm';

    // Enable form validation ---------------------------------------------------------------------
    //var enableFormValidation = function () {
    //    MyRio2cCommon.enableFormValidation({
    //        formIdOrClass: formId,
    //        enableHiddenInputsValidation: true
    //    });
    //};

    // Enable plugins -----------------------------------------------------------------------------
    var enablePlugins = function () {
        if (typeof (MyRio2cPublicEmail) !== 'undefined') {
            MyRio2cPublicEmail.init();
        }

        MyRio2cCropper.init({ formIdOrClass: formId });
        MyRio2cCommon.enableSelect2({ inputIdOrClass: formId + ' .enable-select2' });
        AddressesForm.init();
        //MyRio2cCommon.enableCkEditor({ idOrClass: '.ckeditor-rio2c-minibio', maxCharCount: 710 });
    };

    return {
        init: function() {
            enablePlugins();
        }
    };
}();
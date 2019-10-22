// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 09-06-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-21-2019
// ***********************************************************************
// <copyright file="onboarding.playerinfo.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var OnboardingPlayerInfo = function () {

    //var modalId = '#UpdatePlayerExecutiveModal';
    var formId = '#OrganizationDataForm';

    // Enable form validation ---------------------------------------------------------------------
    //var enableFormValidation = function () {
    //    MyRio2cCommon.enableFormValidation({ formIdOrClass: formId, enableHiddenInputsValidation: true, enableMaxlength: true });
    //};

    // Enable plugins -----------------------------------------------------------------------------
    var enablePlugins = function () {
        MyRio2cCropper.init({ formIdOrClass: formId });
        MyRio2cCommon.enableSelect2({ inputIdOrClass: formId + ' .enable-select2' });
        AddressesForm.init();
        //MyRio2cCommon.enableCkEditor({ idOrClass: '.ckeditor-rio2c', maxCharCount: 710 });
        MyRio2cCommon.enableAtLeastOnCheckboxByNameValidation(formId);

        // Enable activity additional info textbox
        if (typeof (MyRio2cCommonActivity) !== 'undefined') {
            MyRio2cCommonActivity.init();
        }
    };

    return {
        init: function() {
            enablePlugins();
        }
    };
}();
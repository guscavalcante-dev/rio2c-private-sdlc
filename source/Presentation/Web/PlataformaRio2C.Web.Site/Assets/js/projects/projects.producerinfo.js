// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 10-29-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-08-2019
// ***********************************************************************
// <copyright file="projects.producerinfo.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var ProjectsProducerInfo = function () {

    //var modalId = '#UpdatePlayerExecutiveModal';
    var formId = '#OrganizationDataForm';

    // Enable form validation ---------------------------------------------------------------------
    //var enableFormValidation = function () {
    //    MyRio2cCommon.enableFormValidation({ formIdOrClass: formId, enableHiddenInputsValidation: true, enableMaxlength: true });
    //};

    // Enable plugins -----------------------------------------------------------------------------
    var enablePlugins = function () {
        if (typeof (MyRio2cCropper) !== 'undefined') {
            MyRio2cCropper.init({ formIdOrClass: formId });
        }

        if (typeof (AddressesForm) !== 'undefined') {
            AddressesForm.init();
        }

        if (typeof (CompanyInfoAutocomplete) !== 'undefined') {
            CompanyInfoAutocomplete.init('/Companies/ShowProducerFilledForm', enablePlugins);
        }

        MyRio2cCommon.enableSelect2({ inputIdOrClass: formId + ' .enable-select2' });
        MyRio2cCommon.enableAtLeastOnCheckboxByNameValidation(formId);

        // Enable activity additional info textbox
        if (typeof (MyRio2cCommonActivity) !== 'undefined') {
            MyRio2cCommonActivity.init();
        }

        MyRio2cCommon.enableFormValidation({ formIdOrClass: formId, enableHiddenInputsValidation: true, enableMaxlength: true });
    };

    return {
        init: function() {
            enablePlugins();
        }
    };
}();
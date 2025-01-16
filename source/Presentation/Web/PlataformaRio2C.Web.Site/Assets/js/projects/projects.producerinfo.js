// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 10-29-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-22-2019
// ***********************************************************************
// <copyright file="projects.producerinfo.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var ProjectsProducerInfo = function () {

    //var modalId = '#UpdatePlayerExecutiveModal';
    var formId = '#OrganizationDataForm';
    var countryUid = '#CountryUid';
    var isCompanyNumberRequired = '#IsCompanyNumberRequired';
    var document = '#Document';

    // Enable form validation ---------------------------------------------------------------------
    //var enableFormValidation = function () {
    //    MyRio2cCommon.enableFormValidation({ formIdOrClass: formId, enableHiddenInputsValidation: true, enableMaxlength: true });
    //};


    // Enable country change to prevent document number if not brazil event -----------------------------------------------------------------------
    var enableCountryChangePreventDocumentNumberIfNotBrazilEvent = function () {
        var countryUidElement = $(countryUid);
        var isCompanyNumberRequiredElement = $(isCompanyNumberRequired);
        var documentElement = $(document);

        countryUidElement.on('change', function () {
            //Brazil UID.
            if (countryUidElement.val() == 'a659270e-f221-40e8-aaf6-fe5db29d8ce9') {
                isCompanyNumberRequiredElement.val(true);
                documentElement.val('');
                documentElement.closest('.form-group').show();
            }
            else {
                isCompanyNumberRequiredElement.val(false);
                documentElement.val('');
                documentElement.closest('.form-group').hide();
            }
        });
    };

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

        // Enable additional info textbox
        if (typeof (MyRio2cCommonAdditionalInfo) !== 'undefined') {
            MyRio2cCommonAdditionalInfo.init();
        }

        MyRio2cCommon.enableFormValidation({ formIdOrClass: formId, enableHiddenInputsValidation: true, enableMaxlength: true });
    };

    return {
        init: function() {
            enablePlugins();
            enableCountryChangePreventDocumentNumberIfNotBrazilEvent();
        }
    };
}();
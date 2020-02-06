// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 09-06-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-21-2019
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
    //    MyRio2cCommon.enableFormValidation({ formIdOrClass: formId, enableHiddenInputsValidation: true, enableMaxlength: true });
    //};

    // Enable plugins -----------------------------------------------------------------------------
    var enablePlugins = function () {
        if (typeof (MyRio2cPublicEmail) !== 'undefined') {
            MyRio2cPublicEmail.init();
        }

        MyRio2cCropper.init({ formIdOrClass: formId });
        MyRio2cCommon.enableSelect2({ inputIdOrClass: formId + ' .enable-select2' });                    
        MyRio2cCommon.enableDatePicker({ inputIdOrClass: formId + ' .enable-datepicker' });
        enableDropdownChangeEvent("CollaboratorGenderUid");
        enableDropdownChangeEvent("CollaboratorRoleUid");
        enableDropdownChangeEvent("CollaboratorIndustryUid");
        enableCheckboxChangeEvent("HasAnySpecialNeeds");
        enableCheckboxChangeEvent("HaveYouBeenToRio2CBefore");
        AddressesForm.init();
    };

    var changeIsRequired = function (originDropdownIdOrClass) {
        var element = $(originDropdownIdOrClass);
        if (typeof (element) === 'undefined') {
            return;
        }

        var isCompanyNumberRequired = element.find(":selected").data("companynumber-required");
        if (!MyRio2cCommon.isNullOrEmpty(isCompanyNumberRequired)) {
            $('#IsCompanyNumberRequired').val(isCompanyNumberRequired);
        }
        else {
            $('#IsCompanyNumberRequired').val('False');
        }
    };
    
    return {
        init: function() {
            enablePlugins();
        },
        enableGenderChangeEvent: function () {
            enableGenderChangeEvent();
        },
        changeIsRequired: function (originDropdownIdOrClass) {
            changeIsRequired(originDropdownIdOrClass);
        },
    };
}();

    //// Enable change events -----------------------------------------------------------------------
    //var enableCountryChangeEvent = function () {
    //    countryUidElement.not('.change-event-enabled').on('change', function () {
    //        toggleState(true);
    //        toggleCity(true);
    //        enableStateSelect2(true);
    //        enableZipCodeMask();

    //        if (typeof (MyRio2cCompanyDocument) !== 'undefined') {
    //            MyRio2cCompanyDocument.enableCompanyNumberMask(countryUid, '#Document');
    //            MyRio2cCompanyDocument.changeIsRequired(countryUid);
    //        }
    //    });
    //    countryUidElement.addClass('change-event-enabled');
    //};
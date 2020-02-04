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
    
    // Enable change events -----------------------------------------------------------------------
    var enableCheckboxChangeEvent = function (elementId) {
        var element = $('#' + elementId);

        element.not('.change-event-enabled').on('click', function () {            
            if (element.prop('checked')) {
                $("[data-additionalinfo='"+ element.attr("id") +"']").removeClass('d-none');
            }
            else {
                $("[data-additionalinfo='"+element.attr("id")+"']").addClass('d-none');
            }
        });

        element.addClass('change-event-enabled');
    };

    var enableDropdownChangeEvent = function (elementId) {
        var element = $('#' + elementId);

        element.not('.change-event-enabled').on('change', function () {            
            if (element.find(':selected').data('aditionalinfo') === "True") {
                $("[data-additionalinfo='"+ element.attr("id") +"']").removeClass('d-none');
            }
            else {
                $("[data-additionalinfo='"+element.attr("id")+"']").addClass('d-none');
            }
        });

        element.addClass('change-event-enabled');
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
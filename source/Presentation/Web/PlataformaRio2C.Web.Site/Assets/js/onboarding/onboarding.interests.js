// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 09-13-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-08-2019
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
    };

    // Custom validation --------------------------------------------------------------------------
    var validateInterests = function () {
        var isValid = true;

        $(".require-one-group").each(function (index, element) {

            var dataId = $(element).data("id");
            if (MyRio2cCommon.isNullOrEmpty(dataId)) {
                return;
            }

            if ($('[data-id="' + dataId + '"].require-one-item:checked').length > 0 === false) {
                $('[data-valmsg-for="' + dataId + '"]').html('<span for="'+ dataId + '" generated="true" class="">' + labels.selectAtLeastOneOption + '</span>');
                $('[data-valmsg-for="' + dataId + '"]').removeClass('field-validation-valid');
                $('[data-valmsg-for="' + dataId + '"]').addClass('field-validation-error');

                isValid = false;
            }
            else {
                $('[data-valmsg-for="' + dataId + '"]').html('');
                $('[data-valmsg-for="' + dataId + '"]').addClass('field-validation-valid');
                $('[data-valmsg-for="' + dataId + '"]').removeClass('field-validation-error');
            }
        });

        // Enable checkbox change on first submit
        $(".require-one-item").not('.change-event-enabled').on('change', function () {
            validateInterests();
        });

        $(".require-one-item").addClass('change-event-enabled');

        return isValid;
    };

    // Form submit --------------------------------------------------------------------------------
    var submit = function () {
        if (validateInterests()) {
            MyRio2cCommon.submitForm(formId);
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
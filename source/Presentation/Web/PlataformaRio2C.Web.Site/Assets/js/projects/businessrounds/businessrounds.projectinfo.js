// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 11-08-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-22-2019
// ***********************************************************************
// <copyright file="businessrounds.projectinfo.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var ProjectsProjectInfo = function () {

    //var modalId = '#UpdatePlayerExecutiveModal';
    var formId = '#CreateProjectForm';

    // Enable plugins -----------------------------------------------------------------------------
    var enablePlugins = function () {
        MyRio2cInputMask.enableMask('#TotalPlayingTime', '99:99:99');
        MyRio2cInputMask.enableMask('#EachEpisodePlayingTime', '99:99:99');
        MyRio2cCommon.enableAtLeastOnCheckboxByNameValidation(formId);
        MyRio2cCommon.enableYesNoRadioEvent("HasAnyTypeOfFinancing");

        // Enable additional info textbox
        if (typeof (MyRio2cCommonAdditionalInfo) !== 'undefined') {
            MyRio2cCommonAdditionalInfo.init();
            MyRio2cCommon.enableSelect2({ inputIdOrClass: formId + ' .enable-select2', allowClear: true });
        }
    };

    //// Form submit --------------------------------------------------------------------------------
    //var submit = function () {
    //    var validator = $(formId).validate();
    //    var formValidation = $(formId).valid();
    //    //var interestsValidation = MyRio2cCommon.validateRequireOneGroup();

    //    if (formValidation/* && interestsValidation*/) {
    //        MyRio2cCommon.submitForm(formId);
    //    }
    //    else {
    //        validator.focusInvalid();
    //    }
    //};

    return {
        init: function () {
            enablePlugins();
        },
        //submit: function () {
        //    submit();
        //}
    };
}();

var PlayersCategoriesValidation = function () {

    //function to control players categories field in project info form
    var handleCheckboxSelection = function () {
        // all checkboxes
        var checkboxes = $('input[name="PlayerCategoriesUids"]');
        var textFieldWrapper = $('#PlayerCategoriesThatHaveOrHadContract').closest('.form-group');
        var textField = $('#PlayerCategoriesThatHaveOrHadContract');

        checkboxes.on('change', function () {
            var anyChecked = checkboxes.is(':checked');
            $('#IsPlayersCategoriesDiscursiveRequired').val(anyChecked);

            //hide or show control
            if (anyChecked) {
                textFieldWrapper.show();
            } else {
                textFieldWrapper.hide();
                textField.val('');
            }
        });

        if (!checkboxes.is(':checked')) {
            textFieldWrapper.hide();
        }
    };

    return {
        init: function () {
            handleCheckboxSelection();
        }
    };
}();

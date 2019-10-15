// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 10-14-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-14-2019
// ***********************************************************************
// <copyright file="onboarding.companyinfo.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var OnboardingCompanyInfo = function () {

    //var modalId = '#UpdatePlayerExecutiveModal';
    var formId = '#CompanyInfoForm';
    var organizationUidId = '#OrganizationUid';
    var companyNameId = '#CompanyName';
    var tradeNameId = '#TradeName';
    var documentId = '#Document';

    // Enable form validation ---------------------------------------------------------------------
    //var enableFormValidation = function () {
    //    MyRio2cCommon.enableFormValidation({
    //        formIdOrClass: formId,
    //        enableHiddenInputsValidation: true
    //    });
    //};

    // Update form values -------------------------------------------------------------------------
    var updateFormValues = function (companyJson) {
        if (MyRio2cCommon.isNullOrEmpty(companyJson)) {
            return;
        }

        if (!MyRio2cCommon.isNullOrEmpty(companyJson.uid)) {
            $(organizationUidId).val(companyJson.uid);
        }

        if (!MyRio2cCommon.isNullOrEmpty(companyJson.tradeName)) {
            $(tradeNameId).val(companyJson.tradeName);
        }

        if (!MyRio2cCommon.isNullOrEmpty(companyJson.companyNumber)) {
            $(documentId).val(companyJson.companyNumber);
        }
    };

    // Enable company name autocomplete -----------------------------------------------------------
    var enableCompanyNameAutocomplete = function () {
        var companyNameElement = $(companyNameId);

        if (companyNameElement.length <= 0) {
            return;
        }

        companyNameElement.autocomplete({
            serviceUrl: '/api/v1.0/organizations',
            minChars: 3,
            transformResult: function (data) {
                MyRio2cCommon.handleAjaxReturn({
                    data: data,
                    // Success
                    onSuccess: function () {
                        return {
                            suggestions: $.map(data.myData, function (dataItem) {
                                return { value: dataItem.companyName, data: dataItem };
                            })
                        };
                    },
                    // Error
                    onError: function () {
                    }
                });

                var json = jQuery.parseJSON(data);

                return {
                    suggestions: $.map(json.organizations, function (dataItem) {
                        return { value: dataItem.companyName, data: dataItem };
                    })
                };
            },
            onSelect: function (suggestion) {
                updateFormValues(suggestion.data);
                console.log(suggestion);
                //$('#selction-ajax').html('You selected: ' + suggestion.value + ', ' + suggestion.data);
            },
            onHint: function (hint) {
                console.log(hint);
                //$('#autocomplete-ajax-x').val(hint);
            },
            onInvalidateSelection: function () {
                console.log('You selected: none');
                //$('#selction-ajax').html('You selected: none');
            }
            //source: function (request, response) {
            //    $.ajax({
            //        url: "http://gd.geobytes.com/AutoCompleteCity",
            //        dataType: "jsonp",
            //        data: {
            //            q: request.term
            //        },
            //        success: function (data) {
            //            response(data);
            //        }
            //    });
            //},
            //minLength: 3,
            //select: function (event, ui) {
            //    log(ui.item ?
            //        "Selected: " + ui.item.label :
            //        "Nothing selected, input was " + this.value);
            //},
            //open: function () {
            //    $(this).removeClass("ui-corner-all").addClass("ui-corner-top");
            //},
            //close: function () {
            //    $(this).removeClass("ui-corner-top").addClass("ui-corner-all");
            //}
        });
    };

    // Enable plugins -----------------------------------------------------------------------------
    var enablePlugins = function () {
        MyRio2cCropper.init({ formIdOrClass: formId });
        MyRio2cCommon.enableSelect2({ inputIdOrClass: formId + ' .enable-select2' });
        AddressesForm.init();
        MyRio2cCommon.enableCkEditor({ idOrClass: '.ckeditor-rio2c', maxCharCount: 710 });
        MyRio2cCommon.enableAtLeastOnCheckboxByNameValidation(formId);
        //enableCompanyNameAutocomplete();

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
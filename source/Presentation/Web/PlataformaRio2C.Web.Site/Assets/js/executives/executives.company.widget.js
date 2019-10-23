// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 10-09-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-09-2019
// ***********************************************************************
// <copyright file="executives.company.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var ExecutivesCompanyWidget = function () {

    var widgetElementId = '#ExecutiveCompanyWidget';
    var widgetElement = $(widgetElementId);

    var createModalId = '#CreateCompanyInfoModal';
    var createFormId = '#CreateCompanyInfoForm';
   // var countryUid = '#CountryUid';

    //var organizationUidId = '#OrganizationUid';
    //var companyNameId = '#CompanyName';
    //var tradeNameId = '#TradeName';
    //var companyNumberId = '#Document';


    // Show ---------------------------------------------------------------------------------------
    var enableShowPlugins = function () {
        KTApp.initTooltips();
    };

    var show = function () {
        if (widgetElement.length <= 0) {
            return;
        }

        var jsonParameters = new Object();
        jsonParameters.collaboratorUid = $('#AggregateId').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Executives/ShowCompanyWidget'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    enableShowPlugins();
                },
                // Error
                onError: function () {
                }
            });
        })
            .fail(function () {
            })
            .always(function () {
                MyRio2cCommon.unblock({ idOrClass: widgetElementId });
            });
    };

    var enableAjaxForm = function () {
        MyRio2cCommon.enableAjaxForm({
            idOrClass: createFormId,
            onSuccess: function (data) {
                $(createModalId).modal('hide');

                if (typeof (ExecutivesCompanyWidget) !== 'undefined') {
                    ExecutivesCompanyWidget.init();
                }
            },
            onError: function (data) {
                if (MyRio2cCommon.hasProperty(data, 'pages')) {
                    enableUpdatePlugins();
                }
            }
        });
    };


    //// Enable company name autocomplete -----------------------------------------------------------
    //var enableCompanyNameAutocomplete = function () {
    //    var companyNameElement = $(companyNameId);

    //    if (companyNameElement.length <= 0) {
    //        return;
    //    }

    //    companyNameElement.autocomplete({
    //        serviceUrl: '/api/v1.0/organizations',
    //        paramName: 'companyName',
    //        minChars: 3,
    //        deferRequestBy: 500,
    //        //noCache: true,
    //        transformResult: function (data) {
    //            var json = jQuery.parseJSON(data);

    //            return MyRio2cCommon.handleAjaxReturn({
    //                data: json,
    //                // Success
    //                onSuccess: function () {
    //                    return {
    //                        suggestions: $.map(json.organizations, function (dataItem) {
    //                            if (!MyRio2cCommon.isNullOrEmpty(dataItem.companyName)) {
    //                                return { value: dataItem.companyName, data: dataItem };
    //                            }
    //                        })
    //                    };
    //                },
    //                // Error
    //                onError: function () {
    //                }
    //            });
    //        },
    //        onSelect: function (suggestion) {
    //            updateOrganizationUid(suggestion.data.uid, 'companyName');
    //        },
    //        onHint: function (hint) {
    //        },
    //        onInvalidateSelection: function () {
    //            updateOrganizationUid('', 'companyName');
    //        }
    //    });
    //};

    //// Enable trade name autocomplete -------------------------------------------------------------
    //var enableTradeNameAutocomplete = function () {
    //    var tradeNameElement = $(tradeNameId);

    //    if (tradeNameElement.length <= 0) {
    //        return;
    //    }

    //    tradeNameElement.autocomplete({
    //        serviceUrl: '/api/v1.0/organizations',
    //        paramName: 'tradeName',
    //        minChars: 3,
    //        deferRequestBy: 500,
    //        //noCache: true,
    //        transformResult: function (data) {
    //            var json = jQuery.parseJSON(data);

    //            return MyRio2cCommon.handleAjaxReturn({
    //                data: json,
    //                // Success
    //                onSuccess: function () {
    //                    return {
    //                        suggestions: $.map(json.organizations, function (dataItem) {
    //                            if (!MyRio2cCommon.isNullOrEmpty(dataItem.tradeName)) {
    //                                return { value: dataItem.tradeName, data: dataItem };
    //                            }
    //                        })
    //                    };
    //                },
    //                // Error
    //                onError: function () {
    //                }
    //            });
    //        },
    //        onSelect: function (suggestion) {
    //            updateOrganizationUid(suggestion.data.uid, 'tradeName');
    //        },
    //        onHint: function (hint) {
    //        },
    //        onInvalidateSelection: function () {
    //            updateOrganizationUid('', 'tradeName');
    //        }
    //    });
    //};


    //// Enable company number autocomplete ---------------------------------------------------------
    //var enableCompanyNumberAutocomplete = function () {
    //    var companyNumberElement = $(companyNumberId);

    //    if (companyNumberElement.length <= 0) {
    //        return;
    //    }

    //    companyNumberElement.autocomplete({
    //        serviceUrl: '/api/v1.0/organizations',
    //        paramName: 'companyNumber',
    //        minChars: 3,
    //        preventBadQueries: false,
    //        deferRequestBy: 500,
    //        //noCache: true,
    //        transformResult: function (data) {
    //            var json = jQuery.parseJSON(data);

    //            return MyRio2cCommon.handleAjaxReturn({
    //                data: json,
    //                // Success
    //                onSuccess: function () {
    //                    return {
    //                        suggestions: $.map(json.organizations, function (dataItem) {
    //                            if (!MyRio2cCommon.isNullOrEmpty(dataItem.companyNumber)) {
    //                                return { value: dataItem.companyNumber, data: dataItem };
    //                            }
    //                        })
    //                    };
    //                },
    //                // Error
    //                onError: function () {
    //                }
    //            });
    //        },
    //        onSelect: function (suggestion) {
    //            updateOrganizationUid(suggestion.data.uid, 'companyNumber');
    //        },
    //        onHint: function (hint) {
    //        },
    //        onInvalidateSelection: function () {
    //            updateOrganizationUid('', 'companyNumber');
    //        }
    //    });
    //};

    var enableUpdatePlugins = function () {
        MyRio2cCropper.init({ formIdOrClass: createFormId });
        //MyRio2cCommon.enableSelect2({ inputIdOrClass: createFormId + ' .enable-select2' });
        AddressesForm.init();
        MyRio2cCommon.enableCkEditor({ idOrClass: '.ckeditor-rio2c', maxCharCount: 710 });
        //MyRio2cCommon.enableAtLeastOnCheckboxByNameValidation(createFormId);
        //MyRio2cCompanyDocument.enableCompanyNumberMask(countryUid, '#Document');
        MyRio2cCommon.enableFormValidation({ formIdOrClass: createFormId, enableHiddenInputsValidation: true });
       
        enableAjaxForm();
        
        //enableCompanyNameAutocomplete();
        //enableTradeNameAutocomplete();
        //enableCompanyNumberAutocomplete();

        // Enable activity additional info textbox
        if (typeof (MyRio2cCommonActivity) !== 'undefined') {
            MyRio2cCommonActivity.init();
        }


    };

    var showCreateModal = function () {
        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Executives/ShowCreateCompanyInfoModal'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    enableUpdatePlugins();
                    $(createModalId).modal();
                },
                // Error
                onError: function () {
                }
            });
        })
            .fail(function () {
            })
            .always(function () {
                MyRio2cCommon.unblock();
            });
    };


    return {
        init: function () {
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            show();
        },
        showCreateModal: function () {
            showCreateModal();
        }
    };
}();
// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 10-23-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-23-2019
// ***********************************************************************
// <copyright file="companyinfo.autocomplete.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var CompanyInfoAutocomplete = function () {

    var organizationUidId = '#OrganizationUid';
    var companyNameId = '#CompanyName';
    var tradeNameId = '#TradeName';
    var companyNumberId = '#Document';
    var showFormUrl = '/Companies/ShowTicketBuyerFilledForm';
    var formPluginsCallback = null;

    var spinnerClass = 'kt-spinner kt-spinner--sm kt-spinner--brand kt-spinner--right kt-spinner--input';

    // Init elements ------------------------------------------------------------------------------
    var initElements = function (pShowFormUrl, pFormPluginsCallback) {
        if (!MyRio2cCommon.isNullOrEmpty(pShowFormUrl)) {
            showFormUrl = pShowFormUrl;
        }

        if (!MyRio2cCommon.isNullOrEmpty(pFormPluginsCallback)) {
            formPluginsCallback = pFormPluginsCallback;
        }
    };

    // Show form ----------------------------------------------------------------------------------
    var showFilledForm = function (companyName, tradeName, companyNumber) {
        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();
        jsonParameters.organizationUid = $(organizationUidId).val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition(showFormUrl), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {

                    if (!MyRio2cCommon.isNullOrEmpty(companyName) && MyRio2cCommon.isNullOrEmpty($(companyNameId).val())) {
                        $(companyNameId).val(companyName);
                        $(companyNameId).focus();
                    }

                    if (!MyRio2cCommon.isNullOrEmpty(tradeName) && MyRio2cCommon.isNullOrEmpty($(tradeNameId).val())) {
                        $(tradeNameId).val(tradeName);
                        $(tradeNameId).focus();
                    }

                    if (!MyRio2cCommon.isNullOrEmpty(companyNumber) && MyRio2cCommon.isNullOrEmpty($(companyNumberId).val())) {
                        $(companyNumberId).val(companyNumber);
                        $(companyNumberId).focus();
                    }

                    //enablePlugins();

                    if (!MyRio2cCommon.isNullOrEmpty(formPluginsCallback)) {
                        formPluginsCallback();
                    }
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

    // Update form values -------------------------------------------------------------------------
    var updateOrganizationUid = function (organizationUid, changedElementName) {

        var currentOrganizationUid = $(organizationUidId).val();
        if (currentOrganizationUid === organizationUid) {
            return;
        }

        if (MyRio2cCommon.isNullOrEmpty(organizationUid)) {
            $('.autocomplete-suggestions').remove();
        }

        $(organizationUidId).val(organizationUid);

        if (changedElementName === 'companyName') {
            showFilledForm($(companyNameId).val(), null, null);
        }
        else if (changedElementName === 'tradeName') {
            showFilledForm(null, $(tradeNameId).val(), null);
        }
        else if (changedElementName === 'companyNumber') {
            showFilledForm(null, null, $(companyNumberId).val());
        }
    };

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
    //        onSearchStart: function (query) {
    //            tradeNameElement.parent('div.spinner-container').addClass(spinnerClass);
    //        },
    //        onSearchComplete: function (query, suggestions) {
    //            tradeNameElement.parent('div.spinner-container').removeClass(spinnerClass);
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
    //        onSearchStart: function (query) {
    //            companyNameElement.parent('div.spinner-container').addClass(spinnerClass);
    //        },
    //        onSearchComplete: function (query, suggestions) {
    //            companyNameElement.parent('div.spinner-container').removeClass(spinnerClass);
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

    // Enable company number autocomplete ---------------------------------------------------------
    var enableCompanyNumberAutocomplete = function () {
        var companyNumberElement = $(companyNumberId);

        if (companyNumberElement.length <= 0) {
            return;
        }

        companyNumberElement.autocomplete({
            serviceUrl: '/api/v1.0/organizations',
            paramName: 'companyNumber',
            minChars: 3,
            preventBadQueries: false,
            deferRequestBy: 500,
            //noCache: true,
            transformResult: function (data) {
                var json = jQuery.parseJSON(data);

                return MyRio2cCommon.handleAjaxReturn({
                    data: json,
                    // Success
                    onSuccess: function () {
                        return {
                            suggestions: $.map(json.organizations, function (dataItem) {
                                if (!MyRio2cCommon.isNullOrEmpty(dataItem.companyNumber)) {
                                    return { value: dataItem.companyNumber, data: dataItem };
                                }
                            })
                        };
                    },
                    // Error
                    onError: function () {
                    }
                });
            },
            onSearchStart: function (query) {
                companyNumberElement.parent('div.spinner-container').addClass(spinnerClass);
            },
            onSearchComplete: function (query, suggestions) {
                companyNumberElement.parent('div.spinner-container').removeClass(spinnerClass);
            },
            onSelect: function (suggestion) {
                updateOrganizationUid(suggestion.data.uid, 'companyNumber');
            },
            onHint: function (hint) {
            },
            onInvalidateSelection: function () {
                updateOrganizationUid('', 'companyNumber');
            }
        });
    };

    // Enable plugins -----------------------------------------------------------------------------
    var enablePlugins = function () {
        //enableTradeNameAutocomplete();
        //enableCompanyNameAutocomplete();
        enableCompanyNumberAutocomplete();
    };

    return {
        init: function (pShowFormUrl, pFormPluginsCallback) {
            initElements(pShowFormUrl, pFormPluginsCallback);
            enablePlugins();
        }
    };
}();
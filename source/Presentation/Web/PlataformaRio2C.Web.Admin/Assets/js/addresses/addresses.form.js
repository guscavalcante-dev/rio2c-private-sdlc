// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 08-23-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-23-2019
// ***********************************************************************
// <copyright file="addresses.form.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var AddressesForm = function (formId) {

    var countryId = '#Address_CountryUid';
    var stateId = '#Address_StateUid';
    var cityId = '#Address_CityUid';

    var countryIdElement;
    var stateIdElement;
    var cityIdElement;

    // Init elements ------------------------------------------------------------------------------
    var initElements = function () {
        countryIdElement = $(countryId);
        stateIdElement = $(stateId);
        cityIdElement = $(cityId);
    };

    // Country select2 ----------------------------------------------------------------------------
    var enableCountrySelect2 = function () {
        countryIdElement.select2({
            width: '100%',
            placeholder: labels.selectPlaceholder,
            triggerChange: true,
            allowClear: true
        });
    };

    // State select2 ------------------------------------------------------------------------------
    var emptyStateSelect2 = function () {
        stateIdElement.val('').trigger('change');

        stateIdElement.select2({
            width: '100%',
            placeholder: labels.selectPlaceholder,
            data: []
        });

        stateIdElement.empty();
        stateIdElement.prop("disabled", true);
    };

    var enableStateSelect2 = function () {
        var countryUid = countryIdElement.val();

        if (MyRio2cCommon.isNullOrEmpty(countryUid)) {
            emptyStateSelect2();
        }
        else {
            var jsonParameters = new Object();
            jsonParameters.countryUid = countryUid;

            $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/States/FindAllByCountryUid'), jsonParameters, function (data) {
                MyRio2cCommon.handleAjaxReturn({
                    data: data,
                    // Success
                    onSuccess: function () {
                        if (data.states.length <= 0) {
                            emptyStateSelect2();
                        }

                        var statesData = new Array();

                        // Placeholder
                        stateObject = new Object();
                        stateObject.id = '';
                        stateObject.text = labels.selectPlaceholder;
                        statesData.push(stateObject);

                        for (var i in data.states) {
                            if (data.states.hasOwnProperty(i)) {
                                stateObject = new Object();
                                stateObject.id = data.states[i].Uid;
                                stateObject.text = data.states[i].Name;

                                statesData.push(stateObject);
                            }
                        }

                        stateIdElement.val('').trigger('change');

                        stateIdElement.empty().select2({
                            placeholder: labels.selectPlaceholder,
                            triggerChange: true,
                            allowClear: true,
                            data: statesData
                        });

                        stateIdElement.prop("disabled", false);
                    },
                    // Error
                    onError: function () {
                        emptyStateSelect2();
                    }
                });
            })
            .fail(function () {
                emptyStateSelect2();
            })
            .always(function () {
                MyRio2cCommon.unblock();
            });
        }
    };

    // City select2 ------------------------------------------------------------------------------
    var emptyCitySelect2 = function () {
        cityIdElement.val('').trigger('change');

        cityIdElement.select2({
            width: '100%',
            placeholder: labels.selectPlaceholder,
            data: []
        });

        cityIdElement.empty();
        cityIdElement.prop("disabled", true);
    };

    var enableCitySelect2 = function () {
        var stateUid = stateIdElement.val();

        if (MyRio2cCommon.isNullOrEmpty(stateUid)) {
            emptyCitySelect2();
        }
        else {
            var jsonParameters = new Object();
            jsonParameters.stateUid = stateUid;

            $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Cities/FindAllByStateUid'), jsonParameters, function (data) {
                MyRio2cCommon.handleAjaxReturn({
                    data: data,
                    // Success
                    onSuccess: function () {
                        if (data.cities.length <= 0) {
                            emptyCitySelect2();
                        }

                        var citiesData = new Array();

                        // Placeholder
                        cityObject = new Object();
                        cityObject.id = '';
                        cityObject.text = labels.selectPlaceholder;
                        citiesData.push(cityObject);

                        for (var i in data.cities) {
                            if (data.cities.hasOwnProperty(i)) {
                                cityObject= new Object();
                                cityObject.id = data.cities[i].Uid;
                                cityObject.text = data.cities[i].Name;
                                citiesData.push(cityObject);
                            }
                        }

                        cityIdElement.val('').trigger('change');

                        cityIdElement.empty().select2({
                            placeholder: labels.selectPlaceholder,
                            triggerChange: true,
                            allowClear: true,
                            data: citiesData
                        });

                        cityIdElement.prop("disabled", false);
                    },
                    // Error
                    onError: function () {
                        emptyCitySelect2();
                    }
                });
            })
            .fail(function () {
                emptyCitySelect2();
            })
            .always(function () {
                MyRio2cCommon.unblock();
            });
        }
    };

    var enableSelect2 = function () {
        enableCountrySelect2();
        enableStateSelect2();
        enableCitySelect2();
    };

    // Enable change events -----------------------------------------------------------------------
    var enableCountryChangeEvent = function() {
        countryIdElement.not('.change-event-enabled').on('change', function() {
            enableStateSelect2();
        });

        countryIdElement.addClass('change-event-enabled');
    };

    var enableStateChangeEvent = function () {
        stateIdElement.not('.change-event-enabled').on('change', function () {
            enableCitySelect2();
        });

        stateIdElement.addClass('change-event-enabled');
    };

    var enableCityChangeEvent = function () {
        cityIdElement.not('.change-event-enabled').on('change', function () {
        });

        cityIdElement.addClass('change-event-enabled');
    };

    var enableChangeEvents = function () {
        enableCountryChangeEvent();
        enableStateChangeEvent();
        enableCityChangeEvent();
    };

    // Enable plugins -----------------------------------------------------------------------------
    var enablePlugins = function () {
        enableSelect2();
        enableChangeEvents();
    };

    return {
        init: function () {
            initElements();
            enablePlugins();
        }
    };
}();
// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 08-23-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-17-2019
// ***********************************************************************
// <copyright file="addresses.form.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var AddressesForm = function () {

    var countryDataId = 'Country';
    var stateDataId = 'State';
    var cityDataId = 'City';

    var countryUid = '#CountryUid';
    var initialstateUid = '#Address_InitialStateUid';
    var stateUid = '#Address_StateUid';
    var stateName = '#Address_StateName';
    var initialCityUid = '#Address_InitialCityUid';
    var cityUid = '#Address_CityUid';
    var cityName = '#Address_CityName';
    var addressZipCode = "#Address_AddressZipCode";

    var countryUidElement;
    var initialStateUidElement;
    var stateUidElement;
    var stateNameElement;
    var initialCityUidElement;
    var cityUidElement;
    var cityNameElement;

    var userInterfaceLanguage = 'en';

    // Init elements ------------------------------------------------------------------------------
    var initElements = function () {
        countryUidElement = $(countryUid);
        initialStateUidElement = $(initialstateUid);
        stateUidElement = $(stateUid);
        stateNameElement = $(stateName);
        initialCityUidElement = $(initialCityUid);
        cityUidElement = $(cityUid);
        cityNameElement = $(cityName);

        var globalVariables = MyRio2cCommon.getGlobalVariables();
        userInterfaceLanguage = globalVariables.userInterfaceLanguageUppercade;
    };

    // Country select2 ----------------------------------------------------------------------------
    var enableNewCountry = function () {
        MyRio2cCommon.enableFieldEdit({ dataId: countryDataId });
        countryUidElement.val('').trigger('change');

        return false;
    };

    var enableCountrySelect2 = function () {
        countryUidElement.select2({
            language: userInterfaceLanguage,
            width: '100%',
            placeholder: labels.selectPlaceholder,
            triggerChange: true,
            allowClear: true
        });
    };

    // State select2 ------------------------------------------------------------------------------
    var enableNewState = function () {
        MyRio2cCommon.enableFieldEdit({ dataId: stateDataId });

        if (!MyRio2cCommon.isNullOrEmpty(stateUidElement.val())) {
            stateUidElement.val('').trigger('change');
        }
        else {
            stateUidElement.val('');
        }

        return false;
    };

    var disableNewState = function () {
        MyRio2cCommon.disableFieldEdit({ dataId: stateDataId });

        if (!MyRio2cCommon.isNullOrEmpty(stateNameElement.val())) {
            stateNameElement.val('').trigger('change');
        }
        else {
            stateNameElement.val('');
        }

        return false;
    };

    var toggleState = function () {
        if (!MyRio2cCommon.isNullOrEmpty(countryUidElement.val())) {
            $('[data-id="' + stateDataId + '"] .btn-edit').removeClass('disabled');
            stateUidElement.prop("disabled", false);
        }
        else {
            $('[data-id="' + stateDataId + '"] .btn-edit').addClass('disabled');
            stateUidElement.prop("disabled", true);
            stateNameElement.val('');
        }

        if (!MyRio2cCommon.isNullOrEmpty(stateNameElement.val())) {
            enableNewState();
        }
        else {
            disableNewState();
        }
    };

    var emptyStateSelect2 = function () {
        stateUidElement.val('').trigger('change');

        stateUidElement.select2({
            language: userInterfaceLanguage,
            width: '100%',
            placeholder: labels.selectPlaceholder,
            data: []
        });

        stateUidElement.empty();
    };

    var enableStateSelect2 = function (isParentChanged) {
        if (!MyRio2cCommon.isNullOrEmpty(isParentChanged) && isParentChanged === true) {
            if (!MyRio2cCommon.isNullOrEmpty(stateUidElement.val())) {
                stateUidElement.val('').trigger('change');
            }
            else {
                stateUidElement.val('');
            }
        }

        toggleState();

        var countryUid = countryUidElement.val();

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

                        stateUidElement.empty().select2({
                            language: userInterfaceLanguage,
                            width: '100%',
                            placeholder: labels.selectPlaceholder,
                            triggerChange: true,
                            allowClear: true,
                            data: statesData
                        });

                        var initialStateNameValue = stateNameElement.val();
                        var initialStateUidValue = initialStateUidElement.val();
                        if (!MyRio2cCommon.isNullOrEmpty(initialStateNameValue)) {
                            enableNewState();
                        }
                        else if (!MyRio2cCommon.isNullOrEmpty(initialStateUidValue)) {
                            stateUidElement.val(initialStateUidValue).trigger('change');
                            initialStateUidElement.val('');
                        }
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
    var enableNewCity = function () {
        MyRio2cCommon.enableFieldEdit({ dataId: cityDataId });

        if (!MyRio2cCommon.isNullOrEmpty(cityUidElement.val())) {
            cityUidElement.val('').trigger('change');
        }
        else {
            cityUidElement.val('');
        }

        return false;
    };

    var disableNewCity = function () {
        MyRio2cCommon.disableFieldEdit({ dataId: cityDataId });

        if (!MyRio2cCommon.isNullOrEmpty(cityNameElement.val())) {
            cityNameElement.val('').trigger('change');
        }
        else {
            cityNameElement.val('');
        }

        return false;
    };

    var toggleCity = function () {
        if (!MyRio2cCommon.isNullOrEmpty(initialStateUidElement.val()) || !MyRio2cCommon.isNullOrEmpty(stateUidElement.val()) || !MyRio2cCommon.isNullOrEmpty(stateNameElement.val())) {
            $('[data-id="' + cityDataId + '"] .btn-edit').removeClass('disabled');
            cityUidElement.prop("disabled", false);
        }
        else {
            $('[data-id="' + cityDataId + '"] .btn-edit').addClass('disabled');
            cityUidElement.prop("disabled", true);
            cityNameElement.val('');
        }

        if (!MyRio2cCommon.isNullOrEmpty(cityNameElement.val())) {
            enableNewCity();
        }
        else {
            disableNewCity();
        }
    };

    var emptyCitySelect2 = function () {
        cityUidElement.val('').trigger('change');

        cityUidElement.select2({
            language: userInterfaceLanguage,
            width: '100%',
            placeholder: labels.selectPlaceholder,
            data: []
        });

        cityUidElement.empty();
    };

    var enableCitySelect2 = function (isParentChanged) {
        if (!MyRio2cCommon.isNullOrEmpty(isParentChanged) && isParentChanged === true) {
            if (!MyRio2cCommon.isNullOrEmpty(cityUidElement.val())) {
                cityUidElement.val('').trigger('change');
            }
            else {
                cityUidElement.val('');
            }
        }

        toggleCity();

        var stateUid = stateUidElement.val();

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
                                cityObject = new Object();
                                cityObject.id = data.cities[i].Uid;
                                cityObject.text = data.cities[i].Name;
                                citiesData.push(cityObject);
                            }
                        }

                        cityUidElement.empty().select2({
                            language: userInterfaceLanguage,
                            width: '100%',
                            placeholder: labels.selectPlaceholder,
                            triggerChange: true,
                            allowClear: true,
                            data: citiesData
                        });

                        var initialCityNameValue = cityNameElement.val();
                        var initialCityUidValue = initialCityUidElement.val();
                        if (!MyRio2cCommon.isNullOrEmpty(initialCityNameValue)) {
                            enableNewCity();
                        }
                        else if (!MyRio2cCommon.isNullOrEmpty(initialCityUidValue)) {
                            cityUidElement.val(initialCityUidValue).trigger('change');
                            initialCityUidElement.val('');
                        }
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
        enableStateSelect2(false);
        enableCitySelect2(false);
    };

    // Enable zip code mask -----------------------------------------------------------------------
    var enableZipCodeMask = function () {
        if (typeof (MyRio2cInputMask) === 'undefined') {
            return;
        }

        var countryZipCodeMask = countryUidElement.find(":selected").data("zipcode-mask");
        if (!MyRio2cCommon.isNullOrEmpty(countryZipCodeMask)) {
            MyRio2cInputMask.enableMask(addressZipCode, countryZipCodeMask);
        }
        else {
            MyRio2cInputMask.removeMask(addressZipCode);
        }
    };

    // Enable change events -----------------------------------------------------------------------
    var enableCountryChangeEvent = function () {
        countryUidElement.not('.change-event-enabled').on('change', function () {
            enableStateSelect2(true);
            enableZipCodeMask();
            MyRio2cCommon.enableCompanyNumberMask(countryUid, '#Document');
        });
        countryUidElement.addClass('change-event-enabled');
    };

    var enableStateChangeEvent = function () {
        stateUidElement.not('.change-event-enabled').on('change', function () {
            enableCitySelect2(true);
        });
        stateUidElement.addClass('change-event-enabled');

        stateNameElement.not('.change-event-enabled').on('change keyup', function () {
            toggleCity();
        });
        stateNameElement.addClass('change-event-enabled');
    };

    var enableCityChangeEvent = function () {
        cityUidElement.not('.change-event-enabled').on('change', function () {
        });
        cityUidElement.addClass('change-event-enabled');

        cityNameElement.not('.change-event-enabled').on('change keyup', function () {
        });
        cityNameElement.addClass('change-event-enabled');
    };

    var enableChangeEvents = function () {
        enableCountryChangeEvent();
        enableStateChangeEvent();
        enableCityChangeEvent();
    };

    // Enable plugins -----------------------------------------------------------------------------
    var enablePlugins = function () {
        enableChangeEvents();
        enableSelect2();
        enableZipCodeMask();
        MyRio2cCommon.enableCompanyNumberMask(countryUid, '#Document');
    };

    return {
        init: function () {
            initElements();
            enablePlugins();
        },
        enableNewCountry: function () {
            return enableNewCountry();
        },
        enableNewState: function () {
            return enableNewState();
        },
        disableNewState: function () {
            return disableNewState();
        },
        enableNewCity: function () {
            return enableNewCity();
        },
        disableNewCity: function () {
            return disableNewCity();
        }
    };
}();
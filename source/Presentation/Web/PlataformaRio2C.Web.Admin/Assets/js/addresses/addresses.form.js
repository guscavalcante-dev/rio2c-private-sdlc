// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 08-23-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-24-2019
// ***********************************************************************
// <copyright file="addresses.form.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var AddressesForm = function (formId) {

    var countryDataId = 'Country';
    var stateDataId = 'State';
    var cityDataId = 'City';

    var countryUid = '#Address_CountryUid';
    var stateUid = '#Address_StateUid';
    var stateName = '#Address_StateName';
    var cityUid = '#Address_CityUid';
    var cityName = '#Address_CityName';

    var countryUidElement;
    var stateUidElement;
    var stateNameElement;
    var cityUidElement;
    var cityNameElement;

    // Init elements ------------------------------------------------------------------------------
    var initElements = function () {
        countryUidElement = $(countryUid);
        stateUidElement = $(stateUid);
        stateNameElement = $(stateName);
        cityUidElement = $(cityUid);
        cityNameElement = $(cityName);
    };

    // Country select2 ----------------------------------------------------------------------------
    var enableNewCountry = function () {
        MyRio2cCommon.enableFieldEdit({ dataId: countryDataId });
        countryUidElement.val('').trigger('change');

        return false;
    };

    var enableCountrySelect2 = function () {
        countryUidElement.select2({
            width: '100%',
            placeholder: labels.selectPlaceholder,
            triggerChange: true,
            allowClear: true
        });
    };

    // State select2 ------------------------------------------------------------------------------
    var enableNewState = function () {
        MyRio2cCommon.enableFieldEdit({ dataId: stateDataId });
        stateUidElement.val('').trigger('change');

        return false;
    };

    var toggleNewStateButton = function () {
        if (MyRio2cCommon.isNullOrEmpty(countryUidElement.val())) {
            $('[data-id="' + stateDataId + '"] .btn-edit').addClass('disabled');
            MyRio2cCommon.disableFieldEdit({ dataId: stateDataId });
        }
        else {
            $('[data-id="' + stateDataId + '"] .btn-edit').removeClass('disabled');
        }
    };

    var emptyStateSelect2 = function () {
        stateUidElement.val('').trigger('change');

        stateUidElement.select2({
            width: '100%',
            placeholder: labels.selectPlaceholder,
            data: []
        });

        stateUidElement.empty();
        stateUidElement.prop("disabled", true);
    };

    var enableStateSelect2 = function () {
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

                        stateUidElement.val('').trigger('change');

                        stateUidElement.empty().select2({
                            placeholder: labels.selectPlaceholder,
                            triggerChange: true,
                            allowClear: true,
                            data: statesData
                        });

                        stateUidElement.prop("disabled", false);
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
        cityUidElement.val('').trigger('change');

        return false;
    };

    var toggleNewCityButton = function () {
        if (MyRio2cCommon.isNullOrEmpty(stateUidElement.val()) && MyRio2cCommon.isNullOrEmpty(stateNameElement.val())) {
            $('[data-id="' + cityDataId + '"] .btn-edit').addClass('disabled');
            MyRio2cCommon.disableFieldEdit({ dataId: cityDataId });
        }
        else {
            $('[data-id="' + cityDataId + '"] .btn-edit').removeClass('disabled');
        }
    };

    var emptyCitySelect2 = function () {
        cityUidElement.val('').trigger('change');

        cityUidElement.select2({
            width: '100%',
            placeholder: labels.selectPlaceholder,
            data: []
        });

        cityUidElement.empty();
        cityUidElement.prop("disabled", true);
    };

    var enableCitySelect2 = function () {
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
                                cityObject= new Object();
                                cityObject.id = data.cities[i].Uid;
                                cityObject.text = data.cities[i].Name;
                                citiesData.push(cityObject);
                            }
                        }

                        cityUidElement.val('').trigger('change');

                        cityUidElement.empty().select2({
                            placeholder: labels.selectPlaceholder,
                            triggerChange: true,
                            allowClear: true,
                            data: citiesData
                        });

                        cityUidElement.prop("disabled", false);
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
        countryUidElement.not('.change-event-enabled').on('change', function() {
            enableStateSelect2();
            toggleNewStateButton();
        });
        countryUidElement.addClass('change-event-enabled');
    };

    var enableStateChangeEvent = function () {
        stateUidElement.not('.change-event-enabled').on('change', function () {
            enableCitySelect2();
            toggleNewCityButton();
        });
        stateUidElement.addClass('change-event-enabled');

        stateNameElement.not('.change-event-enabled').on('change keyup', function () {
            toggleNewCityButton();
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
        enableSelect2();
        enableChangeEvents();
        toggleNewStateButton();
        toggleNewCityButton();
    };

    return {
        init: function () {
            initElements();
            enablePlugins();
        },
        enableNewCountry: function() {
            return enableNewCountry();
        },
        enableNewState: function () {
            return enableNewState();
        },
        enableNewCity: function () {
            return enableNewCity();
        }
    };
}();
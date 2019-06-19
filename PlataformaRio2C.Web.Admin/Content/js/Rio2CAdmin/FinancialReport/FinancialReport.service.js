(function () {
    'use strict';

    angular
        .module('FinancialReport')
        .factory('FinancialReportService', FinancialReportService);

    FinancialReportService.$inject = ['$http'];

    function FinancialReportService($http) {
        var self = this;

        const enableGroupLabels = false;

        self.allItems = [];

        ////////////////////////////////////

        self.getData = getData;
        self.getDataGroupBy = getDataGroupBy;
        self.getOptions = getOptions;

        //////////////////////////////////////////

        var groupsCountry = [
            {
                label: 'argentina',
                options: ['agentina', 'aregntina', 'argentina']
            },
            {
                label: 'brasil',
                options: ['braisl', 'brasil', 'brasil / são paulo', 'brasil/eua', 'brasil/peru', 'brasil/uk', 'brasili', 'brazil']
            }
        ];

        function groupBy(list, key) {
            if (angular.isArray(list) && list.length > 0 && key != null) {
                var tempResult;
                if (key.indexOf("date") !== -1) {
                    tempResult = list.reduce(function (r, a) {
                        let keyValue = a[key];
                        let keyDateFormat = moment(keyValue).format("DD/MM/YYYY");

                        r[keyDateFormat] = r[keyDateFormat] || [];
                        r[keyDateFormat].push(a);
                        return r;
                    }, Object.create(null));
                }
                else if (key.indexOf("country") !== -1) {
                    tempResult = list.reduce(function (r, a) {
                        let keyValue = a[key];

                        keyValue = keyValue.trim().toLowerCase();                       

                        if (enableGroupLabels && groupsCountry.some(function (i) { return i.options.some(function (o) { return o == keyValue }); })) {
                            let optionFind = groupsCountry.find(function (i) { return i.options.some(function (o) { return o == keyValue }); });

                            r[optionFind.label] = r[optionFind.label] || [];
                            r[optionFind.label].push(a);
                            return r;
                        }
                        else {
                            r[keyValue] = r[keyValue] || [];
                            r[keyValue].push(a);
                            return r;
                        }

                    }, Object.create(null));
                }
                else {
                    tempResult = list.reduce(function (r, a) {
                        r[a[key]] = r[a[key]] || [];
                        r[a[key]].push(a);
                        return r;
                    }, Object.create(null));
                }

                var myArray = Object.keys(tempResult).map(function (_key) {
                    var label = _key;

                    if (key.indexOf("country") !== -1 && _key !== "") {
                        
                        var labelsArray = Array.from(new Set(tempResult[_key].map(function (i) { return i.country })));
                        var labelsString = labelsArray.join(", ");

                        if (labelsArray.length > 1) {                            
                            label = _key + " ( " + labelsString + " )";
                        }                        
                    }

                    return {
                        label: label,
                        items: tempResult[_key],
                        count: tempResult[_key].length,
                        value: getCurrencyValue(tempResult[_key])
                    };
                });

                return myArray;
            }
        }

        function getCurrencyValue(list) {
            var listValues = list.map(function (i) {
                return i.order_total_sale_price;
            })

            return listValues.reduce(function (a, b) {
                return a + b;
            });
        }

        function getOptions(key) {
            var options = [];

            if (self.allItems.length > 0) {
                angular.forEach(self.allItems, function (item) {
                    if (options.indexOf(item[key]) === -1) {
                        options.push(item[key]);
                    }
                });
            }

            return options;
        }

        /////////////////


        function getData() {
            return $http.get('/api/financialreports/sales', {
                params: {}
            })
            .then(function (response) {
                if (angular.isArray(response.data)) {
                    self.allItems = angular.copy(response.data);
                    return response.data;
                }

                return [];
            })
            .catch(function () {
                self.allItems = [];
                return [];
            })
            .finally(function () {

            });
        }

        function getDataGroupBy(keyGroup) {
            return getData().then(function (response) {
                return groupBy(response, keyGroup);
            })

        }

        /////////////////////////////////////////////

        return {
            getData: self.getData,
            getDataGroupBy: self.getDataGroupBy,
            getOptions: self.getOptions
        }
    }
})();
(function () {
    'use strict';

    angular
        .module('FinancialReport')
        .controller('FinancialReportCtrl', FinancialReportCtrl);

    FinancialReportCtrl.$inject = ['$scope', 'FinancialReportService', '$element'];

    function FinancialReportCtrl($scope, FinancialReportService, $element) {
        var vm = this;

        vm.groupBy = $element.context.dataset.groupBy;
        vm.filterTransactionType = $element.context.dataset.filterTransactionType || null;
        

        vm.allItems = [];
        vm.items = [];
        vm.options = [];

        vm.loadingList = false;
        vm.filter = {};
        vm.filter.categoryName = null;
        vm.filter.startDate = null;
        vm.filter.endDate = null;

        /////////////////////////////////////////////////////

        vm.getData = getData;
        vm.filterItems = filterItems;
        vm.clearFilter = clearFilter;

        /////////////////////////////////////////////////////

        function filterItems() {
            var results = angular.copy(vm.allItems);
            vm.quantitySum = 0;
            vm.sumOfValue = 0;

            if (hasGroupBy()) {
                if (vm.filter.categoryName !== null && vm.filter.categoryName !== "") {
                    results = results.filter(function (i) { return i.label.trim().toLowerCase().indexOf(vm.filter.categoryName.trim().toLowerCase()) !== -1; });
                }

                if (vm.filter.startDate !== null && vm.filter.startDate !== "" && vm.filter.endDate !== null && vm.filter.endDate !== "") {
                    var dayStart = moment(vm.filter.startDate, "DD/MM/YYYY");
                    var dayEnd = moment(vm.filter.endDate, "DD/MM/YYYY");

                    results = results.filter(function (i) {
                        var items = i.items.filter(function (s) {
                            let saleDate = moment(s.order_date).format("DD/MM/YYYY");
                            let saleDateMoment = moment(saleDate, "DD/MM/YYYY");
                            return saleDateMoment.isSameOrAfter(dayStart) && saleDateMoment.isSameOrBefore(dayEnd);
                        });

                        i.items = items;

                        return items.length > 0;
                    });
                }
                else if (vm.filter.startDate !== null && vm.filter.startDate !== "") {
                    var dayStart = moment(vm.filter.startDate, "DD/MM/YYYY");

                    results = results.filter(function (i) {
                        var items = i.items.filter(function (s) {
                            let saleDate = moment(s.order_date).format("DD/MM/YYYY");
                            let saleDateMoment = moment(saleDate, "DD/MM/YYYY");
                            return saleDateMoment.isSameOrAfter(dayStart);
                        });

                        i.items = items;

                        return items.length > 0;
                    });
                }
                else if (vm.filter.endDate !== null && vm.filter.endDate !== "") {
                    var dayEnd = moment(vm.filter.endDate, "DD/MM/YYYY");

                    results = results.filter(function (i) {
                        var items = i.items.filter(function (s) {
                            let saleDate = moment(s.order_date).format("DD/MM/YYYY");
                            let saleDateMoment = moment(saleDate, "DD/MM/YYYY");
                            return saleDateMoment.isSameOrBefore(dayEnd);
                        });

                        i.items = items;

                        return items.length > 0;
                    });
                }

                results = results.map(function (i) {
                    return {
                        label: i.label,
                        items: i.items,
                        count: i.items.length,
                        value: getCurrencyValue(i.items)
                    }
                });
                if (results != null && results.length > 0) {
                    var counts = results.map(function (i) { return i.count });
                    vm.quantitySum = counts.reduce(function (a, b) { return a + b });

                    var valuesCurrency = results.map(function (i) { return i.value });
                    vm.sumOfValue = valuesCurrency.reduce(function (a, b) { return a + b });
                }
            }
            else {
                if (vm.filter.categoryName !== null && vm.filter.categoryName !== "") {
                    results = results.filter(function (i) { return i.ticket_name.indexOf(vm.filter.categoryName) !== -1; });
                }

                if (vm.filter.startDate !== null && vm.filter.startDate !== "" && vm.filter.endDate !== null && vm.filter.endDate !== "") {
                    var dayStart = moment(vm.filter.startDate, "DD/MM/YYYY");
                    var dayEnd = moment(vm.filter.endDate, "DD/MM/YYYY");

                    results = results.filter(function (i) {
                        let saleDate = moment(i.order_date).format("DD/MM/YYYY");
                        let saleDateMoment = moment(saleDate, "DD/MM/YYYY");

                        return saleDateMoment.isSameOrAfter(dayStart) && saleDateMoment.isSameOrBefore(dayEnd);
                    });                   
                }
                else if (vm.filter.startDate !== null && vm.filter.startDate !== "") {
                    var dayStart = moment(vm.filter.startDate, "DD/MM/YYYY");

                    results = results.filter(function (i) {
                        let saleDate = moment(i.order_date).format("DD/MM/YYYY");
                        let saleDateMoment = moment(saleDate, "DD/MM/YYYY");

                        return saleDateMoment.isSameOrAfter(dayStart);
                    });
                }
                else if (vm.filter.endDate !== null && vm.filter.endDate !== "") {                    
                    var dayStart = moment(vm.filter.endDate, "DD/MM/YYYY");

                    results = results.filter(function (i) {
                        let saleDate = moment(i.order_date).format("DD/MM/YYYY");
                        let saleDateMoment = moment(saleDate, "DD/MM/YYYY");

                        return saleDateMoment.isSameOrBefore(dayStart);
                    });                   
                }

                if (results != null && results.length > 0) {
                    var valuesCurrency = results.map(function (i) { return i.order_total_sale_price });
                    vm.sumOfValue = valuesCurrency.reduce(function (a, b) { return a + b });
                }               
            }

            vm.items = results;
        }

        function hasGroupBy() {
            return angular.isDefined(vm.groupBy) && vm.groupBy !== null && vm.groupBy !== "";
        }

        function getCurrencyValue(list) {
            var listValues = list.map(function (i) {
                return i.order_total_sale_price;
            })

            return listValues.reduce(function (a, b) {
                return a + b;
            });
        }

        function clearFilter() {
            vm.filter.categoryName = null;
            vm.filter.startDate = null;
            vm.filter.endDate = null;
            filterItems();
        }

        function getData() {
            vm.allItems = [];
            vm.items = [];
            vm.loadingList = true;

            if (angular.isUndefined(vm.groupBy) || vm.groupBy === null || vm.groupBy === "") {
                FinancialReportService.getData()
               .then(function (response) {
                   vm.allItems = angular.copy(response);

                   if (vm.filterTransactionType != null) {
                       vm.allItems = vm.allItems.filter(function (i) { return i.transaction_type == vm.filterTransactionType });
                   }

                   vm.options = FinancialReportService.getOptions('ticket_name');
                   filterItems();
               })
               .catch(function () {
                   vm.items = [];
               })
               .finally(function () {
                   vm.loadingList = false;
               });
            }
            else {
                FinancialReportService.getDataGroupBy(vm.groupBy)
                .then(function (response) {
                    vm.allItems = angular.copy(response);
                    vm.options = FinancialReportService.getOptions(vm.groupBy);
                    filterItems();
                })
                .catch(function () {
                    vm.items = [];
                })
                .finally(function () {
                    vm.loadingList = false;
                });
            }
        }

        getData();

        vm.orderByCustom = function (item) {
            var parts = item.label.split('/');
            var number = parseInt(parts[2] + parts[1] + parts[0]);

            return -number;
        };

        ////////////////////////////////////////

        $scope.open1 = function () {
            $scope.popup1.opened = true;
        };

        $scope.dateOptions = {
            formatYear: 'yyyy'
        };

        $scope.open2 = function () {
            $scope.popup2.opened = true;
        };

        $scope.popup1 = {
            opened: false
        };

        $scope.popup2 = {
            opened: false
        };

        var tomorrow = new Date();
        tomorrow.setDate(tomorrow.getDate() + 1);

        var afterTomorrow = new Date();
        afterTomorrow.setDate(tomorrow.getDate() + 1);

        $scope.events = [
          {
              date: tomorrow,
              status: 'full'
          },
          {
              date: afterTomorrow,
              status: 'partially'
          }
        ];
    }
})();
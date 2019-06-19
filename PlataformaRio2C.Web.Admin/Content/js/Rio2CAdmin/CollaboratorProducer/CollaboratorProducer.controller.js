(function () {
    'use strict';

    angular
    .module('CollaboratorProducer')
    .controller('CollaboratorProducerCtrl', CollaboratorProducerCtrl);

    CollaboratorProducerCtrl.$inject = ['CollaboratorProducerService', '$filter'];

    function CollaboratorProducerCtrl(CollaboratorProducerService, $filter) {
        var vm = this;

        vm.allItems = [];
        vm.items = [];
        vm.loadingList = false;
        vm.orderBy = null;
        vm.orderByDesc = false;

        vm.filter = {};

        ///////////////////////////////////////////////

        vm.list = list;
        vm.toSort = toSort;
        vm.toFilter = toFilter;
        vm.clearFilter = clearFilter;

        ////////////////////////////////////////////////

        function list(val) {          
            vm.loadingList = true;            

            var params = {};

            if (val != null) {
                params.orderBy = val;

                if (val === vm.orderBy) {
                    params.orderByDesc = !vm.orderByDesc;
                    vm.orderByDesc = params.orderByDesc;
                }
                else {
                    vm.orderByDesc = false;
                }
            }

            CollaboratorProducerService.getAll(params)
            .then(function (response) {
                if (angular.isArray(response)) {
                    vm.items = response;
                    vm.allItems = response;
                    vm.orderBy = params.orderBy.toLowerCase();
                    getOptionsProducers();
                }
            })
            .catch(function (response) {

            })
            .finally(function () {
                vm.loadingList = false;
            });
        }

        function toFilter() {
            vm.items = angular.copy(vm.allItems);

            if (vm.filter.name != null && vm.filter.name != "") {
                vm.items = vm.items.filter(function (i) { return accentFold(i.name).toLowerCase().indexOf(accentFold(vm.filter.name).toLowerCase()) !== -1 });
            }

            if (vm.filter.email != null && vm.filter.email != "") {
                vm.items = vm.items.filter(function (i) { return i.email.toLowerCase().indexOf(vm.filter.email.toLowerCase()) !== -1 });
            }

            if (vm.filter.producerName != null && vm.filter.producerName != "") {
                vm.items = vm.items.filter(function (i) { return i.producerName != null && accentFold(i.producerName.toLowerCase()).indexOf(accentFold(vm.filter.producerName.toLowerCase())) !== -1 });
            }

            if (angular.isDefined(vm.filter.hasAcceptTerm) && vm.filter.hasAcceptTerm !== "") {
                vm.items = vm.items.filter(function (i) { return i.hasAcceptTerm === (vm.filter.hasAcceptTerm == 'true') });
            }            
        }

        function clearFilter() {
            vm.items = angular.copy(vm.allItems);
            vm.orderByDesc = false;
            vm.filter.name = null;
            vm.filter.email = null;
            vm.filter.producerName = null;
            vm.filter.hasAcceptTerm = undefined;
        }

        function getOptionsProducers() {
            vm.optionsProducers = [];
            if (vm.allItems != null && vm.allItems.length > 0) {
                angular.forEach(vm.allItems, function (item) {
                    if (!(vm.optionsProducers.indexOf(item.producerName) !== -1)) {
                        vm.optionsProducers.push(item.producerName);
                    }
                });
            }
        }

        function localeSensitiveComparator(v1, v2) {            
            if (v1.type !== 'string' || v2.type !== 'string') {
                return (v1.index < v2.index) ? -1 : 1;
            }

            return v1.value.localeCompare(v2.value);
        }

        function toSort(val) {
            var params = {};

            if (val != null) {
                params.orderBy = val;

                if (val === vm.orderBy) {
                    params.orderByDesc = !vm.orderByDesc;
                    vm.orderByDesc = params.orderByDesc;
                }
                else {
                    vm.orderByDesc = false;
                }

                vm.orderBy = val;
            }

            if (vm.orderByDesc === false) {
                vm.items = $filter('orderBy')(vm.items, ['!!' + params.orderBy, "+" + params.orderBy], false);             
            }
            else {
                vm.items = $filter('orderBy')(vm.items, ['-!!' + params.orderBy, "-" + params.orderBy], false);                
            }
        }

        function accentFold(inStr) {
            return inStr.replace(/([àáâãäå])|([ç])|([èéêë])|([ìíîï])|([ñ])|([òóôõöø])|([ß])|([ùúûü])|([ÿ])|([æ])/g,
                function (str, a, c, e, i, n, o, s, u, y, ae) {
                    if (a) return 'a'; else if (c) return 'c'; else if (e) return 'e'; else if (i) return 'i'; else if (n) return 'n'; else if (o) return 'o'; else if (s) return 's'; else if (u) return 'u'; else if (y) return 'y'; else if (ae) return 'ae';
                });
        }
    }
})();
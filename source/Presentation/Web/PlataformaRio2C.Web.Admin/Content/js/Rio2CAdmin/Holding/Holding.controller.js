(function () {
    'use strict';

    angular
    .module('Holding')
    .controller('HoldingCtrl', HoldingCtrl);

    HoldingCtrl.$inject = ['HoldingService'];

    function HoldingCtrl(HoldingService) {
        var vm = this;

        vm.items = [];
        vm.loadingList = false;
        vm.orderBy = null;
        vm.orderByDesc = false;

        ///////////////////////////////////////////////

        vm.list = list;

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

            HoldingService.getAll(params)
            .then(function (response) {
                if (angular.isArray(response)) {
                    vm.items = response;
                    vm.orderBy = params.orderBy;
                }
            })
            .catch(function (response) {

            })
            .finally(function () {
                vm.loadingList = false;
            });
        }
    }
})();
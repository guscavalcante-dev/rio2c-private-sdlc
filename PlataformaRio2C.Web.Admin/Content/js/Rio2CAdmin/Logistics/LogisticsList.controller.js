(function () {
    'use strict';

    angular
        .module('Logistics')
        .controller('LogisticsList', LogisticsList);

    LogisticsList.$inject = ['$scope', '$http', '$element'];

    function LogisticsList($scope, $http, $element) {
        var vm = this;

        vm.orderBy = null;
        vm.orderByDesc = false;

        vm.listLogistics = [];

        vm.loadingListLogistics = false;


        /////////////////////////////////////////////////////

        vm.getListLogistics = getListLogistics;

        /////////////////////////////////////////////////////

        function getListLogistics(val) {
            vm.loadingListLogistics = true;

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

            $http.get('/api/logistics', {
                params: params
            })
                .then(function (response) {

                    if (angular.isArray(response.data)) {
                        vm.listLogistics = response.data;
                        vm.orderBy = params.orderBy;

                    }
                    else {
                        vm.listLogistics = [];
                    }
                })
                .catch(function () {
                    vm.listLogistics = [];
                })
                .finally(function () {
                    vm.loadingListLogistics = false;
                });
        }


        getListLogistics('Collaborator');
    }
})();
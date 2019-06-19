(function () {
    'use strict';

    angular
        .module('Producer')
        .controller('ProducerList', ProducerList);

    ProducerList.$inject = ['$scope', '$http', '$element'];

    function ProducerList($scope, $http, $element) {
        var vm = this;

        vm.orderBy = null;
        vm.orderByDesc = false;

        vm.listProducer = [];

        vm.loadingListProducer = false;

        /////////////////////////////////////////////////////

        vm.getListProducer = getListProducer;

        /////////////////////////////////////////////////////

        function getListProducer(val) {
            vm.loadingListProducer = true;

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

            $http.get('/api/producer', {
                params: params
            })
                .then(function (response) {

                    if (angular.isArray(response.data)) {
                        vm.listProducer = response.data;
                        vm.orderBy = params.orderBy;

                    }
                    else {
                        vm.listConference = [];
                    }
                })
                .catch(function () {
                    vm.listConference = [];
                })
                .finally(function () {
                    vm.loadingListProducer = false;
                });
        }

        getListProducer('Name');
    }
})();
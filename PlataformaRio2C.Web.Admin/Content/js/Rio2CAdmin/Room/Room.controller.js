(function () {
    'use strict';

    angular
        .module('Room')
        .controller('RoomList', RoomList);

    RoomList.$inject = ['$scope', '$http', '$element'];

    function RoomList($scope, $http, $element) {
        var vm = this;

        vm.orderBy = null;
        vm.orderByDesc = false;

        vm.listRoom = [];

        vm.loadingListRoom = false;

        /////////////////////////////////////////////////////

        vm.getListRoom = getListRoom;

        /////////////////////////////////////////////////////

        function getListRoom(val) {
            vm.loadingListRoom = true;

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

            $http.get('/api/Room', {
                params: params
            })
                .then(function (response) {

                    if (angular.isArray(response.data)) {
                        vm.listRoom = response.data;
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
                    vm.loadingListRoom = false;
                });
        }

        getListRoom('Name');
    }
})();
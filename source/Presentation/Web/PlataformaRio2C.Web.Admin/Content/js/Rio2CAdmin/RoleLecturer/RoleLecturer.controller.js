(function () {
    'use strict';

    angular
        .module('RoleLecturer')
        .controller('RoleLecturerList', RoleLecturerList);

    RoleLecturerList.$inject = ['$scope', '$http', '$element'];

    function RoleLecturerList($scope, $http, $element) {
        var vm = this;

        vm.orderBy = null;
        vm.orderByDesc = false;

        vm.listRoleLecturer = [];

        vm.loadingListRoleLecturer = false;

        /////////////////////////////////////////////////////

        vm.getListRoleLecturer = getListRoleLecturer;

        /////////////////////////////////////////////////////

        function getListRoleLecturer(val) {
            vm.loadingListRoleLecturer = true;

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

            $http.get('/api/RoleLecturer', {
                params: params
            })
                .then(function (response) {

                    if (angular.isArray(response.data)) {
                        vm.listRoleLecturer = response.data;
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
                    vm.loadingListRoleLecturer = false;
                });
        }

        getListRoleLecturer('Name');
    }
})();
(function () {
    'use strict';

    angular
       .module('Rio2CAdmin')
    .run(['$rootScope', Rio2CAdminRun]);

    function Rio2CAdminRun($rootScope) {

        try {
            $rootScope.baseUrl = baseUrl;
        } catch (e) {
            $rootScope.baseUrl = "";
        }

        $rootScope.setViewmodel = function () {
            try {
                $rootScope.viewModel = angular.copy(viewModel);

            } catch (e) {
                $rootScope.viewModel = {};
            }
        }
        $rootScope.setViewmodel();
    }
})();
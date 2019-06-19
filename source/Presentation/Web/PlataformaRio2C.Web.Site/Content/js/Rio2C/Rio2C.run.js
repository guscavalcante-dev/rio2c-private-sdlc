(function () {
    'use strict';

    angular
        .module('Rio2C')
        .run(['$rootScope', Rio2CRun]);

    function Rio2CRun($rootScope) {

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
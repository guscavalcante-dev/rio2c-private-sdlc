(function () {
    'use strict';

    angular
    .module('Holding')
    .factory('HoldingService', HoldingService);

    HoldingService.$inject = ['$http'];

    function HoldingService($http) {    
        var self = this;

        self.list = [];

        //////////////////////////////////////////////////

        function getAll(params) {
            return $http.get('/api/holdings', {
                params: params
            })
            .then(function (response) {
                if (angular.isArray(response.data)) {
                    self.list = response.data;
                    return self.list;
                }
            })
            .catch(function (response) {
                self.list = [];
                return self.list;
            })
            .finally(function () {
                
            })
        }

        /////////////////////////////////

        return {
            getAll: getAll
        };
    }

})();
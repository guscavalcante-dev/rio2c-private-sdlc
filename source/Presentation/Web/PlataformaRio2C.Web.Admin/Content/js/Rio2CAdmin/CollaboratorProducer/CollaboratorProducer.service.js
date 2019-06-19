(function () {
    'use strict';

    angular
    .module('CollaboratorProducer')
    .factory('CollaboratorProducerService', CollaboratorProducerService);

    CollaboratorProducerService.$inject = ['$http'];

    function CollaboratorProducerService($http) {
        var self = this;

        self.list = [];

        //////////////////////////////////////////////////

        function getAll(params) {
            return $http.get('/api/collaboratorsproducers', {
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
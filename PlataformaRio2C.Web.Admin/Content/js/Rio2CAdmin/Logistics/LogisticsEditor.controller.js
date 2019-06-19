(function () {
    'use strict';

    angular
        .module('Logistics')
        .controller('LogisticsEditor', LogisticsEditor);

    LogisticsEditor.$inject = ['$scope', '$http', '$element'];

    function LogisticsEditor($scope, $http, $element) {
        var vm = this;

        vm.collaborator = JSON.parse($element.context.dataset.collaborator);

        vm.collaborators = [];

        vm.getCollaborators = getCollaborators;

        function getCollaborators(val) {

            return $http.get('/api/logistics/GetCollaboratorsOptions', {
                params: {
                    term: val
                }
            })
                .then(function (response) {

                    if (angular.isArray(response.data)) {
                        return response.data;
                    }
                    else {
                        return [];
                    }
                })
                .catch(function () {
                    return [];
                })
                .finally(function () {

                });
        }
    }
})();
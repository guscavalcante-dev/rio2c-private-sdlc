(function () {
    'use strict';

    angular
        .module('MarlinFile')
        .controller("MarlinFileUploadController", MarlinFileUploadController);

    MarlinFileUploadController.$inject = ['$scope', 'MarlinFileService', '$element'];

    function MarlinFileUploadController($scope, MarlinFileService, $element) {

        $scope.sendForm = function (e) {
            e.preventDefault();

            $scope.flagLoadFile = true;
            $scope.error = null;
            $scope.flagSuccess = false;

            var data = $element.serializeObject();

            for (var i = 0; i < Object.keys(data).length; i++) {
                $scope.file.append(Object.keys(data)[i], data[Object.keys(data)[i]]);
            }

            MarlinFileService.enviar($element[0].action, $scope.file, $scope)
                .then(function (result) {
                    $scope.flagLoadFile = false;
                    
                    $scope.$parent.showForm = false;
                    $scope.$parent.flagSuccess = true;
                })
                .catch(function (result) {
                    console.error(result);
                    $scope.error = result;
                    $scope.flagLoadFile = false;
                    $scope.flagSuccess = false;
                });
        }
    }
})();
(function () {
    'use strict';

    angular
        .module('MarlinFilter')
        .controller("MarlinFilterController", MarlinFilterController);

    MarlinFilterController.$inject = ["MarlinFilterService", '$scope', '$element']

    function MarlinFilterController(MarlinFilterService, $scope, $element) {
        $scope.form = angular.copy($element.serializeObject());

        $scope.sendForm = function (e) {            
            if (e != null) {
                MarlinFilterService.enviar($element[0].action, $element.serializeObject(), $scope, $($element.data('target')), $element.data('marlin-filter'));
            }
        }

        $scope.setStatus = function (status) {
            if (status != null) {
                $scope.form['Filter.StatusId'] = status;

                MarlinFilterService.enviar($element[0].action, $scope.form, $scope, $($element.data('target')), $element.data('marlin-filter'));
            }
        }

        $scope.LimpaFiltro = function (e, url, elemTarget, nameObjectDataFilter) {
            e.preventDefault();

            $(e.target).closest('form')[0].reset();

            MarlinFilterService.enviar(url, $element.serializeObject(), $scope, $(elemTarget), nameObjectDataFilter);
        }
    }
})();
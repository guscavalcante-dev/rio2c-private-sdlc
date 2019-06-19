(function () {
    'use strict';
    angular
        .module('MarlinDataBindExtensions')
        .filter('marlinTextOption', MarlinTextOption)
        .filter('marlinYesNoFromBoolean', marlinYesNoFromBoolean)
        .controller('marlinTable', marlinTable);

    function MarlinTextOption() {
        return function (list, stringConcat) {
            var result = [];

            if (angular.isArray(list)) {
                result = list.map(function (item) {
                    return item.Text
                });

                if (stringConcat != null) {
                    result = result.join(stringConcat);
                }
            }
            return result;
        }
    }

    function marlinYesNoFromBoolean() {
        return function (item) {            
            if (item) {
                return "sim";
            }
            else {
                return "não";
            }
        }
    }

    marlinTable.$inject = ['$scope'];
    function marlinTable($scope) {
        var vm = this;

        vm.objectEditables = [];

        $scope.enableEdit = function (obj, e) {            
            if (!angular.isObject(obj)) {
                if (angular.isUndefined($scope[obj])) {

                    var data = angular.element(e.currentTarget).closest('tr').find('input[name],select[name],textarea[name]').serializeObject();
                    $scope[obj] = data;
                    $scope[obj].showEdit = true;
                }
                else if (angular.isDefined($scope[obj]) && !$scope[obj].showEdit) {
                    $scope[obj].showEdit = true;
                }
            }
            else {
                obj.showEdit = true;
            }
        };

        $scope.disableEdit = function (obj, e) {
            if (!angular.isObject(obj)) {
                if (angular.isDefined($scope[obj]) && !$scope[obj].enterToEdit) {
                    delete $scope[obj];
                }
            }
            else {
                obj.showEdit = false;
            }
        };

        $scope.focus = function (obj, e) {
            if (angular.isUndefined($scope[obj])) {
                var data = angular.element(e.currentTarget).closest('tr').find('input[name],select[name],textarea[name]').serializeObject();
                $scope[obj] = data;
                $scope[obj].showEdit = true;
            }
            else if (angular.isDefined($scope[obj]) && !$scope[obj].showEdit) {
                $scope[obj].showEdit = true;
            }
        };

        $scope.blur = function (obj, e) {
            if (angular.isDefined($scope[obj]) && !$scope[obj].enterToEdit) {
                delete $scope[obj];
            }
        };

        $scope.toEdit = function (obj) {
            obj.defaultObj = angular.copy(obj);
            obj.enterToEdit = true;

        };

        $scope.endToEdit = function (obj) {
            obj.enterToEdit = false;
        };

        $scope.cancelEdit = function (obj, e) {
            if (angular.isDefined(obj) && obj.enterToEdit) {
                obj.enterToEdit = false;
                angular.extend(obj, obj.defaultObj);
            }
        };

    }
})();
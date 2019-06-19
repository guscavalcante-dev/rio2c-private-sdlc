(function () {
    'use strict';

    angular
        .module('MarlinCopyHeight')
        .directive('marlinCopyHeight', MarlinCopyHeight);

    MarlinCopyHeight.$inject = ['$timeout'];

    function MarlinCopyHeight($timeout) {
        var directive = {           
            link: link
        };

        return directive;

        function link(scope, elem, attrs) {
            function updateHeight(elemtTarget, value) {
                if (elemtTarget.height() < value) {
                    elemtTarget.height(value);
                }
            }

            scope.$watch(function () {
                return elem.height();
            },
           function (value) {
               $timeout(function () {
                   updateHeight(angular.element(attrs.marlinCopyHeight), value);
               }, 500);
           });
        }
    }
})();
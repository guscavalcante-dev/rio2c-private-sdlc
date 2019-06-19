(function () {
    'use strict';

    angular
        .module('MarlinFilter')
        .directive('marlinFilter', marlinFiltertDirective);

    marlinFiltertDirective.$inject = ['send', '$compile', '$cookies', '$location', 'MarlinFilterService'];

    function marlinFiltertDirective(send, $compile, $cookies, $location, MarlinFilterService) {
        var directive = {
            link: link
        };

        return directive;

        function link(scope, element, attrs, ngModelCtrl) {
            scope.form = angular.copy(element.serializeObject());            

            MarlinFilterService.clearCookie();

            $cookies.putObject(attrs.marlinFilter, element.serializeObject());

            if (!(attrs.notSubmitDirective === "true")) {
    
            
                element.submit(function (e) {
                    e.preventDefault();

                    if (typeof $.fn.validate !== 'undefined' && !element.valid()) {
                        return false;
                    }

                    scope.$parent.alertaFiltro = {};

                    var elemTrigger = e.target,
                                                $elemTrigger = $(elemTrigger),
                                                url = elemTrigger.action,
                                                $elemTarget = $($elemTrigger.data('target')),
                                                nameObjectDataFilter = $elemTrigger.data('marlin-filter'),
                                               data = $elemTrigger.serializeObject();

                    MarlinFilterService.enviar(url, data, scope, $elemTarget, nameObjectDataFilter);
                });
            }
        }
    }
})();
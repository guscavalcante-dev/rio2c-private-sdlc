(function () {
    'use strict';

    angular
        .module('MarlinFilter')
        .service('MarlinFilterService', MarlinFilterService);

    MarlinFilterService.$inject = ['send', '$compile', '$cookies', '$location', '$alert'];

    function MarlinFilterService(send, $compile, $cookies, $location, $alert) {

        this.enviar = function (url, data, scope, $elemTarget, nameObjectDataFilter) {
            scope.$parent.flagLoadResults = true;

            send.service({
                url: url,
                data: data
            })
            .then(function (result) {
                $elemTarget.html(result);

                $compile($elemTarget)(scope);

                scope.$parent.alertaFiltro = {};
                $cookies.putObject(nameObjectDataFilter, data);

                scope.$parent.flagLoadResults = false;
            })
            .catch(function (result) {
                console.error(result);
                $elemTarget.html($alert.render('danger', result));

                scope.$parent.flagLoadResults = false;
            });
        }

        this.clearCookie = function () {
            var cookiesApp = $cookies.getAll();

            angular.forEach(cookiesApp, function (v, k) {
                if (k.indexOf('Filtro') != -1) {
                    $cookies.remove(k);
                }
            });
        }
    }
})();
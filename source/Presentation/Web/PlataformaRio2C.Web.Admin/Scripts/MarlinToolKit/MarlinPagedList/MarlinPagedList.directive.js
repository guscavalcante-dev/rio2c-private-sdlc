(function () {
    'use strict';

    angular
        .module('MarlinPagedList')
        .directive('marlinPagedList', MarlinPagedListDirective);

    MarlinPagedListDirective.$inject = ['send', '$compile', '$cookies', '$alert'];

    function MarlinPagedListDirective(send, $compile, $cookies, $alert) {
        var directive = {
            scope: {
                formularioFiltro: '@modelFormFiltro'
            },
            link: link
        };

        return directive;

        function link(scope, elem, attr) {
            var $divTarget = $(attr.target);
            
            elem.find('a').on('click', function (e) {
                e.preventDefault();

                var $this = $(this),
                     url = $this.attr("href"),
                     dataSend = {};

                scope.$parent.alertaPagedList = {};
                scope.$parent.flagLoadResults = true;

                scope.formularioFiltro = $cookies.getObject(elem.data('filter-object'));                

                try {
                    dataSend = scope.$parent.preprarFiltroParaEnvio(dataSend);
                } catch (e) {
                    if (scope.formularioFiltro) {
                        dataSend = angular.isObject(scope.formularioFiltro) ? scope.formularioFiltro : JSON.parse(scope.formularioFiltro);
                    }
                }

                send.service({
                    url: url,
                    data: dataSend,
                    type: 'POST'
                })
                .then(function (result) {
                    $('html,body').animate({ scrollTop: ($divTarget.offset().top) - 100 });
                    $divTarget.html(result);
                    $compile($divTarget)(scope.$parent);
                    scope.$parent.flagLoadResults = false;
                })
                .catch(function (result) {
                    console.error(result);
                    $('html,body').animate({ scrollTop: ($divTarget.offset().top) - 100 });
                    $divTarget.html($alert.render('danger', result));
                    scope.$parent.flagLoadResults = false;
                });
            });
        }
    }
})();
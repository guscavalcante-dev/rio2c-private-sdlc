(function () {
    'use strict';

    angular
        .module('MarlinFile')
        .service('MarlinFileService', MarlinFileService);

    MarlinFileService.$inject = ['send', '$compile', '$cookies', '$location', '$alert'];

    function MarlinFileService(send, $compile, $cookies, $location, $alert) {

        this.enviar = function (url, data, scope) {
            return send.serviceFile({
                url: url,
                data: data
            })
            
        }       
    }
})();
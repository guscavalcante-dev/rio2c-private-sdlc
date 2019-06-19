(function () {
    'use strict';

    angular
        .module('MarlinAjax')
        .config(['$httpProvider', ConfigMarlinAjax]);

    function ConfigMarlinAjax($httpProvider) {
        $httpProvider.defaults.headers.common["X-Requested-With"] = 'XMLHttpRequest';

        $httpProvider.defaults.useXDomain = true;
    }
})();
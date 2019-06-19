(function () {
    'use strict';

    angular
        .module('MarlinTextBind')
        .filter('isNullOrEmpty', isNullOrEmpty)

    function isNullOrEmpty() {
        return function (value, defaultValue) {
            if (value === null || value === undefined || value === '') {
                value = defaultValue;
            }

            return value;
        }
    }
})();
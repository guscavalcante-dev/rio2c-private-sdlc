(function () {
    'use strict';

    angular
        .module('Rio2C')
        .filter('pluralize', pluralize);
    

    function pluralize() {
        return function (input, optional1, optional2) {
            let output = input;

            const pluralDictionary = {
                //'palestrante': 'palestrante(s)'
            }

            if (pluralDictionary.hasOwnProperty(input.toLowerCase())) {                
                output = pluralDictionary[input.toLowerCase()];
            }

            return output;
        }       
    }
})();
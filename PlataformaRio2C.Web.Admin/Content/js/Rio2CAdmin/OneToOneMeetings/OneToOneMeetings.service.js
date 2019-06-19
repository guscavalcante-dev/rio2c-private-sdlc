(function () {
    'use strict';

    angular
        .module('OneToOneMeetings')        
        .factory('OneToOneMeetingsService', OneToOneMeetingsService);

    OneToOneMeetingsService.$inject = ['$http'];    

    function OneToOneMeetingsService($http) {
        return {
        }
    }
})();
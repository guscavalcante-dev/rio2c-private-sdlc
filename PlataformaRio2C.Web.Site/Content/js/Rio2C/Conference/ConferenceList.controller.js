(function () {
	'use strict';

	angular
    .module('Conference')
    .controller('ConferenceList', ConferenceList);

	ConferenceList.$inject = ['$scope', '$http', '$element'];

	function ConferenceList($scope, $http, $element) {
	    var vm = this;

		vm.orderBy = null;
		vm.orderByDesc = false;

		vm.listConference = [];

		vm.loadingListConference= false;

        /////////////////////////////////////////////////////

		vm.getListConference = getListConference;

        /////////////////////////////////////////////////////

		function getListConference(val) {
		    
		    vm.loadingListConference = true;
             
		    var params = {};

		    if (val != null) {
		        params.orderBy = val;

		        if (val === vm.orderBy) {
		            params.orderByDesc = !vm.orderByDesc;
		            vm.orderByDesc = params.orderByDesc;
		        }
		        else {
		            vm.orderByDesc = false;
		        }
		    }
		    
		    $http.get('/api/conference', {
		        params: params
		    })
            .then(function (response) {                
                if (angular.isArray(response.data)) {
                    vm.listConference = response.data;
                    vm.orderBy = params.orderBy;
                   
                }
                else {
                    vm.listConference = [];
                }
            })
            .catch(function () {
                vm.listConference = [];
            })
            .finally(function () {
                vm.loadingListConference = false;
            });
		}

		getListConference(null);
	}
})();
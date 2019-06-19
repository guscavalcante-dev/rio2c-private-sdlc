(function () {
    'use strict';

    angular
        .module('OneToOneMeetings')        
        .controller('OneToOneMeetingsTest', OneToOneMeetingsTest);

    OneToOneMeetingsTest.$inject = ['$scope', '$http', '$element', 'OneToOneMeetingsService'];

    function OneToOneMeetingsTest($scope, $http, $element, OneToOneMeetingsService) {
        var vm = this;

        //////////////////////////ATTRIBUTES////////////////////////////////////        

        vm.processingScheduling = false;                
        vm.numberScheduledNegotiations = 0;
        vm.numberUnscheduledNegotiations = 0;
        vm.unscheduledNegotiations = [];
        vm.dateProcess = null;
        vm.processedScheduling = false;
        vm.processingScheduling = false;
        vm.numberScheduledNegotiations = 0;
        vm.numberUnscheduledNegotiations = 0;
        vm.unscheduledNegotiations = [];
        vm.dateProcess = null;
        vm.processedScheduling = false;

        //////////////////////////////METHODS///////////////////////////////////

        vm.generateScheduleOneToOneMeetings = generateScheduleOneToOneMeetings;

        ////////////////////////////////////////////////////////////////////////

        function generateScheduleOneToOneMeetings() {
            vm.processedScheduling = true;
            vm.processingScheduling = true;

            $http.post('/api/scheduleonetoonemeetings/generateTemp')
            .then(function (response) {
                console.info(response);

                if (angular.isDefined(response.data.data)) {
                    angular.extend(vm, response.data.data);
                    vm.groupNegotiations = response.data.data.dates.dates;
                }
            })
            .catch(function (error) {
                console.error(error);
            })
            .finally(function () {
                vm.processingScheduling = false;
                angular.element('#modalGenerateScheduleOneToOneMeetings').modal('hide');
            });
        }
     
    }
})();
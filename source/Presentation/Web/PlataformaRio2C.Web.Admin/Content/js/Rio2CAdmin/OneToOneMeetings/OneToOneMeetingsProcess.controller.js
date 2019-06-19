(function () {
    'use strict';

    angular
        .module('OneToOneMeetings')        
        .controller('OneToOneMeetingsProcess', OneToOneMeetingsProcess);

    OneToOneMeetingsProcess.$inject = ['$scope', '$http', '$element', 'OneToOneMeetingsService'];

    function OneToOneMeetingsProcess($scope, $http, $element, OneToOneMeetingsService) {
        var vm = this;

        //////////////////////////ATTRIBUTES////////////////////////////////////        

        vm.processingScheduling = false;                
        vm.numberScheduledNegotiations = 0;
        vm.numberUnscheduledNegotiations = 0;
        vm.unscheduledNegotiations = [];
        vm.dateProcess = null;
        vm.processedScheduling = false;
        vm.loading = false;

        //////////////////////////////METHODS///////////////////////////////////

        vm.generateScheduleOneToOneMeetings = generateScheduleOneToOneMeetings;

        ////////////////////////////////////////////////////////////////////////

        function generateScheduleOneToOneMeetings() {
            vm.processedScheduling = true;
            vm.processingScheduling = true;

            $http.post('/api/scheduleonetoonemeetings/process')
            .then(function (response) {
                console.info(response);

                if (angular.isDefined(response.data.data)) {
                    angular.extend(vm, response.data.data);
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

        function loadResultProcess() {
            vm.loading = true;
            vm.processedScheduling = true;
            vm.processingScheduling = true;

            $http.get('/api/scheduleonetoonemeetings/process')
            .then(function (response) {
                console.info(response);

                if (angular.isDefined(response.data)) {                    
                    angular.extend(vm, response.data);
                }
            })
            .catch(function (error) {
                console.error(error);
                vm.processedScheduling = false;
            })
            .finally(function () {
                vm.processingScheduling = false;
                vm.loading = false;
            });
        }

        loadResultProcess();
    }
})();
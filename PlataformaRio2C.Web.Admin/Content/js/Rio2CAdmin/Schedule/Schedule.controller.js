(function () {
    'use strict';

    angular
        .module('Schedule')
        .controller('ScheduleCtrl', ScheduleCtrl);

    ScheduleCtrl.$inject = ['$scope', '$http', '$element', '$log', '$uibModal', '$q'];

    function ScheduleCtrl($scope, $http, $element, $log, $uibModal, $q) {
        var vm = this;

        vm.uidCollaborator = $element.context.dataset.uidCollaborator;
        vm.area = $element.context.dataset.area;

        vm.loadingSchedule = false;
        vm.processedError = false;
        vm.messageResultProcessing = {};

        vm.hours = [];
        vm.daysObjectList = [];
        vm.eventItems = [];
        vm.eventsList = [];

        //////////////////////////////////////////

        vm.loadComplete = loadComplete;
        vm.init = init;

        //////////////////////////////////////////

        function loadData() {

            if (angular.isDefined(vm.area) && vm.area === "player") {
                return loadPlayer();
            }
            else if (angular.isDefined(vm.area) && vm.area === "producer") {
                return loadProducer();
            }
            else {
                return loadComplete();
            }
        }

        function loadComplete() {
            vm.loadingSchedule = true;
            return $http.get('/api/scheduleonetoonemeetings/completeschedule',
                {
                    params: {
                        uidCollaborator: vm.uidCollaborator
                    }
                })
                .then(function (response) {
                    vm.eventsList = response.data;
                })
            .catch(function () {
            })
            .finally(function () {
                vm.loadingSchedule = false;
            });
        }

        function loadPlayer() {
            vm.loadingSchedule = true;
            return $http.get('/api/scheduleonetoonemeetings/scheduleplayer',
                {
                    params: {
                        uidCollaborator: vm.uidCollaborator
                    }
                })
                .then(function (response) {
                    vm.eventsList = response.data;
                })
                .catch(function () {
                })
            .finally(function () {
                vm.loadingSchedule = false;
            });
        }

        function loadProducer() {
            vm.loadingSchedule = true;
            return $http.get('/api/scheduleonetoonemeetings/scheduleproducer',
               {
                   params: {
                       uidCollaborator: vm.uidCollaborator
                   }
               })
                .then(function (response) {
                    vm.eventsList = response.data;
                })
                .catch(function () {
                })
            .finally(function () {
                vm.loadingSchedule = false;
            });
        }


        /////////////////////////////////////////////////////////////////       

        /*METHODS*/

        vm.getHours = getHours;
        vm.getDays = getDays;
        vm.getEvent = getEvent;
        vm.setSpan = setSpan;
        vm.showModalAgenda = showModalAgenda;


        /*METHODS IMPLEMENTATIONS*/
        function init() {
            vm.loadingSchedule = true;

            $q.all([
                getDays(),
                getHours(),
                loadData()
            ])
                .then()
                .catch()
                .finally(function () {
                    vm.loadingSchedule = false;
                })
        }

        function getEvent(date, hour) {
            var result = [];

            if (angular.isDefined(vm.eventsList) && angular.isArray(vm.eventsList)) {

                var tmpResultByDate = vm.eventsList.filter(el => el.date === date).map(el => el.items)[0];

                if (angular.isDefined(tmpResultByDate)) {
                    //result = tmpResultByDate.filter(el => el.startHour === hour);

                    result = tmpResultByDate.filter(function (i) {
                        var startTimeMoment = moment(i.startHour, "hh:mm");
                        var endTimeMoment = moment(i.endHour, "hh:mm");
                        var currentMoment = moment(hour, "hh:mm");
                        var currentMomentAdd = moment(hour, "hh:mm").add(30, 'm');

                        if (currentMoment.isSame(startTimeMoment) || startTimeMoment.isBetween(currentMoment, currentMomentAdd)) {
                            return true;
                        }

                        //if (currentMoment.isBetween(startTimeMoment, endTimeMoment)) {                            
                        //    return true;
                        //}     
                    });
                }
            }

            return result;
        }

        function getHours() {
            for (let i = 10; i <= 24; i++) {
                let num = i;
                if (i >= 24) {
                    num = i;
                    num = i - 24;
                }
                if (num < 10)
                    num = "0" + num;

                if (i == 24) {
                    vm.hours.push(num + ":" + "00");
                } else {
                    vm.hours.push(num + ":" + "00");
                    vm.hours.push(num + ":" + "30");
                }
            }
        }

        function getDays() {
            vm.daysObjectList = [];


            return $http.get('/api/scheduleonetoonemeetings/days')
                .then(function (response) {
                    if (angular.isArray(response.data)) {
                        vm.daysObjectList = response.data;
                    }
                })
                .catch(function () {

                })
                .finally(function () {

                });
        }

        function setSpan(duration) {

            let height = 39,
                durIncrement = 30,
                hightIncrement = 40;


            let spanIncrementer = function (duration) {
                durIncrement += 30;
                hightIncrement += 40;
                if (duration <= 30)
                    return height;

                else if (duration > 30 && duration < 60)
                    return height += hightIncrement;

                else if (duration >= durIncrement && duration < durIncrement + 30)
                    return height += hightIncrement;

                else
                    return spanIncrementer(duration);

            }

            height = spanIncrementer(duration);

            return "height:" + height + "px;";
            //return "height:" + 39 + "px;";
        }

        function showModalAgenda(item, date) {
            vm.messageResultProcessing = {};
            vm.thisItem = item;
            vm.thisDate = date;
            vm.modalAgenda = $uibModal.open({
                animation: true,
                ariaLabelledBy: 'modal-title-bottom',
                ariaDescribedBy: 'modal-body-bottom',
                templateUrl: 'modalAgenda.html',
                size: 'md',
                scope: $scope,
                controllerAs: 'vmModalAgenda',
                windowClass: 'modal-messages-success',
                controller: function () {
                    $scope.dismiss = function () {
                        vm.modalAgenda.dismiss('cancel');

                    }
                    return vm;
                }
            });
        }
    }
})();
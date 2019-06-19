(function () {
    'use strict';

    angular
        .module('OneToOneMeetings')
        .controller('OneToOneMeetingsRegisterManualCtrl', OneToOneMeetingsRegisterManualCtrl);

    OneToOneMeetingsRegisterManualCtrl.$inject = ['$scope', '$http', '$element', '$uibModal', '$timeout', '$alert', '$q'];

    function OneToOneMeetingsRegisterManualCtrl($scope, $http, $element, $uibModal, $timeout, $alert, $q) {
        var vm = this;

        //////////////////////////ATTRIBUTES////////////////////////////////////        
        vm.loadingFormOptions = true;
        vm.playerSelected = null;
        vm.dateSelected = null;

        vm.optionsDates = [];

        const UIDEMPTY = '00000000-0000-0000-0000-000000000000';

        var viewmodel = JSON.parse($element.context.dataset.viewmodel);        

        vm.playerSelected = {
            uid: viewmodel.Player,
            name: viewmodel.PlayerName
        };

        vm.projectSelected = {
            uid: viewmodel.Project,
            title: viewmodel.ProjectName
        };

        vm.roomOptionsDefault = JSON.parse($element.context.dataset.roomOptions);
        vm.roomOptions = angular.copy(vm.roomOptionsDefault);
        vm.roomSelected = vm.roomOptions[vm.roomOptions.findIndex(i => i.uid == viewmodel.Room)];

        vm.startTimeOptionsDefault = JSON.parse($element.context.dataset.startTimeOptions);
        vm.startTimeOptions = angular.copy(vm.startTimeOptionsDefault);
        vm.startTimeSelected = vm.startTimeOptions[vm.startTimeOptions.findIndex(i => i == $element.context.dataset.startTimeSelected)];

        vm.tableOptionsDefault = JSON.parse($element.context.dataset.tableOptions);
        vm.tableOptions = angular.copy(vm.tableOptionsDefault);
        vm.tableSelected = vm.tableOptions[0];

        vm.loadingFormOptions = true;
        vm.loadingRooms = false;
        vm.loadingOptionsStartTime = false;
        vm.loadingOptionsTables = false;



        //////////////////////////////METHODS///////////////////////////////////

        vm.getPlayers = getPlayers;
        vm.getProjects = getProjects;
        vm.getOptionsTables = getOptionsTables;

        ////////////////////////////////////////////////////////////////////////

        $scope.$watch(function () {
            return vm.dateSelected;
        }, function (newValue, oldValue) {
            if (newValue !== oldValue) {
                getOptionsRoomsByDate(newValue);
                getOptionsStartTime(newValue);
            }
        });

        $scope.$watch(function () {
            return vm.roomSelected;
        }, function (newValue, oldValue) {
            if (newValue !== oldValue) {
                vm.startTimeSelected = vm.startTimeOptions[0];
                vm.tableOptions = [];
            }
        });

        $scope.$watch(function () {
            return vm.startTimeSelected;
        }, function (newValue, oldValue) {

            getOptionsTables(newValue);

        })

        function init() {


            $q.all([
                 getOptionsDates()
            ])
                .then()
                .catch()
                .finally(function () {
                    vm.loadingFormOptions = false;
                })
        }

        function getOptionsDates(term) {
            $http.get('/api/scheduleonetoonemeetings/getoptionsdates')
            .then(function (response) {
                if (angular.isArray(response.data)) {
                    if (angular.isArray(response.data)) {
                        angular.forEach(response.data, function (item) {
                            vm.optionsDates.push(item);
                        });

                        if (viewmodel.DateFormat != null) {
                            vm.dateSelected = vm.optionsDates[vm.optionsDates.findIndex(i => i == viewmodel.DateFormat)];
                        }
                        else {
                            vm.dateSelected = vm.optionsDates[0];
                        }
                    }
                }
            })
            .catch(function () {
            });
        }

        function getOptionsRoomsByDate(term) {
            vm.loadingRooms = true;

            vm.roomOptions = angular.copy(vm.roomOptionsDefault);
            vm.roomSelected = vm.roomOptions[0];

            $http.get('/api/scheduleonetoonemeetings/getoptionsroomsbydate', {
                params: {
                    date: term
                }
            })
            .then(function (response) {
                if (angular.isArray(response.data)) {
                    if (angular.isArray(response.data)) {
                        angular.forEach(response.data, function (item) {
                            vm.roomOptions.push(item);
                        });

                        vm.roomSelected = vm.roomOptions[vm.roomOptions.findIndex(i => i.uid == viewmodel.Room)];

                        getOptionsTables(null);
                    }
                }
            })
            .catch(function () {
            })
            .finally(function () {
                vm.loadingRooms = false;
            });
        }

        function getOptionsStartTime(term) {
            vm.loadingOptionsStartTime = true;

            vm.startTimeOptions = angular.copy(vm.startTimeOptionsDefault);
            vm.startTimeSelected = vm.startTimeOptions[0];

            $http.get('/api/scheduleonetoonemeetings/getoptionsstarttime', {
                params: {
                    date: term
                }
            })
            .then(function (response) {
                if (angular.isArray(response.data)) {
                    if (angular.isArray(response.data)) {
                        angular.forEach(response.data, function (item) {
                            vm.startTimeOptions.push(item);
                        });


                        if (viewmodel.StarTimeFormat != null) {
                            vm.startTimeSelected = vm.startTimeOptions[vm.startTimeOptions.findIndex(i => i == viewmodel.StarTimeFormat)];
                        }
                        else {
                            vm.startTimeSelected = vm.startTimeOptions[0];
                        }
                    }
                }
            })
            .catch(function () {

            })
            .finally(function () {
                vm.loadingOptionsStartTime = false;
            });
        }

        function getOptionsTables(startTime) {          
            if (startTime === null) {
                startTime = vm.startTimeSelected;
            }

            if (angular.isUndefined(vm.roomSelected) || vm.roomSelected.uid === UIDEMPTY || startTime === 'Selecione') {                
                return true;
            }

            vm.loadingOptionsTables = true;

            vm.tableOptions = angular.copy(vm.tableOptionsDefault);
            vm.tableSelected = vm.tableOptions[0];

            $http.get('/api/scheduleonetoonemeetings/getoptionstables', {
                params: {
                    date: vm.dateSelected,
                    startTime: startTime,
                    room: vm.roomSelected.uid
                }
            })
           .then(function (response) {
               if (angular.isArray(response.data)) {
                   if (angular.isArray(response.data)) {
                       angular.forEach(response.data, function (item) {
                           vm.tableOptions.push(item);
                       });

                       if (viewmodel.Table > 0) {
                           vm.tableSelected = vm.tableOptions[vm.tableOptions.findIndex(i => i == viewmodel.Table)];
                       }
                       else {
                           vm.tableSelected = vm.tableOptions[0];
                       }                       
                   }
               }
           })
           .catch(function () {
           })
           .finally(function () {
               vm.loadingOptionsTables = false;
           });
        }


        function getPlayers(term) {
            return $http.get('/api/players/GetAllOptions', {
                params: {
                    name: term
                }
            })
            .then(function (response) {
                if (angular.isArray(response.data)) {
                    return response.data;
                }
                else {
                    return [];
                }
            })
            .catch(function () {
                return [];
            });
        }

        function getProjects(term) {
            return $http.get('/api/project/GetAllOptions', {
                params: {
                    term: term
                }
            })
           .then(function (response) {
               if (angular.isArray(response.data)) {
                   return response.data;
               }
               else {
                   return [];
               }
           })
           .catch(function () {
               return [];
           });
        }

        function getRoomOptions() {
            return $http.get('/api/room', {

            })
                .then(function (response) {

                    if (angular.isArray(response.data)) {
                        angular.forEach(response.data, function (item) {
                            vm.roomOptions.push(item);

                        });


                    }
                })
                .catch(function () {
                });
        }



        init();
    }




})();
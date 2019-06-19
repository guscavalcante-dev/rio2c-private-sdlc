(function () {
    'use strict';

    angular
        .module('OneToOneMeetings')
        .controller('OneToOneMeetingsReportCtrl', OneToOneMeetingsReportCtrl);

    OneToOneMeetingsReportCtrl.$inject = ['$scope', '$http', '$element', '$uibModal', '$timeout', '$alert'];

    function OneToOneMeetingsReportCtrl($scope, $http, $element, $uibModal, $timeout, $alert) {
        var vm = this;

        vm.loadingNegotiations = false;
        vm.groupNegotiations = [];
        vm.groupAllNegotiations = [];

        vm.optionsDate = [];
        vm.optionsRoom = [];

        //////////////////////////ATTRIBUTES////////////////////////////////////        

    
        //////////////////////////////METHODS///////////////////////////////////

        vm.filterTablesBySlot = filterTablesBySlot;

        vm.filterSchedule = filterSchedule;
        vm.clearFilter = clearFilter;

        ////////////////////////////////////////////////////////////////////////
       
        function accentFold(inStr) {
            return inStr.replace(/([àáâãäå])|([ç])|([èéêë])|([ìíîï])|([ñ])|([òóôõöø])|([ß])|([ùúûü])|([ÿ])|([æ])/g,
                function (str, a, c, e, i, n, o, s, u, y, ae) {
                    if (a) return 'a'; else if (c) return 'c'; else if (e) return 'e'; else if (i) return 'i'; else if (n) return 'n'; else if (o) return 'o'; else if (s) return 's'; else if (u) return 'u'; else if (y) return 'y'; else if (ae) return 'ae';
                });
        }

        function filterSchedule() {
            vm.groupNegotiations = angular.copy(vm.groupAllNegotiations);

            var listFiltred = angular.copy(vm.groupNegotiations);

            if (vm.searchPlayer != null) {
                listFiltred = vm.groupNegotiations.filter(function (i) {
                    i.rooms = i.rooms.filter(function (r) {
                        r.tables = r.tables.filter(function (t) {
                            t.negotiations = t.negotiations.filter(function (n) {
                                return accentFold(n.player.toLowerCase()).indexOf(accentFold(vm.searchPlayer.toLowerCase())) !== -1;
                            });
                            return t.negotiations.length > 0;
                        });

                        return r.tables.length > 0;
                    });
                    return i.rooms.length > 0;
                });
            }

            if (vm.searchProject != null) {
                listFiltred = vm.groupNegotiations.filter(function (i) {      
                    i.rooms = i.rooms.filter(function (r) {
                        r.tables = r.tables.filter(function (t) {
                            t.negotiations = t.negotiations.filter(function (n) {
                                return accentFold(n.project.toLowerCase()).indexOf(accentFold(vm.searchProject.toLowerCase())) !== -1;
                            });
                            return t.negotiations.length > 0;
                        });

                        return r.tables.length > 0;
                    });
                    return i.rooms.length > 0;
                });
            }

            if (vm.searchProducer != null) {
                listFiltred = vm.groupNegotiations.filter(function (i) {
                    i.rooms = i.rooms.filter(function (r) {
                        r.tables = r.tables.filter(function (t) {
                            t.negotiations = t.negotiations.filter(function (n) {
                                return accentFold(n.producer.toLowerCase()).indexOf(accentFold(vm.searchProducer.toLowerCase())) !== -1;
                            });
                            return t.negotiations.length > 0;
                        });

                        return r.tables.length > 0;
                    });
                    return i.rooms.length > 0;
                });
            }

            if (vm.searchRoom != null) {
                listFiltred = vm.groupNegotiations.filter(function (i) {
                    i.rooms = i.rooms.filter(function (r) {
                        return r.room === vm.searchRoom;
                    });
                    return i.rooms.length > 0;
                });
            }

            if (vm.searchDate != null) {
                listFiltred = vm.groupNegotiations.filter(function (i) {
                    return i.date === vm.searchDate;
                });

            }

            vm.groupNegotiations = listFiltred;

            //console.log(vm.groupNegotiations);
            vm.counter(vm.groupNegotiations);

        }

        function clearFilter() {
            vm.searchPlayer = null;
            vm.searchProject = null;
            vm.searchProducer = null;
            vm.searchDate = null;
            vm.searchRoom = null;

            filterSchedule();
        }

        vm.countTables;

        vm.counter = counter;

        function counter(groupNegotiations) {
            let auxArr = [];

            //groupNegotiations
            //    .map(el => el.rooms)
            //    .forEach(el => {
            //        let i = 0;
            //        for (; i < el.length; i++) {
            //            auxArr.push(el[i]);
            //        }

            //    });

            //vm.countTables = auxArr.filter(el => el).map(el => el.slots);

            //auxArr = [];

            //vm.countTables.forEach(el => {
            //    let i = 0;
            //    for (; i < el.length; i++) {
            //        auxArr.push(el[i]);
            //    }
            //});

            //vm.countTables = [];

            //auxArr.map(el => el.tables).forEach(el => {
            //    let i = 0;
            //    for (; i < el.length; i++) {
            //        vm.countTables.push(el[i]);
            //    }
            //});

        }

        function filterTablesBySlot(list, slotfilter) {
            if (angular.isArray(list)) {
                return list.filter(function (i) { return i.slot === slotfilter});
            }

            return [];
        }


        function getDateOptions() {
            vm.optionsDate = [];
            if (vm.groupAllNegotiations != null && vm.groupAllNegotiations.length > 0) {
                angular.forEach(vm.groupAllNegotiations, function (i) {
                    if (vm.optionsDate.indexOf(i.date) === -1) {
                        vm.optionsDate.push(i.date);
                    }
                });
            }
        }

        function getRoomOptions() {
            vm.optionsRoom = [];
            if (vm.groupAllNegotiations != null && vm.groupAllNegotiations.length > 0) {
                angular.forEach(vm.groupAllNegotiations, function (i) {
                    angular.forEach(i.rooms, function (r) {
                        if (vm.optionsRoom.indexOf(r.room) === -1) {
                            vm.optionsRoom.push(r.room);
                        }
                    })
                });
            }
        }

        function loadOptionsFilter() {
            getDateOptions();
            getRoomOptions();
        }

        

        function loadNegotations() {
            vm.loadingNegotiations = true;

            $http.get('/api/scheduleonetoonemeetings/completeschedulegroupbytable')
                .then(function (response) {
                    vm.groupNegotiations = response.data.dates;
                    vm.groupAllNegotiations = angular.copy(vm.groupNegotiations);
                    loadOptionsFilter();
                })
                .catch(function (error) {
                    console.error(error);
                })
                .finally(function () {
                    vm.loadingNegotiations = false;
                });
        }

        loadNegotations();
    }
})();
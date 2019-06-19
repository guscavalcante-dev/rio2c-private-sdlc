(function () {
    'use strict';

    angular
        .module('OneToOneMeetings')
        .controller('OneToOneMeetingsCtrl', OneToOneMeetingsCtrl);

    OneToOneMeetingsCtrl.$inject = ['$scope', '$http', '$element', '$uibModal', '$timeout', '$alert'];

    function OneToOneMeetingsCtrl($scope, $http, $element, $uibModal, $timeout, $alert) {
        var vm = this;

        //////////////////////////ATTRIBUTES////////////////////////////////////        

        vm.loadingNegotiations = false;
        vm.negotiations = [];
        vm.groupNegotiations = [];
        vm.optionsDate = [];
        vm.optionsRoom = [];
        vm.filterStatus = true;
        vm.filterChanged = 'show-filter';

        //////////////////////////////METHODS///////////////////////////////////
        vm.showHideFilter = showHideFilter;
        vm.showModalDeleteNegotiation = showModalDeleteNegotiation;
        vm.deleteNegotiation = deleteNegotiation;

        vm.filterSchedule = filterSchedule;
        vm.clearFilter = clearFilter;

        ////////////////////////////////////////////////////////////////////////


        function showHideFilter() {
            vm.filterStatus = !vm.filterStatus;
            vm.filterChanged = vm.filterChanged === 'show-filter' ? 'hide-filter' : 'show-filter';

        }

        function clearFilter() {
            vm.searchPlayer = null;
            vm.searchProject = null;
            vm.searchProducer = null;
            vm.searchDate = null;
            vm.searchRoom = null;

            filterSchedule();
        }

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
                        r.slots = r.slots.filter(function (s) {
                            s.tables = s.tables.filter(function (t) {
                                return accentFold(t.player.toLowerCase()).indexOf(accentFold(vm.searchPlayer.toLowerCase())) !== -1;
                            });
                            return s.tables.length > 0;
                        });
                        return r.slots.length > 0;
                    });
                    return i.rooms.length > 0;
                });
            }

            if (vm.searchProject != null) {
                listFiltred = vm.groupNegotiations.filter(function (i) {
                    i.rooms = i.rooms.filter(function (r) {
                        r.slots = r.slots.filter(function (s) {
                            s.tables = s.tables.filter(function (t) {
                                return accentFold(t.project.toLowerCase()).indexOf(accentFold(vm.searchProject.toLowerCase())) !== -1;
                            });
                            return s.tables.length > 0;
                        });
                        return r.slots.length > 0;
                    });
                    return i.rooms.length > 0;
                });
            }

            if (vm.searchProducer != null) {
                listFiltred = vm.groupNegotiations.filter(function (i) {
                    i.rooms = i.rooms.filter(function (r) {
                        r.slots = r.slots.filter(function (s) {
                            s.tables = s.tables.filter(function (t) {
                                return accentFold(t.producer.toLowerCase()).indexOf(accentFold(vm.searchProducer.toLowerCase())) !== -1;
                            });
                            return s.tables.length > 0;
                        });
                        return r.slots.length > 0;
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

        vm.countTables;

        vm.counter = counter;

        function counter(groupNegotiations) {
            let auxArr = [];  

            groupNegotiations
                .map(el => el.rooms)
                .forEach(el => {
                    let i = 0;
                    for (; i < el.length; i++) {
                        auxArr.push(el[i]);
                    }

                });

            vm.countTables = auxArr.filter(el => el).map(el => el.slots);

            auxArr = [];

            vm.countTables.forEach(el => {
                let i = 0;
                for (; i < el.length; i++) {
                    auxArr.push(el[i]);
                }
            });

            vm.countTables = [];

            auxArr.map(el => el.tables).forEach(el => {
                let i = 0;
                for (; i < el.length; i++) {
                    vm.countTables.push(el[i]);
                }
            });

        }

        function showModalDeleteNegotiation(negotiation) {

            vm.negotiationToDelete = negotiation;

            vm.modalDeleteNegotiation = $uibModal.open({
                animation: true,
                ariaLabelledBy: 'modal-title-bottom',
                ariaDescribedBy: 'modal-body-bottom',
                templateUrl: 'modalDeleteNegotiation.html',
                size: 'md',
                scope: $scope,
                controllerAs: 'vmModalDeleteNegotiation',
                windowClass: '',
                controller: function () {
                    $scope.dismiss = function () {
                        vm.modalDeleteNegotiation.dismiss('cancel');
                    }
                    return vm;
                }
            });
        }

        function deleteNegotiation(negotiation) {
            vm.messageResultProcessingDelete = {};
            vm.processingActionDelete = true;

            $http(
                {
                    method: 'POST',
                    url: '/api/scheduleonetoonemeetings/delete',
                    params: {
                        uid: negotiation.uid
                    }
                }
            )
                .then(function (response) {

                    removeNegotiationInList(negotiation);

                    $alert.show(vm.messageResultProcessingDelete, response.data, 'success');

                    $timeout(function () { vm.modalDeleteNegotiation.dismiss('cancel'); }, 1000);
                })
                .catch(function (error) {
                    console.error(error);
                })
                .finally(function () {
                    vm.processingActionDelete = false;
                });
        }

        function removeNegotiationInList(negotiation) {
            angular.forEach(vm.groupNegotiations, function (itemGroupDate) {
                angular.forEach(itemGroupDate.rooms, function (room) {
                    angular.forEach(room.slots, function (slot) {
                        angular.forEach(slot.tables, function (table, i) {
                            if (table.uid === negotiation.uid) {
                                slot.tables.splice(i, 1);
                            }
                        })
                    })
                })
            })
        }

        function getDateOptions() {
            if (vm.groupAllNegotiations != null && vm.groupAllNegotiations.length > 0) {
                angular.forEach(vm.groupAllNegotiations, function (i) {
                    if (vm.optionsDate.indexOf(i.date) === -1) {
                        vm.optionsDate.push(i.date);
                    }
                });
            }
        }

        function getRoomOptions() {
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

        function loadNegotations() {
            vm.loadingNegotiations = true;

            $http.get('/api/scheduleonetoonemeetings/list')
                .then(function (response) {
                    vm.groupNegotiations = response.data.dates;
                    vm.groupAllNegotiations = angular.copy(vm.groupNegotiations);
                    getDateOptions();
                    getRoomOptions();
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
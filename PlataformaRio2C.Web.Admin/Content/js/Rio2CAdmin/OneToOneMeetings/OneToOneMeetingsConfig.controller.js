(function () {
    'use strict';

    angular
        .module('OneToOneMeetings')
        .controller('OneToOneMeetingsConfig', OneToOneMeetingsConfig);

    OneToOneMeetingsConfig.$inject = ['$scope', '$http', '$element', '$q'];

    function OneToOneMeetingsConfig($scope, $http, $element, $q) {
        var vm = this;

        //////////////////////////ATTRIBUTES////////////////////////////////////             
        
        vm.dates = JSON.parse($element.context.dataset.viewmodel);
        vm.roomOptions = JSON.parse($element.context.dataset.roomOptions);
        vm.loadingFormOptions = false;

        //////////////////////////////METHODS///////////////////////////////////

        vm.addDate = addDate;
        vm.removeDate = removeDate;
        vm.addRoom = addRoom;
        vm.removeRoom = removeRoom;

        ////////////////////////////////////////////////////////////////////////////
        
        function init() {            
            vm.loadingFormOptions = true;

            $q.all([
                 getRoomOptions()
            ])
                .then()
                .catch()
                .finally(function () {
                    vm.loadingFormOptions = false;
                })
        }

       

        function addDate() {
            vm.dates.push(
                { 
                    CountSlotsFirstTurn: 0,
                    CountSlotsSecondTurn: 0,
                    Rooms:
                    [
                        { 
                            roomSelected: vm.roomOptions[0],
                            CountAutomaticTables: 0,
                            CountManualTables: 0
                        }
                    ]
                }
                );
        }

        function removeDate(index) {
            vm.dates.splice(index, 1);
        }

        function getRoomOptions() {
            return $http.get('/api/room', {

            })
                .then(function (response) {

                    if (angular.isArray(response.data)) {
                        angular.forEach(response.data, function (item) {
                            vm.roomOptions.push(item);

                        });

                        mapRooms();
                    }
                })
                .catch(function () {
                });
        }
        

        function mapRooms() {
            
            angular.forEach(vm.dates, function (date) {
                angular.forEach(date.Rooms, function (room) {                    
                    room.roomSelected = vm.roomOptions[vm.roomOptions.findIndex(i => i.uid == room.RoomUid)];
                })
            })
        }

        function addRoom(date) {
            if (date.Rooms != null && angular.isArray(date.Rooms)) {
                date.Rooms.push(
                    {
                        roomSelected: vm.roomOptions[0],
                        CountAutomaticTables: 0,
                        CountManualTables: 0
                    }
                    );
            }
            else {
                date.Rooms = [
                    {
                        roomSelected: vm.roomOptions[0],
                        CountAutomaticTables: 0,
                        CountManualTables: 0
                    }
                ];
            }
        }

        function removeRoom(date, index) {
            date.Rooms.splice(index, 1);
        }

        init();
    }
})();
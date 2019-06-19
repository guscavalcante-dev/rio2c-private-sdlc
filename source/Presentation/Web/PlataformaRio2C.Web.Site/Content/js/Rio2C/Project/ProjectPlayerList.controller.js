(function () {
    'use strict';

    angular
    .module('Project')
    .controller('ProjectPlayerListController', ProjectPlayerListController);

    ProjectPlayerListController.$inject = ['$http', '$scope', '$uibModal', '$alert', '$filter'];

    function ProjectPlayerListController($http, $scope, $uibModal, $alert, $filter) {
        const NAMESTORAGEFILTER = "ProjectPlayerListControllerFilter";

        var vm = this;

        vm.projects = [];
        vm.playersOptions = [];        
        vm.genresOptions = [];
        vm.statusOptions = [];

        vm.loadingProjects = true;
        vm.loadingOptions = true;
        
        vm.messageLoadProject = {};        
        vm.filter = loadFilter();
        vm.isFiltered = isFiltered;
        
        //////////////////////////////////////////////////

        vm.loadProjects = loadProjects;
        vm.filterByPlayer = filterByPlayer;
        vm.filterByGenre = filterByGenre;
        vm.filterByStatus = filterByStatus;
        vm.getPlayerNameByUid = getPlayerNameByUid;

        vm.getNamesPlayersFiltered = getNamesPlayersFiltered;
        vm.getTextStatusFiltered = getTextStatusFiltered;

        //////////////////////////////////////////////////

        $scope.$watch(function () {
            return vm.filter;
        }, function (newValue, oldValue) {            
            if (newValue != oldValue) {                
                UpdateFilterStorage(newValue);                
            }
        });

        //////////////////////////////////////////////////

        function getTextStatusFiltered() {            
            var statusFiltered = $filter('filter')(vm.statusOptions, { selected: true });

            if (statusFiltered != null) {
                return statusFiltered.map(function (item) { return item.text }).join(',');
            }

            return null;
        }

        function getNamesPlayersFiltered() {
            var playersFiltered = $filter('filter')(vm.playersOptions, { selected: true });

            if (playersFiltered != null) {
                return playersFiltered.map(function (item) { return item.name }).join(',');
            }

            return null;
        }

        function getPlayerNameByUid(uid) {
            return vm.playersOptions.filter(function (item) { return item.uid == uid }).map(function (m) { return m.name });
        }

        function isFiltered() {            
            return angular.isDefined(vm.filter.Players) || angular.isDefined(vm.filter.Genres) || angular.isDefined(vm.filter.Status);
        }

        function loadFilter() {
            return angular.isObject(JSON.parse(localStorage.getItem(NAMESTORAGEFILTER))) ? JSON.parse(localStorage.getItem(NAMESTORAGEFILTER)) : {};
        }

        function UpdateFilterStorage(value) {                        
            localStorage.setItem(NAMESTORAGEFILTER, JSON.stringify(value));
        }

        function updateOptionsPlayer() {            
            if (angular.isDefined(vm.filter) && angular.isDefined(vm.filter.Players)) {
                angular.forEach(vm.playersOptions, function (item) {                   
                    item.selected = vm.filter.Players.filter(function (fItem) { return fItem == item.uid }).length > 0;
                });
            }           
        }

        function updateOptionsGenre() {
            if (angular.isDefined(vm.filter) && angular.isDefined(vm.filter.Genres)) {
                angular.forEach(vm.genresOptions, function (item) {
                    item.selected = vm.filter.Genres.filter(function (fItem) { return fItem == item.name }).length > 0;
                });
            }
        }

        function updateOptionsStatus() {          
            if (angular.isDefined(vm.filter) && angular.isDefined(vm.filter.Status)) {
                if (vm.filter.Status.filter(function (fItem) { return fItem == "All" }).length > 0) {
                    delete vm.filter.Status;
                }
                else {
                    angular.forEach(vm.statusOptions, function (item) {
                        item.selected = vm.filter.Status.filter(function (fItem) { return fItem == item.value }).length > 0;
                    });
                }
            }
        }

        function filterByPlayer(e, playerOption) {

            angular.forEach(vm.playersOptions, function (item) {
                item.selected = false;
            });

            if (playerOption !== null) {
                playerOption.selected = true;
            }

            var playersSelected = vm.playersOptions.filter(function (item) { return item.selected; });
            if (playersSelected != null && playersSelected.length > 0) {
                vm.filter.Players = playersSelected.map(function (item) { return item.uid; });
            }
            else {
                delete vm.filter.Players;
            }

            loadProjects();
        }

        function filterByGenre(e, genreOption) {

            angular.forEach(vm.genresOptions, function (item) {
                item.selected = false;
            });

            if (genreOption !== null) {
                genreOption.selected = true;
            }

            var genresSelected = vm.genresOptions.filter(function (item) { return item.selected; });
            if (genresSelected != null && genresSelected.length > 0) {
                vm.filter.Genres = genresSelected.map(function (item) { return item.name; });
            }
            else {
                delete vm.filter.Genres;
            }

            loadProjects();
        }

        function filterByStatus(e, statusOption) {

            angular.forEach(vm.statusOptions, function (item) {
                item.selected = false;
            });

            if (statusOption.value == "All") {
                delete vm.filter.Status;
            }
            else {                

                if (statusOption != null) {
                    statusOption.selected = true;
                }

                var statusSelected = vm.statusOptions.filter(function (item) { return item.selected; });
                if (statusSelected != null && statusSelected.length > 0) {
                    vm.filter.Status = statusSelected.map(function (item) { return item.value; });
                }
                else {
                    delete vm.filter.Status;
                }
            }          

            loadProjects();
        }

        function loadProjects() {
            vm.loadingProjects = true;
            vm.messageLoadProject = {};

            $http({
                method: 'GET',
                url: '/api/projects/getallbyuserplayerid',
                params: vm.filter
            })
            .then(function (response) {
                if (angular.isArray(response.data)) {
                    vm.projects = response.data;
                }
            })
            .catch(function (error) {
                console.error(error);
                vm.projects = [];
            })
            .finally(function () {
                vm.loadingProjects = false;
                vm.loadingOptions = false;
                UpdateFilterStorage(vm.filter);
            })
        }

        function loadPlayersOptions() {
            $http({
                method: 'GET',
                url: '/api/players/getalloptionbyuser',
            })
           .then(function (response) {
               if (angular.isArray(response.data)) {
                   vm.playersOptions = response.data;
                   updateOptionsPlayer();
                   loadGenresOptions();                   
               }
           })
           .catch(function (error) {
               console.error(error);
               vm.playersOptions = [];
           })
           .finally(function () {               
           })
        }

        function loadGenresOptions() {
            $http({
                method: 'GET',
                url: '/api/interests/genres',
                params: vm.filter
            })
              .then(function (response) {
                  if (angular.isArray(response.data)) {
                      vm.genresOptions = response.data;
                      updateOptionsGenre();
                      loadStatusOptions();                      
                  }
              })
              .catch(function () {
                  vm.genresOptions = [];
              })
              .finally(function () {
                  
              });
        }

        function loadStatusOptions() {
            $http({
                method: 'GET',
                url: '/api/projects/getstatusoption',
                params: vm.filter
            })
            .then(function (response) {
                if (angular.isArray(response.data)) {
                    vm.statusOptions = response.data;
                    updateOptionsStatus();
                    loadProjects();
                }
            })
            .catch(function () {
                vm.statusOptions = [];
            })
            .finally(function () {
            });
        }


        loadPlayersOptions();
        
    }
})();
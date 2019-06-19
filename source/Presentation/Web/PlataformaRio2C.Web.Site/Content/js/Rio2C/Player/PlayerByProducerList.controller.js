(function () {
    'use strict';

    angular
    .module('Player')
    .controller('PlayerByProducerListController', PlayerByProducerListController);

    PlayerByProducerListController.$inject = ['$http', '$scope', '$uibModal', '$alert'];

    function PlayerByProducerListController($http, $scope, $uibModal, $alert) {
        var vm = this;

        vm.groupPlayers = [];
        vm.loadedOptionsPlayers = false;
        vm.loadingPlayers = false;
        vm.genresOptions = [];
        vm.genreSelected = null;

        //////////////////////////////////////////////////

        vm.loadPlayers = loadPlayers;
        vm.filterByGenre = filterByGenre;

        //////////////////////////////////////////////////        

        function filterByGenre(e, value) {
            e.preventDefault();

            console.info('filterByGenre', value);

            vm.genreSelected = value;

            if (value !== null && value !== "") {
                vm.filter = {
                    Genres: [value]
                };
            }
            else {
                vm.filter = {};
            }

            loadPlayers();
            
        }

        loadGenresOptions();

        function loadGenresOptions() {
            vm.loadingPlayers = true;

            $http({
                method: 'GET',
                url: '/api/interests/genres',
                params: vm.filter
            })
               .then(function (response) {
                   if (angular.isArray(response.data)) {
                       vm.genresOptions = response.data;

                       loadPlayers();
                   }
               })
               .catch(function () {
                   vm.genresOptions = [];
               })
               .finally(function () {
               });
        }

        function loadPlayers() {
            vm.loadingPlayers = true;

            $http({
                method: 'GET',
                url: '/api/players/getallwithgenresgroupbyholding',
                params: vm.filter
            })
               .then(function (response) {
                   if (angular.isArray(response.data)) {
                       vm.groupPlayers = response.data;


                   }
               })
               .catch(function () {

                   vm.groupPlayers = [];
               })
               .finally(function () {
                   vm.loadedOptionsPlayers = true;
                   vm.loadingPlayers = false;
               });
        }
    }
})();
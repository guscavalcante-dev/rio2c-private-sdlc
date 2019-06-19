(function () {
    'use strict';

    angular
        .module('OneToOneMeetings')
        .controller('OneToOneMeetingsSendEmailCtrl', OneToOneMeetingsSendEmailCtrl);

    OneToOneMeetingsSendEmailCtrl.$inject = ['$scope', '$http', '$element', '$uibModal', '$timeout', '$alert', '$filter'];

    function OneToOneMeetingsSendEmailCtrl($scope, $http, $element, $uibModal, $timeout, $alert, $filter) {
        var vm = this;

        //////////////////////////ATTRIBUTES////////////////////////////////////        

        vm.loadingPlayers = false;
        vm.players = [];
        vm.playersCount = 0;
        vm.producersCount = 0;

        vm.loadingProducers = false;
        vm.producers = [];

        vm.processingAction = false;
        vm.successAction = false;

        //////////////////////////////METHODS///////////////////////////////////

        vm.loadPlayers = loadPlayers;
        vm.loadProducers = loadProducers;

        vm.showModalConfirmation = showModalConfirmation;
        vm.sendToPlayers = sendToPlayers;
        vm.sendToProducers = sendToProducers;

        vm.checkAllProducers = checkAllProducers;
        vm.uncheckAllProducers = uncheckAllProducers;

        vm.checkAllPlayers = checkAllPlayers;
        vm.uncheckAllPlayers = uncheckAllPlayers;


        vm.getSelectedPlayers = getSelectedPlayers;
        vm.getSelectedProducers = getSelectedProducers;
        ////////////////////////////////////////////////////////////////////////     

        function getSelectedProducers() {

            vm.producersCount = vm.producers.filter(el => el.selected).length;

        }
        vm.getSelectedProducers();

        function getSelectedPlayers() {

            vm.playersCount = vm.players.filter(el => el.selected).length;

        }
        vm.getSelectedPlayers();

        function checkAllProducers() {
            if (vm.producers != null && angular.isArray(vm.producers)) {
                angular.forEach(vm.producers, function (p) {
                    p.selected = true;
                })
            }
            vm.getSelectedProducers();
        }

        function uncheckAllProducers() {
            if (vm.producers != null && angular.isArray(vm.producers)) {
                angular.forEach(vm.producers, function (p) {
                    p.selected = false;
                })
            }
            vm.getSelectedProducers();
        }

        function checkAllPlayers() {
            if (vm.players != null && angular.isArray(vm.players)) {
                angular.forEach(vm.players, function (p) {
                    p.selected = true;
                })
            }
            vm.getSelectedPlayers();
        }

        function uncheckAllPlayers() {
            if (vm.players != null && angular.isArray(vm.players)) {
                angular.forEach(vm.players, function (p) {
                    p.selected = false;
                })
            }
            vm.getSelectedPlayers();
        }


        function loadPlayers() {
            vm.loadingPlayers = true;
            vm.players = [];

            $http.get('/api/scheduleonetoonemeetings/players')
                .then(function (response) {
                    console.info(response);

                    if (angular.isArray(response.data)) {
                        vm.players = response.data;
                    }

                })
                .catch(function (error) {
                    console.error(error);
                    vm.players = [];
                })
                .finally(function () {
                    vm.loadingPlayers = false;
                });
        }

        function loadProducers() {
            vm.loadingProducers = true;
            vm.producers = [];

            $http.get('/api/scheduleonetoonemeetings/producers')
                .then(function (response) {
                    console.info(response);

                    if (angular.isArray(response.data)) {
                        vm.producers = response.data;
                    }

                })
                .catch(function (error) {
                    console.error(error);
                    vm.producers = [];
                })
                .finally(function () {
                    vm.loadingProducers = false;
                });
        }

        function showModalConfirmation() {
            vm.messageResultProcessing = {};
            vm.resultProcessing = null;
            vm.successAction = false;

            vm.modalConfirmationSend = $uibModal.open({
                animation: true,
                ariaLabelledBy: 'modal-title-bottom',
                ariaDescribedBy: 'modal-body-bottom',
                templateUrl: 'modalConfirmationSend.html',
                size: 'lg',
                scope: $scope,
                backdrop: 'static',
                controllerAs: 'vmModalAccept',
                windowClass: 'modal-messages-success',
                controller: function () {
                    $scope.dismiss = function () {
                        vm.modalConfirmationSend.dismiss('cancel');
                    }
                    return vm;
                }
            });
        }

        function sendToPlayers() {
            vm.successAction = false;
            vm.processingAction = true;
            vm.resultProcessing = null;

            $http({
                url: '/api/scheduleonetoonemeetings/sendEmailToPlayers',
                method: 'POST',
                data: {
                    Uids: $filter('filter')(vm.players, { selected: true }).map(i => i.uid)
                }
            })
                .then(function (response) {
                    console.info(response);

                    if (angular.isDefined(response.data.data)) {
                        vm.resultProcessing = response.data.data;
                        vm.successAction = true;
                    }

                })
                .catch(function (error) {
                    console.error(error);
                })
                .finally(function () {
                    vm.processingAction = false;
                })
        }

        function sendToProducers() {
            vm.successAction = false;
            vm.processingAction = true;
            vm.resultProcessing = null;

            $http({
                url: '/api/scheduleonetoonemeetings/sendEmailToProducers',
                method: 'POST',
                data: {
                    Uids: $filter('filter')(vm.producers, { selected: true }).map(i => i.uid)
                }
            })
                .then(function (response) {
                    console.info(response);

                    if (angular.isDefined(response.data.data)) {
                        vm.resultProcessing = response.data.data;
                        vm.successAction = true;
                    }

                })
                .catch(function (error) {
                    console.error(error);
                })
                .finally(function () {
                    vm.processingAction = false;
                })
        }

    }
})();
(function () {
    'use strict';

    angular
    .module('Project')
    .controller('ProjecPlayerEvaluationController', ProjecPlayerEvaluationController);

    ProjecPlayerEvaluationController.$inject = ['$element', '$http', '$scope', '$uibModal', '$alert', '$timeout'];

    function ProjecPlayerEvaluationController($element, $http, $scope, $uibModal, $alert, $timeout) {
        var vm = this;
        
        vm.players = JSON.parse($element.context.dataset.players);
        vm.processingAction = false;
        vm.messageResultProcessing = {};
        
        //////////////////////////////////////////////////

        vm.showModalAcceptProject = showModalAcceptProject;
        vm.showModalRejectProject = showModalRejectProject;

        vm.acceptProject = acceptProject;
        vm.rejectProject = rejectProject;

        //////////////////////////////////////////////////

        function showModalAcceptProject(player) {
            vm.messageResultProcessing = {};
            vm.playerToAccept = player;
            vm.modalAccept = $uibModal.open({
                animation: true,
                ariaLabelledBy: 'modal-title-bottom',
                ariaDescribedBy: 'modal-body-bottom',
                templateUrl: 'modalAccept.html',
                size: 'md',
                scope: $scope,
                controllerAs: 'vmModalAccept',
                windowClass: 'modal-messages-success',
                controller: function () {                    
                    $scope.dismiss = function () {
                        vm.modalAccept.dismiss('cancel');
                        vm.ErrosSavePlayerSelection = {};
                    }
                    return vm;
                }
            });
        }

        function showModalRejectProject(player) {
            vm.messageResultProcessing = {};
            vm.playerToReject = player;
            vm.modalReject = $uibModal.open({
                animation: true,
                ariaLabelledBy: 'modal-title-bottom',
                ariaDescribedBy: 'modal-body-bottom',
                templateUrl: 'modalReject.html',
                size: 'md',
                scope: $scope,
                controllerAs: 'vmModalReject',
                windowClass: 'modal-messages-success',
                controller: function () {
                    $scope.dismiss = function () {
                        vm.modalReject.dismiss('cancel');
                        vm.ErrosSavePlayerSelection = {};
                    }
                    return vm;
                }
            });
        }

        function acceptProject(player, projectUid) {
            vm.processingAction = true;
            vm.messageResultProcessing = {};            

            $http({
                method: 'POST',
                url: '/api/projects/' + projectUid + '/acceptbyplayer',
                data: {
                    Uid: player.Uid
                }
            })
             .then(function (response) {
                 console.info(response);

                 if (angular.isDefined(response.data.status)) {
                     player.Status = response.data.status;
                 }

                 if (angular.isDefined(response.data.statusCode)) {
                     player.StatusCode = response.data.statusCode;
                 }

                 if (angular.isDefined(response.data.message)) {
                     $alert.show(vm.messageResultProcessing, response.data.message, 'success');
                 }

                 if (angular.isDefined(vm.modalAccept)) {
                     $timeout(function () {
                         vm.modalAccept.dismiss('cancel');
                     }, 2000);
                 }
             })
             .catch(function (error) {
                 console.error(error);

                 if (angular.isDefined(error.data.Error) && angular.isDefined(error.data.Error.Message)) {
                     $alert.show(vm.messageResultProcessing, error.data.Error.Message, 'danger');
                 }
                
             })
             .finally(function () {
                 vm.processingAction = false;
             })
        }

        function rejectProject(player, projectUid) {
            vm.processingAction = true;
            vm.messageResultProcessing = {};

            $http({
                method: 'POST',
                url: '/api/projects/' + projectUid + '/rejectbyplayer',
                data: {
                    Uid: player.Uid,
                    Reason: player.Reason
                }
            })
             .then(function (response) {
                 console.info(response);

                 if (angular.isDefined(response.data.status)) {
                     player.Status = response.data.status;
                 }

                 if (angular.isDefined(response.data.statusCode)) {
                     player.StatusCode = response.data.statusCode;
                 }

                 if (angular.isDefined(response.data.message)) {
                     $alert.show(vm.messageResultProcessing, response.data.message, 'success');
                 }

                 if (angular.isDefined(vm.modalReject)) {
                     $timeout(function () {
                         vm.modalReject.dismiss('cancel');
                     }, 2000);
                 }
             })
             .catch(function (error) {
                 console.error(error);

                 if (angular.isDefined(error.data.Error) && angular.isDefined(error.data.Error.Message)) {
                     $alert.show(vm.messageResultProcessing, error.data.Error.Message, 'danger');
                 }
             })
             .finally(function () {
                 vm.processingAction = false;
             })
        }
    }

})();
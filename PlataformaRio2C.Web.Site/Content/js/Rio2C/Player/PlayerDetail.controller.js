(function () {
    'use strict';

    angular
    .module('Player')
    .controller('PlayerDetailController', PlayerDetailController);

    PlayerDetailController.$inject = ['$http', '$scope', '$uibModal', '$alert', '$timeout'];

    function PlayerDetailController($http, $scope, $uibModal, $alert, $timeout) {
        var vm = this;

        vm.loadingMiniBioCollaborator = false;
        vm.collaborator = null;
        vm.errosCollaboratorMiniBio = {};

        //////////////////////////////////////////////////

        vm.showModalCollaboratorMiniBio = showModalCollaboratorMiniBio;        

        //////////////////////////////////////////////////

        function showModalCollaboratorMiniBio(e, uid) {
            e.preventDefault();
            vm.collaborator = null;
            vm.errosCollaboratorMiniBio = {};

            vm.loadingMiniBioCollaborator = true;

            vm.modalCollaboratorMiniBio = $uibModal.open({
                animation: true,
                ariaLabelledBy: 'modal-title-bottom',
                ariaDescribedBy: 'modal-body-bottom',
                templateUrl: 'modalCollaboratorMiniBio.html',
                size: 'lg',
                scope: $scope,
                controllerAs: 'vmModalCollaboratorMiniBio',
                windowClass: 'modal-messages-success',
                controller: function () {
                    $scope.dismiss = function () {
                        vm.modalCollaboratorMiniBio.dismiss('cancel');
                        vm.errosCollaboratorMiniBio = {};
                    }
                    return vm;
                }
            });


            $http({
                method: 'GET',
                url: '/api/collaborators/' + uid,
                data: {
                    Uid: uid
                }
            })
              .then(function (response) {                  

                  if (angular.isDefined(response.data) && angular.isObject(response.data)) {
                      vm.collaborator = response.data;
                  }
              })
              .catch(function (error) {
                  console.error(error);

                  if (angular.isDefined(error.data.Error) && angular.isDefined(error.data.Error.Message)) {
                      $alert.show(vm.errosCollaboratorMiniBio, error.data.Error.Message, 'danger');
                  }

                  $timeout(function () {
                      vm.modalCollaboratorMiniBio.dismiss('cancel');
                  }, 2000);
                    
              })
              .finally(function () {
                  vm.loadingMiniBioCollaborator = false;
              });
        }
    }
})();
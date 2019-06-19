(function () {
    'use strict';

    angular
    .module('Player')
    .controller('PlayerSelectionFromProjetController', PlayerSelectionFromProjetController);


    PlayerSelectionFromProjetController.$inject = ['$element', '$http', '$scope', '$uibModal', '$alert', '$window', '$q'];

    function PlayerSelectionFromProjetController($element, $http, $scope, $uibModal, $alert, $window, $q) {
        var vm = this;

        vm.serviceUrl = $element.context.dataset.serviceUrl;
        vm.genresInCurrentProject = JSON.parse($element.context.dataset.genresInCurrentProject);
        vm.projectUid = $element.context.dataset.projectUid;
        vm.relatedPlayers = JSON.parse($element.context.dataset.relatedPlayers);
        vm.relatedPlayersStatus = JSON.parse($element.context.dataset.relatedPlayersStatus);

        vm.projectSubmitted = $element.context.dataset.projectSubmitted === "True";
       
        //////////////////////////////////

        vm.loadedOptionsPlayers = false;
        vm.loadingPlayers = false;

        vm.allPlayers = null;
        vm.playersWithInterestsInCommon = null;

        vm.players = [];
        vm.groupPlayers = [];
        vm.playersSelected = [];

        vm.optionFilterPlayers = "relatedInterests";
        //vm.optionFilterPlayers = "all";
        vm.filter = null;

        $scope.$watch(function () {
            return vm.optionFilterPlayers;
        },
        function (newValue, oldValue) {
            if (newValue !== oldValue) {
                loadPlayers();
            }
        }
        );

        //////////////////////////////////////////////////

        vm.loadPlayers = loadPlayers;
        vm.toggleSelectPlayer = toggleSelectPlayer;
        vm.sendToPlayers = sendToPlayers;
        vm.uncheckAll = uncheckAll;
        vm.uncheckPlayer = uncheckPlayer;

        /////////////////////////////////////////////////     

        function uncheckPlayer(e, player) {
            player.selected = false;

            toggleSelectPlayer(e, player);
        }

        function uncheckAll(e) {
            if (vm.processToggleSelectPlayer) {
                e.preventDefault();

                vm.modalWaitProcessSelectPlayer = $uibModal.open({
                    animation: true,
                    ariaLabelledBy: 'modal-title-bottom',
                    ariaDescribedBy: 'modal-body-bottom',
                    templateUrl: 'modalWaitProcessSelectPlayer.html',
                    size: 'md',
                    scope: $scope,
                    controllerAs: 'vmModalWaitProcessSelectPlayer',
                    windowClass: 'modal-messages-success',
                    controller: function () {
                        $scope.dismiss = function () {
                            vm.modalWaitProcessSelectPlayer.dismiss('cancel');
                            vm.ErrosSavePlayerSelection = {};
                        }
                        return vm;
                    }
                });

                return true;
            }

            vm.processToggleSelectPlayer = true;

            var defer = $q.defer();
            var promises = [];



            angular.forEach(vm.playersSelected, function (player) {
                promises.push(
                    $http({
                        method: 'POST',
                        url: '/api/projects/' + vm.projectUid + '/removeplayerselection',
                        data: {
                            Uid: player.uid
                        }
                    })
                    .then(function (response) {

                        angular.forEach(vm.players, function (item) {
                            if (item.uid == player.uid) {
                                item.selected = false;
                            }
                        });

                        angular.forEach(vm.allPlayers, function (item) {
                            if (item.uid == player.uid) {
                                item.selected = false;
                            }
                        });

                        angular.forEach(vm.playersWithInterestsInCommon, function (item) {
                            if (item.uid == player.uid) {
                                item.selected = false;
                            }
                        });

                        angular.forEach(vm.relatedPlayers, function (item, index) {
                            if (item == player.uid) {
                                vm.relatedPlayers.splice(index, 1);

                            }
                        });

                        angular.forEach(vm.playersSelected, function (item, index) {
                            if (item.uid == player.uid) {
                                vm.playersSelected.splice(index, 1);
                            }
                        })
                    })
                    .catch(function (error) {
                        e.preventDefault();

                        vm.ErrosSavePlayerSelection = {};

                        $alert.show(vm.ErrosSavePlayerSelection, error.data.Error.Message, 'danger');

                        vm.modalErrorSelectPlayer = $uibModal.open({
                            animation: true,
                            ariaLabelledBy: 'modal-title-bottom',
                            ariaDescribedBy: 'modal-body-bottom',
                            templateUrl: 'modalErrorSelectPlayer.html',
                            size: 'md',
                            scope: $scope,
                            controllerAs: 'vmModalErrorSelectPlayer',
                            windowClass: 'modal-messages-success',
                            controller: function () {
                                $scope.dismiss = function () {
                                    vm.modalErrorSelectPlayer.dismiss('cancel');
                                    vm.ErrosSavePlayerSelection = {};
                                }
                                return vm;
                            }
                        });
                    })
                );
            });


            $q.all(promises)
               .then(function (response) {

                   vm.playersSelected = [];

                   angular.forEach(vm.players, function (item) {
                       item.selected = false;
                   });
               })
               .catch(function (error) {
                   console.error(error);
               })
               .finally(function () {
                   vm.processToggleSelectPlayer = false;

                   if (angular.isDefined(vm.modalWaitProcessSelectPlayer)) {
                       vm.modalWaitProcessSelectPlayer.dismiss('cancel');
                   }
               });

        }

        function loadAllPlayers() {

            if (vm.projectSubmitted) {
                vm.playersSelected = vm.relatedPlayersStatus.map(function (i) {
                    return {
                        hasImage: i.HasImage,
                        name: i.Name,
                        status: i.Status,
                        statusCode: i.StatusCode,
                        uid: i.Uid,
                        reason: i.Reason
                    }
                });

                vm.loadedOptionsPlayers = true;
            }
            else {

                $http({
                    method: 'GET',
                    url: vm.serviceUrl
                })
                .then(function (response) {

                    if (angular.isArray(response.data)) {
                        vm.groupPlayers = response.data;

                        var listaAgrupadaSoComListaDePlayers = vm.groupPlayers.map(function (item) { return item.players; });
                        var sublista = listaAgrupadaSoComListaDePlayers.concatAll();
                        vm.players = sublista;

                        if (vm.relatedPlayers !== null && vm.relatedPlayers.length > 0) {
                            angular.forEach(vm.groupPlayers, function (group) {
                                angular.forEach(group.players, function (item) {
                                    item.selected = vm.relatedPlayers.filter(function (i) {
                                        return i === item.uid;
                                    }).length > 0;
                                });
                            });
                        }

                        if (vm.allPlayers === null) {

                            vm.playersSelected = vm.players.filter(function (i) {
                                return i.selected;
                            });
                        }

                        if (vm.playersSelected !== null && vm.playersSelected.length > 0) {

                            angular.forEach(vm.groupPlayers, function (group) {
                                angular.forEach(group.players, function (item) {
                                    item.selected = vm.playersSelected.filter(function (i) {
                                        return i.uid === item.uid;
                                    }).length > 0;
                                });
                            });
                        }

                        if (vm.filter === null) {
                            vm.allPlayers = vm.players;
                        }
                        else if (vm.filter !== null) {
                            vm.playersWithInterestsInCommon = vm.players;
                        }
                    }
                })
                .catch(function () {
                })
                .finally(function () {
                    loadPlayers();
                });
            }
        }

        function loadPlayers() {
            vm.loadingPlayers = true;

            if (vm.optionFilterPlayers === 'all') {
                vm.filter = null;
            } else {
                vm.filter = {
                    genres: vm.genresInCurrentProject
                };
            }

            $http({
                method: 'GET',
                url: vm.serviceUrl,
                params: vm.filter
            })
            .then(function (response) {
                if (angular.isArray(response.data)) {
                    vm.groupPlayers = response.data;

                    var listaAgrupadaSoComListaDePlayers = vm.groupPlayers.map(function (item) { return item.players; });
                    var sublista = listaAgrupadaSoComListaDePlayers.concatAll();
                    vm.players = sublista;

                    if (vm.relatedPlayers !== null && vm.relatedPlayers.length > 0) {
                        angular.forEach(vm.players, function (item) {
                            item.selected = vm.relatedPlayers.filter(function (i) {
                                return i === item.uid;
                            }).length > 0;
                        });
                    }

                    if (vm.allPlayers === null) {

                        vm.playersSelected = vm.players.filter(function (i) {
                            return i.selected;
                        });

                    }

                    if (vm.playersSelected !== null && vm.playersSelected.length > 0) {
                        angular.forEach(vm.players, function (item) {
                            item.selected = vm.playersSelected.filter(function (i) {
                                return i.uid === item.uid;
                            }).length > 0;
                        });
                    }

                    if (vm.filter === null) {
                        vm.allPlayers = vm.players;
                    }
                    else if (vm.filter !== null) {
                        vm.playersWithInterestsInCommon = vm.players;
                    }
                }
            })
            .catch(function () {

                vm.players = [];
            })
            .finally(function () {
                vm.loadedOptionsPlayers = true;
                vm.loadingPlayers = false;
            });
        }

        vm.processToggleSelectPlayer = false;

        function toggleSelectPlayer(e, player) {

            if (vm.processToggleSelectPlayer) {
                e.preventDefault();

                vm.modalWaitProcessSelectPlayer = $uibModal.open({
                    animation: true,
                    ariaLabelledBy: 'modal-title-bottom',
                    ariaDescribedBy: 'modal-body-bottom',
                    templateUrl: 'modalWaitProcessSelectPlayer.html',
                    size: 'md',
                    scope: $scope,
                    controllerAs: 'vmModalWaitProcessSelectPlayer',
                    windowClass: 'modal-messages-success',
                    controller: function () {
                        $scope.dismiss = function () {
                            vm.modalWaitProcessSelectPlayer.dismiss('cancel');
                            vm.ErrosSavePlayerSelection = {};
                        }
                        return vm;
                    }
                });

                return true;
            }

            vm.processToggleSelectPlayer = true;

            if (player.selected) {
                $http({
                    method: 'POST',
                    url: '/api/projects/' + vm.projectUid + '/saveplayerselection',
                    data: {
                        Uid: player.uid
                    }
                })
               .then(function (response) {

                   angular.forEach(vm.allPlayers, function (item) {
                       if (item.uid == player.uid) {
                           item.selected = true;
                       }
                   });

                   angular.forEach(vm.playersWithInterestsInCommon, function (item) {
                       if (item.uid == player.uid) {
                           item.selected = true;
                       }
                   });


                   vm.relatedPlayers = vm.relatedPlayers == null ? [] : vm.relatedPlayers;

                   vm.relatedPlayers.push(player.uid);

                   vm.playersSelected = vm.playersSelected == null ? [] : vm.playersSelected;
                   vm.playersSelected.push(player);
               })
               .catch(function (error) {
                   player.selected = false;
                   console.error(error);

                   e.preventDefault();

                   vm.ErrosSavePlayerSelection = {};

                   $alert.show(vm.ErrosSavePlayerSelection, error.data.Error.Message, 'danger');

                   vm.modalErrorSelectPlayer = $uibModal.open({
                       animation: true,
                       ariaLabelledBy: 'modal-title-bottom',
                       ariaDescribedBy: 'modal-body-bottom',
                       templateUrl: 'modalErrorSelectPlayer.html',
                       size: 'md',
                       scope: $scope,
                       controllerAs: 'vmModalErrorSelectPlayer',
                       windowClass: 'modal-messages-success',
                       controller: function () {
                           $scope.dismiss = function () {
                               vm.modalErrorSelectPlayer.dismiss('cancel');
                               vm.ErrosSavePlayerSelection = {};
                           }
                           return vm;
                       }
                   });
               })
               .finally(function () {
                   vm.processToggleSelectPlayer = false;

                   if (angular.isDefined(vm.modalWaitProcessSelectPlayer)) {
                       vm.modalWaitProcessSelectPlayer.dismiss('cancel');
                   }
               });


            }
            else {
                $http({
                    method: 'POST',
                    url: '/api/projects/' + vm.projectUid + '/removeplayerselection',
                    data: {
                        Uid: player.uid
                    }
                })
              .then(function (response) {

                  angular.forEach(vm.players, function (item) {
                      if (item.uid == player.uid) {
                          item.selected = false;
                      }
                  });

                  angular.forEach(vm.allPlayers, function (item) {
                      if (item.uid == player.uid) {
                          item.selected = false;
                      }
                  });

                  angular.forEach(vm.playersWithInterestsInCommon, function (item) {
                      if (item.uid == player.uid) {
                          item.selected = false;
                      }
                  });

                  angular.forEach(vm.relatedPlayers, function (item, index) {
                      if (item == player.uid) {
                          vm.relatedPlayers.splice(index, 1);

                      }
                  });

                  angular.forEach(vm.playersSelected, function (item, index) {
                      if (item.uid == player.uid) {
                          vm.playersSelected.splice(index, 1);
                      }
                  })
              })
              .catch(function (error) {
                  e.preventDefault();

                  vm.ErrosSavePlayerSelection = {};

                  $alert.show(vm.ErrosSavePlayerSelection, error.data.Error.Message, 'danger');

                  vm.modalErrorSelectPlayer = $uibModal.open({
                      animation: true,
                      ariaLabelledBy: 'modal-title-bottom',
                      ariaDescribedBy: 'modal-body-bottom',
                      templateUrl: 'modalErrorSelectPlayer.html',
                      size: 'md',
                      scope: $scope,
                      controllerAs: 'vmModalErrorSelectPlayer',
                      windowClass: 'modal-messages-success',
                      controller: function () {
                          $scope.dismiss = function () {
                              vm.modalErrorSelectPlayer.dismiss('cancel');
                              vm.ErrosSavePlayerSelection = {};
                          }
                          return vm;
                      }
                  });
              })
              .finally(function () {
                  vm.processToggleSelectPlayer = false;

                  if (angular.isDefined(vm.modalWaitProcessSelectPlayer)) {
                      vm.modalWaitProcessSelectPlayer.dismiss('cancel');
                  }
              });


            }
        }

        function sendToPlayers() {
            vm.sendingToPlayers = true;
            vm.ErrosSendPlayerSelection = {};

            $http({
                method: 'POST',
                url: '/api/projects/' + vm.projectUid + '/sendtoplayers',
                data: {
                    UidsPlayers: vm.playersSelected.map(function (item) { return item.uid; })
                }
            })
               .then(function (response) {

                   $alert.show(vm.ErrosSendPlayerSelection, response.data, 'success');

                   $window.location.href = "/ProducerArea/project";

               })
               .catch(function (error) {
                   console.error(error);

                   vm.ErrosSendPlayerSelection = {};

                   $alert.show(vm.ErrosSendPlayerSelection, error.data.Error.Message, 'danger');
               })
                .finally(function () {
                    vm.sendingToPlayers = false;
                });

        }

        loadAllPlayers();
        //loadPlayers();
    }

   
})();




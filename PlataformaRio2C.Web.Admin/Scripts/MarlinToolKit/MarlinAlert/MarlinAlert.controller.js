(function () {
    "use strict";

    angular
       .module('MarlinAlert')
       .controller("MarlinAlertController", MarlinAlertController);

    MarlinAlertController.$inject = ['$scope', 'ngToast', '$element', '$uibModal', '$alert'];

    function MarlinAlertController($scope, ngToast, $element, $uibModal, $alert) {
        var vm = this;

        vm.seacrhEndReview = "concluiu a sua revisão";

        vm.modalForDangerMessages = true;
        vm.modalForWarningMessages = true;
        vm.modalForEndReview = true;

        vm.showMessages = showMessages;

        vm.messagesError = [];
        vm.messagesWarning = [];
        vm.messageEndReview = [];

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        function showMessages(messages) {
            vm.messages = messages;
            
            if (angular.isArray(vm.messages) && vm.messages.length > 0) {
                vm.messagesError = vm.messages.filter(function (item) { return item.Type === 'danger' });
                vm.messagesWarning = vm.messages.filter(function (item) { return item.Type === 'warning' });
                vm.messageEndReview = vm.messages.filter(function (item) { return item.Message.indexOf(vm.seacrhEndReview) !== -1 });

                angular.forEach(vm.messages, function (item) {                    
                    if (item.Type == 'success') {
                        if (item.Message.indexOf(vm.seacrhEndReview) === -1 || !vm.modalForEndReview || !vm.modalForDangerMessages) {
                            ngToast.create(item.Message);
                        }
                    }
                    else if (item.Type == 'info') {
                        ngToast.info(item.Message);
                    }
                    else if (item.Type == 'warning' && !vm.modalForWarningMessages) {
                        ngToast.warning(item.Message);
                    }
                    else if (item.Type == 'danger' && !vm.modalForDangerMessages) {
                        ngToast.danger({
                            content: item.Message,
                            dismissOnTimeout: false
                        });
                    }
                });

                if (vm.messagesWarning.length > 0 && vm.modalForWarningMessages) {
                    vm.modalInstanceMessageWarning = $uibModal.open({
                        animation: true,
                        ariaLabelledBy: 'modal-title-bottom',
                        ariaDescribedBy: 'modal-body-bottom',
                        templateUrl: 'messageWarningModal.html',
                        backdrop: 'static',
                        size: 'md',
                        controllerAs: '$ctrl',
                        scope: $scope,
                        windowClass: 'modal-messages-error',
                        controller: function () {
                            $scope.alertaMessagesError = {};
                            $alert.show($scope.alertaMessagesError, vm.messagesWarning.map(function (item) { return item.Message }), 'warning');
                            $scope.messagesWarning = vm.messagesWarning;

                            $scope.dismiss = function () {
                                vm.modalInstanceMessageWarning.dismiss('cancel');
                                vm.messagesWarning = [];
                                showModalEndReview();
                            }
                        }
                    });
                }

                if (vm.messagesError.length > 0 && vm.modalForDangerMessages) {
                    vm.modalInstanceMessageError =  $uibModal.open({
                        animation: true,
                        ariaLabelledBy: 'modal-title-bottom',
                        ariaDescribedBy: 'modal-body-bottom',
                        templateUrl: 'messageErrorModal.html',
                        backdrop: 'static',
                        size: 'md',
                        scope: $scope,
                        controllerAs: '$ctrl',
                        windowClass: 'modal-messages-error',
                        controller: function () {
                            $scope.alertaMessagesError = {};
                            $alert.show($scope.alertaMessagesError, vm.messagesError.map(function (item) { return item.Message }), 'danger');
                            $scope.messagesError = vm.messagesError;

                            $scope.dismiss = function () {
                                vm.modalInstanceMessageError.dismiss('cancel');
                                vm.messagesError = [];
                                showModalEndReview();
                            }
                        }
                    });
                }
                showModalEndReview();               
            }
        }        
        
        function showModalEndReview() {
            if (vm.messagesError.length <= 0 && vm.messageEndReview.length > 0 && vm.modalForEndReview) {
                $uibModal.open({
                    animation: true,
                    ariaLabelledBy: 'modal-title-bottom',
                    ariaDescribedBy: 'modal-body-bottom',
                    templateUrl: 'messageEndReview.html',
                    size: 'md',
                    scope: $scope,
                    controllerAs: '$ctrl',
                    windowClass: 'modal-end-review',
                    controller: function () {
                    }
                });
            }
        }
    }
})();
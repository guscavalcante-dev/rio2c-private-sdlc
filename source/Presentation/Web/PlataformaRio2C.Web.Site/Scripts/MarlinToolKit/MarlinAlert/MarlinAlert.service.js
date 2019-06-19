(function () {
    'use strict';

    angular
        .module('MarlinAlert')
        .service('$alert', $alert);

    $alert.$inject = ['$timeout', '$uibModal', '$rootScope'];

    function $alert($timeout, $uibModal, $rootScope) {
        var vm = this;

        vm.messagesError = [];

        this.hide = function (obj) {
            $timeout(function () { obj = null }, 5000);
        }

        this.show = function (obj, msg, type) {
            obj.alerts = obj.alerts ? obj.alerts : [];

            obj.alert = {
                show: true,
                type: type,
                msg: angular.isArray(msg) ? msg : [msg]
            }

            obj.alerts.push(obj.alert);
        };

        this.render = function (type, msg) {
            var result = '';

            if (angular.isArray(msg)) {
                angular.forEach(msg, function (item) {
                    result += '<div class="alert alert-' + type + '" role="alert">' + item + '</div>';
                });

            }
            else {
                result = '<div class="alert alert-' + type + '" role="alert">' + msg + '</div>';
            }

            return result;
        }

        this.showErrorMessages = function (messagesError) {
            vm.messagesError = messagesError;
            vm.modalInstanceMessageError = $uibModal.open({
                animation: true,
                ariaLabelledBy: 'modal-title-bottom',
                ariaDescribedBy: 'modal-body-bottom',
                templateUrl: 'messageErrorModal.html',
                backdrop: 'static',
                size: 'md',
                scope: $rootScope,
                controllerAs: '$ctrl',
                windowClass: 'modal-messages-error',
                controller: function () {
                    $rootScope.alertaMessagesError = {};
                    vm.show($rootScope.alertaMessagesError, vm.messagesError, 'danger');
                    $rootScope.messagesError = vm.messagesError;

                    $rootScope.dismiss = function () {
                        vm.modalInstanceMessageError.dismiss('cancel');
                        vm.messagesError = [];                        
                    }
                }
            });
        }
    }
})();
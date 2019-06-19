(function () {
    'use strict';

    angular
        .module('MarlinAlert')
        .directive('alerta', alerta)
        .directive('statusMessages', statusMessages);

    function alerta() {
        var directive = {
            template: function ($dom, $elem) {                
                if (angular.isDefined($elem.$attr.hidebtnclose) && $elem.$attr.hidebtnclose) {
                    return '<div id="' + $elem.ngModel + '" data-ng-repeat="alert in ' + $elem.ngModel + '.alerts track by $index" class="alert alert-{{alert.type}}" role="alert" data-ng-show="alert.show"><p data-ng-repeat="msg in alert.msg track by $index" ng-bind-html="msg"></p> </div>'
                }
                return '<div id="' + $elem.ngModel + '" data-ng-repeat="alert in ' + $elem.ngModel + '.alerts track by $index" class="alert alert-{{alert.type}}" role="alert" data-ng-show="alert.show"> <button type="button" class="close" data-dismiss="alert" aria-label="Close"> <span aria-hidden="true">&times;</span> </button> <p data-ng-repeat="msg in alert.msg track by $index" ng-bind-html="msg"></p> </div>'
            }
        };

        return directive;
    }

    function statusMessages() {
        var directive = {
            link: link,
            controller: 'MarlinAlertController'
        }

        function link(scope, element, attrs, ctrl, transclude) {           
            if (angular.isDefined(attrs.messages) && attrs.messages != '') {
                ctrl.showMessages(JSON.parse(attrs.messages));
            }            
        }

        return directive;
    }
})();
(function () {
    'use strict';
    angular
        .module('MarlinCkeditor')
        .directive('marlinCkeditorDirective', MarlinCkeditorDirective);

    function MarlinCkeditorDirective() {
        return {
            require: '?ngModel',
            link: link
        }

        function link(scope, elm, attr, ngModel) {
            var ck = CKEDITOR.replace(elm[0], {
                customConfig: '/Content/js/ckeditor_config.js'
            });

            if (!ngModel) return;

            ck.on('instanceReady', function () {
                ck.setData(ngModel.$viewValue);
            });


            ck.on('pasteState', function () {
                scope.$apply(function () {
                    ngModel.$setViewValue(ck.getData());
                });
            });

            ngModel.$render = function (value) {
                ck.setData(ngModel.$viewValue);
            };
        }
    }

})();
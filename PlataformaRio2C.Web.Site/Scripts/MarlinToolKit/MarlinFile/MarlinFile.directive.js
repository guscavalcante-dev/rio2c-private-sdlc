(function () {
    'use strict';

    angular
        .module('MarlinFile')
        .directive('file', file);

    function file() {
        var directive = {
            require: "ngModel",
            restrict: 'A',
            link: function ($scope, el, attrs, ngModel) {
                el.bind('change', function (event) {
                    var files = event.target.files;
                    var file = files[0];                  

                    var fd = new FormData();
                    fd.append("file", file);                    

                    ngModel.$setViewValue(fd);
                    ngModel.$modelValue = fd;
                    $scope.$apply();
                });

                el.on('fileclear', function (event) {
                    ngModel.$setViewValue(null);
                    ngModel.$modelValue = null;
                });

                if (attrs.caption != undefined && attrs.caption != null && attrs.caption != '') {
                    el.fileinput({
                        initialPreview: ['teste'],
                        initialCaption: attrs.caption
                    });
                }
               
            }
        }

        return directive;
    }
})();
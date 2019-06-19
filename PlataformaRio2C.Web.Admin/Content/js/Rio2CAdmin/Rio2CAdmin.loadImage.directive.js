(function () {
    'use strict';

    angular
        .module('Rio2CAdmin')
        .directive('loadImage', loadImage)

    loadImage.$inject = ['$http'];

    function loadImage($http) {
        var directive = {
            require: "ngModel",
            scope: {
                label: '@',
                ngModel: '='
            },
            link: link
        }

        function link(scope, element, attrs, ngModel) {

            scope.$watch(function () {
                return scope.ngModel;
            }, function (newValue, oldValue) {
                if (newValue !== oldValue) {
                    _loadImage();
                }
            });



            function _loadImage() {
                if (angular.isUndefined(scope.ngModel)) {
                    scope.ngModel = {};
                }

                var disableLoad = attrs.ngDisableLoadImage ? JSON.parse(attrs.ngDisableLoadImage) : false,
                    uid = attrs.loadImage,
                    url = attrs.urlLoad;


                if (angular.isDefined(attrs.ngImageDefault)) {
                    angular.element(element).attr('src', attrs.ngImageDefault);
                }

                if (angular.element(element).hasClass("image-loading")) {
                    element.removeClass('image-loading');
                    element.closest('.wrap-image-loading').find('.loader-image').remove();
                    element.unwrap('.wrap-image-loading');
                }


                if (!disableLoad) {

                    if (angular.isUndefined(scope.ngModel.image)) {

                        element.wrap("<div class='wrap-image-loading' style='width: inherit;'></div>");
                        element.addClass('image-loading');

                        $('<div class="loader-image"></div>').insertAfter(element);

                        $http({
                            method: 'GET',
                            url: url,
                            params: {
                                uid: uid
                            }
                        })
                            .then(function (response) {
                                scope.ngModel.image = response.data;
                                angular.element(element).attr('src', 'data:image/jpeg;base64, ' + response.data.file);


                            })
                            .catch(function () {

                            })
                            .finally(function () {
                                if (angular.element(element).hasClass("image-loading")) {
                                    element.removeClass('image-loading');
                                    element.closest('.wrap-image-loading').find('.loader-image').remove();
                                    element.unwrap('.wrap-image-loading');
                                }
                            });

                    }
                    else {
                        angular.element(element).attr('src', 'data:image/jpeg;base64, ' + scope.ngModel.image.file);
                    }
                }
            }

            _loadImage();

        }

        return directive;
    }

})();
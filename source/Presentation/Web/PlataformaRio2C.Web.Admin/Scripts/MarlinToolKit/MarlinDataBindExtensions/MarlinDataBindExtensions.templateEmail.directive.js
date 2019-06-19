(function () {
    'use strict';
    angular
        .module('MarlinDataBindExtensions')
        .directive('templateEmail', includeReplace);

    includeReplace.$inject = ['$compile'];

    function includeReplace($compile) {
        return {
            require: 'ngInclude',
            link: function (scope, el, attrs) {
                el.find('[data-src-file]').each(function (i) {
                    $(this).attr('src', $(this).data('src-file'));
                });

                var output = el.html();

                output = output.replace("{@Message}", "<span ng-bind-html='vm.Message'></span>");
                output = output.replace(new RegExp("{@PATH_IMAGES_TEMPLATES}", 'g'), "{{vm.PATH_IMAGES_TEMPLATES}}");

                el.html(output);

                $compile(el.contents())(scope);

                scope.$watch(function () {
                    return scope.$eval(attrs.bindHtmlCompile);
                }, function (value) {
                    $compile(el.contents(), true)(scope);
                });
            }
        };
    }
})();
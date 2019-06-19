(function () {
    'use strict';

    angular
        .module('Project')
        .controller('ProjectEditor', ProjectEditor);

    ProjectEditor.$inject = ['$scope', '$http', '$element'];

    function ProjectEditor($scope, $http, $element) {
        var vm = this;

        vm.linksImage = [{ Value: '' }];
        vm.linksTeaser = [{ Value: '' }];

        vm.loadLinksImages = loadLinksImages;
        vm.loadLinksTeaser = loadLinksTeaser;

        vm.addLinkImage = addLinkImage;
        vm.addLinkTeaser = addLinkTeaser;

        vm.removeLinkImage = removeLinkImage;
        vm.removeLinkTeaser = removeLinkTeaser;

        function loadLinksImages(value) {
            value = JSON.parse(value);
            if (value != null) {
                vm.linksImage = value;
            }

        }

        function loadLinksTeaser(value) {
            value = JSON.parse(value);
            if (value != null) {
                vm.linksTeaser = value;
            }
        }

        function addLinkImage() {
            vm.linksImage.push({ Value: '' });
        }

        function addLinkTeaser() {
            vm.linksTeaser.push({ Value: '' });
        }

        function removeLinkImage(index) {
            vm.linksImage.splice(index, 1);
        }

        function removeLinkTeaser(index) {
            vm.linksTeaser.splice(index, 1);
        }
    }
})();
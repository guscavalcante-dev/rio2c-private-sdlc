(function () {
    //'use strict';

    angular
        .module('Project', ['Pagination'])
        .controller('ProjectList', ProjectList);

    ProjectList.$inject = ['$scope', '$http', '$element', 'SimplePaginate'];

    function ProjectList($scope, $http, $element,SimplePaginate) {
        var vm = this;

        vm.orderBy = null;
        vm.orderByDesc = true;

        vm.listProject = [];

        vm.loadingListProject = false;


        /////////////////////////////////////////////////////

        //vm.getListProject = getListProject;

        /////////////////////////////////////////////////////

        function getListProject(val) {
            vm.loadingListProject = true;

            var params = {};

            if (val != null) {
                params.orderBy = val;

                if (val === vm.orderBy) {
                    params.orderByDesc = !vm.orderByDesc;
                    vm.orderByDesc = params.orderByDesc;
                }
                else if (vm.orderBy == null) {
                    params.orderByDesc = vm.orderByDesc;
                }
                else {
                    vm.orderByDesc = false;
                }
            }

            $http.get('/api/project', {
                params: params
            })
            .then(function (response) {

                if (angular.isArray(response.data)) {
                    //vm.listProject = response.data;
                    vm.orderBy = params.orderBy;

                    SimplePaginate.configure({
                        data: response.data,
                        perPage: 100
                    });

                    vm.listProject = {
                        result: SimplePaginate.goToPage(0),
                        total: SimplePaginate.itemsTotal(),
                        next: function () {
                            $scope.paginate.result = SimplePaginate.next();
                        },
                        prev: function () {
                            $scope.paginate.result = SimplePaginate.prev();
                        }
                    };
                }
                else {
                    vm.listProject = [];
                }
            })
            .catch(function () {
                console.log('catch');

                vm.listProject = [];
            })
            .finally(function () {
                vm.loadingListProject = false;
                });

            console.log(vm);
        }

        getListProject('CreationDate');
    }
})();
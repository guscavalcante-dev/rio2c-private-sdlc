(function () {
    'use strict';

    angular
        .module('Dashboard')
        .controller('DashboardIndex', DashboardIndex);

    DashboardIndex.$inject = ['$scope', '$http', '$element', '$window', '$interval', '$timeout', '$q'];

    function DashboardIndex($scope, $http, $element, $window, $interval, $timeout, $q) {
        var vm = this;


        vm.countTotalHoldings = 0;
        vm.countTotalPlayers = 0;
        vm.countTotalProducers = 0;
        vm.countTotalProjects = 0;

        /*ANGULAR CHART PROPERTIES*/
        vm.labels = ["Download Sales", "In-Store Sales", "Mail-Order Sales"];
        vm.data = [300, 500, 100];
        vm.colorsProject = ['#a1c036', '#c20e1a'];
        vm.colorsProjectSentPlayers = ['#f0ad4e', '#a1c036', '#c20e1a'];
        vm.dataChartProject = [];

        vm.loadingNumbers = false;
        vm.loadingChartProjectChart = false;
        vm.loadingChartProjectSubmissions = false;
        vm.loadingChartPlayer = false;
        vm.loadingChartProducer = false;

        vm.chartResponsive = false;
        vm.time;
        /////////////////////////////////////////////////////

        vm.getNumbers = getNumbers;
        vm.getCharts = getCharts;
        vm.updateNumbers = updateNumbers;
        vm.updateCharts = updateCharts;
        vm.changeChartByWindowSize = changeChartByWindowSize;
        vm.callInitAndUpdate = callInitAndUpdate;


        /////////////////////////////////////////////////////

         //GET NUMBERS SPECIFIC
        function getNumberHolding() {
            return $http.get('/api/dashboard/totalholding')
                .then(function (response) {
                    vm.countTotalHoldings = response.data;

                })
                .catch(function () {
                    vm.countTotalHoldings = 0;
                })
                .finally(function () {

                });
        }

        function getNumberPlayer() {
            return $http.get('/api/dashboard/totalplayer')
                .then(function (response) {
                    vm.countTotalPlayers = response.data;

                })
                .catch(function () {
                    vm.countTotalPlayers = 0;
                })
                .finally(function () {

                });
        }

        function getNumberProducer() {
            return $http.get('/api/dashboard/totalproducer')
                .then(function (response) {
                    vm.countTotalProducers = response.data;

                })
                .catch(function () {
                    vm.countTotalProducers = 0;
                })
                .finally(function () {

                });
        }

        function getNumberProject() {
            return $http.get('/api/dashboard/totalproject')
                .then(function (response) {
                    vm.countTotalProjects = response.data;

                })
                .catch(function () {
                    vm.countTotalProjects = 0;
                })
                .finally(function () {

                });
        }

        //GET CHARTS SPECIFIC
        function getChartProjectChart() {
            $http.get('/api/dashboard/projetcchart')
                .then(function (response) {
                    //console.info(response); 
                    if (angular.isArray(response.data)) {
                        vm.itensChartProject = response.data;

                        vm.labelsChartProject = vm.itensChartProject.map(function (i) { return i.label });

                        vm.dataChartProject = vm.itensChartProject.map(function (i) { return i.number });
                    }

                })
                .catch(function () {

                })
                .finally(function () {
                    vm.loadingChartProjectChart = false;

                });
        }

        function getChartProjectSubmissions() {
            $http.get('/api/dashboard/projetcssubmissions')
                .then(function (response) {
                    //console.info(response);
                    if (angular.isArray(response.data)) {
                        vm.itensChartProjectSubmissions = response.data;

                        vm.labelsChartProjectSubmissions = vm.itensChartProjectSubmissions.map(function (i) { return i.label });

                        vm.dataChartProjectSubmissions = vm.itensChartProjectSubmissions.map(function (i) { return i.number });

                    }

                })
                .catch(function () {

                })
                .finally(function () {
                    vm.loadingChartProjectSubmissions = false;

                });
        }

        function getCountryPlayer() {
            $http.get('/api/dashboard/countryplayer')
                .then(function (response) {
                    //console.info(response);

                    if (angular.isArray(response.data)) {
                        vm.itensChartCountryPlayer = response.data;
                        //console.log(vm.itensChartCountryPlayer);

                        vm.labelsCountryPlayer = vm.itensChartCountryPlayer.map(el => el.label);
                        vm.dataCountryPlayer = vm.itensChartCountryPlayer.map(el => el.number);
                    }

                })
                .catch(function () {

                })
                .finally(function () {
                    vm.loadingChartPlayer = false;

                });
        }

        function getCountryProducer() {
            $http.get('/api/dashboard/countryproducer')
                .then(function (response) {
                    console.info(response);

                    if (angular.isArray(response.data)) {
                        vm.itensChartCountryProducer = response.data;

                        vm.labelsCountryProducer = vm.itensChartCountryProducer.map(el => el.label);
                        vm.dataCountryProducer = vm.itensChartCountryProducer.map(el => el.number);
                    }

                })
                .catch(function () {

                })
                .finally(function () {
                    vm.loadingChartProducer = false;
                });
        } 
        
        //GET AND UPDATE GENERAL
        function getNumbers() {
            vm.loadingNumbers = true;

            $q.all([
                getNumberHolding(),
                getNumberPlayer(),
                getNumberProducer(),
                getNumberProject()
            ])
                .then()
                .catch()
                .finally(function () {
                    vm.loadingNumbers = false;
                });
        }

        function getCharts() {

            vm.loadingChartProjectChart = true;
            vm.loadingChartProjectSubmissions = true;
            vm.loadingChartPlayer = true;
            vm.loadingChartProducer = true;

            getChartProjectChart();
            getChartProjectSubmissions();
            getCountryPlayer();
            getCountryProducer();


        }

        function updateNumbers() {
            vm.updatingNumbers = false;
            vm.getNumbers();

        }

        function updateCharts() {
            vm.updatingChartProjectChart = false;
            vm.updatingChartProjectSubmissions = false;
            vm.updatingChartPlayer = false;
            vm.updatingChartProducer = false;

            vm.getCharts();
        } 

        function changeChartByWindowSize() {
            function testResize() {
                var theWidth = window.innerWidth
                    || document.documentElement.clientWidth
                    || document.body.clientWidth;

                if (theWidth <= 1000)
                    vm.chartResponsive = true;
                else
                    vm.chartResponsive = false;

            }

            $interval(testResize, 1000);

        }

        //INIT ALL CALLS (happens only one time)
        function init() {
            vm.getNumbers();

            vm.getCharts();

            vm.changeChartByWindowSize();
        }      
        
        //UPDATE ALL CALLS (happens every 60 seconds)
        function update() {           
            $interval(function () {
                vm.updatingNumbers = true;
                vm.updatingChartProjectChart = true;
                vm.updatingChartProjectSubmissions = true;
                vm.updatingChartPlayer = true;
                vm.updatingChartProducer = true;

                vm.time = 5;
                
                $interval(function () {
                    vm.time--;
                }, 1000, 5);

                $timeout(function () {
                    vm.updateNumbers();
                    vm.updateCharts();
                }, 5000);


            }, 180000);
            
        }   

        //CALL INIT AND UPDATE
        function callInitAndUpdate() {
            init();
            update();
        }

        callInitAndUpdate();
           


    }
})();
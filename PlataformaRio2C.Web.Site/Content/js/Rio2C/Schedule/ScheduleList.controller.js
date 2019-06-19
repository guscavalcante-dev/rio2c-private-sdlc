(function () {
    'use strict';

    angular
        .module('Schedule')
        .controller('ScheduleList', ScheduleList);

    ScheduleList.$inject = ['$scope', '$http', '$element', '$log', '$uibModal', '$q', '$timeout'];

    function ScheduleList($scope, $http, $element, $log, $uibModal, $q, $timeout) {
        var vm = this;

        vm.area = $element.context.dataset.area;

        vm.loadingSchedule = false;
        vm.processedError = false;
        vm.messageResultProcessing = {};

        /*PROPERTIES*/
        vm.hours = [];
        vm.daysObjectList = [
            {
                date: "03/04/2018",
                dayOfWeek: "Terça-Feira"
            },
            {
                date: "04/04/2018",
                dayOfWeek: "Quarta-Feira"
            },
            {
                date: "05/04/2018",
                dayOfWeek: "Quinta-Feira"
            },
            {
                date: "06/04/2018",
                dayOfWeek: "Sexta-Feira"
            },
            {
                date: "07/04/2018",
                dayOfWeek: "Sábado-Feira"
            },
            {
                date: "08/04/2018",
                dayOfWeek: "Domingo-Feira"
            },

        ];
        vm.daysObjectList = [];
        vm.eventItems = [];
        vm.eventsList = [
            {
                date: "03/04/2018",
                dayOfWeek: "Terça-Feira",
                items: [

                    {
                        type: "meetings",
                        title: "Duro de Matar",
                        uid: "00000000-0000-0000-0000-000000000000",
                        genre: ["Ação", "Show"],
                        startHour: "10:30",
                        endHour: "10:50",
                        duration: "20",
                        room: "Rodadas de Negócio (Sala 1)",
                        table: "10",
                        player: {
                            name: "Marlin Homologação",
                            uid: "f66fa733-392a-40b3-9c66-353ed7a7f21c"
                        },
                        producer: {
                            name: "Razão Social Aron",
                            uid: "b26763a4-f789-4e96-a947-b1201796425e"
                        }

                    },
                    {
                        type: "meetings",
                        title: "O Lobo de Wall Street",
                        uid: "00000000-0000-0000-0000-000000000000",
                        genre: ["ficção", "ação"],
                        startHour: "11:00",
                        endHour: "11:20",
                        duration: "20",
                        room: "Rodadas de Negócio (Sala 2)",
                        table: "22",
                        player: {
                            name: "TV Cultura",
                            uid: "4f87b3a1-bc66-4d07-8300-c701e8ad83c6"
                        },
                        producer: {
                            name: "Produtora Marlin Social",
                            uid: "db5b13d3-d7b0-406b-9c81-345b389d4c3e"
                        }
                    },
                    {
                        type: "meetings",
                        title: "Assassin's Creed",
                        uid: "00000000-0000-0000-0000-000000000000",
                        genre: ["Documentário/Factual", "Show"],
                        startHour: "11:30",
                        endHour: "11:50",
                        duration: "20",
                        room: "Rodadas de Negócio (Sala 1)",
                        table: "10",
                        player: {
                            name: "Marlin Homologação",
                            uid: "f66fa733-392a-40b3-9c66-353ed7a7f21c"
                        },
                        producer: {
                            name: "Razão Social Aron",
                            uid: "b26763a4-f789-4e96-a947-b1201796425e"
                        }

                    },
                    {
                        type: "meetings",
                        title: "Contra Anistia",
                        uid: "00000000-0000-0000-0000-000000000000",
                        genre: ["ficção"],
                        startHour: "12:00",
                        endHour: "12:20",
                        duration: "20",
                        room: "Rodadas de Negócio (Sala 1)",
                        table: "17",
                        player: {
                            name: "Marlin Desenvolvimento02 Networks International Brasil (Universal, Syfy, Studio Universal)",
                            uid: "5ac3ba4a-8497-4440-819f-fb53e9e0bbe1"
                        },
                        producer: {
                            name: "Produtora Master",
                            uid: "546552d0-512e-466e-b94c-b9e486c23e6f"
                        }
                    },
                    {
                        type: "meetings",
                        title: "Vingadores: Era de Ultron",
                        uid: "00000000-0000-0000-0000-000000000000",
                        genre: ["ficção"],
                        startHour: "14:00",
                        endHour: "14:20",
                        duration: "20",
                        room: "Rodadas de Negócio (Sala 2)",
                        table: "17",
                        player: {
                            name: "Marlin Desenvolvimento02 Networks International Brasil (Universal, Syfy, Studio Universal)",
                            uid: "5ac3ba4a-8497-4440-819f-fb53e9e0bbe1"
                        },
                        producer: {
                            name: "Produtora Master",
                            uid: "546552d0-512e-466e-b94c-b9e486c23e6f"
                        }
                    },
                    {
                        type: "meetings",
                        title: "Homem-Formiga",
                        uid: "00000000-0000-0000-0000-000000000000",
                        genre: ["ficção"],
                        startHour: "14:30",
                        endHour: "14:50",
                        duration: "20",
                        room: "Rodadas de Negócio (Sala 1)",
                        table: "17",
                        player: {
                            name: "Marlin Desenvolvimento02 Networks International Brasil (Universal, Syfy, Studio Universal)",
                            uid: "5ac3ba4a-8497-4440-819f-fb53e9e0bbe1"
                        },
                        producer: {
                            name: "Produtora Master",
                            uid: "546552d0-512e-466e-b94c-b9e486c23e6f"
                        }
                    },

                ]
            },
            {
                date: "04/04/2018",
                dayOfWeek: "Quarta-Feira",
                items: [
                    {
                        type: "meetings",
                        title: "Deadpool",
                        uid: "00000000-0000-0000-0000-000000000000",
                        genre: ["ficção", "ação"],
                        startHour: "10:00",
                        endHour: "10:20",
                        duration: "20",
                        room: "Rodadas de Negócio (Sala 2)",
                        table: "22",
                        player: {
                            name: "TV Cultura",
                            uid: "4f87b3a1-bc66-4d07-8300-c701e8ad83c6"
                        },
                        producer: {
                            name: "Produtora Marlin Social",
                            uid: "db5b13d3-d7b0-406b-9c81-345b389d4c3e"
                        }
                    },
                    {
                        type: "meetings",
                        title: "Sherlock Holmes 3",
                        uid: "00000000-0000-0000-0000-000000000000",
                        genre: ["ficção", "ação"],
                        startHour: "10:30",
                        endHour: "10:50",
                        duration: "20",
                        room: "Rodadas de Negócio (Sala 2)",
                        table: "22",
                        player: {
                            name: "TV Cultura",
                            uid: "4f87b3a1-bc66-4d07-8300-c701e8ad83c6"
                        },
                        producer: {
                            name: "Produtora Marlin Social",
                            uid: "db5b13d3-d7b0-406b-9c81-345b389d4c3e"
                        }
                    },
                    {
                        type: "meetings",
                        title: "Cavalo de Tróia",
                        uid: "00000000-0000-0000-0000-000000000000",
                        genre: ["ficção", "ação"],
                        startHour: "10:30",
                        endHour: "10:50",
                        duration: "20",
                        room: "Rodadas de Negócio (Sala 2)",
                        table: "22",
                        player: {
                            name: "TV Cultura",
                            uid: "4f87b3a1-bc66-4d07-8300-c701e8ad83c6"
                        },
                        producer: {
                            name: "Produtora Marlin Social",
                            uid: "db5b13d3-d7b0-406b-9c81-345b389d4c3e"
                        }
                    },
                    {
                        type: "conference",
                        title: "A melhor palestra de todas",
                        uid: "00000000-0000-0000-0000-000000000000",
                        startHour: "11:00",
                        endHour: "12:00",
                        duration: "60",
                        room: "Sala 2: Teatro de Câmara",
                        lecturers: [
                            { name: "Aurélio Ribeiro alterado", uid: "1c54f334-8d5f-425d-8d1c-b96e7c40b525" },
                            { name: "Zico", uid: "1c54f334-8d5f-425d-8d1c-b96e7c40b525" },
                            { name: "Cristiano Ronaldo", uid: "1c54f334-8d5f-425d-8d1c-b96e7c40b525" },
                            { name: "Messy", uid: "1c54f334-8d5f-425d-8d1c-b96e7c40b525" }
                        ]
                    },
                    {
                        type: "meetings",
                        title: "Titanic",
                        uid: "00000000-0000-0000-0000-000000000000",
                        genre: ["Documentário / Factual", "ação"],
                        startHour: "14:00",
                        endHour: "14:20",
                        duration: "20",
                        room: "Rodadas de Negócio (Sala 2)",
                        table: "22",
                        player: {
                            name: "TV Cultura",
                            uid: "4f87b3a1-bc66-4d07-8300-c701e8ad83c6"
                        },
                        producer: {
                            name: "Produtora Marlin Social",
                            uid: "db5b13d3-d7b0-406b-9c81-345b389d4c3e"
                        }
                    },
                    {
                        type: "meetings",
                        title: "Avatar 2",
                        uid: "00000000-0000-0000-0000-000000000000",
                        genre: ["Documentário / Factual", "ação"],
                        startHour: "14:30",
                        endHour: "14:50",
                        duration: "20",
                        room: "Rodadas de Negócio (Sala 2)",
                        table: "22",
                        player: {
                            name: "TV Cultura",
                            uid: "4f87b3a1-bc66-4d07-8300-c701e8ad83c6"
                        },
                        producer: {
                            name: "Produtora Marlin Social",
                            uid: "db5b13d3-d7b0-406b-9c81-345b389d4c3e"
                        }
                    },
                    {
                        type: "conference",
                        title: "A palestra mais demorada de todas",
                        uid: "00000000-0000-0000-0000-000000000000",
                        startHour: "15:00",
                        endHour: "00:00",
                        duration: "540",
                        room: "Sala 2: Teatro de Câmara",
                        lecturers: [
                            { name: "Aurélio Ribeiro alterado", uid: "1c54f334-8d5f-425d-8d1c-b96e7c40b525" },
                            { name: "Zico", uid: "1c54f334-8d5f-425d-8d1c-b96e7c40b525" },
                            { name: "Cristiano Ronaldo", uid: "1c54f334-8d5f-425d-8d1c-b96e7c40b525" },
                            { name: "Messy", uid: "1c54f334-8d5f-425d-8d1c-b96e7c40b525" }
                        ]
                    }

                ]


            },
            {
                date: "05/04/2018",
                dayOfWeek: "Quinta-Feira",
                items: [

                    {
                        type: "conference",
                        title: "Titulo em português 4",
                        uid: "00000000-0000-0000-0000-000000000000",
                        startHour: "10:00",
                        endHour: "11:30",
                        duration: "90",
                        room: "Sala 1: Grande Sala",
                        lecturers: [
                            { name: "Aurélio Ribeiro alterado", uid: "1c54f334-8d5f-425d-8d1c-b96e7c40b525" }
                        ]
                    },
                    {
                        type: "meetings",
                        title: "Capitã Marvel",
                        uid: "00000000-0000-0000-0000-000000000000",
                        genre: ["Documentário/Factual", "Show"],
                        startHour: "12:00",
                        endHour: "12:20",
                        duration: "20",
                        room: "Rodadas de Negócio (Sala 1)",
                        table: "10",
                        player: {
                            name: "Marlin Homologação",
                            uid: "f66fa733-392a-40b3-9c66-353ed7a7f21c"
                        },
                        producer: {
                            name: "Razão Social Aron",
                            uid: "b26763a4-f789-4e96-a947-b1201796425e"
                        }

                    },
                    {
                        type: "meetings",
                        title: "A Origem",
                        uid: "00000000-0000-0000-0000-000000000000",
                        genre: ["ficção", "ação"],
                        startHour: "14:00",
                        endHour: "14:20",
                        duration: "20",
                        table: "25",
                        room: "Rodadas de Negócio (Sala 1)",
                        player: {
                            name: "JOSÉ AURELIO DO CARMO",
                            uid: "8e2aed9b-101a-4e97-8f9d-43127cb82cce"
                        },
                        producer: {
                            name: "Teste Producer 01",
                            uid: "23c29e0a-8386-479c-8c02-bdf77e1829d0"
                        }
                    },
                    {
                        type: "meetings",
                        title: "StartCraft2",
                        uid: "00000000-0000-0000-0000-000000000000",
                        genre: ["ficção"],
                        startHour: "14:30",
                        endHOur: "14:50",
                        duration: "30",
                        room: "Rodadas de Negócio (Sala 2)",
                        table: "20",
                        player: {
                            name: "Marlin Desenvolvimento04 Networks International Brasil (Universal, Syfy, Studio Universal)",
                            uid: "ad205191-2c31-4c79-a94d-002185798a47"
                        },
                        producer: {
                            name: "Teste de exibição de detalhe",
                            uid: "aec07033-9c7f-47f1-bce0-2e51d20807d2"
                        }
                    }
                ]


            },
            {
                date: "06/04/2018",
                dayOfWeek: "Sexta-Feira",
                items: [
                    {
                        type: "conference",
                        title: "Palestra de Sexta-Feira",
                        uid: "00000000-0000-0000-0000-000000000000",
                        startHour: "11:00",
                        endHour: "11:30",
                        duration: "50",
                        room: "Sala 1: Grande Sala",
                        lecturers: [
                            { name: "Aurélio Ribeiro alterado", uid: "1c54f334-8d5f-425d-8d1c-b96e7c40b525" }
                        ]
                    }
                ]


            },
            {
                date: "07/04/2018",
                dayOfWeek: "Sábado",
                items: [

                ]


            }
            //{
            //    date: "08/04/2018",
            //    dayOfWeek: "Domingo",
            //    items: [

            //    ]


            //}
        ];
        vm.eventsList = [];

        /*METHODS*/
        vm.growTd = growTd;
        vm.shrinkTd = shrinkTd;  
        vm.getEventItems = getEventItems;
        vm.getHours = getHours;
        vm.getDays = getDays;
        vm.getEvent = getEvent;
        vm.setSpan = setSpan;
        vm.showModalAgenda = showModalAgenda;


        /*METHODS IMPLEMENTATIONS*/
        function init() {
            vm.loadingSchedule = true;

            $q.all([
                getDays(),
                getHours(),
                getEventItems()
            ])
                .then()
                .catch()
                .finally(function () {
                    vm.loadingSchedule = false;
                    
                    if (angular.isDefined(vm.area) && vm.area == "print") {
                        $timeout(function () {
                            window.print();
                        }, 1000);
                        
                    }
                })
        }

        init();      

        $(document).on({
            mouseenter: function (e) {
                let theTarget = angular.element(e.currentTarget);

                if (theTarget.hasClass('green-flag') && !theTarget.parents("td").hasClass("grow-td")) {
                    theTarget.parents("td").addClass("grow-td");
                    theTarget.parent().find(".orange-flag").addClass("hide-div");
                }

            },
            mouseleave: function (e ) {
                let theTarget = angular.element(e.currentTarget);

                if (theTarget.hasClass('green-flag') && theTarget.parents("td").hasClass("grow-td")) {
                    theTarget.parents("td").removeClass("grow-td");
                    theTarget.parent().find(".orange-flag").removeClass("hide-div");
                }
            }
        }, '.wrap-agenda-1 .wrap-agenda-2 div');

        function growTd(target) { 
            //let theTarget = angular.element(target);
            
            //if (theTarget.hasClass('green-flag') && !theTarget.parents("td").hasClass("grow-td")) {
            //    theTarget.parents("td").addClass("grow-td");
            //    theTarget.parent().find(".orange-flag").addClass("hide-div");
            //}
           
        }    
       
        function shrinkTd(target) { 
            //let theTarget = angular.element(target);
                     
            //if (theTarget.hasClass('green-flag') && theTarget.parents("td").hasClass("grow-td")) {
            //    theTarget.parents("td").removeClass("grow-td");
            //    theTarget.parent().find(".orange-flag").removeClass("hide-div");
            //}
           
        }

        function getEventItems() {
            //vm.eventsList.map(el => el.items).forEach(el => vm.eventItems.push(el));

            var url = "/api/schedule/player";

            if (angular.isDefined(vm.area) && vm.area == "producer") {
                url = "/api/schedule/producer";
            }

            return $http.get(url)
                .then(function (response) {
                    if (angular.isArray(response.data)) {
                        vm.eventsList = response.data;                        
                    }
                })
                .catch(function () {

                })
                .finally(function () {

                });
        }

        function getEvent(date, hour) {
            var result = [];

            if (angular.isDefined(vm.eventsList) && angular.isArray(vm.eventsList)) {

                var tmpResultByDate = vm.eventsList.filter(el => el.date === date).map(el => el.items)[0];

                if (angular.isDefined(tmpResultByDate)) {
                    //result = tmpResultByDate.filter(el => el.startHour === hour);

                    result = tmpResultByDate.filter(function (i) {
                        var startTimeMoment = moment(i.startHour, "hh:mm");
                        var endTimeMoment = moment(i.endHour, "hh:mm");
                        var currentMoment = moment(hour, "hh:mm");
                        var currentMomentAdd = moment(hour, "hh:mm").add(30, 'm');

                        if (currentMoment.isSame(startTimeMoment) || startTimeMoment.isBetween(currentMoment, currentMomentAdd)) {
                            return true;
                        }

                        //if (currentMoment.isBetween(startTimeMoment, endTimeMoment)) {                            
                        //    return true;
                        //}     
                    });
                }
            }


            return result;
        }

        function getHours() {
            for (let i = 10; i <= 24; i++) {
                let num = i;
                if (i >= 24) {
                    num = i;
                    num = i - 24;
                }
                if (num < 10)
                    num = "0" + num;

                if (i == 24) {
                    vm.hours.push(num + ":" + "00");
                } else {
                    vm.hours.push(num + ":" + "00");
                    vm.hours.push(num + ":" + "30");
                }
            }
        }

        function getDays() {
            vm.daysObjectList = [];

            //vm.eventsList.forEach(el => {
            //    //vm.daysObjectList.push({
            //    //    dayOfWeek: el.dayOfWeek,
            //    //    date: el.date
            //    //});

            //    vm.dateList.push(el.date);
            //});


            return $http.get('/api/schedule/days')
                .then(function (response) {
                    if (angular.isArray(response.data)) {
                        vm.daysObjectList = response.data;
                    }
                })
                .catch(function () {

                })
                .finally(function () {

                });

        }

        function setSpan(duration) {

            let height = 39,
                durIncrement = 30,
                hightIncrement = 40;


            let spanIncrementer = function (duration) {
                durIncrement += 30;
                hightIncrement += 40;
                if (duration <= 30)
                    return height;

                else if (duration > 30 && duration < 60)
                    return height += hightIncrement;

                else if (duration >= durIncrement && duration < durIncrement + 30)
                    return height += hightIncrement;

                else
                    return spanIncrementer(duration);

            }

            height = spanIncrementer(duration);

            return "height:" + height + "px;";
            //return "height:" + 39 + "px;";
        }

        function showModalAgenda(item, date) {
            vm.messageResultProcessing = {};
            vm.thisItem = item;
            vm.thisDate = date;
            vm.modalAgenda = $uibModal.open({
                animation: true,
                ariaLabelledBy: 'modal-title-bottom',
                ariaDescribedBy: 'modal-body-bottom',
                templateUrl: 'modalAgenda.html',
                size: 'md',
                scope: $scope,
                controllerAs: 'vmModalAgenda',
                windowClass: 'modal-messages-success',
                controller: function () {
                    $scope.dismiss = function () {
                        vm.modalAgenda.dismiss('cancel');

                    }
                    return vm;
                }
            });
        }
    }
})();
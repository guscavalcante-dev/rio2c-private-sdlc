(function () {
    'use strict';

    angular
        .module('Message')
        .controller('MessageList', MessageList);

    MessageList.$inject = ['$scope', '$http', '$element', '$log', '$timeout', 'messageDataService', '$uibModal', '$alert'];

    function MessageList($scope, $http, $element, $log, $timeout, messageDataService, $uibModal, $alert) {
        var vm = this;

        $.connection.url = "/signalr";
        $.connection.hub.url = "/signalr";

        //--PROPERTIES--////////////////////////////////////////////////
        vm.goBackStatus = true;
        vm.iconElement = "icon-arrow-right";
        vm.buttonPosition = "repositioned";
        vm.activeProducer = "";
        vm.activePlayer = "active";
        vm.executiveGroup = "players";
        vm.messageTemp = "";
        vm.enterToSend = false;
        vm.collaboratorSelected = null;
        vm.loadingListCollaborator = false;
        vm.allCollaborators = [];
        vm.collaborators = [];
        vm.unreadsMessages = [];
        vm.messageHub = $.connection.messageHub;
        vm.offline = false;
        vm.termSearch = null;

        //--METHODS--////////////////////////////////////////////////

        vm.changeBackButton = changeBackButton;
        vm.choosePlayer = choosePlayer;
        vm.chooseProducer = chooseProducer;
        vm.scrollChatDown = scrollChatDown;
        vm.checkIfEnter = checkIfEnter;
        vm.checkIfShiftEnter = checkIfShiftEnter;
        vm.selectCollaborator = selectCollaborator;
        vm.filterByTerm = filterByTerm;
        vm.sendMessage = sendMessage;
        vm.loadChat = loadChat;
        vm.getUnreadsMessages = getUnreadsMessages;
        vm.messageHub.client.receiveMessage = receiveMessage;
        vm.messageHub.client.addUnreadsMessages = addUnreadsMessages;
        vm.orderByUnreadsMessages = orderByUnreadsMessages;

        //--IMPLEMENTS--////////////////////////////////////////////////////
        function orderByUnreadsMessages(collaborator) {
            return [-(collaborator.unreadsMessages.length), collaborator.name];
        }


        $scope.$watch(function () {
            return vm.executiveGroup
        }, function (newValue, oldValue) {
            if (newValue !== oldValue) {
                filterCollaborators();
            }
        });

        function loadChat() {
            getListCollaborator();
        }


        function checkIfShiftEnter(e) {

            if (!e.shiftKey && vm.enterToSend) {
                checkIfEnter(e);

            }

        }

        function checkIfEnter(e) {
            if (e.keyCode === 13) {

                return vm.sendMessage();
            }
        }

        function scrollChatDown() {
            $timeout(function () {
                if (angular.isDefined(angular.element('.chat-history')[0])) {
                    angular.element('.chat-history').animate({ scrollTop: (angular.element('.chat-history')[0].scrollHeight) })
                }
            }, 400);

            //$('.chat-history').ready(function () {
            //    var chatHistory = document.querySelector(".chat-history");
            //    var ul = document.querySelector(".chat-history ul");
            //    chatHistory.scrollTop = chatHistory.scrollHeight;
            //});
        }

        function choosePlayer() {
            vm.activeProducer = "";
            vm.activePlayer = "active";
            vm.executiveGroup = "players";
        }

        function chooseProducer() {
            vm.activePlayer = "";
            vm.activeProducer = "active";
            vm.executiveGroup = "producers";
        }

        function filterByTerm() {
            var term = angular.copy(vm.termSearch);

            if (term !== null && term !== "") {
                term = term.toLowerCase();
            }

            if (term !== null && term !== "") {
                if (vm.executiveGroup === "players") {
                    vm.collaborators = vm.allCollaborators.filter(function (item) { return item.isPlayer }).filter(function (item) { return item.name.toLowerCase().indexOf(term) !== -1 || item.playersName.toLowerCase().indexOf(term) !== -1 });
                }
                else if (vm.executiveGroup === "producers") {
                    vm.collaborators = vm.allCollaborators.filter(function (item) { return item.isProducer }).filter(function (item) { return item.name.toLowerCase().indexOf(term) !== -1 || (item.producerName != null && item.producerName.toLowerCase().indexOf(term) !== -1) });
                }
            }
            else {
                filterCollaborators();
            }
        }

       
        function selectCollaborator(e, collaborator) {
            e.preventDefault();
         
            vm.collaboratorSelected = collaborator;
         
            loadMessagesByCollaborator(collaborator);
        }

        function changeBackButton() {
            vm.goBackStatus = !vm.goBackStatus;

            vm.iconElement = vm.goBackStatus === true ? "icon-arrow-right" : "icon-arrow-left";
            vm.displayChat = vm.goBackStatus === true ? "not-display" : "display";
            vm.displayPeopleList = vm.goBackStatus === true ? "display" : "not-display";
            vm.backgroundBtn = vm.goBackStatus === true ? "background-btn-true" : "background-btn-false";
            vm.buttonPosition = vm.goBackStatus === true ? "repositioned" : "first-position";
        }

        function filterCollaborators() {
            if (vm.termSearch !== null && vm.termSearch !== "") {
                filterByTerm();
            }
            else {
                if (vm.executiveGroup === "players") {
                    vm.collaborators = vm.allCollaborators.filter(function (item) { return item.isPlayer });
                }
                else if (vm.executiveGroup === "producers") {
                    vm.collaborators = vm.allCollaborators.filter(function (item) { return item.isProducer });
                }
                else {
                    vm.collaborators = [];
                }
            }
        }

        function getListCollaborator() {
            vm.loadingListCollaborator = true;

            $http.get('/api/collaborators/optionschat', {

            })
                .then(function (response) {
                    if (angular.isArray(response.data)) {
                        vm.allCollaborators = response.data;
                        getUnreadsMessages();
                    }
                    else {
                        vm.allCollaborators = [];
                    }

                    filterCollaborators();
                })
                .catch(function () {
                    vm.allCollaborators = [];
                })
                .finally(function () {
                    vm.loadingListCollaborator = false;
                });
        }

        function checkUnreadMessages() {
            if (vm.allCollaborators !== null && angular.isArray(vm.allCollaborators) && vm.unreadsMessages !== null && angular.isArray(vm.unreadsMessages)) {
                angular.forEach(vm.allCollaborators, function (collaborator) {
                    collaborator.unreadsMessages = vm.unreadsMessages.filter(function (m) { return (m.senderEmail === collaborator.email) });
                });
            }
        }

        function loadMessagesByCollaborator(collaborator) {
            collaborator.loadingMessages = true;

            var params = {};

            params.email = collaborator.email;

            $http.get('/api/messages', {
                params: params
            })
                .then(function (response) {
                    if (angular.isArray(response.data)) {
                        collaborator.messages = response.data;

                        if (collaborator.uid == vm.collaboratorSelected.uid) {
                            markListMessageAsRead(collaborator.messages);
                        }

                        scrollChatDown();

                    }
                })
                .catch(function () {
                    vm.messages = [];
                })
                .finally(function () {
                    collaborator.loadingMessages = false;
                });
        }

        function getUnreadsMessages() {
            vm.unreadsMessages = [];

            messageDataService.getUnreadsMessages()
                .then(function (response) {
                    vm.unreadsMessages = response;
                    checkUnreadMessages();
                })
                .catch(function (error) {
                    vm.unreadsMessages = error;
                })
                .finally(function () {

                });
        }

        function _markMessageAsRead(messages) {
            angular.forEach(messages, function (message) {
                message.isRead = true;
            });

            messageDataService.markMessagesAsRead(messages);

            getUnreadsMessages();
        }

        function markListMessageAsRead(messages) {
            var messagesUnread = messages.filter(function (i) { return !i.isRead });
            var data = {};

            if (messagesUnread.length > 0) {
                data.Uids = messagesUnread.map(function (i) { return i.uid; });


                $http({
                    url: '/api/messages/markmessageasread',
                    method: 'POST',
                    data: data
                })
                    .then(function (response) {
                        _markMessageAsRead(messages);
                    })
                    .catch(function () {

                    });
            }
        }

        function markMessageAsRead(message) {

            var data = {
                Uids: [message.uid]
            }

            $http({
                url: '/api/messages/markmessageasread',
                method: 'POST',
                data: data
            })
                .then(function (response) {
                    _markMessageAsRead([message]);
                })
                .catch(function () {

                });

        }

        //--IMPLEMENTS HUB///////////////////////////////////////////////////
        $.connection.hub.stateChanged(function (state) {
            $scope.$apply(function () {
                if ($.connection.hub && $.connection.hub.state === $.signalR.connectionState.disconnected) {
                    vm.offline = true;
                }
            });
        });

        function startHub() {
            $.connection.hub.start({})
                .done(function () {
                    $scope.$apply(function () {
                        vm.offline = false;
                    });
                })
                .fail(function (error) {

                });
        }
        startHub();


        function addUnreadsMessages(unreadMessage) {
            $scope.$apply(function () {
                unreadMessage = JSON.parse(unreadMessage);

                if (vm.collaboratorSelected != null && vm.collaboratorSelected.email == unreadMessage.senderEmail) {
                    markMessageAsRead(unreadMessage);
                }
                else {
                    messageDataService.addUnreadsMessages(unreadMessage);
                    getUnreadsMessages();
                }

            });
        }

        function receiveMessage(emailSender, message) {
            $scope.$apply(function () {
                message = JSON.parse(message);

                if (vm.allCollaborators !== null) {
                    angular.forEach(vm.allCollaborators, function (item) {
                        if (item.email === emailSender) {
                            if (angular.isDefined(item.messages) && angular.isArray(item.messages)) {
                                item.messages.push(message);
                                scrollChatDown();
                            }
                            else {
                                //item.messages = [message];
                                loadMessagesByCollaborator(item);
                            }
                        }

                    });
                }
            });

        }

        function sendMessage() {
            //if ($.connection.hub && $.connection.hub.state === $.signalR.connectionState.disconnected) {
            //    $.connection.hub.start();
            //}

            if (vm.message !== null && vm.message !== "") {

                vm.sendingMessage = true;


                //vm.message = vm.messageTemp;

                vm.messageHub.server.sendMessage(vm.collaboratorSelected.email, vm.message)
                    .done(function (response) {


                        $scope.$apply(function () {
                            if (angular.isDefined(response)) {
                                response = JSON.parse(response);
                                //response.text = response.text.replace(/\\n/g, "<br/>");  
                                //let tempResponse = JSON.stringify(response).replace(/\\n/g, '<br/>'); //convert to JSON string
                                //response = JSON.parse(tempResponse); //convert back to array

                                //window.myresponse = response;
                                //console.log(window.myresponse);
                                
                                if (angular.isDefined(vm.collaboratorSelected.messages)) {
                                    vm.collaboratorSelected.messages.push(response);
                                }
                                else {
                                    vm.collaboratorSelected.messages = [response];
                                }


                                if (!angular.isDefined(response.errors)) {
                                    scrollChatDown();
                                    if (angular.isDefined(vm.collaboratorSelected.messages) && angular.isArray(vm.collaboratorSelected.messages)) {


                                    }
                                    else {
                                        vm.collaboratorSelected.messages = [response];
                                    }

                                    vm.sendingMessage = false;
                                    vm.message = null;
                                }
                                else {
                                    console.error(response.errors);
                                    vm.errosSendMessage = {};
                                    $alert.show(vm.errosSendMessage, response.errors.map(function (i) { return i.message }), 'danger');

                                    vm.modalErrorSendMessage = $uibModal.open({
                                        animation: true,
                                        ariaLabelledBy: 'modal-title-bottom',
                                        ariaDescribedBy: 'modal-body-bottom',
                                        templateUrl: 'modalErrorSendMessage.html',
                                        size: 'sm',
                                        scope: $scope,
                                        controllerAs: 'vmModalErrorSendMessage',
                                        windowClass: 'modal-messages-error',
                                        controller: function () {
                                            $scope.dismiss = function () {
                                                vm.modalErrorSendMessage.dismiss('cancel');
                                                vm.errosSendMessage = {};
                                            }
                                            return vm;
                                        }
                                    });

                                    vm.sendingMessage = false;
                                }
                            }
                        });
                    })
                    .fail(function (error) {
                        $scope.$apply(function () {
                            console.error('sendMessage', error);
                        });
                    });
            }
        }

        ///////////////////////////////////////////////////////////////////
    }
})();
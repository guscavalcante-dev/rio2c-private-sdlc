// service
angular
    .module('Message')
    .factory('messageDataService', messageDataService);

messageDataService.$inject = ['$http', '$q'];

function messageDataService($http, $q) {
    var unreadsMessages = [];
    var loadUnreadsMessages = false;

    return {
        getUnreadsMessages: _getUnreadsMessages,
        markMessagesAsRead: _markMessagesAsRead,
        addUnreadsMessages: _addUnreadsMessages
    };

    function _getUnreadsMessages() {
        if (!loadUnreadsMessages) {
            return $http.get('/api/messages/unreadsmessages')
                .then(getUnreadsMessagesComplete)
                .catch(getUnreadsMessagesFailed)
                .finally(function () {
                    loadUnreadsMessages = true;
                });
        }
        else {
            var deferred = $q.defer();

            deferred.resolve(unreadsMessages);

            return deferred.promise;
        }

        function getUnreadsMessagesComplete(response) {
            if (angular.isArray(response.data)) {
                unreadsMessages = response.data;
                return response.data;
            }
            return unreadsMessages;
        }

        function getUnreadsMessagesFailed(error) {
            return unreadsMessages;
        }
    }

    function _markMessagesAsRead(messages) {     
        angular.forEach(unreadsMessages, function (message, index) {
            if (messages.filter(function (i) { return i.uid === message.uid; }).length > 0) {
                unreadsMessages.splice(index, 1);
            }
        });
    }

    function _addUnreadsMessages(unreadMessage) {     
        if (unreadsMessages ===  null) {
            unreadsMessages = [];
        }

        unreadsMessages.push(unreadMessage);
    }
}
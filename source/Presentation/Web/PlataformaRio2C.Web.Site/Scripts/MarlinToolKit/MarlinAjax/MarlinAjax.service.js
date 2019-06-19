(function () {
    'use strict';

    angular
        .module('MarlinAjax')
        .factory("send", send);

    send.$inject = ["$http", "$timeout", "$window", "$q", "$rootScope"];

    function send($http, $timeout, $window, $q, $rootScope) {        
        var _opt = {
            type: 'POST',
            data: {},
            params: {},
            contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            beforeSend: function () { 
                // extensivel 
            },
            ignoreLoadingBar: false,
            headers: {
                '__RequestVerificationToken': angular.element('input[name="__RequestVerificationToken"]').attr('value')
            },
            transformRequest: $http.defaults.transformRequest,
        }     

        function service(configurations) {
            var defer = $q.defer();
            var opt = angular.copy(_opt);
            
            
            if (configurations)
                angular.extend(opt, configurations);


            $http({
                method: opt.type,
                url: $rootScope.compileUrl(opt.url),
                timeout: 0,
                data: opt.data,
                ignoreLoadingBar: opt.ignoreLoadingBar,                
                params: opt.params,
                transformRequest: opt.transformRequest,
                headers: opt.headers                
            })
            .then(function (response) {    
                if (angular.isDefined(response.data.RedirectPath) && response.data.RedirectPath != null) {
                    defer.resolve({ path: response.data.RedirectPath });
                }
                else if (angular.isDefined(response.data.DataSuccess) && response.data.DataSuccess != null) {
                    if (angular.isDefined(response.data.MessageSuccess) && response.data.MessageSuccess != null) {
                        response.data.DataSuccess.MessageSuccess = response.data.MessageSuccess;
                    }                    
                    
                    defer.resolve(response.data.DataSuccess);
                }              
                else if (angular.isDefined(response.data.MessageSuccess) && response.data.MessageSuccess != null) {
                    defer.resolve(response.data.MessageSuccess);
                }              
                else if (angular.isDefined(response.data.ErrorMessage) && response.data.ErrorMessage != null) {
                    defer.reject(response.data.ErrorMessage);
                }
                else if (angular.isDefined(response.data.DataError) && response.data.DataError != null) {
                    defer.reject(response.data.DataError);
                }
                else {
                    defer.reject("Ocorreu um erro ao processar a requisição (Código: " + response.status + "). Por favor tente novamente!");
                }
            })
            .catch(function (response) {
                if (response.status == 403 && angular.isDefined(response.data.ErrorMessage)) {
                    defer.reject("Ocorreu um erro ao processar a requisição (Código: " + response.status + ")." + response.data.ErrorMessage);
                }
                else if (response.status == -1) {
                    defer.reject("Tempo limite da requisição foi excedido. Por favor tente novamente!");
                }
                else {
                    defer.reject("Ocorreu um erro ao processar a requisição (Código: " + response.status + "). Por favor tente novamente!");
                }
            });

            return defer.promise;
        }

        function serviceFile(configurations) {
            var defer = $q.defer();
            var opt = angular.copy(_opt);

            if (configurations)
                angular.extend(opt, configurations);

            $http({
                method: opt.type,
                url: $rootScope.compileUrl(opt.url),
                data: opt.data,
                ignoreLoadingBar: opt.ignoreLoadingBar,
                params: opt.params,
                headers: {
                    'Content-Type': undefined,
                    '__RequestVerificationToken': angular.element('input[name="__RequestVerificationToken"]').attr('value')
                },
                transformRequest: angular.identity,
                withCredentials: true,
                contentType: undefined
            })
            .then(function (response) {
                if (angular.isDefined(response.data.RedirectPath) && response.data.RedirectPath != null) {
                    defer.resolve({ path: response.data.RedirectPath });
                }
                else if (angular.isDefined(response.data.DataSuccess) && response.data.DataSuccess != null) {
                    if (angular.isDefined(response.data.MessageSuccess) && response.data.MessageSuccess != null) {
                        response.data.DataSuccess.MessageSuccess = response.data.MessageSuccess;
                    }

                    defer.resolve(response.data.DataSuccess);
                }
                else if (angular.isDefined(response.data.MessageSuccess) && response.data.MessageSuccess != null) {
                    defer.resolve(response.data.MessageSuccess);
                }
                else if (angular.isDefined(response.data.ErrorMessage) && response.data.ErrorMessage != null) {
                    defer.reject(response.data.ErrorMessage);
                }
                else if (angular.isDefined(response.data.DataError) && response.data.DataError != null) {
                    defer.reject(response.data.DataError);
                }
                else {
                    defer.reject("Ocorreu um erro ao processar a requisição (Código: " + response.status + "). Por favor tente novamente!");
                }
            },
            function (response) {
                defer.reject("Ocorreu um erro ao processar a requisição (Código: " + response.status + "). Por favor tente novamente!");

            })
            .catch(function (response) {
                defer.reject("Ocorreu um erro ao processar a requisição (Código: " + response.status + "). Por favor tente novamente!");
            });

            return defer.promise;
        }

        return {
            service: service,
            serviceFile: serviceFile
        }
    }
})();
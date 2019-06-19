(function () {
    'use strict';

    angular
        .module('Conference')
        .controller('ConferenceEditor', ConferenceEditor);

    ConferenceEditor.$inject = ['$http', '$element', '$q', '$timeout'];


    function ConferenceEditor($http, $element, $q, $timeout) {
        var vm = this;

        vm.loadingOptions = false;
        vm.lecturers = JSON.parse($element.context.dataset.lecturers);
        vm.lecturerRolesOptions = JSON.parse($element.context.dataset.lecturerRolesOptions);
        vm.roomOptions = JSON.parse($element.context.dataset.roomOptions);
        vm.roomSelected = vm.roomOptions[vm.roomOptions.findIndex(i => i.uid == $element.context.dataset.roomSelected)];
        vm.languageOptions = JSON.parse($element.context.dataset.languageOptions);

        var lecturerDefaultItem = {
            IsPreRegistered: false,
            Name: null,
            Image: null,
            Role: vm.lecturerRolesOptions[0],
            Email: null,
            CompanyName: null,
            JobTitles: angular.copy(vm.languageOptions).map(function (i) {
                return {
                    LanguageName: i.Name,
                    LanguageCode: i.Code
                }
            })

        }

        /////////////////////////////////////////



        /////////////////////////////////////////////

        vm.getLecturerOptions = getLecturerOptions;
        vm.addLecturer = addLecturer;
        vm.removeLecturer = removeLecturer;
        vm.getRoomOptions = getRoomOptions;
        vm.getRoleLecturerOptions = getRoleLecturerOptions;

        ///////////////////////////////////////////////

        function loadingOptions() {           
            vm.loadingOptions = true;            

            $q.all([
                getRoleLecturerOptions(),
                getRoomOptions()
            ])
                .then()
                .catch()
                .finally(function () {
                    vm.loadingOptions = false;

                    $timeout(
                        function ()
                        {
                            $(document).ready(function () {

                                $('.ckeditor-rio2c').each(function () {
                                    var ck = CKEDITOR.replace($(this)[0], {
                                        customConfig: '/Content/js/ckeditor_config.js'
                                    });
                                });
                            })
                        }
                       , 500);
                })
        }
        loadingOptions();

        function getRoleLecturerOptions() {
            return $http.get('/api/rolelecturer', {

            })
                .then(function (response) {
                    if (angular.isArray(response.data)) {
                        angular.forEach(response.data, function (item) {
                            vm.lecturerRolesOptions.push(item);
                        });
                    }
                })
                .catch(function () {
                    vm.lecturerRolesOptions = [];
                })
                .finally(function () {
                    proccessLecturers();
                });
        }       


        function getRoomOptions(term) {
            return $http.get('/api/room', {

            })
                .then(function (response) {

                    if (angular.isArray(response.data)) {
                        angular.forEach(response.data, function (item) {
                            vm.roomOptions.push(item);
                        });
                        vm.roomSelected = vm.roomOptions[vm.roomOptions.findIndex(i => i.uid == $element.context.dataset.roomSelected)];
                    }
                })
                .catch(function () {
                });
        }
        

        function getLecturerOptions(term) {
            return $http.get('/api/collaborator/GetOptions', {
                params: {
                    term: term
                }
            })
                .then(function (response) {

                    if (angular.isArray(response.data)) {
                        return response.data;
                    }
                    else {
                        return [];
                    }
                })
                .catch(function () {
                    return [];
                });

        }

        function addLecturer() {
            vm.lecturers.push(angular.copy(lecturerDefaultItem));
        }

        function removeLecturer(i) {
            vm.lecturers.splice(i, 1);
        }

        function proccessLecturers() {
            if (vm.lecturers == null) {
                vm.lecturers = [angular.copy(lecturerDefaultItem)];
            }
            else {
                vm.lecturers = vm.lecturers.map(function (item) {
                    if (item.Collaborator != null) {
                        var collaborator = {
                            name: item.Collaborator.Name,
                            uid: item.Collaborator.Uid
                        }

                        return {
                            IsPreRegistered: item.IsPreRegistered,
                            Name: item.Name,
                            Image: item.Image,
                            ImageBase64: item.ImageBase64,
                            Collaborator: collaborator,
                            Uid: item.Uid,
                            RoleLecturerUid: item.RoleLecturerUid,
                            Role: vm.lecturerRolesOptions[vm.lecturerRolesOptions.findIndex(i => i.uid == item.RoleLecturerUid)],
                            Email: item.Email,
                            CompanyName: item.CompanyName,
                            JobTitles: item.JobTitles
                        }
                    }
                    else {

                        return {
                            IsPreRegistered: item.IsPreRegistered,
                            Name: item.Name,
                            Image: item.Image,
                            ImageBase64: item.ImageBase64,
                            Uid: item.Uid,
                            RoleLecturerUid: item.RoleLecturerUid,
                            Role: vm.lecturerRolesOptions[vm.lecturerRolesOptions.findIndex(i => i.uid == item.RoleLecturerUid)],
                            Email: item.Email,
                            CompanyName: item.CompanyName,
                            JobTitles: item.JobTitles
                        };
                    }
                });
            }
        }




        function toCamel(o) {
            var newO, origKey, newKey, value
            if (o instanceof Array) {
                newO = []
                for (origKey in o) {
                    if (o.hasOwnProperty(origKey)) {
                        value = o[origKey]
                        if (typeof value === "object") {
                            value = toCamel(value)
                        }
                        newO.push(value)
                    }
                }
            } else {
                newO = {}
                for (origKey in o) {
                    if (o.hasOwnProperty(origKey)) {
                        newKey = (origKey.charAt(0).toLowerCase() + origKey.slice(1) || origKey).toString()
                        value = o[origKey]
                        if (value instanceof Array || (value !== null && value.constructor === Object)) {
                            value = toCamel(value)
                        }
                        newO[newKey] = value
                    }
                }
            }
            return newO
        }

    }

})();
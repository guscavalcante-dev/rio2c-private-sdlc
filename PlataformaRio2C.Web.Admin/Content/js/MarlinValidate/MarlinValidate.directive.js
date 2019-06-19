(function () {
    'use strict';

    angular
    .module('marlin.validade')
    .directive('formValidator', formValidator)
    .controller('controllerdirective', controllerdirective);

    controllerdirective.$inject = ['$scope', '$element', '$parse'];

    function formValidator() {
        return {
            compile: function (el, attrs) {
                var Validator = {};

                Validator.CreateMessages = function (data, formNome, inputNome, $_el) {
                    var $messages = angular.element("<div/>");

                    if ($_el.attr('name').indexOf('$index') !== -1 && angular.isDefined($_el.attr('data-item-field-name'))) {
                        var $errorpropString = "(" + formNome + "['['+$index+']." + $_el.attr('data-item-field-name') + "'])['$error']";
                        var $touchedpropString = "(" + formNome + "['['+$index+']." + $_el.attr('data-item-field-name') + "'])['$touched']";

                        $messages.attr({
                            'data-ng-messages': $errorpropString,
                            'data-ng-show': $touchedpropString +  '|| '+ formNome +'.$submit'
                        });
                    }
                    else {
                        $messages.attr({
                            'data-ng-messages': formNome + "." + inputNome + '.$error',
                            'data-ng-show': formNome + "." + inputNome + '.$touched' 
                        });
                    }

                    if (data.errorRequired) {
                        var $message = angular.element("<div/>");
                        $message.attr({
                            'data-ng-message': 'required',
                            'class': 'help-block has-error'
                        });

                        $message.text(data.errorRequired);
                        $message.appendTo($messages);
                    }

                    //if (data.errorInvalid) {
                    //    var $message = angular.element("<div/>");
                    //    $message.attr({
                    //        'data-ng-message': 'invalid',
                    //        'class': 'help-block has-error'
                    //    });

                    //    $message.text(data.errorInvalid);
                    //    $message.appendTo($messages);
                    //}

                    //if (data.errorCustom) {
                    //    angular.forEach(angular.fromJson(data.errorCustom), function (item) {
                    //        var $message = angular.element("<div/>");
                    //        $message.attr({
                    //            'data-ng-message': item.type,
                    //            'class': 'help-block has-error'
                    //        });

                    //        $message.text(item.msg);
                    //        $message.appendTo($messages);
                    //    });
                    //}
                    return $messages;
                }

                Validator.validateInput = function ($el) {

                    var $this = angular.element($el),
                        $group = angular.element($el.closest('.form-group'));

                    if ($group.attr('data-ng-class')) {
                        var groupNgClass = $group.attr('data-ng-class');

                        groupNgClass = groupNgClass.substring(0, groupNgClass.length - 1);

                        groupNgClass += "|| ";

                        groupNgClass += attrs.name + "." + $this.attr('name') + ".$invalid && " + attrs.name + "." + $this.attr('name') + ".$touched }";


                        $group.attr('data-ng-class', groupNgClass);
                    }
                    else if (angular.isDefined($this.attr('name'))) {
                        if ($this.attr('name').indexOf('$index') !== -1 && angular.isDefined($this.attr('data-item-field-name'))) {
                            var $invalidpropString = "(" + attrs.name + "['['+$index+']." + $this.attr('data-item-field-name') + "'])['$invalid']";
                            var $touchedpropString = "((" + attrs.name + "['['+$index+']." + $this.attr('data-item-field-name') + "'])['$touched'] || " + attrs.name + ".$submit)";

                            $group.attr("data-ng-class", "{'has-error' : " + $invalidpropString + " && " + $touchedpropString + "}");
                        }
                        else if ($this.attr('name').indexOf('$index') === -1) {
                            $group.attr("data-ng-class", "{'has-error' : " + attrs.name + "." + $this.attr('name') + ".$invalid && " + attrs.name + "." + $this.attr('name') + ".$touched }");
                        }
                    }

                    if ($this.data("error")) {
                        var $messages = Validator.CreateMessages($this.data(), attrs.name, $this.attr('name'), $this);
                        $messages.appendTo($group);
                    }
                }

                Validator.init = function () {
                    el.attr("novalidate", true);

                    el.find('input, select, textarea').each(function (e) {
                        Validator.validateInput($(this));
                    });
                }

                Validator.init();
            },           
            controller:  controllerdirective
        };       
    }

    function controllerdirective($scope, $element, $parse) {
        $element.on('submit', function () {
            var objForm = $parse($element.attr('name'))($scope);

            $scope.$apply(function () {
                objForm.$submit = true;
            });

            if (objForm.$invalid) {
                $('html,body').animate({ scrollTop: ($('.has-error').offset().top) - 100 });

                return false;
            }
        });
    }
})();
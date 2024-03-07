var foolproof = function () { };
foolproof.is = function (value1, operator, value2, passOnNull) {
    if (passOnNull) {
        var isNullish = function (input) {
            return input == null || input == undefined || input == "";
        };

        var value1nullish = isNullish(value1);
        var value2nullish = isNullish(value2);

        if ((value1nullish && !value2nullish) || (value2nullish && !value1nullish))
            return true;
    }

    var isNullOrEmpty = function (value) {
        if (typeof (value) === 'undefined' || value == null || value === '' || isNaN(value)) {
            return true;
        }

        return false;
    };

    var isNumeric = function (input) {
        return (input - 0) == input && input.length > 0;
    };

    var isDate = function (input) {    
        //This is a custom fix designed by Softo developers at 11/30/2021. It's consider Globalize culture.
        //The original MVCFoolproof doesn't consider Globalize.
        return moment(input, Globalize.culture().calendar.patterns.d.toUpperCase(), true).isValid();
    };

    var isBool = function (input) {
        return input === true || input === false || input === "true" || input === "false";
    };

    if (isDate(value1) != 'NaN' && isDate(value1)) {
        //This is a custom fix designed by Softo developers at 11/30/2021. It's consider Globalize culture. 
        //The original MVCFoolproof doesn't consider Globalize.
        var dateValue1 = Date.parse(moment(value1, Globalize.culture().calendar.patterns.d.toUpperCase()).format('YYYY-MM-DD'));
        var dateValue2 = Date.parse(moment(value2, Globalize.culture().calendar.patterns.d.toUpperCase()).format('YYYY-MM-DD'));

        if (isNullOrEmpty(dateValue1)) {
            value1 = new Date(value1.split('/').reverse().join('-'));
        }
        else {
            value1 = dateValue1;
        }

        if (isNullOrEmpty(dateValue2)) {
            value2 = new Date(value2.split('/').reverse().join('-'));
        }
        else {
            value2 = dateValue2;
        }
    }
    else if (isBool(value1)) {
        if (value1 == "false") value1 = false;
        if (value2 == "false") value2 = false;
        value1 = !!value1;
        value2 = !!value2;
    }
    else if (isNumeric(value1)) {
        value1 = parseFloat(value1);
        value2 = parseFloat(value2);
    }

    switch (operator) {
        case "EqualTo": if (value1 == value2) return true; break;
        case "NotEqualTo": if (value1 != value2) return true; break;
        case "GreaterThan": if (value1 > value2) return true; break;
        case "LessThan": if (value1 < value2) return true; break;
        case "GreaterThanOrEqualTo": if (value1 >= value2) return true; break;
        case "LessThanOrEqualTo": if (value1 <= value2) return true; break;
        case "RegExMatch": return (new RegExp(value2)).test(value1); break;
        case "NotRegExMatch": return !(new RegExp(value2)).test(value1); break;
    }

    return false;
};

foolproof.getId = function (element, dependentPropety) {
    var pos = element.id.lastIndexOf("_") + 1;
    return element.id.substr(0, pos) + dependentPropety.replace(/\./g, "_");
};

foolproof.getName = function (element, dependentPropety) {
    var pos = element.name.lastIndexOf(".") + 1;
    return element.name.substr(0, pos) + dependentPropety;
};

(function () {
    jQuery.validator.addMethod("is", function (value, element, params) {
        var dependentProperty = foolproof.getId(element, params["dependentproperty"]);
        var operator = params["operator"];
        var passOnNull = params["passonnull"];
        var dependentValue = document.getElementById(dependentProperty).value;

        if (foolproof.is(value, operator, dependentValue, passOnNull))
            return true;

        return false;
    });

    jQuery.validator.addMethod("requiredif", function (value, element, params) {
        var dependentProperty = foolproof.getName(element, params["dependentproperty"]);
        var dependentTestValue = params["dependentvalue"];
        var operator = params["operator"];
        var pattern = params["pattern"];
        var dependentPropertyElement = document.getElementsByName(dependentProperty);
        var dependentValue = null;

        if (dependentPropertyElement.length > 1) {
            for (var index = 0; index != dependentPropertyElement.length; index++)
                if (dependentPropertyElement[index]["checked"]) {
                    dependentValue = dependentPropertyElement[index].value;
                    break;
                }

            if (dependentValue == null)
                dependentValue = false
        }
        else
            dependentValue = dependentPropertyElement[0].value;

        if (foolproof.is(dependentValue, operator, dependentTestValue)) {
            if (pattern == null) {
                if (value != null && value.toString().replace(/^\s\s*/, '').replace(/\s\s*$/, '') != "")
                    return true;
            }
            else
                return (new RegExp(pattern)).test(value);
        }
        else
            return true;

        return false;
    });

    jQuery.validator.addMethod("requiredifempty", function (value, element, params) {
        var dependentProperty = foolproof.getId(element, params["dependentproperty"]);
        var dependentValue = document.getElementById(dependentProperty).value;

        if (dependentValue == null || dependentValue.toString().replace(/^\s\s*/, '').replace(/\s\s*$/, '') == "") {
            if (value != null && value.toString().replace(/^\s\s*/, '').replace(/\s\s*$/, '') != "")
                return true;
        }
        else
            return true;

        return false;
    });

    jQuery.validator.addMethod("requiredifnotempty", function (value, element, params) {
        var dependentProperty = foolproof.getId(element, params["dependentproperty"]);
        var dependentValue = document.getElementById(dependentProperty).value;

        if (dependentValue != null && dependentValue.toString().replace(/^\s\s*/, '').replace(/\s\s*$/, '') != "") {
            if (value != null && value.toString().replace(/^\s\s*/, '').replace(/\s\s*$/, '') != "")
                return true;
        }
        else
            return true;

        return false;
    });

    var setValidationValues = function (options, ruleName, value) {
        options.rules[ruleName] = value;
        if (options.message) {
            options.messages[ruleName] = options.message;
        }
    };

    var $Unob = $.validator.unobtrusive;

    $Unob.adapters.add("requiredif", ["dependentproperty", "dependentvalue", "operator", "pattern"], function (options) {
        var value = {
            dependentproperty: options.params.dependentproperty,
            dependentvalue: options.params.dependentvalue,
            operator: options.params.operator,
            pattern: options.params.pattern
        };
        setValidationValues(options, "requiredif", value);
    });

    $Unob.adapters.add("is", ["dependentproperty", "operator", "passonnull"], function (options) {
        setValidationValues(options, "is", {
            dependentproperty: options.params.dependentproperty,
            operator: options.params.operator,
            passonnull: options.params.passonnull
        });
    });

    $Unob.adapters.add("requiredifempty", ["dependentproperty"], function (options) {
        setValidationValues(options, "requiredifempty", {
            dependentproperty: options.params.dependentproperty
        });
    });

    $Unob.adapters.add("requiredifnotempty", ["dependentproperty"], function (options) {
        setValidationValues(options, "requiredifnotempty", {
            dependentproperty: options.params.dependentproperty
        });
    });
})();


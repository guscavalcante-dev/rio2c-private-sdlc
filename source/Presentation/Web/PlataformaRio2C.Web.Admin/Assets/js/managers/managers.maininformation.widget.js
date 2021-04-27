
var ManagersMainInformationWidget = function () {

    var widgetElementId = '#ManagersMainInformationWidget';
    var widgetElement = $(widgetElementId);

    
    // Show ---------------------------------------------------------------------------------------
    
    var show = function () {
        if (widgetElement.length <= 0) {
            return;
        }

        var jsonParameters = new Object();
        jsonParameters.collaboratorUid = $('#AggregateId').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Collaborators/ShowMainInformationWidget'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    //enableShowPlugins();
                },
                // Error
                onError: function () {
                }
            });
        })
            .fail(function () {
            })
            .always(function () {                
            });
    };

    return {
        init: function () {
            //MyRio2cCommon.block({ idOrClass: widgetElementId });
            show();
        },
        show: function () {
            show();
        }
    };
}();
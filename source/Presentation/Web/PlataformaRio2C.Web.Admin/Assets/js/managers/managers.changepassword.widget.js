
var ManagersChangePassword = function () {

    
    var executeChangePassword = function (userId, newPassword) {
        MyRio2cCommon.block();

        var jsonParameters = new Object();
        jsonParameters.userId = 2;
        jsonParameters.newPassword = "!MinhaSenha123";

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Managers/UpdatePassword'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    if (typeof (ManagersDataTableWidget) !== 'undefined') {
                        ManagersDataTableWidget.refreshData();
                    }

                    if (typeof (ManagersTotalCountWidget) !== 'undefined') {
                        ManagersTotalCountWidget.init();
                    }

                    if (typeof (ManagersEditionCountWidget) !== 'undefined') {
                        ManagersEditionCountWidget.init();
                    }
                },
                // Error
                onError: function () {
                }
            });
        })
            .fail(function () {
            })
            .always(function () {
                MyRio2cCommon.unblock();
            });
    };

    var showModal = function (collaboratorUid) {
        var message = 'Confirme a alteração de senha'; // labels.changePasswordConfirmationMessage;

        bootbox.dialog({
            message: message,
            buttons: {
                cancel: {
                    label: labels.cancel,
                    className: "btn btn-secondary mr-auto",
                    callback: function () {
                    }
                },
                confirm: {
                    label: labels.confirm,
                    className: "btn btn-danger",
                    callback: function () {
                        executeChangePassword(collaboratorUid);
                    }
                }
            }
        });
    };

    return {
        showModal: function (collaboratorUid) {
            showModal(collaboratorUid);
        }
    };
}();
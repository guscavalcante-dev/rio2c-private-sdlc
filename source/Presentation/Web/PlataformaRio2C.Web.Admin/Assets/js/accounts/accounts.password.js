// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 10-17-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-21-2020
// ***********************************************************************
// <copyright file="accounts.password.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var AccountsPassword = function () {

    var updateModalId = '#UpdatePasswordModal';
    var updateFormId = '#UpdatePasswordForm';

    // Update -------------------------------------------------------------------------------------
    var enableAjaxForm = function () {
        MyRio2cCommon.enableAjaxForm({
            idOrClass: updateFormId,
            onSuccess: function (data) {
                $(updateModalId).modal('hide');
            },
            onError: function (data) {
                if (MyRio2cCommon.hasProperty(data, 'pages')) {
                    enableUpdatePlugins();
                }

                $(updateFormId).find(":input.input-validation-error:first").focus();
            }
        });
    };

    var enableUpdatePlugins = function () {
        if (typeof (MyRio2cShowHidePassword) !== 'undefined') {
            MyRio2cShowHidePassword.init();
        }

        enableAjaxForm();
        MyRio2cCommon.enableFormValidation({ formIdOrClass: updateFormId, enableHiddenInputsValidation: true, enableMaxlength: true });
    };

    var showUpdateModal = function () {
        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();
        jsonParameters.organizationUid = $('#AggregateId').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Account/ShowUpdatePasswordModal'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
            data: data,
            // Success
            onSuccess: function () {
                enableUpdatePlugins();
                $(updateModalId).modal();
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

    // Reset -------------------------------------------------------------------------------------
    var executeResetPassword = function (userId, newPassword) {
        MyRio2cCommon.block();

        var jsonParameters = new Object();
        jsonParameters.userId = userId;
        jsonParameters.newPassword = newPassword;

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Account/ResetPasswordAdmin'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    if (typeof (AdministratorsDataTableWidget) !== 'undefined') {
                        AdministratorsDataTableWidget.refreshData();
                    }

                    if (typeof (AdministratorsTotalCountWidget) !== 'undefined') {
                        AdministratorsTotalCountWidget.init();
                    }

                    if (typeof (AdministratorsEditionCountWidget) !== 'undefined') {
                        AdministratorsEditionCountWidget.init();
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

    var showResetModal = function (userId) {
        var html = '<form action="xyzzzzzz" enctype="multipart/form-data" id="ChangePasswordManagerForm" method="post">                    <div id="form-container">';
        html += '   <div class="kt-section">';
        html += '     <input id="IsCreatingNewManager" name="IsCreatingNewManager" type="hidden" value="True">';
        html += '     <div class="kt-section__content mt-3">';
        html += '       <div class="form-group row">';
        html += '           <div class="col-md-6" style="position: relative;">';
        html += '               <label class="control-label" for="Password">Senha</label>';
        html += '               <input autocomplete="off" class="form-control showhidepassword maxlength-enabled valid" data-val="true" data-val-length="O campo Senha deve ter entre 6 e 100 caracteres." data-val-length-max="100" data-val-length-min="6" data-val-requiredif="O campo é obrigatório." data-val-requiredif-dependentproperty="IsCreatingNewManager" data-val-requiredif-dependentvalue="True" data-val-requiredif-operator="EqualTo" id="Password" name="Password" type="password">';
        html += '               <span class="text-danger field-validation-valid" data-valmsg-for="Password" data-valmsg-replace="true"></span>';
        html += '           </div>';
        html += '           <div class="col-md-6">';
        html += '               <label class="control-label" for="ConfirmPassword">Confirmação de senha</label>';
        html += '               <input autocomplete="off" class="form-control showhidepassword" data-val="true" data-val-equalto="Senha de confirmação incorreta." data-val-equalto-other="*.Password" id="ConfirmPassword" name="ConfirmPassword" type="password">';
        html += '               <span class="field-validation-valid text-danger" data-valmsg-for="ConfirmPassword" data-valmsg-replace="true"></span>';
        html += '           </div>';
        html += '       </div>';
        html += '     </div>';
        html += '   </div>';
        html += '</form>';

        bootbox.dialog({
            message: html,
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
                        var formIdOrClass = "#ChangePasswordManagerForm";
                        var validator = $(formIdOrClass).validate({
                            debug: true
                        });

                        if ($(formIdOrClass).valid()) {
                            MyRio2cCommon.block();
                            executeResetPassword(userId, $("#Password").val());
                        }
                        else {
                            validator.focusInvalid();
                            return false;
                        }
                    }
                }
            }
        });

        MyRio2cCommon.enableFormValidation({ formIdOrClass: "#ChangePasswordManagerForm", enableHiddenInputsValidation: true, enableMaxlength: true });
    };

    return {
        showUpdateModal: function () {
            showUpdateModal();
        },
        showResetModal: function (userId) {
            showResetModal(userId);
        }
    };
}();
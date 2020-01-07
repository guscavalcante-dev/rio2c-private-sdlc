// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 01-07-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="conferenceparticipantroles.create.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var ConferenceParticipantRolesCreate = function () {

    var modalId = '#CreateConferenceParticipantRoleModal';
    var formId = '#CreateConferenceParticipantRoleForm';

    // Enable form validation ---------------------------------------------------------------------
    var enableFormValidation = function () {
        MyRio2cCommon.enableFormValidation({ formIdOrClass: formId, enableHiddenInputsValidation: true, enableMaxlength: true });
    };

    // Enable plugins -----------------------------------------------------------------------------
    var enablePlugins = function () {
        enableAjaxForm();
        enableFormValidation();
    };

    // Show modal ---------------------------------------------------------------------------------
    var showModal = function () {
        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/ParticipantRoles/ShowCreateModal'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    enablePlugins();
                    $(modalId).modal();
                },
                // Error
                onError: function() {
                }
            });
        })
        .fail(function () {
        })
        .always(function () {
            MyRio2cCommon.unblock();
        });
    };

    // Enable ajax form ---------------------------------------------------------------------------
    var enableAjaxForm = function () {
        MyRio2cCommon.enableAjaxForm({
            idOrClass: formId,
            onSuccess: function (data) {
                $(modalId).modal('hide');

                if (typeof (ConferenceParticipantRolesDataTableWidget) !== 'undefined') {
                    ConferenceParticipantRolesDataTableWidget.refreshData();
                }

                if (typeof (ConferenceParticipantRolesTotalCountWidget) !== 'undefined') {
                    ConferenceParticipantRolesTotalCountWidget.init();
                }

                if (typeof (ConferenceParticipantRolesEditionCountWidget) !== 'undefined') {
                    ConferenceParticipantRolesEditionCountWidget.init();
                }
            },
            onError: function (data) {
                if (MyRio2cCommon.hasProperty(data, 'pages')) {
                    enablePlugins();
                }
            }
        });
    };

    return {
        showModal: function () {
            showModal();
        }
    };
}();
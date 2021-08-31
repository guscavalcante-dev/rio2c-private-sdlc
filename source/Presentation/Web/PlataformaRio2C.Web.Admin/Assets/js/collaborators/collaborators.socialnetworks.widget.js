// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 08-14-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-14-2021
// ***********************************************************************
// <copyright file="collaborators.socialnetworks.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var CollaboratorsSocialNetworksWidget = function () {

    var widgetElementId = '#SocialNetworksWidget';
    var widgetElement = $(widgetElementId);

    var updateModalId = '#UpdateSocialNetworksModal';
    var updateFormId = '#UpdateSocialNetworksForm';
    var collaboratorTypeNameId = '#CollaboratorTypeName';

    // Show ---------------------------------------------------------------------------------------
    var enableShowPlugins = function () {
        KTApp.initTooltips();
    };

    var show = function (collaboratorType) {
        if (widgetElement.length <= 0) {
            return;
        }

        var jsonParameters = new Object();
        jsonParameters.collaboratorUid = $('#AggregateId').val();
        jsonParameters.collaboratorType = collaboratorType;

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Collaborators/ShowSocialNetworksWidget'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    enableShowPlugins();
                },
                // Error
                onError: function () {
                }
            });
        })
        .fail(function () {
        })
        .always(function () {
            MyRio2cCommon.unblock({ idOrClass: widgetElementId });
        });
    };

    // Update -------------------------------------------------------------------------------------
    var enableAjaxForm = function () {
        MyRio2cCommon.enableAjaxForm({
            idOrClass: updateFormId,
            onSuccess: function (data) {
                $(updateModalId).modal('hide');

                if (typeof (CollaboratorsSocialNetworksWidget) !== 'undefined') {
                    CollaboratorsSocialNetworksWidget.init($(collaboratorTypeNameId).val());
                }
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
        enableAjaxForm();
        MyRio2cCommon.enableFormValidation({ formIdOrClass: updateFormId, enableHiddenInputsValidation: true, enableMaxlength: true });
    };

    var showUpdateModal = function (collaboratorType) {
        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();
        jsonParameters.collaboratorUid = $('#AggregateId').val();
        jsonParameters.collaboratorType = collaboratorType;

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Collaborators/ShowUpdateSocialNetworksModal'), jsonParameters, function (data) {
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

    return {
        init: function (collaboratorType) {
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            show(collaboratorType);
        },
        showUpdateModal: function (collaboratorType) {
            showUpdateModal(collaboratorType);
        }
    };
}();
// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 09-13-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 09-13-2021
// ***********************************************************************
// <copyright file="company.executive.create.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var CompanyExecutiveCreate = function () {
    var createModalId = '#CreateOrganizationExecutiveModal';
    var createFormId = '#CreateOrganizationExecutiveForm';

    // Create ---------------------------------------------------------------------------------------
    var enableAjaxForm = function () {
        MyRio2cCommon.enableAjaxForm({
            idOrClass: createFormId,
            onSuccess: function (data) {
                $(createModalId).modal('hide');

                if (typeof (CompanyExecutiveWidget) !== 'undefined') {
                    AudiovisualOrganizationsExecutiveWidget.init();
                }
            },
            onError: function (data) {
                if (MyRio2cCommon.hasProperty(data, 'pages')) {
                    enablePlugins();
                }

                $(createFormId).find(":input.input-validation-error:first").focus();
            }
        });
    };

    var enablePlugins = function () {
        enableAjaxForm();
        MyRio2cCommon.enableCollaboratorSelect2({ url: '/Collaborators/FindAllExecutivesByFilters' });
        MyRio2cCommon.enableSelect2({ inputIdOrClass: createFormId + ' .enable-select2', allowClear: true });
        MyRio2cCommon.enableFormValidation({ formIdOrClass: createFormId, enableHiddenInputsValidation: true, enableMaxlength: true });
    };

    var showModal = function (attendeeOrganizationUid) {
        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();
        jsonParameters.attendeeOrganizationUid = attendeeOrganizationUid;

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Companies/ShowCreateExecutiveModal'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    enablePlugins();
                    $(createModalId).modal();
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
        showModal: function (attendeeOrganizationUid) {
            showModal(attendeeOrganizationUid);
        }
    };
}();
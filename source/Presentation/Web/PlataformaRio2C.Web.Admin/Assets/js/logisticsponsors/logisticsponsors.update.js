// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 08-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-21-2019
// ***********************************************************************
// <copyright file="collaborators.update.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var LogisticSponsorsUpdate = function () {

    var modalId = '#UpdateLogisticSponsorModal';
    var formId = '#UpdateLogisticSponsorForm';

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
    var showModal = function (uid, isAddingToCurrentEdition) {
        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();
        jsonParameters.sponsorUid = uid;
        jsonParameters.isAddingToCurrentEdition = isAddingToCurrentEdition;

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/LogisticSponsors/ShowUpdateModal'), jsonParameters, function (data) {
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

                if (typeof (CollaboratorsDataTableWidget) !== 'undefined') {
                    CollaboratorsDataTableWidget.refreshData();
                }

                if (typeof (CollaboratorsTotalCountWidget) !== 'undefined') {
                    CollaboratorsTotalCountWidget.init();
                }

                if (typeof (CollaboratorsEditionCountWidget) !== 'undefined') {
                    CollaboratorsEditionCountWidget.init();
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
        showModal: function (collaboratorUid, isAddingToCurrentEdition) {
            showModal(collaboratorUid, isAddingToCurrentEdition);
        }
    };
}();
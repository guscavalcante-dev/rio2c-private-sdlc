// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 09-06-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-06-2019
// ***********************************************************************
// <copyright file="onboarding.collaboratordata.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var OnboardingCollaboratorData = function () {

    //var modalId = '#UpdatePlayerExecutiveModal';
    var formId = '#CollaboratorDataForm';

    // Enable form validation ---------------------------------------------------------------------
    //var enableFormValidation = function () {
    //    MyRio2cCommon.enableFormValidation({
    //        formIdOrClass: formId,
    //        enableHiddenInputsValidation: true
    //    });
    //};

    // Enable plugins -----------------------------------------------------------------------------
    var enablePlugins = function () {
        MyRio2cCropper.init({ formIdOrClass: formId });
        MyRio2cCommon.enableSelect2({ inputIdOrClass: formId + ' .enable-select2' });
        AddressesForm.init();
        //MyRio2cCommon.enableCkEditor({ idOrClass: '.ckeditor-rio2c-jobtitle', maxCharCount: 81 });
        MyRio2cCommon.enableCkEditor({ idOrClass: '.ckeditor-rio2c-minibio', maxCharCount: 710 });
        //enableAjaxForm();
        //enableFormValidation();
    };

    //// Enable ajax form ---------------------------------------------------------------------------
    //var enableAjaxForm = function () {
    //    MyRio2cCommon.enableAjaxForm({
    //        idOrClass: formId,
    //        onSuccess: function (data) {
    //            $(modalId).modal('hide');

    //            if (typeof (CollaboratorsDataTableWidget) !== 'undefined') {
    //                CollaboratorsDataTableWidget.refreshData();
    //            }

    //            if (typeof (CollaboratorsTotalCountWidget) !== 'undefined') {
    //                CollaboratorsTotalCountWidget.init();
    //            }

    //            if (typeof (CollaboratorsEditionCountWidget) !== 'undefined') {
    //                CollaboratorsEditionCountWidget.init();
    //            }
    //        },
    //        onError: function (data) {
    //            if (MyRio2cCommon.hasProperty(data, 'pages')) {
    //                enablePlugins();
    //            }
    //        }
    //    });
    //};

    return {
        init: function() {
            enablePlugins();
        }
        //showModal: function (collaboratorUid, isAddingToCurrentEdition) {
        //    showModal(collaboratorUid, isAddingToCurrentEdition);
        //}
    };
}();
// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 08-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-09-2021
// ***********************************************************************
// <copyright file="administrators.form.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var AdministratorsForm = function () {

    var listId = 'collaboratortypename-list';
    var itemClass = 'collaboratortypename-list-item';
    var removeClass = 'collaboratortypename-remove';
    var listPrefix = 'CollaboratorTypeNames';
    var formId = '';

    // Initialize properties ----------------------------------------------------------------------
    var initializeProperties = function (mainFormId) {
        formId = mainFormId;
    };

    // Enable form plugins ------------------------------------------------------------------------
    var enableFormPlugins = function () {
        MyRio2cCommon.enableSelect2({ inputIdOrClass: '.enable-select2-collaboratortypename' });
        DynamicList.toggleItemRemoveButton(removeClass, itemClass);
    };

    // Add to list --------------------------------------------------------------------------------
    var add = function () {
        DynamicList.addItem(templates.AttendeeOrganization, listId, itemClass, listPrefix);
        enableFormPlugins();

        if (!MyRio2cCommon.isNullOrEmpty(formId)) {
            MyRio2cCommon.enableFormValidation({ formIdOrClass: formId, enableHiddenInputsValidation: true, enableMaxlength: true });
        }
    };

    // Remove from list ---------------------------------------------------------------------------
    var remove = function (element) {
        $('.enable-select2-collaboratortypename').select2('destroy'); 
        DynamicList.removeItem(element, itemClass, listPrefix);
        enableFormPlugins();
    };

    return {
        init: function (mainFormId) {
            initializeProperties(mainFormId);
            enableFormPlugins();
        },
        add: function () {
            add();
        },
        remove: function (element) {
            remove(element);
        }
    };
}();
// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 08-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-21-2019
// ***********************************************************************
// <copyright file="attendeeorganizations.form.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var AttendeeOrganizationsForm = function () {

    var listId = 'attendeeorganization-list';
    var itemClass = 'attendeeorganization-list-item';
    var removeClass = 'attendeeorganization-remove';
    var listPrefix = 'AttendeeOrganizationBaseCommands';
    var formId = '';

    // Initialize properties ----------------------------------------------------------------------
    var initializeProperties = function (mainFormId) {
        formId = mainFormId;
    };

    // Enable form plugins ------------------------------------------------------------------------
    var enableFormPlugins = function () {
        MyRio2cCommon.enableSelect2({ inputIdOrClass: '.enable-select2-attendeeorganization' });
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
        $('.enable-select2-attendeeorganization').select2('destroy'); 
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
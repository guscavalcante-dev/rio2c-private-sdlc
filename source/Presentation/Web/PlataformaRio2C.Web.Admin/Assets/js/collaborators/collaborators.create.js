// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 08-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-21-2019
// ***********************************************************************
// <copyright file="collaborators.create.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var CollaboratorsCreate = function () {

    var modalId = '#CreatePlayerExecutiveModal';
    var formId = '#CreatePlayerExecutiveForm';

    // Enable form validation ---------------------------------------------------------------------
    var enableFormValidation = function () {
        MyRio2cCommon.enableFormValidation({ formIdOrClass: formId, enableHiddenInputsValidation: true, enableMaxlength: true });
    };

    // Enable plugins -----------------------------------------------------------------------------
    var enablePlugins = function () {
        if (typeof (MyRio2cPublicEmail) !== 'undefined') {
            MyRio2cPublicEmail.init();
        }

        MyRio2cCropper.init({ formIdOrClass: formId });
        MyRio2cCommon.enableSelect2({ inputIdOrClass: formId + ' .enable-select2' });
        AttendeeOrganizationsForm.init(formId);
        AddressesForm.init();        
        enableDropdownChangeEvent("CollaboratorGenderUid");
        enableDropdownChangeEvent("CollaboratorRoleUid");
        enableDropdownChangeEvent("CollaboratorIndustryUid");
        enableCheckboxChangeEvent("HasAnySpecialNeeds");
        enableCheckboxChangeEvent("HaveYouBeenToRio2CBefore");
        enableAjaxForm();
        enableFormValidation();
    };

    // Show modal ---------------------------------------------------------------------------------
    var showModal = function () {
        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/PlayersExecutives/ShowCreateModal'), jsonParameters, function (data) {
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
    
    // Enable change events -----------------------------------------------------------------------
    var enableCheckboxChangeEvent = function (elementId) {
        var element = $('#' + elementId);
        
        function toggleChanged(element) {       
            if (element.prop('checked')) {
                $("[data-additionalinfo='"+ element.attr("id") +"']").removeClass('d-none');
            }
            else {
                $("[data-additionalinfo='"+element.attr("id")+"']").addClass('d-none');
            }
        }
        
        toggleChanged(element);

        element.not('.change-event-enabled').on('click', function () {   
            toggleChanged(element);  
        });

        element.addClass('change-event-enabled');
    };

    var enableDropdownChangeEvent = function (elementId) {
        var element = $('#' + elementId);

        function toggleChanged(element) {
            if (element.find(':selected').data('aditionalinfo') === "True") {
                $("[data-additionalinfo='"+ element.attr("id") +"']").removeClass('d-none');
            }
            else {
                $("[data-additionalinfo='"+element.attr("id")+"']").addClass('d-none');
            }
        }

        toggleChanged(element);

        element.not('.change-event-enabled').on('change', function () {            
            toggleChanged(element);
        });

        element.addClass('change-event-enabled');
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
        showModal: function () {
            showModal();
        }
    };
}();
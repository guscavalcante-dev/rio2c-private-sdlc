// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 08-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-21-2020
// ***********************************************************************
// <copyright file="collaborators.update.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var CollaboratorsUpdate = function () {

    var modalId = '#UpdatePlayerExecutiveModal';
    var formId = '#UpdatePlayerExecutiveForm';

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
        MyRio2cCommon.enableDatePicker({ inputIdOrClass: formId + ' .enable-datepicker' });        
        AttendeeOrganizationsForm.init(formId);
        AddressesForm.init();
        //MyRio2cCommon.enableCkEditor({ idOrClass: '.ckeditor-rio2c-jobtitle', maxCharCount: 81 });
        //MyRio2cCommon.enableCkEditor({ idOrClass: '.ckeditor-rio2c-minibio', maxCharCount: 710 });

        MyRio2cCommon.enableDropdownChangeEvent("CollaboratorGenderUid", "CollaboratorGenderAdditionalInfo");
        MyRio2cCommon.enableDropdownChangeEvent("CollaboratorRoleUid", "CollaboratorRoleAdditionalInfo");
        MyRio2cCommon.enableDropdownChangeEvent("CollaboratorIndustryUid", "CollaboratorIndustryAdditionalInfo");
        MyRio2cCommon.enableYesNoRadioEvent("HasAnySpecialNeeds");
        MyRio2cCommon.enableYesNoRadioEvent("HaveYouBeenToRio2CBefore");
        changePreviousEditionsRequired();
        enableAjaxForm();
        enableFormValidation();
    };
    
    var changePreviousEditionsRequired = function () {
	    $("#HasEditionSelected").val($('[data-additionalinfo="HaveYouBeenToRio2CBefore"] :checkbox:checked').length > 0 ? "True" : null);
	    var dataValMsgFor = $('[data-valmsg-for="HasEditionSelected"]');

	    $('[data-additionalinfo="HaveYouBeenToRio2CBefore"] :checkbox').on('click', function () {
		    if ($('[data-additionalinfo="HaveYouBeenToRio2CBefore"] :checkbox:checked').length > 0) {
			    $("#HasEditionSelected").val("True");
			    dataValMsgFor.html('');
			    dataValMsgFor.addClass('field-validation-valid');
			    dataValMsgFor.removeClass('field-validation-error');
		    } else {
			    $("#HasEditionSelected").val("False");
			    dataValMsgFor.html('<span for="' + name + '" generated="true" class="">' + labels.selectAtLeastOneOption + '</span>');
			    dataValMsgFor.removeClass('field-validation-valid');
			    dataValMsgFor.addClass('field-validation-error');
		    }
	    });
    }

    // Show modal ---------------------------------------------------------------------------------
    var showModal = function (collaboratorUid, isAddingToCurrentEdition) {
        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();
        jsonParameters.collaboratorUid = collaboratorUid;
        jsonParameters.isAddingToCurrentEdition = isAddingToCurrentEdition;

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/PlayersExecutives/ShowUpdateModal'), jsonParameters, function (data) {
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

                $(formId).find(":input.input-validation-error:first").focus();
            }
        });
    };

    return {
        showModal: function (collaboratorUid, isAddingToCurrentEdition) {
            showModal(collaboratorUid, isAddingToCurrentEdition);
        }
    };
}();
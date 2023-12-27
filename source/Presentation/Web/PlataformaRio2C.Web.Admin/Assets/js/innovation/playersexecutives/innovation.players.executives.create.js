// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Elton Assunção
// Created          : 12-22-2023
//
// Last Modified By : Elton Assunção
// Last Modified On : 12-22-2023
// ***********************************************************************
// <copyright file="innovation.players.executives.create.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var InnovationPlayersExecutivesCreate = function () {

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
        MyRio2cCommon.enableDropdownChangeEvent("CollaboratorGenderUid", "CollaboratorGenderAdditionalInfo");
        MyRio2cCommon.enableDropdownChangeEvent("CollaboratorRoleUid", "CollaboratorRoleAdditionalInfo");
        MyRio2cCommon.enableDropdownChangeEvent("CollaboratorIndustryUid", "CollaboratorIndustryAdditionalInfo");
        MyRio2cCommon.enableYesNoRadioEvent("HasAnySpecialNeeds");
        MyRio2cCommon.enableYesNoRadioEvent("HaveYouBeenToRio2CBefore");
        MyRio2cCommon.enableDatePicker({ inputIdOrClass: formId + ' .enable-datepicker' });
        changePreviousEditionsRequired();
        MyRio2cCommon.enableAtLeastOnCheckboxByNameValidation("HaveYouBeenToRio2CBefore");
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
    var showModal = function (attendeeOrganizationUid) {
        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();
        jsonParameters.attendeeOrganizationUid = attendeeOrganizationUid;

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Innovation/PlayersExecutives/ShowCreateModal'), jsonParameters, function (data) {
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

                if (typeof (InnovationPlayersExecutivesDataTableWidget) !== 'undefined') {
                    InnovationPlayersExecutivesDataTableWidget.refreshData();
                }

                if (typeof (InnovationPlayersExecutivesTotalCountWidget) !== 'undefined') {
                   InnovationPlayersExecutivesTotalCountWidget.init();
                }

                if (typeof (InnovationPlayersExecutivesEditionCountWidget) !== 'undefined') {
                    InnovationPlayersExecutivesEditionCountWidget.init();
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
        showModal: function (attendeeOrganizationUid) {
            showModal(attendeeOrganizationUid);
        }
    };
}();
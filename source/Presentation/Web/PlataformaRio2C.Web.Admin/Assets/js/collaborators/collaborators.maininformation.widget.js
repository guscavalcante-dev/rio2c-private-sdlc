// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 08-14-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-14-2021
// ***********************************************************************
// <copyright file="collaborators.maininformation.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var CollaboratorsMainInformationWidget = function () {

    var widgetElementId = '#MainInformationWidget';
    var widgetElement = $(widgetElementId);

    var updateModalId = '#UpdateMainInformationModal';
    var updateFormId = '#UpdateMainInformationForm';
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

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Collaborators/ShowMainInformationWidget'), jsonParameters, function (data) {
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

                if (typeof (CollaboratorsMainInformationWidget) !== 'undefined') {
                    CollaboratorsMainInformationWidget.init($(collaboratorTypeNameId).val());
                }

                if (typeof (SpeakersApiConfigurationWidget) !== 'undefined') {
                    SpeakersApiConfigurationWidget.init();
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
        if (typeof (MyRio2cPublicEmail) !== 'undefined') {
            MyRio2cPublicEmail.init();
        }

        //MyRio2cCommon.enableCkEditor({ idOrClass: '.ckeditor-rio2c-minibio', maxCharCount: 710 });
        MyRio2cCropper.init({ formIdOrClass: updateFormId });
        MyRio2cCommon.enableDatePicker({ inputIdOrClass: updateFormId + ' .enable-datepicker' });
        MyRio2cCommon.enableSelect2({ inputIdOrClass: updateFormId + ' .enable-select2' });
        MyRio2cCommon.enableDropdownChangeEvent("CollaboratorGenderUid", "CollaboratorGenderAdditionalInfo");
        MyRio2cCommon.enableDropdownChangeEvent("CollaboratorRoleUid", "CollaboratorRoleAdditionalInfo");
        MyRio2cCommon.enableDropdownChangeEvent("CollaboratorIndustryUid", "CollaboratorIndustryAdditionalInfo");
        MyRio2cCommon.enableYesNoRadioEvent("HasAnySpecialNeeds");
        MyRio2cCommon.enableYesNoRadioEvent("HaveYouBeenToRio2CBefore");
        changePreviousEditionsRequired();
        enableAjaxForm();
        MyRio2cCommon.enableFormValidation({ formIdOrClass: updateFormId, enableHiddenInputsValidation: true, enableMaxlength: true });
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

    var showUpdateModal = function (collaboratorType) {
        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();
        jsonParameters.collaboratorUid = $('#AggregateId').val();
        jsonParameters.isAddingToCurrentEdition = true;
        jsonParameters.collaboratorType = collaboratorType;

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Collaborators/ShowUpdateMainInformationModal'), jsonParameters, function (data) {
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
// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 12-16-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-16-2019
// ***********************************************************************
// <copyright file="speakers.maininformation.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var SpeakersMainInformationWidget = function () {

    var widgetElementId = '#SpeakerMainInformationWidget';
    var widgetElement = $(widgetElementId);

    var updateModalId = '#UpdateMainInformationModal';
    var updateFormId = '#UpdateMainInformationForm';

    // Show ---------------------------------------------------------------------------------------
    var enableShowPlugins = function () {
        KTApp.initTooltips();
    };

    var show = function () {
        if (widgetElement.length <= 0) {
            return;
        }

        var jsonParameters = new Object();
        jsonParameters.collaboratorUid = $('#AggregateId').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Speakers/ShowMainInformationWidget'), jsonParameters, function (data) {
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

                if (typeof (SpeakersMainInformationWidget) !== 'undefined') {
                    SpeakersMainInformationWidget.init();
                }
            },
            onError: function (data) {
                if (MyRio2cCommon.hasProperty(data, 'pages')) {
                    enableUpdatePlugins();
                }
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

    var showUpdateModal = function () {
        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();
        jsonParameters.collaboratorUid = $('#AggregateId').val();
        jsonParameters.isAddingToCurrentEdition = true;

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Speakers/ShowUpdateMainInformationModal'), jsonParameters, function (data) {
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
        init: function () {
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            show();
        },
        showUpdateModal: function () {
            showUpdateModal();
        }
    };
}();
// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Arthur Souza
// Created          : 01-27-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-20-2020
// ***********************************************************************
// <copyright file="collaborators.create.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var LogisticsCreate = function () {

    var modalId = '#CreateLogisticRequestModal';
    var formId = '#CreateLogisticRequestForm';

    // Enable form validation ---------------------------------------------------------------------
    var enableFormValidation = function () {
        MyRio2cCommon.enableFormValidation({ formIdOrClass: formId, enableHiddenInputsValidation: true, enableMaxlength: true });
    };

    // Create -------------------------------------------------------------------------------------
    var enableAjaxForm = function () {
	    MyRio2cCommon.enableAjaxForm({
		    idOrClass: formId,
		    onSuccess: function (data) {
			    $(modalId).modal('hide');

			    if (typeof (LogisticsDataTableWidget) !== 'undefined') {
				    LogisticsDataTableWidget.refreshData();
			    }
		    },
		    onError: function (data) {
			    if (MyRio2cCommon.hasProperty(data, 'pages')) {
				    enablePlugins();
			    }
		    }
	    });
    };

    var enablePlugins = function () {        
        MyRio2cCommon.enableCollaboratorSelect2({ url: '/Collaborators/FindAllByFilters' });
        MyRio2cCommon.enableSelect2({ inputIdOrClass: formId + ' .enable-select2', allowClear: true });
        enableAjaxForm();

        MyRio2cCommon.enableCheckboxChangeEvent("IsAirfareSponsored", function () { toggleMainSponsorCheck('Airfare'); toggleOtherSponsorVisibility('Airfare'); });
        MyRio2cCommon.enableCheckboxChangeEvent("IsAccommodationSponsored", function () { toggleMainSponsorCheck('Accommodation'); toggleOtherSponsorVisibility('Accommodation'); });
        MyRio2cCommon.enableCheckboxChangeEvent("IsAirportTransferSponsored", function () { toggleMainSponsorCheck('AirportTransfer'); toggleOtherSponsorVisibility('AirportTransfer'); });

        enableOtherSponsorChangeEvent('Airfare');
        enableOtherSponsorChangeEvent('Accommodation');
        enableOtherSponsorChangeEvent('AirportTransfer');
        
        enableFormValidation();
        userInterfaceLanguage = MyRio2cCommon.getGlobalVariables().userInterfaceLanguageUppercade;
    };
    
    // Show modal ---------------------------------------------------------------------------------
    var showModal = function (attendeeCollaboratorUid) {
        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();
        jsonParameters.attendeeCollaboratorUid = attendeeCollaboratorUid;

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Logistics/ShowCreateModal'), jsonParameters, function (data) {
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

    // Main sponsors ------------------------------------------------------------------------------
    var toggleMainSponsorCheck = function (prefix) {
	    var isSponsored = "Is" + prefix + "Sponsored";

        if (!$('#' + isSponsored).prop('checked')) {
	        $("[name='" + prefix + "SponsorUid']").prop('checked', false);
        }
    };
    
    // Other sponsors -----------------------------------------------------------------------------
    var toggleOtherSponsorVisibility = function (prefix) {
        var isSponsored = "Is" + prefix + "Sponsored";

	    if ($('#' + isSponsored).prop('checked') && $("[data-additionalinfo='" + isSponsored + "']").find(":checked").data('isothers') === 'True') {
		    $("[data-id='" + prefix + "']").removeClass('d-none');
		    $('#' + prefix + 'Required').val('True');
	    }
	    else {
		    $("[data-id='" + prefix + "']").addClass('d-none');
		    $('#' + prefix + 'Required').val('False');
		    $('#' + prefix + 'SponsorOtherUid').val(null).trigger('change');
		    disableNewSponsor(prefix);
	    }
    };

    var enableOtherSponsorChangeEvent = function (prefix) {
	    toggleOtherSponsorVisibility(prefix);

	    var selector = $('[name="' + prefix + 'SponsorUid"]:input');
	    selector.not('.change-event-enabled').on('change', function () {
		    toggleOtherSponsorVisibility(prefix);
	    });
	    selector.not('.change-event-enabled').addClass('change-event-enabled');
    }

    var enableNewSponsor = function (prefix) {
	    MyRio2cCommon.enableFieldEdit({ dataId: prefix });
	    $('#' + prefix + 'SponsorOtherUid').val(null).trigger('change');

	    return false;
    };

    var disableNewSponsor = function (prefix) {
	    MyRio2cCommon.disableFieldEdit({ dataId: prefix });
	    $('#' + prefix + 'SponsorOtherName').val('');

	    return false;
    };

    return {
        showModal: function (attendeeCollaboratorUid) {
            showModal(attendeeCollaboratorUid);
        },
        enableNewSponsor: function(preffix) {
            return enableNewSponsor(preffix);
        },
        disableNewSponsor: function (preffix) {
            return disableNewSponsor(preffix);
        }
    };
}();
// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Arthur Souza
// Created          : 01-27-2020
//
// Last Modified By : Arthur Souza
// Last Modified On : 01-27-2020
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

    // Enable plugins -----------------------------------------------------------------------------
    var enablePlugins = function () {        
        MyRio2cCommon.enableCollaboratorSelect2({ url: '/Collaborators/FindAllByFilters' });
        MyRio2cCommon.enableSelect2({ inputIdOrClass: formId + ' .enable-select2', allowClear: true });
        enableAjaxForm();
        MyRio2cCommon.enableCheckboxChangeEvent("IsAccommodationSponsored");
        MyRio2cCommon.enableCheckboxChangeEvent("IsAirfareSponsored");
        MyRio2cCommon.enableCheckboxChangeEvent("IsAirportTransferSponsored");
        enableFormValidation();
        initElements();
        enableSponsorSelect2();
    };

    // Show modal ---------------------------------------------------------------------------------
    var showModal = function () {
        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();

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
    
    // Toggle other sponsors ------------------------------------------------------------------------------

    var enableOtherSponsorsSelect2 = function (elementId, requiredFieldId) {
	    var element = $('#' + elementId);

	    function toggleChanged(element) {
		    var hasAdditionalInfo = element.find(':selected').data('additionalinfo');

		    if (hasAdditionalInfo === "True") {
			    $("[data-additionalinfo='" + element.attr("id") + "']").removeClass('d-none');
			    $('#' + requiredFieldId + 'Required').val("True");
		    }
		    else {
			    $("[data-additionalinfo='" + element.attr("id") + "']").addClass('d-none');
			    $('#' + requiredFieldId + 'Required').val("False");
		    }
	    }

	    toggleChanged(element);
	    element.not('.change-event-enabled').on('change', function () {
		    toggleChanged(element);
	    });

	    element.addClass('change-event-enabled');
    };

    // Airfare sponsor others select2 ------------------------------------------------------------------------------
    var initialAirportSponsorUid = '#AirportSponsor_InitialSponsorUid';
    var airportSponsorUid = '#AirportSponsor_SponsorUid';
    var airportSponsorName = '#AirportSponsor_SponsorName';

    var airportSponsorDataId = 'AirportSponsor';

    var initialAirportSponsorUidElement;
    var airportSponsorUidElement;
    var airportSponsorNameElement;

    var userInterfaceLanguage = 'en';

    var initElements = function () {
        initialAirportSponsorUidElement = $(initialAirportSponsorUid);
        airportSponsorUidElement = $(airportSponsorUid);
        airportSponsorNameElement = $(airportSponsorName);
        
        userInterfaceLanguage = MyRio2cCommon.getGlobalVariables().userInterfaceLanguageUppercade;
    };
    
    var enableNewSponsor = function () {
        MyRio2cCommon.enableFieldEdit({ dataId: airportSponsorDataId });

        if (!MyRio2cCommon.isNullOrEmpty(airportSponsorUidElement.val())) {
	        airportSponsorUidElement.val('').trigger('change');
        }
        else {
	        airportSponsorUidElement.val('');
        }

        return false;
    };

    var disableNewSponsor = function () {
        MyRio2cCommon.disableFieldEdit({ dataId: airportSponsorDataId });

        if (!MyRio2cCommon.isNullOrEmpty(airportSponsorNameElement.val())) {
	        airportSponsorUidElement.val('').trigger('change');
        }
        else {
	        airportSponsorUidElement.val('');
        }

        return false;
    };

    var toggleSponsor = function (forceDisable) {
        if (MyRio2cCommon.isNullOrEmpty(forceDisable)) {
            forceDisable = false;
        }

        if (!forceDisable)// && (/*!MyRio2cCommon.isNullOrEmpty(initialStateUidElement.val()) ||*/ !MyRio2cCommon.isNullOrEmpty(stateUidElement.val()) || !MyRio2cCommon.isNullOrEmpty(stateNameElement.val())))
        {
            $('[data-id="' + airportSponsorDataId + '"] .btn-edit').removeClass('disabled');
            airportSponsorUidElement.prop("disabled", false);
        }
        else {
            $('[data-id="' + airportSponsorDataId + '"] .btn-edit').addClass('disabled');
            airportSponsorUidElement.prop("disabled", true);
            airportSponsorNameElement.val('');
        }

        if (!forceDisable && !MyRio2cCommon.isNullOrEmpty(airportSponsorNameElement.val())) {
            enableNewSponsor();
        }
        else {
            disableNewSponsor();
        }
    };

    var emptySponsorSelect2 = function () {
        airportSponsorUidElement.val('').trigger('change');

        airportSponsorUidElement.select2({
            language: userInterfaceLanguage,
            width: '100%',
            placeholder: labels.selectPlaceholder,
            data: []
        });

        airportSponsorUidElement.empty();
    };

    var enableSponsorSelect2 = function (isParentChanged) {
        if (!MyRio2cCommon.isNullOrEmpty(isParentChanged) && isParentChanged === true) {
            if (!MyRio2cCommon.isNullOrEmpty(airportSponsorUidElement.val())) {
                airportSponsorUidElement.val('').trigger('change');
            }
            else {
                airportSponsorUidElement.val('');
            }
        }

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/LogisticSponsors/FindAllByIsOther'), null, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    if (data.list.length <= 0) {
                        emptySponsorSelect2();
                    }

                    var selectData = new Array();

                    // Placeholder
                    var selectOption = new Object();
                    selectOption.id = '';
                    selectOption.text = labels.selectPlaceholder;
                    selectData.push(selectOption);

                    for (var i in data.list) {
                        if (data.list.hasOwnProperty(i)) {
		                    selectOption = new Object();
                            selectOption.id = data.list[i].Uid;
                            selectOption.text = data.list[i].Name;
		                    selectData.push(selectOption);
	                    }
                    }

                    airportSponsorUidElement.empty().select2({
                        language: userInterfaceLanguage,
                        width: '100%',
                        placeholder: labels.selectPlaceholder,
                        triggerChange: true,
                        allowClear: true,
                        data: selectData
                    });

                    var initialNameValue = airportSponsorNameElement.val();
                    var initialUidValue = initialAirportSponsorUidElement.val();
                    if (!MyRio2cCommon.isNullOrEmpty(initialNameValue)) {
                        enableNewSponsor();
                    }
                    else if (!MyRio2cCommon.isNullOrEmpty(initialUidValue)) {
                        airportSponsorUidElement.val(initialUidValue).trigger('change');
                        initialAirportSponsorUidElement.val('');
                    }

                    toggleSponsor();
                },
                // Error
                onError: function () {
                    emptySponsorSelect2();
                }
            });
        })
            .fail(function () {
                emptySponsorSelect2();
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

                if (typeof (LogisticSponsorsDataTableWidget) !== 'undefined') {
                    LogisticSponsorsDataTableWidget.refreshData();
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
        },
        enableNewSponsor: function() {
	        return enableNewSponsor();
        },
        disableNewSponsor: function() {
	        return disableNewSponsor();
        }
    };
}();
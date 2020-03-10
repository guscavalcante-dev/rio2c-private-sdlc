// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Arthur Souza
// Created          : 01-27-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-10-2020
// ***********************************************************************
// <copyright file="collaborators.create.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var LogisticsCreate = function () {

    var modalId = '#CreateLogisticRequestModal';
    var formId = '#CreateLogisticRequestForm';
    var userInterfaceLanguage = 'en';

    // Enable form validation ---------------------------------------------------------------------
    var enableFormValidation = function () {
        MyRio2cCommon.enableFormValidation({ formIdOrClass: formId, enableHiddenInputsValidation: true, enableMaxlength: true });
    };

    // Enable plugins -----------------------------------------------------------------------------
    var enablePlugins = function () {        
        MyRio2cCommon.enableCollaboratorSelect2({ url: '/Collaborators/FindAllByFilters' });
        MyRio2cCommon.enableSelect2({ inputIdOrClass: formId + ' .enable-select2', allowClear: true });
        enableAjaxForm();

        enableOtherSponsorsSelect2('Airfare');
        enableOtherSponsorsSelect2('Accommodation');
        enableOtherSponsorsSelect2('AirportTransfer');

        MyRio2cCommon.enableCheckboxChangeEvent("IsAirfareSponsored", function () { clearSponsor("Airfare"); });
        MyRio2cCommon.enableCheckboxChangeEvent("IsAccommodationSponsored", function () { clearSponsor("Accommodation"); });
        MyRio2cCommon.enableCheckboxChangeEvent("IsAirportTransferSponsored", function () { clearSponsor("AirportTransfer"); });
        
        enableFormValidation();
        userInterfaceLanguage = MyRio2cCommon.getGlobalVariables().userInterfaceLanguageUppercade;
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
    
    // Sponsor others select2 ------------------------------------------------------------------------------
    var initialSponsorUid = 'InitialSponsorUid';
    var sponsorUid = 'SponsorOtherUid';
    var sponsorName = 'SponsorOtherName';

    var clearSponsor = function (preffix) {
	    $("[name='" + preffix + "SponsorUid']").prop("checked", false);
	    emptySponsorSelect2(preffix);
	    disableNewSponsor(preffix);
	    hideSponsorSelect2(preffix);
    }

    var hideSponsorSelect2 = function (preffix) {
	    $("[data-id='" + preffix + "']").addClass('d-none');
	    $('#' + preffix + 'Required').val("");
    }

    var enableShowHideOtherSponsorsSelect2 = function (preffix) {
        var attribute = "Is" + preffix + "Sponsored";

        function toggleChanged(value) {
            if (value === "True") {
                $("[data-id='" + preffix + "']").removeClass('d-none');
                $('#' + preffix + 'Required').val("True");
		    }
		    else {
	            hideSponsorSelect2(preffix);
                //emptySponsorSelect2(preffix);
            }
	    }
        
        toggleChanged($("[data-additionalinfo='" + attribute + "']").find(":checked").data('isothers'));

        var selector = $("[data-additionalinfo='" + attribute + "'] input");
        selector.not('.change-event-enabled').change(function () {
            toggleChanged($(this).data('isothers'));
        });
        selector.addClass('change-event-enabled');
    };
    
    var enableNewSponsor = function (preffix) {
	    var sponsorUidElement = $("#" + preffix + sponsorUid);
        
        MyRio2cCommon.enableFieldEdit({ dataId: preffix });

        if (!MyRio2cCommon.isNullOrEmpty(sponsorUidElement.val())) {
	        sponsorUidElement.val('').trigger('change');
        }
        else {
	        sponsorUidElement.val('');
        }

        return false;
    };

    var disableNewSponsor = function (preffix) {
	    var sponsorUidElement = $("#" + preffix + sponsorUid);
        var sponsorNameElement = $("#" + preffix + sponsorName);

        MyRio2cCommon.disableFieldEdit({ dataId: preffix });

        if (!MyRio2cCommon.isNullOrEmpty(sponsorNameElement.val())) {
	        sponsorUidElement.val('').trigger('change');
        }
        else {
	        sponsorUidElement.val('');
        }

        return false;
    };

    var toggleSponsor = function (preffix, forceDisable) {
        if (MyRio2cCommon.isNullOrEmpty(forceDisable)) {
            forceDisable = false;
        }

        var sponsorUidElement = $("#" + preffix + sponsorUid);
        var sponsorNameElement = $("#" + preffix + sponsorName);

        if (!forceDisable)// && (/*!MyRio2cCommon.isNullOrEmpty(initialStateUidElement.val()) ||*/ !MyRio2cCommon.isNullOrEmpty(stateUidElement.val()) || !MyRio2cCommon.isNullOrEmpty(stateNameElement.val())))
        {
            $('[data-id="' + preffix + '"] .btn-edit').removeClass('disabled');
            sponsorUidElement.prop("disabled", false);
        }
        else {
            $('[data-id="' + preffix + '"] .btn-edit').addClass('disabled');
            sponsorUidElement.prop("disabled", true);
            sponsorNameElement.val('');
        }

        if (!forceDisable && !MyRio2cCommon.isNullOrEmpty(sponsorNameElement.val())) {
            enableNewSponsor(preffix);
        }
        else {
            disableNewSponsor(preffix);
        }
    };

    var emptySponsorSelect2 = function (preffix) {
	    var sponsorUidElement = $("#" + preffix + sponsorUid);
	    sponsorUidElement.val('').trigger('change');
    };

    var enableOtherSponsorsSelect2 = function (preffix, isParentChanged) {
        enableShowHideOtherSponsorsSelect2(preffix);

	    var initialSponsorUidElement = $("#" + preffix + initialSponsorUid);
	    var sponsorUidElement = $("#" + preffix + sponsorUid);
        var sponsorNameElement = $("#" + preffix + sponsorName);

        if (!MyRio2cCommon.isNullOrEmpty(isParentChanged) && isParentChanged === true) {
            if (!MyRio2cCommon.isNullOrEmpty(sponsorUidElement.val())) {
	            sponsorUidElement.val('').trigger('change');
            }
            else {
	            sponsorUidElement.val('');
            }
        }

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/LogisticSponsors/FindAllByIsOther'), null, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    if (data.list.length <= 0) {
                        emptySponsorSelect2(preffix);
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

                    sponsorUidElement.empty().select2({
                        language: userInterfaceLanguage,
                        width: '100%',
                        placeholder: labels.selectPlaceholder,
                        triggerChange: true,
                        allowClear: true,
                        data: selectData
                    });

                    var initialNameValue = sponsorNameElement.val();
                    var initialUidValue = initialSponsorUidElement.val();
                    if (!MyRio2cCommon.isNullOrEmpty(initialNameValue)) {
                        enableNewSponsor();
                    }
                    else if (!MyRio2cCommon.isNullOrEmpty(initialUidValue)) {
	                    sponsorUidElement.val(initialUidValue).trigger('change');
                        initialSponsorUidElement.val('');
                    }

                    toggleSponsor();
                },
                // Error
                onError: function () {
                    emptySponsorSelect2(preffix);
                }
            });
        })
            .fail(function () {
                emptySponsorSelect2(preffix);
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

    return {
        showModal: function () {
	        showModal();
        },
        enableNewSponsor: function(preffix) {
            return enableNewSponsor(preffix);
        },
        disableNewSponsor: function (preffix) {
            return disableNewSponsor(preffix);
        }
    };
}();
// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 01-02-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-13-2020
// ***********************************************************************
// <copyright file="logistics.maininformation.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var LogisticsMainInformationWidget = function () {
    var widgetElementId = '#LogisticsMainInformationWidget';
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
        jsonParameters.logisticsUid = $('#AggregateId').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Logistics/ShowMainInformationWidget'), jsonParameters, function (data) {
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

                if (typeof (LogisticsMainInformationWidget) !== 'undefined') {
	                LogisticsMainInformationWidget.init();
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
        MyRio2cCommon.enableSelect2({ inputIdOrClass: updateFormId + ' .enable-select2', allowClear: true });

        MyRio2cCommon.enableCheckboxChangeEvent("IsAirfareSponsored", function () { toggleMainSponsorCheck('Airfare'); toggleOtherSponsorVisibility('Airfare'); });
        MyRio2cCommon.enableCheckboxChangeEvent("IsAccommodationSponsored", function () { toggleMainSponsorCheck('Accommodation'); toggleOtherSponsorVisibility('Accommodation'); });
        MyRio2cCommon.enableCheckboxChangeEvent("IsAirportTransferSponsored", function () { toggleMainSponsorCheck('AirportTransfer'); toggleOtherSponsorVisibility('AirportTransfer'); });

        enableOtherSponsorChangeEvent('Airfare');
        enableOtherSponsorChangeEvent('Accommodation');
        enableOtherSponsorChangeEvent('AirportTransfer');

        userInterfaceLanguage = MyRio2cCommon.getGlobalVariables().userInterfaceLanguageUppercade;
        enableAjaxForm();
        MyRio2cCommon.enableFormValidation({ formIdOrClass: updateFormId, enableHiddenInputsValidation: true, enableMaxlength: true });
    };

    var showUpdateModal = function () {
        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();
        jsonParameters.logisticsUid = $('#AggregateId').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Logistics/ShowUpdateMainInformationModal'), jsonParameters, function (data) {
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
        init: function () {
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            show();
        },
        showUpdateModal: function () {
            showUpdateModal();
        },
        enableNewSponsor: function (prefix) {
	        return enableNewSponsor(prefix);
        },
        disableNewSponsor: function (prefix) {
	        return disableNewSponsor(prefix);
        }
    };
}();
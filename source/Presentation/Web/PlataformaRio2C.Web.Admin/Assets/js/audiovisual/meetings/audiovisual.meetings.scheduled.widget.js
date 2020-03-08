// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 03-07-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-08-2020
// ***********************************************************************
// <copyright file="audiovisual.meetings.scheduled.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var AudiovisualMeetingsScheduledWidget = function () {

    var widgetElementId = '#AudiovisualMeetingsScheduledWidget';
    var widgetElement = $(widgetElementId);

    //var updateModalId = '#UpdateMainInformationModal';
    //var updateFormId = '#UpdateMainInformationForm';

    // Search form  -------------------------------------------------------------------------------
    var enableSearchForm = function () {
	    MyRio2cCommon.enableOrganizationSelect2({ inputIdOrClass: '#BuyerOrganizationUid', url: '/Players/FindAllByFilters', filterByProjectsInNegotiation: true, placeholder: translations.playerDropdownPlaceholder + '...' });
	    MyRio2cCommon.enableOrganizationSelect2({ inputIdOrClass: '#SellerOrganizationUid', url: '/Audiovisual/Producers/FindAllByFilters', filterByProjectsInNegotiation: true, placeholder: translations.producerDropdownPlaceholder + '...' });
    }

    // Show ---------------------------------------------------------------------------------------
    var enableShowPlugins = function () {
        KTApp.initTooltips();
        MyRio2cCommon.initScroll();
    };

    var show = function () {
        if (widgetElement.length <= 0) {
            return;
        }

        var jsonParameters = new Object();
        jsonParameters.buyerOrganizationUid = $('#BuyerOrganizationUid').val();
        jsonParameters.sellerOrganizationUid = $('#SellerOrganizationUid').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/Meetings/ShowScheduledDataWidget'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    enableShowPlugins();
                },
                // Error
                onError: function() {
                }
            });
        })
        .fail(function () {
            //showAlert();
            //MyRio2cCommon.unblock(widgetElementId);
        })
        .always(function() {
            MyRio2cCommon.unblock({ idOrClass: widgetElementId });
        });
    };

    return {
        init: function () {
	        enableSearchForm();
	        AudiovisualMeetingsScheduledWidget.search();
        },
        search: function() {
	        MyRio2cCommon.block({ idOrClass: widgetElementId });
	        show();
        }
    };
}();
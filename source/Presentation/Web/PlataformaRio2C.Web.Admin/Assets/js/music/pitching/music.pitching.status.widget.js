// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 03-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-20-2020
// ***********************************************************************
// <copyright file="audiovisual.meetings.status.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var MusicPitchingStatusWidget = function () {

    var widgetElementId = '#MusicPitchingStatusWidget';
    var widgetElement = $(widgetElementId);

    //var updateModalId = '#UpdateMainInformationModal';
    //var updateFormId = '#UpdateMainInformationForm';

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

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Music/Pitching/ShowStatusWidget'), jsonParameters, function (data) {
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

    // Generate -------------------------------------------------------------------------------------
    var generate = function () {
        MyRio2cCommon.block();

        var jsonParameters = new Object();

        $.ajaxSetup({ timeout: 3600000 });

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Music/Pitching/DistributeMembers'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
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

            if (typeof (MusicPitchingStatusWidget) !== 'undefined') {
	            MusicPitchingStatusWidget.init();
            }

            if (typeof (MusicPitchingEditionScheduledCountWidget) !== 'undefined') {
	            MusicPitchingEditionScheduledCountWidget.init();
            }

            if (typeof (MusicPitchingEditionUnscheduledCountWidget) !== 'undefined') {
	            MusicPitchingEditionUnscheduledCountWidget.init();
            }
        });
    };

    var showModal = function () {
        var message = translations.distributeCommissionMembers;

        bootbox.dialog({
            message: message,
            buttons: {
                cancel: {
                    label: labels.cancel,
                    className: "btn btn-secondary mr-auto",
                    callback: function () {
                    }
                },
                confirm: {
                    label: translations.distributeCommissionMembers,
                    className: "btn btn-brand",
                    callback: function () {
                        generate();
                    }
                }
            }
        });
    };

    return {
        init: function () {
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            show();
        },
        showModal: function () {
	        showModal();
        }
    };
}();
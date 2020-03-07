// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 03-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-07-2020
// ***********************************************************************
// <copyright file="audiovisual.meetings.status.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var AudiovisualMeetingsStatusWidget = function () {

    var widgetElementId = '#AudiovisualMeetingsStatusWidget';
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

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/Meetings/ShowStatusWidget'), jsonParameters, function (data) {
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

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/Meetings/Generate'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    if (typeof (AudiovisualMeetingsStatusWidget) !== 'undefined') {
	                    AudiovisualMeetingsStatusWidget.init();
                    }

                    if (typeof (AudiovisualMeetingsEditionScheduledCountWidget) !== 'undefined') {
	                    AudiovisualMeetingsEditionScheduledCountWidget.init();
                    }

                    if (typeof (AudiovisualMeetingsEditionNotScheduledCountWidget) !== 'undefined') {
	                    AudiovisualMeetingsEditionNotScheduledCountWidget.init();
                    }

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

    var showModal = function () {
        var message = translations.generateCalendarMessage;

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
                    label: translations.generateCalendar,
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
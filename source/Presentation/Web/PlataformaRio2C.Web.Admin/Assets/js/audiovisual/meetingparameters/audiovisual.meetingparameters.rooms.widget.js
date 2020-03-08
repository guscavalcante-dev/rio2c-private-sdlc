// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 03-05-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-08-2020
// ***********************************************************************
// <copyright file="audiovisual.meetingparameters.rooms.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var AudiovisualMeetingParametersRoomsWidget = function () {

    var widgetElementId = '#AudiovisualMeetingParametersRoomsWidget';
    var widgetElement = $(widgetElementId);

    var createModalId = '#CreateRoomModal';
    var createFormId = '#CreateRoomForm';

    var updateModalId = '#UpdateRoomModal';
    var updateFormId = '#UpdateRoomForm';

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
        jsonParameters.negotiationConfigUid = $('#AggregateId').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/MeetingParameters/ShowRoomsWidget'), jsonParameters, function (data) {
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

    // Create -------------------------------------------------------------------------------------
    var enableCreateAjaxForm = function () {
	    MyRio2cCommon.enableAjaxForm({
            idOrClass: createFormId,
		    onSuccess: function (data) {
                $(createModalId).modal('hide');

			    if (typeof (AudiovisualMeetingParametersRoomsWidget) !== 'undefined') {
				    AudiovisualMeetingParametersRoomsWidget.init();
			    }
		    },
		    onError: function (data) {
			    if (MyRio2cCommon.hasProperty(data, 'pages')) {
                    enableCreatePlugins();
			    }

                //$(createFormId).find(":input.input-validation-error:first").focus(); //Removed because was erasing the server error on RoomUid (select2)
		    }
	    });
    };

    var enableCreatePlugins = function () {
        MyRio2cCommon.enableSelect2({ inputIdOrClass: createFormId + ' .enable-select2' });
	    enableCreateAjaxForm();
        MyRio2cCommon.enableFormValidation({ formIdOrClass: createFormId, enableHiddenInputsValidation: true, enableMaxlength: true });
    };

    var showCreateModal = function () {
	    MyRio2cCommon.block({ isModal: true });

	    var jsonParameters = new Object();
	    jsonParameters.negotiationConfigUid = $('#AggregateId').val();

	    $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/MeetingParameters/ShowCreateRoomModal'), jsonParameters, function (data) {
			    MyRio2cCommon.handleAjaxReturn({
				    data: data,
				    // Success
				    onSuccess: function () {
					    enableCreatePlugins();
                        $(createModalId).modal();
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

    // Update -------------------------------------------------------------------------------------
    var enableUpdateAjaxForm = function () {
        MyRio2cCommon.enableAjaxForm({
            idOrClass: updateFormId,
            onSuccess: function (data) {
                $(updateModalId).modal('hide');

                if (typeof (AudiovisualMeetingParametersRoomsWidget) !== 'undefined') {
	                AudiovisualMeetingParametersRoomsWidget.init();
                }
            },
            onError: function (data) {
                if (MyRio2cCommon.hasProperty(data, 'pages')) {
                    enableUpdatePlugins();
                }

                //$(updateFormId).find(":input.input-validation-error:first").focus();  //Removed because was erasing the server error on RoomUid (select2)
            }
        });
    };

    var enableUpdatePlugins = function () {
        MyRio2cCommon.enableSelect2({ inputIdOrClass: updateFormId + ' .enable-select2' });
	    enableUpdateAjaxForm();
        MyRio2cCommon.enableFormValidation({ formIdOrClass: updateFormId, enableHiddenInputsValidation: true, enableMaxlength: true });
    };

    var showUpdateModal = function (negotiationRoomConfigUid) {
        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();
        jsonParameters.negotiationConfigUid = $('#AggregateId').val();
        jsonParameters.negotiationRoomConfigUid = negotiationRoomConfigUid;

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/MeetingParameters/ShowUpdateRoomModal'), jsonParameters, function (data) {
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

    // Delete -------------------------------------------------------------------------------------
    var executeDelete = function (negotiationRoomConfigUid) {
        MyRio2cCommon.block();

        var jsonParameters = new Object();
        jsonParameters.negotiationConfigUid = $('#AggregateId').val();
        jsonParameters.negotiationRoomConfigUid = negotiationRoomConfigUid;

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/MeetingParameters/DeleteRoom'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
	                if (typeof (AudiovisualMeetingParametersRoomsWidget) !== 'undefined') {
		                AudiovisualMeetingParametersRoomsWidget.init();
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

    var showDeleteModal = function (negotiationRoomConfigUid) {
        var message = labels.deleteConfirmationMessage;

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
                    label: labels.remove,
                    className: "btn btn-danger",
                    callback: function () {
                        executeDelete(negotiationRoomConfigUid);
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
        showCreateModal: function () {
            showCreateModal();
        },
        showUpdateModal: function (negotiationRoomConfigUid) {
            showUpdateModal(negotiationRoomConfigUid);
        },
        showDeleteModal: function (negotiationRoomConfigUid) {
            showDeleteModal(negotiationRoomConfigUid);
        }
    };
}();
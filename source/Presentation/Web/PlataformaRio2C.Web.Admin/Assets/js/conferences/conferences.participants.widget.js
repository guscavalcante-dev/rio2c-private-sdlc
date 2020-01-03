// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 01-02-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-03-2020
// ***********************************************************************
// <copyright file="conferences.participants.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var ConferencesParticipantsWidget = function () {

    var widgetElementId = '#ConferenceParticipantsWidget';
    var widgetElement = $(widgetElementId);

    var createModalId = '#CreateParticipantModal';
    var createFormId = '#CreateParticipantForm';

    var globalVariables = MyRio2cCommon.getGlobalVariables();
    var imageDirectory = 'https://' + globalVariables.bucket + '/img/users/';

    // Show ---------------------------------------------------------------------------------------
    var enableShowPlugins = function () {
        KTApp.initTooltips();
        //enableSpeakerSelect2();
    };

    var show = function () {
        if (widgetElement.length <= 0) {
            return;
        }

        var jsonParameters = new Object();
        jsonParameters.conferenceUid = $('#AggregateId').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Conferences/ShowParticipantsWidget'), jsonParameters, function (data) {
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

    // Create -------------------------------------------------------------------------------------
    var enableAjaxForm = function () {
        MyRio2cCommon.enableAjaxForm({
            idOrClass: createFormId,
            onSuccess: function (data) {
                $(createModalId).modal('hide');

                if (typeof (ConferencesParticipantsWidget) !== 'undefined') {
                    ConferencesParticipantsWidget.init();
                }
            },
            onError: function (data) {
                if (MyRio2cCommon.hasProperty(data, 'pages')) {
                    enableCreatePlugins();
                }
            }
        });
    };

    // Speaker Select2
    var formatSpeakerResult = function (speaker) {
        if (speaker.loading) {
            return speaker.text;
        }

        var container =
            '<div class="select2-result-collaborator clearfix">' +
                '<div class="select2-result-collaborator__avatar">';

        // Picture
        if (!MyRio2cCommon.isNullOrEmpty(speaker.Picture)) {
            container +=
                '<img src="' + speaker.Picture + '" />';
        }
        else {
            container +=
                '<img src="' + imageDirectory + 'no-image.png?v=20190818200849" />';
        }

        container +=
            '</div > ' +
            '<div class="select2-result-collaborator__meta">' +
            '<div class="select2-result-collaborator__title">' + (speaker.BadgeName || speaker.Name) + '</div>';

        if (!MyRio2cCommon.isNullOrEmpty(speaker.JobTitle)) {
            container +=
                '<div class="select2-result-collaborator__description">' + speaker.JobTitle + '</div>';
        }

        if (!MyRio2cCommon.isNullOrEmpty(speaker.Companies) && speaker.Companies.length > 0) {
            container +=
                '<div class="select2-result-collaborator__description">' + speaker.Companies[0].TradeName + '</div>';
        }

        container +=
            '   </div>' +
            '</div>';

        var $container = $(container);

        return $container;
    };

    var formatSpeakerSelection = function (speaker) {
        return speaker.text;
    };

    var enableSpeakerSelect2 = function () {
        $('#CollaboratorUid').select2({
            language: MyRio2cCommon.getGlobalVariable('userInterfaceLanguageUppercade'),
            width: '100%',
            allowClear: true,
            placeholder: labels.selectPlaceholder,
            delay: 250,
            ajax: {
                url: MyRio2cCommon.getUrlWithCultureAndEdition('/Speakers/FindAllByFilters'),
                dataType: 'json',
                type: "GET",
                quietMillis: 50,
                data: function (params) {
                    var query = {
                        keywords: params.term,
                        page: params.page
                    };

                    return query;
                },
                processResults: function (data, params) {
                    params.page = params.page || 1;

                    return MyRio2cCommon.handleAjaxReturn({
                        data: data,
                        // Success
                        onSuccess: function () {
                            for (var i = data.Speakers.length - 1; i >= 0; i--) {
                                data.Speakers[i].id = data.Speakers[i].Uid;
                                data.Speakers[i].text = data.Speakers[i].BadgeName || data.Speakers[i].Name;
                            }

                            return {
                                results: data.Speakers,
                                pagination: {
                                    more: data.HasNextPage
                                }
                            };
                        },
                        // Error
                        onError: function () {
                        }
                    });
                }
            },
            templateResult: formatSpeakerResult,
            templateSelection: formatSpeakerSelection
        });
    };

    var enableCreatePlugins = function () {
        enableAjaxForm();
        enableSpeakerSelect2();
        MyRio2cCommon.enableSelect2({ inputIdOrClass: createFormId + ' .enable-select2', allowClear: true });
        MyRio2cCommon.enableFormValidation({ formIdOrClass: createFormId, enableHiddenInputsValidation: true, enableMaxlength: true });
    };

    var showCreateModal = function () {
        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();
        jsonParameters.conferenceUid = $('#AggregateId').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Conferences/ShowCreateParticipantModal'), jsonParameters, function (data) {
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

    //// Delete -------------------------------------------------------------------------------------
    //var executeDelete = function (organizationUid) {
    //    MyRio2cCommon.block();

    //    var jsonParameters = new Object();
    //    jsonParameters.collaboratorUid = $('#AggregateId').val();
    //    jsonParameters.organizationUid = organizationUid;

    //    $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Speakers/DeleteOrganization'), jsonParameters, function (data) {
    //        MyRio2cCommon.handleAjaxReturn({
    //            data: data,
    //            // Success
    //            onSuccess: function () {
    //                if (typeof (SpeakersCompanyWidget) !== 'undefined') {
    //                    SpeakersCompanyWidget.init();
    //                }
    //            },
    //            // Error
    //            onError: function () {
    //            }
    //        });
    //    })
    //    .fail(function () {
    //    })
    //    .always(function () {
    //        MyRio2cCommon.unblock();
    //    });
    //};

    //var showDeleteModal = function (organizationUid, isDeletingFromCurrentEdition) {
    //    var message = labels.deleteConfirmationMessage;

    //    if (isDeletingFromCurrentEdition) {
    //        message = labels.deleteCurrentEditionConfirmationMessage;
    //    }

    //    bootbox.dialog({
    //        message: message,
    //        buttons: {
    //            cancel: {
    //                label: labels.cancel,
    //                className: "btn btn-secondary mr-auto",
    //                callback: function () {
    //                }
    //            },
    //            confirm: {
    //                label: labels.remove,
    //                className: "btn btn-danger",
    //                callback: function () {
    //                    executeDelete(organizationUid);
    //                }
    //            }
    //        }
    //    });
    //};

    return {
        init: function () {
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            show();
        },
        showCreateModal: function () {
            showCreateModal();
        },
        //showDeleteModal: function (organizationUid) {
        //    showDeleteModal(organizationUid);
        //}
    };
}();
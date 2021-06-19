// ********// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 06-03-2021
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-19-2021
// ***********************************************************************
// <copyright file="audiovisual.organizations.interest.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var AudiovisualOrganizationsInterestWidget = function () {

    var widgetElementId = '#CompanyInterestWidget';
    var widgetElement = $(widgetElementId);

    var updateModalId = '#UpdateInterestModal';
    var updateFormId = '#UpdateInterestForm';

    // Show ---------------------------------------------------------------------------------------
    var enableShowPlugins = function () {
        KTApp.initTooltips();
    };

    var show = function () {
        if (widgetElement.length <= 0) {
            return;
        }

        var jsonParameters = new Object();
        jsonParameters.organizationUid = $('#AggregateId').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/Organizations/ShowInterestWidget'), jsonParameters, function (data) {
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

    // Update -------------------------------------------------------------------------------------
    var enableAjaxForm = function () {
        MyRio2cCommon.enableAjaxForm({
            idOrClass: updateFormId,
            onSuccess: function (data) {
                $(updateModalId).modal('hide');

                if (typeof (AudiovisualOrganizationsInterestWidget) !== 'undefined') {
                    AudiovisualOrganizationsInterestWidget.init();
                }
            },
            onError: function (data) {
                if (MyRio2cCommon.hasProperty(data, 'pages')) {
                    enableUpdatePlugins();
                }

                $(updateFormId).find(":input.input-validation-error:first").focus();
            }
        });
    };

    var enableUpdatePlugins = function () {
        enableAjaxForm();
        //MyRio2cCommon.enableCkEditor({ idOrClass: '.ckeditor-rio2c-restrictions', maxCharCount: 270 });
        MyRio2cCommon.enableAtLeastOnCheckboxByNameValidation(updateFormId);
        MyRio2cCommon.enableFormValidation({ formIdOrClass: updateFormId, enableHiddenInputsValidation: true, enableMaxlength: true });

        // Enable additional info textbox
        if (typeof (MyRio2cCommonAdditionalInfo) !== 'undefined') {
            MyRio2cCommonAdditionalInfo.init();
        }
    };

    var showUpdateModal = function () {
        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();
        jsonParameters.organizationUid = $('#AggregateId').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/Organizations/ShowUpdateInterestModal'), jsonParameters, function (data) {
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

    //// Custom validation --------------------------------------------------------------------------
    //var validateInterests = function () {
    //    var isValid = true;

    //    $(".require-one-group").each(function (index, element) {

    //        var dataId = $(element).data("id");
    //        if (MyRio2cCommon.isNullOrEmpty(dataId)) {
    //            return;
    //        }

    //        if ($('[data-id="' + dataId + '"].require-one-item:checked').length > 0 === false) {
    //            $('[data-valmsg-for="' + dataId + '"]').html('<span for="' + dataId + '" generated="true" class="">' + labels.selectAtLeastOneOption + '</span>');
    //            $('[data-valmsg-for="' + dataId + '"]').removeClass('field-validation-valid');
    //            $('[data-valmsg-for="' + dataId + '"]').addClass('field-validation-error');

    //            isValid = false;
    //        }
    //        else {
    //            $('[data-valmsg-for="' + dataId + '"]').html('');
    //            $('[data-valmsg-for="' + dataId + '"]').addClass('field-validation-valid');
    //            $('[data-valmsg-for="' + dataId + '"]').removeClass('field-validation-error');
    //        }
    //    });

    //    // Enable checkbox change on first submit
    //    $(".require-one-item").not('.change-event-enabled').on('change', function () {
    //        validateInterests();
    //    });

    //    $(".require-one-item").addClass('change-event-enabled');

    //    return isValid;
    //};

    // Form submit --------------------------------------------------------------------------------
    var submit = function () {
        //if (validateInterests()) {
            $(updateFormId).submit();
        //}
    };

    return {
        init: function () {
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            show();
        },
        showUpdateModal: function () {
            showUpdateModal();
        },
        submit: function () {
            submit();
        }
    };
}();
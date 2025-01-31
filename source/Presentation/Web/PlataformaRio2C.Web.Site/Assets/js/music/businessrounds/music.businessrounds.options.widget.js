// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Gilson Oliveira
// Created          : 01-31-2025
//
// Last Modified By : Gilson Oliveira
// Last Modified On : 01-31-2025
// ***********************************************************************
// <copyright file="music.businessrounds.options.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
var MusicBusinessRoundProjectsOptionsWidget = function () {

    var widgetElementId = '#MusicBusinessRoundProjectsOptionsWidget';
    var widgetElement = $(widgetElementId);

    var updateModalId = '#UpdateOptionsModal';
    var updateFormId = '#UpdateOptionsForm';

    // Show ---------------------------------------------------------------------------------------
    var enableShowPlugins = function () {
        KTApp.initTooltips();
    };

    var show = function () {
        if (widgetElement.length <= 0) {
            return;
        }

        var jsonParameters = new Object();
        jsonParameters.musicProjectUid = $('#AggregateId').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Music/BusinessRoundProjects/ShowOptionsWidget'), jsonParameters, function (data) {
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

                if (typeof (MusicBusinessRoundProjectsOptionsWidget) !== 'undefined') {
                    MusicBusinessRoundProjectsOptionsWidget.init();
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
        MyRio2cCommon.enableAtLeastOnCheckboxByNameValidation(updateFormId);
        enableAjaxForm();
        MyRio2cCommon.enableFormValidation({ formIdOrClass: updateFormId, enableHiddenInputsValidation: true, enableMaxlength: true });

        // Enable additional info textbox
        if (typeof (MyRio2cCommonAdditionalInfo) !== 'undefined') {
            MyRio2cCommonAdditionalInfo.init();
        }
    };

    var showUpdateModal = function () {
        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();
        jsonParameters.musicProjectUid = $('#AggregateId').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Music/BusinessRoundProjects/ShowUpdateOptionsModal'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
            data: data,
            // Success
            onSuccess: function () {
                enableUpdatePlugins();
                $(updateModalId).modal();
                PlayersCategoriesValidation.init();
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
   
    //// Form submit --------------------------------------------------------------------------------
    //var submit = function () {
    //    var validator = $(updateFormId).validate();
    //    var formValidation = $(updateFormId).valid();
    //    //var interestsValidation = MyRio2cCommon.validateRequireOneGroup();

    //    if (formValidation/* && interestsValidation*/) {
    //        MyRio2cCommon.submitForm(updateFormId);
    //    }
    //    else {
    //        validator.focusInvalid();
    //    }
    //};

    return {
        init: function () {
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            show();
        },
        showUpdateModal: function () {
            showUpdateModal();
        },
        //submit: function () {
        //    submit();
        //}
    };
}();

var PlayersCategoriesValidation = function () {

    //function to control players categories field in project info form
    var handleCheckboxSelection = function () {
        // all checkboxes
        var checkboxes = $('input[name="PlayerCategoriesUids"]');
        var textFieldWrapper = $('#PlayerCategoriesThatHaveOrHadContract').closest('.form-group');
        var textField = $('#PlayerCategoriesThatHaveOrHadContract');

        checkboxes.on('change', function () {
            var anyChecked = checkboxes.is(':checked');
            $('#IsPlayersCategoriesDiscursiveRequired').val(anyChecked);

            //hide or show control
            if (anyChecked) {
                textFieldWrapper.show();
            } else {
                textFieldWrapper.hide();
                textField.val('');
            }
        });

        if (!checkboxes.is(':checked')) {
            textFieldWrapper.hide();
        }
    };

    return {
        init: function () {
            handleCheckboxSelection();
        }
    };
}();
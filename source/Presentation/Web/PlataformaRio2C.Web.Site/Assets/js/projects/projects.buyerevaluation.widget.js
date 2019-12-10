// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 12-09-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-10-2019
// ***********************************************************************
// <copyright file="projects.buyerevaluation.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var ProjectsBuyerEvaluationWidget = function () {

    var widgetElementId = '#ProjectBuyerEvaluationWidget';
    var widgetElement;

    var acceptModalId = '#AcceptEvaluationModal';
    var acceptFormId = '#AcceptEvaluationForm';


    // Initialize Elements ------------------------------------------------------------------------
    var initElements = function () {
        widgetElement = $(widgetElementId);
    };

    // Show ---------------------------------------------------------------------------------------
    var enableShowPlugins = function () {
        KTApp.initTooltips();
        MyRio2cCommon.initScroll();
        MyRio2cCommon.enableSelect2({ inputIdOrClass: '.enable-select2' });
        enablePageSizeChangeEvent();
    };

    var show = function () {
        if (widgetElement.length <= 0) {
            return;
        }

        var jsonParameters = new Object();
        jsonParameters.searchKeywords = $('#SearchKeywords').val();
        jsonParameters.interestUid = $('#InterestUid').val();
        jsonParameters.page = $('#Page').val();
        jsonParameters.pageSize = $('#PageSize').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Projects/ShowEvaluationListWidget'), jsonParameters, function (data) {
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
            MyRio2cCommon.unblock();
        });
    };

    // Search -------------------------------------------------------------------------------------
    var search = function () {
        $('#Page').val('1');
        ProjectsBuyerEvaluationWidget.init();
    };

    var enableSearchEvent = function () {
        $('#SearchKeywords').not('.search-event-enabled').on('search', function () {
            search();
        });

        $('#SearchKeywords').addClass('search-event-enabled');
    };

    // Pagination ---------------------------------------------------------------------------------
    var enablePageSizeChangeEvent = function () {
        $('#PageSizeDropdown').not('.change-event-enabled').on('change', function () {
            $('#PageSize').val($(this).val());
            ProjectsBuyerEvaluationWidget.search();
        });

        $('#PageSizeDropdown').addClass('change-event-enabled');
    };

    var changePage = function () {
        MyRio2cCommon.block();
    };

    var handlePaginationReturn = function (data) {
        MyRio2cCommon.handleAjaxReturn({
            data: data,
            // Success
            onSuccess: function () {
                enableShowPlugins();
                MyRio2cCommon.unblock();
                $('#ContactsSearchKeywords').focus();
            },
            // Error
            onError: function () {
                MyRio2cCommon.unblock();
            }
        });
    };

    // Accept -------------------------------------------------------------------------------------
    var enableAcceptAjaxForm = function () {
        MyRio2cCommon.enableAjaxForm({
            idOrClass: acceptFormId,
            onSuccess: function (data) {
                $(acceptModalId).modal('hide');

                if (typeof (ProjectsBuyerEvaluationWidget) !== 'undefined') {
                    ProjectsBuyerEvaluationWidget.init();
                }
            },
            onError: function (data) {
                if (MyRio2cCommon.hasProperty(data, 'pages')) {
                    enableAcceptPlugins();
                }
            }
        });
    };

    var enableAcceptPlugins = function () {
        MyRio2cCommon.enableSelect2({ inputIdOrClass: acceptFormId + ' .enable-select2' });
        enableAcceptAjaxForm();
        MyRio2cCommon.enableFormValidation({ formIdOrClass: acceptFormId, enableHiddenInputsValidation: true, enableMaxlength: true });
    };

    var showAcceptModal = function (projectUid) {
        if (MyRio2cCommon.isNullOrEmpty(projectUid)) {
            return;
        }

        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();
        jsonParameters.projectUid = projectUid;

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Projects/ShowAcceptEvaluationModal'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    enableAcceptPlugins();
                    $(acceptModalId).modal();
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

    //// Accept -------------------------------------------------------------------------------------
    //var accept = function (projectUid) {
    //    var jsonParameters = new Object();
    //    jsonParameters.projectUid = projectUid;

    //    $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Projects/Accept'), jsonParameters, function (data) {
    //        MyRio2cCommon.handleAjaxReturn({
    //            data: data,
    //            // Success
    //            onSuccess: function () {
    //                enableShowPlugins();
    //                show();
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

    //var showAcceptModal = function (projectUid) {
    //    bootbox.dialog({
    //        title: translations2.finishModalTitle,
    //        message: translations2.finishModalMessage,
    //        closeButton: false,
    //        buttons: {
    //            cancel: {
    //                label: labels.cancel,
    //                className: "btn btn-secondary mr-auto",
    //                callback: function () {
    //                }
    //            },
    //            confirm: {
    //                label: labels.confirm,
    //                className: "btn btn-info",
    //                callback: function () {
    //                    accept();
    //                }
    //            }
    //        }
    //    });
    //};

    //// Refuse -------------------------------------------------------------------------------------
    //var refuse = function (reason) {
    //    var jsonParameters = new Object();
    //    jsonParameters.id = $('#Uid').val();
    //    jsonParameters.reason = reason;
    //    //jsonParameters.reason = $('#Reason').val();

    //    $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Projects/Refuse'), jsonParameters, function (data) {
    //        MyRio2cCommon.handleAjaxReturn({
    //            data: data,
    //            // Success
    //            onSuccess: function () {
    //                enableShowPlugins();
    //                show();
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

    //var showRefuseModal = function (projectUid) {
    //    var informReason = function () {
    //        bootbox.alert("Reason is a required field!", function () {
    //            showRefuseModal();
    //            return;
    //        });
    //    };

    //    bootbox.dialog({
    //        title: translations.finishModalTitle,
    //        message: translations.finishModalMessage + "<br/>Reason: <input type='text' id='reason' class='form-control'>",
    //        closeButton: false,
    //        buttons: {
    //            cancel: {
    //                label: labels.cancel,
    //                className: "btn btn-secondary mr-auto",
    //                callback: function () {
    //                }
    //            },
    //            confirm: {
    //                label: labels.confirm,
    //                className: "btn btn-info",
    //                callback: function () {
    //                    if ($('#reason').val().trim() == "") {
    //                        informReason();
    //                        return;
    //                    }
    //                    refuse($('#reason').val());
    //                }
    //            }
    //        }
    //    });
    //};

    return {
        init: function () {
            initElements();
            MyRio2cCommon.block();
            enableSearchEvent();
            show();
        },
        search: function () {
            search();
        },
        changePage: function () {
            changePage();
        },
        handlePaginationReturn: function (data) {
            handlePaginationReturn(data);
        },
        showAcceptModal: function (projectUid) {
            showAcceptModal(projectUid);
        },
        showRefuseModal: function (projectUid) {
            showRefuseModal(projectUid);
        }
    };
}();
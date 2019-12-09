// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 12-09-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-09-2019
// ***********************************************************************
// <copyright file="projects.buyerevaluation.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var ProjectsBuyerEvaluationWidget = function () {

    var widgetElementId = '#ProjectBuyerEvaluationWidget';
    var widgetElement;

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
        jsonParameters.interestUid = $('#Interest_Uid').val();
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

    // Accept -------------------------------------------------------------------------------------
    var confirmAccept = function () {
        var accept = function () {
            if (widgetElement.length <= 0) {
                return;
            }

            var jsonParameters = new Object();
            jsonParameters.id = $('#Uid').val();

            $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Projects/Approve'), jsonParameters, function (data) {
                MyRio2cCommon.handleAjaxReturn({
                    data: data,
                    // Success
                    onSuccess: function () {
                        enableShowPlugins();
                        show();
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

        bootbox.dialog({
            title: translations2.finishModalTitle,
            message: translations2.finishModalMessage,
            closeButton: false,
            buttons: {
                cancel: {
                    label: labels.cancel,
                    className: "btn btn-secondary mr-auto",
                    callback: function () {
                    }
                },
                confirm: {
                    label: labels.confirm,
                    className: "btn btn-info",
                    callback: function () {
                        accept();
                    }
                }
            }
        });
    };

    // Refuse -------------------------------------------------------------------------------------
    var refuse = function (reason) {
        if (widgetElement.length <= 0) {
            return;
        }

        var jsonParameters = new Object();
        jsonParameters.id = $('#Uid').val();
        jsonParameters.reason = reason;
        //jsonParameters.reason = $('#Reason').val();

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Projects/Refuse'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    enableShowPlugins();
                    show();
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

    var confirmRefuse = function () {
        var informReason = function () {
            bootbox.alert("Reason is a required field!", function () {
                confirmRefuse();
                return;
            });
        };

        bootbox.dialog({
            title: translations.finishModalTitle,
            message: translations.finishModalMessage + "<br/>Reason: <input type='text' id='reason' class='form-control'>",
            closeButton: false,
            buttons: {
                cancel: {
                    label: labels.cancel,
                    className: "btn btn-secondary mr-auto",
                    callback: function () {
                    }
                },
                confirm: {
                    label: labels.confirm,
                    className: "btn btn-info",
                    callback: function () {
                        if ($('#reason').val().trim() == "") {
                            informReason();
                            return;
                        }
                        refuse($('#reason').val());
                    }
                }
            }
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
        confirmAccept: function () {
            confirmAccept();
        },
        confirmRefuse: function () {
            confirmRefuse();
        }
    };
}();
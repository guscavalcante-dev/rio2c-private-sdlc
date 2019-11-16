// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 11-11-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-16-2019
// ***********************************************************************
// <copyright file="projects.buyercompany.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var ProjectsBuyerCompanyWidget = function () {

    var widgetElementId = '#ProjectBuyercompanyWidget';
    var widgetElement = $(widgetElementId);

    var updateModalId = '#UpdateBuyerCompanyModal';
    //var updateFormId = '#UpdateBuyerCompanyForm';

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
        jsonParameters.projectUid = $('#AggregateId').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Projects/ShowBuyerCompanyWidget'), jsonParameters, function (data) {
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
    //var enableAjaxForm = function () {
    //    MyRio2cCommon.enableAjaxForm({
    //        idOrClass: updateFormId,
    //        onSuccess: function (data) {
    //            $(updateModalId).modal('hide');

    //            if (typeof (ProjectsBuyerCompanyWidget) !== 'undefined') {
    //                ProjectsBuyerCompanyWidget.init();
    //            }
    //        },
    //        onError: function (data) {
    //            if (MyRio2cCommon.hasProperty(data, 'pages')) {
    //                enableUpdatePlugins();
    //            }
    //        }
    //    });
    //};

    var enableUpdatePlugins = function () {
        //MyRio2cCommon.enableSelect2({ inputIdOrClass: updateFormId + ' .enable-select2' });
        //enableAjaxForm();
        //MyRio2cCommon.enableFormValidation({ formIdOrClass: updateFormId, enableHiddenInputsValidation: true, enableMaxlength: true });
    };

    var showUpdateModal = function () {
        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();
        jsonParameters.projectUid = $('#AggregateId').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Projects/ShowUpdateBuyerCompanyModal'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    enableUpdatePlugins();
                    $(updateModalId).modal();

                    setTimeout(function () {
                        if (typeof (ProjectsBuyerCompanySelectedWidget) !== 'undefined') {
                            ProjectsBuyerCompanySelectedWidget.init();
                        }

                        if (typeof (ProjectsMatchBuyerCompanyWidget) !== 'undefined') {
                            ProjectsMatchBuyerCompanyWidget.init();
                        }

                        if (typeof (ProjectsAllBuyerCompanyWidget) !== 'undefined') {
                            ProjectsAllBuyerCompanyWidget.init();
                        }
                    }, 300);
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

    // Select/Unselect company --------------------------------------------------------------------
    var toggleSelectButtons = function () {
        var selectedBuyerEvaluations = $('.selected-buyer-evaluation');
        if ($('#ProjectsBuyerEvaluationsAvailable').val() <= 0) {
            $('.select-buyer-evaluation-button').each(function() {
                MyRio2cCommon.hide($(this));
            });
        }
        else {
            $('.select-buyer-evaluation-button').each(function () {
                MyRio2cCommon.show($(this));
            });

            selectedBuyerEvaluations.each(function () {
                $('.select-buyer-evaluation-button[data-attendeeorganizationuid="' + $(this).data('attendeeorganizationuid') + '"]').each(function () {
                    MyRio2cCommon.hide($(this));
                });
            });
        }
    };

    var selectCompany = function (attendeeOrganizationUid) {
        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();
        jsonParameters.projectUid = $('#AggregateId').val();
        jsonParameters.attendeeOrganizationUid = attendeeOrganizationUid;

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Projects/CreateBuyerEvaluation'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    enableUpdatePlugins();
                    $(updateModalId).modal();

                    if (typeof (ProjectsBuyerCompanyWidget) !== 'undefined') {
                        ProjectsBuyerCompanyWidget.init();
                    }

                    if (typeof (ProjectsBuyerCompanySelectedWidget) !== 'undefined') {
                        ProjectsBuyerCompanySelectedWidget.init();
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

    var unselectCompany = function (attendeeOrganizationUid) {
        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();
        jsonParameters.projectUid = $('#AggregateId').val();
        jsonParameters.attendeeOrganizationUid = attendeeOrganizationUid;

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Projects/DeleteBuyerEvaluation'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    enableUpdatePlugins();
                    $(updateModalId).modal();

                    if (typeof (ProjectsBuyerCompanyWidget) !== 'undefined') {
                        ProjectsBuyerCompanyWidget.init();
                    }

                    if (typeof (ProjectsBuyerCompanySelectedWidget) !== 'undefined') {
                        ProjectsBuyerCompanySelectedWidget.init();
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

    // Finish -------------------------------------------------------------------------------------
    var finish = function (originPage) {
        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();
        jsonParameters.projectUid = $('#AggregateId').val();
        jsonParameters.originPage = originPage;

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Projects/Finish'), jsonParameters, function (data) {
                MyRio2cCommon.handleAjaxReturn({
                    data: data,
                    // Success
                    onSuccess: function () {
                        if (originPage === 'SubmittedDetails') {
                            if (typeof (ProjectsMainInformationWidget) !== 'undefined') {
                                ProjectsMainInformationWidget.init();
                            }

                            if (typeof (ProjectsInterestWidget) !== 'undefined') {
                                ProjectsInterestWidget.init();
                            }

                            if (typeof (ProjectsLinksWidget) !== 'undefined') {
                                ProjectsLinksWidget.init();
                            }

                            if (typeof (ProjectsBuyerCompanyWidget) !== 'undefined') {
                                ProjectsBuyerCompanyWidget.init();
                            }

                            var modal = $(updateModalId);
                            if (modal.length > 0) {
                                modal.modal('hide');
                            }
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

    var showFinishModal = function (originPage) {
        var buyerEvaluationsAvailable = $('#ProjectsBuyerEvaluationsAvailable').val();
        if (buyerEvaluationsAvailable <= 0) {
            bootbox.dialog({
                title: translations.finishModalTitle,
                message: translations.finishModalMessage,
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
                            finish(originPage);
                        }
                    }
                }
            });
        }
        else {
            var playerTranslation = translations.players;
            if (buyerEvaluationsAvailable === '1') {
                playerTranslation = translations.player;
            }

            bootbox.dialog({
                title: translations.finishModalTitle,
                message: translations.finishPendingModalMessage
                                            .replace('{0}', buyerEvaluationsAvailable)
                                            .replace('{1}', playerTranslation),
                closeButton: false,
                className: "modal-bg-red",
                buttons: {
                    cancel: {
                        label: labels.cancel,
                        className: "btn btn-light mr-auto",
                        callback: function () {
                        }
                    },
                    confirm: {
                        label: labels.confirm,
                        className: "btn btn-info",
                        callback: function () {
                            finish(originPage);
                        }
                    }
                }
            });
        }
    };

    var save = function (url) {
        MyRio2cCommon.block();
        window.location.replace(url);
    };

    return {
        init: function () {
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            show();
        },
        showUpdateModal: function () {
            showUpdateModal();
        },
        toggleSelectButtons: function () {
            toggleSelectButtons();
        },
        selectCompany: function (attendeeOrganizationUid) {
            selectCompany(attendeeOrganizationUid);
        },
        unselectCompany: function (attendeeOrganizationUid) {
            unselectCompany(attendeeOrganizationUid);
        },
        showFinishModal: function (originPage) {
            showFinishModal(originPage);
        },
        save: function (url) {
            save(url);
        }
    };
}();

var ProjectsBuyerCompanySelectedWidget = function () {

    var widgetElementId = '#ProjectBuyerCompanySelectedWidget';
    var widgetElement;

    // Init elements ------------------------------------------------------------------------------
    var initElements = function () {
        widgetElement = $(widgetElementId);
    };

    // Show ---------------------------------------------------------------------------------------
    var enableShowPlugins = function () {
        KTApp.initTooltips();
        MyRio2cCommon.initScroll();

        if (typeof (ProjectsBuyerCompanyWidget) !== 'undefined') {
            ProjectsBuyerCompanyWidget.toggleSelectButtons();
        }
    };

    var show = function () {
        if (widgetElement.length <= 0) {
            return;
        }

        var jsonParameters = new Object();
        jsonParameters.projectUid = $('#AggregateId').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Projects/ShowBuyerCompanySelectedWidget'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    enableShowPlugins();
                    MyRio2cCommon.initScroll();
                },
                // Error
                onError: function () {
                }
            });
        })
        .fail(function () {
            //showAlert();
            //MyRio2cCommon.unblock(widgetElementId);
        })
        .always(function () {
            MyRio2cCommon.unblock({ idOrClass: widgetElementId });
        });
    };

    return {
        init: function () {
            initElements();
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            show();
        }
    };
}();

var ProjectsMatchBuyerCompanyWidget = function () {

    var widgetElementId = '#ProjectMatchBuyerCompanyWidget';
    var widgetElement;

    // Init elements ------------------------------------------------------------------------------
    var initElements = function () {
        widgetElement = $(widgetElementId);
    };

    // Show ---------------------------------------------------------------------------------------
    var enableShowPlugins = function () {
        KTApp.initTooltips();
        MyRio2cCommon.initScroll();

        if (typeof (ProjectsBuyerCompanyWidget) !== 'undefined') {
            ProjectsBuyerCompanyWidget.toggleSelectButtons();
        }
    };

    var show = function () {
        if (widgetElement.length <= 0) {
            return;
        }

        var jsonParameters = new Object();
        jsonParameters.projectUid = $('#AggregateId').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Projects/ShowProjectMatchBuyerCompanyWidget'), jsonParameters, function (data) {
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
                //showAlert();
                //MyRio2cCommon.unblock(widgetElementId);
            })
            .always(function () {
                MyRio2cCommon.unblock({ idOrClass: widgetElementId });
            });
    };

    // Pagination ---------------------------------------------------------------------------------
    var changePage = function () {
        MyRio2cCommon.block({ idOrClass: widgetElementId });
    };

    var handlePaginationReturn = function (data) {
        MyRio2cCommon.handleAjaxReturn({
            data: data,
            // Success
            onSuccess: function () {
                enableShowPlugins();
                MyRio2cCommon.unblock({ idOrClass: widgetElementId });
            },
            // Error
            onError: function () {
                MyRio2cCommon.unblock({ idOrClass: widgetElementId });
            }
        });
    };

    return {
        init: function () {
            initElements();
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            show();
        },
        changePage: function () {
            changePage();
        },
        handlePaginationReturn: function(data) {
            handlePaginationReturn(data);
        }
    };
}();

var ProjectsAllBuyerCompanyWidget = function () {

    var widgetElementId = '#ProjectAllBuyerCompanyWidget';
    var widgetElement;

    // Init elements ------------------------------------------------------------------------------
    var initElements = function () {
        widgetElement = $(widgetElementId);
    };

    // Show ---------------------------------------------------------------------------------------
    var enableShowPlugins = function () {
        KTApp.initTooltips();
        MyRio2cCommon.initScroll();

        if (typeof (ProjectsBuyerCompanyWidget) !== 'undefined') {
            ProjectsBuyerCompanyWidget.toggleSelectButtons();
        }
    };

    var show = function () {
        if (widgetElement.length <= 0) {
            return;
        }

        var jsonParameters = new Object();
        jsonParameters.projectUid = $('#AggregateId').val();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Projects/ShowProjectAllBuyerCompanyWidget'), jsonParameters, function (data) {
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
            //showAlert();
            //MyRio2cCommon.unblock(widgetElementId);
        })
        .always(function () {
            MyRio2cCommon.unblock({ idOrClass: widgetElementId });
        });
    };

    // Pagination ---------------------------------------------------------------------------------
    var changePage = function () {
        MyRio2cCommon.block({ idOrClass: widgetElementId });
    };

    var handlePaginationReturn = function (data) {
        MyRio2cCommon.handleAjaxReturn({
            data: data,
            // Success
            onSuccess: function () {
                enableShowPlugins();
                MyRio2cCommon.unblock({ idOrClass: widgetElementId });
            },
            // Error
            onError: function () {
                MyRio2cCommon.unblock({ idOrClass: widgetElementId });
            }
        });
    };

    return {
        init: function () {
            initElements();
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            show();
        },
        changePage: function () {
            changePage();
        },
        handlePaginationReturn: function (data) {
            handlePaginationReturn(data);
        }
    };
}();
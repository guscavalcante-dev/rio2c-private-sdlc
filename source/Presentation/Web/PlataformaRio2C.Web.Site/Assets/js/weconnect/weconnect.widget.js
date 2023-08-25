// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Renan Valentim
// Created          : 08-19-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-23-2023
// ***********************************************************************
// <copyright file="weconnect.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var WeConnectWidget = function () {

    var widgetElementId = '#WeConnectWidget';
    var widgetElement = $(widgetElementId);

    // Show ---------------------------------------------------------------------------------------
    var enableShowPlugins = function () {
        KTApp.initTooltips();
        MyRio2cCommon.initScroll();
        enablePauseAllVideosWhenAnotherPlays();
    };

    var enablePauseAllVideosWhenAnotherPlays = function () {
        var medias = Array.prototype.slice.apply(document.querySelectorAll('audio,video'));
        medias.forEach(function (media) {
            media.addEventListener('play', function (event) {
                medias.forEach(function (media) {
                    if (event.target != media) media.pause();
                });
            });
        });
    }

    var show = function () {
        if (widgetElement.length <= 0) {
            return;
        }

        var jsonParameters = new Object();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Home/ShowWeConnectWidget'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    enableShowPlugins();

                    if (typeof data.hasNextPage !== 'undefined' && data.hasNextPage != null && data.hasNextPage === true) {
                        $('.btn-loadMore').removeClass('d-none');
                    }
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

    var loadMorePageCont = 2;
    var loadMore = function () {
        if (widgetElement.length <= 0) {
            return;
        }

        var jsonParameters = new Object();
        jsonParameters.page = loadMorePageCont;

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Home/WeConnectWidgetLoadMore'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    if (typeof data.page !== 'undefined' && data.page != null && data.page !== '') {
                        $(".weconnect-publications-items").after(data.page);
                        scrollSmoothlyToBottom('#weconnect-scroll');
                        enablePauseAllVideosWhenAnotherPlays();
                    }

                    if (typeof data.hasNextPage !== 'undefined' && data.hasNextPage != null && data.hasNextPage === true) {
                        loadMorePageCont++;
                    }
                    else {
                        $('.btn-loadMore').addClass('d-none');
                    }
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

    const scrollSmoothlyToBottom = (elementId) => {
        const element = $(`${elementId}`);
        element.animate({
            scrollTop: element.prop("scrollHeight")
        }, 1000);
    }

    return {
        init: function () {
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            show();
        },
        loadMore: function () {
            loadMore();
        }
    };
}();
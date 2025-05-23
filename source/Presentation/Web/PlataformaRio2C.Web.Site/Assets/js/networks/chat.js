// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 11-29-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-05-2019
// ***********************************************************************
// <copyright file="chat.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
"use strict";

// Class definition
var MyRio2cKTAppChat = function () {
	var chatAsideEl;
	var chatContentEl;

	// Private functions
    var initAside = function () {

        // Disable canvas when new conversation selected
        KTUtil.data(chatAsideEl).remove('offcanvas');
        $('#kt_chat_aside_close').click();

		// Mobile offcanvas for mobile mode
		var offcanvas = new KTOffcanvas(chatAsideEl, {
            overlay: true,  
            baseClass: 'kt-app__aside',
            closeBy: 'kt_chat_aside_close',
            toggleBy: 'kt_chat_aside_mobile_toggle'
        }); 

		// User listing 
		var userListEl = KTUtil.find(chatAsideEl, '.kt-scroll');
		if (!userListEl) {
			return;
		}

		// Initialize perfect scrollbar(see:  https://github.com/utatti/perfect-scrollbar) 
        KTUtil.scrollInit(userListEl, {
            windowScroll: false,
			mobileNativeScroll: true,  // enable native scroll for mobile
			desktopNativeScroll: false, // disable native scroll and use custom scroll for desktop 
			resetHeightOnDestroy: true,  // reset css height on scroll feature destroyed
			handleWindowResize: true, // recalculate hight on window resize
			rememberPosition: true, // remember scroll position in cookie
            height: function () {  // calculate height
                return 533;

				//var height;
				//var portletBodyEl = KTUtil.find(chatAsideEl, '.kt-portlet > .kt-portlet__body');
				//var widgetEl = KTUtil.find(chatAsideEl, '.kt-widget.kt-widget--users');
				//var searchbarEl = KTUtil.find(chatAsideEl, '.kt-searchbar');

				//if (KTUtil.isInResponsiveRange('desktop')) {
				//	height = KTLayout.getContentHeight();
				//} else {
				//	height = KTUtil.getViewPort().height;
				//}

				//if (chatAsideEl) {
				//	height = height - parseInt(KTUtil.css(chatAsideEl, 'margin-top')) - parseInt(KTUtil.css(chatAsideEl, 'margin-bottom'));
				//	height = height - parseInt(KTUtil.css(chatAsideEl, 'padding-top')) - parseInt(KTUtil.css(chatAsideEl, 'padding-bottom'));
				//}

				//if (widgetEl) {
				//	height = height - parseInt(KTUtil.css(widgetEl, 'margin-top')) - parseInt(KTUtil.css(widgetEl, 'margin-bottom'));
				//	height = height - parseInt(KTUtil.css(widgetEl, 'padding-top')) - parseInt(KTUtil.css(widgetEl, 'padding-bottom'));
				//}

				//if (portletBodyEl) {
				//	height = height - parseInt(KTUtil.css(portletBodyEl, 'margin-top')) - parseInt(KTUtil.css(portletBodyEl, 'margin-bottom'));
				//	height = height - parseInt(KTUtil.css(portletBodyEl, 'padding-top')) - parseInt(KTUtil.css(portletBodyEl, 'padding-bottom'));
				//}

				//if (searchbarEl) {
				//	height = height - parseInt(KTUtil.css(searchbarEl, 'height'));
				//	height = height - parseInt(KTUtil.css(searchbarEl, 'margin-top')) - parseInt(KTUtil.css(searchbarEl, 'margin-bottom'));
				//}

				//// remove additional space
				//height = height - 93;
				
				//return height;
			} 
		});
	};

	return {
		// public functions
		init: function() {
			// elements
			chatAsideEl = KTUtil.getByID('kt_chat_aside');

			// init aside and user list
			initAside();

			// init inline chat example
            MyRio2cKTChat.setup(KTUtil.getByID('kt_chat_content'));
		}
	};
}();

var MyRio2cKTChat = function () {
    var initChat = function (parentEl) {
        var messageListEl = KTUtil.find(parentEl, '.kt-scroll');

        if (!messageListEl) {
            return;
        }

        // initialize perfect scrollbar(see:  https://github.com/utatti/perfect-scrollbar) 
        KTUtil.scrollInit(messageListEl, {
            windowScroll: false, // allow browser scroll when the scroll reaches the end of the side
            mobileNativeScroll: true,  // enable native scroll for mobile
            desktopNativeScroll: false, // disable native scroll and use custom scroll for desktop 
            resetHeightOnDestroy: true,  // reset css height on scroll feature destroyed
            handleWindowResize: true, // recalculate hight on window resize
            rememberPosition: true, // remember scroll position in cookie
            height: function () {  // calculate height
                return 400;
                //var height;

                //// Mobile mode
                //if (KTUtil.isInResponsiveRange('tablet-and-mobile')) {
                //    return KTUtil.hasAttr(messageListEl, 'data-mobile-height') ? parseInt(KTUtil.attr(messageListEl, 'data-mobile-height')) : 300;
                //}

                //// Desktop mode
                //if (KTUtil.isInResponsiveRange('desktop') && KTUtil.hasAttr(messageListEl, 'data-height')) {
                //    return parseInt(KTUtil.attr(messageListEl, 'data-height'));
                //}

                //var chatEl = KTUtil.find(parentEl, '.kt-chat');
                //var portletHeadEl = KTUtil.find(parentEl, '.kt-portlet > .kt-portlet__head');
                //var portletBodyEl = KTUtil.find(parentEl, '.kt-portlet > .kt-portlet__body');
                //var portletFootEl = KTUtil.find(parentEl, '.kt-portlet > .kt-portlet__foot');

                //if (KTUtil.isInResponsiveRange('desktop')) {
                //    height = KTLayout.getContentHeight();
                //} else {
                //    height = KTUtil.getViewPort().height;
                //}

                //if (chatEl) {
                //    height = height - parseInt(KTUtil.css(chatEl, 'margin-top')) - parseInt(KTUtil.css(chatEl, 'margin-bottom'));
                //    height = height - parseInt(KTUtil.css(chatEl, 'padding-top')) - parseInt(KTUtil.css(chatEl, 'padding-bottom'));
                //}

                //if (portletHeadEl) {
                //    height = height - parseInt(KTUtil.css(portletHeadEl, 'height'));
                //    height = height - parseInt(KTUtil.css(portletHeadEl, 'margin-top')) - parseInt(KTUtil.css(portletHeadEl, 'margin-bottom'));
                //}

                //if (portletBodyEl) {
                //    height = height - parseInt(KTUtil.css(portletBodyEl, 'margin-top')) - parseInt(KTUtil.css(portletBodyEl, 'margin-bottom'));
                //    height = height - parseInt(KTUtil.css(portletBodyEl, 'padding-top')) - parseInt(KTUtil.css(portletBodyEl, 'padding-bottom'));
                //}

                //if (portletFootEl) {
                //    height = height - parseInt(KTUtil.css(portletFootEl, 'height'));
                //    height = height - parseInt(KTUtil.css(portletFootEl, 'margin-top')) - parseInt(KTUtil.css(portletFootEl, 'margin-bottom'));
                //}

                //// remove additional space
                //height = height - 5;

                //return height;
            }
        });

        //// attach events
        //KTUtil.on(parentEl, '.kt-chat__input textarea', 'keydown', function (e) {
        //    if (e.keyCode == 13) {
        //        handleMessaging();
        //        e.preventDefault();

        //        return false;
        //    }
        //});

        //KTUtil.on(parentEl, '.kt-chat__input .kt-chat__reply', 'click', function (e) {
        //    handleMessaging();
        //});
    };

    return {
        // public functions
        init: function () {
        },
        setup: function (element) {
            initChat(element);
        }
    };
}();
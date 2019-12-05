// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 11-18-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-05-2019
// ***********************************************************************
// <copyright file="networks.contacts.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var NetworksContacts = function () {

    //var searchFormId = '#NetworksContactsSearchForm';

    // Search -------------------------------------------------------------------------------------
    var search = function () {
        if (typeof (NetworksContactsListWidget) !== 'undefined') {
            NetworksContactsListWidget.search();
        }
    };

    var enableSearchEvent = function () {
        $('#SearchKeywords').not('.search-event-enabled').on('search', function () {
            search();
        });

        $('#SearchKeywords').addClass('search-event-enabled');
    };

    // Plugins ------------------------------------------------------------------------------------
    var enableListPlugins = function () {
        enableSearchEvent();
        MyRio2cCommon.enablePaginationBlockUi();
    };

    return {
        init: function () {
            enableListPlugins();
        },
        search: function () {
            search();
        }
    };
}();
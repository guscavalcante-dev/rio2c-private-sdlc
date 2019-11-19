// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 11-18-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-18-2019
// ***********************************************************************
// <copyright file="networks.contacts.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var NetworksContacts = function () {

    //var updateModalId = '#UpdateInterestModal';
    //var updateFormId = '#UpdateInterestForm';

    // Plugins ------------------------------------------------------------------------------------
    var enableListPlugins = function () {
        MyRio2cCommon.enablePaginationBlockUi();
    };

    return {
        init: function () {
            enableListPlugins();
        }
    };
}();
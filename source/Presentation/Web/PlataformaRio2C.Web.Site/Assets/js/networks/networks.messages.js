// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 11-25-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-25-2019
// ***********************************************************************
// <copyright file="networks.messages.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var NetworksMessages = function () {

    //var updateModalId = '#UpdateInterestModal';
    //var updateFormId = '#UpdateInterestForm';

    // Plugins ------------------------------------------------------------------------------------
    var enablePlugins = function () {
        $("time.timeago").timeago();
    };

    return {
        init: function () {
            enablePlugins();
        }
    };
}();
// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 09-11-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-11-2019
// ***********************************************************************
// <copyright file="myrio2c.showhidepassword.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
var MyRio2cShowHidePassword = function () {

    var init = function (options) {
        if (!MyRio2cCommon.hasProperty(options, 'idOrClass') || MyRio2cCommon.isNullOrEmpty(options.idOrClass)) {
            options = new Object();
            options.idOrClass = '.showhidepassword';
        }

        $(options.idOrClass).hidePassword(true);
    };

    return {
        init: function (options) {
            init(options);
        }
    };
}();
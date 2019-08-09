// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 08-09-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-09-2019
// ***********************************************************************
// <copyright file="myrio2c.common.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var MyRio2cCommon = function () {

    var block = function (idOrClass) {
        KTApp.block(idOrClass);
    };

    var unblock = function (idOrClass) {
        KTApp.unblock(idOrClass);
    };

    return {
        init: function () {
        },
        block: function (idOrClass) {
            block(idOrClass);
        },
        unblock: function (idOrClass) {
            unblock(idOrClass);
        }
    };
}();
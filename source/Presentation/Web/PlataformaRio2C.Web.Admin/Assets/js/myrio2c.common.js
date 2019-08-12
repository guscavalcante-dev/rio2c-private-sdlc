// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 08-09-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-12-2019
// ***********************************************************************
// <copyright file="myrio2c.common.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var MyRio2cCommon = function () {

    // General ------------------------------------------------------------------------------------
    var has = function (obj, key) {
        return key.split(".").every(function (x) {
            if (typeof obj != "object" || obj === null || !(x in obj)) {
                return false;
            }

            obj = obj[x];

            return true;
        });
    };

    // Block/unblock UI ---------------------------------------------------------------------------
    var block = function (options) {
        var idOrClass = 'body';

        if (has(options, 'idOrClass')) {
            idOrClass = options.idOrClass;
        }

        KTApp.block(idOrClass);
    };

    var unblock = function (options) {
        var idOrClass = 'body';

        if (has(options, 'idOrClass')) {
            idOrClass = options.idOrClass;
        }

        KTApp.unblock(idOrClass);
    };

    return {
        has: function (obj, key) {
            has(obj, key);
        },
        block: function (options) {
            block(options);
        },
        unblock: function (options) {
            unblock(options);
        }
    };
}();
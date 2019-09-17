// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 09-17-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-17-2019
// ***********************************************************************
// <copyright file="myrio2c.inputmask.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
var MyRio2cInputMask = function () {

    var enableMask = function (inputClassOrId, mask) {
        $(inputClassOrId).inputmask({
            mask: mask,
            removeMaskOnSubmit: false,
            autoUnmask: false
        });
    };

    var removeMask = function (inputClassOrId) {
        $(inputClassOrId).inputmask('remove');
    };

    return {
        enableMask: function (inputClassOrId, mask) {
            enableMask(inputClassOrId, mask);
        },
        removeMask: function (inputClassOrId) {
            removeMask(inputClassOrId);
        }
    };
}();
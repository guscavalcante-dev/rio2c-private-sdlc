// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 08-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-06-2019
// ***********************************************************************
// <copyright file="collaborators.create.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var OnboardWizard = function () {

    // Submit form with block ui ------------------------------------------------------------------
    var submit = function (formIdOrClass) {
        if (MyRio2cCommon.isNullOrEmpty(formIdOrClass)) {
            return;
        }

        if ($(formIdOrClass).valid()) {
            MyRio2cCommon.block();
            $(formIdOrClass).submit();
        }
    };

    return {
        submit: function (formIdOrClass) {
            submit(formIdOrClass);
        }
    };
}();
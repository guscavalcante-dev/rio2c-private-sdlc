// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 08-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-18-2019
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

        var validator = $(formIdOrClass).validate();

        if ($(formIdOrClass).valid()) {
            MyRio2cCommon.block();
            $(formIdOrClass).submit();
        }
        else {
            validator.focusInvalid();
        }
    };

    return {
        submit: function (formIdOrClass) {
            submit(formIdOrClass);
        }
    };
}();
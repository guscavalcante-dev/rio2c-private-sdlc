// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 09-21-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-21-2019
// ***********************************************************************
// <copyright file="myrio2c.publicemail.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
var MyRio2cPublicEmail = function () {

    var togglePublicEmail = function () {
        var sharePublicEmailValue = $("input[name='SharePublicEmail']:checked").val();
        if (sharePublicEmailValue === 'True') {
            $('#PublicEmail').prop('disabled', false);
        }
        else {
            $('#PublicEmail').prop('disabled', true);
            $('#PublicEmail').val('');
        }
    };

    var enableSharePublicEmailChangeEvent = function () {
        $("input[name='SharePublicEmail']").not('.change-event-enabled').on('change', function () {
            togglePublicEmail();
        });

        $("input[name='SharePublicEmail']").not('.change-event-enabled').addClass('change-event-enabled');
    };

    return {
        init: function () {
            togglePublicEmail();
            enableSharePublicEmailChangeEvent();
        }
    };
}();
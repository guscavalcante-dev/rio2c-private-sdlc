// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 09-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-22-2019
// ***********************************************************************
// <copyright file="myrio2c.additionalinfo.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var MyRio2cCommonAdditionalInfo = function () {

    // Enable checkbox toggle additional info -----------------------------------------------------
    var enableToggleAdditionalInfo = function () {
        $('.toggle-additional-info').not('.toggle-event-enabled').on('click', function () {
            var dataId = $(this).data('toggle-id');
            if (MyRio2cCommon.isNullOrEmpty(dataId)) {
                return;
            }

            if ($(this).is(':checked')) {
                $('[data-id="' + dataId + '"] .additional-info').removeClass('d-none');
                $('[data-id="' + dataId + '"] .additional-info .additional-info-textbox').prop('disabled', false);
            }
            else {
                $('[data-id="' + dataId + '"] .additional-info').addClass('d-none');
                $('[data-id="' + dataId + '"] .additional-info .additional-info-textbox').prop('disabled', true);
            }
        });

        $('.toggle-additional-info').not('.toggle-event-enabled').addClass('toggle-event-enabled');
    };

    return {
        init: function() {
            enableToggleAdditionalInfo();
        }
    };
}();
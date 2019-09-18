// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 09-18-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-18-2019
// ***********************************************************************
// <copyright file="myrio2c.companynumber.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
var MyRio2cCompanyDocument = function () {

    var validateBrazil = function(companyNumber) {

        var cnpj = companyNumber.replace(/[^\d]+/g, '');

        if (cnpj === '') {
            return true;
        }

        if (cnpj.length != 14) {
            return false;
        }

        // Elimina CNPJs invalidos conhecidos
        if (cnpj === "00000000000000" ||
            cnpj === "11111111111111" ||
            cnpj === "22222222222222" ||
            cnpj === "33333333333333" ||
            cnpj === "44444444444444" ||
            cnpj === "55555555555555" ||
            cnpj === "66666666666666" ||
            cnpj === "77777777777777" ||
            cnpj === "88888888888888" ||
            cnpj === "99999999999999") {
            return false;
        }

        // Valida DVs
        var tamanho = cnpj.length - 2;
        var numeros = cnpj.substring(0, tamanho);
        var digitos = cnpj.substring(tamanho);
        var soma = 0;
        var pos = tamanho - 7;
        for (var i = tamanho; i >= 1; i--) {
            soma += numeros.charAt(tamanho - i) * pos--;
            if (pos < 2) {
                pos = 9;
            }
        }

        var resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
        if (resultado != digitos.charAt(0)) {
            return false;
        }

        tamanho = tamanho + 1;
        numeros = cnpj.substring(0, tamanho);
        soma = 0;
        pos = tamanho - 7;
        for (var x = tamanho; x >= 1; x--) {
            soma += numeros.charAt(tamanho - x) * pos--;
            if (pos < 2) {
                pos = 9;
            }
        }

        resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
        if (resultado != digitos.charAt(1)) {
            return false;
        }

        return true;
    };

    var validate = function (countryCode, companyNumber) {

        // Brazil
        if (countryCode === 'BR') {
            return validateBrazil(companyNumber);
        }

        return true;
    };

    return {
        validate: function (countryCode, companyNumber) {
            return validate(countryCode, companyNumber);
        }
    };
}();
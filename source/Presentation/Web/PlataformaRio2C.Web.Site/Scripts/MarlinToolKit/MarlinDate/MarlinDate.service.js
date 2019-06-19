(function () {
    'use strict';

    angular
        .module('MarlinDate')
        .service('MarlinDateService', MarlinDateService);

    function MarlinDateService() {
        this.formatarDataMvc = function (dataMvc, idioma) {            
            var pattern = /Date\(([^)]+)\)/,
                results = pattern.exec(dataMvc),
                dt = new Date(parseFloat(results[1]));

            function formatarUnidadeDeData(value, tipo) {
                if (tipo == 'dia' && value < 10) {
                    value = '0' + value;
                }
                else if (tipo == 'mes') {
                    value = (dt.getMonth() + 1);

                    if (value < 10) {
                        value = '0' + value;
                    }
                }
                return value;
            }

            if (idioma == null) {              

                dataMvc = formatarUnidadeDeData(dt.getDate(), 'dia') + "/" + formatarUnidadeDeData(dt.getDate(), 'mes') + "/" + dt.getFullYear();
            }
            else {
                dataMvc = (dt.getMonth() + 1) + "/" + dia + "/" + dt.getFullYear();
            }
            
            return dataMvc;
        }
    }
})();
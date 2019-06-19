(function () {
    'use strict';

    angular
        .module('MarlinAlert')
        .config(['ngToastProvider', MarlinAlertConfig]);

    function MarlinAlertConfig(ngToastProvider) {
        ngToastProvider.configure({
            animation: 'fade', // slide or 'fade'
            verticalPosition: 'bottom',
            dismissButton: true,
            dismissOnTimeout: true,
            timeout: 8000,
            dismissOnClick: false,
            horizontalPosition: 'right'
        });
    }
})();
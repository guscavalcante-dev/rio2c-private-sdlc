    // Show modal ---------------------------------------------------------------------------------
    $('#OnboardingModal').modal({
        backdrop: 'static',
        keyboard: false
    }).css({
        'width': function () {
            var width = $(this).data('width');
            //alert(width);
            //var percentIndex = width.indexOf("%");
            if (percentIndex === width.length - 1) {
                var percentWidth = eval(width.substring(0, percentIndex)) / 100;
                return $(document).width() * percentWidth;
            } else {
                return width;
            }
        },
        'margin-left': function () {
            return -($(this).width() / 2);
        }
    });
    $('#OnboardingModal').modal('show');

/*---LEFT BAR ACCORDION----*/
$(function () {
    $('#nav-accordion').dcAccordion({
        eventType: 'click',
        autoClose: true,
        saveState: true,
        disableLink: true,
        speed: 'slow',
        showCount: false,
        autoExpand: true,
        //        cookie: 'dcjq-accordion-1',
        classExpand: 'dcjq-current-parent'
    });
});

Array.prototype.concatAll = function () {
    var results = [];
    this.forEach(function (subArray) {
        subArray.forEach(function (item) {
            results.push(item);
        });
    });
    return results;
}


var Script = function () {

    //    sidebar dropdown menu auto scrolling

    jQuery('#sidebar .sub-menu > a').click(function () {
        var o = ($(this).offset());
        diff = 250 - o.top;
        if (diff > 0)
            $("#sidebar").scrollTo("-=" + Math.abs(diff), 500);
        else
            $("#sidebar").scrollTo("+=" + Math.abs(diff), 500);
    });

    //    sidebar toggle

    $(function () {
        function responsiveView() {
            var wSize = $(window).width();
            if (wSize <= 768) {
                $('#container').addClass('sidebar-close');
                $('#sidebar > ul').hide();
            }

            if (wSize > 768) {
                $('#container').removeClass('sidebar-close');
                $('#sidebar > ul').show();
            }
        }
        $(window).on('load', responsiveView);
        $(window).on('resize', responsiveView);
    });

    $('.icon-reorder').click(function () {
        if ($('#sidebar > ul').is(":visible") === true) {
            $('#main-content').css({
                'margin-left': '0px'
            });
            $('#sidebar').css({
                'margin-left': '-210px'
            });
            $('#sidebar > ul').hide();
            $("#container").addClass("sidebar-closed");
        } else {
            $('#main-content').css({
                'margin-left': '210px'
            });
            $('#sidebar > ul').show();
            $('#sidebar').css({
                'margin-left': '0'
            });
            $("#container").removeClass("sidebar-closed");
        }
    });

    // custom scrollbar
    //$("#sidebar").niceScroll({styler:"fb",cursorcolor:"#e8403f", cursorwidth: '3', cursorborderradius: '10px', background: '#404040', spacebarenabled:false, cursorborder: ''});

    //$("html").niceScroll({styler:"fb",cursorcolor:"#e8403f", cursorwidth: '6', cursorborderradius: '10px', background: '#404040', spacebarenabled:false,  cursorborder: '', zindex: '1000'});

    // widget tools

    jQuery('.panel .tools .icon-chevron-down').click(function () {
        var el = jQuery(this).parents(".panel").children(".panel-body");
        if (jQuery(this).hasClass("icon-chevron-down")) {
            jQuery(this).removeClass("icon-chevron-down").addClass("icon-chevron-up");
            el.slideUp(200);
        } else {
            jQuery(this).removeClass("icon-chevron-up").addClass("icon-chevron-down");
            el.slideDown(200);
        }
    });

    //Original delete query for the icon-remove glyphicon
    //jQuery('.panel .tools .icon-remove').click(function () {
    //    jQuery(this).parents(".panel").parent().remove();
    //});


    //Edite delete query for icon-remove glyphicon   
    $('#modalDeleteConfirmation').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget) // Button that triggered the modal
        var recipient = button.data('urltarget') // Extract info from data-* attributes
        var modalConfirm = $("div a.button-confirm");
        modalConfirm.attr("href", recipient);
    });

    $('#modalShowReason').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget); 
        var playerName = button.data('playerName');
        var playerReason = button.data('playerReason');
        var playerStatus = button.data('playerStatus');

        $('#playerName').text(playerName);
        $('#playerReason').text(playerReason);
        $('#status').text(playerStatus);
    });


    //    tool tips

    $('.tooltips').tooltip();

    //    popovers

    $('.popovers').popover();



    // custom bar chart

    if ($(".custom-bar-chart")) {
        $(".bar").each(function () {
            var i = $(this).find(".value").html();
            $(this).find(".value").html("");
            $(this).find(".value").animate({
                height: i
            }, 2000)
        })
    }


    $('select[name="culture"]').change(function () {
        window.location.href = $(this).data('target') + "?culture=" + $(this).val();
    });

    $('select[name="area"]').change(function () {
        window.location.href = $(this).data('target') + "?area=" + $(this).val();
    });


}();

$(document).ready(function () {
    $('.input-validation-error').parents('.form-group').addClass('has-error');
    $('.field-validation-error').parents('.form-group').addClass('has-error');

    $('.field-validation-error').addClass('text-danger');

    $('.input-validation-error').on('change', function () {
        $(this).closest('.form-group').removeClass('has-error');
        $(this).closest('.form-group').find('.field-validation-error').remove();
        $(this).closest('.form-group').remove('.field-validation-error');
    });

    $('.form-control').on('change', function () {
        $(this).closest('.form-group').removeClass('has-error');
        $(this).closest('.form-group').find('.field-validation-error').remove();
        $(this).closest('.form-group').remove('.field-validation-error');
    });

    $('.field-validation-error').siblings('.user-heading').addClass('has-error');
});

$(document).ready(function () {

    $.validator.unobtrusive.options = {
        errorPlacement: function (label, element) {
            // Add Bootstrap classes to newly added elements
            label.parents('.form-group').addClass('has-error');
            label.addClass('text-danger');
            console.info('errorPlacement');
        },

        success: function (label) {
            console.info('success');
            // Remove error class from <div class="form-group">, but don't worry about
            // validation error messages as the plugin is going to remove it anyway
            label.parents('.form-group').removeClass('has-error');
        }
    }


    $('#modalStatusMessage').modal('show');



    $(":file").bind("click", function () {
        $('.image-container').unbind("DOMNodeInserted");
        $('.image-container').bind("DOMNodeInserted", function () {
            $(this).unbind("DOMNodeInserted");
            $(".image-container img").ready(function () {
                var dkrm = new Darkroom('.image-container img', {
                    //minWidth: 190,
                    //minHeight: 190,
                    //maxWidth: 290,
                    //maxHeight: 290,
                    //ratio: 3 / 4,
                    backgroundColor: 'transparent',
                    plugins: {
                        crop: {
                            //quickCropKey: 100,
                            //minWidth: 640,
                            //minHeight: 480,
                            ratio: 1,
                        },
                        save: {
                            callback: function () {
                                this.darkroom.selfDestroy();

                                $(".image-container").append("<input type='hidden' name='NewImage.File' />");

                                var data = dkrm.canvas.toDataURL();

                                var base64 = data.replace("data:image/png;base64,", "");

                                $('.image-container input[name="NewImage.File"]').val(base64);
                            }
                        }
                    }
                });
            });
        });
    });
});

//Script for EXCLAMATION TOOLTIPS//
$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
});


$(function () {
    $('form[action="/Term/AcceptTerm"], .form-term').on('submit', function () {
        $(this).find('input[type="submit"]').attr('disabled', 'disabled');
    });

    $('#modalLayoult.modal-term').on('hide.bs.modal', function (e) {
        $(this).find('form').submit();

        e.preventDefault();
    });


    $('.rio2c-btn-logout').on('click', function (e) {
        localStorage.clear();
    });

    $('form').on('submit', function (e) {
        var $form = $(this);

        if (!$form.data('submitted') && $form.valid()) {
            // mark it so that the next submit can be ignored
            $form.data('submitted', true);
            return;
        }

        // form is invalid or previously submitted - skip submit
        e.preventDefault();
    });
});
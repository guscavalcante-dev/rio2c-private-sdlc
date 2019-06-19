/*---LEFT BAR ACCORDION----*/
$(function () {
    $('#nav-accordion').dcAccordion({
        eventType: 'click',
        autoClose: true,
        saveState: false,
        disableLink: true,
        speed: 'slow',
        showCount: false,
        autoExpand: true,
        cookie: 'dcjq-accordion-1',
        classExpand: 'dcjq-current-parent'
    });
});

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
    $("#sidebar").niceScroll({ styler: "fb", cursorcolor: "#e8403f", cursorwidth: '3', cursorborderradius: '10px', background: '#404040', spacebarenabled: false, cursorborder: '' });

    //$("html").niceScroll({ styler: "fb", cursorcolor: "#e8403f", cursorwidth: '6', cursorborderradius: '10px', background: '#404040', spacebarenabled: false, cursorborder: '', zindex: '1000' });

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

    jQuery('.panel .tools .icon-remove').click(function () {
        jQuery(this).parents(".panel").parent().remove();
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


    $('#modalDelete').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget) // Button that triggered the modal
        var recipient = button.data('urltarget') // Extract info from data-* attributes
        var modalConfirm = $("div a.button-confirm");
        var descName = button.data('name');
        var descEmail = button.data('email');
        var descricao = descName + "(" + descEmail + ")";

        modalConfirm.attr("href", recipient);
        $("#modalDeleteDesc").html(descricao);
    });
}();



$(document).ready(function () {
    //$('#modalDelete').modal();
    $('.input-validation-error').parents('.form-group').addClass('has-error');

    $('.field-validation-error').addClass('text-danger');

    $('.input-validation-error').on('change', function () {
        $(this).closest('.form-group').removeClass('has-error');
        $(this).closest('.form-group').find('.field-validation-error').remove();
        $(this).closest('.form-group').remove('.field-validation-error');
    });

    $('.form-control').on('change', function () {
        $(this).closest('.form-group').removeClass('has-error');
        $(this).closest('.form-group').find('.field-validation-error span').remove();
        $(this).closest('.form-group').find('.field-validation-error').removeClass('field-validation-error');
        //    $(this).closest('.form-group').remove('.field-validation-error');
    });

    $('.field-validation-error').siblings('.user-heading').addClass('has-error');
    $('.field-validation-error').closest('.user-heading').addClass('has-error');


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
});

$(":file").bind("click", function () {
    $('.image-container').unbind("DOMNodeInserted");
    $('.image-container').bind("DOMNodeInserted", function () {
        $(this).unbind("DOMNodeInserted");
        $(".image-container img").ready(function () {
            var dkrm = new Darkroom('.image-container img', {
                backgroundColor: 'transparent',
                plugins: {
                    crop: {
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
// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 08-14-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-14-2019
// ***********************************************************************
// <copyright file="myrio2c.cropper.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
var MyRio2cCropper = function () {

    var enableCropper = function () {
        var $image = $('#image');
        var $dataX = $('#CropperImage_DataX');
        var $dataY = $('#CropperImage_DataY');
        var $dataHeight = $('#CropperImage_DataHeight');
        var $dataWidth = $('#CropperImage_DataWidth');
        var $dataRotate = $('#CropperImage_DataRotate');
        var $dataScaleX = $('#CropperImage_DataScaleX');
        var $dataScaleY = $('#CropperImage_DataScaleY');

        var options = {
            //preview: '.img-preview',
            cropBoxMovable: false,
            cropBoxResizable: false,
            aspectRatio: 1 / 1,
            dragMode: 'move',
            crop: function (e) {
                $dataX.val(Math.round(e.x));
                $dataY.val(Math.round(e.y));
                $dataHeight.val(Math.round(e.height));
                $dataWidth.val(Math.round(e.width));
                $dataRotate.val(e.rotate);
                $dataScaleX.val(e.scaleX);
                $dataScaleY.val(e.scaleY);
            }
        };

        // Cropper
        $image
            .on({
                'build.cropper': function (e) {
                },
                'built.cropper': function (e) {
                },
                'cropstart.cropper': function (e) {
                },
                'cropmove.cropper': function (e) {
                },
                'cropend.cropper': function (e) {
                },
                'crop.cropper': function (e) {
                },
                'zoom.cropper': function (e) {
                }
            })
            .cropper(options);

        // Zoom / Reset
        $('.docs-buttons').on('click', '[data-method]', function () {
            var $this = $(this);
            var data = $this.data();
            var $target;
            var result;

            if ($this.prop('disabled') || $this.hasClass('disabled')) {
                return;
            }

            if ($image.data('cropper') && data.method) {
                data = $.extend({}, data); // Clone a new one

                if (typeof data.target !== 'undefined') {
                    $target = $(data.target);

                    if (typeof data.option === 'undefined') {
                        try {
                            data.option = JSON.parse($target.val());
                        } catch (e) {
                            console.log(e.message);
                        }
                    }
                }

                result = $image.cropper(data.method, data.option, data.secondOption);

                switch (data.method) {
                case 'scaleX':
                case 'scaleY':
                    $(this).data('option', -data.option);
                    break;
                }

                if ($.isPlainObject(result) && $target) {
                    try {
                        $target.val(JSON.stringify(result));
                    } catch (e) {
                        console.log(e.message);
                    }
                }

            }
        });

        // Keyboard
        $(document.body).on('keydown', function (e) {
            if (!$image.data('cropper') || this.scrollTop > 300) {
                return;
            }

            switch (e.which) {
            case 37:
                e.preventDefault();
                $image.cropper('move', -1, 0);
                break;

            case 38:
                e.preventDefault();
                $image.cropper('move', 0, -1);
                break;

            case 39:
                e.preventDefault();
                $image.cropper('move', 1, 0);
                break;

            case 40:
                e.preventDefault();
                $image.cropper('move', 0, 1);
                break;
            }
        });

        // Import image
        var $inputImage = $('#ImageFile');
        var URL = window.URL || window.webkitURL;
        var blobURL;

        if (URL) {
            $inputImage.change(function () {

                var files = this.files;
                var file;

                if (!$image.data('cropper')) {
                    return;
                }

                if (files && files.length) {
                    file = files[0];

                    if (/^image\/\w+$/.test(file.type)) {
                        MyRio2cCommon.hide($('.existent-image-container'));
                        MyRio2cCommon.show($('.cropper-control-hide'));

                        blobURL = URL.createObjectURL(file);
                        $image.one('built.cropper', function () {
                            // Revoke when load complete
                            URL.revokeObjectURL(blobURL);
                        }).cropper('reset').cropper('replace', blobURL);
                    }
                    else {
                        MyRio2cCommon.show($('.existent-image-container'));
                        MyRio2cCommon.hide($('.cropper-control-hide'));
                        $('#image').val('');
                    }
                }

                // Validate the form
                //$('#upload-picture-form').validate().form(); //TODO: Enable validation
            });
        } else {
            $inputImage.prop('disabled', true).parent().addClass('disabled');
        }
    };

    var cancel = function() {
        $('#ImageFile').val('');
        MyRio2cCommon.show($('.existent-image-container'));
        MyRio2cCommon.hide($('.cropper-control-hide'));
    };

    return {
        //main function to initiate the module
        init: function () {
            enableCropper();
        },
        cancel: function() {
            cancel();
        }
    };
}();


//$('.cropper-range').on("change mousemove", function () {
//    var currentValue = $(this).val();
//    console.log('currentValue: ' + currentValue);

//    if (currentValue != lastRangeValue) {
//        var divisor = 100;
//        if (currentValue > 0) {
//            divisor = 10;
//        }
//        var ratio = 1 + (currentValue / divisor);
//        console.log('----------');
//        console.log('currentValue: ' + currentValue);
//        console.log('lastRangeValue: ' + lastRangeValue);
//        console.log('lastRangeValue: ' + ratio);
//        $('#image').cropper('zoomTo', ratio);
//    }

//    lastRangeValue = currentValue;
//});
/*
Copyright (c) 2003-2011, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/



CKEDITOR.editorConfig = function (config) {
    var self = this;

    //console.log('config', config);
    //console.log('CKEDITOR', CKEDITOR);
    //config.removePlugins = 'htmldataprocessor';
    //config.removePlugins = 'elementspath';
    //config.format_tags = 'p;h1;h2;h3;h4;h5;h6;pre;address;div';   
    //config.removeFormatTags = 'b,big,cite,code,del,dfn,em,font,i,ins,kbd,pre,q,s,samp,small,span,strike,strong,sub,sup,tt,u,var';

    config.basicEntities = false;
    config.fillEmptyBlocks = false;
    config.skin = 'moono';
    config.allowedContent = {
        h1: {
        },
        h2: {
        },
        h3: {
        },
        h4: {
        },
        h5: {
        },
        h6: {
        },
        b: {
        },
        i: {
        },
        u: {
        },
        p: {
        },
        ul: {
        },
        li: {
        },
        ol: {
        },
        strong: {
        },
        em: {
        },
        mark: {
        },
        cite: {
        },
        dfn: {
        }

    };

    config.removeFormatTags = 'b,big,code,del,dfn,em,font,i,ins,kbd,span';
    config.language = 'pt-br';
    config.toolbar = 'editor1';
    config.basicEntities = false;
    config.pasteFromWordRemoveFontStyles = false;
    config.pasteFromWordRemoveStyles = false;
    config.forcePasteAsPlainText = false;
    config.colorButton_colors = 'DFD1AB,B29C45';
    config.toolbar_editor1 =
        [
            { name: 'basicstyles', items: ['Bold', 'Italic', 'Underline', '-', 'RemoveFormat'] }
        ];


    config.format_p = {
        element: 'p',
        styles: {
            'margin': 0,
            'font-family': "'Segoe UI'",
            'font-size': '13px'
        }
    };

    config.format_h1 = {
        element: 'h1',
        styles: {
            'margin': 0,
            'font-family': "'Segoe UI'"
        }
    };

    config.format_h2 = {
        element: 'h2',
        styles: {
            'margin': 0,
            'font-family': "'Segoe UI'"
        }
    };

    config.format_h3 = {
        element: 'h3',
        styles: {
            'margin': 0,
            'font-family': "'Segoe UI'"
        }
    };

    config.format_h4 = {
        element: 'h4',
        styles: {
            'margin': 0,
            'font-family': "'Segoe UI'"
        }
    };

    config.format_pre = {
        element: 'pre',
        styles: {
            'margin': 0,
            'font-family': "'Segoe UI'"
        }
    };

    config.format_address = {
        element: 'address',
        styles: {
            'margin': 0,
            'font-family': "'Segoe UI'"
        }
    };

    config.format_h5 = {
        element: 'h5',
        styles: {
            'margin': 0,
            'font-family': "'Segoe UI'"
        }
    };

    config.format_div = {
        element: 'div',
        styles: {
            'margin': 0,
            'font-family': "'Segoe UI'"
        }
    };

    function getMaxLength(_self) {
        if (_self.element.$.attributes.maxlength !== 'undefined' && _self.element.$.attributes.maxlength != null) {
            return _self.element.$.attributes.maxlength.value;
        }

        if (_self.element.$.attributes.maxlengthckeditor !== 'undefined' && _self.element.$.attributes.maxlengthckeditor != null) {
            return _self.element.$.attributes.maxlengthckeditor.value;
        }

        return 8000;
    }

    config.wordcount = {
        countBytesAsChars: false,
        countLineBreaks: false,
        // Whether or not you want to show the Paragraphs Count
        showParagraphs: false,

        // Whether or not you want to show the Word Count
        showWordCount: false,

        // Whether or not you want to show the Char Count
        showCharCount: true,

        // Whether or not you want to count Spaces as Chars
        countSpacesAsChars: true,

        // Whether or not to include Html chars in the Char Count
        countHTML: false,

        // Maximum allowed Word Count, -1 is default for unlimited
        maxWordCount: -1,

        // Maximum allowed Char Count, -1 is default for unlimited
        maxCharCount: getMaxLength(self),

        // Add filter to add or remove element before counting (see CKEDITOR.htmlParser.filter), Default value : null (no filter)
        filter: new CKEDITOR.htmlParser.filter({
            elements: {
                div: function (element) {
                    if (element.attributes.class == 'mediaembed') {
                        return false;
                    }
                }
            }
        })
    };


    config.extraPlugins = 'wordcount,notification';
    //config.extraPlugins = 'charcount';
    //config.MaxLength = 8000;
    //config.charcount_limit = '1000';

};




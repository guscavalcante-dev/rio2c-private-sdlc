CKEDITOR.plugins.add('customtags',
{
    requires: ['richcombo'],
    init: function (editor) {
        var config = editor.config,
           lang = editor.lang.format;

        //customtags
        // Gets the list of tags from the settings.
        var tags = customtags;

        // Create style objects for all defined styles.
        editor.ui.addRichCombo('customtags',
           {
               label: "Tags",
               title: "Inserir tags",
               voiceLabel: "Inserir tags",
               className: 'cke_tag',
               multiSelect: false,
               panel:
               {
                   css: [CKEDITOR.skin.getPath('editor')].concat(config.contentsCss),
                   voiceLabel: lang.panelVoiceLabel,
                   attributes: { 'aria-label': lang.panelTitle }
               },

               init: function () {
                   this.startGroup("Selecione uma tag");
                   for (var this_tag in tags) {
                       if (tags.hasOwnProperty(this_tag)) {
                           this.add('##' + tags[this_tag][0] + '##', tags[this_tag][1], tags[this_tag][2]);
                       }                       
                   }
               },

               onClick: function (value) {
                   editor.focus();
                   editor.fire('saveSnapshot');
                   editor.insertHtml(value);
                   editor.setData(editor.getData());
                   editor.fire('saveSnapshot');
               }
           });
    }
});

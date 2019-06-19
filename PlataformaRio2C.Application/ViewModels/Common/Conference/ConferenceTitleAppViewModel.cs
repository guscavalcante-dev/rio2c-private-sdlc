using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using System.Web.Mvc;

namespace PlataformaRio2C.Application.ViewModels
{
    public class ConferenceTitleAppViewModel : EntityViewModel<ConferenceTitleAppViewModel, ConferenceTitle>, IEntityViewModel<ConferenceTitle>
    {
        [AllowHtml]
        public string Value { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageName { get; set; }

        public LanguageAppViewModel Language { get; set; }

        public ConferenceTitleAppViewModel()
            :base()
        {

        }

        public ConferenceTitleAppViewModel(ConferenceTitle entity)
            :base(entity)
        {            
            Value = entity.Value;

            LanguageCode = entity.Language.Code;
            LanguageName = entity.Language.Name;
        }


        public ConferenceTitle MapReverse()
        {
            var entity = new ConferenceTitle(Value, LanguageCode);

            return entity;
        }

        public ConferenceTitle MapReverse(ConferenceTitle entity)
        {
            return entity;
        }

    }
}

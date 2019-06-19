using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using System.Web.Mvc;

namespace PlataformaRio2C.Application.ViewModels
{
    public class ConferenceSynopsisAppViewModel : EntityViewModel<ConferenceSynopsisAppViewModel, ConferenceSynopsis>, IEntityViewModel<ConferenceSynopsis>
    {
        [AllowHtml]
        public string Value { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageName { get; set; }

        public LanguageAppViewModel Language { get; set; }


        public ConferenceSynopsisAppViewModel()
            :base()
        {

        }

        public ConferenceSynopsisAppViewModel(ConferenceSynopsis entity)
            : base(entity)
        {
            
            Value = entity.Value;

            LanguageCode = entity.Language.Code;
            LanguageName = entity.Language.Name;
        }


        public ConferenceSynopsis MapReverse()
        {
            var entity = new ConferenceSynopsis(Value, LanguageCode);

            return entity;
        }

        public ConferenceSynopsis MapReverse(ConferenceSynopsis entity)
        {
            return entity;
        }
    }
}

using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Application.ViewModels
{
    public class LanguageAppViewModel : EntityViewModel<LanguageAppViewModel, Language>, IEntityViewModel<Language>
    {        
        public string Name { get; set; }
        public string Code { get; set; }

        public LanguageAppViewModel()
        {

        }

        public LanguageAppViewModel(Language language)
        {
            Uid = language.Uid;
            CreationDate = language.CreationDate;
            Name = language.Name;
            Code = language.Code;
        }

        public LanguageAppViewModel(string languageCode)
        {
            Code = languageCode;
        }

        public Language MapReverse()
        {
            return new Language(this.Name, this.Code);
        }

        public Language MapReverse(Language entity)
        {
            entity.SetName(Name);
            return entity;
        }
    }
}

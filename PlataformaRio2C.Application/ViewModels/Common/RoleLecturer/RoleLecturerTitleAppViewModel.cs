using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.ViewModels
{
    public class RoleLecturerTitleAppViewModel : EntityViewModel<RoleLecturerTitleAppViewModel, RoleLecturerTitle>, IEntityViewModel<RoleLecturerTitle>
    {
        [Display(Name = "Title", ResourceType = typeof(Labels))]
        public string Value { get; set; }

        public string LanguageCode { get; set; }
        public string LanguageName { get; set; }

        public LanguageAppViewModel Language { get; set; }
        

        public RoleLecturerTitleAppViewModel()
        {
           
        }

        public RoleLecturerTitleAppViewModel(RoleLecturerTitle entity)
        {
            CreationDate = entity.CreationDate;
            Uid = entity.Uid;
            Value = entity.Value;

            LanguageCode = entity.Language.Code;
            LanguageName = entity.Language.Name;
        }


        public void SetLanguage(LanguageAppViewModel language)
        {
            Language = language;
        }

        public RoleLecturerTitle MapReverse()
        {
            var entity = new RoleLecturerTitle(Value, LanguageCode);

            return entity;
        }

        public RoleLecturerTitle MapReverse(RoleLecturerTitle entity)
        {
            return entity;
        }
    }
}

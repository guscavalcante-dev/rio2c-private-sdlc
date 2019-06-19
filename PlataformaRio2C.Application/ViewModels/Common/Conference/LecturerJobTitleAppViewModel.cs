using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.ViewModels
{
    public class LecturerJobTitleAppViewModel : EntityViewModel<LecturerJobTitleAppViewModel, LecturerJobTitle>, IEntityViewModel<LecturerJobTitle>
    {
        [Display(Name = "JobTitle", ResourceType = typeof(Labels))]
        public string Value { get; set; }

        public string LanguageCode { get; set; }
        public string LanguageName { get; set; }

        public LanguageAppViewModel Language { get; set; }
        

        public LecturerJobTitleAppViewModel()
        {
           
        }

        public LecturerJobTitleAppViewModel(LecturerJobTitle entity)
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

        public LecturerJobTitle MapReverse()
        {
            var entity = new LecturerJobTitle(Value, LanguageCode);

            return entity;
        }

        public LecturerJobTitle MapReverse(LecturerJobTitle entity)
        {
            return entity;
        }
    }
}

using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.ViewModels
{
    public class CollaboratorJobTitleAppViewModel : EntityViewModel<CollaboratorJobTitleAppViewModel, CollaboratorJobTitle>, IEntityViewModel<CollaboratorJobTitle>
    {
        [Display(Name = "JobTitle", ResourceType = typeof(Labels))]
        public string Value { get; set; }

        public string LanguageCode { get; set; }
        public string LanguageName { get; set; }

        public LanguageAppViewModel Language { get; set; }

        public CollaboratorAppViewModel Collaborator { get; set; }

        public CollaboratorJobTitleAppViewModel()
        {
           
        }

        public CollaboratorJobTitleAppViewModel(CollaboratorJobTitle entity)
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

        public void SetHolding(CollaboratorAppViewModel collaborator)
        {
            Collaborator = collaborator;
        }

        public CollaboratorJobTitle MapReverse()
        {
            var entity = new CollaboratorJobTitle(Value, LanguageCode);

            return entity;
        }

        public CollaboratorJobTitle MapReverse(CollaboratorJobTitle entity)
        {
            return entity;
        }
    }
}

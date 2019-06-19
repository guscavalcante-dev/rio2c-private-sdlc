using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PlataformaRio2C.Application.ViewModels
{
    public class CollaboratorMiniBioAppViewModel : EntityViewModel<CollaboratorMiniBioAppViewModel, CollaboratorMiniBio>, IEntityViewModel<CollaboratorMiniBio>
    {
        [AllowHtml]
        [Display(Name = "MiniBio", ResourceType = typeof(Labels))]
        public string Value { get; set; }

        public string LanguageCode { get; set; }
        public string LanguageName { get; set; }

        public LanguageAppViewModel Language { get; set; }

        public CollaboratorAppViewModel Collaborator { get; set; }

        public CollaboratorMiniBioAppViewModel()
        {

        }

        public CollaboratorMiniBioAppViewModel(CollaboratorMiniBio entity)
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

        public CollaboratorMiniBio MapReverse()
        {
            var entity = new CollaboratorMiniBio(Value, LanguageCode);

            return entity;
        }

        public CollaboratorMiniBio MapReverse(CollaboratorMiniBio entity)
        {
            return entity;
        }
    }
}

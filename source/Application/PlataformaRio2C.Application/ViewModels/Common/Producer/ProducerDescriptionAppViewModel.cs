using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using System.Web.Mvc;

namespace PlataformaRio2C.Application.ViewModels
{
    public class ProducerDescriptionAppViewModel : EntityViewModel<ProducerDescriptionAppViewModel, ProducerDescription>, IEntityViewModel<ProducerDescription>
    {
        [AllowHtml]
        public string Value { get; set; }

        public string LanguageCode { get; set; }

        public string LanguageName{ get; set; }

        public  LanguageAppViewModel Language { get; set; }       

        public ProducerDescriptionAppViewModel()
        {

        }

        public ProducerDescriptionAppViewModel(ProducerDescription entity)
        {
            CreationDate = entity.CreationDate;
            Uid = entity.Uid;
            Value = entity.Value;

            LanguageCode = entity.Language.Code;
            LanguageName = entity.Language.Name;
        }

        public ProducerDescription MapReverse()
        {
            var producerDescription = new ProducerDescription(Value, LanguageCode);

            return producerDescription;
        }

        public void SetLanguage(LanguageAppViewModel language)
        {
            Language = language;
        }
      

        public ProducerDescription MapReverse(ProducerDescription entity)
        {
            return entity;
        }
    }
}

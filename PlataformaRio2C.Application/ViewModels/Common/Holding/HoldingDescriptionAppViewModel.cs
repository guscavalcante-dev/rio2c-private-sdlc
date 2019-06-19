using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using System.Web.Mvc;

namespace PlataformaRio2C.Application.ViewModels
{
    public class HoldingDescriptionAppViewModel : EntityViewModel<HoldingDescriptionAppViewModel, HoldingDescription>, IEntityViewModel<HoldingDescription>
    {
        [AllowHtml]
        public string Value { get; set; }
        public string LanguageCode { get; set; }

        public  LanguageAppViewModel Language { get; set; }
        public  HoldingAppViewModel Holding { get; set; }

        public HoldingDescriptionAppViewModel()
        {

        }

        public HoldingDescriptionAppViewModel(Domain.Entities.HoldingDescription entity)
        {
            CreationDate = entity.CreationDate;
            Uid = entity.Uid;
            Value = entity.Value;

            LanguageCode = entity.Language.Code;
            //Language = new LanguageAppViewModel(entity.Language);
            //Holding = new HoldingAppViewModel(entity.Holding);
        }

        public HoldingDescription MapReverse()
        {
            var holdingDescription = new HoldingDescription(Value, LanguageCode);

            return holdingDescription;
        }

        public void SetLanguage(LanguageAppViewModel language)
        {
            Language = language;
        }

        public void SetHolding(HoldingAppViewModel holding)
        {
            Holding = holding;
        }

        public HoldingDescription MapReverse(HoldingDescription entity)
        {
            return entity;
        }
    }
}

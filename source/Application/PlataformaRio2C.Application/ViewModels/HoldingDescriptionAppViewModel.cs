using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Application.ViewModels
{
    public class HoldingDescriptionAppViewModel : EntityViewModel<HoldingDescriptionAppViewModel, HoldingDescription>, IEntityViewModel<HoldingDescription>
    {
        public string Value { get; set; }
        public string LanguageCode { get; set; }

        public  LanguageAppViewModel Language { get; set; }
        public  HoldingAppViewModel Holding { get; set; }

        public HoldingDescriptionAppViewModel()
        {

        }

        public HoldingDescriptionAppViewModel(Domain.Entities.HoldingDescription entity)
        {
            Uid = entity.Uid;
            Value = entity.Value;

            LanguageCode = entity.Language.Code;
            //Language = new LanguageAppViewModel(entity.Language);
            //Holding = new HoldingAppViewModel(entity.Holding);
        }

        public HoldingDescription MapReverse()
        {
            if (this == null) return null;

            var holdingDescription = new HoldingDescription(Value, Language.MapReverse());

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

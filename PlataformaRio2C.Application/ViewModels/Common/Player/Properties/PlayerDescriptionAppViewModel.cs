using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using System.Web.Mvc;

namespace PlataformaRio2C.Application.ViewModels
{
    public class PlayerDescriptionAppViewModel : EntityViewModel<PlayerDescriptionAppViewModel, PlayerDescription>, IEntityViewModel<PlayerDescription>
    {
        [AllowHtml]
        public string Value { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageName{ get; set; }

        public  LanguageAppViewModel Language { get; set; }
        public  HoldingAppViewModel Holding { get; set; }

        public PlayerDescriptionAppViewModel()
        {

        }

        public PlayerDescriptionAppViewModel(Domain.Entities.PlayerDescription entity)
        {
            CreationDate = entity.CreationDate;
            Uid = entity.Uid;
            Value = entity.Value;

            LanguageCode = entity.Language.Code;
            LanguageName = entity.Language.Name;
        }

        public PlayerDescription MapReverse()
        {
            var holdingDescription = new PlayerDescription(Value, LanguageCode);

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

        public PlayerDescription MapReverse(PlayerDescription entity)
        {
            return entity;
        }
    }
}

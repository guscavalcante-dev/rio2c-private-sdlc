//using PlataformaRio2C.Domain.Entities;
//using PlataformaRio2C.Infra.CrossCutting.Resources;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;

//namespace PlataformaRio2C.Application.ViewModels
//{
//    public class PlayerEditAppViewModel : PlayerBasicAppViewModel
//    {
//        #region props

//        public IEnumerable<LanguageAppViewModel> LanguagesOptions { get; set; }

//        [Display(Name = "Holdings", ResourceType = typeof(Labels))]
//        public IEnumerable<HoldingItemListAppViewModel> HoldingsOptions { get; set; }

//        [Display(Name = "Activity", ResourceType = typeof(Labels))]
//        public IEnumerable<PlayerActivityAppViewModel> Activitys { get; set; }

//        [Display(Name = "TargetAudience", ResourceType = typeof(Labels))]
//        public IEnumerable<PlayerTargetAudienceAppViewModel> TargetAudience { get; set; }
//        public HoldingOptionAppViewModel Holding { get; set; }

//        public IEnumerable<Country> Countries { get; set; }
//        public IEnumerable<State> States { get; set; }
//        public IEnumerable<City> Cities { get; set; }
//        public int CountryId { get; set; }
//        public int StateId { get; set; }
//        public int CityId { get; set; }
//        #endregion

//        #region ctor
//        public PlayerEditAppViewModel()
//            : base()
//        {
//            LanguagesOptions = new List<LanguageAppViewModel>();
//            HoldingsOptions = new List<HoldingItemListAppViewModel>();
//            Activitys = new List<PlayerActivityAppViewModel>();
//            TargetAudience = new List<PlayerTargetAudienceAppViewModel>();
//        }

//        public PlayerEditAppViewModel(Player entity)
//            :base(entity)
//        {
//            if (entity.Holding != null)
//            {
//                Holding = new HoldingOptionAppViewModel(entity.Holding);
//            }

//            if (entity.Image != null)
//            {
//                Image = new ImageFileAppViewModel(entity.Image);
//            }
//        }

//        #endregion
//    }
//}

//using PlataformaRio2C.Domain.Entities;
//using PlataformaRio2C.Infra.CrossCutting.Resources;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;

//namespace PlataformaRio2C.Application.ViewModels
//{
//    public class PlayerAppViewModel : PlayerBasicAppViewModel
//    {      
//        public HoldingAppViewModel Holding { get; set; }

//        public IEnumerable<LanguageAppViewModel> LanguagesOptions { get; set; }

//        public IEnumerable<PlayerInterestAppViewModel> Interests { get; set; }      

//        [Display(Name = "Activity", ResourceType = typeof(Labels))]
//        public IEnumerable<PlayerActivityAppViewModel> Activitys { get; set; }

//        [Display(Name = "TargetAudience", ResourceType = typeof(Labels))]
//        public IEnumerable<PlayerTargetAudienceAppViewModel> TargetAudience { get; set; }

//        public PlayerAppViewModel()
//            :base()
//        {
//            Interests = new List<PlayerInterestAppViewModel>();           
//            Activitys = new List<PlayerActivityAppViewModel>();
//            TargetAudience = new List<PlayerTargetAudienceAppViewModel>();
//            LanguagesOptions = new List<LanguageAppViewModel>();
//        }

//        public PlayerAppViewModel(Player player)
//            :base(player)
//        {
                    
//        }      
//    }
//}

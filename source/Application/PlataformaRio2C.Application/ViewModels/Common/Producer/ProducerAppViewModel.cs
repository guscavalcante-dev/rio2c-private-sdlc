//using PlataformaRio2C.Application.Common;
//using PlataformaRio2C.Domain.Entities;
//using PlataformaRio2C.Infra.CrossCutting.Resources;
//using PlataformaRio2C.Infra.CrossCutting.Tools.Attributes;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Web;

//namespace PlataformaRio2C.Application.ViewModels
//{
//    public class ProducerAppViewModel : ProducerBasicAppViewModel
//    {
//        public IEnumerable<LanguageAppViewModel> LanguagesOptions { get; set; }

//        public IEnumerable<PlayerInterestAppViewModel> Interests { get; set; }

//        [Display(Name = "Activity", ResourceType = typeof(Labels))]
//        public IEnumerable<PlayerActivityAppViewModel> Activitys { get; set; }

//        [Display(Name = "TargetAudience", ResourceType = typeof(Labels))]
//        public IEnumerable<PlayerTargetAudienceAppViewModel> TargetAudience { get; set; }


//        public ProducerAppViewModel()
//            :base()
//        {
//            Interests = new List<PlayerInterestAppViewModel>();
//            Activitys = new List<PlayerActivityAppViewModel>();
//            TargetAudience = new List<PlayerTargetAudienceAppViewModel>();
//            LanguagesOptions = new List<LanguageAppViewModel>();
//        }

//        public ProducerAppViewModel(Producer entity)
//            :base(entity)
//        {

//        }
//    }
//}

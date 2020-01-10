//using PlataformaRio2C.Domain.Entities;
//using PlataformaRio2C.Infra.CrossCutting.Resources;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;

//namespace PlataformaRio2C.Application.ViewModels
//{
//    public class ProducerEditAppViewModel: ProducerBasicAppViewModel
//    {
//        #region props

//        public IEnumerable<LanguageAppViewModel> LanguagesOptions { get; set; }

//        [Display(Name = "Activity", ResourceType = typeof(Labels))]
//        public IEnumerable<ProducerActivityAppViewModel> Activitys { get; set; }

//        [Display(Name = "TargetAudience", ResourceType = typeof(Labels))]
//        public IEnumerable<ProducerTargetAudienceAppViewModel> TargetAudience { get; set; }

//        public IEnumerable<Country> Countries { get; set; }
//        public IEnumerable<State> States { get; set; }
//        public IEnumerable<City> Cities { get; set; }
//        public int CountryId { get; set; }
//        public int StateId { get; set; }
//        public int CityId { get; set; }

//        #endregion

//        #region ctor

//        public ProducerEditAppViewModel()
//            :base()
//        {
//            LanguagesOptions = new List<LanguageAppViewModel>();
//            Activitys = new List<ProducerActivityAppViewModel>();
//            TargetAudience = new List<ProducerTargetAudienceAppViewModel>();
//        }

//        public ProducerEditAppViewModel(Producer entity)
//            :base(entity)
//        {
//            if (entity.Image != null)
//            {
//                Image = new ImageFileAppViewModel(entity.Image);
//            }
//        }

//        #endregion
//    }
//}

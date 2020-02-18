//using PlataformaRio2C.Application.Common;
//using PlataformaRio2C.Domain.Entities;
//using PlataformaRio2C.Infra.CrossCutting.Resources;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace PlataformaRio2C.Application.ViewModels.Common
//{
//    public class CountryBasicAppViewModel : EntityViewModel<CountryBasicAppViewModel, Country>, IEntityViewModel<Country>
//    {
//        [Display(Name = "CountryName", ResourceType = typeof(Labels))]
//        public string CountryName { get; set; }

//        [Display(Name = "CountryCode", ResourceType = typeof(Labels))]
//        public string CountryCode { get; set; }

//        public CountryBasicAppViewModel()
//        {
//        }

//        public CountryBasicAppViewModel(CountryBasicAppViewModel entity)
//        {
//            CountryName = entity.CountryName;
//            CountryCode = entity.CountryCode;
//        }

//        public Country MapReverse()
//        {
//            throw new NotImplementedException();
//        }

//        public Country MapReverse(Country entity)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}

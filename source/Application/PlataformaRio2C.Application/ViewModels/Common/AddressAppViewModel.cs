//using PlataformaRio2C.Application.Common;
//using PlataformaRio2C.Domain.Entities;
//using PlataformaRio2C.Infra.CrossCutting.Resources;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;

//namespace PlataformaRio2C.Application.ViewModels
//{
//    public class AddressAppViewModel : EntityViewModel<AddressAppViewModel, Address>, IEntityViewModel<Address>
//    {
//        //public static readonly int AddressValueMaxLength = Address.AddressValueMaxLength;

//        [Display(Name = "ZipCode", ResourceType = typeof(Labels))]
//        public string ZipCode { get; set; }
//        [Display(Name = "Country", ResourceType = typeof(Labels))]
//        public int? Country { get; set; }
//        [Display(Name = "State", ResourceType = typeof(Labels))]
//        public string State { get; set; }
//        [Display(Name = "City", ResourceType = typeof(Labels))]
//        public string City { get; set; }
//        [Display(Name = "Address", ResourceType = typeof(Labels))]
//        public string AddressValue { get; set; }

//        [Display(Name = "Country", ResourceType = typeof(Labels))]
//        public int? CountryId { get; set; }
//        [Display(Name = "State", ResourceType = typeof(Labels))]
//        public int? StateId { get; set; }
//        [Display(Name = "City", ResourceType = typeof(Labels))]
//        public int? CityId { get; set; }

//        public AddressAppViewModel()
//        {

//        }

//        public AddressAppViewModel(Address entity)
//        {
//            //Uid = entity.Uid;
//            //ZipCode = entity.ZipCode;
//            //Country = entity.CountryId;
//            //State = entity.State;
//            //City = entity.City;
//            //AddressValue = entity.AddressValue;

//            //CountryId = entity.CountryId;
//            //CityId = entity.CityId;
//            //StateId = entity.StateId;
//        }       

//        public Address MapReverse()
//        {
//            int countryid = (CountryId == null ? 0 : (int)CountryId);
//            int stateid = (StateId == null ? 0 : (int)StateId);
//            int cityid = (CityId == null ? 0 : (int)CityId);

//            //var address = new Address(this.ZipCode);
//            //address.SetCountry(countryid);
//            //address.SetState(stateid); 
//            //address.SetCity(cityid);           
//            //address.SetAddressValue(AddressValue);

//            //return address;

//            return null;
//        }

//        public Address MapReverse(Address entity)
//        {
//            //int stateid = (int)entity.StateId;
//            //int cityid = (int)entity.CityId;
//            //int countryid = (int)entity.CountryId;

//            //entity.SetZipCode(ZipCode);
//            //entity.SetCountry(countryid);
//            //entity.SetState(stateid);
//            //entity.SetCity(cityid);
//            //entity.SetAddressValue(AddressValue);

//            return entity;
//        }
//    }
//}

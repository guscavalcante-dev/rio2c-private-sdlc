using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Entities
{
    public class Address : Entity
    {
        public static readonly int NameMinLength = 2;
        public static readonly int NameMaxLength = 50;
        public static readonly int AddressValueMaxLength = 1000;

        public string ZipCode { get; private set; }
        public string Country { get; private set; }
        public string State { get; private set; }
        public string City { get; private set; }
        public string AddressValue { get; private set; }

        public int? CountryId { get; private set; }
        public int? StateId { get; private set; }
        public int? CityId { get; private set; }

        public virtual ICollection<Country> Countries { get; set; }
        public virtual ICollection<State> States { get; set; }
        public virtual ICollection<City> Cities { get; set; }

        public Address()
        {

        }

        //public Address() { }

        public Address(string zipCode)
        {
            SetZipCode(zipCode);
        }

        public void SetZipCode(string zipCode)
        {
            ZipCode = zipCode;
        }

        public void SetCountry(string country)
        {
            Country = country;
        }


        public void SetCountry(int country)
        {
            CountryId = country;
        }

        public void SetState(string state)
        {
            State = state;
        }

        public void SetState(int state)
        {
            StateId = state;
        }

        public void SetCity(string city)
        {
            City = city;
        }

        public void SetCity(int city)
        {
            CityId = city;
        }

        public void SetAddressValue(string addressValue)
        {
            AddressValue = addressValue;
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}

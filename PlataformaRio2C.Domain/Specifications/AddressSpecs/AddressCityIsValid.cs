using PlataformaRio2C.Domain.Enums;
using PlataformaRio2C.Domain.Interfaces;
using System.Text.RegularExpressions;

namespace PlataformaRio2C.Domain.Entities.Specifications
{
    public class AddressCityIsValid : ISpecification<Address>
    {
        public string Target { get { return "Address.City"; } }
        public ErrorCodes Code { get { return ErrorCodes.IsInvalid; } }

        public bool IsSatisfiedBy(Address entity)
        {
            if (entity == null) return false;

            if (entity.CountryId != 30)
                return !string.IsNullOrWhiteSpace(entity.City) && WordValid(entity.City);
            else
                return (int)entity.CityId != 0;            
        }

        bool WordValid(string value)
        {
            Regex rgx = new Regex(@"[a-zA-Z]{2,}");
            return !string.IsNullOrWhiteSpace(value) && rgx.IsMatch(value);
        }
    }
}

using PlataformaRio2C.Domain.Enums;
using PlataformaRio2C.Domain.Interfaces;
using System.Text.RegularExpressions;

namespace PlataformaRio2C.Domain.Entities.Specifications
{
    public class AddressStateIsValid : ISpecification<Address>
    {
        public string Target { get { return "Address.State"; } }
        public ErrorCodes Code { get { return ErrorCodes.IsInvalid; } }

        public bool IsSatisfiedBy(Address entity)
        {
            if (entity == null) return false;

            if (entity.CountryId != 30)
                return !string.IsNullOrWhiteSpace(entity.State) && WordValid(entity.State);
            else
                return (int)entity.StateId != 0;            
        }

        bool WordValid(string value)
        {
            Regex rgx = new Regex(@"[a-zA-Z]{2,}");
            return !string.IsNullOrWhiteSpace(value) && rgx.IsMatch(value);
        }
    }
}

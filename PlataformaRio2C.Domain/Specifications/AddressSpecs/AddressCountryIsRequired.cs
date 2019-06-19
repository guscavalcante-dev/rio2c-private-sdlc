using PlataformaRio2C.Domain.Enums;
using PlataformaRio2C.Domain.Interfaces;
using System.Text.RegularExpressions;

namespace PlataformaRio2C.Domain.Entities.Specifications
{
    public class AddressCountryIsRequired : ISpecification<Address>
    {
        public string Target { get { return "Address.Country"; } }
        public ErrorCodes Code { get { return ErrorCodes.IsRequired; } }

        public bool IsSatisfiedBy(Address entity)
        {
            if (entity == null) return false;

            //return !string.IsNullOrWhiteSpace(entity.Country);
            double num = 0;

            return !double.TryParse(entity.Country, out num);
            //return !int.(entity.Country);
        }
    }
}

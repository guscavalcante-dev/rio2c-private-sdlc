using PlataformaRio2C.Domain.Enums;
using PlataformaRio2C.Domain.Interfaces;

namespace PlataformaRio2C.Domain.Entities.Specifications
{
    public class AddressCityIsRequired : ISpecification<Address>
    {
        public string Target { get { return "Address.City"; } }
        public ErrorCodes Code { get { return ErrorCodes.IsRequired; } }

        public bool IsSatisfiedBy(Address entity)
        {
            if (entity == null) return false;

            if (entity.CountryId != 30)
                return !string.IsNullOrWhiteSpace(entity.City);
            else
                return (int)entity.CityId != 0;
        }       
    }
}

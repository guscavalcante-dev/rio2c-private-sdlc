using PlataformaRio2C.Domain.Enums;
using PlataformaRio2C.Domain.Interfaces;

namespace PlataformaRio2C.Domain.Entities.Specifications
{
    public class AddressValueIsRequired : ISpecification<Address>
    {
        public string Target { get { return "Address.AddressValue"; } }
        public ErrorCodes Code { get { return ErrorCodes.IsRequired; } }

        public bool IsSatisfiedBy(Address entity)
        {
            if (entity == null) return false;

            return !string.IsNullOrWhiteSpace(entity.AddressValue);
        }       
    }
}

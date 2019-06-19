using PlataformaRio2C.Domain.Enums;
using PlataformaRio2C.Domain.Interfaces;

namespace PlataformaRio2C.Domain.Entities.Specifications
{
    public class NegotiationDateIsRequired : ISpecification<Negotiation>
    {
        public string Target { get { return "Date"; } }
        public ErrorCodes Code { get { return ErrorCodes.IsRequired; } }

        public bool IsSatisfiedBy(Negotiation entity)
        {
            return entity.Date != null;
        }
    }
}

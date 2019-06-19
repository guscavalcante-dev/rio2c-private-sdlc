using PlataformaRio2C.Domain.Enums;
using PlataformaRio2C.Domain.Interfaces;

namespace PlataformaRio2C.Domain.Entities.Specifications
{
    public class NegotiationRoomConfigCountAutomaticTablesIsInvalid : ISpecification<NegotiationRoomConfig>
    {
        public string Target { get { return "Date"; } }
        public ErrorCodes Code { get { return ErrorCodes.IsRequired; } }

        public bool IsSatisfiedBy(NegotiationRoomConfig entity)
        {
            return entity.CountAutomaticTables >= 0;
        }
    }
}

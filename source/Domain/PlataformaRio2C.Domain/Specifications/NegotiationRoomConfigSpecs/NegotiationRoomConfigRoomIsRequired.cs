using PlataformaRio2C.Domain.Enums;
using PlataformaRio2C.Domain.Interfaces;

namespace PlataformaRio2C.Domain.Entities.Specifications
{
    public class NegotiationRoomConfigRoomIsRequired : ISpecification<NegotiationRoomConfig>
    {
        public string Target { get { return "Room"; } }
        public ErrorCodes Code { get { return ErrorCodes.IsRequired; } }

        public bool IsSatisfiedBy(NegotiationRoomConfig entity)
        {
            return entity.Room != null;
        }
    }
}

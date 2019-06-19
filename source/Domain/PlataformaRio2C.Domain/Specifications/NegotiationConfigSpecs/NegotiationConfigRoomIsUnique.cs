using PlataformaRio2C.Domain.Enums;
using PlataformaRio2C.Domain.Interfaces;
using System.Linq;

namespace PlataformaRio2C.Domain.Entities.Specifications
{
    public class NegotiationConfigRoomIsUnique : ISpecification<NegotiationConfig>
    {
        public string Target { get { return "Room"; } }
        public ErrorCodes Code { get { return ErrorCodes.IsRequired; } }

        public bool IsSatisfiedBy(NegotiationConfig entity)
        {            
            return entity.Rooms != null && entity.Rooms.Any() && !(entity.Rooms.GroupBy(e => e.RoomId).Any(e => e.Count() > 1));
        }
    }
}

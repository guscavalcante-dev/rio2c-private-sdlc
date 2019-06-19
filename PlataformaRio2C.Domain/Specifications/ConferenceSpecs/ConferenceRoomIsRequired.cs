using PlataformaRio2C.Domain.Enums;
using PlataformaRio2C.Domain.Interfaces;

namespace PlataformaRio2C.Domain.Entities.Specifications
{
    public class ConferenceRoomIsRequired : ISpecification<Conference>
    {
        public string Target { get { return "Room"; } }
        public ErrorCodes Code { get { return ErrorCodes.IsRequired; } }

        public bool IsSatisfiedBy(Conference entity)
        {
            return entity.Room != null && entity.RoomId > 0;
        }
    }
}

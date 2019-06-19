using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    public class RoomNameRepository : Repository<PlataformaRio2CContext, RoomName>, IRoomNameRepository
    {
        public RoomNameRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }
    }
}

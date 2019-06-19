using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    public class PlayerTargetAudienceRepository : Repository<PlataformaRio2CContext, PlayerTargetAudience>, IPlayerTargetAudienceRepository
    {
        public PlayerTargetAudienceRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }
    }
}

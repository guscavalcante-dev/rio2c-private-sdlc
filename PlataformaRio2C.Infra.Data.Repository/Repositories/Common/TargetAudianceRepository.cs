using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    public class TargetAudienceRepository : Repository<PlataformaRio2CContext, TargetAudience>, ITargetAudienceRepository
    {
        public TargetAudienceRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }
    }
}

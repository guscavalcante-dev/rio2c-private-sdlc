using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    public class ActivityRepository : Repository<PlataformaRio2CContext, Activity>, IActivityRepository
    {
        public ActivityRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }
    }
}

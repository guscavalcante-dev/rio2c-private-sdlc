using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    public class StateRepository : Repository<PlataformaRio2CContext, State>, IStateRepository
    {
        public StateRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }
    }
}

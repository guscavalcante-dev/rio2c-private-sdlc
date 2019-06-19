using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    public class CollaboratorMiniBioRepository : Repository<PlataformaRio2CContext, CollaboratorMiniBio>, ICollaboratorMiniBioRepository
    {
        public CollaboratorMiniBioRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }
    }
}

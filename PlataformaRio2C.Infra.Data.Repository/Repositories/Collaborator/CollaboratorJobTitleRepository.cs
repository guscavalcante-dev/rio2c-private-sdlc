using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    public class CollaboratorJobTitleRepository : Repository<PlataformaRio2CContext, CollaboratorJobTitle>, ICollaboratorJobTitleRepository
    {
        public CollaboratorJobTitleRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }
    }
}

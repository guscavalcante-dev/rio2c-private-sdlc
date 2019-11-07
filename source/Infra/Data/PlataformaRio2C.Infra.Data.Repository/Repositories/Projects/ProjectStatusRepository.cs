using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    public class ProjectStatusRepository : Repository<PlataformaRio2CContext, ProjectStatus>, IProjectStatusRepository
    {
        public ProjectStatusRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }
    }
}

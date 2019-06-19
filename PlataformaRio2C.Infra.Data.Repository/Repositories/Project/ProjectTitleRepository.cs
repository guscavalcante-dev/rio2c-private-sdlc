using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    public class ProjectTitleRepository : Repository<PlataformaRio2CContext, ProjectTitle>, IProjectTitleRepository
    {
        public ProjectTitleRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }
    }
}

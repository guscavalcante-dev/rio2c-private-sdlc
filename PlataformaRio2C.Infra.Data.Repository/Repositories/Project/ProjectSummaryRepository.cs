using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    public class ProjectSummaryRepository : Repository<PlataformaRio2CContext, ProjectSummary>, IProjectSummaryRepository
    {
        public ProjectSummaryRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }
    }
}

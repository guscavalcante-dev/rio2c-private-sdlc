using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    public class ProjectPlayerEvaluationRepository : Repository<PlataformaRio2CContext, ProjectPlayerEvaluation>, IProjectPlayerEvaluationRepository
    {
        public ProjectPlayerEvaluationRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }
    }
}

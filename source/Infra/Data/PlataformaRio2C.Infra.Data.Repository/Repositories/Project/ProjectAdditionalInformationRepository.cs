using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    public class ProjectAdditionalInformationRepository : Repository<PlataformaRio2CContext, ProjectAdditionalInformation>, IProjectAdditionalInformationRepository
    {
        public ProjectAdditionalInformationRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }
    }
}

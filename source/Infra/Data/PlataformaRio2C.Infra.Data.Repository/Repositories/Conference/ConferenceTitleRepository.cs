using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    public class ConferenceTitleRepository : Repository<PlataformaRio2CContext, ConferenceTitle>, IConferenceTitleRepository
    {
        public ConferenceTitleRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }
    }
}

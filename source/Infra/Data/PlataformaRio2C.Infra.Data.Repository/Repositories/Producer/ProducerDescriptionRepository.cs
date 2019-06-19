using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    public class ProducerDescriptionRepository : Repository<PlataformaRio2CContext, ProducerDescription>, IProducerDescriptionRepository
    {
        public ProducerDescriptionRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }      
    }
}

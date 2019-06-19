using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    public class CityRepository : Repository<PlataformaRio2CContext, City>, ICityRepository
    {
        public CityRepository(PlataformaRio2CContext context)
            : base(context) { }
    }
}
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    public class AddressRepository : Repository<PlataformaRio2CContext, Address>, IAddressRepository
    {
        public AddressRepository(PlataformaRio2CContext context)
            : base(context) { }
    }
}

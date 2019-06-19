using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    public class RoleRepository : Repository<PlataformaRio2CContext, Role>, IRoleRepository
    {
        private PlataformaRio2CContext _context;

        public RoleRepository(PlataformaRio2CContext context)
            : base(context)
        {
            _context = context;
        }
    }
}

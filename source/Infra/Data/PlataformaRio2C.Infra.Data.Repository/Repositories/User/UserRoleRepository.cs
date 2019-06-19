using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    public class UserRoleRepository : Repository<PlataformaRio2CContext, UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }
    }
}

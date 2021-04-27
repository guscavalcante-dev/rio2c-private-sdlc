using PlataformaRio2C.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Interfaces
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<List<Role>> FindAllAdminRolesAsync();

        Task<List<Role>> FindByNameAsync(string roleName);
    }    
}

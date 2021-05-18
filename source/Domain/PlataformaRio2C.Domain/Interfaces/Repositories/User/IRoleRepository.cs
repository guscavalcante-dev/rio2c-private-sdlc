using PlataformaRio2C.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Interfaces
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<List<Role>> FindAllAdminRolesAsync();
        List<Role> FindAllAdminRoles();

        Task<List<Role>> FindByNameAsync(string roleName);
    }    
}

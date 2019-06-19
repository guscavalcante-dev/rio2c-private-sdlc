using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PlataformaRio2C.Infra.CrossCutting.Identity.Models
{
    public class CustomRoleStore<U> : RoleStore<CustomRole, int, CustomUserRole>, IRoleStore<CustomRole, int>
        where U : User
    {
        public CustomRoleStore(IdentityDbContext<U, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim> context)
            : base(context)
        {
        }
    }
}

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PlataformaRio2C.Infra.CrossCutting.Identity.Models
{
    public class CustomUserStore<U> : UserStore<U, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>, IUserStore<U, int>
        where U : User
    {
        public CustomUserStore(IdentityDbContext<U, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim> context)
            : base(context)
        {
        }
    }
}

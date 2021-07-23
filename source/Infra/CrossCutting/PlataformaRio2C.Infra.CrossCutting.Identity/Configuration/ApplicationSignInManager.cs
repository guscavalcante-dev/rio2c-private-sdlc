using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using PlataformaRio2C.Infra.CrossCutting.Identity.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PlataformaRio2C.Infra.CrossCutting.Identity.Configuration
{
    public class ApplicationSignInManager<U> : SignInManager<U, int>
        where U : User
    {
        public ApplicationSignInManager(ApplicationUserManager<U> userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(U user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager<U>)UserManager);
        }

        public static ApplicationSignInManager<U> Create(IdentityFactoryOptions<ApplicationSignInManager<U>> options, IOwinContext context)
        {
            return new ApplicationSignInManager<U>(context.GetUserManager<ApplicationUserManager<U>>(), context.Authentication);
        }
    }
}

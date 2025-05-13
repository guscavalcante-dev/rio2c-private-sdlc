using Microsoft.Owin.Security.OAuth;
using PlataformaRio2C.Infra.CrossCutting.Identity.Models;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace PlataformaRio2C.WebApi.Providers
{
    public class AuthorizationApiServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            var identityController = (IdentityAutenticationService)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IdentityAutenticationService));

            try
            {
                var userEmail = context.UserName;
                var password = context.Password;

                var user = AsyncHelpers.RunSync<ApplicationUser>(() => identityController.FindByEmailAsync(userEmail));
                if (user == null)
                {
                    context.SetError("invalid_grant", "Usuário ou senha inválidos");
                    return;
                }
                else
                {
                    var result = await identityController.PasswordSignInAsync(userEmail, password, true, true);

                    if (result == IdentitySignInStatus.Success)
                    {
                        var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                        identity.AddClaim(new Claim(ClaimTypes.Name, userEmail));

                        //foreach (var role in user.Roles)
                        //{
                        //    identity.AddClaim(new Claim(ClaimTypes.Role, role));
                        //}

                        //GenericPrincipal principal = new GenericPrincipal(identity, user.Roles);
                        //Thread.CurrentPrincipal = principal;

                        context.Validated(identity);
                    }
                    else
                    {
                        context.SetError("invalid_grant", "Usuário ou senha inválidos");
                        return;
                    }
                }
            }
            catch (Exception e)
            {
                context.SetError("invalid_grant", "Falha ao autenticar");
            }
        }
    }
}
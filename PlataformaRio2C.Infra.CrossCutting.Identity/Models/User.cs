using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PlataformaRio2C.Infra.CrossCutting.Identity.Models
{
    public class User : IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>, IUser<int>
    {
        [StringLength(150)]
        [Required]
        public string Name { get; set; }

        public virtual async Task<ClaimsIdentity> GenerateUserIdentityAsync<U>(UserManager<U, int> manager, ClaimsIdentity ext = null)
            where U : User
        {
            // Observe que o authenticationType precisa ser o mesmo que foi definido em CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync((U)this, DefaultAuthenticationTypes.ApplicationCookie);

            // Adicionando Claims externos capturados no login
            if (ext != null)
            {
                var claims = new List<Claim>();
                SetExternalProperties(userIdentity, ext);
                userIdentity.AddClaims(claims);
            }

            //Adicionando Claims extras
            SetClaims(userIdentity);

            return userIdentity;
        }

        /// <summary>
        /// Método para injetar outros Claims ao user identity corrente
        /// Ao sobrescrever usar a codificação base caso queira que email, nome completo e id do usuário estejam nos claims
        /// </summary>
        /// <param name="userIdentity"></param>
        protected virtual void SetClaims(ClaimsIdentity userIdentity)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimCodes.Email.ToString(), this.Email));
            claims.Add(new Claim(ClaimCodes.Name.ToString(), this.Name));
            claims.Add(new Claim(ClaimCodes.UserId.ToString(), this.Id.ToString()));

            //Transportar para a classe que irá herdar o User
            //if (this.Papeis != null)
            //{
            //    foreach (var papel in this.Papeis)
            //    {
            //        if (papel.Permissoes != null)
            //        {
            //            foreach (var permissao in papel.Permissoes)
            //            {
            //                claims.Add(new Claim(ClaimCodes.Permissoes.ToString(), ((int)permissao.Codigo).ToString()));
            //            }
            //        }
            //    }
            //}

            userIdentity.AddClaims(claims);
        }


        private void SetExternalProperties(ClaimsIdentity identity, ClaimsIdentity ext)
        {
            if (ext != null)
            {
                var ignoreClaim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims";
                // Adicionando Claims Externos no Identity
                foreach (var c in ext.Claims)
                {
                    if (!c.Type.StartsWith(ignoreClaim))
                        if (!identity.HasClaim(c.Type, c.Value))
                            identity.AddClaim(c);
                }
            }
        }
    }
}

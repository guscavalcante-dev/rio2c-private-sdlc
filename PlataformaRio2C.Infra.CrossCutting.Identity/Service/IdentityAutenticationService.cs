using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PlataformaRio2C.Infra.CrossCutting.Identity.Configuration;
using PlataformaRio2C.Infra.CrossCutting.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace PlataformaRio2C.Infra.CrossCutting.Identity.Service
{
    public class IdentityAutenticationService : IDisposable
    {
        protected readonly ApplicationSignInManager<ApplicationUser> _signInManager;
        protected readonly ApplicationUserManager<ApplicationUser> _userManager;

        public IdentityAutenticationService(ApplicationUserManager<ApplicationUser> userManager, ApplicationSignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task SignInAsync(IAuthenticationManager manager, ApplicationUser user, bool isPersistent)
        {            
            //var clientKey = Request.Browser.Type;
            //await _identityController.SignInClientAsync(user, clientKey);
            // Zerando contador de logins errados.
            await this.ResetAccessFailedCountAsync(user.Id);

            // Coletando Claims externos (se houver)
            ClaimsIdentity ext = await manager.GetExternalIdentityAsync(DefaultAuthenticationTypes.ExternalCookie);

            manager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie, DefaultAuthenticationTypes.ApplicationCookie);
            manager.SignIn
                (
                    new AuthenticationProperties { IsPersistent = isPersistent },
                    // Criação da instancia do Identity e atribuição dos Claims
                    await user.GenerateUserIdentityAsync(_userManager, ext)
                );
        }

        public Task<IdentityResult> ResetPasswordAsync(int userId, string token, string newPassword)
        {
            return _userManager.ResetPasswordAsync(userId, token, newPassword);
        }

        public async Task<IdentityResult> AddPasswordAsync(int userId,  string newPassword)
        {
            return await _userManager.AddPasswordAsync(userId, newPassword);
        }

        public IdentityResult AddPassword(int userId, string newPassword)
        {
            var teste = _userManager.RemovePassword<ApplicationUser, int>(userId);
            return _userManager.AddPassword(userId, newPassword);
        }



        public async Task<IdentitySignInStatus> PasswordSignInAsync(string userName, string password, bool rememberMe, bool shouldLockout = true)
        {
            var result = await _signInManager.PasswordSignInAsync(userName, password, rememberMe, shouldLockout);
            
            return Convert(result);
        }

        public async Task<bool> UserEnabled(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            return user != null && user.Active;
        }

        public Task<bool> HasBeenVerifiedAsync()
        {
            return _signInManager.HasBeenVerifiedAsync();
        }

        public Task<IList<string>> GetValidTwoFactorProvidersAsync(int userId)
        {
            return _userManager.GetValidTwoFactorProvidersAsync(userId);
        }

        public Task<bool> SendTwoFactorCodeAsync(string provider)
        {
            return _signInManager.SendTwoFactorCodeAsync(provider);
        }

        public Task<int> GetVerifiedUserIdAsync()
        {
            return _signInManager.GetVerifiedUserIdAsync();
        }

        public Task<ApplicationUser> FindByIdAsync(int userId)
        {
            return _userManager.FindByIdAsync(userId);
        }

        public Task<string> GenerateTwoFactorTokenAsync(int userId, string provider)
        {
            return _userManager.GenerateTwoFactorTokenAsync(userId, provider);
        }

        public async Task<IdentitySignInStatus> TwoFactorSignInAsync(string provider, string code, bool isPersistent = false, bool rememberBrowser = false)
        {
            var result = await _signInManager.TwoFactorSignInAsync(provider, code, isPersistent, rememberBrowser);
            return Convert(result);
        }

        public async Task<ApplicationUser> FindAsync(string userName, string password)
        {
            return await _userManager.FindAsync(userName, password);
        }

        public async Task<IdentityReturn> CreateAsync(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return new IdentityReturn(result);
        }

        public Task SignInAsync(ApplicationUser user, bool isPersistent = false, bool rememberBrowser = false)
        {
            return _signInManager.SignInAsync(user, isPersistent, rememberBrowser);
        }

        public Task<string> GenerateEmailConfirmationTokenAsync(int userId)
        {
            return _userManager.GenerateEmailConfirmationTokenAsync(userId);
        }

        public Task SendEmailAsync(int userId, string subject, string body)
        {
            return _userManager.SendEmailAsync(userId, subject, body);
        }

        public async Task<IdentityReturn> ConfirmEmailAsync(int userId, string code)
        {
            var result = await _userManager.ConfirmEmailAsync(userId, code);
            return new IdentityReturn(result);
        }

        public Task<ApplicationUser> FindByNameAsync(string name)
        {
            return _userManager.FindByNameAsync(name);
        }

        public Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return _userManager.FindByEmailAsync(email);
        }

        public Task<bool> IsEmailConfirmedAsync(int userId)
        {
            return _userManager.IsEmailConfirmedAsync(userId);
        }

        public Task<string> GeneratePasswordResetTokenAsync(int userId)
        {
            return _userManager.GeneratePasswordResetTokenAsync(userId);
        }

        public async Task<IdentityReturn> ResetPasswordAsync(int userId, string code)
        {
            var result = await _userManager.ConfirmEmailAsync(userId, code);
            return new IdentityReturn(result);
        }

        public async Task<IdentityReturn> ResetAccessFailedCountAsync(int userId)
        {
            var result = await _userManager.ResetAccessFailedCountAsync(userId);
            return new IdentityReturn(result);
        }

        private IdentitySignInStatus Convert(SignInStatus status)
        {
            return (IdentitySignInStatus)((int)status);
        }

        public  Task<bool> IsInRoleAsync(int userId, string role)
        {
            var result = /*await*/ _userManager.IsInRoleAsync(userId, role);
            return result;
        }

        public async Task<IdentityResult> ChangePasswordAsync(int userId, string oldPassword, string newPassword)
        {
            var result = await _userManager.ChangePasswordAsync(userId, oldPassword, newPassword);
            return (result);
        }

        public async Task<bool> AddClaim(int userId, Claim claim)
        {
            var result = await _userManager.AddClaimAsync(userId, claim);

            return result.Succeeded;
        }

        public async Task<bool> RemoveClaim(int userId, string nameClaim)
        {
            var claim = await _userManager.GetClaimsAsync(userId);

            if (claim != null && claim.Any(e => e.Type == nameClaim))
            {
                var result = await _userManager.RemoveClaimAsync(userId, claim.FirstOrDefault(e => e.Type == nameClaim));

                return result.Succeeded;
            }

            return true;
        }

        public void Dispose()
        {
            if (_userManager != null)
            {
                _userManager.Dispose();
                //_userManager = null;
            }

            if (_signInManager != null)
            {
                _signInManager.Dispose();
                //_signInManager = null;
            }
        }
    }

    public enum IdentitySignInStatus
    {
        Success = 0,

        LockedOut = 1,

        RequiresVerification = 2,

        Failure = 3
    }
}

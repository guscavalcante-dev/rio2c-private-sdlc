// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.Identity
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-03-2019
// ***********************************************************************
// <copyright file="IdentityAutenticationService.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PlataformaRio2C.Infra.CrossCutting.Identity.Configuration;
using PlataformaRio2C.Infra.CrossCutting.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PlataformaRio2C.Infra.CrossCutting.Identity.Service
{
    /// <summary>IdentityAutenticationService</summary>
    public class IdentityAutenticationService : IDisposable
    {
        protected readonly ApplicationSignInManager<ApplicationUser> _signInManager;
        protected readonly ApplicationUserManager<ApplicationUser> _userManager;

        /// <summary>Initializes a new instance of the <see cref="IdentityAutenticationService"/> class.</summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="signInManager">The sign in manager.</param>
        public IdentityAutenticationService(ApplicationUserManager<ApplicationUser> userManager, ApplicationSignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>Signs the in asynchronous.</summary>
        /// <param name="manager">The manager.</param>
        /// <param name="user">The user.</param>
        /// <param name="isPersistent">if set to <c>true</c> [is persistent].</param>
        /// <returns></returns>
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

        /// <summary>Resets the password asynchronous.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="token">The token.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns></returns>
        public Task<IdentityResult> ResetPasswordAsync(int userId, string token, string newPassword)
        {
            return _userManager.ResetPasswordAsync(userId, token, newPassword);
        }

        /// <summary>Adds the password asynchronous.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns></returns>
        public async Task<IdentityResult> AddPasswordAsync(int userId,  string newPassword)
        {
            return await _userManager.AddPasswordAsync(userId, newPassword);
        }

        /// <summary>Adds the password.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns></returns>
        public IdentityResult AddPassword(int userId, string newPassword)
        {
            var teste = _userManager.RemovePassword<ApplicationUser, int>(userId);
            return _userManager.AddPassword(userId, newPassword);
        }

        /// <summary>Passwords the sign in asynchronous.</summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="rememberMe">if set to <c>true</c> [remember me].</param>
        /// <param name="shouldLockout">if set to <c>true</c> [should lockout].</param>
        /// <returns></returns>
        public async Task<IdentitySignInStatus> PasswordSignInAsync(string userName, string password, bool rememberMe, bool shouldLockout = true)
        {
            var result = await _signInManager.PasswordSignInAsync(userName, password, rememberMe, shouldLockout);
            
            return Convert(result);
        }

        /// <summary>Users the enabled.</summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public async Task<bool> UserEnabled(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            return user != null && user.Active;
        }

        /// <summary>Determines whether [has been verified asynchronous].</summary>
        /// <returns></returns>
        public Task<bool> HasBeenVerifiedAsync()
        {
            return _signInManager.HasBeenVerifiedAsync();
        }

        /// <summary>Gets the valid two factor providers asynchronous.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public Task<IList<string>> GetValidTwoFactorProvidersAsync(int userId)
        {
            return _userManager.GetValidTwoFactorProvidersAsync(userId);
        }

        /// <summary>Sends the two factor code asynchronous.</summary>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        public Task<bool> SendTwoFactorCodeAsync(string provider)
        {
            return _signInManager.SendTwoFactorCodeAsync(provider);
        }

        /// <summary>Gets the verified user identifier asynchronous.</summary>
        /// <returns></returns>
        public Task<int> GetVerifiedUserIdAsync()
        {
            return _signInManager.GetVerifiedUserIdAsync();
        }

        /// <summary>Finds the by identifier asynchronous.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public Task<ApplicationUser> FindByIdAsync(int userId)
        {
            return _userManager.FindByIdAsync(userId);
        }

        /// <summary>Generates the two factor token asynchronous.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        public Task<string> GenerateTwoFactorTokenAsync(int userId, string provider)
        {
            return _userManager.GenerateTwoFactorTokenAsync(userId, provider);
        }

        /// <summary>Twoes the factor sign in asynchronous.</summary>
        /// <param name="provider">The provider.</param>
        /// <param name="code">The code.</param>
        /// <param name="isPersistent">if set to <c>true</c> [is persistent].</param>
        /// <param name="rememberBrowser">if set to <c>true</c> [remember browser].</param>
        /// <returns></returns>
        public async Task<IdentitySignInStatus> TwoFactorSignInAsync(string provider, string code, bool isPersistent = false, bool rememberBrowser = false)
        {
            var result = await _signInManager.TwoFactorSignInAsync(provider, code, isPersistent, rememberBrowser);
            return Convert(result);
        }

        /// <summary>Finds the asynchronous.</summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public async Task<ApplicationUser> FindAsync(string userName, string password)
        {
            return await _userManager.FindAsync(userName, password);
        }

        /// <summary>Creates the asynchronous.</summary>
        /// <param name="user">The user.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public async Task<IdentityReturn> CreateAsync(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return new IdentityReturn(result);
        }

        /// <summary>Signs the in asynchronous.</summary>
        /// <param name="user">The user.</param>
        /// <param name="isPersistent">if set to <c>true</c> [is persistent].</param>
        /// <param name="rememberBrowser">if set to <c>true</c> [remember browser].</param>
        /// <returns></returns>
        public Task SignInAsync(ApplicationUser user, bool isPersistent = false, bool rememberBrowser = false)
        {
            return _signInManager.SignInAsync(user, isPersistent, rememberBrowser);
        }

        /// <summary>Generates the email confirmation token asynchronous.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public Task<string> GenerateEmailConfirmationTokenAsync(int userId)
        {
            return _userManager.GenerateEmailConfirmationTokenAsync(userId);
        }

        /// <summary>Sends the email asynchronous.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <returns></returns>
        public Task SendEmailAsync(int userId, string subject, string body)
        {
            return _userManager.SendEmailAsync(userId, subject, body);
        }

        /// <summary>Confirms the email asynchronous.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public async Task<IdentityReturn> ConfirmEmailAsync(int userId, string code)
        {
            var result = await _userManager.ConfirmEmailAsync(userId, code);
            return new IdentityReturn(result);
        }

        /// <summary>Finds the by name asynchronous.</summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public Task<ApplicationUser> FindByNameAsync(string name)
        {
            return _userManager.FindByNameAsync(name);
        }

        /// <summary>Finds the by email asynchronous.</summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return _userManager.FindByEmailAsync(email);
        }

        /// <summary>Determines whether [is email confirmed asynchronous] [the specified user identifier].</summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public Task<bool> IsEmailConfirmedAsync(int userId)
        {
            return _userManager.IsEmailConfirmedAsync(userId);
        }

        /// <summary>Generates the password reset token asynchronous.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public Task<string> GeneratePasswordResetTokenAsync(int userId)
        {
            return _userManager.GeneratePasswordResetTokenAsync(userId);
        }

        /// <summary>Resets the password asynchronous.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public async Task<IdentityReturn> ResetPasswordAsync(int userId, string code)
        {
            var result = await _userManager.ConfirmEmailAsync(userId, code);
            return new IdentityReturn(result);
        }

        /// <summary>Resets the access failed count asynchronous.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<IdentityReturn> ResetAccessFailedCountAsync(int userId)
        {
            var result = await _userManager.ResetAccessFailedCountAsync(userId);
            return new IdentityReturn(result);
        }

        /// <summary>Converts the specified status.</summary>
        /// <param name="status">The status.</param>
        /// <returns></returns>
        private IdentitySignInStatus Convert(SignInStatus status)
        {
            return (IdentitySignInStatus)((int)status);
        }

        /// <summary>Determines whether [is in role asynchronous] [the specified user identifier].</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="role">The role.</param>
        /// <returns></returns>
        public Task<bool> IsInRoleAsync(int userId, string role)
        {
            var result = /*await*/ _userManager.IsInRoleAsync(userId, role);
            return result;
        }

        /// <summary>Changes the password asynchronous.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="oldPassword">The old password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns></returns>
        public async Task<IdentityResult> ChangePasswordAsync(int userId, string oldPassword, string newPassword)
        {
            var result = await _userManager.ChangePasswordAsync(userId, oldPassword, newPassword);
            return (result);
        }

        /// <summary>Adds the claim.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="claim">The claim.</param>
        /// <returns></returns>
        public async Task<bool> AddClaim(int userId, Claim claim)
        {
            var result = await _userManager.AddClaimAsync(userId, claim);

            return result.Succeeded;
        }

        /// <summary>Removes the claim.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="nameClaim">The name claim.</param>
        /// <returns></returns>
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

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
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

    /// <summary>IdentitySignInStatus</summary>
    public enum IdentitySignInStatus
    {
        Success = 0,
        LockedOut = 1,
        RequiresVerification = 2,
        Failure = 3
    }
}

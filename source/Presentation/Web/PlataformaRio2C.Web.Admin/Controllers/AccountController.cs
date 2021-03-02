// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-10-2020
// ***********************************************************************
// <copyright file="AccountController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using PlataformaRio2C.Infra.CrossCutting.Identity.Models;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Identity.ViewModels;
using PlataformaRio2C.Infra.CrossCutting.Tools.Enums;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MediatR;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Application.CQRS.Queries;
using PlataformaRio2C.Application.Common;
using System.Text.RegularExpressions;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Infra.CrossCutting.Resources.Helpers;
using PlataformaRio2C.Infra.CrossCutting.Tools.Attributes;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;
using Constants = PlataformaRio2C.Domain.Constants;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    /// <summary>AccountController</summary>
    [AjaxAuthorize(Order = 1, Roles = Constants.Role.AnyAdmin)]
    public class AccountController : BaseController
    {
        private readonly IdentityAutenticationService _identityController;
        private IAuthenticationManager authenticationManager => HttpContext.GetOwinContext().Authentication;

        /// <summary>Initializes a new instance of the <see cref="AccountController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        public AccountController(IMediator commandBus, IdentityAutenticationService identityController)
            : base(commandBus, identityController)
        {
            _identityController = identityController;
        }

        /// <summary>Indexes this instance.</summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Index()
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Login", "Account");
        }

        #region Login

        /// <summary>Logins the specified return URL.</summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(new LoginViewModel());
        }

        /// <summary>Logins the specified model.</summary>
        /// <param name="model">The model.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [HandleAntiforgeryTokenException]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = AsyncHelpers.RunSync<ApplicationUser>(() => _identityController.FindByEmailAsync(model.Email));
            if (user == null)
            {
                ModelState.AddModelError("", Messages.InvalidLoginOrPassword);
                return View(model);
            }

            var result = await _identityController.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, true);
            switch (result)
            {
                case IdentitySignInStatus.Success:

                    if (user.IsDeleted || !user.Active)
                    {
                        ModelState.AddModelError("", Messages.AccessDenied);
                        return View(model);
                    }

                    // Check if user has admin roles
                    var userRoles = await this._identityController.FindAllRolesByUserIdAsync(user.Id);
                    if (userRoles?.Any(role => Constants.Role.AnyAdminArray.Contains(role)) != true)
                    {
                        ModelState.AddModelError("", Messages.YouDontHaveAdminProfile);
                        return View(model);
                    }

                    var userLanguage = await this.CommandBus.Send(new FindUserLanguageDto(user.Id));
                    if (userLanguage != null)
                    {
                        var cookie = ApplicationCookieControl.SetCookie(userLanguage.Language.Code, Response.Cookies[Constants.CookieName.MyRio2CAdminCookie], Constants.CookieName.MyRio2CAdminCookie);
                        Response.Cookies.Add(cookie);
                    }
                    else
                    {
                        await this.CommandBus.Send(new UpdateUserInterfaceLanguage(
                            user.Uid,
                            ViewBag.UserInterfaceLanguage));
                    }

                    if (!string.IsNullOrEmpty(returnUrl?.Replace("/", string.Empty)))
                    {
                        if (userLanguage != null)
                        {
                            if (CultureHelper.Cultures?.Any() == true)
                            {
                                foreach (var configuredCulture in CultureHelper.Cultures)
                                {
                                    returnUrl = Regex.Replace(returnUrl, configuredCulture, userLanguage.Language.Code, RegexOptions.IgnoreCase);
                                }
                            }

                            if (returnUrl.IndexOf(userLanguage.Language.Code, StringComparison.OrdinalIgnoreCase) < 0)
                            {
                                returnUrl = "/" + userLanguage.Language.Code + returnUrl;
                            }
                        }

                        return Redirect(returnUrl);
                    }

                    if (userLanguage != null)
                    {
                        return RedirectToAction("Index", "Home", new { culture = userLanguage?.Language?.Code });
                    }

                    return RedirectToAction("Index", "Home");

                    //var identity = (ClaimsIdentity)User.Identity;

                    //IEnumerable<Claim> claims = identity.Claims;

                    //if (await _apiSymplaAppService.ConfirmUserAllowedFinancialReport(user.Email))
                    //{
                    //    await _identityController.AddClaim(user.Id, new System.Security.Claims.Claim("FinancialReport", "true"));
                    //}
                    //else
                    //{
                    //    await _identityController.RemoveClaim(user.Id, "FinancialReport");
                    //}

                    //if (user.Claims.Count != 0)
                    //{
                    //    foreach (var item in user.Claims)
                    //    {
                    //        if (item.ClaimType.ToString() == "ProjectPitching")
                    //        {
                    //            return RedirectToAction("ProjectPitching", "Project");
                    //        }

                    //        if (item.ClaimType.ToString() == "Logistics")
                    //        {
                    //            return RedirectToAction("Index", "Logistics");
                    //        }
                    //    }
                    //}

                    // return RedirectToAction("Index", "Home");

                case IdentitySignInStatus.LockedOut:
                    return View("Lockout");

                case IdentitySignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });

                case IdentitySignInStatus.Failure:
                default:
                    ModelState.AddModelError("", Messages.InvalidLoginOrPassword);
                    return View(model);
            }
        }

        #endregion

        #region Log Off

        /// <summary>Logs the off.</summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult LogOff()
        {
            authenticationManager.SignOut();
            return RedirectToAction("Index", "Account");
        }

        #endregion

        #region Forgot Password

        /// <summary>Forgots the password.</summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        /// <summary>Forgots the password.</summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _identityController.FindByEmailAsync(model.Email);
            if (user == null || !user.Active || user.IsDeleted)
            {
                ModelState.AddModelError("Email", Messages.UserNotFound);
                return View(model);
            }

            // Check if user has admin roles
            var userRoles = await this._identityController.FindAllRolesByUserIdAsync(user.Id);
            if (userRoles?.Any(role => Constants.Role.AnyAdminArray.Contains(role)) != true)
            {
                ModelState.AddModelError("", Messages.UserNotFound);
                return View(model);
            }

            try
            {
                var passwordResetToken = await _identityController.GeneratePasswordResetTokenAsync(user.Id);

                var result = await this.CommandBus.Send(new SendForgotPasswordEmailAsync(
                    passwordResetToken,
                    user.Id,
                    user.Uid,
                    null,
                    null,
                    user.Email,
                    this.EditionDto.Edition,
                    this.UserInterfaceLanguage));
                if (!result.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }
            }
            catch (DomainException ex)
            {
                ModelState.AddModelError("", ex.GetInnerMessage());
                return View(model);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                ModelState.AddModelError("", Messages.WeFoundAndError);
                return View(model);
            }

            return View("ForgotPasswordConfirmation");
        }

        /// <summary>Resets the password.</summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ResetPassword(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("NotFound", "Error");
            }

            return View(new ResetPasswordViewModel { Token = token });
        }

        /// <summary>Resets the password.</summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _identityController.FindByEmailAsync(model.Email);
            if (user == null || !user.Active || user.IsDeleted)
            {
                ModelState.AddModelError("Email", Messages.UserNotFound);
                return View(model);
            }

            // Check if user has admin roles
            var userRoles = await this._identityController.FindAllRolesByUserIdAsync(user.Id);
            if (userRoles?.Any(role => Constants.Role.AnyAdminArray.Contains(role)) != true)
            {
                ModelState.AddModelError("", Messages.UserNotFound);
                return View(model);
            }

            var result = await _identityController.ResetPasswordAsync(user.Id, model.Token, model.Password);
            if (result.Succeeded)
            {
                if (User.Identity.IsAuthenticated)
                {
                    this.Message(MessageType.SUCCESS, Messages.YourPasswordHasBeenChangedSuccessfully);
                    return RedirectToAction("Index", "Account");
                }

                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }

            AddErrors(result);

            return View(model);
        }

        /// <summary>Resets the password confirmation.</summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        ///// <summary>Resets the password authenticated.</summary>
        ///// <returns></returns>
        //[Authorize]
        //public async Task<ActionResult> ResetPasswordAuthenticated()
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        var user = await _identityController.FindByNameAsync(User.Identity.Name);
        //        if (user == null)
        //        {
        //            this.authenticationManager.SignOut();
        //            return View("UserNotFound");
        //        }
        //        else if (!user.Active)
        //        {
        //            this.authenticationManager.SignOut();
        //            return View("DisabledUser");
        //        }
        //        else
        //        {
        //            var code = await _identityController.GeneratePasswordResetTokenAsync(user.Id);
        //            return View(new ResetPasswordViewModel { Code = code, Email = user.Email });
        //        }
        //    }

        //    return RedirectToAction("Index", "Account");
        //}

        #endregion

        #region Change Password

        /// <summary>Shows the update password modal.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowUpdatePasswordModal()
        {
            ChangePasswordViewModel cmd;

            try
            {
                cmd = new ChangePasswordViewModel();
            }
            catch (DomainException ex)
            {
                return Json(new { status = "error", message = ex.GetInnerMessage() }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Modals/UpdatePasswordModal", cmd), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Updates the password.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        /// <exception cref="DomainException"></exception>
        [HttpPost]
        public async Task<ActionResult> UpdatePassword(ChangePasswordViewModel cmd)
        {
            IdentityResult result = null;
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

                result = await _identityController.ChangePasswordAsync(this.AdminAccessControlDto.User.Id, cmd.OldPassword, cmd.NewPassword);
                if (!result.Succeeded)
                {
                    throw new DomainException(Messages.ErrorUpdatingPassword);
                }
            }
            catch (DomainException ex)
            {
                if (result?.Errors?.Any() == true)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                return Json(new
                {
                    status = "error",
                    message = ex.GetInnerMessage(),
                    pages = new List<dynamic>
                    {
                        new { page = this.RenderRazorViewToString("Modals/UpdatePasswordForm", cmd), divIdOrClass = "#form-container" },
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Password, Labels.UpdatedF) });
        }

        #endregion

        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await _identityController.GetVerifiedUserIdAsync();
            if (userId <= 0)
            {
                return View("Error");
            }
            var userFactors = await _identityController.GetValidTwoFactorProvidersAsync(userId);

            if (!await _identityController.SendTwoFactorCodeAsync(userFactors[0]))
            {
                return View("Error");
            }

            return RedirectToAction("VerifyCode", new { Provider = userFactors[0], ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await _identityController.HasBeenVerifiedAsync())
            {
                return View("Error");
            }

            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _identityController.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case IdentitySignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case IdentitySignInStatus.LockedOut:
                    return View("Lockout");
                case IdentitySignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Código Inválido.");
                    return View(model);
            }
        }

        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(int userId, string code)
        {
            if (userId <= 0 || code == null || await _identityController.FindByIdAsync(userId) == null)
            {
                return View("Error");
            }
            var result = await _identityController.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        #region Auxiliary Private Methods

        /// <summary>Redirects to local.</summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns></returns>
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("ProjectsPitchingList", "Project");
        }

        /// <summary>Adds the errors.</summary>
        /// <param name="result">The result.</param>
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        #endregion

        /// <summary>Releases unmanaged resources and optionally releases managed resources.</summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _identityController.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
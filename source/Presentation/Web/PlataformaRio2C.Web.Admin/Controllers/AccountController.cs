// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-28-2019
// ***********************************************************************
// <copyright file="AccountController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using PlataformaRio2C.Application.Interfaces.Services;
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
using Constants = PlataformaRio2C.Domain.Constants;
using PlataformaRio2C.Application.CQRS.Queries;
using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Constants;
using System.Text.RegularExpressions;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    /// <summary>AccountController</summary>
    [AjaxAuthorize(Order = 1, Roles = Constants.Role.AnyAdmin)]
    public class AccountController : BaseController
    {
        private readonly IdentityAutenticationService _identityController;
        private IAuthenticationManager authenticationManager => HttpContext.GetOwinContext().Authentication;
        private readonly IApiSymplaAppService _apiSymplaAppService;

        /// <summary>Initializes a new instance of the <see cref="AccountController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="apiSymplaAppService">The API sympla application service.</param>
        public AccountController(IMediator commandBus, IdentityAutenticationService identityController, IApiSymplaAppService apiSymplaAppService)
            : base(commandBus, identityController)
        {
            _identityController = identityController;
            _apiSymplaAppService = apiSymplaAppService;
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
            return View();
        }

        /// <summary>Logins the specified model.</summary>
        /// <param name="model">The model.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
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
                        var cookie = new ApplicationCookieControl().SetCookie(userLanguage.Language.Code, Response.Cookies[Role.MyRio2CAdminCookie], Role.MyRio2CAdminCookie);
                        Response.Cookies.Add(cookie);
                    }

                    var listLanguages = await this.CommandBus.Send(new FindAllLanguagesDtosAsync(null));

                    if (!string.IsNullOrEmpty(returnUrl?.Replace("/", string.Empty)))
                    {
                        foreach (var item in listLanguages)
                            returnUrl = Regex.Replace(returnUrl, item.Code, userLanguage?.Language.Code, RegexOptions.IgnoreCase);
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }

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

        #region ForgotPassword

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
            if (ModelState.IsValid)
            {
                var user = await _identityController.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return View("UserNotFound");
                }
                else if (!user.Active || !(await _identityController.IsEmailConfirmedAsync(user.Id)))
                {
                    return View("DisabledUser");
                }

                var code = await _identityController.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                await _identityController.SendEmailAsync(user.Id, "Plataforma Digitaliza - Esqueci minha senha", "Para alterar sua senha clique aqui: <a href='" + callbackUrl + "'>Alterar senha</a>");
                return View("ForgotPasswordConfirmation");
            }

            return View(model);
        }

        /// <summary>Resets the password authenticated.</summary>
        /// <returns></returns>
        [Authorize]
        public async Task<ActionResult> ResetPasswordAuthenticated()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _identityController.FindByNameAsync(User.Identity.Name);
                if (user == null)
                {
                    authenticationManager.SignOut();
                    return View("UserNotFound");
                }
                else if (!user.Active)
                {
                    authenticationManager.SignOut();
                    return View("DisabledUser");
                }
                else
                {
                    var code = await _identityController.GeneratePasswordResetTokenAsync(user.Id);
                    return View(new ResetPasswordViewModel { Code = code, Email = user.Email });
                }
            }

            return RedirectToAction("Index", "Account");
        }

        /// <summary>Resets the password.</summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View(new ResetPasswordViewModel { Code = code });
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
            if (user == null)
            {
                return View("UserNotFound");
            }
            else if (!user.Active)
            {
                return View("DisabledUser");
            }
            var result = await _identityController.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                if (User.Identity.IsAuthenticated)
                {
                    this.Message(MessageType.SUCCESS, "Sua senha foi alterada com sucesso!");
                    return RedirectToAction("Index", "Account");
                }

                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();

        }

        /// <summary>Resets the password confirmation.</summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
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

            return RedirectToAction("ProjectPitching", "Project");
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
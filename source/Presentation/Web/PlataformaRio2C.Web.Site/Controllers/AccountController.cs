// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-04-2019
// ***********************************************************************
// <copyright file="AccountController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using HtmlAgilityPack;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using PlataformaRio2C.Infra.CrossCutting.Identity.Models;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Identity.ViewModels;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Enums;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PlataformaRio2C.Web.Site.Controllers
{
    /// <summary>AccountController</summary>
    [Authorize(Order = 1, Roles = "Player,Producer")]
    public class AccountController : BaseController
    {
        private readonly IdentityAutenticationService _identityController;
        private IAuthenticationManager authenticationManager => HttpContext.GetOwinContext().Authentication;

        /// <summary>Initializes a new instance of the <see cref="AccountController"/> class.</summary>
        /// <param name="identityController">The identity controller.</param>
        public AccountController(IdentityAutenticationService identityController)
            : base(identityController)
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
        public async Task<ActionResult> Login(string returnUrl)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                int userId = User.Identity.GetUserId<int>();
                if (await _identityController.IsInRoleAsync(userId, "Player") || await _identityController.IsInRoleAsync(userId, "Producer"))
                {
                    return RedirectToAction("Index", "Home");
                }
            }

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

            if (returnUrl != null && returnUrl.Contains("LogOff"))
            {
                returnUrl = "";
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //transforma a senha digitada em md5
            //byte[] encodedPassword = new UTF8Encoding().GetBytes(model.Password);
            //byte[] bytePassword = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
            string md5Password = CreateMD5(model.Password);

            ////busca o email no JSON da ticket4you
            //Ticket4youController userByTicket = new Ticket4youController();

            //if (userByTicket.SearchOne(model.Email) == null)
            //{
            //    ModelState.AddModelError("", Messages.LoginOrPasswordIsIncorrect);
            //    //ModelState.AddModelError("", Messages.LoginByTicket4YouIncorrect);
            //    return View(model);

            //}
            //else if (userByTicket.UserTicket.senha != md5Password)
            //{
            //    ModelState.AddModelError("", Messages.LoginOrPasswordIsIncorrect);
            //    //ModelState.AddModelError("", Messages.LoginByTicket4YouIncorrect);
            //    return View(model);
            //}
            ////else if (userByTicket.UserTicket.status_pedido != "Aprovado")
            ////{
            ////    //CRIAR TEXTO DE PAGAMANTO NÃO APROVADO
            ////    ModelState.AddModelError("", Messages.LoginOrPasswordIsIncorrect);
            ////    //ModelState.AddModelError("", Messages.LoginByTicket4YouIncorrect);
            ////    return View(model);
            ////}
            //else
            //{
            
                var user = AsyncHelpers.RunSync<ApplicationUser>(() => _identityController.FindByEmailAsync(model.Email));
                if (user == null)
                {
                    ModelState.AddModelError("", Messages.LoginOrPasswordIsIncorrect);
                    return View(model);
                }
                else if (!user.Active)
                {
                    return View("DisabledUser");
                }

                _identityController.AddPassword(user.Id, md5Password);

                //var result = await _identityController.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, true);
                var result = await _identityController.PasswordSignInAsync(model.Email, md5Password, model.RememberMe, true);
                switch (result)
                {
                    case IdentitySignInStatus.Success:
                        await _identityController.SignInAsync(authenticationManager, user, model.RememberMe);

                        if (!string.IsNullOrEmpty(returnUrl?.Replace("/", string.Empty)))
                        {
                            return Redirect(returnUrl);
                        }

                        if (await _identityController.IsInRoleAsync(user.Id, "Player") || await _identityController.IsInRoleAsync(user.Id, "Producer"))
                        {
                            //returnUrl = returnUrl ?? "/";
                            //return RedirectToLocal(returnUrl);
                            return RedirectToAction("", "Quiz");
                        }

                        this.StatusMessage(Messages.AccessDenied, StatusMessageType.Danger);

                        return RedirectToAction("LogOff");

                    case IdentitySignInStatus.LockedOut:
                        return View("Lockout");

                    case IdentitySignInStatus.RequiresVerification:
                        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });

                    case IdentitySignInStatus.Failure:
                    default:
                        ModelState.AddModelError("", Messages.LoginOrPasswordIsIncorrect);
                        return View(model);
                }
            //}
        }

        /// <summary>Creates the m d5.</summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        private static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
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
                    ModelState.AddModelError("", Messages.InvalidCode);
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
            if (ModelState.IsValid)
            {
                var user = await _identityController.FindByEmailAsync(model.Email);
                if (user == null || !user.Active)
                {
                    return ReturnUserNotFound<ForgotPasswordViewModel>(model);
                }                         
                
                var code = await _identityController.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);

                var bodyMessage = string.Format(Texts.EmailBodyForgotPassword, callbackUrl);

                var messageEmail = CompileHtmlDefaultTemplateMessage().Replace("@{Message}", bodyMessage);

                var path = VirtualPathUtility.ToAbsolute("/");
                var url = new Uri(Request.Url, path).AbsoluteUri;

                messageEmail = messageEmail.Replace("@{UrlSite}", url);                
                
                await _identityController.SendEmailAsync(user.Id, Texts.EmailSubjectForgotPassword, messageEmail);
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
            if (user == null || !user.Active)
            {
                return ReturnUserNotFound<ResetPasswordViewModel>(model);
            }

            var result = await _identityController.ResetPasswordAsync(user.Id, model.Code, model.Password);
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

            return RedirectToAction("Index", "Account");
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

        /// <summary>Compiles the HTML default template message.</summary>
        /// <returns></returns>
        private string CompileHtmlDefaultTemplateMessage()
        {
            HtmlDocument template = new HtmlDocument();
            template.Load(HttpContext.Server.MapPath(string.Format("~/TemplatesEmail/defaultTemplateWithImageSrc.html")));
            return template.DocumentNode.InnerHtml;
        }

        /// <summary>Returns the user not found.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        private ActionResult ReturnUserNotFound<T>(T model)
        {
            ModelState.AddModelError("", Texts.UserNotFound);
            return View(model);
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

        #region Change Password

        /// <summary>Changes the password.</summary>
        /// <returns></returns>
        public ActionResult ChangePassword()
        {
            return View();
        }

        /// <summary>Changes the password.</summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _identityController.ChangePasswordAsync(User.Identity.GetUserId<int>(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await _identityController.FindByIdAsync(User.Identity.GetUserId<int>());
                if (user != null)
                {
                    await _identityController.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index");
            }
            AddErrors(result);
            return View(model);
        }

        #endregion

        //[AllowAnonymous]
        //public ActionResult Error()
        //{
        //    return View();
        //}

        //[AllowAnonymous]
        //public ActionResult ErrorUnexpected()
        //{
        //    return View();
        //}
    }
}
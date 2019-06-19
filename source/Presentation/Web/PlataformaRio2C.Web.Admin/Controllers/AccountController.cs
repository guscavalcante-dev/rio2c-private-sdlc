using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Infra.CrossCutting.Identity.Models;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Identity.ViewModels;
using PlataformaRio2C.Infra.CrossCutting.Tools.Enums;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    [Authorize(Roles = "Administrator,Pitching")]
    public class AccountController : BaseController
    {
        private readonly IdentityAutenticationService _identityController;
        private IAuthenticationManager authenticationManager => HttpContext.GetOwinContext().Authentication;
        private readonly IApiSymplaAppService _apiSymplaAppService;

        public AccountController(IdentityAutenticationService identityController, IApiSymplaAppService apiSymplaAppService)
        {
            _identityController = identityController;
            _apiSymplaAppService = apiSymplaAppService;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            return RedirectToAction("Login", "Account"); 
        }

        // GET: Account
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

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
                ModelState.AddModelError("", "Login ou Senha incorretos.");
                return View(model);
            }
            else if (!user.Active)
            {
                return View("DisabledUser");
            }

            var result = await _identityController.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, true);
            switch (result)
            {
                case IdentitySignInStatus.Success:
                    if (!user.EmailConfirmed)
                    {
                        TempData["AvisoEmail"] = "Usuário não confirmado, verifique seu e-mail.";
                    }

                    var identity = (ClaimsIdentity)User.Identity;
                    IEnumerable<Claim> claims = identity.Claims;

                    if (await _apiSymplaAppService.ConfirmUserAllowedFinancialReport(user.Email))
                    {
                        await _identityController.AddClaim(user.Id, new System.Security.Claims.Claim("FinancialReport", "true"));
                    }
                    else
                    {
                        await _identityController.RemoveClaim(user.Id, "FinancialReport");
                    }
                    if (user.Claims.Count !=0)
                    {
                        foreach (var item in user.Claims)
                        {
                            if (item.ClaimType.ToString() == "ProjectPitching")
                            {
                                return RedirectToAction("ProjectPitching", "Project");
                            }
                            else if (item.ClaimType.ToString() == "Logistics")
                            {
                                return RedirectToAction("Index", "Logistics");
                            }
                        }
                    }

                    await _identityController.SignInAsync(authenticationManager, user, model.RememberMe);
                    return RedirectToLocal(returnUrl);
                case IdentitySignInStatus.LockedOut:
                    return View("Lockout");
                case IdentitySignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case IdentitySignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Login ou Senha incorretos.");
                    return View(model);
            }
        }

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

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        // POST: /Account/ForgotPassword
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

        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View(new ResetPasswordViewModel { Code = code });
        }

        // POST: /Account/ResetPassword
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

        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        // POST: /Account/LogOff        
        [Authorize]
        public ActionResult LogOff()
        {
            authenticationManager.SignOut();
            return RedirectToAction("Index", "Account");
        }

        #region Métodos privados auxiliares

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("ProjectPitching", "Project");
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        #endregion

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
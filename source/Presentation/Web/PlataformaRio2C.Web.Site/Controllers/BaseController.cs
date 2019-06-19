using Microsoft.AspNet.Identity;
using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Infra.CrossCutting.Resources.Helpers;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System.Security.Claims;

namespace PlataformaRio2C.Web.Site.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string cultureName = null;

            // Attempt to read the culture cookie from Request
            HttpCookie cultureCookie = Request.Cookies["_culture"];
            if (cultureCookie != null)
                cultureName = cultureCookie.Value;
            else
                cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ?
                        Request.UserLanguages[0] :  // obtain it from HTTP header AcceptLanguages
                        null;
            // Validate culture name
            cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe

            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            return base.BeginExecuteCore(callback, state);
        }

        [AllowAnonymous]
        public ActionResult SetCulture(string culture, string returnUrl = null)
        {
            if (returnUrl == null && Request.UrlReferrer != null)
            {
                returnUrl = Request.UrlReferrer.PathAndQuery;
            }

            // Validate input
            culture = CultureHelper.GetImplementedCulture(culture);
            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture;   // update cookie value
            else
            {
                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);


            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }    
        
        
        protected void CheckRegisterIsComplete()
        {
            var method = Request.HttpMethod;

            var collaboratorAppService = (ICollaboratorAppService)DependencyResolver.Current.GetService(typeof(ICollaboratorAppService));
            int userId = User.Identity.GetUserId<int>();
            var collaborator = collaboratorAppService.GetStatusRegisterCollaboratorByUserId(userId);

            var userIdentity = (ClaimsIdentity)User.Identity;
            var claims = userIdentity.Claims;
            var roleClaimType = userIdentity.RoleClaimType;
            var roles = claims.Where(c => c.Type == ClaimTypes.Role).ToList();

            if (collaborator != null)
            {
                ViewBag.PlayerComplete = true;
                ViewBag.InterestComplete = true;
                ViewBag.ProducerComplete = true;

                ViewBag.ProfileComplete = collaborator.RegisterComplete;

                if (User.IsInRole("Player"))
                {
                    ViewBag.PlayerComplete = collaborator.PlayersRegisterComplete;
                    ViewBag.InterestComplete = collaborator.PlayersInterestFilled;
                }

                if (User.IsInRole("Producer"))
                {
                    ViewBag.ProducerComplete = collaborator.ProducersRegisterComplete;
                }

                if (!ViewBag.ProfileComplete && method == "GET")
                {
                    this.StatusMessage(@Messages.ProfileIncompleteMessage, Infra.CrossCutting.Tools.Enums.StatusMessageType.Danger);
                }

                if (!ViewBag.PlayerComplete && method == "GET")
                {
                    this.StatusMessage(Messages.PlayerIncompleteMessage, Infra.CrossCutting.Tools.Enums.StatusMessageType.Danger);
                }

                if (!ViewBag.InterestComplete && method == "GET")
                {
                    this.StatusMessage(Messages.InterestsIncompleteMessage, Infra.CrossCutting.Tools.Enums.StatusMessageType.Danger);
                }

                if (!ViewBag.ProducerComplete && method == "GET")
                {
                    //this.StatusMessage("Produtora incompleta", Infra.CrossCutting.Tools.Enums.StatusMessageType.Danger);
                }

                if ( method == "POST" && ViewBag.ProfileComplete && ViewBag.PlayerComplete && ViewBag.InterestComplete && ViewBag.ProducerComplete)
                {
                    if (roles.Count() == 1 && User.IsInRole("Player"))
                    {
                        this.StatusMessageModal(Messages.SuccessfulRegistrationMessage, Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);
                    }
                    else if (roles.Count() == 1 && User.IsInRole("Producer"))
                    {
                        this.StatusMessageModal(string.Format(Messages.SuccessfulRegistrationMessageByProducer, "/ProducerArea/Project"), Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);
                    }
                    else if (roles.Count() == 2 && User.IsInRole("Player") && User.IsInRole("Producer"))
                    {
                        this.StatusMessageModal(string.Format(Messages.SuccessfulRegistrationMessageByProducerAndPlayer, "/ProducerArea/Project"), Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);
                    }                    
                }
            }
        }
        

        [AllowAnonymous]
        public ActionResult SetArea(string area, string returnUrl = null)
        {
            if (area == "Produtora")
            {
                return RedirectToAction("Index", "Dashboard", new { area = "ProducerArea"});
            }

            return RedirectToAction("Index", "Dashboard", new { area = "" });
        }
    }
}
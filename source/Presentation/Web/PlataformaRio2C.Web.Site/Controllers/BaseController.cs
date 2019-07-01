// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-01-2019
// ***********************************************************************
// <copyright file="BaseController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.AspNet.Identity;
using PlataformaRio2C.Application.Interfaces.Services;
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
    /// <summary>BaseController</summary>
    public class BaseController : Controller
    {
        /// <summary>Begins to invoke the action in the current controller context.</summary>
        /// <param name="callback">The callback.</param>
        /// <param name="state">The state.</param>
        /// <returns>Returns an IAsyncController instance.</returns>
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            // Attempt to read the culture cookie from Request
            var routeCulture = RouteData.Values["culture"] as string;
            var cookieCulture = Request.Cookies["Rio2CPlafatormCulture"]?.Value;
            var cultureName = CultureHelper.IsImplementedCulture(routeCulture) ? routeCulture :
                              CultureHelper.IsImplementedCulture(cookieCulture) ? cookieCulture :
                              (Request.UserLanguages != null && Request.UserLanguages.Length > 0 ? Request.UserLanguages[0] : null);

            // Validate culture name
            cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe

            if (RouteData.Values["culture"] as string != cultureName)
            {
                // Force a valid culture in the URL
                RouteData.Values["culture"] = cultureName.ToLowerInvariant(); // lower case too

                // Redirect user
                Response.RedirectToRoute(RouteData.Values);
            }

            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            return base.BeginExecuteCore(callback, state);
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
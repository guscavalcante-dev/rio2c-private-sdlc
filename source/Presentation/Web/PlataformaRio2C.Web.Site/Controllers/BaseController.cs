// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-03-2019
// ***********************************************************************
// <copyright file="BaseController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Infra.CrossCutting.Resources.Helpers;
using System;
using System.Threading;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Web.Site.Controllers
{
    /// <summary>BaseController</summary>
    public class BaseController : Controller
    {
        private readonly IdentityAutenticationService identityController;
        protected string UserInterfaceLanguage;
        protected int UserId;
        protected string UserName;
        protected string Area;

        /// <summary>Initializes a new instance of the <see cref="BaseController"/> class.</summary>
        /// <param name="identityControlle">The identity controlle.</param>
        public BaseController(IdentityAutenticationService identityControlle)
        {
            this.identityController = identityControlle;
        }

        /// <summary>Begins to invoke the action in the current controller context.</summary>
        /// <param name="callback">The callback.</param>
        /// <param name="state">The state.</param>
        /// <returns>Returns an IAsyncController instance.</returns>
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            this.SetCulture();
            this.SetUserInfo();
            this.SetArea();

            return base.BeginExecuteCore(callback, state);
        }

        /// <summary>Sets the culture.</summary>
        private void SetCulture()
        {
            // Attempt to read the culture cookie from Request
            var routeCulture = RouteData.Values["culture"] as string;
            var cookieCulture = Request.Cookies["Rio2CPlafatormCulture"]?.Value;
            var cultureName = routeCulture ??
                              cookieCulture ??
                              (Request.UserLanguages != null && Request.UserLanguages.Length > 0 ? Request.UserLanguages[0] : null);

            // Validate culture name
            cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe

            if (RouteData.Values["culture"] as string != cultureName)
            {
                var routes = new RouteValueDictionary(RouteData.Values);

                // Add or change culture on routes
                if (!routes.ContainsKey("culture"))
                {
                    routes.Add("culture", cultureName.ToLowerInvariant());
                }
                else
                {
                    routes["culture"] = cultureName.ToLowerInvariant();
                }

                // Add other parameters to route
                foreach (string key in HttpContext.Request.QueryString.Keys)
                {
                    if (key != null)
                    {
                        routes[key] = HttpContext.Request.QueryString[key];
                    }
                }

                HttpContext.Response.RedirectToRoute(routes);
            }

            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            this.UserInterfaceLanguage = cultureName;
        }

        /// <summary>Sets the area.</summary>
        private void SetArea()
        {
            this.Area = ViewBag.Area = RouteData.Values["Area"] as string;
        }

        /// <summary>Sets the user information.</summary>
        private void SetUserInfo()
        {
            if (this.identityController == null)
            {
                return;
            }

            this.UserId = User.Identity.GetUserId<int>();
            if (!User.Identity.IsAuthenticated || this.UserId <= 0)
            {
                return;
            }

            var user = this.identityController.FindByIdAsync(this.UserId).Result;
            if (user == null)
            {
                return;
            }

            ViewBag.FullName = this.UserName = user.Name.UppercaseFirstOfEachWord(this.UserInterfaceLanguage);
            ViewBag.FirstName = this.UserName?.GetFirstWord();
        }

        //protected void CheckRegisterIsComplete()
        //{
        //    var method = Request.HttpMethod;

        //    var collaboratorAppService = (ICollaboratorAppService)DependencyResolver.Current.GetService(typeof(ICollaboratorAppService));
        //    int userId = User.Identity.GetUserId<int>();
        //    var collaborator = collaboratorAppService.GetStatusRegisterCollaboratorByUserId(userId);

        //    var userIdentity = (ClaimsIdentity)User.Identity;
        //    var claims = userIdentity.Claims;
        //    var roleClaimType = userIdentity.RoleClaimType;
        //    var roles = claims.Where(c => c.Type == ClaimTypes.Role).ToList();

        //    if (collaborator != null)
        //    {
        //        ViewBag.PlayerComplete = true;
        //        ViewBag.InterestComplete = true;
        //        ViewBag.ProducerComplete = true;

        //        ViewBag.ProfileComplete = collaborator.RegisterComplete;

        //        if (User.IsInRole("Player"))
        //        {
        //            ViewBag.PlayerComplete = collaborator.PlayersRegisterComplete;
        //            ViewBag.InterestComplete = collaborator.PlayersInterestFilled;
        //        }

        //        if (User.IsInRole("Producer"))
        //        {
        //            ViewBag.ProducerComplete = collaborator.ProducersRegisterComplete;
        //        }

        //        if (!ViewBag.ProfileComplete && method == "GET")
        //        {
        //            this.StatusMessage(@Messages.ProfileIncompleteMessage, Infra.CrossCutting.Tools.Enums.StatusMessageType.Danger);
        //        }

        //        if (!ViewBag.PlayerComplete && method == "GET")
        //        {
        //            this.StatusMessage(Messages.PlayerIncompleteMessage, Infra.CrossCutting.Tools.Enums.StatusMessageType.Danger);
        //        }

        //        if (!ViewBag.InterestComplete && method == "GET")
        //        {
        //            this.StatusMessage(Messages.InterestsIncompleteMessage, Infra.CrossCutting.Tools.Enums.StatusMessageType.Danger);
        //        }

        //        if (!ViewBag.ProducerComplete && method == "GET")
        //        {
        //            //this.StatusMessage("Produtora incompleta", Infra.CrossCutting.Tools.Enums.StatusMessageType.Danger);
        //        }

        //        if ( method == "POST" && ViewBag.ProfileComplete && ViewBag.PlayerComplete && ViewBag.InterestComplete && ViewBag.ProducerComplete)
        //        {
        //            if (roles.Count() == 1 && User.IsInRole("Player"))
        //            {
        //                this.StatusMessageModal(Messages.SuccessfulRegistrationMessage, Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);
        //            }
        //            else if (roles.Count() == 1 && User.IsInRole("Producer"))
        //            {
        //                this.StatusMessageModal(string.Format(Messages.SuccessfulRegistrationMessageByProducer, "/ProducerArea/Project"), Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);
        //            }
        //            else if (roles.Count() == 2 && User.IsInRole("Player") && User.IsInRole("Producer"))
        //            {
        //                this.StatusMessageModal(string.Format(Messages.SuccessfulRegistrationMessageByProducerAndPlayer, "/ProducerArea/Project"), Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);
        //            }                    
        //        }
        //    }
        //}


        //[AllowAnonymous]
        //public ActionResult SetArea(string area, string returnUrl = null)
        //{
        //    if (area == "Produtora")
        //    {
        //        return RedirectToAction("Index", "Dashboard", new { area = "ProducerArea"});
        //    }

        //    return RedirectToAction("Index", "Dashboard", new { area = "" });
        //}
    }
}
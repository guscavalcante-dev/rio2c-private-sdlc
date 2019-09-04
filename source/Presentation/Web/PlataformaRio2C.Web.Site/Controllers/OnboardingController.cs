// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Fabio Seixas
// Created          : 08-29-2019
//
// Last Modified By : Fabio Seixas
// Last Modified On : 09-04-2019
// ***********************************************************************
// <copyright file="OnboardingController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.AspNet.Identity;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MediatR;
using PlataformaRio2C.Infra.CrossCutting.Resources.Helpers;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;
using System.Collections.Generic;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;

namespace PlataformaRio2C.Web.Site.Controllers
{
    /// <summary>OnboardingController</summary>
    [AjaxAuthorize(Order = 1)]
    public class OnboardingController : BaseController
    {
        private readonly IdentityAutenticationService identityController;

        /// <summary>Initializes a new instance of the <see cref="HomeController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        public OnboardingController(IMediator commandBus, IdentityAutenticationService identityController)
            : base(commandBus, identityController)
        {
            this.identityController = identityController;
        }

        /// <summary>Indexes this instance.</summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            try
            {
                #region Breadcrumb

                ViewBag.Breadcrumb = new BreadcrumbHelper("Bem-vindo", new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper("Complete seu cadastro", Url.Action("Index", "Onboarding"))
                });

                #endregion

                // Redirect to index
                if (this.UserAccessControlDto?.IsOnboardingPending() != true)
                {
                    return RedirectToAction("Index", "Home");
                }

                //TODO: Command to update OnboardingStartDate



                return View("Index");

                return RedirectToAction("Index", "Onboarding", new { Area = "Player" });

                var userId = User.Identity.GetUserId<int>();

                if (await this.identityController.IsInRoleAsync(userId, "Player"))
                {
                    return RedirectToAction("Index", "Onboarding", new { Area = "Player" });
                }

                if (await this.identityController.IsInRoleAsync(userId, "Producer"))
                {
                    return RedirectToAction("Index", "Onboarding", new { Area = "Producer" });
                }

                return RedirectToAction("LogOff", "Account");
            }
            catch (Exception)
            {
                return RedirectToAction("LogOff", "Account");
            }
        }

        /// <summary>Sets the culture.</summary>
        /// <param name="culture">The culture.</param>
        /// <param name="oldCulture">The old culture.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult SetCulture(string culture, string oldCulture, string returnUrl = null)
        {
            // Validate input
            culture = CultureHelper.GetImplementedCulture(culture);
            RouteData.Values["culture"] = culture;  // set culture

            #region Create/Update cookie culture

            var cookie = Request.Cookies["MyRio2CCulture"];
            if (cookie != null)
            {
                cookie.Value = culture;   // update cookie value
            }
            else
            {
                cookie = new HttpCookie("MyRio2CCulture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }

            Response.Cookies.Add(cookie);

            #endregion

            if (returnUrl == null && Request.UrlReferrer != null)
            {
                returnUrl = Request.UrlReferrer.PathAndQuery;
            }

            returnUrl = returnUrl?.Replace(oldCulture.ToLowerInvariant(), culture?.ToLowerInvariant());

            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Onboarding");
        }
    }
}
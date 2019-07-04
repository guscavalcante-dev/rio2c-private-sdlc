// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-04-2019
// ***********************************************************************
// <copyright file="HomeController.cs" company="Softo">
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
using PlataformaRio2C.Infra.CrossCutting.Resources.Helpers;

namespace PlataformaRio2C.Web.Site.Controllers
{
    /// <summary>HomeController</summary>
    [Authorize(Order = 1)]
    public class HomeController : BaseController
    {
        private readonly IdentityAutenticationService identityController;

        /// <summary>Initializes a new instance of the <see cref="HomeController"/> class.</summary>
        /// <param name="identityController">The identity controller.</param>
        public HomeController(IdentityAutenticationService identityController)
            : base(identityController)
        {
            this.identityController = identityController;
        }

        /// <summary>Indexes this instance.</summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            try
            {
                var userId = User.Identity.GetUserId<int>();

                if (await this.identityController.IsInRoleAsync(userId, "Player"))
                {
                    return RedirectToAction("Index", "Home", new { Area = "Player" });
                }

                if (await this.identityController.IsInRoleAsync(userId, "Producer"))
                {
                    return RedirectToAction("Index", "Home", new { Area = "Producer" });
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

            var cookie = Request.Cookies["MyRio2AdminCCulture"];
            if (cookie != null)
            {
                cookie.Value = culture;   // update cookie value
            }
            else
            {
                cookie = new HttpCookie("MyRio2AdminCCulture");
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

            return RedirectToAction("Index", "Home");
        }
    }
}
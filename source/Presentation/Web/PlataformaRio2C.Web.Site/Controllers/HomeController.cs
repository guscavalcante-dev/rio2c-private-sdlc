// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-28-2019
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
        {
            this.identityController = identityController;
        }

        // GET: Home
        public async Task<ActionResult> Index()
        {
            try
            {
                var userId = User.Identity.GetUserId<int>();

                if (await this.identityController.IsInRoleAsync(userId, "Player"))
                {
                    return RedirectToAction("Index", "Dashboard");
                }

                if (await this.identityController.IsInRoleAsync(userId, "Producer"))
                {
                    return RedirectToAction("Index", "Dashboard", new { area = "ProducerArea" });
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
        /// <param name="returnUrl">The return URL.</param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult SetCulture(string culture, string returnUrl = null)
        {
            if (returnUrl == null && Request.UrlReferrer != null)
            {
                returnUrl = Request.UrlReferrer.PathAndQuery;
            }

            // Validate input
            culture = CultureHelper.GetImplementedCulture(culture);
            RouteData.Values["culture"] = culture;  // set culture

            #region Create/Update cookie culture

            var cookie = Request.Cookies["Rio2CPlafatormCulture"];
            if (cookie != null)
            {
                cookie.Value = culture;   // update cookie value
            }
            else
            {
                cookie = new HttpCookie("Rio2CPlafatormCulture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }

            Response.Cookies.Add(cookie);

            #endregion

            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
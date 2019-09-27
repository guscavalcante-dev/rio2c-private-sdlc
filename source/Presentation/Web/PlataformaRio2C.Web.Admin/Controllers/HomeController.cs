// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-26-2019
// ***********************************************************************
// <copyright file="HomeController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MediatR;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Resources.Helpers;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;
using Role = PlataformaRio2C.Domain.Constants.Role;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    /// <summary>HomeController</summary>
    [AjaxAuthorize(Role.Admin)]
    public class HomeController : BaseController
    {
        /// <summary>Initializes a new instance of the <see cref="HomeController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        public HomeController(IMediator commandBus, IdentityAutenticationService identityController)
            : base(commandBus, identityController)
        {
        }

        /// <summary>Indexes this instance.</summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper("Dashboard", new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper("Dashboard", Url.Action("Index", "Home", new { Area = "" }))
            });

            #endregion

            try
            {
                //if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                //{
                //    return RedirectToAction("Index", "Dashboard");
                //}

                //return RedirectToAction("Login", "Account");
            }
            catch (Exception)
            {
                return RedirectToAction("LogOff", "Account");
            }

            return View();
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

            var cookie = Request.Cookies["MyRio2CAdminCulture"];
            if (cookie != null)
            {
                cookie.Value = culture;   // update cookie value
            }
            else
            {
                cookie = new HttpCookie("MyRio2CAdminCulture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }

            Response.Cookies.Add(cookie);

            #endregion

            if (returnUrl == null && Request.UrlReferrer != null)
            {
                returnUrl = Request.UrlReferrer.PathAndQuery;
            }

            returnUrl = returnUrl?
                            .Replace(oldCulture.ToLowerInvariant(), culture?.ToLowerInvariant())
                            .Replace(oldCulture, culture?.ToLowerInvariant());

            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
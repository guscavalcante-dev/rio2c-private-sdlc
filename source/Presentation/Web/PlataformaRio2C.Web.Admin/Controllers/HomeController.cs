// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-16-2019
// ***********************************************************************
// <copyright file="HomeController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using MediatR;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Resources.Helpers;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;
using PlataformaRio2C.Application.CQRS.Commands.User;
using System.Text.RegularExpressions;
using PlataformaRio2C.Application.Common;
using Constants = PlataformaRio2C.Domain.Constants;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    /// <summary>HomeController</summary>
    [AjaxAuthorize(Order = 1, Roles = Constants.Role.AnyAdmin)]
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

            ViewBag.Breadcrumb = new BreadcrumbHelper("Dashboard", null);

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
        public async Task<ActionResult> SetCulture(string culture, string oldCulture, string returnUrl = null)
        {
            // Validate input
            culture = CultureHelper.GetImplementedCulture(culture);
            RouteData.Values["culture"] = culture;  // set culture

            #region Create/Update cookie culture

            if (this.AdminAccessControlDto != null)
            {
                var result = await this.CommandBus.Send(new UpdateUserInterfaceLanguage(
                    this.AdminAccessControlDto.User.Uid,
                    culture));
            }

            var cookie = ApplicationCookieControl.SetCookie(culture, Response.Cookies[Constants.CookieName.MyRio2CAdminCookie], Constants.CookieName.MyRio2CAdminCookie);
            Response.Cookies.Add(cookie);

            #endregion

            if (returnUrl == null && Request.UrlReferrer != null)
            {
                returnUrl = Request.UrlReferrer.PathAndQuery;
            }

            if (!string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = Regex.Replace(returnUrl, oldCulture, culture, RegexOptions.IgnoreCase);
            }

            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
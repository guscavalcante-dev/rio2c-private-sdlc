// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-18-2019
// ***********************************************************************
// <copyright file="HomeController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Linq;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using System.Threading.Tasks;
using System.Web.Mvc;
using MediatR;
using PlataformaRio2C.Infra.CrossCutting.Resources.Helpers;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Web.Site.Filters;
using PlataformaRio2C.Application.CQRS.Commands.User;
using System.Text.RegularExpressions;
using PlataformaRio2C.Application.Common;
using Constants = PlataformaRio2C.Domain.Constants;

namespace PlataformaRio2C.Web.Site.Controllers
{
    /// <summary>HomeController</summary>
    [AjaxAuthorize(Order = 1)]
    [AuthorizeCollaboratorType(Order = 2)]
    public class HomeController : BaseController
    {
        /// <summary>Initializes a new instance of the <see cref="HomeController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        public HomeController(
            IMediator commandBus, 
            IdentityAutenticationService identityController)
            : base(commandBus, identityController)
        {
        }

        /// <summary>Indexes this instance.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.Dashboard, null);

            #endregion

            return View("Index");

            //try
            //{


            //    //return RedirectToAction("Index", "Home", new { Area = "Player" });

            //    //var userId = User.Identity.GetUserId<int>();

            //    //if (await this.identityController.IsInRoleAsync(userId, "Player"))
            //    //{
            //    //    return RedirectToAction("Index", "Home", new { Area = "Player" });
            //    //}

            //    //if (await this.identityController.IsInRoleAsync(userId, "Producer"))
            //    //{
            //    //    return RedirectToAction("Index", "Home", new { Area = "Producer" });
            //    //}

            //    //return RedirectToAction("LogOff", "Account");
            //}
            //catch (Exception)
            //{
            //    return RedirectToAction("LogOff", "Account");
            //}
        }

        /// <summary>Sets the culture.</summary>
        /// <param name="culture">The culture.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<ActionResult> SetCulture(string culture, string returnUrl = null)
        {
            // Validate input
            culture = CultureHelper.GetImplementedCulture(culture);
            RouteData.Values["culture"] = culture;  // set culture

            #region Create/Update cookie culture

            if (this.UserAccessControlDto != null)
            {
                var result = await this.CommandBus.Send(new UpdateUserInterfaceLanguage(
                    this.UserAccessControlDto.User.Uid,
                    culture));
            }

            var cookie = ApplicationCookieControl.SetCookie(culture, Response.Cookies[Constants.CookieName.MyRio2CCookie], Constants.CookieName.MyRio2CCookie);
            Response.Cookies.Add(cookie);

            #endregion

            if (returnUrl == null && Request.UrlReferrer != null)
            {
                returnUrl = Request.UrlReferrer.PathAndQuery;
            }

            if (!string.IsNullOrEmpty(returnUrl) && CultureHelper.Cultures?.Any() == true)
            {
                foreach (var configuredCulture in CultureHelper.Cultures)
                {
                    returnUrl = Regex.Replace(returnUrl, configuredCulture, culture, RegexOptions.IgnoreCase);
                }
            }

            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
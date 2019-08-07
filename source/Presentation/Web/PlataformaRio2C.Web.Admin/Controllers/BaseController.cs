// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-04-2019
// ***********************************************************************
// <copyright file="BaseController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Infra.CrossCutting.Resources.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using System.Web.Routing;
using MediatR;
using Microsoft.AspNet.Identity;
using PlataformaRio2C.Application.CQRS.Queries;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    /// <summary></summary>
    public class BaseController : Controller
    {
        protected IMediator CommandBus;
        private readonly IdentityAutenticationService identityController;
        protected string UserInterfaceLanguage;
        protected Guid? EditionUid;
        protected int UserId;
        protected string UserName;
        protected IList<string> UserRoles;
        protected string Area;

        /// <summary>Initializes a new instance of the <see cref="BaseController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityControlle">The identity controlle.</param>
        public BaseController(IMediator commandBus, IdentityAutenticationService identityControlle)
        {
            this.CommandBus = commandBus;
            this.identityController = identityControlle;
        }

        /// <summary>Begins to invoke the action in the current controller context.</summary>
        /// <param name="callback">The callback.</param>
        /// <param name="state">The state.</param>
        /// <returns>Returns an IAsyncController instance.</returns>
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            var changedCultureRouteValue = this.ValidateCulture();
            if (changedCultureRouteValue)
            {
                return base.BeginExecuteCore(callback, state);
            }

            var changedEditionRouteValue = this.ValidateEdition();
            if (changedEditionRouteValue)
            {
                return base.BeginExecuteCore(callback, state);
            }

            this.SetUserInfo();
            this.SetArea();

            return base.BeginExecuteCore(callback, state);
        }

        /// <summary>Validates the culture.</summary>
        /// <returns></returns>
        private bool ValidateCulture()
        {
            // Attempt to read the culture cookie from Request
            var routeCulture = RouteData.Values["culture"] as string;
            var cookieCulture = Request.Cookies["MyRio2CAdminCulture"]?.Value;
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
                return true;
            }

            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            this.UserInterfaceLanguage = cultureName;

            return false;
        }

        /// <summary>Validates the edition.</summary>
        /// <returns></returns>
        private bool ValidateEdition()
        {
            // Attempt to read the edition cookie from Request
            var routeEdition = (RouteData.Values["edition"] as string).ToInt();

            var activeEditions = this.CommandBus.Send(new FindAllEditionsByIsActive()).Result;
            if (activeEditions?.Any() != true)
            {
                return false;
            }

            ViewBag.ActiveEditions = activeEditions;

            // Check current edition on url parameter
            if (routeEdition.HasValue)
            {
                var currentRoute = activeEditions.FirstOrDefault(ae => ae.UrlCode == routeEdition);
                if (currentRoute != null)
                {
                    ViewBag.EditionUid = this.EditionUid = currentRoute.Uid;
                    return false;
                }
            }

            // Update edition on url parameter
            var currentEdition = activeEditions.FirstOrDefault(ae => ae.IsCurrent);
            if (currentEdition == null)
            {
                return false;
            }

            var routes = new RouteValueDictionary(RouteData.Values);

            // Add or change culture on routes
            if (!routes.ContainsKey("edition"))
            {
                routes.Add("edition", currentEdition.UrlCode);
            }
            else
            {
                routes["edition"] = currentEdition.UrlCode;
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

            return true;
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
            ViewBag.UserRoles = this.UserRoles = this.identityController.FindAllRolesByUserIdAsync(this.UserId).Result;
        }

        //// GET: Base
        //protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        //{
        //    string cultureName = null;

        //    // Attempt to read the culture cookie from Request
        //    HttpCookie cultureCookie = Request.Cookies["_culture"];
        //    if (cultureCookie != null)
        //        cultureName = cultureCookie.Value;
        //    else
        //        cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ?
        //                Request.UserLanguages[0] :  // obtain it from HTTP header AcceptLanguages
        //                null;
        //    // Validate culture name
        //    cultureName = CultureHelper.GetImplementedCulture("pt-BR"); // This is safe

        //    // Modify current thread's cultures            
        //    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
        //    Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;


        //    // Save culture in a cookie
        //    HttpCookie cookie = Request.Cookies["_culture"];
        //    if (cookie != null)
        //        cookie.Value = cultureName;   // update cookie value
        //    else
        //    {
        //        cookie = new HttpCookie("_culture");
        //        cookie.Value = cultureName;
        //        cookie.Expires = DateTime.Now.AddYears(1);
        //    }
        //    Response.Cookies.Add(cookie);

        //    return base.BeginExecuteCore(callback, state);
        //}

        //[AllowAnonymous]
        //public ActionResult SetCulture(string culture, string returnUrl = null)
        //{
        //    if (returnUrl == null)
        //    {
        //        returnUrl = Request.UrlReferrer.PathAndQuery;
        //    }

        //    // Validate input
        //    culture = CultureHelper.GetImplementedCulture(culture);
        //    // Save culture in a cookie
        //    HttpCookie cookie = Request.Cookies["_culture"];
        //    if (cookie != null)
        //        cookie.Value = culture;   // update cookie value
        //    else
        //    {
        //        cookie = new HttpCookie("_culture");
        //        cookie.Value = culture;
        //        cookie.Expires = DateTime.Now.AddYears(1);
        //    }
        //    Response.Cookies.Add(cookie);


        //    if (Url.IsLocalUrl(returnUrl))
        //    {
        //        return Redirect(returnUrl);
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }
        //}
    }
}
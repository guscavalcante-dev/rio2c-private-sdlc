// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-08-2019
// ***********************************************************************
// <copyright file="BaseController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Infra.CrossCutting.Resources.Helpers;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Mvc;
using System.Web.Routing;
using MediatR;
using Microsoft.AspNet.Identity;
using PlataformaRio2C.Application.CQRS.Queries;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Domain.Constants;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    /// <summary></summary>
    public class BaseController : Controller
    {
        private ActionResult beginExecuteCoreActionResult;

        protected IMediator CommandBus;
        protected IdentityAutenticationService IdentityController;

        protected EditionDto EditionDto;
        protected AdminAccessControlDto AdminAccessControlDto;

        protected string UserInterfaceLanguage;
        protected string Area; //TODO: Remove area from BaseController

        /// <summary>Initializes a new instance of the <see cref="BaseController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityControlle">The identity controlle.</param>
        public BaseController(IMediator commandBus, IdentityAutenticationService identityControlle)
        {
            this.CommandBus = commandBus;
            this.IdentityController = identityControlle;
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

        /// <summary>Called before the action method is invoked.</summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Result = this.beginExecuteCoreActionResult;

            base.OnActionExecuting(filterContext);
        }

        /// <summary>Validates the culture.</summary>
        /// <returns></returns>
        private bool ValidateCulture()
        {
            // Attempt to read the culture cookie from Request
           
            var routeCulture = RouteData.Values["culture"] as string;
            var cookieCulture = Request.Cookies[Role.MyRio2CAdminCookie]?.Value;
            var cultureName = 
                              routeCulture ??
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

                //HttpContext.Response.RedirectToRoute(routes);
                this.beginExecuteCoreActionResult = this.RedirectToRoute(routes);

                return true;
            }

            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            ViewBag.UserInterfaceLanguage = this.UserInterfaceLanguage = cultureName;

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
                var edition = activeEditions.FirstOrDefault(ae => ae.UrlCode == routeEdition);
                if (edition != null)
                {
                    ViewBag.EditionDto = this.EditionDto = edition;
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

            //HttpContext.Response.RedirectToRoute(routes);
            this.beginExecuteCoreActionResult = this.RedirectToRoute(routes);

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
            if (this.IdentityController == null)
            {
                return;
            }

            var userId = User.Identity.GetUserId<int>();
            if (!User.Identity.IsAuthenticated || userId <= 0)
            {
                return;
            }

            ViewBag.AdminAccessControlDto = this.AdminAccessControlDto = this.CommandBus.Send(new FindAdminAccessControlDto(userId, this.EditionDto?.Id ?? 0, this.UserInterfaceLanguage)).Result;
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
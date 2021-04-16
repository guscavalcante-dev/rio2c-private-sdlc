// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-23-2019
// ***********************************************************************
// <copyright file="BaseController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Infra.CrossCutting.Resources.Helpers;
using System;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using System.Web.Routing;
using MediatR;
using Microsoft.AspNet.Identity;
using PlataformaRio2C.Application.CQRS.Queries;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Web.Site.Helpers;
using Constants = PlataformaRio2C.Domain.Constants;
using System.Collections.Generic;

namespace PlataformaRio2C.Web.Site.Controllers
{
    /// <summary>BaseController</summary>
    public class BaseController : Controller
    {
        private ActionResult beginExecuteCoreActionResult;

        protected IMediator CommandBus;
        protected IdentityAutenticationService IdentityController;

        protected EditionDto EditionDto;
        protected UserAccessControlDto UserAccessControlDto;

        protected string Environment;
        protected string UserInterfaceLanguage;
        protected string Area; //TODO: Remove area from BaseController

        /// <summary>Initializes a new instance of the <see cref="BaseController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        public BaseController(IMediator commandBus, IdentityAutenticationService identityController)
        {
            this.CommandBus = commandBus;
            this.IdentityController = identityController;
        }

        /// <summary>Begins to invoke the action in the current controller context.</summary>
        /// <param name="callback">The callback.</param>
        /// <param name="state">The state.</param>
        /// <returns>Returns an IAsyncController instance.</returns>
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            // Set environment
            this.SetEnvironment();

            // Culture
            var changedCultureRouteValue = this.ValidateCulture();
            if (changedCultureRouteValue)
            {
                return base.BeginExecuteCore(callback, state);
            }

            // Edition
            var changedEditionRouteValue = this.ValidateEdition();
            if (changedEditionRouteValue)
            {
                return base.BeginExecuteCore(callback, state);
            }

            this.SetUserInfo();
            this.SetArea();

            // Edition User
            var changedEditionWithUserRouteValue = this.ValidateEditionUser();
            if (changedEditionWithUserRouteValue)
            {
                return base.BeginExecuteCore(callback, state);
            }

            // Onboarding
            var changedOnboardingRoute = this.ValidateOnboarding();
            if (changedOnboardingRoute)
            {
                return base.BeginExecuteCore(callback, state);
            }

            return base.BeginExecuteCore(callback, state);
        }

        /// <summary>Called before the action method is invoked.</summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Result = this.beginExecuteCoreActionResult;

            var userAccessControlDto = (UserAccessControlDto)filterContext.Controller.ViewBag.UserAccessControlDto;

            if (userAccessControlDto != null && !userAccessControlDto.IsAdmin())
            {
                ViewBag.ActiveEditions = ((List<EditionDto>)ViewBag.ActiveEditions)
                                            .Where(e => userAccessControlDto.EditionAttendeeCollaborators
                                                                            .Select(eac => eac.EditionId)
                                                                            .Contains(e.Id)
                                                                            ).ToList();

            }

            base.OnActionExecuting(filterContext);
        }

        /// <summary>Sets the environment.</summary>
        private void SetEnvironment()
        {
            ViewBag.Environment = this.Environment = ConfigurationManager.AppSettings["Environment"];
        }

        /// <summary>Validates the culture.</summary>
        /// <returns></returns>
        private bool ValidateCulture()
        {
            // Attempt to read the culture cookie from Request

            var routeCulture = RouteData.Values["culture"] as string;
            var cookieCulture = Request.Cookies[Constants.CookieName.MyRio2CCookie]?.Value;
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

                //HttpContext.Response.RedirectToRoute(routes);
                this.beginExecuteCoreActionResult = this.RedirectToRoute(routes);

                return true;
            }

            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            ViewBag.UserInterfaceLanguage = this.UserInterfaceLanguage = cultureName?.ToLowerInvariant();

            return false;
        }

        /// <summary>Validates the edition.</summary>
        /// <returns></returns>
        private bool ValidateEdition()
        {
            // Attempt to read the edition cookie from Request
            var routeEdition = (RouteData.Values["edition"] as string).ToInt();

            var activeEditions = this.CommandBus.Send(new FindAllEditionsDtosAsync()).Result;
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

            ViewBag.UserAccessControlDto = this.UserAccessControlDto = this.CommandBus.Send(new FindUserAccessControlDto(userId, this.EditionDto?.Id ?? 0, this.UserInterfaceLanguage)).Result;
        }

        /// <summary>Sets the area.</summary>
        private void SetArea()
        {
            this.Area = ViewBag.Area = RouteData.Values["Area"] as string;
        }

        /// <summary>
        /// Validates the edition user.
        /// </summary>
        /// <returns></returns>
        private bool ValidateEditionUser()
        {
            var activeEditions = (List<EditionDto>)ViewBag.ActiveEditions;
            if (activeEditions?.Any() != true)
            {
                return false;
            }

            var userAccessControlDto = (UserAccessControlDto)ViewBag.UserAccessControlDto;
            if (userAccessControlDto == null)
            {
                return false;
            }

            var userActiveEditions = activeEditions.Where(e => userAccessControlDto.EditionAttendeeCollaborators != null
                                                               && userAccessControlDto.EditionAttendeeCollaborators
                                                                    .Select(eac => eac.EditionId)
                                                                    .Contains(e.Id)
                                                                    ).ToList();

            if (userActiveEditions == null)
            {
                return false;
            }

            //If user doesn't participating at current edition (this.EditionDto.Edition.Id), redirects user to last participated edition.
            if (!userActiveEditions.Select(e => e.Id).Contains(this.EditionDto.Edition.Id))
            {
                if (userAccessControlDto?.EditionAttendeeCollaborators?.Any() != true)
                {
                    return false;
                }

                var lastParticipatedEditionId = userAccessControlDto.EditionAttendeeCollaborators.Max(ac => ac.EditionId);
                var activeEditionDtos = this.CommandBus.Send(new FindAllEditionsDtosAsync()).Result;

                var lastParticipatedEdition = activeEditionDtos?.FirstOrDefault(w => w.Id == lastParticipatedEditionId);
                if(lastParticipatedEdition == null)
                {
                    return false;
                }

                ViewBag.EditionDto = this.EditionDto = lastParticipatedEdition;

                var routes = new RouteValueDictionary(RouteData.Values);

                // Add or change edition on routes
                if (!routes.ContainsKey("edition"))
                {
                    routes.Add("edition", lastParticipatedEdition.UrlCode);
                }
                else
                {
                    routes["edition"] = lastParticipatedEdition.UrlCode;
                }

                this.beginExecuteCoreActionResult = this.RedirectToRoute(routes);

                return true;
            }

            return false;
        }

        /// <summary>Validates the onboarding.</summary>
        /// <returns></returns>
        private bool ValidateOnboarding()
        {
            var controller = RouteData.Values["controller"] as string;
            var action = RouteData.Values["action"] as string;

            if ((controller?.ToLower() == "account" && action?.ToLower() == "onboarding")
                || this.UserAccessControlDto?.IsOnboardingPending() != true
                || OnboardingAllowedRoutesHelper.IsRouteAllowed(RouteData.Values["controller"] as string, RouteData.Values["action"] as string))
            {
                return false;
            }

            var routes = new RouteValueDictionary(RouteData.Values);
            routes["controller"] = "Onboarding";
            routes["action"] = "Index";

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
    }
}
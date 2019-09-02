// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-02-2019
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
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Web.Site.Helpers;

namespace PlataformaRio2C.Web.Site.Controllers
{
    /// <summary>BaseController</summary>
    public class BaseController : Controller
    {
        protected IMediator commandBus;
        private readonly IdentityAutenticationService identityController;
        protected string UserInterfaceLanguage;
        protected int? EditionId;
        protected Guid? EditionUid;
        protected int UserId;
        protected Guid UserUid;
        protected string UserName;
        protected IList<Role> UserRoles;
        protected IList<TicketType> UserTicketTypes;
        protected string Area;
        protected bool OnboardingPending;

        /// <summary>Initializes a new instance of the <see cref="BaseController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        public BaseController(IMediator commandBus, IdentityAutenticationService identityController)
        {
            this.commandBus = commandBus;
            this.identityController = identityController;
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
            this.ValidateOnboarding();

            return base.BeginExecuteCore(callback, state);
        }

        /// <summary>Validates the culture.</summary>
        /// <returns></returns>
        private bool ValidateCulture()
        {
            // Attempt to read the culture cookie from Request
            var routeCulture = RouteData.Values["culture"] as string;
            var cookieCulture = Request.Cookies["MyRio2CCulture"]?.Value;
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

            ViewBag.UserInterfaceLanguage = this.UserInterfaceLanguage = cultureName;

            return false;
        }

        /// <summary>Validates the edition.</summary>
        /// <returns></returns>
        private bool ValidateEdition()
        {
            // Attempt to read the edition cookie from Request
            var routeEdition = (RouteData.Values["edition"] as string).ToInt();

            var activeEditions = this.commandBus.Send(new FindAllEditionsByIsActive()).Result;
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
                    ViewBag.EditionId = this.EditionId = currentRoute.Id;
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

        /// <summary>Sets the user information.</summary>
        private void SetUserInfo()
        {
            if (this.identityController == null)
            {
                return;
            }

            ViewBag.UserId = this.UserId = User.Identity.GetUserId<int>();
            if (!User.Identity.IsAuthenticated || this.UserId <= 0)
            {
                return;
            }

            var accessControllDto = this.commandBus.Send(new FindAccessControlDto(this.EditionUid ?? Guid.NewGuid(), this.UserId, this.UserInterfaceLanguage)).Result;
            if (accessControllDto == null)
            {
                return;
            }

            ViewBag.UserUid = this.UserUid = accessControllDto.User.Uid;
            ViewBag.FullName = this.UserName = accessControllDto.User.Name.UppercaseFirstOfEachWord(this.UserInterfaceLanguage);
            ViewBag.FirstName = this.UserName?.GetFirstWord();
            ViewBag.UserRoles = this.UserRoles = accessControllDto.Roles?.ToList();
            ViewBag.UserTicketTypes = this.UserTicketTypes = accessControllDto.TicketTypes?.ToList();
            this.OnboardingPending = accessControllDto.IsPendingAttendeeCollaboratorOnboarding || accessControllDto.IsPendingAttendeeOrganizationOnboarding;
        }

        /// <summary>Sets the area.</summary>
        private void SetArea()
        {
            this.Area = ViewBag.Area = RouteData.Values["Area"] as string;
        }

        /// <summary>Validates the onboarding.</summary>
        private void ValidateOnboarding()
        {
            var controllerName = RouteData.Values["controller"] as string;
            var actionName = RouteData.Values["action"] as string;

            // Onboarding urls validation
            if (!OnboardingAllowedRoutesHelper.IsRouteAllowed(controllerName, actionName))
            {
                if (!this.OnboardingPending)
                {
                    return;
                }

                var routes = new RouteValueDictionary(RouteData.Values);
                routes["controller"] = "Onboarding";
                routes["action"] = "Index";
                HttpContext.Response.RedirectToRoute(routes);
            }
        }
    }
}
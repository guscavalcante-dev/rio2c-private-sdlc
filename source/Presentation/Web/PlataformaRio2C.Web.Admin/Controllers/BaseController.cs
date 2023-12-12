// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-11-2023
// ***********************************************************************
// <copyright file="BaseController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Infra.CrossCutting.Resources.Helpers;
using System;
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
using Constants = PlataformaRio2C.Domain.Constants;
using System.Collections.Generic;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    /// <summary>
    /// BaseController
    /// </summary>
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
            //Fix to enable RouteAttribute at MVC 5.0+
            var routeData = RouteData;
            if (routeData.Values.ContainsKey("MS_DirectRouteMatches"))
            {
                routeData = ((IEnumerable<RouteData>)routeData.Values["MS_DirectRouteMatches"]).First();
            }

            var changedCultureRouteValue = this.ValidateCulture(routeData);
            if (changedCultureRouteValue)
            {
                return base.BeginExecuteCore(callback, state);
            }

            var changedEditionRouteValue = this.ValidateEdition(routeData);
            if (changedEditionRouteValue)
            {
                return base.BeginExecuteCore(callback, state);
            }

            this.SetUserInfo();
            this.SetArea(routeData);

            // Edition User
            var changedEditionWithUserRouteValue = this.ValidateEditionUser(routeData);
            if (changedEditionWithUserRouteValue)
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

            var adminAccessControlDto = (AdminAccessControlDto)filterContext.Controller.ViewBag.AdminAccessControlDto;

            if (adminAccessControlDto != null && !adminAccessControlDto.IsAdmin())
            {
                ViewBag.ActiveEditions = ((List<EditionDto>)ViewBag.ActiveEditions)
                                            .Where(e => adminAccessControlDto.EditionAttendeeCollaborators
                                                                            .Select(eac => eac.EditionId)
                                                                            .Contains(e.Id)
                                                                            ).ToList();

            }
            else if (adminAccessControlDto == null && (adminAccessControlDto?.IsAdmin() == null || !adminAccessControlDto?.IsAdmin() == true))
            {
                //Clear the ActiveEditions only when is not admin.
                //Without this validation, "ViewBag.ActiveEditions" will always contains all Editions.
                ViewBag.ActiveEditions = new List<EditionDto>();
            }

            base.OnActionExecuting(filterContext);
        }

        /// <summary>Validates the culture.</summary>
        /// <returns></returns>
        private bool ValidateCulture(RouteData routeData)
        {
            // Attempt to read the culture cookie from Request

            var routeCulture = routeData.Values["culture"] as string;
            var cookieCulture = Request.Cookies[Constants.CookieName.MyRio2CAdminCookie]?.Value;
            var cultureName = routeCulture ??
                              cookieCulture ??
                              (Request.UserLanguages != null && Request.UserLanguages.Length > 0 ? Request.UserLanguages[0] : null);

            // Validate culture name
            cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe

            if (routeData.Values["culture"] as string != cultureName)
            {
                var routes = new RouteValueDictionary(routeData.Values);

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
        private bool ValidateEdition(RouteData routeData)
        {
            // Attempt to read the edition cookie from Request
            var routeEdition = (routeData.Values["edition"] as string).ToInt();

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

            var routes = new RouteValueDictionary(routeData.Values);

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
        private void SetArea(RouteData routeData)
        {
            this.Area = ViewBag.Area = routeData.Values["Area"] as string;
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

        /// <summary>
        /// Validates the edition user.
        /// </summary>
        /// <returns></returns>
        private bool ValidateEditionUser(RouteData routeData)
        {
            var activeEditions = (List<EditionDto>)ViewBag.ActiveEditions;
            if (activeEditions?.Any() != true)
            {
                return false;
            }

            var AdminAccessControlDto = (AdminAccessControlDto)ViewBag.AdminAccessControlDto;
            if (AdminAccessControlDto == null)
            {
                return false;
            }

            var adminActiveEditions = activeEditions.Where(e => AdminAccessControlDto.EditionAttendeeCollaborators != null
                                                                && AdminAccessControlDto.EditionAttendeeCollaborators.Select(eac => eac.EditionId).Contains(e.Id)).ToList();

            if (adminActiveEditions == null)
            {
                return false;
            }

            //If user isn't Admin and doesn't participating at current edition, redirects user to last participated edition.
            if (!AdminAccessControlDto.IsAdmin() && !adminActiveEditions.Select(e => e.Id).Contains(this.EditionDto.Edition.Id))
            {
                if (AdminAccessControlDto?.EditionAttendeeCollaborators?.Any() != true)
                {
                    return false;
                }

                var lastParticipatedEditionId = AdminAccessControlDto.EditionAttendeeCollaborators.Max(ac => ac.EditionId);
                var activeEditionDtos = this.CommandBus.Send(new FindAllEditionsDtosAsync()).Result;

                var lastParticipatedEdition = activeEditionDtos?.FirstOrDefault(w => w.Id == lastParticipatedEditionId);
                if (lastParticipatedEdition == null)
                {
                    return false;
                }

                ViewBag.EditionDto = this.EditionDto = lastParticipatedEdition;

                var routes = routeData.Values;

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
    }
}
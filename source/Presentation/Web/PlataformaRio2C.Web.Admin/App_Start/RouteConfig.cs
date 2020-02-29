// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-25-2020
// ***********************************************************************
// <copyright file="RouteConfig.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Web.Mvc;
using System.Web.Routing;

namespace PlataformaRio2C.Web.Admin
{
    /// <summary>RouteConfig</summary>
    public static class RouteConfig
    {
        /// <summary>Registers the routes.</summary>
        /// <param name="routes">The routes.</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            AreaRegistration.RegisterAllAreas();

            // Route to include edition
            routes.MapRoute(
                "DefaultWithCultureAndEdition",
                "{culture}/{edition}/{controller}/{action}/{id}",
                new { culture = string.Empty, edition = string.Empty, controller = "Home", action = "Index", area = "", id = UrlParameter.Optional },
                new { culture = @"^[a-zA-Z]{2}(\-[a-zA-Z]{2})?$", edition = @"^[0-9]{4}$" },
                new[] { "PlataformaRio2C.Web.Admin.Controllers" });

            // Route to include culture
            routes.MapRoute(
                "DefaultWithCulture",
                "{culture}/{controller}/{action}/{id}",
                new { culture = string.Empty, controller = "Home", action = "Index", area = "", id = UrlParameter.Optional },
                new { culture = @"^[a-zA-Z]{2}(\-[a-zA-Z]{2})?$" },
                new[] { "PlataformaRio2C.Web.Admin.Controllers" });

            // Default route
            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", area = "", id = UrlParameter.Optional },
                new[] { "PlataformaRio2C.Web.Admin.Controllers" }
            );
        }
    }
}
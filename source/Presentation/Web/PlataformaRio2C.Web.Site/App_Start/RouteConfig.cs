// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-01-2019
// ***********************************************************************
// <copyright file="RouteConfig.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Web.Mvc;
using System.Web.Routing;

namespace PlataformaRio2C.Web.Site
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

            //routing to include culture
            routes.MapRoute(
                "DefaultWithCulture",
                "{culture}/{controller}/{action}/{id}",
                new { culture = string.Empty, controller = "Home", action = "Index", area = "", id = UrlParameter.Optional },
                new { culture = @"^[a-zA-Z]{2}(\-[a-zA-Z]{2})?$" },
                new[] { "PlataformaRio2C.Web.Site.Controllers" });

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", area ="", id = UrlParameter.Optional },
                new[] { "PlataformaRio2C.Web.Site.Controllers" }
            );
        }
    }
}

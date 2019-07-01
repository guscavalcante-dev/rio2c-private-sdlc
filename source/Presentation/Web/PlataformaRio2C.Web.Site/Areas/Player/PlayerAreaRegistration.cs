// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 07-01-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-01-2019
// ***********************************************************************
// <copyright file="PlayerAreaRegistration.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Web.Mvc;

namespace PlataformaRio2C.Web.Site.Areas.Player
{
    /// <summary>PlayerAreaRegistration</summary>
    public class PlayerAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Player";
            }
        }

        /// <summary>Registers an area in an ASP.NET MVC application using the specified area's context information.</summary>
        /// <param name="context">Encapsulates the information that is required in order to register the area.</param>
        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PlayerWithCulture",
                "{culture}/Player/{controller}/{action}/{id}",
                new { culture = string.Empty, controller = "Home", action = "Index", area = AreaName, id = UrlParameter.Optional },
                new { culture = @"^[a-zA-Z]{2}(\-[a-zA-Z]{2})?$" },
                new[] { "PlataformaRio2C.Web.Site.Areas.Player.Controllers" }
            );

            context.MapRoute(
                "Player",
                "Player/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", area = AreaName, id = UrlParameter.Optional },
                new[] { "PlataformaRio2C.Web.Site.Areas.Player.Controllers" }
            );
        }
    }
}
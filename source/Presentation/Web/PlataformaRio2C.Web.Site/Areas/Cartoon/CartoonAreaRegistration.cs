// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Renan Valentim
// Created          : 01-25-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-25-2022
// ***********************************************************************
// <copyright file="CartoonAreaRegistration.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Web.Mvc;

namespace PlataformaRio2C.Web.Site.Areas.Cartoon
{
    /// <summary>CartoonAreaRegistration</summary>
    public class CartoonAreaRegistration : AreaRegistration
    {
        /// <summary>Gets the name of the area to register.</summary>
        public override string AreaName
        {
            get
            {
                return "Cartoon";
            }
        }

        /// <summary>Registers an area in an ASP.NET MVC application using the specified area's context information.</summary>
        /// <param name="context">Encapsulates the information that is required in order to register the area.</param>
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "CartoonWithCultureAndEdition",
                "{culture}/{edition}/" + this.AreaName + "/{controller}/{action}/{id}",
                new { culture = string.Empty, edition = string.Empty, controller = "Home", action = "Index", area = this.AreaName, id = UrlParameter.Optional },
                new { culture = @"^[a-zA-Z]{2}(\-[a-zA-Z]{2})?$", edition = @"^[0-9]{4}$" },
                new[] { "PlataformaRio2C.Web.Site.Areas.Cartoon.Controllers" }
            );

            context.MapRoute(
                "CartoonWithCulture",
                "{culture}/" + this.AreaName + "/{controller}/{action}/{id}",
                new { culture = string.Empty, controller = "Home", action = "Index", area = this.AreaName, id = UrlParameter.Optional },
                new { culture = @"^[a-zA-Z]{2}(\-[a-zA-Z]{2})?$" },
                new[] { "PlataformaRio2C.Web.Site.Areas.Cartoon.Controllers" }
            );

            context.MapRoute(
                "Cartoon",
                this.AreaName + "/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", area = this.AreaName, id = UrlParameter.Optional },
                new[] { "PlataformaRio2C.Web.Site.Areas.Cartoon.Controllers" }
            );
        }
    }
}
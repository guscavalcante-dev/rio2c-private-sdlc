// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Renan Valentim
// Created          : 07-28-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-28-2021
// ***********************************************************************
// <copyright file="AudiovisualAreaRegistration.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Web.Mvc;

namespace PlataformaRio2C.Web.Site.Areas.Audiovisual
{
    /// <summary>AudiovisualAreaRegistration</summary>
    public class AudiovisualAreaRegistration : AreaRegistration
    {
        /// <summary>Gets the name of the area to register.</summary>
        public override string AreaName
        {
            get
            {
                return "Audiovisual";
            }
        }

        /// <summary>Registers an area in an ASP.NET MVC application using the specified area's context information.</summary>
        /// <param name="context">Encapsulates the information that is required in order to register the area.</param>
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "AudiovisualWithCultureAndEdition",
                "{culture}/{edition}/" + this.AreaName + "/{controller}/{action}/{id}",
                new { culture = string.Empty, edition = string.Empty, controller = "Home", action = "Index", area = this.AreaName, id = UrlParameter.Optional },
                new { culture = @"^[a-zA-Z]{2}(\-[a-zA-Z]{2})?$", edition = @"^[0-9]{4}$" },
                new[] { "PlataformaRio2C.Web.Site.Areas.Audiovisual.Controllers" }
            );

            context.MapRoute(
                "AudiovisualWithCulture",
                "{culture}/" + this.AreaName + "/{controller}/{action}/{id}",
                new { culture = string.Empty, controller = "Home", action = "Index", area = this.AreaName, id = UrlParameter.Optional },
                new { culture = @"^[a-zA-Z]{2}(\-[a-zA-Z]{2})?$" },
                new[] { "PlataformaRio2C.Web.Site.Areas.Audiovisual.Controllers" }
            );

            context.MapRoute(
                "Audiovisual",
                this.AreaName + "/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", area = this.AreaName, id = UrlParameter.Optional },
                new[] { "PlataformaRio2C.Web.Site.Areas.Audiovisual.Controllers" }
            );
        }
    }
}
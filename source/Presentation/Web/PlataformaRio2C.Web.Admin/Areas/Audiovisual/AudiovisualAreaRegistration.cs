// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 03-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-04-2020
// ***********************************************************************
// <copyright file="AudiovisualAreaRegistration.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Web.Mvc;

namespace PlataformaRio2C.Web.Admin.Areas.Music
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
                $"{this.AreaName}WithCultureAndEdition",
                "{culture}/{edition}/" + this.AreaName + "/{controller}/{action}/{id}",
                new { culture = string.Empty, edition = string.Empty, controller = "Home", action = "Index", area = this.AreaName, id = UrlParameter.Optional },
                new { culture = @"^[a-zA-Z]{2}(\-[a-zA-Z]{2})?$", edition = @"^[0-9]{4}$" },
                new[] { $"PlataformaRio2C.Web.Admin.Areas.{this.AreaName}.Controllers" }
            );

            context.MapRoute(
                $"{this.AreaName}WithCulture",
                "{culture}/" + this.AreaName + "/{controller}/{action}/{id}",
                new { culture = string.Empty, controller = "Home", action = "Index", area = this.AreaName, id = UrlParameter.Optional },
                new { culture = @"^[a-zA-Z]{2}(\-[a-zA-Z]{2})?$" },
                new[] { $"PlataformaRio2C.Web.Admin.Areas.{this.AreaName}.Controllers" }
            );

            context.MapRoute(
                this.AreaName,
                this.AreaName + "/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", area = this.AreaName, id = UrlParameter.Optional },
                new[] { $"PlataformaRio2C.Web.Admin.Areas.{this.AreaName}.Controllers" }
            );
        }
    }
}
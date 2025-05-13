// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-20-2019
// ***********************************************************************
// <copyright file="ProducerAreaRegistration.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Web.Mvc;

namespace PlataformaRio2C.Web.Site.Areas.Producer
{
    /// <summary>ProducerAreaRegistration</summary>
    public class ProducerAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Producer";
            }
        }

        /// <summary>Registers an area in an ASP.NET MVC application using the specified area's context information.</summary>
        /// <param name="context">Encapsulates the information that is required in order to register the area.</param>
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ProducerWithCultureAndEdition",
                "{culture}/{edition}/" + AreaName + "/{controller}/{action}/{id}",
                new { culture = string.Empty, edition = string.Empty, controller = "Home", action = "Index", area = AreaName, id = UrlParameter.Optional },
                new { culture = @"^[a-zA-Z]{2}(\-[a-zA-Z]{2})?$", edition = @"^[0-9]{4}$" },
                new[] { "PlataformaRio2C.Web.Site.Areas.Producer.Controllers" }
            );

            context.MapRoute(
                "ProducerWithCulture",
                "{culture}/" + AreaName + "/{controller}/{action}/{id}",
                new { culture = string.Empty, controller = "Home", action = "Index", area = AreaName, id = UrlParameter.Optional },
                new { culture = @"^[a-zA-Z]{2}(\-[a-zA-Z]{2})?$" },
                new[] { "PlataformaRio2C.Web.Site.Areas.Producer.Controllers" }
            );

            context.MapRoute(
                "Producer",
                "Producer/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", area = AreaName, id = UrlParameter.Optional },
                new[] { "PlataformaRio2C.Web.Site.Areas.Producer.Controllers" }
            );
        }
    }
}
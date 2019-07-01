// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-01-2019
// ***********************************************************************
// <copyright file="ProducerAreaAreaRegistration.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Web.Mvc;

namespace PlataformaRio2C.Web.Site.Areas.ProducerArea
{
    /// <summary>ProducerAreaAreaRegistration</summary>
    public class ProducerAreaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ProducerArea";
            }
        }

        /// <summary>Registers an area in an ASP.NET MVC application using the specified area's context information.</summary>
        /// <param name="context">Encapsulates the information that is required in order to register the area.</param>
        public override void RegisterArea(AreaRegistrationContext context) 
        {
            //context.MapRoute(
            //    "ProducerArea_default",
            //    "ProducerArea/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional },
            //    namespaces: new[] { "PlataformaRio2C.Web.Site.Areas.ProducerArea.Controllers" }                
            //);

            context.MapRoute(
                "ProducerArea_default",
                "{culture}/ProducerArea/{controller}/{action}/{id}",
                new { action = "Index", culture = string.Empty, area = AreaName, id = UrlParameter.Optional },
                namespaces: new[] { "PlataformaRio2C.Web.Site.Controllers" }
            );
        }
    }
}
// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-01-2019
// ***********************************************************************
// <copyright file="ConferenceController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Web.Mvc;
using PlataformaRio2C.Application.Interfaces.Services;

namespace PlataformaRio2C.Web.Site.Areas.Producer.Controllers
{
    /// <summary>ConferenceController</summary>
    [Authorize(Order = 1, Roles = "Producer")]
    public class ConferenceController : PlataformaRio2C.Web.Site.Controllers.ConferenceController
    {
        /// <summary>Initializes a new instance of the <see cref="ConferenceController"/> class.</summary>
        /// <param name="appService">The application service.</param>
        public ConferenceController(IConferenceAppService appService) : base(appService)
        {
        }
    }
}
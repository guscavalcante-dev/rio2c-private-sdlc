// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-01-2019
// ***********************************************************************
// <copyright file="ScheduleController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Application.Interfaces.Services;
using System.Web.Mvc;

namespace PlataformaRio2C.Web.Site.Areas.Producer.Controllers
{
    /// <summary>ScheduleController</summary>
    [Authorize(Roles = "Producer")]
    public class ScheduleController : PlataformaRio2C.Web.Site.Controllers.ScheduleController
    {
        /// <summary>Initializes a new instance of the <see cref="ScheduleController"/> class.</summary>
        /// <param name="scheduleAppService">The schedule application service.</param>
        public ScheduleController(IScheduleAppService scheduleAppService)
            :base(scheduleAppService)
        {            
        }
    }
}
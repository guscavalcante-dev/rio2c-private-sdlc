// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-07-2019
// ***********************************************************************
// <copyright file="ScheduleController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Application.Interfaces.Services;
using System.Web.Mvc;
using MediatR;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;

namespace PlataformaRio2C.Web.Site.Controllers
{
    /// <summary>ScheduleController</summary>
    //[TermFilter(Order = 2)]
    [Authorize(Order = 1, Roles = "Player,Producer")]
    public class ScheduleController : BaseController
    {
        private readonly IScheduleAppService _scheduleAppService;

        /// <summary>Initializes a new instance of the <see cref="ScheduleController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="scheduleAppService">The schedule application service.</param>
        public ScheduleController(IMediator commandBus, IdentityAutenticationService identityController, IScheduleAppService scheduleAppService)
            : base(commandBus, identityController)
        {
            _scheduleAppService = scheduleAppService;
        }

        // GET: Schedule
        public ActionResult Index()
        {
            //if (!_scheduleAppService.ScheduleIsEnable())
            //{
            //    return View("Disabled");
            //}

            return View();
        }

        public ActionResult Print()
        {
            if (!_scheduleAppService.ScheduleIsEnable())
            {
                //return View("Disabled");
            }

            return View();
        }
    }
}
// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-07-2019
// ***********************************************************************
// <copyright file="ScheduleOneToOneMeetingsConfigController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System.Collections.Generic;
using System.Web.Mvc;
using MediatR;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    /// <summary>ScheduleOneToOneMeetingsConfigController</summary>
    [Authorize(Roles = "Administrator")]
    public class ScheduleOneToOneMeetingsConfigController : BaseController
    {
        private readonly INegotiationConfigService _scheduleOneToOneMeetingsConfigService;

        /// <summary>Initializes a new instance of the <see cref="ScheduleOneToOneMeetingsConfigController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="scheduleOneToOneMeetingsConfigService">The schedule one to one meetings configuration service.</param>
        public ScheduleOneToOneMeetingsConfigController(IMediator commandBus, IdentityAutenticationService identityController, INegotiationConfigService scheduleOneToOneMeetingsConfigService)
            : base(commandBus, identityController)
        {
            _scheduleOneToOneMeetingsConfigService = scheduleOneToOneMeetingsConfigService;
        }

        // GET: ScheduleOneToOneMeetingsConfig
        public ActionResult Index()
        {
            var result = _scheduleOneToOneMeetingsConfigService.GetByEdit();

            if (result != null)
            {
                return View(result);
            }

            return View();
        }

        public ActionResult Update()
        {
            var result = _scheduleOneToOneMeetingsConfigService.GetByEdit();

            if (result != null)
            {
                return View("Index", result);
            }

            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(IEnumerable<NegotiationConfigAppViewModel> datesViewModel)
        {
            var result = _scheduleOneToOneMeetingsConfigService.Update(datesViewModel);

            if (result.IsValid)
            {
                this.StatusMessage("Configurações da rodada de negócio atualizada com sucesso!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);

                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Erro ao salvar cadastro! Verifique o preenchimento dos campos!");

                foreach (var error in result.Errors)
                {
                    var target = error.Target ?? "";
                    ModelState.AddModelError(target, error.Message);
                }
            }

            return View("Index", datesViewModel);
        }
    }
}
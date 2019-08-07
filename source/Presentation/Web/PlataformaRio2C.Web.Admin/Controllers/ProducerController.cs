// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-07-2019
// ***********************************************************************
// <copyright file="ProducerController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Linq;
using System.Web.Mvc;
using MediatR;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    /// <summary>ProducerController</summary>
    [Authorize(Roles = "Administrator")]
    public class ProducerController : BaseController
    {
        private readonly IProducerAppService _appService;

        /// <summary>Initializes a new instance of the <see cref="ProducerController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="appService">The application service.</param>
        public ProducerController(IMediator commandBus, IdentityAutenticationService identityController, IProducerAppService appService)
            : base(commandBus, identityController)
        {
            _appService = appService;
        }

        // GET: Producer
        public ActionResult Index()
        {
            var viewModel = _appService.GetAllSimple();

            if (viewModel != null)
            {
                viewModel = viewModel.OrderBy(e => e.Name).ToList();
            }

            return View(viewModel);
        }


        // GET: Player/Edit
        public ActionResult Edit(Guid Uid)
        {
            var viewModel = _appService.GetByEdit(Uid);            
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProducerEditAppViewModel viewModel)
        {
            var result = _appService.Update(viewModel);

            if (result.IsValid)
            {
                this.StatusMessage("Produtora atualizada com sucesso!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Erro ao atualizar cadastro! Verifique o preenchimento dos campos!");

                foreach (var error in result.Errors)
                {
                    var target = error.Target ?? "";
                    ModelState.AddModelError(target, error.Message);
                }
            }

            return View(viewModel);
        }
    }
}
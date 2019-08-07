// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-07-2019
// ***********************************************************************
// <copyright file="HoldingController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Web.Mvc;
using MediatR;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    /// <summary>HoldingController</summary>
    [Authorize(Roles = "Administrator")]
    public class HoldingController : BaseController
    {
        private readonly IHoldingAppService _appService;

        /// <summary>Initializes a new instance of the <see cref="HoldingController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="appService">The application service.</param>
        public HoldingController(IMediator commandBus, IdentityAutenticationService identityController, IHoldingAppService appService)
            : base(commandBus, identityController)
        {
            _appService = appService;
        }

        // GET: Holding
        public ActionResult Index()
        {
            //var viewModel = _appService.GetAllSimple();

            //if (viewModel != null)
            //{
            //    viewModel = viewModel.OrderBy(e => e.Name).ToList();
            //}

            return View();
        }

        public ActionResult Create()
        {
            var viewModel = _appService.GetEditViewModel();            
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HoldingAppViewModel viewModel)
        {            
            var result = _appService.Create(viewModel);

            if (result.IsValid)
            {
                this.StatusMessage("Holding criado com sucesso!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);

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

            UpdateHoldingViewModelDefaultValues(viewModel);

            return View(viewModel);
        }

        public ActionResult Edit(Guid Uid)
        {
            var result = _appService.Get(Uid);

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HoldingAppViewModel viewModel)
        {            
            var result = _appService.Update(viewModel);

            if (result.IsValid)
            {
                this.StatusMessage("Holding atualizado com sucesso!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);
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

            UpdateHoldingViewModelDefaultValues(viewModel);

            return View(viewModel);
        }


        public ActionResult Delete(Guid Uid)
        {
            var result = _appService.Delete(Uid);

            if (result.IsValid)
            {
                this.StatusMessage("Holding apagado com sucesso!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    this.StatusMessage(error.Message, Infra.CrossCutting.Tools.Enums.StatusMessageType.Danger);                    
                }
            }

            return RedirectToAction("Index");
        }

        private void UpdateHoldingViewModelDefaultValues(HoldingAppViewModel viewModel)
        {            
            viewModel.MergeWith<HoldingAppViewModel>(_appService.GetEditViewModel());
        }
    }
}
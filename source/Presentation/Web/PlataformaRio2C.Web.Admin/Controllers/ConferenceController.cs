// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-07-2019
// ***********************************************************************
// <copyright file="ConferenceController.cs" company="Softo">
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
    /// <summary>ConferenceController</summary>
    [Authorize(Roles = "Administrator")]
    public class ConferenceController : BaseController
    {
        private readonly IConferenceAppService _appService;

        /// <summary>Initializes a new instance of the <see cref="ConferenceController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="appService">The application service.</param>
        public ConferenceController(IMediator commandBus, IdentityAutenticationService identityController, IConferenceAppService appService)
            : base(commandBus, identityController)
        {
            _appService = appService;
        }

        // GET: Conference
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Create()
        {
            var viewModel = _appService.GetEditViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ConferenceEditAppViewModel viewModel)
        {

            var result = _appService.Create(viewModel);

            if (result.IsValid)
            {
                this.StatusMessage("Palestra criada com sucesso!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);

                return RedirectToAction("Index");
            }
            else
            {

                ModelState.AddModelError("", "Erro ao salvar cadastro! Verifique o preenchimento dos campos!");

                foreach (var error in result.Errors)
                {
                    var target = error.Target ?? "";
                    if (target == "ImageUpload")
                    {
                        target = "";
                    }


                    ModelState.AddModelError(target, error.Message);
                }
            }

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Edit(Guid Uid)
        {
            var result = _appService.GetByEdit(Uid);

            return View(result);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid Uid, ConferenceEditAppViewModel viewModel)
        {

            var result = _appService.Update(viewModel);

            if (result.IsValid)
            {
                this.StatusMessage("Palestra atualizada com sucesso!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Erro ao atualizar cadastro! Verifique o preenchimento dos campos!");

                foreach (var error in result.Errors)
                {
                    var target = error.Target ?? "";

                    if (target == "ImageUpload")
                    {
                        target = "";
                    }

                    ModelState.AddModelError(target, error.Message);
                }
            }

            return View(viewModel);
        }



        public ActionResult Delete(Guid Uid)
        {
            var result = _appService.Delete(Uid);

            if (result.IsValid)
            {
                this.StatusMessage("Palestra apagado com sucesso!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);
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


    }
}
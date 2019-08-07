// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-07-2019
// ***********************************************************************
// <copyright file="MusicalCommissionController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MediatR;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    /// <summary>MusicalCommissionController</summary>
    public class MusicalCommissionController : BaseController
    {
        private readonly IMusicalCommissionService _service;
        private readonly IMusicalCommissionAppService _appService;
        private readonly ICollaboratorAppService _collaboratorAppService;
        private readonly ICollaboratorService _collaboratorService;

        /// <summary>Initializes a new instance of the <see cref="MusicalCommissionController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="commissionService">The commission service.</param>
        /// <param name="appService">The application service.</param>
        /// <param name="collaboratorAppService">The collaborator application service.</param>
        /// <param name="collaboratorService">The collaborator service.</param>
        public MusicalCommissionController(
            IMediator commandBus,
            IdentityAutenticationService identityController, 
            IMusicalCommissionService commissionService, 
            IMusicalCommissionAppService appService, 
            ICollaboratorAppService collaboratorAppService, 
            ICollaboratorService collaboratorService)
            : base(commandBus, identityController)
        {
            _service = commissionService;
            _appService = appService;
            _collaboratorAppService = collaboratorAppService;
            _collaboratorService= collaboratorService;
        }

        public ActionResult Index()
        {
            var viewModel = _collaboratorAppService.GetAllSimple().ToList();
            List<CollaboratorItemListAppViewModel> entity = new List<CollaboratorItemListAppViewModel>();

            if (viewModel != null)
            {
                int i = 0;
                foreach (CollaboratorItemListAppViewModel collaborator in viewModel)
                {
                    if (collaborator.MusicalCommissionId != null)
                    {
                        entity.Add(collaborator);
                    }

                    i++;
                }
            }

            return View(entity);
        }

        public ActionResult Create()
        {
            var viewModel = _collaboratorAppService.GetEditViewModel();
            return View(viewModel);
        }

        public ActionResult Edit(Guid uid)
        {
            var viewModel = _collaboratorAppService.GetByEdit(uid);
            return View(viewModel);
        }



        [HttpPost]
        public ActionResult Edit(CollaboratorEditAppViewModel viewModel)
        {
            var result = _collaboratorAppService.Update(viewModel);

            if (result.IsValid)
            {
                this.StatusMessage(string.Format("Colaborador '{0}' atualizado com sucesso!", viewModel.Name), Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);
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

        [HttpPost]
        public ActionResult Create(CollaboratorEditAppViewModel viewModel)
        {
            //var result = _collaboratorAppService.Create(viewModel);
            var result = _appService.Create(viewModel);


            if (result.IsValid)
            {
                //var resultSpeaker = _service.Create();
                //this.StatusMessage("Player criado com sucesso!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);

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

            return View(viewModel);
        }

        public ActionResult Delete(Guid uid)
        {
            var collaborator = _collaboratorService.Get(uid);
            var entity = _appService.GetAllSimple().FirstOrDefault(a => a.collaboratorId == collaborator.Id);

            var result = _collaboratorAppService.Delete(uid);
            var result2 = _appService.Delete(entity.Uid);
            bool message = result.IsValid;

            if (result.IsValid)
            {
                this.StatusMessage("Usuário apagado com sucesso!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    this.StatusMessage(error.Message, Infra.CrossCutting.Tools.Enums.StatusMessageType.Danger);
                }
            }

            return RedirectToAction("Index");

            //return message;
        }
    }
}
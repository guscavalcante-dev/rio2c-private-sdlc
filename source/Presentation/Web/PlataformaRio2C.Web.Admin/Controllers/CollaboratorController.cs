// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-07-2019
// ***********************************************************************
// <copyright file="CollaboratorController.cs" company="Softo">
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
    /// <summary>CollaboratorController</summary>
    [Authorize(Roles = "Administrator")]
    //[Authorize(Roles = "Administrator", Users = "projeto.rio2c@marlin.com.br")]
    public class CollaboratorController : BaseController
    {
        private readonly ICollaboratorAppService _appService;

        /// <summary>Initializes a new instance of the <see cref="CollaboratorController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="appService">The application service.</param>
        public CollaboratorController(IMediator commandBus, IdentityAutenticationService identityController, ICollaboratorAppService appService)
            : base(commandBus, identityController)
        {
            _appService = appService;
        }

        // GET: Player
        public ActionResult Index()
        {
            var viewModel = _appService.All();

            if (viewModel != null)
            {
                viewModel = viewModel.OrderBy(e => e.Name).ToList();
            }        

            return View(viewModel);
        }

        public ActionResult Create()
        {
            var viewModel = _appService.GetEditViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CollaboratorEditAppViewModel viewModel)
        {
            var result = _appService.Create(viewModel);

            if (result.IsValid)
            {
                this.StatusMessage(string.Format("Colaborador '{0}' criado com sucesso!", viewModel.Name), Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);

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

        public ActionResult Edit(Guid Uid)
        {
            var result = _appService.GetByEdit(Uid);

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CollaboratorEditAppViewModel viewModel)
        {
            var result = _appService.Update(viewModel);            

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

        public ActionResult Delete(Guid Uid)
        {
            var viewModel = _appService.Get(Uid);

            var result = _appService.Delete(Uid);

            if (result.IsValid)
            {
                this.StatusMessage(string.Format("Colaborador '{0}' apagado com sucesso!", viewModel.Name), Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);
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


        [HttpGet]
        public ActionResult Schedule(Guid Uid)
        {
            var colllaboratorViewModel = _appService.Get(Uid);

            if (colllaboratorViewModel != null)
            {

            }

            return View(colllaboratorViewModel);
        }
    }
}
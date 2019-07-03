// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-03-2019
// ***********************************************************************
// <copyright file="ProjectController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.AspNet.Identity;
using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Web.Site.Controllers;
using System;
using System.Linq;
using System.Web.Mvc;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;

namespace PlataformaRio2C.Web.Site.Areas.Producer.Controllers
{
    /// <summary>ProjectController</summary>
    //[TermFilter(Order = 2)]
    [Authorize(Order = 1, Roles = "Producer")]
    public class ProjectController : BaseController
    {
        private readonly IProjectAppService _projectAppService;

        /// <summary>Initializes a new instance of the <see cref="ProjectController"/> class.</summary>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="producerProjectAppService">The producer project application service.</param>
        public ProjectController(IdentityAutenticationService identityController, IProjectAppService producerProjectAppService)
            : base(identityController)
        {
            _projectAppService = producerProjectAppService;
        }


        // GET: ProducerArea/Project
        public ActionResult Index()
        {
            int userId = User.Identity.GetUserId<int>();

            var viewModel = _projectAppService.GetAllByUserProducerId(userId);

            ViewBag.RegistrationProjectDisabled = _projectAppService.RegistrationDisabled();
            ViewBag.SendToPlayersDisabled = _projectAppService.SendToPlayersDisabled();

            return View(viewModel);
        }
        public ActionResult Create()
        {
            if (_projectAppService.RegistrationDisabled())
            {
                this.StatusMessage(Messages.RegistrationOfClosedProject, Infra.CrossCutting.Tools.Enums.StatusMessageType.Danger);

                return RedirectToAction("Index");
            }

            int userId = User.Identity.GetUserId<int>();
            if (!_projectAppService.ExceededProjectMaximumPerProducer(userId))
            {
                var viewModel = _projectAppService.GetEditViewModelProducerProject(userId);
                return View(viewModel);
            }
            else
            {
                this.StatusMessage(string.Format(Messages.ProducerMaxProjects, 3), Infra.CrossCutting.Tools.Enums.StatusMessageType.Danger);

                return RedirectToAction("Index");
            }          
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProjectEditAppViewModel viewModel)
        {
            if (_projectAppService.RegistrationDisabled())
            {
                this.StatusMessage(Messages.RegistrationOfClosedProject, Infra.CrossCutting.Tools.Enums.StatusMessageType.Danger);

                return RedirectToAction("Index");
            }

            int userId = User.Identity.GetUserId<int>();

            var result = _projectAppService.Create(viewModel, userId);

            if (result.IsValid)
            {
                this.StatusMessage(Messages.ProjectSuccessfullyCreated, Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);

                return RedirectToAction("Detail", new { uid = viewModel.UIdCreate });
            }
            else
            {
                if (!result.Errors.Any(e => e.Target == "ProjectSubmitted" || e.Target == "ProducerMaxProject"))
                {
                    ModelState.AddModelError("", Messages.ErrorSavingProject);
                }

                foreach (var error in result.Errors)
                {
                    var target = error.Target ?? "";                    
                    target = target == "ProducerMaxProject" || target == "ProjectSubmitted" ? "" : target;
                    ModelState.AddModelError(target, error.Message);
                }
            }

            return View(viewModel);
        }


        public ActionResult Edit(Guid uid)
        {
            if (_projectAppService.RegistrationDisabled())
            {
                this.StatusMessage(Messages.RegistrationOfClosedProject, Infra.CrossCutting.Tools.Enums.StatusMessageType.Danger);

                return RedirectToAction("Index");
            }

            int userId = User.Identity.GetUserId<int>();
            if (!_projectAppService.ProjectSendToPlayer(uid, userId))
            {
                var viewModel = _projectAppService.GetByEdit(uid, userId);
                if (viewModel != null)
                {
                    return View(viewModel);
                }
                else
                {
                    this.StatusMessage(Messages.ProjectNotFound, Infra.CrossCutting.Tools.Enums.StatusMessageType.Danger);
                    return RedirectToAction("Index");
                }                
            }
            else
            {
                this.StatusMessage(Messages.CanNotEditASubmittedProjectForPlayerEvaluation, Infra.CrossCutting.Tools.Enums.StatusMessageType.Danger);

                return RedirectToAction("Index");
            }
         
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProjectEditAppViewModel viewModel)
        {
            if (_projectAppService.RegistrationDisabled())
            {
                this.StatusMessage(Messages.RegistrationOfClosedProject, Infra.CrossCutting.Tools.Enums.StatusMessageType.Danger);

                return RedirectToAction("Index"); ;
            }

            int userId = User.Identity.GetUserId<int>();

            var result = _projectAppService.Update(viewModel, userId);

            if (result.IsValid)
            {
                this.StatusMessage(Messages.ProjectSuccessfullyUpdated, Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);

                return RedirectToAction("Index");
            }
            else
            {
                if (!result.Errors.Any(e => e.Target == "ProjectSubmitted" || e.Target == "ProducerMaxProject"))
                {
                    ModelState.AddModelError("", Messages.ErrorSavingProject);
                }

                foreach (var error in result.Errors)
                {
                    var target = error.Target ?? "";
                    target = target == "ProducerMaxProject" || target == "ProjectSubmitted" ? "" : target;
                    ModelState.AddModelError(target, error.Message);
                }
            }

            return View(viewModel);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Detail(Guid uid)
        {
            int userId = User.Identity.GetUserId<int>();
            var viewModel = _projectAppService.GetByDetails(uid, userId);
            ViewBag.RegistrationProjectDisabled = _projectAppService.RegistrationDisabled();
            ViewBag.SendToPlayersDisabled = _projectAppService.SendToPlayersDisabled();

            if (viewModel != null)
            {
                return View(viewModel);
            }
            else
            {
                this.StatusMessage(Messages.ProjectNotFound, Infra.CrossCutting.Tools.Enums.StatusMessageType.Danger);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(Guid uid)
        {
            int userId = User.Identity.GetUserId<int>();
            if (!_projectAppService.ProjectSendToPlayer(uid, userId))
            {
                var result = _projectAppService.Delete(uid);

                if (result.IsValid)
                {
                    this.StatusMessage(Messages.ProjectSuccessfullyDeleted, Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        this.StatusMessage(error.Message, Infra.CrossCutting.Tools.Enums.StatusMessageType.Danger);
                    }
                }
            }
            else
            {
                this.StatusMessage(Messages.CanNotEditASubmittedProjectForPlayerEvaluation, Infra.CrossCutting.Tools.Enums.StatusMessageType.Danger);
            }

            return RedirectToAction("Index");
        }
    }
}
// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-01-2019
// ***********************************************************************
// <copyright file="CollaboratorController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.AspNet.Identity;
using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System.Web.Mvc;

namespace PlataformaRio2C.Web.Site.Areas.Producer.Controllers
{
    /// <summary>CollaboratorController</summary>
    //[TermFilter(Order = 2)]
    [Authorize(Order = 1, Roles = "Producer")]
    public class CollaboratorController : PlataformaRio2C.Web.Site.Controllers.CollaboratorController
    {
        private readonly ICollaboratorAppService _collaboratorAppService;
        private readonly IProducerAppService _producerAppService;
        private readonly ICollaboratorProducerAppService _collaboratorProducerAppService;
        private readonly IProjectAppService _projectAppService;

        /// <summary>Initializes a new instance of the <see cref="CollaboratorController"/> class.</summary>
        /// <param name="collaboratorAppService">The collaborator application service.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="producerAppService">The producer application service.</param>
        /// <param name="collaboratorProducerAppService">The collaborator producer application service.</param>
        /// <param name="collaboratorPlayerAppService">The collaborator player application service.</param>
        /// <param name="projectAppService">The project application service.</param>
        public CollaboratorController(ICollaboratorAppService collaboratorAppService, IdentityAutenticationService identityController, IProducerAppService producerAppService, ICollaboratorProducerAppService collaboratorProducerAppService, ICollaboratorPlayerAppService collaboratorPlayerAppService, IProjectAppService projectAppService)
            : base(collaboratorAppService, identityController, collaboratorPlayerAppService)
        {
            _collaboratorAppService = collaboratorAppService;
            _producerAppService = producerAppService;
            _collaboratorProducerAppService = collaboratorProducerAppService;
            _projectAppService = projectAppService;
        }

        [AllowAnonymous]
        public ActionResult PreRegistration()
        {
            if (_projectAppService.PreRegistrationProducerDisabled())
            {
                return View("PreRegistrationDisabled");
            }

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult PreRegistration(PreRegistrationAppViewModel viewmodel)
        {
            if (_projectAppService.PreRegistrationProducerDisabled())
            {
                ModelState.AddModelError("", Messages.ClosedProducerRegistration);
                return View(viewmodel);
            }

            if (ModelState.IsValid)
            {
                var resultService = _collaboratorProducerAppService.PreRegister(viewmodel);
                if (resultService.IsValid)
                {
                    return View("PreRegistrationConfirmation");
                }
                else
                {

                    foreach (var error in resultService.Errors)
                    {
                        var target = error.Target ?? "";
                        ModelState.AddModelError(target, error.Message);
                    }
                }
            }

            return View(viewmodel);
        }

        //public ActionResult ProfileProducerEdit()
        //{
        //    return RedirectToAction("ProfileEdit");
        //}


        public ActionResult ProfileProducerEdit()
        {
            int userId = User.Identity.GetUserId<int>();

            //if (string.IsNullOrWhiteSpace(uid))
            //{
                var result = _collaboratorAppService.GetEditByUserId(userId);

                //CheckRegisterIsComplete();

                //if (result != null && result.Count() >= 2)
                //{
                //    return RedirectToAction("Index");
                //}

                return View("ProfileEdit", result);
            //}
            //else
            //{
            //    var result = _collaboratorAppService.GetEditByUserId(userId);

            //    CheckRegisterIsComplete();

            //    return View("ProfileEdit", result);
            //}
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProfileProducerEdit(CollaboratorEditAppViewModel viewModel)
        {
            viewModel.Address.CountryId = viewModel.CountryId;
            var viewModelCollaboratorProducer = viewModel.Cast<CollaboratorProducerEditAppViewModel>();
            var result = _collaboratorProducerAppService.UpdateByPortal(viewModelCollaboratorProducer);
            //var result = _collaboratorProducerAppService.Update(viewModelCollaboratorProducer);

            if (result.IsValid)
            {
                this.StatusMessage(Messages.ProfileUpdatedSuccessfully, Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);

                //CheckRegisterIsComplete();

                //return RedirectToAction("ProfileEdit", "Producer");
                return RedirectToAction("ProfileEdit", "Collaborator");
            }
            else
            {
                viewModel = viewModelCollaboratorProducer.Cast<CollaboratorEditAppViewModel>();

                ModelState.AddModelError("", Messages.ErrorUpdatingProfileCheckTheFields);

                foreach (var error in result.Errors)
                {
                    var target = error.Target ?? "";
                    ModelState.AddModelError(target, error.Message);
                }
            }

            //CheckRegisterIsComplete();

            return View("ProfileEdit", viewModel);
        }

    }
}
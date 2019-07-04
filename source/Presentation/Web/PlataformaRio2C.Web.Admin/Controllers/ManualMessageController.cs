// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-04-2019
// ***********************************************************************
// <copyright file="ManualMessageController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    /// <summary>ManualMessageController</summary>
    [Authorize(Roles = "Administrator")]
    public class ManualMessageController : BaseController
    {
        private readonly ICollaboratorAppService _appService;
        private readonly ICollaboratorPlayerAppService _collaboratorPlayerAppService;
        private readonly ICollaboratorAppService _collaboratorAppService;
        private readonly IMailAppService _mailAppService;

        /// <summary>Initializes a new instance of the <see cref="ManualMessageController"/> class.</summary>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="appService">The application service.</param>
        /// <param name="collaboratorPlayerAppService">The collaborator player application service.</param>
        /// <param name="mailAppService">The mail application service.</param>
        /// <param name="collaboratorAppService">The collaborator application service.</param>
        public ManualMessageController(
            IdentityAutenticationService identityController, 
            ICollaboratorAppService appService, 
            ICollaboratorPlayerAppService collaboratorPlayerAppService, 
            IMailAppService mailAppService, 
            ICollaboratorAppService collaboratorAppService)
            : base(identityController)
        {
            _appService = appService;
            _collaboratorPlayerAppService = collaboratorPlayerAppService;
            _collaboratorAppService = collaboratorAppService;
            _mailAppService = mailAppService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Collaborators()
        {
            var collaborator = _collaboratorPlayerAppService.All().ToList();
            string subject = "teste";
            var viewModel = _mailAppService.AllMailCollaborator(collaborator, subject);

            //var viewModel = _mailAppService.AllMailCollaborator();

            //if (viewModel != null)
            //{
            //    viewModel = viewModel.OrderBy(e => e.Collaborator.Name).ToList();
            //}

            //var viewModel = collaborator;
            return View(collaborator);
        }

        public ActionResult SendCollaboratorsSelected(IEnumerable<string> emails)
        {
            var result = _appService.SendInvitationCollaboratorsByEmails(emails.ToArray());

            foreach (var envios in result)
            {
                if (envios.Item1)
                {
                    string subject = "teste";
                    Mail mail = _mailAppService.Get(null, subject).MapReverse();
                    var collaborator = (Collaborator)_collaboratorAppService.GetByUserEmail(envios.Item2.Email);
                    DateTime sendDate = DateTime.Now;
                    MailCollaboratorAppViewModel viewModel = new MailCollaboratorAppViewModel(mail, collaborator, sendDate);

                    var resultMail = _mailAppService.CreateMailCollaborator(viewModel);
                }
            }

            TempData["resultSendCollaborator"] = result;

            return RedirectToAction("ResultSendingCollaborators");
        }

        public ActionResult ResultSendingCollaborators()
        {
            var result = TempData["resultSendCollaborator"];
            return View("ResultSendingCollaborators", result);
        }
    }
}
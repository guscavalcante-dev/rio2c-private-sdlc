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
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Web.Mvc;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;

namespace PlataformaRio2C.Web.Site.Controllers
{
    /// <summary>ProjectController</summary>
    //[TermFilter(Order = 2)]
    //[Authorize(Order = 1, Roles = "Player")]
    public class ProjectController : BaseController
    {
        private readonly IProjectAppService _projectAppService;

        /// <summary>Initializes a new instance of the <see cref="ProjectController"/> class.</summary>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="projectAppService">The project application service.</param>
        public ProjectController(IdentityAutenticationService identityController, IProjectAppService projectAppService)
            : base(identityController)
        {
            _projectAppService = projectAppService;
        }

        // GET: Project
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail(Guid uid)
        {
            if (uid != Guid.Empty)
            {
                int userId = User.Identity.GetUserId<int>();
                var result = _projectAppService.GetForEvaluationPlayer(userId, uid);

                if (result != null)
                {
                    return View(result);
                }
            }

            this.StatusMessage("Projeto não encotrado!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Danger);

            return RedirectToAction("Index");
        }
    }
}
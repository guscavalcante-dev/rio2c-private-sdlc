// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-03-2019
// ***********************************************************************
// <copyright file="PlayerController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Domain.Interfaces;
using System;
using System.Web.Mvc;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;

namespace PlataformaRio2C.Web.Site.Areas.Producer.Controllers
{
    /// <summary>PlayerController</summary>
    //[TermFilter(Order = 2)]
    [Authorize(Order = 1, Roles = "Producer")]
    public class PlayerController : PlataformaRio2C.Web.Site.Controllers.PlayerController
    {
        /// <summary>Initializes a new instance of the <see cref="PlayerController"/> class.</summary>
        /// <param name="identityController"></param>
        /// <param name="collaboratorAppService">The collaborator application service.</param>
        /// <param name="playerAppService">The player application service.</param>
        /// <param name="repositoryFactory">The repository factory.</param>
        public PlayerController(IdentityAutenticationService identityController, ICollaboratorAppService collaboratorAppService, IPlayerAppService playerAppService, IRepositoryFactory repositoryFactory)
            :base(identityController, collaboratorAppService, playerAppService, repositoryFactory)
        {

        }

        public ActionResult List()
        {
            return View();
        }


        public ActionResult Details(Guid uid)
        {
            var result = _playerAppService.GetByDetailsWithInterests(uid);

            if (result != null)
            {
                return View("Details", result);
            }

            return RedirectToAction("Index");
        }
    }
}
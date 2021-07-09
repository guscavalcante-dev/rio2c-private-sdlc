// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-09-2021
// ***********************************************************************
// <copyright file="DashboardController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Web.Mvc;
using MediatR;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    /// <summary>
    /// DashboardController
    /// </summary>
    //[Authorize(Roles = "Administrator")]
    [AjaxAuthorize(Order = 1, Roles = Domain.Constants.Role.AnyAdmin)]
    public class DashboardController : BaseController
    {
        /// <summary>Initializes a new instance of the <see cref="DashboardController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        public DashboardController(IMediator commandBus, IdentityAutenticationService identityController)
            : base(commandBus, identityController)
        {
        }

        /// <summary>Indexes this instance.</summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}
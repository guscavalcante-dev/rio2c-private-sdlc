// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-04-2019
// ***********************************************************************
// <copyright file="DashboardController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Web.Mvc;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    /// <summary>DashboardController</summary>
    [Authorize(Roles = "Administrator")]
    public class DashboardController : BaseController
    {
        /// <summary>Initializes a new instance of the <see cref="DashboardController"/> class.</summary>
        /// <param name="identityController">The identity controller.</param>
        public DashboardController(IdentityAutenticationService identityController)
            : base(identityController)
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
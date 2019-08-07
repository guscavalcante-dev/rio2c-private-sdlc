// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-07-2019
// ***********************************************************************
// <copyright file="ConfigNetworkRio2CController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Web.Mvc;
using MediatR;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    /// <summary>ConfigNetworkRio2CController</summary>
    public class ConfigNetworkRio2CController : BaseController
    {
        /// <summary>Initializes a new instance of the <see cref="ConfigNetworkRio2CController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        public ConfigNetworkRio2CController(IMediator commandBus, IdentityAutenticationService identityController)
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
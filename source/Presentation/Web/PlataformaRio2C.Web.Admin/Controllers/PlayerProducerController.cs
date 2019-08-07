// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-07-2019
// ***********************************************************************
// <copyright file="PlayerProducerController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels.Admin;
using System.Web.Mvc;
using MediatR;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    /// <summary>PlayerProducerController</summary>
    public class PlayerProducerController : BaseController
    {
        private readonly IUserAppService _userService;

        /// <summary>Initializes a new instance of the <see cref="PlayerProducerController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="userService">The user service.</param>
        public PlayerProducerController(IMediator commandBus, IdentityAutenticationService identityController, IUserAppService userService)
            : base(commandBus, identityController)
        {

            _userService = userService;
        }

        // GET: PlayerProducer
        public ActionResult Index()
        {
            //var data = _userService.getAllPlayerProducer();
            return View();
        }

        public ActionResult Create()
        {
            var viewmodel = new PlayerProducerEditAppViewModel();
            return View();
        }
    }
}
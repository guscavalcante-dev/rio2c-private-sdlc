//// ***********************************************************************
//// Assembly         : PlataformaRio2C.Web.Admin
//// Author           : Rafael Dantas Ruiz
//// Created          : 06-28-2019
////
//// Last Modified By : Rafael Dantas Ruiz
//// Last Modified On : 08-07-2019
//// ***********************************************************************
//// <copyright file="MailController.cs" company="Softo">
////     Copyright (c) Softo. All rights reserved.
//// </copyright>
//// <summary></summary>
//// ***********************************************************************
//using PlataformaRio2C.Application.Interfaces.Services;
//using System.Linq;
//using System.Web.Mvc;
//using MediatR;
//using PlataformaRio2C.Infra.CrossCutting.Identity.Service;

//namespace PlataformaRio2C.Web.Admin.Controllers
//{
//    /// <summary>MailController</summary>
//    [Authorize(Roles = "Administrator")]
//    public class MailController : BaseController
//    {
//        private readonly IMailAppService _appService;

//        /// <summary>Initializes a new instance of the <see cref="MailController"/> class.</summary>
//        /// <param name="commandBus">The command bus.</param>
//        /// <param name="identityController">The identity controller.</param>
//        /// <param name="appService">The application service.</param>
//        public MailController(IMediator commandBus, IdentityAutenticationService identityController, IMailAppService appService)
//            : base(commandBus, identityController)
//        {
//            _appService = appService;
//        }

//        public ActionResult Index()
//        {
//            var model = _appService.GetAll();

//            if (model != null)
//            {
//                model = model.ToList();
//            }

//            return View(model);
//        }

//    }
//}
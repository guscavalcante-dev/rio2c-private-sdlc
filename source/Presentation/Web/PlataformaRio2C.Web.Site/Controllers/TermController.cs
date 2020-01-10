//// ***********************************************************************
//// Assembly         : PlataformaRio2C.Web.Site
//// Author           : Rafael Dantas Ruiz
//// Created          : 06-28-2019
////
//// Last Modified By : Rafael Dantas Ruiz
//// Last Modified On : 08-07-2019
//// ***********************************************************************
//// <copyright file="TermController.cs" company="Softo">
////     Copyright (c) Softo. All rights reserved.
//// </copyright>
//// <summary></summary>
//// ***********************************************************************
//using Microsoft.AspNet.Identity;
//using PlataformaRio2C.Application.Interfaces.Services;
//using PlataformaRio2C.Application.ViewModels;
//using PlataformaRio2C.Infra.CrossCutting.Resources;
//using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
//using System.Web.Mvc;
//using MediatR;
//using PlataformaRio2C.Infra.CrossCutting.Identity.Service;

//namespace PlataformaRio2C.Web.Site.Controllers
//{
//    /// <summary>TermController</summary>
//    [Authorize]
//    public class TermController : BaseController
//    {
//        private readonly IUserUseTermAppService _userUseTermAppService;

//        /// <summary>Initializes a new instance of the <see cref="TermController"/> class.</summary>
//        /// <param name="commandBus">The command bus.</param>
//        /// <param name="identityController">The identity controller.</param>
//        /// <param name="userUseTermAppService">The user use term application service.</param>
//        public TermController(IMediator commandBus, IdentityAutenticationService identityController, IUserUseTermAppService userUseTermAppService)
//            : base(commandBus, identityController)
//        {
//            _userUseTermAppService = userUseTermAppService;
//        }

//        // GET: Term        
//        public ActionResult Index()
//        {
//            if (User.IsInRole("Player"))
//            {
//                return RedirectToAction("Player");
//            }

//            if (User.IsInRole("Producer"))
//            {
//                return RedirectToAction("Producer");
//            }

//            this.StatusMessage(Messages.AccessDenied, Infra.CrossCutting.Tools.Enums.StatusMessageType.Danger);
//            return RedirectToAction("LogOff", "Account");
//        }

//        [Authorize(Roles = "Player")]
//        public ActionResult Player()
//        {
//            return View();
//        }

//        [Authorize(Roles = "Producer")]
//        public ActionResult Producer()
//        {
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult AcceptTermPlayer(bool Accept)
//        {
//            if (Accept)
//            {
//                int userId = User.Identity.GetUserId<int>();

//                var vm = new UserUseTermAppViewModel()
//                {
//                    UserId = userId,
//                    Role = "Player"
//                };

//                var result = _userUseTermAppService.Create(vm);

//                if (result.IsValid)
//                {
//                    return RedirectToAction("ProfileEdit", "Collaborator", new { area = "" });
//                }
//                else
//                {
//                    foreach (var error in result.Errors)
//                    {
//                        this.StatusMessage(error.Message, Infra.CrossCutting.Tools.Enums.StatusMessageType.Danger);
//                    }
//                }
//            }
//            else
//            {
//                this.StatusMessage(Messages.ToProceedYouMustAcceptTheTerm, Infra.CrossCutting.Tools.Enums.StatusMessageType.Danger);
//            }

//            return RedirectToAction("Player");
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult AcceptTermProducer(bool Accept)
//        {
//            if (Accept)
//            {
//                int userId = User.Identity.GetUserId<int>();

//                var vm = new UserUseTermAppViewModel()
//                {
//                    UserId = userId,
//                    Role = "Producer"
//                };

//                var result = _userUseTermAppService.Create(vm);

//                if (result.IsValid)
//                {

//                    return RedirectToAction("ProfileEdit", "Collaborator", new { area = "ProducerArea" });
//                }
//                else
//                {
//                    foreach (var error in result.Errors)
//                    {
//                        this.StatusMessage(error.Message, Infra.CrossCutting.Tools.Enums.StatusMessageType.Danger);
//                    }
//                }
//            }
//            else
//            {
//                this.StatusMessage(Messages.ToProceedYouMustAcceptTheTerm, Infra.CrossCutting.Tools.Enums.StatusMessageType.Danger);
//            }

//            return RedirectToAction("Producer");
//        }
//    }
//}
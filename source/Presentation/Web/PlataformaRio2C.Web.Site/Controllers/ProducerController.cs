﻿//// ***********************************************************************
//// Assembly         : PlataformaRio2C.Web.Site
//// Author           : Rafael Dantas Ruiz
//// Created          : 06-28-2019
////
//// Last Modified By : Rafael Dantas Ruiz
//// Last Modified On : 08-07-2019
//// ***********************************************************************
//// <copyright file="ProducerController.cs" company="Softo">
////     Copyright (c) Softo. All rights reserved.
//// </copyright>
//// <summary></summary>
//// ***********************************************************************
//using Microsoft.AspNet.Identity;
//using PlataformaRio2C.Application.Interfaces.Services;
//using PlataformaRio2C.Application.ViewModels;
//using PlataformaRio2C.Domain.Interfaces;
//using PlataformaRio2C.Infra.CrossCutting.Resources;
//using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
//using System;
//using System.Web.Mvc;
//using MediatR;
//using PlataformaRio2C.Infra.CrossCutting.Identity.Service;

//namespace PlataformaRio2C.Web.Site.Controllers
//{
//    /// <summary>ProducerController</summary>
//    //[TermFilter(Order = 2)]
//    [Authorize(Order = 1, Roles = "Producer,Player")]
//    public class ProducerController : BaseController
//    {
//        protected readonly IProducerAppService _producerAppService;
//        protected readonly ICollaboratorAppService _collaboratorAppService;

//        /// <summary>Initializes a new instance of the <see cref="ProducerController"/> class.</summary>
//        /// <param name="commandBus">The command bus.</param>
//        /// <param name="identityController">The identity controller.</param>
//        /// <param name="producerAppService">The producer application service.</param>
//        /// <param name="collaboratorAppService">The collaborator application service.</param>
//        /// <param name="repositoryFactory">The repository factory.</param>
//        public ProducerController(IMediator commandBus, IdentityAutenticationService identityController, IProducerAppService producerAppService, ICollaboratorAppService collaboratorAppService, IRepositoryFactory repositoryFactory)
//            : base(commandBus, identityController)
//        {
//            _producerAppService = producerAppService;
//            _collaboratorAppService = collaboratorAppService;
//        }

//        // GET: Producer
//        public ActionResult Index()
//        {
//            return RedirectToAction("ProfileDetails");
//        }

//        public ActionResult ProfileDetails(string uid = null)
//        {
//            int userId = User.Identity.GetUserId<int>();

//            //CheckRegisterIsComplete();

//            if (string.IsNullOrWhiteSpace(uid))
//            {
//                var result = _producerAppService.GetDetailByUserId(userId);
//                return View("ProfileDetails", result);
//            }
//            else
//            {
//                var result = _producerAppService.GetDetailByUserId(userId, new Guid(uid));
//                return View("ProfileDetails", result);
//            }
//        }

//        public ActionResult ProfileEdit(string uid = null)
//        { 
//            int userId = User.Identity.GetUserId<int>();

//            //CheckRegisterIsComplete();

//            if (string.IsNullOrWhiteSpace(uid))
//            {
//                var result = _producerAppService.GetEditByUserId(userId);
//                return View("ProfileEdit", result);
//            }
//            else
//            {
//                var result = _producerAppService.GetEditByUserId(userId, new Guid(uid));
//                return View("ProfileEdit", result);
//            }
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult ProfileEdit(ProducerEditAppViewModel viewModel)
//        {
//            viewModel.Address.CountryId = viewModel.CountryId;
//            var result = _producerAppService.UpdateByPortal(viewModel);

//            if (result.IsValid)
//            {
//                this.StatusMessage(string.Format("", viewModel.Name), Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);

//                //CheckRegisterIsComplete();

//                return RedirectToAction("ProfileDetails", "Producer", new {uid = viewModel.Uid});
//            }
//            else
//            {
//                ModelState.AddModelError("", string.Format(Messages.ErrorUpdatingProducerData, viewModel.Name));

//                foreach (var error in result.Errors)
//                {
//                    var target = error.Target ?? "";
//                    ModelState.AddModelError(target, error.Message);
//                }
//                //CheckRegisterIsComplete();
//                return View(viewModel);
//                //return RedirectToActionPermanent("ProfileEdit", "Producer", new { uid = viewModel.Uid });
//            }

            
//        }


//        public ActionResult Details(Guid uid)
//        {
//            var result = _producerAppService.GetByDetails(uid);

//            if (result != null)
//            {
//                return View("Details", result);
//            }

//            return RedirectToAction("Index");
//        }

//        [HttpPost]
//        public JsonResult ListStates(int code)
//        {
//            var states = _collaboratorAppService.listStates(code);

//            return Json(states);
//        }


//        [HttpPost]
//        public JsonResult ListCities(int code)
//        {
//            var states = _collaboratorAppService.listCities(code);

//            return Json(states);
//        }


//        [HttpPost]
//        public JsonResult ListCountries()
//        {
//            var countries = _collaboratorAppService.listCountries();            
//            return Json(countries);
//        }
//    }
//}
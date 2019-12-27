//// ***********************************************************************
//// Assembly         : PlataformaRio2C.Web.Site
//// Author           : Rafael Dantas Ruiz
//// Created          : 06-28-2019
////
//// Last Modified By : Rafael Dantas Ruiz
//// Last Modified On : 09-10-2019
//// ***********************************************************************
//// <copyright file="ConferenceController.cs" company="Softo">
////     Copyright (c) Softo. All rights reserved.
//// </copyright>
//// <summary></summary>
//// ***********************************************************************
//using PlataformaRio2C.Application.Interfaces.Services;
//using System;
//using System.Web.Mvc;
//using MediatR;
//using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
//using System.Collections.Generic;
//using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;

//namespace PlataformaRio2C.Web.Site.Controllers
//{
//    /// <summary>ConferenceController</summary>
//    //[TermFilter(Order = 2)]
//    [Authorize(Order = 1)]
//    public class ConferenceController : BaseController
//    {
//        private readonly IConferenceAppService _appService;

//        /// <summary>Initializes a new instance of the <see cref="ConferenceController"/> class.</summary>
//        /// <param name="commandBus">The command bus.</param>
//        /// <param name="identityController">The identity controller.</param>
//        /// <param name="appService">The application service.</param>
//        public ConferenceController(IMediator commandBus, IdentityAutenticationService identityController, IConferenceAppService appService)
//            : base(commandBus, identityController)
//        {
//            _appService = appService;
//        }

//        // GET: Lecture
//        [HttpGet]
//        public ActionResult Index()
//        {
//            #region Breadcrumb

//            ViewBag.Breadcrumb = new BreadcrumbHelper("Conference", new List<BreadcrumbItemHelper> {
//                new BreadcrumbItemHelper("Dashboard", Url.Action("Index", "Home", new { Area = "Player" }))
//            });

//            #endregion
//            return View();
//        }

//        [HttpGet]
//        public ActionResult Detail(Guid uid)
//        {
//            var result = _appService.GetByDetails(uid);

//            if (result != null)
//            {
//                return View( result);
//            }

//            return RedirectToAction("Index");            
//        }
//    }
//}
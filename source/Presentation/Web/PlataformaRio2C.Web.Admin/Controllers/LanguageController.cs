//// ***********************************************************************
//// Assembly         : PlataformaRio2C.Web.Admin
//// Author           : Rafael Dantas Ruiz
//// Created          : 06-28-2019
////
//// Last Modified By : Rafael Dantas Ruiz
//// Last Modified On : 08-07-2019
//// ***********************************************************************
//// <copyright file="LanguageController.cs" company="Softo">
////     Copyright (c) Softo. All rights reserved.
//// </copyright>
//// <summary></summary>
//// ***********************************************************************
//using PlataformaRio2C.Application.Interfaces.Services;
//using PlataformaRio2C.Application.ViewModels;
//using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
//using System;
//using System.Linq;
//using System.Web.Mvc;
//using MediatR;
//using PlataformaRio2C.Infra.CrossCutting.Identity.Service;

//namespace PlataformaRio2C.Web.Admin.Controllers
//{
//    /// <summary>LanguageController</summary>
//    [Authorize(Roles = "Administrator", Users = "projeto.rio2c@marlin.com.br")]
//    public class LanguageController : BaseController
//    {
//        private readonly ILanguageAppService _appService;

//        /// <summary>Initializes a new instance of the <see cref="LanguageController"/> class.</summary>
//        /// <param name="commandBus">The command bus.</param>
//        /// <param name="identityController">The identity controller.</param>
//        /// <param name="appService">The application service.</param>
//        public LanguageController(IMediator commandBus, IdentityAutenticationService identityController, ILanguageAppService appService)
//            : base(commandBus, identityController)
//        {
//            _appService = appService;
//        }

//        // GET: Language
//        public ActionResult Index()
//        {
//            var viewModel = _appService.All();

//            if (viewModel != null)
//            {
//                viewModel = viewModel.OrderBy(e => e.Name).ToList();
//            }

//            return View(viewModel);
//        }

//        public ActionResult Edit(Guid Uid)
//        {
//            var result = _appService.Get(Uid);

//            return View(result);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit(LanguageAppViewModel viewModel)
//        {
//            var result = _appService.Update(viewModel);

//            if (result.IsValid)
//            {
//                this.StatusMessage("Idioma atualizado com sucesso!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);
//                return RedirectToAction("Index");
//            }
//            else
//            {
//                ModelState.AddModelError("", "Erro ao atualizar cadastro! Verifique o preenchimento dos campos!");                

//                foreach (var error in result.Errors)
//                {
//                    var target = error.Target ?? "";
//                    ModelState.AddModelError(target, error.Message);
//                }
//            }

//            return View(viewModel);
//        }
//    }
//}
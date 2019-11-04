//// ***********************************************************************
//// Assembly         : PlataformaRio2C.Web.Site
//// Author           : Rafael Dantas Ruiz
//// Created          : 06-28-2019
////
//// Last Modified By : Rafael Dantas Ruiz
//// Last Modified On : 08-07-2019
//// ***********************************************************************
//// <copyright file="CollaboratorController.cs" company="Softo">
////     Copyright (c) Softo. All rights reserved.
//// </copyright>
//// <summary></summary>
//// ***********************************************************************
//using Microsoft.AspNet.Identity;
//using PlataformaRio2C.Application.Interfaces.Services;
//using PlataformaRio2C.Application.ViewModels;
//using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
//using PlataformaRio2C.Infra.CrossCutting.Resources;
//using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
//using System.Web.Mvc;
//using MediatR;

//namespace PlataformaRio2C.Web.Site.Controllers
//{
//    /// <summary>CollaboratorController</summary>
//    //[TermFilter(Order = 2)]
//    [Authorize(Order = 1, Roles = "Player,Producer")]
//    public class CollaboratorController : BaseController
//    {
//        private readonly ICollaboratorAppService _collaboratorAppService;
//        private readonly ICollaboratorPlayerAppService _collaboratorPlayerAppService;
//        protected readonly IdentityAutenticationService _identityController;

//        /// <summary>Initializes a new instance of the <see cref="CollaboratorController"/> class.</summary>
//        /// <param name="commandBus">The command bus.</param>
//        /// <param name="identityController">The identity controller.</param>
//        /// <param name="collaboratorAppService">The collaborator application service.</param>
//        /// <param name="collaboratorPlayerAppService">The collaborator player application service.</param>
//        public CollaboratorController(IMediator commandBus, IdentityAutenticationService identityController, ICollaboratorAppService collaboratorAppService, ICollaboratorPlayerAppService collaboratorPlayerAppService)
//            : base(commandBus, identityController)
//        {
//            _collaboratorAppService = collaboratorAppService;
//            _identityController = identityController;
//            _collaboratorPlayerAppService = collaboratorPlayerAppService;
//        }

//        //GET: Collaborator
//        public ActionResult Index()
//        {
//            return RedirectToAction("ProfileDetails");
//        }

//        //GET: Collaborator/ProfileDetails
//        public ActionResult ProfileDetails()
//        {
//            int userId = User.Identity.GetUserId<int>();
//            var result = _collaboratorAppService.GetDetailByUserId(userId);
//            //CheckRegisterIsComplete();
//            return View(result);
//        }


//        //GET: Collaborator/ProfileEdit
//        public ActionResult ProfileEdit()
//        {
//            int userId = User.Identity.GetUserId<int>();
//            var result = _collaboratorAppService.GetEditByUserId(userId);
//            //CheckRegisterIsComplete();

//            return View(result);
//        }


//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public virtual ActionResult ProfileEdit(CollaboratorEditAppViewModel viewModel)
//        {
//            var viewModelCollaborator = viewModel.Cast<CollaboratorPlayerEditAppViewModel>();
//            var states = _collaboratorAppService.listStates(viewModel.CountryId);
//            var cities = _collaboratorAppService.listCities(viewModel.StateId);
//            var countries = _collaboratorAppService.listCountries();
//            viewModelCollaborator.PhoneNumber = "110000000";

//            var result = _collaboratorPlayerAppService.UpdateByPortal(viewModelCollaborator);

//            if (result.IsValid)
//            {
//                this.StatusMessage(Messages.ProfileUpdatedSuccessfully, Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);

//                //CheckRegisterIsComplete();

//                return RedirectToAction("ProfileEdit", "Player");
//            }
//            else
//            {
//                viewModel = viewModelCollaborator.Cast<CollaboratorEditAppViewModel>();
//                viewModel.Cities = cities;
//                viewModel.States = states;
//                viewModel.Countries = countries;

//                ModelState.AddModelError("", Messages.ErrorUpdatingProfileCheckTheFields);

//                foreach (var error in result.Errors)
//                {
//                    var target = error.Target ?? "";
//                    ModelState.AddModelError(target, error.Message);
//                }
//            }

//            //CheckRegisterIsComplete();

//            return View(viewModel);
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

//        /*
//         public ActionResult ChangePassword()
//        {
//            int userId = User.Identity.GetUserId<int>();
//            var result = _collaboratorAppService.GetByUserId(userId);

//            var viewModel = new CollaboratorChangePasswordAppViewModel()
//            {
//                Name = result.Name,
//                Image = result.Image,
//                Uid = result.UidByEdit
//            };

//            return View(viewModel);
//        }
             

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> ChangePassword(CollaboratorChangePasswordAppViewModel model)
//        {
//            if (!ModelState.IsValid)
//            {
//                return View(model);
//            }

//            var result = await _identityController.ChangePasswordAsync(User.Identity.GetUserId<int>(), model.OldPassword, model.NewPassword);
//            if (result.Succeeded)
//            {
//                var user = await _identityController.FindByIdAsync(User.Identity.GetUserId<int>());
//                if (user != null)
//                {
//                    await _identityController.SignInAsync(user, isPersistent: false, rememberBrowser: false);
//                }

//                this.StatusMessage("Senha atualizada com sucesso!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);

//                return RedirectToAction("Index", "Home", new { area = "" });
//            }
//            else
//            {

//            }
//            AddErrors(result);
//            return View(model);
//        }
//        */

//        private void AddErrors(IdentityResult result)
//        {
//            foreach (var error in result.Errors)
//            {
//                ModelState.AddModelError("", error);
//            }
//        }
//    }
//}
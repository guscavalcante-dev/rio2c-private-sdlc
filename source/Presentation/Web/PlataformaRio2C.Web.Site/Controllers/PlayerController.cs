// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-07-2019
// ***********************************************************************
// <copyright file="PlayerController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.AspNet.Identity;
using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Linq;
using System.Web.Mvc;
using MediatR;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;

namespace PlataformaRio2C.Web.Site.Controllers
{
    /// <summary>PlayerController</summary>
    //[TermFilter(Order = 2)]
    [Authorize(Order = 1, Roles = "Player,Producer")]
    public class PlayerController : BaseController
    {
        protected readonly ICollaboratorAppService _collaboratorAppService;
        protected readonly IPlayerAppService _playerAppService;
        protected readonly ICountryRepository _countryRepository;

        /// <summary>Initializes a new instance of the <see cref="PlayerController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="collaboratorAppService">The collaborator application service.</param>
        /// <param name="playerAppService">The player application service.</param>
        /// <param name="repositoryFactory">The repository factory.</param>
        public PlayerController(IMediator commandBus, IdentityAutenticationService identityController, ICollaboratorAppService collaboratorAppService, IPlayerAppService playerAppService, IRepositoryFactory repositoryFactory)
            : base(commandBus, identityController)
        {
            _collaboratorAppService = collaboratorAppService;
            _playerAppService = playerAppService;
            _countryRepository = repositoryFactory.CountryRepository;
        }

        // GET: Player
        public ActionResult Index()
        {
            return RedirectToAction("ProfileDetails");
        }

        // GET: Player/ProfileDetails
        public ActionResult ProfileDetails(string uid = null)
        {
            int userId = User.Identity.GetUserId<int>();

            if (string.IsNullOrWhiteSpace(uid))
            {
                var result = _playerAppService.GetAllDetailByUserId(userId);
                //CheckRegisterIsComplete();

                if (result != null && result.Count() >= 2)
                {
                    return View("Index", result);
                }

                return View("ProfileDetails", result.FirstOrDefault());
            }
            else
            {
                var result = _playerAppService.GetAllDetailByUserId(userId, new Guid(uid));
                //CheckRegisterIsComplete();

                return View("ProfileDetails", result);
            }
        }

        // GET: Player/ProfileEdit
        public ActionResult ProfileEdit(string uid = null)
        {
            int userId = User.Identity.GetUserId<int>();

            if (string.IsNullOrWhiteSpace(uid))
            {
                var result = _playerAppService.GetAllEditByUserId(userId);

                //CheckRegisterIsComplete();

                if (result != null && result.Count() >= 2)
                {
                    return RedirectToAction("Index");
                }

                return View("ProfileEdit", result.FirstOrDefault());
            }
            else
            {
                var result = _playerAppService.GetAllEditByUserId(userId, new Guid(uid));
                
                //CheckRegisterIsComplete();

                return View("ProfileEdit", result);
            }
        }

        [HttpPost]
        public JsonResult ListStates(int code)
        {
            var states = _collaboratorAppService.listStates(code);

            return Json(states);
        }


        [HttpPost]
        public JsonResult ListCities(int code)
        {
            var states = _collaboratorAppService.listCities(code);

            return Json(states);
        }

        // POST: Player/ProfileEdit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProfileEdit(PlayerEditAppViewModel viewModel)
        {
            viewModel.Address.CountryId = viewModel.CountryId;
            var result = _playerAppService.UpdateByPortal(viewModel);

            if (result.IsValid)
            {
                this.StatusMessage(string.Format(Messages.PlayerDataSuccessfullyUpdated, viewModel.Name), Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);

                var playersVm = _playerAppService.GetAllByUserId(User.Identity.GetUserId<int>());

                //CheckRegisterIsComplete();

                if (playersVm != null && playersVm.Count() <= 1)
                {
                    return RedirectToAction("Interests", "Player");
                }

                return RedirectToAction("ProfileDetails", "Player");
            }
            else
            {
                //CheckRegisterIsComplete();

                ModelState.AddModelError("", string.Format(Messages.ErrorUpdatingPlayerData, viewModel.Name));
                viewModel.Countries = _countryRepository.GetAll();


                foreach (var error in result.Errors)
                {
                    var target = error.Target ?? "";
                    ModelState.AddModelError(target, error.Message);
                }
            }

            return View(viewModel);
        }

        // GET: Player/Interests
        public ActionResult Interests(string uid = null)
        {
            int userId = User.Identity.GetUserId<int>();

            if (string.IsNullOrWhiteSpace(uid))
            {
                var result = _playerAppService.GetAllByUserId(userId);
                //CheckRegisterIsComplete();
                if (result != null && result.Count() >= 2)
                {
                    return View("ListPlayersInterests", result);
                }

                return View("Interests", result.FirstOrDefault());
            }
            else
            {
                var result = _playerAppService.GetAllByUserId(userId, new Guid(uid));
                //CheckRegisterIsComplete();
                return View("Interests", result);
            }
        }

        // POST: Player/Interests
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Interests(PlayerAppViewModel viewModel)
        {
            var result = _playerAppService.SaveInterests(viewModel);

            if (result.IsValid)
            {
                //CheckRegisterIsComplete();
                this.StatusMessage(string.Format(Messages.PlayerInterestsSuccessfullyUpdated, viewModel.Name), Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);
                return RedirectToAction("Interests", "Player");
            }
            else
            {
                ModelState.AddModelError("", string.Format(Messages.ErrorUpdatingPlayerInterests, viewModel.Name));
                foreach (var error in result.Errors)
                {
                    var target = error.Target ?? "";
                    ModelState.AddModelError(target, error.Message);
                }
            }

            //CheckRegisterIsComplete();
            return View(viewModel);
        }

        // GET: Player/CancelInterests
        public ActionResult CancelInterests()
        {
            int userId = User.Identity.GetUserId<int>();

            var result = _playerAppService.GetAllByUserId(userId);

            if (result != null && result.Count() >= 2)
            {
                return RedirectToAction("Interests");
            }

            return RedirectToAction("ProfileDetails");
        }
    }


}
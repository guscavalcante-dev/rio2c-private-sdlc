// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-09-2019
// ***********************************************************************
// <copyright file="HoldingsController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using MediatR;
using PlataformaRio2C.Application.CQRS.Queries;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    /// <summary>HoldingsController</summary>
    [Authorize(Roles = "Admin")]
    public class HoldingsController : BaseController
    {
        private readonly IHoldingAppService _appService;

        /// <summary>Initializes a new instance of the <see cref="HoldingsController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="appService">The application service.</param>
        public HoldingsController(IMediator commandBus, IdentityAutenticationService identityController, IHoldingAppService appService)
            : base(commandBus, identityController)
        {
            _appService = appService;
        }

        #region List

        /// <summary>Indexes this instance.</summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.Holdings, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Holdings, Url.Action("Index", "Holding", new { Area = "" }))
            });

            #endregion

            //var viewModel = _appService.GetAllSimple();

            //if (viewModel != null)
            //{
            //    viewModel = viewModel.OrderBy(e => e.Name).ToList();
            //}

            return View();
        }

        /// <summary>Searches the specified request.</summary>
        /// <param name="request">The request.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Search(IDataTablesRequest request, bool showAllEditions)
        {
            var holdings = await this.CommandBus.Send(new FindAllHoldingsAsync(
                request.Start,
                request.Length,
                request.Search?.Value,
                request.GetSortColumns(),
                showAllEditions,
                this.UserId,
                this.UserUid,
                this.EditionId,
                this.EditionUid,
                this.UserInterfaceLanguage));

            var response = DataTablesResponse.Create(request, holdings.TotalItemCount, holdings.TotalItemCount, holdings);

            return new DataTablesJsonResult(response, JsonRequestBehavior.AllowGet);
        }

        #endregion

        public ActionResult Create()
        {
            var viewModel = _appService.GetEditViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HoldingAppViewModel viewModel)
        {
            var result = _appService.Create(viewModel);

            if (result.IsValid)
            {
                this.StatusMessage("Holding criado com sucesso!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);

                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Erro ao salvar cadastro! Verifique o preenchimento dos campos!");

                foreach (var error in result.Errors)
                {
                    var target = error.Target ?? "";
                    ModelState.AddModelError(target, error.Message);
                }
            }

            UpdateHoldingViewModelDefaultValues(viewModel);

            return View(viewModel);
        }

        public ActionResult Edit(Guid Uid)
        {
            var result = _appService.Get(Uid);

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HoldingAppViewModel viewModel)
        {
            var result = _appService.Update(viewModel);

            if (result.IsValid)
            {
                this.StatusMessage("Holding atualizado com sucesso!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Erro ao atualizar cadastro! Verifique o preenchimento dos campos!");

                foreach (var error in result.Errors)
                {
                    var target = error.Target ?? "";
                    ModelState.AddModelError(target, error.Message);
                }
            }

            UpdateHoldingViewModelDefaultValues(viewModel);

            return View(viewModel);
        }


        public ActionResult Delete(Guid Uid)
        {
            var result = _appService.Delete(Uid);

            if (result.IsValid)
            {
                this.StatusMessage("Holding apagado com sucesso!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    this.StatusMessage(error.Message, Infra.CrossCutting.Tools.Enums.StatusMessageType.Danger);
                }
            }

            return RedirectToAction("Index");
        }

        private void UpdateHoldingViewModelDefaultValues(HoldingAppViewModel viewModel)
        {
            viewModel.MergeWith<HoldingAppViewModel>(_appService.GetEditViewModel());
        }
    }
}
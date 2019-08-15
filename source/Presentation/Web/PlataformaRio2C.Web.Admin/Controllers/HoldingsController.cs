// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-14-2019
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
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    /// <summary>HoldingsController</summary>
    [AjaxAuthorize(Roles = "Admin")]
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

        /// <summary>Indexes the specified search view model.</summary>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(HoldingSearchViewModel searchViewModel)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.Holdings, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Holdings, Url.Action("Index", "Holding", new { Area = "" }))
            });

            #endregion

            return View(searchViewModel);
        }

        #endregion

        #region DataTable Widget

        /// <summary>Searches the specified request.</summary>
        /// <param name="request">The request.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Search(IDataTablesRequest request, bool showAllEditions)
        {
            var holdings = await this.CommandBus.Send(new FindAllHoldingsAsync(
                request.Start / request.Length,
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

            return Json(new
            {
                status = "success",
                dataTable = response
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Total Count Widget

        /// <summary>Shows the total count widget.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowTotalCountWidget()
        {
            var holdingsCount = await this.CommandBus.Send(new CountAllHoldingsAsync(
                true,
                this.UserId,
                this.UserUid,
                this.EditionId,
                this.EditionUid,
                this.UserInterfaceLanguage));

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/TotalCountWidget", holdingsCount), divIdOrClass = "#HoldingTotalCountWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Edition Count Widget

        /// <summary>Shows the edition count widget.</summary>
        /// <returns></returns>
        public async Task<ActionResult> ShowEditionCountWidget()
        {
            var holdingsCount = await this.CommandBus.Send(new CountAllHoldingsAsync(
                false,
                this.UserId,
                this.UserUid,
                this.EditionId,
                this.EditionUid,
                this.UserInterfaceLanguage));

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/EditionCountWidget", holdingsCount), divIdOrClass = "#HoldingEditionCountWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Create

        /// <summary>Shows the create modal.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowCreateModal()
        {
            //var viewModel = _appService.GetEditViewModel();

            var viewModel = new HoldingViewModel(await this.CommandBus.Send(new FindAllLanguagesAsync(
                this.UserId,
                this.UserUid,
                this.EditionId,
                this.EditionUid,
                this.UserInterfaceLanguage)));

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Modals/CreateModal", viewModel), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);

            //return View(viewModel);
        }

        /// <summary>Creates the specified view model.</summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(HoldingViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException("Please, correct the form errors.");
                }

                //try
                //{
                //    ImageHelper.UploadOriginalAndAdvancedCropLogo(this.site.Id, viewModel.File, viewModel.DataX, viewModel.DataY, viewModel.DataWidth, viewModel.DataHeight, "SiteLogos");
                //    var result = this.commandBus.Send(new EditSiteLogo(this.site.Id, Session.GetUserId(), Session.GetUserEmail()));
                //    if (!result.IsExecuted)
                //    {
                //        throw result.Exception;
                //    }
                //}
                //catch (DomainRuleException dex)
                //{
                //    ModelState.AddModelError("File", dex.GetInnerMessage());
                //    throw;
                //}
            }
            catch (DomainException ex)
            {
                //this.SetResultMessage(new ResultMessage(ex.GetInnerMessage(), ResultMessageType.Error));
                //viewModel.UpdateModelsAndLists(this.site);
                //return View("SiteLogoSettings", viewModel);

                return Json(new { status = "error", message = ex.GetInnerMessage() });
            }
            catch (Exception ex)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                //this.SetResultMessage(new ResultMessage("[[[We found an error updating site logo. We are already working on it.]]]", ResultMessageType.Error));
                //return RedirectToAction("SiteLogoSettings");

                return Json(new { status = "error", message = "We found an error creating the holding. We are already working on it." });
            }

            //this.SetResultMessage(new ResultMessage("[[[The site logo was updated successfully.]]]", ResultMessageType.Success));
            //return RedirectToAction("SiteLogoSettings");

            return Json(new { status = "success"/*, message = T._("The person picture was changed successfully."), imageLink = FileHelper.AvatarFor(viewModel.PersonId, viewModel.PersonTypeId)*/ });
        }

        #endregion


        //public ActionResult Create()
        //{
        //    var viewModel = _appService.GetEditViewModel();
        //    return View(viewModel);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(HoldingAppViewModel viewModel)
        //{
        //    var result = _appService.Create(viewModel);

        //    if (result.IsValid)
        //    {
        //        this.StatusMessage("Holding criado com sucesso!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);

        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("", "Erro ao salvar cadastro! Verifique o preenchimento dos campos!");

        //        foreach (var error in result.Errors)
        //        {
        //            var target = error.Target ?? "";
        //            ModelState.AddModelError(target, error.Message);
        //        }
        //    }

        //    UpdateHoldingViewModelDefaultValues(viewModel);

        //    return View(viewModel);
        //}

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
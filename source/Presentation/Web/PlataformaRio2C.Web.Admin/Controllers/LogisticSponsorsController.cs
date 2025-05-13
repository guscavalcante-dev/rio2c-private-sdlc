// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Arthur Souza
// Created          : 01-20-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-09-2021
// ***********************************************************************
// <copyright file="LogisticSponsorsController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using MediatR;
using PlataformaRio2C.Application;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;
using PlataformaRio2C.Web.Admin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Constants = PlataformaRio2C.Domain.Constants;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    /// <summary>
    /// LogisticSponsorsController
    /// </summary>
    [AjaxAuthorize(Order = 1, Roles = Constants.Role.AnyAdmin)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.AdminLogistic)]
    public class LogisticSponsorsController : BaseController
    {
        private readonly ILogisticSponsorRepository logisticSponsorRepo;
        private readonly ILanguageRepository languageRepo;

        /// <summary>Initializes a new instance of the <see cref="LogisticSponsorsController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="logisticSponsorRepo">The logistic sponsor repo.</param>
        /// <param name="languageRepo">The language repo.</param>
        public LogisticSponsorsController(
            IMediator commandBus,
            IdentityAutenticationService identityController,
            ILogisticSponsorRepository logisticSponsorRepo,
            ILanguageRepository languageRepo)
            : base(commandBus, identityController)
        {
            this.logisticSponsorRepo = logisticSponsorRepo;
            this.languageRepo = languageRepo;
        }

        #region List

        /// <summary>Indexes the specified search view model.</summary>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(LogisticSponsorSearchViewModel searchViewModel)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.Logistics, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Sponsors, Url.Action("Index", "LogisticSponsors", new { Area = "" }))
            });

            #endregion

            return View(searchViewModel);
        }

        #region DataTable Widget

        /// <summary>Searches the specified request.</summary>
        /// <param name="request">The request.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Search(IDataTablesRequest request, bool showAllEditions)
        {
            var sponsors = await this.logisticSponsorRepo.FindAllByDataTableAsync(
                request.Start / request.Length,
                request.Length,
                request.Search?.Value,
                request.GetSortColumns(),
                showAllEditions,
                this.EditionDto.Id);

            foreach (var sponsor in sponsors)
            {
                sponsor.Name = sponsor.Name.GetSeparatorTranslation(this.UserInterfaceLanguage, Language.Separator);
            }

            var response = DataTablesResponse.Create(request, sponsors.TotalItemCount, sponsors.TotalItemCount, sponsors);

            return Json(new
            {
                status = "success",
                dataTable = response
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion

        #region Total Count Widget

        /// <summary>Shows the total count widget.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowTotalCountWidget()
        {
            var placesCount = await this.logisticSponsorRepo.CountAllByDataTableAsync(this.EditionDto.Id, true);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/TotalCountWidget", placesCount), divIdOrClass = "#LogisticSponsorsTotalCountWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Edition Count Widget

        /// <summary>Shows the edition count widget.</summary>
        /// <returns></returns>
        public async Task<ActionResult> ShowEditionCountWidget()
        {
            var placesCount = await this.logisticSponsorRepo.CountAllByDataTableAsync(this.EditionDto.Id, false);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/EditionCountWidget", placesCount), divIdOrClass = "#LogisticSponsorsEditionCountWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Create

        /// <summary>Shows the create modal.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowCreateModal(Guid? logisticSponsorUid)
        {
            var cmd = new CreateLogisticSponsor(
                logisticSponsorUid.HasValue ? await this.logisticSponsorRepo.FindDtoAsync(this.EditionDto.Id, logisticSponsorUid.Value) : null,
                await this.languageRepo.FindAllDtosAsync());

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Modals/CreateModal", cmd), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Creates the specified logistic sponsor.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Create(CreateLogisticSponsor cmd)
        {
            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

                cmd.UpdatePreSendProperties(
                    this.AdminAccessControlDto.User.Id,
                    this.AdminAccessControlDto.User.Uid,
                    this.EditionDto.Id,
                    this.EditionDto.Uid,
                    this.UserInterfaceLanguage);

                result = await this.CommandBus.Send(cmd);
                if (!result.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }
            }
            catch (DomainException ex)
            {
                foreach (var error in result.Errors)
                {
                    var target = error.Target ?? "";
                    ModelState.AddModelError(target, error.Message);
                }

                return Json(new
                {
                    status = "error",
                    message = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError")?.Message ?? ex.GetInnerMessage(),
                    pages = new List<dynamic>
                    {
                        new { page = this.RenderRazorViewToString("Modals/CreateForm", cmd), divIdOrClass = "#form-container" },
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Sponsor, Labels.CreatedM) });
        }

        #endregion

        #region Details

        /// <summary>Detailses the specified identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Details(Guid? id)
        {
            var logisticSponsorDto = await this.logisticSponsorRepo.FindDtoAsync(this.EditionDto.Id, id ?? Guid.Empty);
            if (logisticSponsorDto == null)
            {
                this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Sponsor, Labels.FoundM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "LogisticSponsors", new { Area = "" });
            }

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.Logistics, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Sponsors, Url.Action("Index", "LogisticSponsors", new { Area = "" })),
                new BreadcrumbItemHelper(logisticSponsorDto.LogisticSponsor?.Name?.GetSeparatorTranslation(this.UserInterfaceLanguage, '|'), Url.Action("Details", "LogisticSponsors", new { Area = "", id }))
            });

            #endregion

            return View(logisticSponsorDto);
        }

        #region Main Information Widget

        /// <summary>Shows the main information widget.</summary>
        /// <param name="logisticSponsorUid">The logistic sponsor uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowMainInformationWidget(Guid? logisticSponsorUid)
        {
            var mainInformationWidgetDto = await this.logisticSponsorRepo.FindMainInformationWidgetDtoAsync(this.EditionDto.Id, logisticSponsorUid ?? Guid.Empty);
            if (mainInformationWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Sponsor, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/MainInformationWidget", mainInformationWidgetDto), divIdOrClass = "#LogisticSponsorMainInformationWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #region Update

        /// <summary>Shows the update main information modal.</summary>
        /// <param name="logisticSponsorUid">The logistic sponsor uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowUpdateMainInformationModal(Guid? logisticSponsorUid)
        {
            UpdateLogisticSponsorMainInformation cmd;

            try
            {
                var mainInformationWidgetDto = await this.logisticSponsorRepo.FindMainInformationWidgetDtoAsync(this.EditionDto.Id, logisticSponsorUid ?? Guid.Empty);
                if (mainInformationWidgetDto == null)
                {
                    throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Sponsor, Labels.FoundM.ToLowerInvariant()));
                }

                cmd = new UpdateLogisticSponsorMainInformation(
                    mainInformationWidgetDto,
                    await this.languageRepo.FindAllDtosAsync());
            }
            catch (DomainException ex)
            {
                return Json(new { status = "error", message = ex.GetInnerMessage() }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Modals/UpdateMainInformationModal", cmd), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Updates the main information.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> UpdateMainInformation(UpdateLogisticSponsorMainInformation cmd)
        {
            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

                cmd.UpdatePreSendProperties(
                    this.AdminAccessControlDto.User.Id,
                    this.AdminAccessControlDto.User.Uid,
                    this.EditionDto.Id,
                    this.EditionDto.Uid,
                    this.UserInterfaceLanguage);
                result = await this.CommandBus.Send(cmd);
                if (!result.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }
            }
            catch (DomainException ex)
            {
                foreach (var error in result.Errors)
                {
                    var target = error.Target ?? "";
                    ModelState.AddModelError(target, error.Message);
                }

                return Json(new
                {
                    status = "error",
                    message = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError")?.Message ?? ex.GetInnerMessage(),
                    pages = new List<dynamic>
                    {
                        new { page = this.RenderRazorViewToString("Modals/UpdateMainInformationForm", cmd), divIdOrClass = "#form-container" },
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Sponsor, Labels.UpdatedM) });
        }

        #endregion

        #endregion

        #endregion

        #region Delete

        /// <summary>Deletes the specified logistic sponsor.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Delete(DeleteLogisticSponsor cmd)
        {
            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

                cmd.UpdatePreSendProperties(
                    this.AdminAccessControlDto.User.Id,
                    this.AdminAccessControlDto.User.Uid,
                    this.EditionDto.Id,
                    this.EditionDto.Uid,
                    this.UserInterfaceLanguage);

                result = await this.CommandBus.Send(cmd);
                if (!result.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }
            }
            catch (DomainException ex)
            {
                foreach (var error in result.Errors)
                {
                    var target = error.Target ?? "";
                    ModelState.AddModelError(target, error.Message);
                }

                return Json(new
                {
                    status = "error",
                    message = ex.GetInnerMessage(),
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Sponsor, Labels.DeletedM) });
        }

        #endregion
    }
}
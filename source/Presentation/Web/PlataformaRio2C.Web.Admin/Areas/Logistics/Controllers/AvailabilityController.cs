// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 12-04-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-04-2024
// ***********************************************************************
// <copyright file="AvailabilityController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using MediatR;
using PlataformaRio2C.Application;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;
using PlataformaRio2C.Web.Admin.Filters;
using Constants = PlataformaRio2C.Domain.Constants;
using PlataformaRio2C.Web.Admin.Controllers;

namespace PlataformaRio2C.Web.Admin.Areas.Logistics.Controllers
{
    /// <summary>
    /// AvailabilityController
    /// </summary>
    [AjaxAuthorize(Order = 1, Roles = Constants.Role.AnyAdmin)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.AdminLogistic)]
    public class AvailabilityController : BaseController
    {
        private readonly ILogisticRepository logisticRepo;
        private readonly ILogisticAirfareRepository AvailabilityAirfareRepo;
        private readonly ILogisticAccommodationRepository AvailabilityAccommodationRepo;
        private readonly ILogisticTransferRepository AvailabilityTransferRepo;
        private readonly IAttendeePlacesRepository attendeePlacesRepo;
        private readonly ICollaboratorRepository collaboratorRepo;
        private readonly IAttendeeCollaboratorRepository attendeeCollaboratorRepo;
        private readonly ILanguageRepository languageRepo;

        /// <summary>Initializes a new instance of the <see cref="AvailabilityController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="logisticRepository">The logistic repository.</param>
        /// <param name="AvailabilityAirfareRepository">The Availability airfare repository.</param>
        /// <param name="AvailabilityAccommodationRepository">The Availability accommodation repository.</param>
        /// <param name="AvailabilityTransferRepository">The Availability transfer repository.</param>
        /// <param name="attendeePlacesRepository">The attendee places repository.</param>
        /// <param name="collaboratorRepository">The collaborator repository.</param>
        /// <param name="attendeeCollaboratorRepository">The attendee collaborator repository.</param>
        /// <param name="languageRepository">The language repository.</param>
        public AvailabilityController(
            IMediator commandBus,
            IdentityAutenticationService identityController,
            ILogisticRepository logisticRepository,
            ILogisticAirfareRepository AvailabilityAirfareRepository,
            ILogisticAccommodationRepository AvailabilityAccommodationRepository,
            ILogisticTransferRepository AvailabilityTransferRepository,
            IAttendeePlacesRepository attendeePlacesRepository,
            ICollaboratorRepository collaboratorRepository,
            IAttendeeCollaboratorRepository attendeeCollaboratorRepository,
            ILanguageRepository languageRepository)
            : base(commandBus, identityController)
        {
            this.logisticRepo = logisticRepository;
            this.AvailabilityAirfareRepo = AvailabilityAirfareRepository;
            this.AvailabilityAccommodationRepo = AvailabilityAccommodationRepository;
            this.AvailabilityTransferRepo = AvailabilityTransferRepository;
            this.attendeePlacesRepo = attendeePlacesRepository;
            this.collaboratorRepo = collaboratorRepository;
            this.attendeeCollaboratorRepo = attendeeCollaboratorRepository;
            this.languageRepo = languageRepository;
        }

        #region List

        /// <summary>
        /// Indexes the specified search view model.
        /// </summary>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        public ActionResult Index(AvailabilitySearchViewModel searchViewModel)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.Availability, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Availability, Url.Action("Index", "Availability", new { Area = "Logistics" }))
            });

            #endregion

            return View(searchViewModel);
        }

        #region DataTable Widget

        /// <summary>
        /// Searches the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Search(IDataTablesRequest request)
        {
            var availabilities = await this.collaboratorRepo.FindAllAvailabilitiesByDataTable(
                request.Start / request.Length,
                request.Length,
                request.Search?.Value,
                request.GetSortColumns(),
                EditionDto.Id);

            var response = DataTablesResponse.Create(request, availabilities.TotalItemCount, availabilities.TotalItemCount, availabilities);

            return Json(new
            {
                status = "success",
                dataTable = response
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion

        #region Total Count Widget

        /// <summary>
        /// Shows the total count widget.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowTotalCountWidget()
        {
            var availabilitiesCount = await this.collaboratorRepo.CountAllAvailabilitiesByDataTable(true, this.EditionDto.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/TotalCountWidget", availabilitiesCount), divIdOrClass = "#AvailabilitiesTotalCountWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Edition Count Widget

        /// <summary>
        /// Shows the edition count widget.
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> ShowEditionCountWidget()
        {
            var availabilitiesCount = await this.collaboratorRepo.CountAllAvailabilitiesByDataTable(false, this.EditionDto.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/EditionCountWidget", availabilitiesCount), divIdOrClass = "#AvailabilitiesEditionCountWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        //#region Create

        ///// <summary>Shows the create modal.</summary>
        ///// <param name="attendeeCollaboratorUid">The attendee collaborator uid.</param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult> ShowCreateModal(Guid? attendeeCollaboratorUid)
        //{
        //    var cmd = new CreateLogistic(
        //        attendeeCollaboratorUid,
        //        (await attendeeAvailabilityponsorRepo.FindAllBaseDtosByIsOtherAsnyc(this.EditionDto.Id, false)).GetSeparatorTranslation(m => m.Name, this.UserInterfaceLanguage, Language.Separator),
        //        (await attendeeAvailabilityponsorRepo.FindAllBaseDtosByIsOtherAsnyc(this.EditionDto.Id, true)).GetSeparatorTranslation(m => m.Name, this.UserInterfaceLanguage, Language.Separator),
        //        UserInterfaceLanguage);

        //    return Json(new
        //    {
        //        status = "success",
        //        pages = new List<dynamic>
        //        {
        //            new { page = this.RenderRazorViewToString("Modals/CreateModal", cmd), divIdOrClass = "#GlobalModalContainer" },
        //        }
        //    }, JsonRequestBehavior.AllowGet);
        //}

        ///// <summary>Creates the specified logistic.</summary>
        ///// <param name="cmd">The command.</param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<ActionResult> Create(CreateLogistic cmd)
        //{
        //    var result = new AppValidationResult();

        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            throw new DomainException(Messages.CorrectFormValues);
        //        }

        //        cmd.UpdatePreSendProperties(
        //            this.AdminAccessControlDto.User.Id,
        //            this.AdminAccessControlDto.User.Uid,
        //            this.EditionDto.Id,
        //            this.EditionDto.Uid,
        //            this.UserInterfaceLanguage);

        //        result = await this.CommandBus.Send(cmd);
        //        if (!result.IsValid)
        //        {
        //            throw new DomainException(Messages.CorrectFormValues);
        //        }
        //    }
        //    catch (DomainException ex)
        //    {
        //        foreach (var error in result.Errors)
        //        {
        //            var target = error.Target ?? "";
        //            ModelState.AddModelError(target, error.Message);
        //        }

        //        cmd.UpdateModelsAndLists(
        //            (await attendeeAvailabilityponsorRepo.FindAllBaseDtosByIsOtherAsnyc(this.EditionDto.Id, false)).GetSeparatorTranslation(m => m.Name, this.UserInterfaceLanguage, Language.Separator),
        //            (await attendeeAvailabilityponsorRepo.FindAllBaseDtosByIsOtherAsnyc(this.EditionDto.Id, true)).GetSeparatorTranslation(m => m.Name, this.UserInterfaceLanguage, Language.Separator),
        //            UserInterfaceLanguage);

        //        return Json(new
        //        {
        //            status = "error",
        //            message = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError")?.Message ?? ex.GetInnerMessage(),
        //            pages = new List<dynamic>
        //            {
        //                new { page = this.RenderRazorViewToString("Modals/CreateForm", cmd), divIdOrClass = "#form-container"}
        //            }
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        //        return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Request, Labels.CreatedF) });
        //}

        //#endregion

        //#region Details

        ///// <summary>Detailses the specified identifier.</summary>
        ///// <param name="id">The identifier.</param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult> Details(Guid? id)
        //{
        //    var logisticDto = await this.logisticRepo.FindDtoAsync(id ?? Guid.Empty, this.languageRepo.Get(f => f.Code == UserInterfaceLanguage));
        //    if (logisticDto == null)
        //    {
        //        this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Availability, Labels.FoundF.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
        //        return RedirectToAction("Index", "Availability", new { Area = "Logistics" });
        //    }

        //    #region Breadcrumb

        //    ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.Availability, new List<BreadcrumbItemHelper> {
        //        new BreadcrumbItemHelper(Labels.Requests, Url.Action("Index", "Availability", new { Area ="", id })),
        //        new BreadcrumbItemHelper(logisticDto?.AttendeeCollaboratorDto?.Collaborator?.GetDisplayName(), Url.Action("Details", "Availability", new { Area ="", id }))
        //    });

        //    #endregion

        //    return View(logisticDto);
        //}

        //#region Main Information Widget

        ///// <summary>Shows the main information widget.</summary>
        ///// <param name="AvailabilityUid">The Availability uid.</param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult> ShowMainInformationWidget(Guid? AvailabilityUid)
        //{
        //    var mainInformationWidgetDto = await this.logisticRepo.FindMainInformationWidgetDtoAsync(AvailabilityUid ?? Guid.Empty);
        //    if (mainInformationWidgetDto == null)
        //    {
        //        return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Request, Labels.FoundF.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(new
        //    {
        //        status = "success",
        //        pages = new List<dynamic>
        //        {
        //            new { page = this.RenderRazorViewToString("Widgets/MainInformationWidget", mainInformationWidgetDto), divIdOrClass = "#AvailabilityMainInformationWidget" },
        //        }
        //    }, JsonRequestBehavior.AllowGet);
        //}

        //#region Update

        ///// <summary>Shows the update main information modal.</summary>
        ///// <param name="AvailabilityUid">The Availability uid.</param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult> ShowUpdateMainInformationModal(Guid? AvailabilityUid)
        //{
        //    UpdateLogisticMainInformation cmd;

        //    try
        //    {
        //        var mainInformationWidgetDto = await this.logisticRepo.FindMainInformationWidgetDtoAsync(AvailabilityUid ?? Guid.Empty);
        //        if (mainInformationWidgetDto == null)
        //        {
        //            throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Request, Labels.FoundF.ToLowerInvariant()));
        //        }

        //        cmd = new UpdateLogisticMainInformation(
        //            mainInformationWidgetDto,
        //            await attendeeAvailabilityponsorRepo.FindOtherDtoAsync(this.EditionDto.Id),
        //            (await attendeeAvailabilityponsorRepo.FindAllBaseDtosByIsOtherAsnyc(this.EditionDto.Id, false)).GetSeparatorTranslation(m => m.Name, this.UserInterfaceLanguage, Language.Separator),
        //            (await attendeeAvailabilityponsorRepo.FindAllBaseDtosByIsOtherAsnyc(this.EditionDto.Id, true)).GetSeparatorTranslation(m => m.Name, this.UserInterfaceLanguage, Language.Separator),
        //            UserInterfaceLanguage);
        //    }
        //    catch (DomainException ex)
        //    {
        //        return Json(new { status = "error", message = ex.GetInnerMessage() }, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(new
        //    {
        //        status = "success",
        //        pages = new List<dynamic>
        //        {
        //            new { page = this.RenderRazorViewToString("Modals/UpdateMainInformationModal", cmd), divIdOrClass = "#GlobalModalContainer" },
        //        }
        //    }, JsonRequestBehavior.AllowGet);
        //}

        ///// <summary>Updates the main information.</summary>
        ///// <param name="cmd">The command.</param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<ActionResult> UpdateMainInformation(UpdateLogisticMainInformation cmd)
        //{
        //    var result = new AppValidationResult();

        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            throw new DomainException(Messages.CorrectFormValues);
        //        }

        //        cmd.UpdatePreSendProperties(
        //            this.AdminAccessControlDto.User.Id,
        //            this.AdminAccessControlDto.User.Uid,
        //            this.EditionDto.Id,
        //            this.EditionDto.Uid,
        //            this.UserInterfaceLanguage);

        //        result = await this.CommandBus.Send(cmd);
        //        if (!result.IsValid)
        //        {
        //            throw new DomainException(Messages.CorrectFormValues);
        //        }
        //    }
        //    catch (DomainException ex)
        //    {
        //        foreach (var error in result.Errors)
        //        {
        //            var target = error.Target ?? "";
        //            ModelState.AddModelError(target, error.Message);
        //        }

        //        cmd.UpdateModelsAndLists(
        //            (await attendeeAvailabilityponsorRepo.FindAllBaseDtosByIsOtherAsnyc(this.EditionDto.Id, false)).GetSeparatorTranslation(m => m.Name, this.UserInterfaceLanguage, Language.Separator),
        //            (await attendeeAvailabilityponsorRepo.FindAllBaseDtosByIsOtherAsnyc(this.EditionDto.Id, true)).GetSeparatorTranslation(m => m.Name, this.UserInterfaceLanguage, Language.Separator),
        //            UserInterfaceLanguage);

        //        return Json(new
        //        {
        //            status = "error",
        //            message = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError")?.Message ?? ex.GetInnerMessage(),
        //            pages = new List<dynamic>
        //            {
        //                new { page = this.RenderRazorViewToString("Modals/UpdateMainInformationForm", cmd), divIdOrClass = "#form-container" },
        //            }
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        //        return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Request, Labels.UpdatedF) });
        //}

        //#endregion

        //#endregion

        //#region Airfare Widget

        ///// <summary>Shows the airfare widget.</summary>
        ///// <param name="AvailabilityUid">The Availability uid.</param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult> ShowAirfareWidget(Guid? AvailabilityUid)
        //{
        //    var logisticAirfareDtos = await this.AvailabilityAirfareRepo.FindAllDtosAsync(AvailabilityUid ?? Guid.Empty);
        //    if (logisticAirfareDtos == null)
        //    {
        //        return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Request, Labels.FoundF.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(new
        //    {
        //        status = "success",
        //        pages = new List<dynamic>
        //        {
        //            new { page = this.RenderRazorViewToString("Widgets/AvailabilityAirfareWidget", logisticAirfareDtos), divIdOrClass = "#AvailabilityAirfareWidget" },
        //        }
        //    }, JsonRequestBehavior.AllowGet);
        //}

        //#region Create

        ///// <summary>Shows the create airfare modal.</summary>
        ///// <param name="AvailabilityUid">The Availability uid.</param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult> ShowCreateAirfareModal(Guid? AvailabilityUid)
        //{
        //    var cmd = new CreateLogisticAirfare(AvailabilityUid ?? Guid.Empty);

        //    return Json(new
        //    {
        //        status = "success",
        //        pages = new List<dynamic>
        //        {
        //            new { page = this.RenderRazorViewToString("Modals/CreateAirfareModal", cmd), divIdOrClass = "#GlobalModalContainer" },
        //        }
        //    }, JsonRequestBehavior.AllowGet);
        //}

        ///// <summary>Creates the airfare.</summary>
        ///// <param name="cmd">The command.</param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<ActionResult> CreateAirfare(CreateLogisticAirfare cmd)
        //{
        //    var result = new AppValidationResult();

        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            throw new DomainException(Messages.CorrectFormValues);
        //        }

        //        cmd.UpdatePreSendProperties(
        //            this.AdminAccessControlDto.User.Id,
        //            this.AdminAccessControlDto.User.Uid,
        //            this.EditionDto.Id,
        //            this.EditionDto.Uid,
        //            this.UserInterfaceLanguage);

        //        result = await this.CommandBus.Send(cmd);
        //        if (!result.IsValid)
        //        {
        //            throw new DomainException(Messages.CorrectFormValues);
        //        }
        //    }
        //    catch (DomainException ex)
        //    {
        //        foreach (var error in result.Errors)
        //        {
        //            var target = error.Target ?? "";
        //            ModelState.AddModelError(target, error.Message);
        //        }

        //        return Json(new
        //        {
        //            status = "error",
        //            message = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError")?.Message ?? ex.GetInnerMessage(),
        //            pages = new List<dynamic>
        //            {
        //                new { page = this.RenderRazorViewToString("Modals/_AirfareForm", cmd), divIdOrClass = "#form-container" },
        //            }
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        //        return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Airfare, Labels.CreatedM) });
        //}

        //#endregion

        //#region Update

        ///// <summary>Shows the update airfare modal.</summary>
        ///// <param name="uid">The uid.</param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult> ShowUpdateAirfareModal(Guid? uid)
        //{
        //    UpdateLogisticAirfare cmd;

        //    try
        //    {
        //        var logisticAirfareDto = await this.AvailabilityAirfareRepo.FindDtoAsync(uid ?? Guid.Empty);
        //        if (logisticAirfareDto == null)
        //        {
        //            throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Request, Labels.FoundF.ToLowerInvariant()));
        //        }

        //        cmd = new UpdateLogisticAirfare(logisticAirfareDto);
        //    }
        //    catch (DomainException ex)
        //    {
        //        return Json(new { status = "error", message = ex.GetInnerMessage() }, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(new
        //    {
        //        status = "success",
        //        pages = new List<dynamic>
        //        {
        //            new { page = this.RenderRazorViewToString("Modals/UpdateAirfareModal", cmd), divIdOrClass = "#GlobalModalContainer" },
        //        }
        //    }, JsonRequestBehavior.AllowGet);
        //}

        ///// <summary>Updates the airfare.</summary>
        ///// <param name="cmd">The command.</param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<ActionResult> UpdateAirfare(UpdateLogisticAirfare cmd)
        //{
        //    var result = new AppValidationResult();

        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            throw new DomainException(Messages.CorrectFormValues);
        //        }

        //        cmd.UpdatePreSendProperties(
        //            this.AdminAccessControlDto.User.Id,
        //            this.AdminAccessControlDto.User.Uid,
        //            this.EditionDto.Id,
        //            this.EditionDto.Uid,
        //            this.UserInterfaceLanguage);

        //        result = await this.CommandBus.Send(cmd);
        //        if (!result.IsValid)
        //        {
        //            throw new DomainException(Messages.CorrectFormValues);
        //        }
        //    }
        //    catch (DomainException ex)
        //    {
        //        foreach (var error in result.Errors)
        //        {
        //            var target = error.Target ?? "";
        //            ModelState.AddModelError(target, error.Message);
        //        }

        //        return Json(new
        //        {
        //            status = "error",
        //            message = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError")?.Message ?? ex.GetInnerMessage(),
        //            pages = new List<dynamic>
        //            {
        //                new { page = this.RenderRazorViewToString("Modals/_AirfareForm", cmd), divIdOrClass = "#form-container" },
        //            }
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        //        return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Airfare, Labels.UpdatedM) });
        //}

        //#endregion

        //#region Delete

        ///// <summary>Deletes the logistic airfare.</summary>
        ///// <param name="cmd">The command.</param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<ActionResult> DeleteLogisticAirfare(DeleteLogisticAirfare cmd)
        //{
        //    var result = new AppValidationResult();

        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            throw new DomainException(Messages.CorrectFormValues);
        //        }

        //        cmd.UpdatePreSendProperties(
        //            this.AdminAccessControlDto.User.Id,
        //            this.AdminAccessControlDto.User.Uid,
        //            this.EditionDto.Id,
        //            this.EditionDto.Uid,
        //            this.UserInterfaceLanguage);

        //        result = await this.CommandBus.Send(cmd);
        //        if (!result.IsValid)
        //        {
        //            throw new DomainException(Messages.CorrectFormValues);
        //        }
        //    }
        //    catch (DomainException ex)
        //    {
        //        foreach (var error in result.Errors)
        //        {
        //            var target = error.Target ?? "";
        //            ModelState.AddModelError(target, error.Message);
        //        }

        //        return Json(new
        //        {
        //            status = "error",
        //            message = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError")?.Message ?? ex.GetInnerMessage(),
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        //        return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Airfare, Labels.DeletedM) });
        //}

        //#endregion

        //#endregion

        //#region Accommodation Widget

        ///// <summary>Shows the accommodation widget.</summary>
        ///// <param name="AvailabilityUid">The Availability uid.</param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult> ShowAccommodationWidget(Guid? AvailabilityUid)
        //{
        //    var AvailabilityRequestDto = await this.AvailabilityAccommodationRepo.FindAllDtosAsync(AvailabilityUid ?? Guid.Empty);
        //    if (AvailabilityRequestDto == null)
        //    {
        //        return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Request, Labels.FoundF.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(new
        //    {
        //        status = "success",
        //        pages = new List<dynamic>
        //        {
        //            new { page = this.RenderRazorViewToString("Widgets/AvailabilityAccommodationWidget", AvailabilityRequestDto), divIdOrClass = "#AvailabilityAccommodationWidget" },
        //        }
        //    }, JsonRequestBehavior.AllowGet);
        //}

        //#region Create

        ///// <summary>Shows the create accommodation modal.</summary>
        ///// <param name="AvailabilityUid">The Availability uid.</param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult> ShowCreateAccommodationModal(Guid? AvailabilityUid)
        //{
        //    var cmd = new CreateLogisticAccommodation(
        //        AvailabilityUid ?? Guid.Empty,
        //        await this.attendeePlacesRepo.FindAllDropdownDtosAsync(EditionDto.Id, true));

        //    return Json(new
        //    {
        //        status = "success",
        //        pages = new List<dynamic>
        //        {
        //            new { page = this.RenderRazorViewToString("Modals/CreateAccommodationModal", cmd), divIdOrClass = "#GlobalModalContainer" },
        //        }
        //    }, JsonRequestBehavior.AllowGet);
        //}

        ///// <summary>Creates the accommodation.</summary>
        ///// <param name="cmd">The command.</param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<ActionResult> CreateAccommodation(CreateLogisticAccommodation cmd)
        //{
        //    var result = new AppValidationResult();

        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            throw new DomainException(Messages.CorrectFormValues);
        //        }

        //        cmd.UpdatePreSendProperties(
        //            this.AdminAccessControlDto.User.Id,
        //            this.AdminAccessControlDto.User.Uid,
        //            this.EditionDto.Id,
        //            this.EditionDto.Uid,
        //            this.UserInterfaceLanguage);

        //        result = await this.CommandBus.Send(cmd);
        //        if (!result.IsValid)
        //        {
        //            throw new DomainException(Messages.CorrectFormValues);
        //        }
        //    }
        //    catch (DomainException ex)
        //    {
        //        foreach (var error in result.Errors)
        //        {
        //            var target = error.Target ?? "";
        //            ModelState.AddModelError(target, error.Message);
        //        }

        //        cmd.UpdateLists(
        //            await attendeePlacesRepo.FindAllDropdownDtosAsync(EditionDto.Id, true));

        //        return Json(new
        //        {
        //            status = "error",
        //            message = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError")?.Message ?? ex.GetInnerMessage(),
        //            pages = new List<dynamic>
        //            {
        //                new { page = this.RenderRazorViewToString("Modals/_AccommodationForm", cmd), divIdOrClass = "#form-container" },
        //            }
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        //        return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Accommodation, Labels.CreatedF) });
        //}

        //#endregion

        //#region Update

        ///// <summary>Shows the update accommodation modal.</summary>
        ///// <param name="uid">The uid.</param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult> ShowUpdateAccommodationModal(Guid? uid)
        //{
        //    UpdateLogisticAccommodation cmd;

        //    try
        //    {
        //        var entity = await this.AvailabilityAccommodationRepo.GetAsync(uid ?? Guid.Empty);
        //        if (entity == null)
        //        {
        //            throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Request, Labels.FoundF.ToLowerInvariant()));
        //        }

        //        cmd = new UpdateLogisticAccommodation(
        //            entity.Uid,
        //            entity.CheckInDate,
        //            entity.CheckOutDate,
        //            entity.AdditionalInfo,
        //            entity.AttendeePlaceId,
        //            await attendeePlacesRepo.FindAllDropdownDtosAsync(EditionDto.Id, true));
        //    }
        //    catch (DomainException ex)
        //    {
        //        return Json(new { status = "error", message = ex.GetInnerMessage() }, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(new
        //    {
        //        status = "success",
        //        pages = new List<dynamic>
        //        {
        //            new { page = this.RenderRazorViewToString("Modals/UpdateAccommodationModal", cmd), divIdOrClass = "#GlobalModalContainer" },
        //        }
        //    }, JsonRequestBehavior.AllowGet);
        //}

        ///// <summary>Updates the accommodation.</summary>
        ///// <param name="cmd">The command.</param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<ActionResult> UpdateAccommodation(UpdateLogisticAccommodation cmd)
        //{
        //    var result = new AppValidationResult();

        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            throw new DomainException(Messages.CorrectFormValues);
        //        }

        //        cmd.UpdatePreSendProperties(
        //            this.AdminAccessControlDto.User.Id,
        //            this.AdminAccessControlDto.User.Uid,
        //            this.EditionDto.Id,
        //            this.EditionDto.Uid,
        //            this.UserInterfaceLanguage);

        //        result = await this.CommandBus.Send(cmd);
        //        if (!result.IsValid)
        //        {
        //            throw new DomainException(Messages.CorrectFormValues);
        //        }
        //    }
        //    catch (DomainException ex)
        //    {
        //        foreach (var error in result.Errors)
        //        {
        //            var target = error.Target ?? "";
        //            ModelState.AddModelError(target, error.Message);
        //        }

        //        cmd.UpdateLists(
        //            await attendeePlacesRepo.FindAllDropdownDtosAsync(EditionDto.Id, true));

        //        return Json(new
        //        {
        //            status = "error",
        //            message = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError")?.Message ?? ex.GetInnerMessage(),
        //            pages = new List<dynamic>
        //            {
        //                new { page = this.RenderRazorViewToString("Modals/_AccommodationForm", cmd), divIdOrClass = "#form-container" },
        //            }
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        //        return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Accommodation, Labels.UpdatedF) });
        //}

        //#endregion

        //#region Delete

        ///// <summary>Deletes the logistic accommodation.</summary>
        ///// <param name="cmd">The command.</param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<ActionResult> DeleteLogisticAccommodation(DeleteLogisticAccommodation cmd)
        //{
        //    var result = new AppValidationResult();

        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            throw new DomainException(Messages.CorrectFormValues);
        //        }

        //        cmd.UpdatePreSendProperties(
        //            this.AdminAccessControlDto.User.Id,
        //            this.AdminAccessControlDto.User.Uid,
        //            this.EditionDto.Id,
        //            this.EditionDto.Uid,
        //            this.UserInterfaceLanguage);

        //        result = await this.CommandBus.Send(cmd);
        //        if (!result.IsValid)
        //        {
        //            throw new DomainException(Messages.CorrectFormValues);
        //        }
        //    }
        //    catch (DomainException ex)
        //    {
        //        foreach (var error in result.Errors)
        //        {
        //            var target = error.Target ?? "";
        //            ModelState.AddModelError(target, error.Message);
        //        }

        //        return Json(new
        //        {
        //            status = "error",
        //            message = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError")?.Message ?? ex.GetInnerMessage(),
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        //        return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Accommodation, Labels.DeletedF) });
        //}

        //#endregion

        //#endregion

        //#region Transfer Widget

        ///// <summary>Shows the transfer widget.</summary>
        ///// <param name="AvailabilityUid">The Availability uid.</param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult> ShowTransferWidget(Guid? AvailabilityUid)
        //{
        //    var AvailabilityRequestDto = await this.AvailabilityTransferRepo.FindAllDtosAsync(AvailabilityUid ?? Guid.Empty);
        //    if (AvailabilityRequestDto == null)
        //    {
        //        return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Request, Labels.FoundF.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(new
        //    {
        //        status = "success",
        //        pages = new List<dynamic>
        //        {
        //            new { page = this.RenderRazorViewToString("Widgets/AvailabilityTransferWidget", AvailabilityRequestDto), divIdOrClass = "#AvailabilityTransferWidget" },
        //        }
        //    }, JsonRequestBehavior.AllowGet);
        //}

        //#region Create

        ///// <summary>Shows the create transfer modal.</summary>
        ///// <param name="AvailabilityUid">The Availability uid.</param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult> ShowCreateTransferModal(Guid? AvailabilityUid)
        //{
        //    var cmd = new CreateLogisticTransfer(
        //        AvailabilityUid ?? Guid.Empty,
        //        (await attendeePlacesRepo.FindAllDropdownDtosAsync(EditionDto.Id))?.GetSeparatorTranslation(m => m.Name, this.UserInterfaceLanguage, Language.Separator));

        //    return Json(new
        //    {
        //        status = "success",
        //        pages = new List<dynamic>
        //        {
        //            new { page = this.RenderRazorViewToString("Modals/CreateTransferModal", cmd), divIdOrClass = "#GlobalModalContainer" },
        //        }
        //    }, JsonRequestBehavior.AllowGet);
        //}

        ///// <summary>Creates the transfer.</summary>
        ///// <param name="cmd">The command.</param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<ActionResult> CreateTransfer(CreateLogisticTransfer cmd)
        //{
        //    var result = new AppValidationResult();

        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            throw new DomainException(Messages.CorrectFormValues);
        //        }

        //        cmd.UpdatePreSendProperties(
        //            this.AdminAccessControlDto.User.Id,
        //            this.AdminAccessControlDto.User.Uid,
        //            this.EditionDto.Id,
        //            this.EditionDto.Uid,
        //            this.UserInterfaceLanguage);

        //        result = await this.CommandBus.Send(cmd);
        //        if (!result.IsValid)
        //        {
        //            throw new DomainException(Messages.CorrectFormValues);
        //        }
        //    }
        //    catch (DomainException ex)
        //    {
        //        foreach (var error in result.Errors)
        //        {
        //            var target = error.Target ?? "";
        //            ModelState.AddModelError(target, error.Message);
        //        }

        //        cmd.UpdateLists(
        //            (await attendeePlacesRepo.FindAllDropdownDtosAsync(EditionDto.Id))?.GetSeparatorTranslation(m => m.Name, this.UserInterfaceLanguage, Language.Separator));

        //        return Json(new
        //        {
        //            status = "error",
        //            message = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError")?.Message ?? ex.GetInnerMessage(),
        //            pages = new List<dynamic>
        //            {
        //                new { page = this.RenderRazorViewToString("Modals/_TransferForm", cmd), divIdOrClass = "#form-container" },
        //            }
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        //        cmd.UpdateLists(await attendeePlacesRepo.FindAllDropdownDtosAsync(EditionDto.Id));
        //        return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Transfer, Labels.CreatedM) });
        //}

        //#endregion

        //#region Update

        ///// <summary>Shows the update transfer modal.</summary>
        ///// <param name="uid">The uid.</param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult> ShowUpdateTransferModal(Guid? uid)
        //{
        //    UpdateLogisticTransfer cmd;

        //    try
        //    {
        //        var entity = await this.AvailabilityTransferRepo.GetAsync(uid ?? Guid.Empty);
        //        if (entity == null)
        //        {
        //            throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Request, Labels.FoundF.ToLowerInvariant()));
        //        }

        //        cmd = new UpdateLogisticTransfer(entity.Uid, entity.FromAttendeePlaceId, entity.ToAttendeePlaceId, entity.Date, entity.AdditionalInfo, await attendeePlacesRepo.FindAllDropdownDtosAsync(EditionDto.Id));
        //    }
        //    catch (DomainException ex)
        //    {
        //        return Json(new { status = "error", message = ex.GetInnerMessage() }, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(new
        //    {
        //        status = "success",
        //        pages = new List<dynamic>
        //        {
        //            new { page = this.RenderRazorViewToString("Modals/UpdateTransferModal", cmd), divIdOrClass = "#GlobalModalContainer" },
        //        }
        //    }, JsonRequestBehavior.AllowGet);
        //}

        ///// <summary>Updates the transfer.</summary>
        ///// <param name="cmd">The command.</param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<ActionResult> UpdateTransfer(UpdateLogisticTransfer cmd)
        //{
        //    var result = new AppValidationResult();

        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            throw new DomainException(Messages.CorrectFormValues);
        //        }

        //        cmd.UpdatePreSendProperties(
        //            this.AdminAccessControlDto.User.Id,
        //            this.AdminAccessControlDto.User.Uid,
        //            this.EditionDto.Id,
        //            this.EditionDto.Uid,
        //            this.UserInterfaceLanguage);

        //        result = await this.CommandBus.Send(cmd);
        //        if (!result.IsValid)
        //        {
        //            throw new DomainException(Messages.CorrectFormValues);
        //        }
        //    }
        //    catch (DomainException ex)
        //    {
        //        foreach (var error in result.Errors)
        //        {
        //            var target = error.Target ?? "";
        //            ModelState.AddModelError(target, error.Message);
        //        }

        //        return Json(new
        //        {
        //            status = "error",
        //            message = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError")?.Message ?? ex.GetInnerMessage(),
        //            pages = new List<dynamic>
        //            {
        //                new { page = this.RenderRazorViewToString("Modals/_TransferForm", cmd), divIdOrClass = "#form-container" },
        //            }
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        //        return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Transfer, Labels.UpdatedM) });
        //}

        //#endregion

        //#region Delete

        ///// <summary>Deletes the logistic transfer.</summary>
        ///// <param name="cmd">The command.</param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<ActionResult> DeleteLogisticTransfer(DeleteLogisticTransfer cmd)
        //{
        //    var result = new AppValidationResult();

        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            throw new DomainException(Messages.CorrectFormValues);
        //        }

        //        cmd.UpdatePreSendProperties(
        //            this.AdminAccessControlDto.User.Id,
        //            this.AdminAccessControlDto.User.Uid,
        //            this.EditionDto.Id,
        //            this.EditionDto.Uid,
        //            this.UserInterfaceLanguage);

        //        result = await this.CommandBus.Send(cmd);
        //        if (!result.IsValid)
        //        {
        //            throw new DomainException(Messages.CorrectFormValues);
        //        }
        //    }
        //    catch (DomainException ex)
        //    {
        //        foreach (var error in result.Errors)
        //        {
        //            var target = error.Target ?? "";
        //            ModelState.AddModelError(target, error.Message);
        //        }

        //        return Json(new
        //        {
        //            status = "error",
        //            message = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError")?.Message ?? ex.GetInnerMessage(),
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        //        return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Transfer, Labels.DeletedM) });
        //}

        //#endregion

        //#endregion

        //#endregion

        //#region Delete

        ///// <summary>Deletes the specified logistic.</summary>
        ///// <param name="cmd">The command.</param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<ActionResult> Delete(DeleteLogistic cmd)
        //{
        //    var result = new AppValidationResult();

        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            throw new DomainException(Messages.CorrectFormValues);
        //        }

        //        cmd.UpdatePreSendProperties(
        //            this.AdminAccessControlDto.User.Id,
        //            this.AdminAccessControlDto.User.Uid,
        //            this.EditionDto.Id,
        //            this.EditionDto.Uid,
        //            this.UserInterfaceLanguage);

        //        result = await this.CommandBus.Send(cmd);
        //        if (!result.IsValid)
        //        {
        //            throw new DomainException(Messages.CorrectFormValues);
        //        }
        //    }
        //    catch (DomainException ex)
        //    {
        //        foreach (var error in result.Errors)
        //        {
        //            var target = error.Target ?? "";
        //            ModelState.AddModelError(target, error.Message);
        //        }

        //        return Json(new
        //        {
        //            status = "error",
        //            message = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError")?.Message ?? ex.GetInnerMessage(),
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        //        return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Request, Labels.DeletedF) });
        //}

        //#endregion

        //#region Logistic Info Widget

        ///// <summary>Shows the information widget.</summary>
        ///// <param name="collaboratorUid">The collaborator uid.</param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult> ShowInfoWidget(Guid? collaboratorUid)
        //{
        //    var logisticInfoWidgetDto = await this.attendeeCollaboratorRepo.FindLogisticInfoWidgetDtoAsync(collaboratorUid ?? Guid.Empty, this.EditionDto.Id);
        //    if (logisticInfoWidgetDto == null)
        //    {
        //        return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Availability, Labels.FoundMP.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
        //    }

        //    ViewBag.HideActions = true;

        //    return Json(new
        //    {
        //        status = "success",
        //        pages = new List<dynamic>
        //        {
        //            new { page = this.RenderRazorViewToString("Widgets/InfoWidget", logisticInfoWidgetDto), divIdOrClass = "#AvailabilityInfoWidget" },
        //        }
        //    }, JsonRequestBehavior.AllowGet);
        //}

        //#endregion
    }
}
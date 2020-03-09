// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Arthur Souza
// Created          : 01-20-2020
//
// Last Modified By : Arthur Souza
// Last Modified On : 03-06-2020
// ***********************************************************************
// <copyright file="LogisticSponsorsController.cs" company="Softo">
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
using Newtonsoft.Json;
using PlataformaRio2c.Infra.Data.FileRepository;
using PlataformaRio2C.Application;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Application.CQRS.Queries;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Statics;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.Eventbrite.Models;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;
using PlataformaRio2C.Web.Admin.Filters;
using Constants = PlataformaRio2C.Domain.Constants;
using PagedListExtensions = X.PagedList.PagedListExtensions;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    /// <summary>
    /// LogisticSponsorsController
    /// </summary>
    [AjaxAuthorize(Order = 1, Roles = Constants.Role.AnyAdmin)]
    [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.AdminAudiovisual + "," + Constants.CollaboratorType.CuratorshipAudiovisual)]
    public class LogisticsController : BaseController
    {
        //private readonly ICollaboratorRepository collaboratorRepo;
        //private readonly IAttendeeCollaboratorRepository attendeeCollaboratorRepo;
        //private readonly IAttendeeSalesPlatformTicketTypeRepository attendeeSalesPlatformTicketTypeRepo;
        //private readonly IFileRepository fileRepo;

        /// <summary>
        /// The logistic sponsor repo
        /// </summary>
        private readonly ILogisticSponsorRepository logisticSponsorRepo;

        private IAttendeeLogisticSponsorRepository attendeeLogisticSponsorRepo;
        private readonly IAttendeeCollaboratorRepository attendeCollaboratorRepo;
        /// <summary>
        /// The logistics repo
        /// </summary>
        private readonly ILogisticsRepository logisticsRepo;
        private readonly ILogisticAirfareRepository logisticsAirfareRepo;
        /// <summary>
        /// The logistics accommodation repo
        /// </summary>
        private readonly ILogisticAccommodationRepository logisticsAccommodationRepo;
        private readonly ILogisticTransferRepository logisticsTransferRepo;
        private readonly IAttendeePlacesRepository attendeePlacesRepo;
        /// <summary>
        /// The language repo
        /// </summary>
        private readonly ILanguageRepository languageRepo;
        /// <summary>
        /// The collaborator repo
        /// </summary>
        private readonly ICollaboratorRepository collaboratorRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpeakersController" /> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="logisticSponsorRepo">The logistic sponsor repo.</param>
        /// <param name="collaboratorRepo">The collaborator repo.</param>
        /// <param name="logisticsRepo">The logistics repo.</param>
        /// <param name="logisticsAccommodationRepo">The logistics accommodation repo.</param>
        /// <param name="languageRepo">The language repo.</param>
        public LogisticsController(
            IMediator commandBus, 
            IdentityAutenticationService identityController,
            ILogisticSponsorRepository logisticSponsorRepo,
            ICollaboratorRepository collaboratorRepo,
            ILogisticsRepository logisticsRepo,
            ILogisticAccommodationRepository logisticsAccommodationRepo,
            ILogisticAirfareRepository logisticsAirfareRepo,
            IAttendeePlacesRepository attendeePlacesRepo,
            IAttendeeLogisticSponsorRepository attendeeLogisticSponsorRepo,
            ILogisticTransferRepository logisticsTransferRepo,
            IAttendeeCollaboratorRepository attendeCollaboratorRepo,
            ILanguageRepository languageRepo)
            : base(commandBus, identityController)
        {
            this.logisticSponsorRepo = logisticSponsorRepo;
            this.logisticsRepo = logisticsRepo;
            this.languageRepo = languageRepo;
            this.attendeePlacesRepo = attendeePlacesRepo;
            this.logisticsTransferRepo = logisticsTransferRepo;
            this.logisticsAirfareRepo = logisticsAirfareRepo;
            this.collaboratorRepo = collaboratorRepo;
            this.attendeCollaboratorRepo = attendeCollaboratorRepo;
            this.logisticsAccommodationRepo = logisticsAccommodationRepo;
            this.attendeeLogisticSponsorRepo = attendeeLogisticSponsorRepo;
        }

        #region List

        /// <summary>
        /// Indexes the specified search view model.
        /// </summary>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        public ActionResult Index(LogisticRequestSearchViewModel searchViewModel)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.Logistics, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Requests, Url.Action("Index", "Logistics", new { Area = "" }))
            });

            #endregion

            return View(searchViewModel);
        }

        #region DataTable Widget

        /// <summary>
        /// Searches the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="showAllParticipants">if set to <c>true</c> [show all participants].</param>
        /// <param name="showAllSponsored">if set to <c>true</c> [show all sponsored].</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        [HttpGet]
        public async Task<ActionResult> Search(IDataTablesRequest request, bool showAllParticipants, bool showAllSponsored)
        {
            var list = await this.collaboratorRepo.FindAllLogisticsByDatatable(
                EditionDto.Id,
                request.Start / request.Length,
                request.Length,
                request.Search?.Value,
                request.GetSortColumns(),
                showAllParticipants,
                showAllSponsored);
            
            var response = DataTablesResponse.Create(request, 100, 100, list);
            
            return Json(new
            {
                status = "success",
                dataTable = response
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion

        #region Create

        /// <summary>
        /// Shows the create modal.
        /// </summary>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        [HttpGet]
        public async Task<ActionResult> ShowCreateModal()
        {
            var cmd = new CreateLogisticRequest(
                await attendeeLogisticSponsorRepo.FindAllDtosByIsOther(this.EditionDto.Id, true),
                UserInterfaceLanguage);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Modals/CreateModal", cmd), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Creates the specified collaborator.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        /// <exception cref="DomainException"></exception>
        /// <exception cref="DomainException"></exception>
        [HttpPost]
        public async Task<ActionResult> Create(CreateLogisticRequest cmd)
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
                    pages = new List<dynamic>
                    {
                        new { page = this.RenderRazorViewToString("Modals/_TinyForm", cmd), divIdOrClass = "#form-container" },
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Speaker, Labels.CreatedM) });
        }

        #endregion

        #region Update

        /// <summary>
        /// Shows the update modal.
        /// </summary>
        /// <param name="sponsorUid">The sponsor uid.</param>
        /// <param name="isAddingToCurrentEdition">The is adding to current edition.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        [HttpGet]
        public async Task<ActionResult> ShowUpdateMainInformationModal(Guid? logisticsUid)
        {
            UpdateLogisticRequest cmd;

            try
            {
                var entity = await this.logisticsRepo.GetAsync(logisticsUid ?? Guid.Empty);
                if (entity == null)
                {
                    throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Request, Labels.FoundF.ToLowerInvariant()));
                }

                cmd = new UpdateLogisticRequest(
                    entity.Uid,
                    await attendeCollaboratorRepo.GetAsync(entity.AttendeeCollaboratorId),
                    await logisticSponsorRepo.GetByIsOthersRequired(),
                    entity.AccommodationSponsor,
                    entity.AirfareSponsor,
                    entity.AirportTransferSponsor,
                    entity.IsVehicleDisposalRequired,
                    entity.IsCityTransferRequired,
                    await logisticSponsorRepo.FindAllDtosByEditionUidAsync(this.EditionDto.Id),
                    UserInterfaceLanguage);
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

        /// <summary>
        /// Updates the specified tiny collaborator.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        /// <exception cref="DomainException"></exception>
        /// <exception cref="DomainException"></exception>
        [HttpPost]
        public async Task<ActionResult> UpdateRequest(UpdateLogisticRequest cmd)
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
                    pages = new List<dynamic>
                    {
                        new { page = this.RenderRazorViewToString("Modals/_CreateForm", cmd), divIdOrClass = "#form-container" },
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Speaker, Labels.UpdatedM) });
        }

        #endregion

        #region Update Airfare
        /// <summary>Shows the update main information modal.</summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowUpdateAirfareModal(Guid? uid)
        {
            UpdateLogisticAirfare cmd;

            try
            {
                var entity = await this.logisticsAirfareRepo.GetAsync(uid ?? Guid.Empty);
                if (entity == null)
                {
                    throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Request, Labels.FoundF.ToLowerInvariant()));
                }

                cmd = new UpdateLogisticAirfare(entity.Uid, entity.From, entity.To, entity.IsNational, entity.DepartureDate, entity.ArrivalDate, entity.TicketNumber, entity.AdditionalInfo);
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
                    new { page = this.RenderRazorViewToString("Modals/UpdateAirfareModal", cmd), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Updates the specified tiny collaborator.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        /// <exception cref="DomainException"></exception>
        /// <exception cref="DomainException"></exception>
        [HttpPost]
        public async Task<ActionResult> UpdateAirfare(UpdateLogisticAirfare cmd)
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
                    pages = new List<dynamic>
                    {
                        new { page = this.RenderRazorViewToString("Modals/_CreateForm", cmd), divIdOrClass = "#form-container" },
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Speaker, Labels.UpdatedM) });
        }
        #endregion
        
        #region Update Accommodation
        /// <summary>Shows the update main information modal.</summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowUpdateAccommodationModal(Guid? uid)
        {
            UpdateLogisticAccomodation cmd;

            try
            {
                var entity = await this.logisticsAccommodationRepo.GetAsync(uid ?? Guid.Empty);
                if (entity == null)
                {
                    throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Request, Labels.FoundF.ToLowerInvariant()));
                }

                cmd = new UpdateLogisticAccomodation(entity.Uid, entity.CheckInDate, entity.CheckOutDate, entity.AdditionalInfo, entity.AttendeePlaceId, await attendeePlacesRepo.FindAllDtosByEdition(EditionDto.Id));
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
                    new { page = this.RenderRazorViewToString("Modals/UpdateAccommodationModal", cmd), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Updates the specified tiny collaborator.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        /// <exception cref="DomainException"></exception>
        /// <exception cref="DomainException"></exception>
        [HttpPost]
        public async Task<ActionResult> UpdateAccommodation(UpdateLogisticAccomodation cmd)
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
                    pages = new List<dynamic>
                    {
                        new { page = this.RenderRazorViewToString("Modals/_CreateForm", cmd), divIdOrClass = "#form-container" },
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Speaker, Labels.UpdatedM) });
        }
        #endregion

        #region Update Transfer
        /// <summary>Shows the update main information modal.</summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowUpdateTransferModal(Guid? uid)
        {
            UpdateLogisticTransfer cmd;

            try
            {
                var entity = await this.logisticsTransferRepo.GetAsync(uid ?? Guid.Empty);
                if (entity == null)
                {
                    throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Request, Labels.FoundF.ToLowerInvariant()));
                }

                cmd = new UpdateLogisticTransfer(entity.Uid, entity.FromAttendeePlaceId, entity.ToAttendeePlaceId, entity.Date, entity.AdditionalInfo, await attendeePlacesRepo.FindAllDtosByEdition(EditionDto.Id));
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
                    new { page = this.RenderRazorViewToString("Modals/UpdateTransferModal", cmd), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Updates the specified tiny collaborator.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        /// <exception cref="DomainException"></exception>
        /// <exception cref="DomainException"></exception>
        [HttpPost]
        public async Task<ActionResult> UpdateTransfer(UpdateLogisticTransfer cmd)
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
                    pages = new List<dynamic>
                    {
                        new { page = this.RenderRazorViewToString("Modals/_CreateForm", cmd), divIdOrClass = "#form-container" },
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Speaker, Labels.UpdatedM) });
        }
        #endregion
        #region Details

        /// <summary>
        /// Detailses the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        [HttpGet]
        public async Task<ActionResult> Details(Guid? id)
        {
            var logisticsRequestDto = await this.logisticsRepo.GetDto(id ?? Guid.Empty, this.languageRepo.Get(f => f.Code == UserInterfaceLanguage));
            if (logisticsRequestDto == null)
            {
                this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Logistics, Labels.FoundF.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "Logistics", new { Area = "" });
            }
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.Logistics, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Requests, Url.Action("Index", "Logistics", new { id })),
                new BreadcrumbItemHelper("", Url.Action("Details", "Logistics", new { id }))
            });

            #endregion

            return View(logisticsRequestDto);
        }

        #endregion

        #region Create Airfare

        /// <summary>
        /// Shows the create modal.
        /// </summary>
        /// <param name="logisticsUid">The logistics uid.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        [HttpGet]
        public async Task<ActionResult> ShowCreateAirfareModal(Guid? logisticsUid)
        {
            var cmd = new CreateLogisticAirfare(logisticsUid ?? Guid.Empty);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Modals/CreateAirfareModal", cmd), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Creates the specified collaborator.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        /// <exception cref="DomainException"></exception>
        /// <exception cref="DomainException"></exception>
        [HttpPost]
        public async Task<ActionResult> CreateAirfare(CreateLogisticAirfare cmd)
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
                    pages = new List<dynamic>
                    {
                        new { page = this.RenderRazorViewToString("Modals/_TinyForm", cmd), divIdOrClass = "#form-container" },
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Speaker, Labels.CreatedM) });
        }

        #endregion

        #region Create Accommodation

        /// <summary>
        /// Shows the create modal.
        /// </summary>
        /// <param name="logisticsUid">The logistics uid.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        [HttpGet]
        public async Task<ActionResult> ShowCreateAccommodationModal(Guid? logisticsUid)
        {
            var cmd = new CreateLogisticAccomodation(logisticsUid ?? Guid.Empty, await attendeePlacesRepo.FindAllDtosByEdition(EditionDto.Id));

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Modals/CreateAccommodationModal", cmd), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Creates the specified collaborator.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        /// <exception cref="DomainException"></exception>
        /// <exception cref="DomainException"></exception>
        [HttpPost]
        public async Task<ActionResult> CreateAccommodation(CreateLogisticAccomodation cmd)
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

                cmd.UpdateLists(await attendeePlacesRepo.FindAllDtosByEdition(EditionDto.Id));

                return Json(new
                {
                    status = "error",
                    message = ex.GetInnerMessage(),
                    pages = new List<dynamic>
                    {
                        new { page = this.RenderRazorViewToString("Modals/_AccommodationForm", cmd), divIdOrClass = "#form-container" },
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Accommodation, Labels.CreatedM) });
        }

        #endregion

        #region Create Transport

        /// <summary>
        /// Shows the create modal.
        /// </summary>
        /// <param name="logisticsUid">The logistics uid.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        [HttpGet]
        public async Task<ActionResult> ShowCreateTransferModal(Guid? logisticsUid)
        {
            var cmd = new CreateLogisticTransfer(logisticsUid ?? Guid.Empty, await attendeePlacesRepo.FindAllDtosByEdition(EditionDto.Id));

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Modals/CreateTransferModal", cmd), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Creates the specified collaborator.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        /// <exception cref="DomainException"></exception>
        /// <exception cref="DomainException"></exception>
        [HttpPost]
        public async Task<ActionResult> CreateTransfer(CreateLogisticTransfer cmd)
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

                cmd.UpdateLists(await attendeePlacesRepo.FindAllDtosByEdition(EditionDto.Id));
                return Json(new
                {
                    status = "error",
                    message = ex.GetInnerMessage(),
                    pages = new List<dynamic>
                    {
                        new { page = this.RenderRazorViewToString("Modals/_TransferForm", cmd), divIdOrClass = "#form-container" },
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                cmd.UpdateLists(await attendeePlacesRepo.FindAllDtosByEdition(EditionDto.Id));
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Speaker, Labels.CreatedM) });
        }

        #endregion

        #region Delete

        /// <summary>
        /// Deletes the specified collaborator.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        /// <exception cref="DomainException"></exception>
        /// <exception cref="DomainException"></exception>
        [HttpPost]
        public async Task<ActionResult> Delete(DeleteLogisticRequest cmd)
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

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Request, Labels.DeletedF) });
        }

        /// <summary>
        /// Deletes the specified collaborator.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        /// <exception cref="DomainException"></exception>
        /// <exception cref="DomainException"></exception>
        [HttpPost]
        public async Task<ActionResult> DeleteLogisticAirfare(DeleteLogisticAirfare cmd)
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

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Speaker, Labels.DeletedM) });
        }


        /// <summary>
        /// Deletes the specified collaborator.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        /// <exception cref="DomainException"></exception>
        /// <exception cref="DomainException"></exception>
        [HttpPost]
        public async Task<ActionResult> DeleteLogisticAccommodation(DeleteLogisticAccommodation cmd)
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

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Speaker, Labels.DeletedM) });
        }


        /// <summary>
        /// Deletes the specified collaborator.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        /// <exception cref="DomainException"></exception>
        /// <exception cref="DomainException"></exception>
        [HttpPost]
        public async Task<ActionResult> DeleteLogisticTransfer(DeleteLogisticTransfer cmd)
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

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Speaker, Labels.DeletedM) });
        }

        #endregion

        #region Airfare Widget

        /// <summary>
        /// Shows the released projects widget.
        /// </summary>
        /// <param name="logisticsUid">The logistics uid.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        [HttpGet]
        public async Task<ActionResult> ShowAirfareWidget(Guid? logisticsUid)
        {
            var logisticsRequestDto = await this.logisticsAirfareRepo.FindAllDtosPaged(logisticsUid ?? Guid.Empty);
            if (logisticsRequestDto == null)
            {
                this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Logistics, Labels.FoundF.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "Logistics", new { Area = "" });
            }
            
            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/LogisticsAirfareWidget", logisticsRequestDto), divIdOrClass = "#LogisticsAirfareWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Accommodation Widget

        /// <summary>
        /// Shows the released projects widget.
        /// </summary>
        /// <param name="logisticsUid">The logistics uid.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        [HttpGet]
        public async Task<ActionResult> ShowAccommodationWidget(Guid? logisticsUid)
        {
            var logisticsRequestDto = await this.logisticsAccommodationRepo.FindAllDtosPaged(logisticsUid ?? Guid.Empty);
            if (logisticsRequestDto == null)
            {
                this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Logistics, Labels.FoundF.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "Logistics", new { Area = "" });
            }
            
            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/LogisticsAccommodationWidget", logisticsRequestDto), divIdOrClass = "#LogisticsAccommodationWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Transport Widget

        /// <summary>
        /// Shows the released projects widget.
        /// </summary>
        /// <param name="logisticsUid">The logistics uid.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        [HttpGet]
        public async Task<ActionResult> ShowTransferWidget(Guid? logisticsUid)
        {
            var logisticsRequestDto = await this.logisticsTransferRepo.FindAllDtosPaged(logisticsUid ?? Guid.Empty);
            if (logisticsRequestDto == null)
            {
                this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Logistics, Labels.FoundF.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "Logistics", new { Area = "" });
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/LogisticsTransferWidget", logisticsRequestDto), divIdOrClass = "#LogisticsTransferWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Arthur Souza
// Created          : 01-20-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-11-2020
// ***********************************************************************
// <copyright file="LogisticsController.cs" company="Softo">
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
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;
using PlataformaRio2C.Web.Admin.Filters;
using Constants = PlataformaRio2C.Domain.Constants;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    /// <summary>
    /// LogisticsController
    /// </summary>
    [AjaxAuthorize(Order = 1, Roles = Constants.Role.AnyAdmin)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.AdminAudiovisual + "," + Constants.CollaboratorType.AdminLogistic + "," + Constants.CollaboratorType.CuratorshipAudiovisual)]
    public class LogisticsController : BaseController
    {
        private readonly ILogisticSponsorRepository logisticSponsorRepo;
        private IAttendeeLogisticSponsorRepository attendeeLogisticSponsorRepo;
        private readonly IAttendeeCollaboratorRepository attendeCollaboratorRepo;
        private readonly ILogisticRepository logisticRepo;
        private readonly ILogisticAirfareRepository logisticsAirfareRepo;
        private readonly ILogisticAccommodationRepository logisticsAccommodationRepo;
        private readonly ILogisticTransferRepository logisticsTransferRepo;
        private readonly IAttendeePlacesRepository attendeePlacesRepo;
        private readonly ILanguageRepository languageRepo;
        private readonly ICollaboratorRepository collaboratorRepo;

        /// <summary>Initializes a new instance of the <see cref="LogisticsController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="logisticSponsorRepository">The logistic sponsor repository.</param>
        /// <param name="collaboratorRepository">The collaborator repository.</param>
        /// <param name="logisticRepository">The logistic repository.</param>
        /// <param name="logisticsAccommodationRepository">The logistics accommodation repository.</param>
        /// <param name="logisticsAirfareRepository">The logistics airfare repository.</param>
        /// <param name="attendeePlacesRepository">The attendee places repository.</param>
        /// <param name="attendeeLogisticSponsorRepository">The attendee logistic sponsor repository.</param>
        /// <param name="logisticsTransferRepository">The logistics transfer repository.</param>
        /// <param name="attendeCollaboratorRepository">The attende collaborator repository.</param>
        /// <param name="languageRepository">The language repository.</param>
        public LogisticsController(
            IMediator commandBus,
            IdentityAutenticationService identityController,
            ILogisticSponsorRepository logisticSponsorRepository,
            ICollaboratorRepository collaboratorRepository,
            ILogisticRepository logisticRepository,
            ILogisticAccommodationRepository logisticsAccommodationRepository,
            ILogisticAirfareRepository logisticsAirfareRepository,
            IAttendeePlacesRepository attendeePlacesRepository,
            IAttendeeLogisticSponsorRepository attendeeLogisticSponsorRepository,
            ILogisticTransferRepository logisticsTransferRepository,
            IAttendeeCollaboratorRepository attendeCollaboratorRepository,
            ILanguageRepository languageRepository)
            : base(commandBus, identityController)
        {
            this.logisticSponsorRepo = logisticSponsorRepository;
            this.logisticRepo = logisticRepository;
            this.languageRepo = languageRepository;
            this.attendeePlacesRepo = attendeePlacesRepository;
            this.logisticsTransferRepo = logisticsTransferRepository;
            this.logisticsAirfareRepo = logisticsAirfareRepository;
            this.collaboratorRepo = collaboratorRepository;
            this.attendeCollaboratorRepo = attendeCollaboratorRepository;
            this.logisticsAccommodationRepo = logisticsAccommodationRepository;
            this.attendeeLogisticSponsorRepo = attendeeLogisticSponsorRepository;
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

        /// <summary>Searches the specified request.</summary>
        /// <param name="request">The request.</param>
        /// <param name="showAllParticipants">if set to <c>true</c> [show all participants].</param>
        /// <param name="showAllSponsors">if set to <c>true</c> [show all sponsors].</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Search(IDataTablesRequest request, bool showAllParticipants, bool showAllSponsors)
        {
            var logistics = await this.collaboratorRepo.FindAllLogisticsByDatatable(
                EditionDto.Id,
                request.Start / request.Length,
                request.Length,
                request.Search?.Value,
                request.GetSortColumns(),
                showAllParticipants,
                showAllSponsors);

            foreach (var logisticRequestBaseDto in logistics)
            {
                logisticRequestBaseDto.AccommodationSponsor = logisticRequestBaseDto.AccommodationSponsor.GetSeparatorTranslation(this.UserInterfaceLanguage, '|');
                logisticRequestBaseDto.AirportTransferSponsor = logisticRequestBaseDto.AirportTransferSponsor.GetSeparatorTranslation(this.UserInterfaceLanguage, '|');
                logisticRequestBaseDto.AirfareSponsor = logisticRequestBaseDto.AirfareSponsor.GetSeparatorTranslation(this.UserInterfaceLanguage, '|');
            }

            var response = DataTablesResponse.Create(request, logistics.TotalItemCount, logistics.TotalItemCount, logistics);

            return Json(new
            {
                status = "success",
                dataTable = response
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion

        #region Create

        /// <summary>Shows the create modal.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowCreateModal()
        {
            var cmd = new CreateLogisticRequest(
                await attendeeLogisticSponsorRepo.FindAllDtosByIsOther(this.EditionDto.Id, false),
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

        /// <summary>Creates the specified logistic request.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
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
                    message = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError")?.Message ?? ex.GetInnerMessage(),
                    pages = new List<dynamic>
                    {
                        new { page = this.RenderRazorViewToString("Modals/_CreateForm", cmd), divIdOrClass = "#form-container"}
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Request, Labels.CreatedF) });
        }

        #endregion

        #region Update

        /// <summary>Shows the update main information modal.</summary>
        /// <param name="logisticsUid">The logistics uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowUpdateMainInformationModal(Guid? logisticsUid)
        {
            UpdateLogisticRequest cmd;

            try
            {
                var entity = await this.logisticRepo.GetAsync(logisticsUid ?? Guid.Empty);
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

        /// <summary>Updates the logistic request.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
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
                    message = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError")?.Message ?? ex.GetInnerMessage(),
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

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Request, Labels.UpdatedF) });
        }

        #endregion

        #region Details

        /// <summary>Detailses the specified identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Details(Guid? id)
        {
            var logisticsRequestDto = await this.logisticRepo.FindDtoAsync(id ?? Guid.Empty, this.languageRepo.Get(f => f.Code == UserInterfaceLanguage));
            if (logisticsRequestDto == null)
            {
                this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Logistics, Labels.FoundF.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "Logistics", new { Area = "" });
            }

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.Logistics, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Requests, Url.Action("Index", "Logistics", new { Area ="", id })),
                new BreadcrumbItemHelper(logisticsRequestDto?.Name, Url.Action("Details", "Logistics", new { Area ="", id }))
            });

            #endregion

            return View(logisticsRequestDto);
        }

        #region Airfare Widget

        /// <summary>Shows the airfare widget.</summary>
        /// <param name="logisticsUid">The logistics uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowAirfareWidget(Guid? logisticsUid)
        {
            var logisticAirfareJsonDtos = await this.logisticsAirfareRepo.FindAllJsonDtosAsync(logisticsUid ?? Guid.Empty);
            if (logisticAirfareJsonDtos == null)
            {
                this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Logistics, Labels.FoundF.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "Logistics", new { Area = "" });
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/LogisticsAirfareWidget", logisticAirfareJsonDtos), divIdOrClass = "#LogisticsAirfareWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #region Create

        /// <summary>Shows the create airfare modal.</summary>
        /// <param name="logisticsUid">The logistics uid.</param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.AdminAudiovisual + "," + Constants.CollaboratorType.AdminLogistic)]
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

        /// <summary>Creates the airfare.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.AdminAudiovisual + "," + Constants.CollaboratorType.AdminLogistic)]
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
                    message = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError")?.Message ?? ex.GetInnerMessage(),
                    pages = new List<dynamic>
                    {
                        new { page = this.RenderRazorViewToString("Modals/_AirfareForm", cmd), divIdOrClass = "#form-container" },
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Airfare, Labels.CreatedM) });
        }

        #endregion

        #region Update

        /// <summary>Shows the update airfare modal.</summary>
        /// <param name="uid">The uid.</param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.AdminAudiovisual + "," + Constants.CollaboratorType.AdminLogistic)]
        public async Task<ActionResult> ShowUpdateAirfareModal(Guid? uid)
        {
            UpdateLogisticAirfare cmd;

            try
            {
                var logisticAirfareDto = await this.logisticsAirfareRepo.FindDtoAsync(uid ?? Guid.Empty);
                if (logisticAirfareDto == null)
                {
                    throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Request, Labels.FoundF.ToLowerInvariant()));
                }

                cmd = new UpdateLogisticAirfare(logisticAirfareDto);
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

        /// <summary>Updates the airfare.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.AdminAudiovisual + "," + Constants.CollaboratorType.AdminLogistic)]
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
                    message = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError")?.Message ?? ex.GetInnerMessage(),
                    pages = new List<dynamic>
                    {
                        new { page = this.RenderRazorViewToString("Modals/_AirfareForm", cmd), divIdOrClass = "#form-container" },
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Airfare, Labels.UpdatedM) });
        }

        #endregion

        #region Delete

        /// <summary>Deletes the logistic airfare.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.AdminAudiovisual + "," + Constants.CollaboratorType.AdminLogistic)]
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
                    message = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError")?.Message ?? ex.GetInnerMessage(),
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Airfare, Labels.DeletedM) });
        }

        #endregion

        #endregion

        #region Accommodation Widget

        /// <summary>Shows the accommodation widget.</summary>
        /// <param name="logisticsUid">The logistics uid.</param>
        /// <returns></returns>
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

        #region Create

        /// <summary>Shows the create accommodation modal.</summary>
        /// <param name="logisticsUid">The logistics uid.</param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.AdminAudiovisual + "," + Constants.CollaboratorType.AdminLogistic)]
        public async Task<ActionResult> ShowCreateAccommodationModal(Guid? logisticsUid)
        {
            var cmd = new CreateLogisticAccommodation(
                logisticsUid ?? Guid.Empty,
                await this.attendeePlacesRepo.FindAllDropdownDtosAsync(EditionDto.Id, true));

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Modals/CreateAccommodationModal", cmd), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Creates the accommodation.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.AdminAudiovisual + "," + Constants.CollaboratorType.AdminLogistic)]
        public async Task<ActionResult> CreateAccommodation(CreateLogisticAccommodation cmd)
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

                cmd.UpdateLists(
                    await attendeePlacesRepo.FindAllDropdownDtosAsync(EditionDto.Id, true));

                return Json(new
                {
                    status = "error",
                    message = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError")?.Message ?? ex.GetInnerMessage(),
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

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Accommodation, Labels.CreatedF) });
        }

        #endregion

        #region Update

        /// <summary>Shows the update accommodation modal.</summary>
        /// <param name="uid">The uid.</param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.AdminAudiovisual + "," + Constants.CollaboratorType.AdminLogistic)]
        public async Task<ActionResult> ShowUpdateAccommodationModal(Guid? uid)
        {
            UpdateLogisticAccommodation cmd;

            try
            {
                var entity = await this.logisticsAccommodationRepo.GetAsync(uid ?? Guid.Empty);
                if (entity == null)
                {
                    throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Request, Labels.FoundF.ToLowerInvariant()));
                }

                cmd = new UpdateLogisticAccommodation(
                    entity.Uid,
                    entity.CheckInDate,
                    entity.CheckOutDate,
                    entity.AdditionalInfo,
                    entity.AttendeePlaceId,
                    await attendeePlacesRepo.FindAllDropdownDtosAsync(EditionDto.Id, true));
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

        /// <summary>Updates the accommodation.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.AdminAudiovisual + "," + Constants.CollaboratorType.AdminLogistic)]
        public async Task<ActionResult> UpdateAccommodation(UpdateLogisticAccommodation cmd)
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

                cmd.UpdateLists(
                    await attendeePlacesRepo.FindAllDropdownDtosAsync(EditionDto.Id, true));

                return Json(new
                {
                    status = "error",
                    message = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError")?.Message ?? ex.GetInnerMessage(),
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

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Accommodation, Labels.UpdatedF) });
        }

        #endregion

        #region Delete

        /// <summary>Deletes the logistic accommodation.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.AdminAudiovisual + "," + Constants.CollaboratorType.AdminLogistic)]
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
                    message = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError")?.Message ?? ex.GetInnerMessage(),
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Accommodation, Labels.DeletedF) });
        }

        #endregion

        #endregion

        #region Transfer Widget

        /// <summary>Shows the transfer widget.</summary>
        /// <param name="logisticsUid">The logistics uid.</param>
        /// <returns></returns>
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

        #region Create

        /// <summary>Shows the create transfer modal.</summary>
        /// <param name="logisticsUid">The logistics uid.</param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.AdminAudiovisual + "," + Constants.CollaboratorType.AdminLogistic)]
        public async Task<ActionResult> ShowCreateTransferModal(Guid? logisticsUid)
        {
            var cmd = new CreateLogisticTransfer(
                logisticsUid ?? Guid.Empty,
                await attendeePlacesRepo.FindAllDropdownDtosAsync(EditionDto.Id));

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Modals/CreateTransferModal", cmd), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Creates the transfer.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.AdminAudiovisual + "," + Constants.CollaboratorType.AdminLogistic)]
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

                cmd.UpdateLists(
                    await attendeePlacesRepo.FindAllDropdownDtosAsync(EditionDto.Id));

                return Json(new
                {
                    status = "error",
                    message = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError")?.Message ?? ex.GetInnerMessage(),
                    pages = new List<dynamic>
                    {
                        new { page = this.RenderRazorViewToString("Modals/_TransferForm", cmd), divIdOrClass = "#form-container" },
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                cmd.UpdateLists(await attendeePlacesRepo.FindAllDropdownDtosAsync(EditionDto.Id));
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Transfer, Labels.CreatedM) });
        }

        #endregion

        #region Update

        /// <summary>Shows the update transfer modal.</summary>
        /// <param name="uid">The uid.</param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.AdminAudiovisual + "," + Constants.CollaboratorType.AdminLogistic)]
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

                cmd = new UpdateLogisticTransfer(entity.Uid, entity.FromAttendeePlaceId, entity.ToAttendeePlaceId, entity.Date, entity.AdditionalInfo, await attendeePlacesRepo.FindAllDropdownDtosAsync(EditionDto.Id));
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

        /// <summary>Updates the transfer.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.AdminAudiovisual + "," + Constants.CollaboratorType.AdminLogistic)]
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
                    message = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError")?.Message ?? ex.GetInnerMessage(),
                    pages = new List<dynamic>
                    {
                        new { page = this.RenderRazorViewToString("Modals/_TransferForm", cmd), divIdOrClass = "#form-container" },
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Transfer, Labels.UpdatedM) });
        }

        #endregion

        #region Delete

        /// <summary>Deletes the logistic transfer.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.AdminAudiovisual + "," + Constants.CollaboratorType.AdminLogistic)]
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
                    message = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError")?.Message ?? ex.GetInnerMessage(),
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Transfer, Labels.DeletedM) });
        }

        #endregion

        #endregion

        #endregion

        #region Delete

        /// <summary>Deletes the specified delete logistic request.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
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
                    message = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError")?.Message ?? ex.GetInnerMessage(),
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Request, Labels.DeletedF) });
        }

        #endregion
    }
}
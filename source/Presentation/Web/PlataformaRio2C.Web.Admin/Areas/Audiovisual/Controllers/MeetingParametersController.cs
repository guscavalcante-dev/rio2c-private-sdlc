// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 03-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-25-2020
// ***********************************************************************
// <copyright file="MeetingParametersController.cs" company="Softo">
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
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;
using PlataformaRio2C.Web.Admin.Controllers;
using PlataformaRio2C.Web.Admin.Filters;
using Constants = PlataformaRio2C.Domain.Constants;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Application.Interfaces.Common;
using DocumentFormat.OpenXml.Office.Word;

namespace PlataformaRio2C.Web.Admin.Areas.Audiovisual.Controllers
{
    /// <summary>MeetingParametersController</summary>
    [AjaxAuthorize(Order = 1, Roles = Constants.Role.AnyAdmin)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.AdminAudiovisual)]
    public class MeetingParametersController : BaseController
    {
        private readonly INegotiationConfigRepository negotiationConfigRepo;
        private readonly INegotiationRoomConfigRepository negotiationRoomConfigRepo;
        private readonly IRoomRepository roomRepo;
        private readonly IOrganizationRepository organizationRepo;
        private readonly INegotiationValidationService negotiationValidationService;
        private readonly IProjectRepository projectRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="MeetingParametersController" /> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="negotiationConfigRepository">The negotiation configuration repository.</param>
        /// <param name="negotiationRoomConfigRepository">The negotiation room configuration repository.</param>
        /// <param name="roomRepository">The room repository.</param>
        /// <param name="organizationRepository">The attendee organization repository.</param>
        public MeetingParametersController(
            IMediator commandBus, 
            IdentityAutenticationService identityController,
            INegotiationConfigRepository negotiationConfigRepository,
            IProjectRepository projectRepository,
            INegotiationRoomConfigRepository negotiationRoomConfigRepository,
            IRoomRepository roomRepository,
            INegotiationValidationService negotiationValidationService,
            IOrganizationRepository organizationRepository)
            : base(commandBus, identityController)
        {
            this.projectRepo = projectRepository;
            this.negotiationValidationService = negotiationValidationService;
            this.negotiationConfigRepo = negotiationConfigRepository;
            this.negotiationRoomConfigRepo = negotiationRoomConfigRepository;
            this.roomRepo = roomRepository;
            this.organizationRepo = organizationRepository;
        }

        #region List

        /// <summary>Indexes the specified search view model.</summary>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(MeetingParameterSearchViewModel searchViewModel)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.OneToOneMeetings, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Parameters, Url.Action("Index", "MeetingParameters", new { Area = "Audiovisual" }))
            });

            #endregion

            return View(searchViewModel);
        }

        #region DataTable Widget

        /// <summary>Searches the specified request.</summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Search(IDataTablesRequest request)
        {
            var negotiationConfigs = await this.negotiationConfigRepo.FindAllJsonDtosPagedAsync(
                request.Start / request.Length,
                request.Length,
                request.GetSortColumns(),
                request.Search?.Value,
                null,
                null,
                this.UserInterfaceLanguage,
                this.EditionDto.Id,
                ProjectType.AudiovisualBusinessRound.Id
            );

            var response = DataTablesResponse.Create(request, negotiationConfigs.TotalItemCount, negotiationConfigs.TotalItemCount, negotiationConfigs);

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
            var executivesCount = await this.negotiationConfigRepo.CountAsync(this.EditionDto.Id,ProjectType.AudiovisualBusinessRound.Id, true);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/TotalCountWidget", executivesCount), divIdOrClass = "#AudiovisualMeetingParametersTotalCountWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Edition Count Widget

        /// <summary>Shows the edition count widget.</summary>
        /// <returns></returns>
        public async Task<ActionResult> ShowEditionCountWidget()
        {
            var executivesCount = await this.negotiationConfigRepo.CountAsync(this.EditionDto.Id, ProjectType.AudiovisualBusinessRound.Id, false);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/EditionCountWidget", executivesCount), divIdOrClass = "#AudiovisualMeetingParametersEditionCountWidget" },
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
            var cmd = new CreateNegotiationConfig();

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Modals/CreateModal", cmd), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Creates the specified negotiation configuration.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Create(CreateNegotiationConfig cmd)
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
                cmd.ProjectTypeId = ProjectType.AudiovisualBusinessRound.Id;
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

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Parameter, Labels.CreatedM) });
        }

        #endregion

        #region Details

        /// <summary>Detailses the specified identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Details(Guid? id)
        {
            var mainInformationWidgetDto = await this.negotiationConfigRepo.FindMainInformationWidgetDtoAsync(id ?? Guid.Empty, ProjectType.AudiovisualBusinessRound.Id);
            if (mainInformationWidgetDto == null)
            {
                this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Parameter, Labels.FoundM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "Commissions", new { Area = "Music" });
            }

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.OneToOneMeetings, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Parameters, Url.Action("Index", "MeetingParameters", new { Area = "Audiovisual" })),
                new BreadcrumbItemHelper(Labels.Parameter, Url.Action("Details", "MeetingParameters", new { Area = "Audiovisual", id }))
            });

            #endregion

            return View(mainInformationWidgetDto);
        }

        #region Main Information Widget

        /// <summary>Shows the main information widget.</summary>
        /// <param name="negotiationConfigUid">The negotiation configuration uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowMainInformationWidget(Guid? negotiationConfigUid)
        {
            var mainInformationWidgetDto = await this.negotiationConfigRepo.FindMainInformationWidgetDtoAsync(negotiationConfigUid ?? Guid.Empty, ProjectType.AudiovisualBusinessRound.Id);
            if (mainInformationWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Parameter, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/MainInformationWidget", mainInformationWidgetDto), divIdOrClass = "#AudiovisualMeetingParametersMainInformationWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #region Update

        /// <summary>Shows the update main information modal.</summary>
        /// <param name="negotiationConfigUid">The negotiation configuration uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowUpdateMainInformationModal(Guid? negotiationConfigUid)
        {
            UpdateNegotiationConfigMainInformation cmd;

            try
            {
                var mainInformationWidgetDto = await this.negotiationConfigRepo.FindMainInformationWidgetDtoAsync(negotiationConfigUid ?? Guid.Empty, ProjectType.AudiovisualBusinessRound.Id);
                if (mainInformationWidgetDto == null)
                {
                    throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Parameter, Labels.FoundM.ToLowerInvariant()));
                }

                cmd = new UpdateNegotiationConfigMainInformation(mainInformationWidgetDto);
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
        public async Task<ActionResult> UpdateMainInformation(UpdateNegotiationConfigMainInformation cmd)
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
                        new { page = this.RenderRazorViewToString("Modals/MainInformationForm.cshtml", cmd), divIdOrClass = "#form-container" },
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Parameter, Labels.UpdatedM) });
        }

        #endregion

        #endregion

        #region Rooms Widget

        /// <summary>Shows the rooms widget.</summary>
        /// <param name="negotiationConfigUid">The negotiation configuration uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowRoomsWidget(Guid? negotiationConfigUid)
        {
            var roomsWidgetDto = await this.negotiationConfigRepo.FindRoomsWidgetDtoAsync(negotiationConfigUid ?? Guid.Empty, ProjectType.AudiovisualBusinessRound.Id);
            if (roomsWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Parameter, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/RoomsWidget", roomsWidgetDto), divIdOrClass = "#AudiovisualMeetingParametersRoomsWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #region Create

        /// <summary>Shows the create room modal.</summary>
        /// <param name="negotiationConfigUid">The negotiation configuration uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowCreateRoomModal(Guid? negotiationConfigUid)
        {
            CreateNegotiationRoomConfig cmd;

            try
            {
                var roomsWidgetDto = await this.negotiationConfigRepo.FindRoomsWidgetDtoAsync(negotiationConfigUid ?? Guid.Empty, ProjectType.AudiovisualBusinessRound.Id);
                if (roomsWidgetDto == null)
                {
                    throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Parameter, Labels.FoundM.ToLowerInvariant()));
                }

                cmd = new CreateNegotiationRoomConfig(
                    roomsWidgetDto,
                    await this.roomRepo.FindAllDtoByEditionIdAsync(this.EditionDto.Id),
                    this.UserInterfaceLanguage);
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
                    new { page = this.RenderRazorViewToString("Modals/CreateRoomModal", cmd), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Creates the room.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CreateRoom(CreateNegotiationRoomConfig cmd)
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
                cmd.ProjectTypeId = ProjectType.AudiovisualBusinessRound.Id;
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

                cmd.UpdateModelsAndLists(
                    await this.roomRepo.FindAllDtoByEditionIdAsync(this.EditionDto.Id),
                    this.UserInterfaceLanguage);

                return Json(new
                {
                    status = "error",
                    message = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError")?.Message ?? ex.GetInnerMessage(),
                    pages = new List<dynamic>
                    {
                        new { page = this.RenderRazorViewToString("Modals/CreateRoomForm", cmd), divIdOrClass = "#form-container" },
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Room, Labels.CreatedM) });
        }

        #endregion

        #region Update

        /// <summary>Shows the update room modal.</summary>
        /// <param name="negotiationRoomConfigUid">The negotiation room configuration uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowUpdateRoomModal(Guid? negotiationRoomConfigUid)
        {
            UpdateNegotiationRoomConfig cmd;

            try
            {
                var roomWidgetDto = await this.negotiationRoomConfigRepo.FindMainInformationWidgetDtoAsync(negotiationRoomConfigUid ?? Guid.Empty, ProjectType.AudiovisualBusinessRound.Id);
                if (roomWidgetDto == null)
                {
                    throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Room, Labels.FoundM.ToLowerInvariant()));
                }

                cmd = new UpdateNegotiationRoomConfig(
                    roomWidgetDto,
                    await this.roomRepo.FindAllDtoByEditionIdAsync(this.EditionDto.Id),
                    this.UserInterfaceLanguage);
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
                    new { page = this.RenderRazorViewToString("Modals/UpdateRoomModal", cmd), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Updates the room.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        /// <exception cref="DomainException"></exception>
        [HttpPost]
        public async Task<ActionResult> UpdateRoom(UpdateNegotiationRoomConfig cmd)
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

                cmd.UpdateModelsAndLists(
                    await this.roomRepo.FindAllDtoByEditionIdAsync(this.EditionDto.Id),
                    this.UserInterfaceLanguage);

                return Json(new
                {
                    status = "error",
                    message = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError")?.Message ?? ex.GetInnerMessage(),
                    pages = new List<dynamic>
                    {
                        new { page = this.RenderRazorViewToString("Modals/UpdateRoomForm", cmd), divIdOrClass = "#form-container" },
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Room, Labels.UpdatedM) });
        }

        #endregion

        #region Delete

        /// <summary>Deletes the room.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> DeleteRoom(DeleteNegotiationRoomConfig cmd)
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

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Room, Labels.DeletedM) });
        }

        #endregion

        #endregion

        #endregion

        #region Delete

        /// <summary>Deletes the specified negotiation configuration.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Delete(DeleteNegotiationConfig cmd)
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

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Parameter, Labels.DeletedM) });
        }

        #endregion

        #region Finds

        /// <summary>Finds all dates.</summary>
        /// <param name="customFilter">The custom filter.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> FindAllDates(Guid? buyerOrganizationUid = null, string customFilter = "")
        {
            var buyerOrganizationDto = await this.organizationRepo.FindDtoByUidAsync(buyerOrganizationUid ?? Guid.Empty, this.EditionDto.Edition.Id);
            var negotiationConfigDtos = await this.negotiationConfigRepo.FindAllDatesDtosAsync(this.EditionDto.Id, customFilter, buyerOrganizationDto?.IsVirtualMeeting == true, ProjectType.AudiovisualBusinessRound.Id);

            return Json(new
            {
                status = "success",
                negotiationConfigs = negotiationConfigDtos?.Select(ncd => new NegotiationConfigDropdownDto
                {
                    Uid = ncd.NegotiationConfig.Uid,
                    StartDate = ncd.NegotiationConfig.StartDate,
                    EndDate = ncd.NegotiationConfig.EndDate
                })
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Finds all dates.</summary>
        /// <param name="customFilter">The custom filter.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> FindAllDatesAvailables(Guid? buyerOrganizationUid = null, Guid? projectUid = null, string customFilter = "")
        {
            var buyerOrganizationDto = await this.organizationRepo.FindDtoByUidAsync(buyerOrganizationUid ?? Guid.Empty, this.EditionDto.Edition.Id);
            var negotiationConfigDtos = await this.negotiationConfigRepo.FindAllDatesDtosAsync(this.EditionDto.Id, customFilter, buyerOrganizationDto?.IsVirtualMeeting == true, ProjectType.AudiovisualBusinessRound.Id);
            var project = await this.projectRepo.GetAsync(projectUid ?? Guid.Empty);


            for (int i = negotiationConfigDtos.Count - 1; i >= 0; i--)
            {
                var negotiationConfig = negotiationConfigDtos[i];
                var dayStart = negotiationConfig.NegotiationConfig.StartDate.Date;
                var dayEnd = dayStart.AddDays(1).AddTicks(-1); // até 23:59:59.9999999

                var result = await this.negotiationValidationService.ValidateOverbookingDatesAsync(
                    this.EditionDto.Id,
                    dayStart,
                    dayEnd,
                    buyerOrganizationUid.Value,
                    project.SellerAttendeeOrganization.Uid);

                if (!result.IsValid)
                {
                    negotiationConfigDtos.RemoveAt(i);
                }
            }

            return Json(new
            {
                status = "success",
                negotiationConfigs = negotiationConfigDtos?.Select(ncd => new NegotiationConfigDropdownDto
                {
                    Uid = ncd.NegotiationConfig.Uid,
                    StartDate = ncd.NegotiationConfig.StartDate,
                    EndDate = ncd.NegotiationConfig.EndDate
                })
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Finds all rooms.</summary>
        /// <param name="negotiationConfigUid">The negotiation configuration uid.</param>
        /// <param name="customFilter">The custom filter.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> FindAllRooms(Guid? negotiationConfigUid = null, Guid? buyerOrganizationUid = null, string customFilter = "")
        {
            var buyerOrganizationDto = await this.organizationRepo.FindDtoByUidAsync(buyerOrganizationUid ?? Guid.Empty, this.EditionDto.Edition.Id);
            var negotiationConfigDtos = await this.negotiationConfigRepo.FindAllRoomsDtosAsync(this.EditionDto.Id, negotiationConfigUid ?? Guid.Empty, customFilter, buyerOrganizationDto.IsVirtualMeeting == true, ProjectType.AudiovisualBusinessRound.Id);

            return Json(new
            {
                status = "success",
                rooms = negotiationConfigDtos?.SelectMany(ncd => ncd.NegotiationRoomConfigDtos.Select(nrc => new NegotiationRoomConfigDropdownDto
                {
                    NegotiationRoomConfigUid = nrc.NegotiationRoomConfig.Uid,
                    RoomUid = nrc.RoomDto.Room.Uid,
                    RoomName = nrc.RoomDto.GetRoomNameByLanguageCode(this.UserInterfaceLanguage).RoomName.Value
                }))
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Finds all times.</summary>
        /// <param name="negotiationRoomConfigUid">The negotiation room configuration uid.</param>
        /// <param name="customFilter">The custom filter.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> FindAllTimes(Guid? negotiationRoomConfigUid = null, Guid? buyerOrganizationUid = null, string customFilter = "")
        {
            var buyerOrganizationDto = await this.organizationRepo.FindDtoByUidAsync(buyerOrganizationUid ?? Guid.Empty, this.EditionDto.Edition.Id);
            var negotiationConfigDto = await this.negotiationConfigRepo.FindAllTimesDtosAsync(this.EditionDto.Id, negotiationRoomConfigUid ?? Guid.Empty, customFilter, buyerOrganizationDto.IsVirtualMeeting == true, ProjectType.AudiovisualBusinessRound.Id);

            return Json(new
            {
                status = "success",
                times = this.GetNegotiationTimesSlots(negotiationConfigDto)
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Finds all times.</summary>
        /// <param name="negotiationRoomConfigUid">The negotiation room configuration uid.</param>
        /// <param name="customFilter">The custom filter.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> FindAllTimesAvailables(Guid? negotiationRoomConfigUid = null, Guid? projectUid = null, Guid? buyerOrganizationUid = null, string customFilter = "")
        {
            var buyerOrganizationDto = await this.organizationRepo.FindDtoByUidAsync(buyerOrganizationUid ?? Guid.Empty, this.EditionDto.Edition.Id);
            var negotiationConfigDto = await this.negotiationConfigRepo.FindAllTimesDtosAsync(this.EditionDto.Id, negotiationRoomConfigUid ?? Guid.Empty, customFilter, buyerOrganizationDto.IsVirtualMeeting == true, ProjectType.AudiovisualBusinessRound.Id);
            var project = await this.projectRepo.GetAsync(projectUid ?? Guid.Empty);

            var allSlots = this.GetNegotiationTimesSlots(negotiationConfigDto);
            var availableSlots = new List<NegotiationTimeDropdownDto>();

            foreach (var slot in allSlots)
            {
                var validation = await this.negotiationValidationService.ValidateOverbookingAsync(
                    editionId: this.EditionDto.Id,
                    startDate: slot.StartTime,
                    endDate: slot.EndTime,
                    buyerOrganizationId: buyerOrganizationUid ?? Guid.Empty,
                    sellerOrganizationId: project.SellerAttendeeOrganization.Organization.Uid
                );

                if (validation.IsValid)
                {
                    availableSlots.Add(slot);
                }
            }

            return Json(new
            {
                status = "success",
                times = availableSlots
            }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>Gets the negotiation times slots.</summary>
        /// <param name="negotiationConfigDto">The negotiation configuration dto.</param>
        /// <returns></returns>
        private List<NegotiationTimeDropdownDto> GetNegotiationTimesSlots(NegotiationConfigDto negotiationConfigDto)
        {
            var negotiationTimesSlots = new List<NegotiationTimeDropdownDto>();
            var roundNumber = 1;

            if (negotiationConfigDto == null)
            {
                return negotiationTimesSlots;
            }

            var startDate = negotiationConfigDto.NegotiationConfig.StartDate;

            #region Round first turn

            // Each slot
            for (int iSlot = 0; iSlot < negotiationConfigDto.NegotiationConfig.RoundFirstTurn; iSlot++)
            {
                if (startDate.Add(negotiationConfigDto.NegotiationConfig.TimeOfEachRound) <= negotiationConfigDto.NegotiationConfig.EndDate)
                {
                    // Each room
                    foreach (var negotiationRoomConfigDto in negotiationConfigDto.NegotiationRoomConfigDtos)
                    {
                        // Each automatic table
                        //for (int tableNumber = 1; tableNumber <= negotiationRoomConfigDto.NegotiationRoomConfig.CountManualTables; tableNumber++)
                        //{
                            negotiationTimesSlots.Add(new NegotiationTimeDropdownDto
                            {
                                StartTime = startDate,
                                EndTime = startDate.Add(negotiationConfigDto.NegotiationConfig.TimeOfEachRound),
                                RoundNumber = roundNumber
                            });
                        //}
                    }
                }

                startDate = startDate.Add(negotiationConfigDto.NegotiationConfig.TimeOfEachRound.Add(negotiationConfigDto.NegotiationConfig.TimeIntervalBetweenRound));
                roundNumber++;
            }

            #endregion

            #region Round second turn

            startDate = startDate.Add(negotiationConfigDto.NegotiationConfig.TimeIntervalBetweenTurn);

            // Each slot
            for (int iSlot = 0; iSlot < negotiationConfigDto.NegotiationConfig.RoundSecondTurn; iSlot++)
            {

                if (startDate.Add(negotiationConfigDto.NegotiationConfig.TimeOfEachRound) <= negotiationConfigDto.NegotiationConfig.EndDate)
                {
                    // Each room
                    foreach (var negotiationRoomConfigDto in negotiationConfigDto.NegotiationRoomConfigDtos)
                    {
                        // Each automatic table
                        //for (int tableNumber = 1; tableNumber <= negotiationRoomConfigDto.NegotiationRoomConfig.CountManualTables; tableNumber++)
                        //{
                            negotiationTimesSlots.Add(new NegotiationTimeDropdownDto
                            {
                                StartTime = startDate,
                                EndTime = startDate.Add(negotiationConfigDto.NegotiationConfig.TimeOfEachRound),
                                RoundNumber = roundNumber
                            });
                        //}
                    }
                }

                startDate = startDate.Add(negotiationConfigDto.NegotiationConfig.TimeOfEachRound.Add(negotiationConfigDto.NegotiationConfig.TimeIntervalBetweenRound));
                roundNumber++;
            }

            #endregion

            return negotiationTimesSlots;
        }

        #endregion
    }
}
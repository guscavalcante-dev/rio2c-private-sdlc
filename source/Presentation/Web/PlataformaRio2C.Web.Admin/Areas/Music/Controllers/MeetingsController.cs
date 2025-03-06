// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Ribeiro 
// Created          : 21-02-2025
//
// Last Modified By : Rafael Ribeiro 
// Last Modified On : 21-02-2025
// ***********************************************************************
// <copyright file="MeetingsController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Application;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Application.CQRS.Queries;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;
using PlataformaRio2C.Web.Admin.Controllers;
using PlataformaRio2C.Web.Admin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Constants = PlataformaRio2C.Domain.Constants;
using PlataformaRio2C.Application.TemplateDocuments;
using PlataformaRio2C.Infra.Report.Models;
using PlataformaRio2C.Application.CQRS.Queries;
using PlataformaRio2C.Application.CQRS.QueriesHandlers;
using DocumentFormat.OpenXml.Office.Word;
using System.Web.Http.Results;
using PlataformaRio2C.Domain.Interfaces.Repositories;

namespace PlataformaRio2C.Web.Admin.Areas.Music.Controllers
{
    /// <summary>MeetingsController</summary>
    [AjaxAuthorize(Order = 1, Roles = Constants.Role.AnyAdmin)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.AdminMusic)]
    public class MeetingsController : BaseController
    {
        private readonly IMusicBusinessRoundNegotiationRepository musicbusinessRoundnegotiationRepo;
        private readonly IProjectBuyerEvaluationRepository projectBuyerEvaluationRepo;
        private readonly IRoomRepository roomRepo;
        private readonly IAttendeeOrganizationRepository attendeeOrganizationRepo;
        private readonly IAttendeeCollaboratorRepository attendeeCollaboratorRepo;
        private readonly INegotiationConfigRepository negotiationConfigRepo;
        /// <summary>
        /// Initializes a new instance of the <see cref="MeetingsController" /> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="musicbusinessroundnegotiationRepository">The negotiation repository.</param>
        /// <param name="projectBuyerEvaluationRepository">The project buyer evaluation repository.</param>
        /// <param name="roomRepository">The room repository.</param>
        /// <param name="attendeeOrganizationRepository">The attendee organization repository.</param>
        /// <param name="attendeeCollaboratorRepository">The attendee collaborator repository.</param>
        /// <param name="negotiationConfigRepo">The negotiation configuration repo.</param>
        public MeetingsController(
            IMediator commandBus,
            IdentityAutenticationService identityController,
            IMusicBusinessRoundNegotiationRepository musicbusinessroundnegotiationRepository,
            IProjectBuyerEvaluationRepository projectBuyerEvaluationRepository,
            IRoomRepository roomRepository,
            IAttendeeOrganizationRepository attendeeOrganizationRepository,
            IAttendeeCollaboratorRepository attendeeCollaboratorRepository,
            INegotiationConfigRepository negotiationConfigRepo)
            : base(commandBus, identityController)
        {
            this.musicbusinessRoundnegotiationRepo = musicbusinessroundnegotiationRepository;
            this.projectBuyerEvaluationRepo = projectBuyerEvaluationRepository;
            this.roomRepo = roomRepository;
            this.attendeeOrganizationRepo = attendeeOrganizationRepository;
            this.attendeeCollaboratorRepo = attendeeCollaboratorRepository;
            this.negotiationConfigRepo = negotiationConfigRepo;
        }

        #region Generate Agenda

        /// <summary>Indexes the specified search view model.</summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.OneToOneMeetings, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.GenerateCalendar, Url.Action("Index", "Meetings", new { Area = "Music" }))
            });

            #endregion

            return View();
        }

        #region Status Widget

        

        /// <summary>Shows the status widget.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowStatusWidget()
        {
            var generateAgendaStatusWidgetDto = new GenerateAgendaStatusWidgetDto(
                await this.negotiationConfigRepo.CountNegotiationConfigsWithPresentialRoomConfiguredAsync(this.EditionDto.Id),
                await this.negotiationConfigRepo.CountNegotiationConfigsWithVirtualRoomConfiguredAsync(this.EditionDto.Id));

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/StatusWidget", generateAgendaStatusWidgetDto), divIdOrClass = "#MusicMeetingsStatusWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #region Generate

        /// <summary>Generates the specified command.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Generate(CreateNegotiations cmd)
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

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Agenda, Labels.GeneratedF.ToLowerInvariant()) });
        }

        #endregion

        #endregion

        #endregion

        #region Scheduled

        /// <summary>Scheduleds this instance.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Scheduled()
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.OneToOneMeetings, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.ScheduledNegotiations, Url.Action("Scheduled", "Meetings", new { Area = "Music" }))
            });

            #endregion

            ViewBag.Rooms = (await this.roomRepo.FindAllDtoByEditionIdAsync(this.EditionDto.Id))?.Select(r => new RoomJsonDto
            {
                Id = r.Room.Id,
                Uid = r.Room.Uid,
                Name = r.GetRoomNameByLanguageCode(this.UserInterfaceLanguage)?.RoomName?.Value
            })?.ToList();

            return View();
        }

        #region Edition Scheduled Count Widget

        /// <summary>Shows the edition scheduled count widget.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowEditionScheduledCountWidget()
        {
            var scheduledCount = await this.projectBuyerEvaluationRepo.CountNegotiationScheduledAsync(this.EditionDto.Id, false);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/EditionScheduledCountWidget", scheduledCount), divIdOrClass = "#MusicMeetingsEditionScheduledCountWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Edition Scheduled Count Gauge Widget

        /// <summary>
        /// Shows the edition scheduled count gauge widget.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowEditionScheduledCountGaugeWidget()
        {
            var scheduledCount = await this.projectBuyerEvaluationRepo.CountNegotiationScheduledAsync(this.EditionDto.Id, false);
            var maximumAvailableSlotsByEditionId = new GetMusicMaximumAvailableSlotsByEditionId(this.EditionDto.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/EditionScheduledCountGaugeWidget", scheduledCount), divIdOrClass = "#MusicMeetingsEditionScheduledCountGaugeWidget" },
                },
                chartData = scheduledCount,
                maximumAvailableSlots = await this.CommandBus.Send(maximumAvailableSlotsByEditionId)
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Scheduled Widget

        /// <summary>Shows the scheduled data widget.</summary>
        /// <param name="buyerOrganizationUid">The buyer organization uid.</param>
        /// <param name="sellerOrganizationUid">The seller organization uid.</param>
        /// <param name="projectKeywords">The project keywords.</param>
        /// <param name="date">The date.</param>
        /// <param name="roomUid">The room uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowScheduledDataWidget(ScheduledSearchViewModel searchViewModel)
        {
            var negotiations = await this.musicbusinessRoundnegotiationRepo.FindScheduledWidgetDtoAsync(
                this.EditionDto.Id,
                searchViewModel.BuyerOrganizationUid,
                searchViewModel.SellerOrganizationUid,
                searchViewModel.ProjectKeywords,
                searchViewModel.Date,
                searchViewModel.RoomUid,
                searchViewModel.ShowParticipants);

            ViewBag.ShowParticipants = searchViewModel.ShowParticipants;

            return new JsonResult()
            {
                Data = new
                {
                    status = "success",
                    pages = new List<dynamic>
                    {
                        new { page = this.RenderRazorViewToString("Widgets/ScheduledDataWidget", negotiations), divIdOrClass = "#MusicMeetingsScheduledWidget" },
                    }
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
        }

        #endregion

        #region Create

        /// <summary>Shows the create modal.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowCreateModal()
        {
            var cmd = new CreateMusicBusinessNegotiation();

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Modals/CreateModal", cmd), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Creates the specified create negotiation.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Create(CreateMusicBusinessNegotiation cmd)
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
                        new { page = this.RenderRazorViewToString("/Areas/Music/Views/Meetings/Modals/CreateForm.cshtml", cmd), divIdOrClass = "#form-container" },
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Negotiation, Labels.CreatedF) });
        }

        #endregion

        #region Update

        /// <summary>
        /// Shows the update modal.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowUpdateModal(Guid? MusicRoundNegotiationUid)
        {
            UpdateMusicBusinessRoundNegotiation cmd;

            try
            {
                var MusicBusinessRoundNegotiationDto = await this.musicbusinessRoundnegotiationRepo.FindDtoAsync(MusicRoundNegotiationUid ?? Guid.Empty);
                if (MusicBusinessRoundNegotiationDto == null)
                {
                    throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Negotiation, Labels.FoundM.ToLowerInvariant()));
                }

                cmd = new UpdateMusicBusinessRoundNegotiation(MusicBusinessRoundNegotiationDto, this.UserInterfaceLanguage);
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
                    new { page = this.RenderRazorViewToString("Modals/UpdateModal", cmd), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Updates the specified command.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        /// <exception cref="DomainException">
        /// </exception>
        [HttpPost]
        public async Task<ActionResult> Update(UpdateMusicBusinessRoundNegotiation cmd)
        {
            var result = new AppValidationResult();

            try
            {
                //BuyerOrganizationUid and ProjectUid get allways null from dropdown selected item, because dropdown is populated via JavaScript and has initialSelection.
                //These fiels isn't enabled to change, so don't worry with this backend fix!
                cmd.BuyerOrganizationUid = cmd.InitialBuyerOrganizationUid;
                cmd.ProjectUid = cmd.InitialProjectUid;

                UpdateMusicBusinessRoundNegotiation u;
                ModelState.Remove(nameof(u.BuyerOrganizationUid));
                ModelState.Remove(nameof(u.ProjectUid));

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
                        new { page = this.RenderRazorViewToString("/Areas/Music/Views/Meetings/Modals/UpdateForm.cshtml", cmd), divIdOrClass = "#form-container" },
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Negotiation, Labels.CreatedF) });
        }

        #endregion

        #region Delete

        /// <summary>Deletes the specified delete negotiation.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Delete(DeleteMusicBusinessRoundNegotiationCommandHandler cmd)
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

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Negotiation, Labels.DeletedF) });
        }

        #endregion

        #endregion

        #region Unscheduled

        /// <summary>Unscheduleds this instance.</summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Unscheduled()
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.OneToOneMeetings, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.UnscheduledNegotiations, Url.Action("Unscheduled", "Meetings", new { Area = "Music" }))
            });

            #endregion

            return View();
        }

        #region Edition Unscheduled Count Widget

        /// <summary>Shows the edition unscheduled count widget.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowEditionUnscheduledCountWidget()
        {
            var notScheduledCount = await this.musicbusinessRoundnegotiationRepo.CountNegotiationNotScheduledAsync(this.EditionDto.Id, false);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/EditionUnscheduledCountWidget", notScheduledCount), divIdOrClass = "#MusicMeetingsEditionUnscheduledCountWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Unscheduled Widget

        /// <summary>Shows the unscheduled widget.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowUnscheduledWidget()
        {
            var negotiations = await this.musicbusinessRoundnegotiationRepo.FindUnscheduledWidgetDtoAsync(this.EditionDto.Id);

            return new JsonResult()
            {
                Data = new
                {
                    status = "success",
                    pages = new List<dynamic>
                    {
                        new { page = this.RenderRazorViewToString("Widgets/UnscheduledWidget", negotiations), divIdOrClass = "#MusicMeetingsUnscheduledWidget" },
                    }
                },
                //ContentType = contentType,
                //ContentEncoding = contentEncoding,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
        }

        #endregion

        #region Manual Schedule

        [HttpGet]
        public async Task<ActionResult> ShowManualScheduleModal(Guid? projectBuyerEvaluationUid)
        {
            ScheduleManualNegotiation cmd;

            try
            {
                var projectBuyerEvaluationDto = await this.projectBuyerEvaluationRepo.FindDtoAsync(projectBuyerEvaluationUid ?? Guid.Empty);
                if (projectBuyerEvaluationDto == null)
                {
                    throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Negotiation, Labels.FoundM.ToLowerInvariant()));
                }

                cmd = new ScheduleManualNegotiation(projectBuyerEvaluationDto, this.UserInterfaceLanguage);
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
                    new { page = this.RenderRazorViewToString("Modals/ManualScheduleModal", cmd), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Creates the specified create negotiation.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> ManualSchedule(ScheduleManualNegotiation cmd)
        {
            var result = new AppValidationResult();

            try
            {
                //BuyerOrganizationUid and ProjectUid get allways null from dropdown selected item, because dropdown is populated via JavaScript and has initialSelection.
                //These fiels isn't enabled to change, so don't worry with this backend fix!
                cmd.BuyerOrganizationUid = cmd.InitialBuyerOrganizationUid;
                cmd.ProjectUid = cmd.InitialProjectUid;

                ScheduleManualNegotiation c;
                ModelState.Remove(nameof(c.BuyerOrganizationUid));
                ModelState.Remove(nameof(c.ProjectUid));

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
                        new { page = this.RenderRazorViewToString("/Areas/Music/Views/Meetings/Modals/ManualScheduleForm.cshtml", cmd), divIdOrClass = "#form-container" },
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Negotiation, Labels.CreatedF) });
        }

        #endregion

        #endregion

        #region Logistic Info Widget

        ///// <summary>
        ///// Shows the logistics information widget.
        ///// </summary>
        ///// <param name="organizationsUids">The organizations uids.</param>
        ///// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowLogisticsInfoWidget(string organizationsUids)
        {
            var attendeeCollaboratorDtos = await this.attendeeCollaboratorRepo.FindExecutivesSchedulesByOrganizationsUidsAsync(organizationsUids?.ToListGuid(','), this.EditionDto.Id);
            if (attendeeCollaboratorDtos == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Logistics, Labels.FoundMP.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("~/Areas/Music/Views/Meetings/Widgets/LogisticsInfoWidget.cshtml", attendeeCollaboratorDtos), divIdOrClass = "#MusicMeetingsLogisticsInfoWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        //#region Send E-mails to Players

        //#region List

        /// <summary>
        /// Sends the email to players.
        /// </summary>
        /// <param name = "searchViewModel" > The search view model.</param>
        /// <returns></returns>
        //[HttpGet]
        //public ActionResult SendEmailToPlayers(SendEmailToPlayersSearchViewModel searchViewModel)
        //{
            #region Breadcrumb

        //    ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.OneToOneMeetings, new List<BreadcrumbItemHelper> {
        //        new BreadcrumbItemHelper(Labels.AudioVisual, null),
        //        new BreadcrumbItemHelper(Labels.OneToOneMeetings, null),
        //        new BreadcrumbItemHelper(Labels.SendEmailToPlayers, Url.Action("SendEmailToPlayers", "Meetings", new { Area = "Music" }))
        //    });

           #endregion

        //    return View(searchViewModel);
        //}

        /// <summary>
        /// Sends the email to players search.
        /// </summary>
        /// <param name="request">The request.</param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult> SendEmailToPlayersSearch(IDataTablesRequest request)
        //{
        //    var producers = await this.attendeeOrganizationRepo.FindAllByActiveBuyerNegotiationsAndByDataTable(
        //        request.Start / request.Length,
        //        request.Length,
        //        request.Search?.Value,
        //        request.GetSortColumns(),
        //        this.EditionDto.Id,
        //        this.AdminAccessControlDto.Language.Id);

        //    var response = DataTablesResponse.Create(request, producers.TotalItemCount, producers.TotalItemCount, producers);

        //    return Json(new
        //    {
        //        status = "success",
        //        dataTable = response
        //    }, JsonRequestBehavior.AllowGet);
        //}

        //#endregion

        //#region Total Count Widget

        /// <summary>
        /// Shows the send email to players total count widget.
        /// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult> ShowSendEmailToPlayersTotalCountWidget()
        //{
        //    var producers = await this.attendeeOrganizationRepo.CountAllByActiveBuyerNegotiationsAndByDataTable(true, this.EditionDto.Id);

        //    return Json(new
        //    {
        //        status = "success",
        //        pages = new List<dynamic>
        //        {
        //            new { page = this.RenderRazorViewToString("Widgets/SendEmailToPlayersTotalCountWidget", producers), divIdOrClass = "#AudiovisualMeetingsSendEmailToPlayersTotalCountWidget" },
        //        }
        //    }, JsonRequestBehavior.AllowGet);
        //}

        //#endregion

        //#region Edition Count Widget

        /// <summary>
        /// Shows the send email to players edition count widget.
        /// </summary>
        /// <returns></returns>
        //public async Task<ActionResult> ShowSendEmailToPlayersEditionCountWidget()
        //{
        //    var producers = await this.attendeeOrganizationRepo.CountAllByActiveBuyerNegotiationsAndByDataTable(false, this.EditionDto.Id);

        //    return Json(new
        //    {
        //        status = "success",
        //        pages = new List<dynamic>
        //        {
        //            new { page = this.RenderRazorViewToString("Widgets/SendEmailToPlayersEditionCountWidget", producers), divIdOrClass = "#AudiovisualMeetingsSendEmailToPlayersEditionCountWidget" },
        //        }
        //    }, JsonRequestBehavior.AllowGet);
        //}

        //#endregion

        //#region Send E-mails

        ///// <summary>
        ///// Sends the players emails.
        ///// </summary>
        ///// <param name="keywords">The keywords.</param>
        ///// <param name="selectedAttendeeOrganizationsUids">The selected attendee organizations uids.</param>
        ///// <returns></returns>
        ///// <exception cref="DomainException">
        ///// </exception>
        //[HttpPost]
        //public async Task<ActionResult> SendPlayersEmails(string keywords, string selectedAttendeeOrganizationsUids)
        //{
        //    AppValidationResult result = null;

        //    try
        //    {
        //        var attendeeOrganizationBaseDtos = await this.attendeeOrganizationRepo.FindAllBaseDtoByActiveBuyerNegotiations(
        //            keywords,
        //            selectedAttendeeOrganizationsUids?.ToListGuid(','),
        //            this.EditionDto.Id,
        //            this.AdminAccessControlDto.Language.Id);
        //        if (attendeeOrganizationBaseDtos?.Any() != true)
        //        {
        //            throw new DomainException(Messages.SelectAtLeastOneOption);
        //        }

        //        List<string> errors = new List<string>();
        //        foreach (var attendeeOrganizationBaseDto in attendeeOrganizationBaseDtos)
        //        {
        //            foreach (var attendeeCollaboratorBaseDto in attendeeOrganizationBaseDto.AttendeeCollaboratorBaseDtos)
        //            {
        //                // If the collaborator does not have an user interface language, use the user interface language of the current user
        //                var collaboratorLanguageCode = attendeeCollaboratorBaseDto.CollaboratorBaseDto.UserBaseDto.UserInterfaceLanguageCode ?? this.UserInterfaceLanguage;

        //                try
        //                {
        //                    result = await this.CommandBus.Send(new SendPlayerNegotiationsEmailAsync(
        //                        attendeeOrganizationBaseDto,
        //                        attendeeCollaboratorBaseDto.CollaboratorBaseDto.UserBaseDto.Id,
        //                        attendeeCollaboratorBaseDto.CollaboratorBaseDto.UserBaseDto.Uid,
        //                        attendeeCollaboratorBaseDto.CollaboratorBaseDto.FirstName,
        //                        attendeeCollaboratorBaseDto.CollaboratorBaseDto.FullName,
        //                        attendeeCollaboratorBaseDto.CollaboratorBaseDto.Email,
        //                        this.EditionDto.Edition,
        //                        this.AdminAccessControlDto.User.Id,
        //                        collaboratorLanguageCode));
        //                    if (!result.IsValid)
        //                    {
        //                        throw new DomainException(Messages.CorrectFormValues);
        //                    }
        //                }
        //                catch (DomainException)
        //                {
        //                    //Cannot stop sending email when exception occurs.
        //                    errors.AddRange(result.Errors.Select(e => e.Message));
        //                }
        //                catch (Exception ex)
        //                {
        //                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        //                }
        //            }

        //            if (errors.Any())
        //            {
        //                throw new DomainException(string.Format(Messages.OneOrMoreEmailsNotSend, Labels.WelcomeEmail));
        //            }
        //        }
        //    }
        //    catch (DomainException ex)
        //    {
        //        return Json(new
        //        {
        //            status = "error",
        //            message = result?.Errors?.FirstOrDefault(e => e.Target == "ToastrError")?.Message ?? ex.GetInnerMessage(),
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        //        return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Emails, Labels.SentMP.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
        //}

        //#endregion

        //#endregion

        //#region Send E-mails to Producers

        //#region List

        ///// <summary>
        ///// Sends the email to producers.
        ///// </summary>
        ///// <param name="searchViewModel">The search view model.</param>
        ///// <returns></returns>
        //[HttpGet]
        //public ActionResult SendEmailToProducers(SendEmailToProducersSearchViewModel searchViewModel)
        //{
        //    #region Breadcrumb

        //    ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.OneToOneMeetings, new List<BreadcrumbItemHelper> {
        //        new BreadcrumbItemHelper(Labels.AudioVisual, null),
        //        new BreadcrumbItemHelper(Labels.OneToOneMeetings, null),
        //        new BreadcrumbItemHelper(Labels.SendEmailToProducers, Url.Action("SendEmailToProducers", "Meetings", new { Area = "Music" }))
        //    });

        //    #endregion

        //    return View(searchViewModel);
        //}

        ///// <summary>
        ///// Sends the email to producers search.
        ///// </summary>
        ///// <param name="request">The request.</param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult> SendEmailToProducersSearch(IDataTablesRequest request)
        //{
        //    var producers = await this.attendeeOrganizationRepo.FindAllByActiveSellerNegotiationsAndByDataTable(
        //        request.Start / request.Length,
        //        request.Length,
        //        request.Search?.Value,
        //        request.GetSortColumns(),
        //        this.EditionDto.Id,
        //        this.AdminAccessControlDto.Language.Id);

        //    var response = DataTablesResponse.Create(request, producers.TotalItemCount, producers.TotalItemCount, producers);

        //    return Json(new
        //    {
        //        status = "success",
        //        dataTable = response
        //    }, JsonRequestBehavior.AllowGet);
        //}

        //#endregion

        //#region Total Count Widget

        ///// <summary>
        ///// Shows the send email to players total count widget.
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult> ShowSendEmailToProducersTotalCountWidget()
        //{
        //    var producers = await this.attendeeOrganizationRepo.CountAllByActiveSellerNegotiationsAndByDataTable(true, this.EditionDto.Id);

        //    return Json(new
        //    {
        //        status = "success",
        //        pages = new List<dynamic>
        //        {
        //            new { page = this.RenderRazorViewToString("Widgets/SendEmailToProducersTotalCountWidget", producers), divIdOrClass = "#MusicMeetingsSendEmailToProducersTotalCountWidget" },
        //        }
        //    }, JsonRequestBehavior.AllowGet);
        //}

        //#endregion

        //#region Edition Count Widget

        ///// <summary>
        ///// Shows the send email to players edition count widget.
        ///// </summary>
        ///// <returns></returns>
        //public async Task<ActionResult> ShowSendEmailToProducersEditionCountWidget()
        //{
        //    var producers = await this.attendeeOrganizationRepo.CountAllByActiveSellerNegotiationsAndByDataTable(false, this.EditionDto.Id);

        //    return Json(new
        //    {
        //        status = "success",
        //        pages = new List<dynamic>
        //        {
        //            new { page = this.RenderRazorViewToString("Widgets/SendEmailToProducersEditionCountWidget", producers), divIdOrClass = "#MusicMeetingsSendEmailToProducersEditionCountWidget" },
        //        }
        //    }, JsonRequestBehavior.AllowGet);
        //}

        //#endregion

        //#region Send E-mails

        ///// <summary>
        ///// Sends the producers emails.
        ///// </summary>
        ///// <param name="keywords">The keywords.</param>
        ///// <param name="selectedAttendeeOrganizationsUids">The selected attendee organizations uids.</param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<ActionResult> SendProducersEmails(string keywords, string selectedAttendeeOrganizationsUids)
        //{
        //    AppValidationResult result = null;

        //    try
        //    {
        //        var attendeeOrganizationBaseDtos = await this.attendeeOrganizationRepo.FindAllBaseDtoByActiveSellerNegotiations(
        //            keywords,
        //            selectedAttendeeOrganizationsUids?.ToListGuid(','),
        //            this.EditionDto.Id,
        //            this.AdminAccessControlDto.Language.Id);
        //        if (attendeeOrganizationBaseDtos?.Any() != true)
        //        {
        //            throw new DomainException(Messages.SelectAtLeastOneOption);
        //        }

        //        List<string> errors = new List<string>();
        //        foreach (var attendeeOrganizationBaseDto in attendeeOrganizationBaseDtos)
        //        {
        //            foreach (var attendeeCollaboratorBaseDto in attendeeOrganizationBaseDto.AttendeeCollaboratorBaseDtos)
        //            {
        //                // If the collaborator does not have an user interface language, use the user interface language of the current user
        //                var collaboratorLanguageCode = attendeeCollaboratorBaseDto.CollaboratorBaseDto.UserBaseDto.UserInterfaceLanguageCode ?? this.UserInterfaceLanguage;

        //                try
        //                {
        //                    result = await this.CommandBus.Send(new SendProducerNegotiationsEmailAsync(
        //                        attendeeOrganizationBaseDto,
        //                        attendeeCollaboratorBaseDto.CollaboratorBaseDto.UserBaseDto.Id,
        //                        attendeeCollaboratorBaseDto.CollaboratorBaseDto.UserBaseDto.Uid,
        //                        attendeeCollaboratorBaseDto.CollaboratorBaseDto.FirstName,
        //                        attendeeCollaboratorBaseDto.CollaboratorBaseDto.FullName,
        //                        attendeeCollaboratorBaseDto.CollaboratorBaseDto.Email,
        //                        this.EditionDto.Edition,
        //                        this.AdminAccessControlDto.User.Id,
        //                        collaboratorLanguageCode));
        //                    if (!result.IsValid)
        //                    {
        //                        throw new DomainException(Messages.CorrectFormValues);
        //                    }
        //                }
        //                catch (DomainException ex)
        //                {
        //                    //Cannot stop sending email when exception occurs.
        //                    errors.AddRange(result.Errors.Select(e => e.Message));
        //                }
        //                catch (Exception ex)
        //                {
        //                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        //                }
        //            }

        //            if (errors.Any())
        //            {
        //                throw new DomainException(string.Format(Messages.OneOrMoreEmailsNotSend, Labels.WelcomeEmail));
        //            }
        //        }
        //    }
        //    catch (DomainException ex)
        //    {
        //        return Json(new
        //        {
        //            status = "error",
        //            message = result?.Errors?.FirstOrDefault(e => e.Target == "ToastrError")?.Message ?? ex.GetInnerMessage(),
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        //        return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Emails, Labels.SentMP.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
        //}

        //#endregion

        ////#endregion

        //#region Report

        ///// <summary>Indexes the specified search view model.</summary>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult> Report()
        //{
        //    #region Breadcrumb

        //    ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.OneToOneMeetings, new List<BreadcrumbItemHelper> {
        //        new BreadcrumbItemHelper(Labels.CalendarReport, Url.Action("Report", "Meetings", new { Area = "Music" }))
        //    });

        //    #endregion

        //    ViewBag.Rooms = (await this.roomRepo.FindAllDtoByEditionIdAsync(this.EditionDto.Id))?.Select(r => new RoomJsonDto
        //    {
        //        Id = r.Room.Id,
        //        Uid = r.Room.Uid,
        //        Name = r.GetRoomNameByLanguageCode(this.UserInterfaceLanguage)?.RoomName?.Value
        //    })?.ToList();

        //    return View();
        //}

        ///// <summary>
        ///// Shows the report data widget.
        ///// </summary>
        ///// <param name="searchViewModel">The search view model.</param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult> ShowReportDataWidget(ScheduledSearchViewModel searchViewModel)
        //{
        //    var negotiationDtos = await this.negotiationRepo.FindReportWidgetDtoAsync(
        //        this.EditionDto.Id,
        //        searchViewModel.BuyerOrganizationUid,
        //        searchViewModel.SellerOrganizationUid,
        //        searchViewModel.ProjectKeywords,
        //        searchViewModel.Date,
        //        searchViewModel.RoomUid,
        //        searchViewModel.ShowParticipants);

        //    return new JsonResult()
        //    {
        //        Data = new
        //        {
        //            status = "success",
        //            pages = new List<dynamic>
        //            {
        //                new { page = this.RenderRazorViewToString("Widgets/ReportDataWidget", negotiationDtos), divIdOrClass = "#MusicMeetingsReportWidget" },
        //            }
        //        },
        //        JsonRequestBehavior = JsonRequestBehavior.AllowGet,
        //        MaxJsonLength = Int32.MaxValue
        //    };
        //}

        ///// <summary>
        ///// Exports the report to excel.
        ///// </summary>
        ///// <param name="searchViewModel">The search view model.</param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult> ExportReportToExcel(ScheduledSearchViewModel searchViewModel)
        //{
        //    var negotiationDtos = await this.negotiationRepo.FindReportWidgetDtoAsync(
        //        this.EditionDto.Id,
        //        searchViewModel.BuyerOrganizationUid,
        //        searchViewModel.SellerOrganizationUid,
        //        searchViewModel.ProjectKeywords,
        //        searchViewModel.Date,
        //        searchViewModel.RoomUid,
        //        searchViewModel.ShowParticipants);

        //    var workbook = new XLWorkbook();
        //    var worksheet = workbook.Worksheets.Add(Labels.CalendarReport);
        //    worksheet.Outline.SummaryVLocation = XLOutlineSummaryVLocation.Top;
        //    worksheet.ShowGridLines = true;

        //    // Excel colors and formatting
        //    var dateBackgroundColor = XLColor.FromHtml("#808080");
        //    var dateFontColor = XLColor.White;
        //    var roomBackgroundColor = XLColor.FromHtml("#d1d1d1");
        //    var roomFontColor = XLColor.Black;
        //    var tableBackgroundColor = XLColor.FromHtml("#d1d1d1");
        //    var tableFontColor = XLColor.Black;
        //    var totalColumns = 1;

        //    if (negotiationDtos?.Any() == true)
        //    {
        //        var lineIndex = 1;

        //        foreach (var negotiationReportGroupedByDateDto in negotiationDtos)
        //        {
        //            #region Date Header

        //            var columnsCount = negotiationReportGroupedByDateDto.NegotiationReportGroupedByRoomDtos.SelectMany(nr => nr.NegotiationReportGroupedByStartDateDtos?.SelectMany(nd => nd.Negotiations.Select(n => n.TableNumber)))?.Distinct()?.Count() ?? 0;

        //            var columnIndex = 0;

        //            worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = negotiationReportGroupedByDateDto.Date.ToShortDateString();
        //            worksheet.Range(columnIndex.GetExcelColumnLetters() + lineIndex + ":" + (columnsCount + 1).GetExcelColumnLetters() + lineIndex).Row(1).Merge();
        //            worksheet.Cell(lineIndex, columnIndex).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(lineIndex, columnIndex).Style.Font.Bold = true;
        //            worksheet.Cell(lineIndex, columnIndex).Style.Fill.BackgroundColor = dateBackgroundColor;
        //            worksheet.Cell(lineIndex, columnIndex).Style.Font.SetFontColor(dateFontColor);

        //            #endregion Date Header

        //            if (negotiationReportGroupedByDateDto.NegotiationReportGroupedByRoomDtos?.Any() == true)
        //            {
        //                foreach (var negotiationReportGroupedByRoomDto in negotiationReportGroupedByDateDto.NegotiationReportGroupedByRoomDtos)
        //                {
        //                    var roomName = negotiationReportGroupedByRoomDto?.GetRoomNameByLanguageCode(ViewBag.UserInterfaceLanguage)?.Value;
        //                    var tableNumbers = negotiationReportGroupedByRoomDto?.NegotiationReportGroupedByStartDateDtos?.SelectMany(nd => nd.Negotiations.Select(n => n.TableNumber))?.Distinct()?.OrderBy(tn => tn)?.ToList() ??
        //                                       new List<int>();

        //                    #region Room Sub Header

        //                    lineIndex++;
        //                    columnIndex = 0;

        //                    worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = roomName;
        //                    worksheet.Range(columnIndex.GetExcelColumnLetters() + lineIndex + ":" + (tableNumbers.Count() + 1).GetExcelColumnLetters() + lineIndex).Row(1).Merge();
        //                    worksheet.Cell(lineIndex, columnIndex).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                    worksheet.Cell(lineIndex, columnIndex).Style.Font.Bold = true;
        //                    worksheet.Cell(lineIndex, columnIndex).Style.Fill.BackgroundColor = roomBackgroundColor;
        //                    worksheet.Cell(lineIndex, columnIndex).Style.Font.SetFontColor(roomFontColor);

        //                    #endregion Room Sub Headers

        //                    #region Table Header

        //                    lineIndex++;
        //                    columnIndex = 0;

        //                    worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = Labels.Hour;
        //                    worksheet.Cell(lineIndex, columnIndex).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        //                    worksheet.Cell(lineIndex, columnIndex).Style.Font.Bold = true;
        //                    worksheet.Cell(lineIndex, columnIndex).Style.Fill.BackgroundColor = tableBackgroundColor;
        //                    worksheet.Cell(lineIndex, columnIndex).Style.Font.SetFontColor(tableFontColor);

        //                    foreach (var tableNumber in tableNumbers)
        //                    {
        //                        worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = $"{Labels.Table} {tableNumber}";
        //                        worksheet.Cell(lineIndex, columnIndex).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        //                        worksheet.Cell(lineIndex, columnIndex).Style.Font.Bold = true;
        //                        worksheet.Cell(lineIndex, columnIndex).Style.Fill.BackgroundColor = tableBackgroundColor;
        //                        worksheet.Cell(lineIndex, columnIndex).Style.Font.SetFontColor(tableFontColor);
        //                    }

        //                    #endregion Table Header

        //                    #region Hours

        //                    foreach (var negotiationReportGroupedByStartDateDto in negotiationReportGroupedByRoomDto.NegotiationReportGroupedByStartDateDtos.OrderBy(n => n.StartDate))
        //                    {
        //                        lineIndex++;
        //                        columnIndex = 0;

        //                        worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = $"{negotiationReportGroupedByStartDateDto.StartDate.ToString("HH:mm")}\n{negotiationReportGroupedByStartDateDto.EndDate.ToString("HH:mm")}";
        //                        worksheet.Cell(lineIndex, columnIndex).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        //                        worksheet.Cell(lineIndex, columnIndex).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
        //                        worksheet.Cell(lineIndex, columnIndex).Style.Font.Bold = true;
        //                        worksheet.Cell(lineIndex, columnIndex).Style.Alignment.WrapText = true;

        //                        foreach (var tableNumber in tableNumbers)
        //                        {
        //                            totalColumns = columnIndex > totalColumns ? columnIndex : totalColumns;

        //                            var negotiation = negotiationReportGroupedByStartDateDto.Negotiations.FirstOrDefault(n => n.TableNumber == tableNumber);
        //                            if (negotiation != null)
        //                            {
        //                                worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = negotiation.ProjectBuyerEvaluation?.BuyerAttendeeOrganization?.Organization?.TradeName +
        //                                                                                                           "\n" + negotiation.ProjectBuyerEvaluation?.Project?.ProjectTitles?.FirstOrDefault(pt => pt.Language.Code == ViewBag.UserInterfaceLanguage)?.Value +
        //                                                                                                           "\n" + negotiation.ProjectBuyerEvaluation?.Project?.SellerAttendeeOrganization?.Organization?.TradeName;
        //                            }
        //                            else
        //                            {
        //                                worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = string.Empty;
        //                            }

        //                            worksheet.Cell(lineIndex, columnIndex).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        //                            worksheet.Cell(lineIndex, columnIndex).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
        //                            worksheet.Cell(lineIndex, columnIndex).Style.Alignment.WrapText = true;
        //                        }
        //                    }

        //                    #endregion Hours
        //                }
        //            }

        //            lineIndex++;
        //        }
        //    }

        //    // AdjustColumns
        //    for (var i = 1; i <= totalColumns + 1; i++)
        //    {
        //        worksheet.Column(i).AdjustToContents();
        //    }

        //    return new ExcelResult(workbook, Labels.CalendarReport + "_" + DateTime.UtcNow.ToBrazilTimeZone().ToString("yyyyMMdd"));
        //}

        ///// <summary>
        ///// Exports the report to PDF.
        ///// </summary>
        ///// <param name="searchViewModel">The search view model.</param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult> ExportReportToPdf(ScheduledSearchViewModel searchViewModel)
        //{
        //    var negotiationsDtos = await this.negotiationRepo.FindReportWidgetDtoAsync(
        //        this.EditionDto.Id,
        //        searchViewModel.BuyerOrganizationUid,
        //        searchViewModel.SellerOrganizationUid,
        //        searchViewModel.ProjectKeywords,
        //        searchViewModel.Date,
        //        searchViewModel.RoomUid,
        //        searchViewModel.ShowParticipants);

        //    if (negotiationsDtos.Count == 0)
        //    {
        //        return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.ScheduledOneToOneMeetings, Labels.FoundFP) }, JsonRequestBehavior.AllowGet);
        //    }

        //    var pdf = new PlataformaRio2CDocument(new ScheduledMeetingsReportDocumentTemplate(negotiationsDtos, this.UserInterfaceLanguage, this.EditionDto));
        //    var fileName = $"{Labels.ScheduledNegotiations.RemoveFilenameInvalidChars().Trim()}_{DateTime.Now.ToStringHourMinute()}.pdf".RemoveFilenameInvalidChars();
        //    return File(pdf.GetStream(), "application/pdf", fileName);
        //}

        //#endregion
    }
}
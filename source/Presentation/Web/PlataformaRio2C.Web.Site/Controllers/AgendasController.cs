// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-27-2020
// ***********************************************************************
// <copyright file="AgendasController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using MediatR;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Web.Site.Controllers
{
    /// <summary>AgendasController</summary>
    [Authorize(Order = 1)]
    public class AgendasController : BaseController
    {
        private readonly IConferenceRepository conferenceRepo;
        private readonly INegotiationRepository negotiationRepo;
        private readonly ILogisticAirfareRepository logisticAirfareRepo;
        private readonly ILogisticAccommodationRepository logisticAccommodationRepo;
        private readonly ILogisticTransferRepository logisticTransferRepo;

        /// <summary>Initializes a new instance of the <see cref="AgendasController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="conferenceRepository">The conference repository.</param>
        /// <param name="negotiationRepository">The negotiation repository.</param>
        /// <param name="logisticAirfareRepository">The logistic airfare repository.</param>
        /// <param name="logisticAccommodationRepository">The logistic accommodation repository.</param>
        /// <param name="logisticTransferRepository">The logistic transfer repository.</param>
        public AgendasController(
            IMediator commandBus, 
            IdentityAutenticationService identityController,
            IConferenceRepository conferenceRepository,
            INegotiationRepository negotiationRepository,
            ILogisticAirfareRepository logisticAirfareRepository,
            ILogisticAccommodationRepository logisticAccommodationRepository,
            ILogisticTransferRepository logisticTransferRepository)
            : base(commandBus, identityController)
        {
            this.conferenceRepo = conferenceRepository;
            this.negotiationRepo = negotiationRepository;
            this.logisticAirfareRepo = logisticAirfareRepository;
            this.logisticAccommodationRepo = logisticAccommodationRepository;
            this.logisticTransferRepo = logisticTransferRepository;
        }

        /// <summary>Indexes this instance.</summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.Agenda, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Agenda, Url.Action("Index", "Agendas", new { Area = "" }))
            });

            #endregion

            return View(new AgendaSearchViewModel());
        }

        #region Conferences

        /// <summary>Gets the conferences data.</summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetConferencesData(AgendaSearchViewModel viewModel)
        {
            if (!viewModel.ShowMyConferences && !viewModel.ShowAllConferences)
            {
                return Json(new { status = "success", events = new List<AgendaBaseEventJsonDto>() }, JsonRequestBehavior.AllowGet);
            }

            if (!viewModel.StartDate.HasValue || !viewModel.EndDate.HasValue)
            {
                return Json(new { status = "error", message = string.Format(Messages.TheFieldIsRequired, Labels.Date) }, JsonRequestBehavior.AllowGet);
            }

            var conferenceDtos = await this.conferenceRepo.FindAllScheduleDtosAsync(
                this.EditionDto.Id,
                this.UserAccessControlDto?.EditionAttendeeCollaborator?.Id ?? 0,
                DateTimeOffset.FromUnixTimeSeconds(viewModel.StartDate.Value),
                DateTimeOffset.FromUnixTimeSeconds(viewModel.EndDate.Value),
                viewModel.ShowMyConferences,
                viewModel.ShowAllConferences);

            var events = conferenceDtos?.Select(cd => new AgendaConferenceEventJsonDto
            {
                Id = cd.Conference.Uid.ToString(),
                Type = "Conference",
                Title = cd.GetConferenceTitleDtoByLanguageCode(this.UserInterfaceLanguage)?.ConferenceTitle?.Value,
                Start = cd.Conference.StartDate,
                End = cd.Conference.EndDate,
                AllDay = false,
                Css = cd.IsParticipant == true ? "fc-event-solid-primary fc-event-light popover-enabled" : "fc-event-solid-light fc-event-brand popover-enabled",
                Room = cd.RoomDto.GetRoomNameByLanguageCode(this.UserInterfaceLanguage)?.RoomName.Value,
                EditionEvent = cd.EditionEvent.Name.GetSeparatorTranslation(this.UserInterfaceLanguage, Language.Separator),
                Synopsis = cd.GetConferenceSynopsisDtoByLanguageCode(this.UserInterfaceLanguage)?.ConferenceSynopsis.Value
            });

            return Json(new
            {
                status = "success",
                events
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Meetings

        /// <summary>Gets the audiovisual meetings data.</summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetAudiovisualMeetingsData(AgendaSearchViewModel viewModel)
        {
            if (DateTime.UtcNow < this.EditionDto.OneToOneMeetingsScheduleDate || !viewModel.ShowOneToOneMeetings)
            {
                return Json(new { status = "success", events = new List<AgendaBaseEventJsonDto>() }, JsonRequestBehavior.AllowGet);
            }

            if (!viewModel.StartDate.HasValue || !viewModel.EndDate.HasValue)
            {
                return Json(new { status = "error", message = string.Format(Messages.TheFieldIsRequired, Labels.Date) }, JsonRequestBehavior.AllowGet);
            }

            var negotiationsDtos = await this.negotiationRepo.FindAllScheduleDtosAsync(
                this.EditionDto.Id,
                this.UserAccessControlDto?.EditionAttendeeCollaborator?.Id ?? 0,
                DateTimeOffset.FromUnixTimeSeconds(viewModel.StartDate.Value),
                DateTimeOffset.FromUnixTimeSeconds(viewModel.EndDate.Value));

            var events = negotiationsDtos?.Select(nd => new AgendaNegotiationEventJsonDto
            {
                Id = nd.Negotiation.Uid.ToString(),
                Type = "AudiovisualMeeting",
                Title = nd.ProjectBuyerEvaluationDto.ProjectDto.GetTitleDtoByLanguageCode(this.UserInterfaceLanguage).ProjectTitle.Value,
                Start = nd.Negotiation.StartDate,
                End = nd.Negotiation.EndDate,
                AllDay = false,
                Css = "fc-event-solid-danger fc-event-light popover-enabled",
                ProjectLogLine = nd.ProjectBuyerEvaluationDto.ProjectDto.GetLogLineDtoByLanguageCode(this.UserInterfaceLanguage)?.ProjectLogLine.Value,
                Producer = nd.ProjectBuyerEvaluationDto.ProjectDto.SellerAttendeeOrganizationDto.Organization.Name,
                Player = nd.ProjectBuyerEvaluationDto.BuyerAttendeeOrganizationDto.Organization.TradeName,
                Room = nd.RoomDto.GetRoomNameByLanguageCode(this.UserInterfaceLanguage)?.RoomName?.Value,
                TableNumber = nd.Negotiation.TableNumber,
                RoundNumber = nd.Negotiation.RoundNumber,
            });

            return Json(new
            {
                status = "success",
                events
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Logistic Airfares

        /// <summary>Gets the logistic airfares data.</summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetLogisticAirfaresData(AgendaSearchViewModel viewModel)
        {
            if (DateTime.UtcNow < this.EditionDto.OneToOneMeetingsScheduleDate || !viewModel.ShowFlights)
            {
                return Json(new { status = "success", events = new List<AgendaBaseEventJsonDto>() }, JsonRequestBehavior.AllowGet);
            }

            if (!viewModel.StartDate.HasValue || !viewModel.EndDate.HasValue)
            {
                return Json(new { status = "error", message = string.Format(Messages.TheFieldIsRequired, Labels.Date) }, JsonRequestBehavior.AllowGet);
            }

            var logisticAirfareDtos = await this.logisticAirfareRepo.FindAllScheduleDtosAsync(
                this.EditionDto.Id,
                this.UserAccessControlDto?.EditionAttendeeCollaborator?.Id ?? 0,
                DateTimeOffset.FromUnixTimeSeconds(viewModel.StartDate.Value),
                DateTimeOffset.FromUnixTimeSeconds(viewModel.EndDate.Value));

            // Logistic airfares (in day)
            var events = logisticAirfareDtos?.Select(lad => new AgendaLogisticAirfareEventJsonDto
            {
                Id = lad.LogisticAirfare.Uid.ToString(),
                Type = "LogisticAirfare",
                Title = $"{lad.LogisticAirfare.From} - {lad.LogisticAirfare.To}",
                Start = lad.LogisticAirfare.DepartureDate,
                End = lad.LogisticAirfare.ArrivalDate,
                AllDay = false,
                Css = "fc-event-solid-warning fc-event-light popover-enabled",
                FlightType = lad.LogisticAirfare.IsNational ? Labels.National : Labels.International,
                FromPlace = lad.LogisticAirfare.From,
                ToPlace = lad.LogisticAirfare.To,
                TicketNumber = lad.LogisticAirfare.TicketNumber ?? "-"
            }) ?? new List<AgendaLogisticAirfareEventJsonDto>();

            return Json(new
            {
                status = "success",
                events
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Logistic Accommodations

        /// <summary>Gets the logistic accommodations data.</summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetLogisticAccommodationsData(AgendaSearchViewModel viewModel)
        {
            if (DateTime.UtcNow < this.EditionDto.OneToOneMeetingsScheduleDate || !viewModel.ShowAccommodations)
            {
                return Json(new { status = "success", events = new List<AgendaBaseEventJsonDto>() }, JsonRequestBehavior.AllowGet);
            }

            if (!viewModel.StartDate.HasValue || !viewModel.EndDate.HasValue)
            {
                return Json(new { status = "error", message = string.Format(Messages.TheFieldIsRequired, Labels.Date) }, JsonRequestBehavior.AllowGet);
            }

            var logisticAccommodationDtos = await this.logisticAccommodationRepo.FindAllScheduleDtosAsync(
                this.EditionDto.Id,
                this.UserAccessControlDto?.EditionAttendeeCollaborator?.Id ?? 0,
                DateTimeOffset.FromUnixTimeSeconds(viewModel.StartDate.Value),
                DateTimeOffset.FromUnixTimeSeconds(viewModel.EndDate.Value));

            // Accommodations (all day)
            var events = (logisticAccommodationDtos?.Select(lad => new AgendaBaseEventJsonDto
            {
                Id = lad.LogisticAccommodation.Uid.ToString(),
                Type = "LogisticAccommodation",
                Title = lad.PlaceDto.Place.Name.GetSeparatorTranslation(this.UserInterfaceLanguage, Language.Separator),
                Start = lad.LogisticAccommodation.CheckInDate,
                End = lad.LogisticAccommodation.CheckOutDate.AddDays(1),
                AllDay = true,
                Css = "fc-event-solid-success fc-event-light popover-enabled"
            }) ?? new List<AgendaBaseEventJsonDto>())?
            // Accommodations (checkin)
            .Union(logisticAccommodationDtos?.Select(lad => new AgendaBaseEventJsonDto
            {
                Id = lad.LogisticAccommodation.Uid.ToString(),
                Type = "LogisticAccommodation",
                Title = lad.PlaceDto.Place.Name.GetSeparatorTranslation(this.UserInterfaceLanguage, Language.Separator) + " (checkin)",
                Start = lad.LogisticAccommodation.CheckInDate,
                End = lad.LogisticAccommodation.CheckInDate.AddMinutes(30),
                AllDay = false,
                Css = "fc-event-solid-success fc-event-light popover-enabled"
            }) ?? new List<AgendaBaseEventJsonDto>())
            // Accommodations (checkout)
            .Union(logisticAccommodationDtos?.Select(lad => new AgendaBaseEventJsonDto
            {
                Id = lad.LogisticAccommodation.Uid.ToString(),
                Type = "LogisticAccommodation",
                Title = lad.PlaceDto.Place.Name.GetSeparatorTranslation(this.UserInterfaceLanguage, Language.Separator) + " (checkout)",
                Start = lad.LogisticAccommodation.CheckOutDate,
                End = lad.LogisticAccommodation.CheckOutDate.AddMinutes(30),
                AllDay = false,
                Css = "fc-event-solid-success fc-event-light popover-enabled"
            }) ?? new List<AgendaBaseEventJsonDto>());

            return Json(new
            {
                status = "success",
                events
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Logistic Transfers

        /// <summary>Gets the logistic transfers data.</summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetLogisticTransfersData(AgendaSearchViewModel viewModel)
        {
            if (DateTime.UtcNow < this.EditionDto.OneToOneMeetingsScheduleDate || !viewModel.ShowTransfers)
            {
                return Json(new { status = "success", events = new List<AgendaBaseEventJsonDto>() }, JsonRequestBehavior.AllowGet);
            }

            if (!viewModel.StartDate.HasValue || !viewModel.EndDate.HasValue)
            {
                return Json(new { status = "error", message = string.Format(Messages.TheFieldIsRequired, Labels.Date) }, JsonRequestBehavior.AllowGet);
            }

            var logisticTransferDtos = await this.logisticTransferRepo.FindAllScheduleDtosAsync(
                this.EditionDto.Id,
                this.UserAccessControlDto?.EditionAttendeeCollaborator?.Id ?? 0,
                DateTimeOffset.FromUnixTimeSeconds(viewModel.StartDate.Value),
                DateTimeOffset.FromUnixTimeSeconds(viewModel.EndDate.Value));

            // Transfers
            var events = logisticTransferDtos?.Select(ltd => new AgendaBaseEventJsonDto
            {
                Id = ltd.LogisticTransfer.Uid.ToString(),
                Type = "LogisticTransfer",
                Title = $"{ltd.FromPlaceDto.Place.Name.GetSeparatorTranslation(this.UserInterfaceLanguage, Language.Separator)} - {ltd.ToPlaceDto.Place.Name.GetSeparatorTranslation(this.UserInterfaceLanguage, Language.Separator)}",
                Start = ltd.LogisticTransfer.Date,
                End = ltd.LogisticTransfer.Date.AddMinutes(30),
                AllDay = false,
                Css = "fc-event-solid-brand fc-event-light popover-enabled"
            }) ?? new List<AgendaBaseEventJsonDto>();

            return Json(new
            {
                status = "success",
                events
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
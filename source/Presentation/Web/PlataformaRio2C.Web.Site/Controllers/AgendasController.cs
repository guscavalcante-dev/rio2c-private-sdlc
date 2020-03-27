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
        private readonly ILogisticRepository logisticRepo;
        private readonly INegotiationRepository negotiationRepo;

        /// <summary>Initializes a new instance of the <see cref="AgendasController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="conferenceRepository">The conference repository.</param>
        /// <param name="logisticRepository">The logistic repository.</param>
        /// <param name="negotiationRepository">The negotiation repository.</param>
        public AgendasController(
            IMediator commandBus, 
            IdentityAutenticationService identityController,
            IConferenceRepository conferenceRepository,
            ILogisticRepository logisticRepository,
            INegotiationRepository negotiationRepository)
            : base(commandBus, identityController)
        {
            this.conferenceRepo = conferenceRepository;
            this.logisticRepo = logisticRepository;
            this.negotiationRepo = negotiationRepository;
        }

        /// <summary>Indexes this instance.</summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            //if (!_scheduleAppService.ScheduleIsEnable())
            //{
            //    return View("Disabled");
            //}

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.Agenda, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Agenda, Url.Action("Index", "Agendas", new { Area = "" }))
            });

            #endregion

            return View();
        }

        #region Conferences

        /// <summary>Gets the conferences data.</summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetConferencesData(long? startDate, long? endDate)
        {
            if (!startDate.HasValue || !endDate.HasValue)
            {
                return Json(new { status = "error", message = string.Format(Messages.TheFieldIsRequired, Labels.Date) }, JsonRequestBehavior.AllowGet);
            }

            var conferenceDtos = await this.conferenceRepo.FindAllScheduleDtosAsync(
                this.EditionDto.Id,
                this.UserAccessControlDto?.EditionAttendeeCollaborator?.Id,
                DateTimeOffset.FromUnixTimeSeconds(startDate.Value),
                DateTimeOffset.FromUnixTimeSeconds(endDate.Value));

            var events = conferenceDtos?.Select(cd => new ScheduleJsonDto
            {
                Id = cd.Conference.Uid.ToString(),
                Type = "Conference",
                Title = $"[{Labels.Conference}] {cd.GetConferenceTitleDtoByLanguageCode(this.UserInterfaceLanguage)?.ConferenceTitle?.Value ?? "-"}",
                Start = cd.Conference.StartDate,
                End = cd.Conference.EndDate,
                AllDay = false,
                Css = cd.IsParticipant == true ? "fc-event-solid-primary fc-event-light" : "fc-event-solid-light fc-event-brand"
            });

            return Json(new
            {
                status = "success",
                events
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Logistics

        /// <summary>Gets the logistics data.</summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetLogisticsData(long? startDate, long? endDate)
        {
            if (DateTime.UtcNow < this.EditionDto.OneToOneMeetingsScheduleDate)
            {
                return Json(new { status = "success", events = new List<ScheduleJsonDto>() }, JsonRequestBehavior.AllowGet);
            }

            if (!startDate.HasValue || !endDate.HasValue)
            {
                return Json(new { status = "error", message = string.Format(Messages.TheFieldIsRequired, Labels.Date) }, JsonRequestBehavior.AllowGet);
            }

            var logisticDto = await this.logisticRepo.FindScheduleDtoAsync(
                this.EditionDto.Id,
                this.UserAccessControlDto?.EditionAttendeeCollaborator?.Id ?? 0,
                DateTimeOffset.FromUnixTimeSeconds(startDate.Value),
                DateTimeOffset.FromUnixTimeSeconds(endDate.Value));

            // Logistic airfares (in day)
            var events = (logisticDto?.LogisticAirfareDtos?.Select(lad => new ScheduleJsonDto
            {
                Id = lad.LogisticAirfare.Uid.ToString(),
                Type = "LogisticAirfare",
                Title = $"[{Labels.Flight}] {lad.LogisticAirfare.From} - {lad.LogisticAirfare.To}",
                Start = lad.LogisticAirfare.DepartureDate,
                End = lad.LogisticAirfare.ArrivalDate,
                AllDay = false,
                Css = "fc-event-solid-warning fc-event-light"
            }) ?? new List<ScheduleJsonDto>())?
            // Accommodations (all day)
            .Union(logisticDto?.LogisticAccommodationDtos?.Select(lad => new ScheduleJsonDto
            {
                Id = lad.LogisticAccommodation.Uid.ToString(),
                Type = "LogisticAccommodation",
                Title = lad.PlaceDto.Place.Name.GetSeparatorTranslation(this.UserInterfaceLanguage, Language.Separator),
                Start = lad.LogisticAccommodation.CheckInDate,
                End = lad.LogisticAccommodation.CheckOutDate.AddDays(1),
                AllDay = true,
                Css = "fc-event-solid-success fc-event-light"
            }) ?? new List<ScheduleJsonDto>())?
            // Accommodations (check-in)
            .Union(logisticDto?.LogisticAccommodationDtos?.Select(lad => new ScheduleJsonDto
            {
                Id = lad.LogisticAccommodation.Uid.ToString(),
                Type = "LogisticAccommodation",
                Title = $"[Checkin] {lad.PlaceDto.Place.Name.GetSeparatorTranslation(this.UserInterfaceLanguage, Language.Separator)}",
                Start = lad.LogisticAccommodation.CheckInDate,
                End = lad.LogisticAccommodation.CheckInDate.AddMinutes(30),
                AllDay = false,
                Css = "fc-event-solid-success fc-event-info"
            }) ?? new List<ScheduleJsonDto>())?
            // Accommodations (check-out)
            .Union(logisticDto?.LogisticAccommodationDtos?.Select(lad => new ScheduleJsonDto
            {
                Id = lad.LogisticAccommodation.Uid.ToString(),
                Type = "LogisticAccommodation",
                Title = $"[Checkout] {lad.PlaceDto.Place.Name.GetSeparatorTranslation(this.UserInterfaceLanguage, Language.Separator)}",
                Start = lad.LogisticAccommodation.CheckOutDate,
                End = lad.LogisticAccommodation.CheckOutDate.AddMinutes(30),
                AllDay = false,
                Css = "fc-event-solid-success fc-event-light"
            }) ?? new List<ScheduleJsonDto>())?
            // Transfers
            .Union(logisticDto?.LogisticTransferDtos?.Select(ltd => new ScheduleJsonDto
            {
                Id = ltd.LogisticTransfer.Uid.ToString(),
                Type = "LogisticTransfer",
                Title = $"[Transfer] {ltd.FromPlaceDto.Place.Name.GetSeparatorTranslation(this.UserInterfaceLanguage, Language.Separator)} - {ltd.ToPlaceDto.Place.Name.GetSeparatorTranslation(this.UserInterfaceLanguage, Language.Separator)}",
                Start = ltd.LogisticTransfer.Date,
                End = ltd.LogisticTransfer.Date.AddMinutes(30),
                AllDay = false,
                Css = "fc-event-solid-brand fc-event-light"
            }) ?? new List<ScheduleJsonDto>())?
            .ToList();

            return Json(new
            {
                status = "success",
                events
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Meetings

        /// <summary>Gets the audiovisual meetings data.</summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetAudiovisualMeetingsData(long? startDate, long? endDate)
        {
            if (DateTime.UtcNow < this.EditionDto.OneToOneMeetingsScheduleDate)
            {
                return Json(new { status = "success", events = new List<ScheduleJsonDto>() }, JsonRequestBehavior.AllowGet);
            }

            if (!startDate.HasValue || !endDate.HasValue)
            {
                return Json(new { status = "error", message = string.Format(Messages.TheFieldIsRequired, Labels.Date) }, JsonRequestBehavior.AllowGet);
            }

            var negotiationsDtos = await this.negotiationRepo.FindAllScheduleDtosAsync(
                this.EditionDto.Id,
                this.UserAccessControlDto?.EditionAttendeeCollaborator?.Id ?? 0,
                DateTimeOffset.FromUnixTimeSeconds(startDate.Value),
                DateTimeOffset.FromUnixTimeSeconds(endDate.Value));

            var events = negotiationsDtos?.Select(nd => new ScheduleJsonDto
            {
                Id = nd.Negotiation.Uid.ToString(),
                Type = "AudiovisualMeeting",
                Title = $"[{Labels.OneToOneMeeting}] {nd.ProjectBuyerEvaluationDto.ProjectDto.GetTitleDtoByLanguageCode(this.UserInterfaceLanguage).ProjectTitle.Value}",
                Start = nd.Negotiation.StartDate,
                End = nd.Negotiation.EndDate,
                AllDay = false,
                Css = "fc-event-solid-danger fc-event-light"
            });

            return Json(new
            {
                status = "success",
                events
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
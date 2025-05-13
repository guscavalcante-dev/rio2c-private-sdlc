// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 05-15-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 05-15-2024
// ***********************************************************************
// <copyright file="UpdateMusicBusinessRoundNegotiationCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Interfaces.Repositories;
using PlataformaRio2C.Domain.Interfaces.Repositories.Music.Projects;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>UpdateMusicBusinessRoundNegotiationCommandHandler</summary>
    public class UpdateMusicBusinessRoundNegotiationCommandHandler : MusicBusinesRoundNegotiationBaseCommandHandler, IRequestHandler<UpdateMusicBusinessRoundNegotiation, AppValidationResult>
    {
        private readonly IOrganizationRepository organizationRepo;
        private readonly IMusicBusinessRoundProjectRepository musicbusinesroundprojectRepo;
        private readonly INegotiationConfigRepository NegotiationRepo;
        private readonly INegotiationRoomConfigRepository negotiationRoomConfigRepo;
        private readonly IMusicBusinessRoundNegotiationRepository musicbusinessRoundnegotiationRepo;
        private readonly IConferenceRepository conferenceRepo;
        private readonly ILogisticAirfareRepository logisticAirfareRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateMusicBusinessRoundNegotiationCommandHandler"/> class.
        /// </summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="organizationRepository">The organization repository.</param>
        /// <param name="negotiationConfigRepository">The negotiation configuration repository.</param>
        /// <param name="negotiationRoomConfigRepository">The negotiation room configuration repository.</param>
        public UpdateMusicBusinessRoundNegotiationCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IOrganizationRepository organizationRepository,
            IMusicBusinessRoundProjectRepository musicbusinesroundprojectRepo,
            INegotiationConfigRepository negotiationConfigRepository,
            IMusicBusinessRoundNegotiationRepository musicbusinessroundnegotiationRepository,
            INegotiationRoomConfigRepository negotiationRoomConfigRepository,
            IConferenceRepository conferenceRepository,
            ILogisticAirfareRepository logisticsAirfareRepository)
            : base(eventBus, uow, musicbusinessroundnegotiationRepository)
        {
            this.organizationRepo = organizationRepository;
            this.musicbusinesroundprojectRepo = musicbusinesroundprojectRepo;
            this.NegotiationRepo = negotiationConfigRepository;
            this.musicbusinessRoundnegotiationRepo = musicbusinessroundnegotiationRepository;
            this.negotiationRoomConfigRepo = negotiationRoomConfigRepository;
            this.conferenceRepo = conferenceRepository;
            this.logisticAirfareRepo = logisticsAirfareRepository;
        }

        /// <summary>
        /// Handles the specified command.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(UpdateMusicBusinessRoundNegotiation cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var buyerOrganization = await this.organizationRepo.GetAsync(cmd.BuyerOrganizationUid ?? Guid.Empty);
            var project = await this.musicbusinesroundprojectRepo.GetAsync(cmd.ProjectUid ?? Guid.Empty);
            var negotiationConfig = await this.NegotiationRepo.GetAsync(cmd.NegotiationConfigUid ?? Guid.Empty);
            var negotiationRoomConfig = await this.negotiationRoomConfigRepo.GetAsync(cmd.NegotiationRoomConfigUid ?? Guid.Empty);
            var manualScheduledNegotiationsInThisRoom = await this.musicbusinessRoundnegotiationRepo.FindManualScheduledNegotiationsByRoomIdAsync(negotiationRoomConfig?.Room?.Id ?? 0);

            var startDatePreview = negotiationConfig.StartDate.Date.JoinDateAndTime(cmd.StartTime, true).ToUtcTimeZone();
            var endDatePreview = startDatePreview.Add(negotiationConfig.TimeOfEachRound);

            List<MusicBusinessRoundNegotiation> automaticScheduledNegotiationsInThisRoom = new List<MusicBusinessRoundNegotiation>();
            bool isUsingAutomaticTable = false;

            #region Overbooking Validations

            // Availability check
            DateTimeOffset startDate = negotiationConfig.StartDate.Date.JoinDateAndTime(cmd.StartTime, true).ToUtcTimeZone();
            DateTimeOffset endDate = startDate.Add(negotiationConfig.TimeOfEachRound);

            if (!((project.SellerAttendeeCollaborator.AvailabilityBeginDate == null && project.SellerAttendeeCollaborator.AvailabilityEndDate == null)
                || (project.SellerAttendeeCollaborator.AvailabilityBeginDate <= startDate && project.SellerAttendeeCollaborator.AvailabilityEndDate >= endDate)))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(
                                                    Messages.NoPlayerExecutivesAvailableForDate,
                                                    negotiationConfig.StartDate.ToShortDateString()),
                                                        new string[] { "ToastrError" }));
            }
            // Available tables check
            var manualNegotiationsGroupedByRoomAndStartDate = manualScheduledNegotiationsInThisRoom.GroupBy(n => n.StartDate);
            var hasNoMoreManualTablesAvailable = manualNegotiationsGroupedByRoomAndStartDate.Any(n => n.Count(w => w.StartDate == startDatePreview) >= negotiationRoomConfig.CountManualTables);
            if (hasNoMoreManualTablesAvailable)
            {
                // Has no more manual tables available, so, try to use slots available at automatic tables
                automaticScheduledNegotiationsInThisRoom = await this.musicbusinessRoundnegotiationRepo.FindAutomaticScheduledNegotiationsByRoomIdAsync(negotiationRoomConfig?.Room?.Id ?? 0);
                var automaticNegotiationsGroupedByRoomAndStartDate = automaticScheduledNegotiationsInThisRoom.GroupBy(n => n.StartDate);
                var hasNoMoreAutomaticTablesAvailable = automaticNegotiationsGroupedByRoomAndStartDate.Any(n => n.Count(w => w.StartDate == startDatePreview) >= negotiationRoomConfig.CountAutomaticTables);
                if (hasNoMoreAutomaticTablesAvailable)
                {
                    this.ValidationResult.Add(new ValidationError(string.Format(
                    Messages.NoMoreTablesAvailableAtTheRoomAndStartTime,
                    cmd.StartTime,
                    negotiationRoomConfig.Room.GetRoomNameByLanguageCode(cmd.UserInterfaceLanguage)),
                        new string[] { "ToastrError" }));
                }

                isUsingAutomaticTable = true;
            }

            // Negotiations checks
            var scheduledNegotiationsAtThisTime = await this.musicbusinessRoundnegotiationRepo.FindAllScheduledNegotiationsDtosAsync(cmd.EditionId.Value, null, startDatePreview, endDatePreview);

            var hasPlayerScheduledNegotiationsAtThisTime = scheduledNegotiationsAtThisTime.Count(ndto => ndto.ProjectBuyerEvaluationDto.BuyerAttendeeOrganizationDto.AttendeeOrganization.OrganizationId == buyerOrganization.Id) > 0;
            if (hasPlayerScheduledNegotiationsAtThisTime)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(
                    Messages.HasBusinessRoundScheduled,
                    Labels.TheM,
                    Labels.Player,
                    ($"{startDatePreview.ToBrazilTimeZone().ToStringHourMinute()} - {endDatePreview.ToBrazilTimeZone().ToShortTimeString()}")),
                        new string[] { "ToastrError" }));
            }

            var hasProducerScheduledNegotiationsAtThisTime = scheduledNegotiationsAtThisTime.Count(ndto => ndto.ProjectBuyerEvaluationDto.MusicBusinessRoundProjectDto.SellerAttendeeCollaboratorDto.AttendeeCollaborator.CollaboratorId == project.SellerAttendeeCollaborator.CollaboratorId) > 0;
            if (hasProducerScheduledNegotiationsAtThisTime)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(
                    Messages.HasBusinessRoundScheduled,
                    Labels.TheF,
                    Labels.Producer,
                    ($"{startDatePreview.ToBrazilTimeZone().ToStringHourMinute()} - {endDatePreview.ToBrazilTimeZone().ToShortTimeString()}")),
                        new string[] { "ToastrError" }));
            }

            #region [DISABLED] This Conferences and Airfares checks are disabled but its working perfectly! (Dont delete, can be used in future!)

            //// Conferences checks
            //var scheduledConferencesAtThisTime = await this.conferenceRepo.FindAllScheduleDtosAsync(cmd.EditionId.Value, 0, startDatePreview, endDatePreview, true, true);

            //var hasPlayerExecutivesScheduledConferencesAtThisTime = scheduledConferencesAtThisTime.Count(cdto => cdto.ConferenceParticipantDtos.Any(cpdto => cpdto.AttendeeCollaboratorDto.AttendeeOrganizationsDtos.Any(aodto => aodto.Organization.Id == buyerOrganization.Id))) > 0;
            //if (hasPlayerExecutivesScheduledConferencesAtThisTime)
            //{
            //    this.ValidationResult.Add(new ValidationError(string.Format(
            //        Messages.HasConferenceScheduled,
            //        Labels.TheF,
            //        Labels.Producer,
            //        ($"{startDatePreview.ToBrazilTimeZone().ToStringHourMinute()} - {endDatePreview.ToBrazilTimeZone().ToShortTimeString()}")),
            //            new string[] { "ToastrError" }));
            //}

            //var hasProducerExecutivesScheduledConferencesAtThisTime = scheduledConferencesAtThisTime.Count(cdto => cdto.ConferenceParticipantDtos.Any(cpdto => cpdto.AttendeeCollaboratorDto.AttendeeOrganizationsDtos.Any(aodto => aodto.Organization.Id == project.SellerAttendeeOrganization.OrganizationId))) > 0;
            //if (hasProducerExecutivesScheduledConferencesAtThisTime)
            //{
            //    this.ValidationResult.Add(new ValidationError(string.Format(
            //        Messages.HasConferenceScheduled,
            //        Labels.TheF,
            //        Labels.Producer,
            //        ($"{startDatePreview.ToBrazilTimeZone().ToStringHourMinute()} - {endDatePreview.ToBrazilTimeZone().ToShortTimeString()}")),
            //            new string[] { "ToastrError" }));
            //}

            //// Airfares checks
            //var scheduledLogisticAirfaresAtThisTime = await this.logisticAirfareRepo.FindAllScheduleDtosAsync(cmd.EditionId.Value, null, startDatePreview, endDatePreview);

            //var hasPlayerExecutivesScheduledAirfaresAtThisTime = scheduledLogisticAirfaresAtThisTime.Count(ladto => ladto.LogisticDto.AttendeeCollaboratorDto.AttendeeOrganizationsDtos.Any(aodto => aodto.Organization.Id == buyerOrganization.Id)) > 0;
            //if (hasPlayerExecutivesScheduledAirfaresAtThisTime)
            //{
            //    this.ValidationResult.Add(new ValidationError(string.Format(
            //        Messages.HasAirfareScheduled,
            //        Labels.TheF,
            //        Labels.Producer,
            //        ($"{startDatePreview.ToBrazilTimeZone().ToStringHourMinute()} - {endDatePreview.ToBrazilTimeZone().ToShortTimeString()}")),
            //            new string[] { "ToastrError" }));
            //}

            //var hasProducerExecutivesScheduledAirfaresAtThisTime = scheduledLogisticAirfaresAtThisTime.Count(ladto => ladto.LogisticDto.AttendeeCollaboratorDto.AttendeeOrganizationsDtos.Any(aodto => aodto.Organization.Id == project.SellerAttendeeOrganization.OrganizationId)) > 0;
            //if (hasProducerExecutivesScheduledAirfaresAtThisTime)
            //{
            //    this.ValidationResult.Add(new ValidationError(string.Format(
            //        Messages.HasAirfareScheduled,
            //        Labels.TheF,
            //        Labels.Producer,
            //        ($"{startDatePreview.ToBrazilTimeZone().ToStringHourMinute()} - {endDatePreview.ToBrazilTimeZone().ToShortTimeString()}")),
            //            new string[] { "ToastrError" }));
            //}

            #endregion

            #endregion

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }


            // Concat Manual and Automatic negotiations to use inside negotiation.Update();
            var negotiationsInThisRoomAndStartDate = manualScheduledNegotiationsInThisRoom
                                                        .Concat(automaticScheduledNegotiationsInThisRoom)
                                                        .Where(n => n.StartDate == startDatePreview)
                                                        .ToList();


            var negotiation = await this.GetNegotiationByUid(cmd.MusicRoundNegotiationUid);
            negotiation.Update(
                negotiationConfig,
                negotiationRoomConfig,
                negotiationsInThisRoomAndStartDate,
                cmd.StartTime,
                cmd.RoundNumber ?? 0,
                cmd.UserId,
                cmd.UserInterfaceLanguage,
                isUsingAutomaticTable);

            if (!negotiation.IsValid())
            {
                this.AppValidationResult.Add(negotiation.ValidationResult);
                return this.AppValidationResult;
            }

            this.musicbusinessRoundnegotiationRepo.Update(negotiation);
            this.Uow.SaveChanges();

            return this.AppValidationResult;
        }
    }
}
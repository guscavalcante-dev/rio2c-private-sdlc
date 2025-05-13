// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Ribeiro 
// Created          : 05-03-2025
//
// Last Modified By : Rafael Ribeiro 
// Last Modified On : 05-03-2025
// ***********************************************************************
// <copyright file="CreateMusicBusinesRoundNegotiationCommandHandler.cs" company="Softo">
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
    /// <summary>CreateMusicBusinesRoundNegotiationCommandHandler</summary>
    public class CreateMusicBusinesRoundNegotiationCommandHandler : BaseCommandHandler, IRequestHandler<CreateMusicBusinessRoundNegotiation, AppValidationResult>
    {
        private readonly IOrganizationRepository organizationRepo;
        private readonly IMusicBusinessRoundProjectRepository musicbusinesroundprojectRepo;
        private readonly INegotiationConfigRepository negotiationConfigRepo;
        private readonly INegotiationRoomConfigRepository negotiationRoomConfigRepo;
        private readonly IMusicBusinessRoundNegotiationRepository MusicBusinessRoundNegotiationRepo;

        public CreateMusicBusinesRoundNegotiationCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IOrganizationRepository organizationRepository,
            IMusicBusinessRoundNegotiationRepository musicbusinessroundnegotiationRepository,
            IMusicBusinessRoundProjectRepository musicbusinesroundprojectRepo,
            INegotiationConfigRepository negotiationConfigRepository,
            INegotiationRoomConfigRepository negotiationRoomConfigRepository,
            IConferenceRepository conferenceRepository,
            ILogisticAirfareRepository logisticAirfareRepository)
            : base(eventBus, uow)
        {
            this.MusicBusinessRoundNegotiationRepo = musicbusinessroundnegotiationRepository;
            this.organizationRepo = organizationRepository;
            this.musicbusinesroundprojectRepo = musicbusinesroundprojectRepo;
            this.negotiationConfigRepo = negotiationConfigRepository;
            this.negotiationRoomConfigRepo = negotiationRoomConfigRepository;
        }

        /// <summary>Handles the specified create negotiation.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreateMusicBusinessRoundNegotiation cmd, CancellationToken cancellationToken)
        {

            this.Uow.BeginTransaction();

            var buyerOrganization = await this.organizationRepo.GetAsync(cmd.BuyerOrganizationUid ?? Guid.Empty);
            var project = await this.musicbusinesroundprojectRepo.GetAsync(cmd.ProjectUid ?? Guid.Empty);
            var negotiationConfig = await this.negotiationConfigRepo.GetAsync(cmd.NegotiationConfigUid ?? Guid.Empty);
            var negotiationRoomConfig = await this.negotiationRoomConfigRepo.GetAsync(cmd.NegotiationRoomConfigUid ?? Guid.Empty);
            var manualScheduledNegotiationsInThisRoom = await this.MusicBusinessRoundNegotiationRepo.FindManualScheduledNegotiationsByRoomIdAsync(negotiationRoomConfig?.Room?.Id ?? 0);

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
                automaticScheduledNegotiationsInThisRoom = await this.MusicBusinessRoundNegotiationRepo.FindAutomaticScheduledNegotiationsByRoomIdAsync(negotiationRoomConfig?.Room?.Id ?? 0);
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
            var scheduledNegotiationsAtThisTime = await this.MusicBusinessRoundNegotiationRepo.FindAllScheduledNegotiationsDtosAsync(cmd.EditionId.Value, null, startDatePreview, endDatePreview);

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


            var hasProducerScheduledNegotiationsAtThisTime = scheduledNegotiationsAtThisTime.Count(ndto => ndto.ProjectBuyerEvaluationDto.MusicBusinessRoundProjectDto.SellerAttendeeCollaboratorDto.Collaborator.Id == project.SellerAttendeeCollaboratorId) > 0;
            if (hasProducerScheduledNegotiationsAtThisTime)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(
                    Messages.HasBusinessRoundScheduled,
                    Labels.TheF,
                    Labels.Producer,
                    ($"{startDatePreview.ToBrazilTimeZone().ToStringHourMinute()} - {endDatePreview.ToBrazilTimeZone().ToShortTimeString()}")),
                    new string[] { "ToastrError" }));
            }
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

            //Update command properties to return to form before throws any ValidationError
            cmd.InitialProjectUid = project.Uid;
            if (hasProducerScheduledNegotiationsAtThisTime)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(
                    Messages.HasBusinessRoundScheduled,
                    Labels.TheF,
                    Labels.Producer,
                    ($"{startDatePreview.ToBrazilTimeZone().ToStringHourMinute()} - {endDatePreview.ToBrazilTimeZone().ToShortTimeString()}")),
                        new string[] { "ToastrError" }));
            }
            cmd.InitialBuyerOrganizationUid = buyerOrganization.Uid;
            cmd.InitialBuyerOrganizationName = buyerOrganization.CompanyName;

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            // Concat Manual and Automatic negotiations to use inside negotiation.Update();
            if (hasProducerScheduledNegotiationsAtThisTime)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(
                    Messages.HasBusinessRoundScheduled,
                    Labels.TheF,
                    Labels.Producer,
                    ($"{startDatePreview.ToBrazilTimeZone().ToStringHourMinute()} - {endDatePreview.ToBrazilTimeZone().ToShortTimeString()}")),
                    new string[] { "ToastrError" }));
            }
            var negotiationsInThisRoomAndStartDate = manualScheduledNegotiationsInThisRoom
                                                                    .Cast<MusicBusinessRoundNegotiation>()
                                                                    .Concat(automaticScheduledNegotiationsInThisRoom)
                                                                    .Where(n => n.StartDate == startDatePreview)
                                                                    .ToList();


            var negotiation = new MusicBusinessRoundNegotiation(
            cmd.EditionId.Value,
            buyerOrganization,
            project,
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

            this.MusicBusinessRoundNegotiationRepo.Create(negotiation);
            this.Uow.SaveChanges();

            return this.AppValidationResult;
        }
    }
}
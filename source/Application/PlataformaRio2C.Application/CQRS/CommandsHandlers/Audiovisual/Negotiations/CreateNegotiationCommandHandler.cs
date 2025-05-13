// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 03-08-2020
//
// Last Modified By : Daniel Giese Rodrigues
// Last Modified On : 05-09-2025
// ***********************************************************************
// <copyright file="CreateNegotiationCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Application.Interfaces;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>CreateNegotiationCommandHandler</summary>
    public class CreateNegotiationCommandHandler : NegotiationBaseCommandHandler, IRequestHandler<CreateNegotiation, AppValidationResult>
    {
        private readonly IOrganizationRepository organizationRepo;
        private readonly IProjectRepository projectRepo;
        private readonly INegotiationConfigRepository negotiationConfigRepo;
        private readonly INegotiationRoomConfigRepository negotiationRoomConfigRepo;
        private readonly IConferenceRepository conferenceRepo;
        private readonly INegotiationRepository negotiationRepo;
        private readonly ILogisticAirfareRepository logisticAirfareRepo;
        private readonly IProjectBuyerEvaluationRepository projectBuyerEvaluationRepo;
        private readonly INegotiationService negotiationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateNegotiationCommandHandler" /> class.
        /// </summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="negotiationRepository">The negotiation repository.</param>
        /// <param name="organizationRepository">The organization repository.</param>
        /// <param name="projectRepository">The project repository.</param>
        /// <param name="negotiationConfigRepository">The negotiation configuration repository.</param>
        /// <param name="negotiationRoomConfigRepository">The negotiation room configuration repository.</param>
        /// <param name="conferenceRepository">The conference repository.</param>
        /// <param name="logisticAirfareRepository">The logistic airfare repository.</param>
        /// <param name="projectBuyerEvaluationRepository">The project buyer evaluation repository.</param>
        /// <param name="attendeeOrganizationRepository">The attendee organization repository.</param>
        /// <param name="negotiationService">The negotiation service.</param>
        public CreateNegotiationCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            INegotiationRepository negotiationRepository,
            IOrganizationRepository organizationRepository,
            IProjectRepository projectRepository,
            INegotiationConfigRepository negotiationConfigRepository,
            INegotiationRoomConfigRepository negotiationRoomConfigRepository,
            IConferenceRepository conferenceRepository,
            ILogisticAirfareRepository logisticAirfareRepository,
            IProjectBuyerEvaluationRepository projectBuyerEvaluationRepository,
            IAttendeeOrganizationRepository attendeeOrganizationRepository,
            INegotiationService negotiationService)
            : base(eventBus, uow, negotiationRepository)
        {
            this.negotiationRepo = negotiationRepository;
            this.organizationRepo = organizationRepository;
            this.projectRepo = projectRepository;
            this.negotiationConfigRepo = negotiationConfigRepository;
            this.negotiationRoomConfigRepo = negotiationRoomConfigRepository;
            this.conferenceRepo = conferenceRepository;
            this.logisticAirfareRepo = logisticAirfareRepository;
            this.projectBuyerEvaluationRepo = projectBuyerEvaluationRepository;
            this.negotiationService = negotiationService;
        }

        /// <summary>Handles the specified create negotiation.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreateNegotiation cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var buyerOrganization = await this.organizationRepo.GetAsync(cmd.BuyerOrganizationUid ?? Guid.Empty);
            var project = await this.projectRepo.GetAsync(cmd.ProjectUid ?? Guid.Empty);
            var negotiationConfig = await this.negotiationConfigRepo.GetAsync(cmd.NegotiationConfigUid ?? Guid.Empty);
            var negotiationRoomConfig = await this.negotiationRoomConfigRepo.GetAsync(cmd.NegotiationRoomConfigUid ?? Guid.Empty);
            var manualScheduledNegotiationsInThisRoom = await this.NegotiationRepo.FindManualScheduledNegotiationsByRoomIdAsync(negotiationRoomConfig?.Room?.Id ?? 0);

            var startDatePreview = negotiationConfig.StartDate.Date.JoinDateAndTime(cmd.StartTime, true).ToUtcTimeZone();
            var endDatePreview = startDatePreview.Add(negotiationConfig.TimeOfEachRound);

            List<Negotiation> automaticScheduledNegotiationsInThisRoom = new List<Negotiation>();
            bool isUsingAutomaticTable = false;

            #region Initial Validations

            #region Executives Availability check

            var projectBuyerEvaluation = await this.projectBuyerEvaluationRepo.FindByProjectIdAndBuyerOrganizationUidAsync(
                project.Id,
                cmd.BuyerOrganizationUid ?? Guid.Empty,
                cmd.EditionId ?? 0);
            
            var executivesAvailabilities = negotiationService.GetExecutivesAvailabilities(projectBuyerEvaluation);

            // Player and Producer have Executives with Availability configured, but into different dates.
            // Ex.: Player Executive has Availability only for 29/05/2025 and Producer Executive has Availability only for 30/05/2025
            // so, we don't have availability to create a Negotiation for this match.
            bool hasConflictIntoExecutivesAvailabilities = executivesAvailabilities.Count == 0
                                                            && (negotiationService.GetPlayerExecutivesAvailabilities(projectBuyerEvaluation).Count > 0
                                                                || negotiationService.GetProducerExecutivesAvailabilities(projectBuyerEvaluation).Count > 0);

            var isStartDatePreviewIntoExecutivesAvailabilityRange = executivesAvailabilities.Count > 0
                                                                        && executivesAvailabilities.Any(ea => startDatePreview >= ea.AvailabilityBeginDate && endDatePreview <= ea.AvailabilityEndDate);

            if (hasConflictIntoExecutivesAvailabilities
                || (executivesAvailabilities.Count != 0 && !isStartDatePreviewIntoExecutivesAvailabilityRange))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(
                    Messages.NoPlayerExecutivesAvailableForDate,
                    startDatePreview.ToShortDateString()),
                        new string[] { "ToastrError" }));
            }

            #endregion

            #region Available manual tables check

            var manualNegotiationsGroupedByRoomAndStartDate = manualScheduledNegotiationsInThisRoom.GroupBy(n => n.StartDate);
            var hasNoMoreManualTablesAvailable = manualNegotiationsGroupedByRoomAndStartDate.Any(n => n.Count(w => w.StartDate == startDatePreview) >= negotiationRoomConfig.CountManualTables);
            if (hasNoMoreManualTablesAvailable)
            {
                // Has no more manual tables available, so, try to use slots available at automatic tables
                automaticScheduledNegotiationsInThisRoom = await this.NegotiationRepo.FindAutomaticScheduledNegotiationsByRoomIdAsync(negotiationRoomConfig?.Room?.Id ?? 0);
                var automaticNegotiationsGroupedByRoomAndStartDate = automaticScheduledNegotiationsInThisRoom.GroupBy(n => n.StartDate);
                var hasNoMoreAutomaticTablesAvailable = automaticNegotiationsGroupedByRoomAndStartDate.Any(n => n.Count(w => w.StartDate == startDatePreview) >= negotiationRoomConfig.CountAutomaticTables);
                if (hasNoMoreAutomaticTablesAvailable)
                {
                    this.ValidationResult.Add(new ValidationError(string.Format(
                    Messages.NoMoreTablesAvailableAtTheRoomAndStartTime,
                    cmd.StartTime,
                    negotiationRoomConfig.Room.GetRoomNameByLanguageCode(cmd.UserInterfaceLanguage)),
                        new string[] { "ToastrError" }));
                };

                isUsingAutomaticTable = true;
            }

            #endregion

            #region Overbooking check

            var scheduledNegotiationsAtThisTime = await this.NegotiationRepo.FindAllScheduledNegotiationsDtosAsync(
                cmd.EditionId.Value, 
                null, 
                startDatePreview, 
                endDatePreview);

            // Player scheduled Negotiations checks
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

            // Producer scheduled Negotiations checks
            var hasProducerScheduledNegotiationsAtThisTime = scheduledNegotiationsAtThisTime.Count(ndto => ndto.ProjectBuyerEvaluationDto.ProjectDto.SellerAttendeeOrganizationDto.AttendeeOrganization.OrganizationId == project.SellerAttendeeOrganization.OrganizationId) > 0;
            if (hasProducerScheduledNegotiationsAtThisTime)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(
                    Messages.HasBusinessRoundScheduled,
                    Labels.TheF,
                    Labels.Producer,
                    ($"{startDatePreview.ToBrazilTimeZone().ToStringHourMinute()} - {endDatePreview.ToBrazilTimeZone().ToShortTimeString()}")),
                        new string[] { "ToastrError" }));
            }

            #endregion

            #region Conferences check [DISABLED] - But stills working perfectly! Dont delete, can be used in future!

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

            #endregion

            #region Airfares check [DISABLED] - But stills working perfectly! Dont delete, can be used in future!
            
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

            // Update command properties to return to form before throws any ValidationError
            cmd.InitialProjectUid = project.Uid;
            cmd.InitialProjectName = project.GetTitleByLanguageCode(cmd.UserInterfaceLanguage);
            cmd.InitialBuyerOrganizationUid = buyerOrganization.Uid;
            cmd.InitialBuyerOrganizationName = buyerOrganization.CompanyName;
            cmd.SellerOrganizationUid = project.SellerAttendeeOrganization.Organization.Uid;

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            // Concat Manual and Automatic negotiations to use inside negotiation.Update();
            var negotiationsInThisRoomAndStartDate = manualScheduledNegotiationsInThisRoom
                                                        .Concat(automaticScheduledNegotiationsInThisRoom)
                                                        .Where(n => n.StartDate == startDatePreview)
                                                        .ToList();

            var negotiation = new Negotiation(
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

            this.NegotiationRepo.Create(negotiation);
            this.Uow.SaveChanges();

            return this.AppValidationResult;
        }
    }
}
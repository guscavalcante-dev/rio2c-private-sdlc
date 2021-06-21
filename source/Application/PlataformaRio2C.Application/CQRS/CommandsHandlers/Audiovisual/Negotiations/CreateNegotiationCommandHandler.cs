// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 03-08-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-25-2020
// ***********************************************************************
// <copyright file="CreateNegotiationCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
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
        private readonly ILogisticAccommodationRepository logisticAccommodationRepo;
        private readonly ILogisticTransferRepository logisticTransferRepo;

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
            ILogisticAccommodationRepository logisticAccommodationRepository,
            ILogisticTransferRepository logisticTransferRepository)
            : base(eventBus, uow, negotiationRepository)
        {
            this.negotiationRepo = negotiationRepository;
            this.organizationRepo = organizationRepository;
            this.projectRepo = projectRepository;
            this.negotiationConfigRepo = negotiationConfigRepository;
            this.negotiationRoomConfigRepo = negotiationRoomConfigRepository;
            this.conferenceRepo = conferenceRepository;
            this.logisticAirfareRepo = logisticAirfareRepository;
            this.logisticAccommodationRepo = logisticAccommodationRepository;
            this.logisticTransferRepo = logisticTransferRepository;
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
            var negotiationsInThisRoom = await this.NegotiationRepo.FindManualScheduledNegotiationsByRoomIdAsync(negotiationRoomConfig?.Room?.Id ?? 0);

            var startDatePreview = negotiationConfig.StartDate.Date.JoinDateAndTime(cmd.StartTime, true).ToUtcTimeZone();
            var endDatePreview = startDatePreview.Add(negotiationConfig.TimeOfEachRound);

            #region Overbooking Validations

            var negotiationsGroupedByRoomAndStartDate = negotiationsInThisRoom.GroupBy(n => n.StartDate);
            var hasNoMoreTablesAvailable = negotiationsGroupedByRoomAndStartDate.Any(n => n.Count(w => w.StartDate == startDatePreview) >= negotiationRoomConfig.CountManualTables);
            if (hasNoMoreTablesAvailable)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(
                    Messages.NoMoreTablesAvailableAtTheRoomAndStartTime,
                    cmd.StartTime,
                    negotiationRoomConfig.Room.GetRoomNameByLanguageCode(cmd.UserInterfaceLanguage)),
                        new string[] { "ToastrError" }));
            }

            var scheduledNegotiationsAtThisTime = await this.NegotiationRepo.FindAllScheduleDtosAsync(cmd.EditionId.Value, null, startDatePreview, endDatePreview);
            var hasPlayerScheduledNegotiationsAtThisTime = scheduledNegotiationsAtThisTime.Count(ndto => ndto.ProjectBuyerEvaluationDto.ProjectBuyerEvaluation.BuyerAttendeeOrganization.OrganizationId == buyerOrganization.Id) > 0;
            if (hasPlayerScheduledNegotiationsAtThisTime)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(
                    Messages.HasAlreadyBusinessRoundScheduled,
                    Labels.TheM,
                    Labels.Player,
                    ($"{startDatePreview.ToUserTimeZone().ToStringHourMinute()} - {endDatePreview.ToUserTimeZone().ToShortTimeString()}")),
                        new string[] { "ToastrError" }));
            }

            var hasProducerScheduledNegotiationsAtThisTime = scheduledNegotiationsAtThisTime.Count(ndto => ndto.ProjectBuyerEvaluationDto.ProjectBuyerEvaluation.Project.SellerAttendeeOrganization.OrganizationId == project.SellerAttendeeOrganization.OrganizationId) > 0;
            if (hasProducerScheduledNegotiationsAtThisTime)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(
                    Messages.HasAlreadyBusinessRoundScheduled,
                    Labels.TheF,
                    Labels.Producer,
                    ($"{startDatePreview.ToUserTimeZone().ToStringHourMinute()} - {endDatePreview.ToUserTimeZone().ToShortTimeString()}")),
                        new string[] { "ToastrError" }));
            }

            #endregion

            //Update command properties to return to form before throws any ValidationError
            cmd.InitialProjectUid = project.Uid;
            cmd.InitialProjectName = project.GetTitleByLanguageCode(cmd.UserInterfaceLanguage);
            cmd.InitialBuyerOrganizationUid = buyerOrganization.Uid;
            cmd.InitialBuyerOrganizationName = buyerOrganization.CompanyName;

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            var negotiationsInThisRoomAndStartDate = negotiationsInThisRoom.Where(n => n.StartDate == startDatePreview).ToList();

            var negotiationUid = Guid.NewGuid();
            var negotiation = new Negotiation(
                cmd.EditionId.Value,
                negotiationUid,
                buyerOrganization,
                project,
                negotiationConfig,
                negotiationRoomConfig,
                negotiationsInThisRoomAndStartDate,
                cmd.StartTime,
                cmd.RoundNumber ?? 0,
                cmd.UserId);
            if (!negotiation.IsValid())
            {
                this.AppValidationResult.Add(negotiation.ValidationResult);
                return this.AppValidationResult;
            }

            this.NegotiationRepo.Create(negotiation);
            this.Uow.SaveChanges();

            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}
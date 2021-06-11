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

        public CreateNegotiationCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            INegotiationRepository negotiationRepository,
            IOrganizationRepository organizationRepository,
            IProjectRepository projectRepository,
            INegotiationConfigRepository negotiationConfigRepository,
            INegotiationRoomConfigRepository negotiationRoomConfigRepository)
            : base(eventBus, uow, negotiationRepository)
        {
            this.organizationRepo = organizationRepository;
            this.projectRepo = projectRepository;
            this.negotiationConfigRepo = negotiationConfigRepository;
            this.negotiationRoomConfigRepo = negotiationRoomConfigRepository;
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

            //Update command properties
            cmd.InitialProjectUid = project.Uid;
            cmd.InitialProjectName = project.GetTitleByLanguageCode(cmd.UserInterfaceLanguage);
            cmd.InitialBuyerOrganizationUid = buyerOrganization.Uid;
            cmd.InitialBuyerOrganizationName = buyerOrganization.CompanyName;

            var startDatePreview = negotiationConfig.StartDate.Date.JoinDateAndTime(cmd.StartTime, true).ToUtcTimeZone();
            var negotiationsGroupedByRoomAndStartDate = negotiationsInThisRoom.GroupBy(n => n.StartDate.ToUserTimeZone());
            var hasNoMoreTablesAvailable = negotiationsGroupedByRoomAndStartDate.Any(n => n.Count(w => w.StartDate.ToUserTimeZone() == startDatePreview) >= negotiationRoomConfig.CountManualTables);
            if (hasNoMoreTablesAvailable)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.NoMoreTablesAvailableAtTheRoomAndStartTime, cmd.StartTime, negotiationRoomConfig.Room.GetRoomNameByLanguageCode(cmd.UserInterfaceLanguage)), new string[] { "ToastrError" }));
            }

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            var negotiationsAtThisRoomAndStartDate = negotiationsInThisRoom.Where(n => n.StartDate.ToUserTimeZone() == startDatePreview).ToList();

            var negotiationUid = Guid.NewGuid();
            var negotiation = new Negotiation(
                cmd.EditionId.Value,
                negotiationUid,
                buyerOrganization, 
                project, 
                negotiationConfig, 
                negotiationRoomConfig,
                negotiationsAtThisRoomAndStartDate,
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
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
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
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

            var negotiationUid = Guid.NewGuid();
            var negotiation = new Negotiation(
                negotiationUid,
                buyerOrganization, 
                project, 
                negotiationConfig, 
                negotiationRoomConfig, 
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
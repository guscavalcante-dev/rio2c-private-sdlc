// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 03-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-04-2020
// ***********************************************************************
// <copyright file="CreateNegotiationConfigCommandHandler.cs" company="Softo">
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
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>CreateNegotiationConfigCommandHandler</summary>
    public class CreateNegotiationConfigCommandHandler : NegotiationConfigBaseCommandHandler, IRequestHandler<CreateNegotiationConfig, AppValidationResult>
    {
        private readonly IEditionRepository editionRepo;

        /// <summary>Initializes a new instance of the <see cref="CreateNegotiationConfigCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="negotiationConfigRepository">The negotiation configuration repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        public CreateNegotiationConfigCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            INegotiationConfigRepository negotiationConfigRepository,
            IEditionRepository editionRepository)
            : base(eventBus, uow, negotiationConfigRepository)
        {
            this.editionRepo = editionRepository;
        }

        /// <summary>Handles the specified create negotiation configuration.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreateNegotiationConfig cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var negotiationConfigUid = Guid.NewGuid();

            var negotiationConfig = new NegotiationConfig(
                negotiationConfigUid,
                await this.editionRepo.GetAsync(cmd.EditionUid ?? Guid.Empty),
                cmd.Date.Value,
                cmd.StartTime,
                cmd.EndTime,
                cmd.RoundFirstTurn.Value,
                cmd.RoundSecondTurn.Value,
                cmd.TimeIntervalBetweenTurn,
                cmd.TimeOfEachRound,
                cmd.TimeIntervalBetweenRound,
                cmd.UserId);
            if (!negotiationConfig.IsValid())
            {
                this.AppValidationResult.Add(negotiationConfig.ValidationResult);
                return this.AppValidationResult;
            }

            this.NegotiationConfigRepo.Create(negotiationConfig);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = negotiationConfig;

            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}
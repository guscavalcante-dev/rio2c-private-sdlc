// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 03-05-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-05-2020
// ***********************************************************************
// <copyright file="CreateNegotiationRoomConfigCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>CreateNegotiationRoomConfigCommandHandler</summary>
    public class CreateNegotiationRoomConfigCommandHandler : NegotiationConfigBaseCommandHandler, IRequestHandler<CreateNegotiationRoomConfig, AppValidationResult>
    {
        private readonly IRoomRepository roomRepo;

        /// <summary>Initializes a new instance of the <see cref="CreateNegotiationRoomConfigCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="negotiationConfigRepository">The negotiation configuration repository.</param>
        /// <param name="roomRepository">The room repository.</param>
        public CreateNegotiationRoomConfigCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            INegotiationConfigRepository negotiationConfigRepository,
            IRoomRepository roomRepository)
            : base(eventBus, uow, negotiationConfigRepository)
        {
            this.roomRepo = roomRepository;
        }

        /// <summary>Handles the specified create negotiation room configuration.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreateNegotiationRoomConfig cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var negotiationConfig = await this.GetNegotiationConfigByUid(cmd.NegotiationConfigUid);

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            negotiationConfig.CreateNegotiationRoomConfig(
                await this.roomRepo.FindByUidAsync(cmd.RoomUid ?? Guid.Empty),
                cmd.CountAutomaticTables.Value,
                cmd.CountManualTables.Value,
                cmd.UserId);
            if (!negotiationConfig.IsValid())
            {
                this.AppValidationResult.Add(negotiationConfig.ValidationResult);
                return this.AppValidationResult;
            }

            this.NegotiationConfigRepo.Update(negotiationConfig);
            this.Uow.SaveChanges();

            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}
// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 03-05-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-05-2020
// ***********************************************************************
// <copyright file="DeleteNegotiationRoomConfigCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>DeleteNegotiationRoomConfigCommandHandler</summary>
    public class DeleteNegotiationRoomConfigCommandHandler : NegotiationConfigBaseCommandHandler, IRequestHandler<DeleteNegotiationRoomConfig, AppValidationResult>
    {
        /// <summary>Initializes a new instance of the <see cref="DeleteNegotiationRoomConfigCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="negotiationConfigRepository">The negotiation configuration repository.</param>
        public DeleteNegotiationRoomConfigCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            INegotiationConfigRepository negotiationConfigRepository)
            : base(eventBus, uow, negotiationConfigRepository)
        {
        }

        /// <summary>Handles the specified delete negotiation room configuration.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(DeleteNegotiationRoomConfig cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var negotiationConfig = await this.GetNegotiationConfigByUid(cmd.NegotiationConfigUid ?? Guid.Empty);

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            negotiationConfig.DeleteNegotiationRoomConfig(
                cmd.NegotiationRoomConfigUid ?? Guid.Empty,
                cmd.UserId);
            if (!negotiationConfig.IsNegotiationRoomConfigValid())
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
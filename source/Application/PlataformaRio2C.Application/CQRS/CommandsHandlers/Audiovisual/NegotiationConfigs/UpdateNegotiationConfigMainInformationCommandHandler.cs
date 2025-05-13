// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 03-05-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-05-2020
// ***********************************************************************
// <copyright file="UpdateNegotiationConfigMainInformationCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>UpdateNegotiationConfigMainInformationCommandHandler</summary>
    public class UpdateNegotiationConfigMainInformationCommandHandler : NegotiationConfigBaseCommandHandler, IRequestHandler<UpdateNegotiationConfigMainInformation, AppValidationResult>
    {
        /// <summary>Initializes a new instance of the <see cref="UpdateNegotiationConfigMainInformationCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="negotiationConfigRepository">The negotiation configuration repository.</param>
        public UpdateNegotiationConfigMainInformationCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            INegotiationConfigRepository negotiationConfigRepository)
            : base(eventBus, uow, negotiationConfigRepository)
        {
        }

        /// <summary>Handles the specified update negotiation configuration main information.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(UpdateNegotiationConfigMainInformation cmd, CancellationToken cancellationToken)
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

            negotiationConfig.UpdateMainInformation(
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

            this.NegotiationConfigRepo.Update(negotiationConfig);
            this.Uow.SaveChanges();

            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}
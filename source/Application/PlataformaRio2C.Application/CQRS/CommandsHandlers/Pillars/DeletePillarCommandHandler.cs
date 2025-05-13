// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="DeletePillarCommandHandler.cs" company="Softo">
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
    /// <summary>DeletePillarCommandHandler</summary>
    public class DeletePillarCommandHandler : PillarBaseCommandHandler, IRequestHandler<DeletePillar, AppValidationResult>
    {
        /// <summary>Initializes a new instance of the <see cref="DeletePillarCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="pillarRepository">The pillar repository.</param>
        public DeletePillarCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IPillarRepository pillarRepository)
            : base(eventBus, uow, pillarRepository)
        {
        }

        /// <summary>Handles the specified delete pillar.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(DeletePillar cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var pillar = await this.GetPillarByUid(cmd.PillarUid);

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            pillar.Delete(cmd.UserId);
            if (!pillar.IsValid())
            {
                this.AppValidationResult.Add(pillar.ValidationResult);
                return this.AppValidationResult;
            }

            this.PillarRepo.Update(pillar);
            this.Uow.SaveChanges();

            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}
// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-09-2020
// ***********************************************************************
// <copyright file="CreateTrackCommandHandler.cs" company="Softo">
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
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>CreateLogisticsCommandHandler</summary>
    public class DeleteLogisticTransferCommandHandler : LogisticTransferBaseCommandHandler, IRequestHandler<DeleteLogisticTransfer, AppValidationResult>
    {
        /// <summary>Initializes a new instance of the <see cref="T:PlataformaRio2C.Application.CQRS.CommandsHandlers.CreateLogisticsCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="logisticsRepo">The logistics repo.</param>
        /// <param name="logisticSponsorRepo">The logistic sponsor repo.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="attendeeCollaboratorRepo">The attendee collaborator repo.</param>
        /// <param name="languageRepository">The language repository.</param>
        public DeleteLogisticTransferCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            ILogisticTransferRepository repository) : base(eventBus, uow, repository)
        {
        }

        /// <summary>Handles the specified create track.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(DeleteLogisticTransfer cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var entity = await this.GetByUid(cmd.Uid);

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            entity.Delete(cmd.UserId);
            if (!entity.IsValid())
            {
                this.AppValidationResult.Add(entity.ValidationResult);
                return this.AppValidationResult;
            }

            this.repository.Update(entity);
            this.Uow.SaveChanges();

            return this.AppValidationResult;
        }
    }
}
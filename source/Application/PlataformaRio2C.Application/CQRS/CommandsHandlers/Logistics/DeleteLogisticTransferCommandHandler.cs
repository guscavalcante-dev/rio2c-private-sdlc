// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-13-2020
// ***********************************************************************
// <copyright file="DeleteLogisticTransferCommandHandler.cs" company="Softo">
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
    /// <summary>DeleteLogisticTransferCommandHandler</summary>
    public class DeleteLogisticTransferCommandHandler : LogisticTransferBaseCommandHandler, IRequestHandler<DeleteLogisticTransfer, AppValidationResult>
    {
        /// <summary>Initializes a new instance of the <see cref="DeleteLogisticTransferCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="repository"></param>
        public DeleteLogisticTransferCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            ILogisticTransferRepository repository)
            : base(eventBus, uow, repository)
        {
        }

        /// <summary>Handles the specified delete logistic transfer.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(DeleteLogisticTransfer cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var logisticTransfer = await this.GetLogisticTransferByUid(cmd.Uid);

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            logisticTransfer.Delete(cmd.UserId);
            if (!logisticTransfer.IsValid())
            {
                this.AppValidationResult.Add(logisticTransfer.ValidationResult);
                return this.AppValidationResult;
            }

            this.LogisticTransferRepo.Update(logisticTransfer);
            this.Uow.SaveChanges();

            return this.AppValidationResult;
        }
    }
}
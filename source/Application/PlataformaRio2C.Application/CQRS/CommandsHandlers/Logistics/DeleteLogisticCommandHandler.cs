// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-13-2020
// ***********************************************************************
// <copyright file="DeleteLogisticCommandHandler.cs" company="Softo">
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
    /// <summary>DeleteLogisticCommandHandler</summary>
    public class DeleteLogisticCommandHandler : LogisticsBaseCommandHandler, IRequestHandler<DeleteLogistic, AppValidationResult>
    {
        /// <summary>Initializes a new instance of the <see cref="DeleteLogisticCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="logisticRepository">The logistic repository.</param>
        public DeleteLogisticCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            ILogisticRepository logisticRepository)
            : base(eventBus, uow, logisticRepository)
        {
        }

        /// <summary>Handles the specified delete logistic.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(DeleteLogistic cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var entity = await this.GetLogisticByUid(cmd.Uid);

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
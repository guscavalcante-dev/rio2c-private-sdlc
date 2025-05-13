// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-13-2020
// ***********************************************************************
// <copyright file="DeleteLogisticAirfareCommandHandler.cs" company="Softo">
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
    /// <summary>DeleteLogisticAirfareCommandHandler</summary>
    public class DeleteLogisticAirfareCommandHandler : LogisticAirfareBaseCommandHandler, IRequestHandler<DeleteLogisticAirfare, AppValidationResult>
    {
        /// <summary>Initializes a new instance of the <see cref="DeleteLogisticAirfareCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="repository"></param>
        public DeleteLogisticAirfareCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            ILogisticAirfareRepository repository)
            : base(eventBus, uow, repository)
        {
        }

        /// <summary>Handles the specified delete logistic airfare.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(DeleteLogisticAirfare cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var logisticAirfare = await this.GetLogisticAirfareByUid(cmd.Uid);

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            logisticAirfare.Delete(cmd.UserId);
            if (!logisticAirfare.IsValid())
            {
                this.AppValidationResult.Add(logisticAirfare.ValidationResult);
                return this.AppValidationResult;
            }

            this.LogisticAirfareRepo.Update(logisticAirfare);
            this.Uow.SaveChanges();

            return this.AppValidationResult;
        }
    }
}
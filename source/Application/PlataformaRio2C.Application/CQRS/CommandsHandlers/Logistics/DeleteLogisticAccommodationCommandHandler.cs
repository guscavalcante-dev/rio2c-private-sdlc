// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-13-2020
// ***********************************************************************
// <copyright file="DeleteLogisticAccommodationCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>DeleteLogisticAccommodationCommandHandler</summary>
    public class DeleteLogisticAccommodationCommandHandler : LogisticAccommodationBaseCommandHandler, IRequestHandler<DeleteLogisticAccommodation, AppValidationResult>
    {
        /// <summary>Initializes a new instance of the <see cref="DeleteLogisticAccommodationCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="repository"></param>
        public DeleteLogisticAccommodationCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            ILogisticAccommodationRepository repository) 
            : base(eventBus, uow, repository)
        {
        }

        /// <summary>Handles the specified delete logistic accommodation.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(DeleteLogisticAccommodation cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var logisticAccommodation = await this.GetLogisticAccommodationByUid(cmd.Uid);

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            logisticAccommodation.Delete(cmd.UserId);
            if (!logisticAccommodation.IsValid())
            {
                this.AppValidationResult.Add(logisticAccommodation.ValidationResult);
                return this.AppValidationResult;
            }

            this.LogisticAccommodationRepo.Update(logisticAccommodation);
            this.Uow.SaveChanges();

            return this.AppValidationResult;
        }
    }
}
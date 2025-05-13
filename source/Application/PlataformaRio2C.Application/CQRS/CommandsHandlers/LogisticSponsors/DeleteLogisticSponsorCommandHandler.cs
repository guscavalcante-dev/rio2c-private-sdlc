// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Arthur Souza
// Created          : 01-30-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-17-2020
// ***********************************************************************
// <copyright file="DeleteLogisticSponsorCommandHandler.cs" company="Softo">
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
    /// <summary>DeleteLogisticSponsorCommandHandler</summary>
    public class DeleteLogisticSponsorCommandHandler : LogisticSponsorBaseCommandHandler, IRequestHandler<DeleteLogisticSponsor, AppValidationResult>
    {
        private readonly IEditionRepository editionRepo;

        /// <summary>Initializes a new instance of the <see cref="DeleteLogisticSponsorCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="logisticSponsorRepository">The logistic sponsor repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        public DeleteLogisticSponsorCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            ILogisticSponsorRepository logisticSponsorRepository,
            IEditionRepository editionRepository)
            : base(eventBus, uow, logisticSponsorRepository)
        {
            this.editionRepo = editionRepository;
        }

        /// <summary>Handles the specified delete logistic sponsor.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(DeleteLogisticSponsor cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var logisticSponsor = await this.GetLogisticSponsorByUid(cmd.SponsorUid);

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            logisticSponsor.Delete(
                await this.editionRepo.GetAsync(cmd.EditionId ?? 0),
                cmd.UserId);

            if (!logisticSponsor.IsValid())
            {
                this.AppValidationResult.Add(logisticSponsor.ValidationResult);
                return this.AppValidationResult;
            }

            this.logisticSponsorRepo.Update(logisticSponsor);
            this.Uow.SaveChanges();

            return this.AppValidationResult;
        }
    }
}
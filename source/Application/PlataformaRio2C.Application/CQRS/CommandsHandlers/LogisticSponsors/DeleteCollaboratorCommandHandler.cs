// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Arthur Souza
// Created          : 01-30-2020
//
// Last Modified By : Arthur Souza
// Last Modified On : 01-30-2020
// ***********************************************************************
// <copyright file="DeleteLogisticSponsorCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2c.Infra.Data.FileRepository.Helpers;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Statics;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>DeleteLogisticSponsorCommandHandler</summary>
    public class DeleteLogisticSponsorCommandHandler : LogisticSponsorBaseCommandHandler, IRequestHandler<DeleteLogisticSponsors, AppValidationResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteLogisticSponsorCommandHandler"/> class.
        /// </summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="repository">The repository.</param>
        public DeleteLogisticSponsorCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            ILogisticSponsorRepository repository)
            : base(eventBus, uow, repository)
        {
        }

        /// <summary>
        /// Handles the specified command.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;AppValidationResult&gt;.</returns>
        public async Task<AppValidationResult> Handle(DeleteLogisticSponsors cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var sponsor = await this.GetByUid(cmd.SponsorUid);

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion
                        
            sponsor.Delete(cmd.UserId);

            if (!sponsor.IsValid())
            {
                this.AppValidationResult.Add(sponsor.ValidationResult);
                return this.AppValidationResult;
            }

            this.repository.Update(sponsor);
            this.Uow.SaveChanges();
            
            return this.AppValidationResult;
        }
    }
}
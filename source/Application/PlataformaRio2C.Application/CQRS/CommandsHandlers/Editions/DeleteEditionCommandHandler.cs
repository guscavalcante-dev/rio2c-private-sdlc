// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-06-2020
// ***********************************************************************
// <copyright file="DeleteEditionCommandHandler.cs" company="Softo">
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
    /// <summary>DeleteEditionCommandHandler</summary>
    public class DeleteEditionCommandHandler : EditionBaseCommandHandler, IRequestHandler<DeleteEdition, AppValidationResult>
    {
        /// <summary>Initializes a new instance of the <see cref="DeleteEditionCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="editionRepository">The edition repository.</param>
        public DeleteEditionCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IEditionRepository editionRepository)
            : base(eventBus, uow, editionRepository)
        {
        }


        /// <summary>
        /// Handles the specified command.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(DeleteEdition cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var edition = await this.GetEditionByUid(cmd.EditionUid);

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            edition.Delete(cmd.UserId);
            if (!edition.IsValid())
            {
                this.AppValidationResult.Add(edition.ValidationResult);
                return this.AppValidationResult;
            }

            this.EditionRepo.Update(edition);
            this.Uow.SaveChanges();

            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}
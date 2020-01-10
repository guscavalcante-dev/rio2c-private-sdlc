// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-06-2020
// ***********************************************************************
// <copyright file="DeleteEditionEventCommandHandler.cs" company="Softo">
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
    /// <summary>DeleteEditionEventCommandHandler</summary>
    public class DeleteEditionEventCommandHandler : EditionEventBaseCommandHandler, IRequestHandler<DeleteEditionEvent, AppValidationResult>
    {
        /// <summary>Initializes a new instance of the <see cref="DeleteEditionEventCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="editionEventRepository">The edition event repository.</param>
        public DeleteEditionEventCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IEditionEventRepository editionEventRepository)
            : base(eventBus, uow, editionEventRepository)
        {
        }

        /// <summary>Handles the specified delete edition event.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(DeleteEditionEvent cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var editionEvent = await this.GetEditionEventByUid(cmd.EditionEventUid);

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            editionEvent.Delete(cmd.UserId);
            if (!editionEvent.IsValid())
            {
                this.AppValidationResult.Add(editionEvent.ValidationResult);
                return this.AppValidationResult;
            }

            this.EditionEventRepo.Update(editionEvent);
            this.Uow.SaveChanges();

            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}
// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-06-2020
// ***********************************************************************
// <copyright file="CreateEditionEventCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>CreateEditionEventCommandHandler</summary>
    public class CreateEditionEventCommandHandler : EditionEventBaseCommandHandler, IRequestHandler<CreateEditionEvent, AppValidationResult>
    {
        private readonly IEditionRepository editionRepo;

        /// <summary>Initializes a new instance of the <see cref="CreateEditionEventCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="editionEventRepository">The edition event repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        public CreateEditionEventCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IEditionEventRepository editionEventRepository,
            IEditionRepository editionRepository)
            : base(eventBus, uow, editionEventRepository)
        {
            this.editionRepo = editionRepository;
        }

        /// <summary>Handles the specified create edition event.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreateEditionEvent cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var editionEventUid = Guid.NewGuid();

            var editionEvent = new EditionEvent(
                editionEventUid,
                await this.editionRepo.GetAsync(cmd.EditionId ?? 0),
                cmd.Name,
                cmd.StartDate.Value,
                cmd.EndDate.Value,
                cmd.UserId);
            if (!editionEvent.IsValid())
            {
                this.AppValidationResult.Add(editionEvent.ValidationResult);
                return this.AppValidationResult;
            }

            this.EditionEventRepo.Create(editionEvent);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = editionEvent;

            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}
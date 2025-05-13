// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-04-2020
// ***********************************************************************
// <copyright file="EditionEventBaseCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>EditionEventBaseCommandHandler</summary>
    public class EditionEventBaseCommandHandler : BaseCommandHandler
    {
        protected readonly IEditionEventRepository EditionEventRepo;

        /// <summary>Initializes a new instance of the <see cref="EditionEventBaseCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="editionEventRepository">The edition event repository.</param>
        public EditionEventBaseCommandHandler(IMediator eventBus, IUnitOfWork uow, IEditionEventRepository editionEventRepository)
            : base(eventBus, uow)
        {
            this.EditionEventRepo = editionEventRepository;
        }

        /// <summary>Gets the edition event by uid.</summary>
        /// <param name="editionEventUid">The edition event uid.</param>
        /// <returns></returns>
        public async Task<EditionEvent> GetEditionEventByUid(Guid editionEventUid)
        {
            var editionEvent = await this.EditionEventRepo.GetAsync(editionEventUid);
            if (editionEvent == null || editionEvent.IsDeleted)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Event, Labels.FoundM), new string[] { "ToastrError" }));
            }

            return editionEvent;
        }
    }
}
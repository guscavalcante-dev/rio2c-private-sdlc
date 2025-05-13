// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-04-2020
// ***********************************************************************
// <copyright file="EditionBaseCommandHandler.cs" company="Softo">
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
    /// <summary>EditiontBaseCommandHandler</summary>
    public class EditionBaseCommandHandler : BaseCommandHandler
    {
        protected readonly IEditionRepository EditionRepo;

        /// <summary>Initializes a new instance of the <see cref="EditionBaseCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="editionRepository">The edition event repository.</param>
        public EditionBaseCommandHandler(IMediator eventBus, IUnitOfWork uow, IEditionRepository editionRepository)
            : base(eventBus, uow)
        {
            this.EditionRepo = editionRepository;
        }

        /// <summary>Gets the edition by uid.</summary>
        /// <param name="editionUid">The edition uid.</param>
        /// <returns></returns>
        public async Task<Edition> GetEditionByUid(Guid editionUid)
        {
            var edition = await this.EditionRepo.GetAsync(editionUid);
            if (edition == null || edition.IsDeleted)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Edition, Labels.FoundM), new string[] { "ToastrError" }));
            }

            return edition;
        }
    }
}
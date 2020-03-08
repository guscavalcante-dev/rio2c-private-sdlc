// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="PillarBaseCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>PillarBaseCommandHandler</summary>
    public class LogisticAccommodationBaseCommandHandler : BaseCommandHandler
    {
        protected readonly ILogisticAccommodationRepository repository;

        /// <summary>Initializes a new instance of the <see cref="PillarBaseCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="pillarRepository">The pillar repository.</param>
        public LogisticAccommodationBaseCommandHandler(IMediator eventBus, IUnitOfWork uow, ILogisticAccommodationRepository repository)
            : base(eventBus, uow)
        {
            this.repository = repository;
        }

        /// <summary>Gets the pillar by uid.</summary>
        /// <param name="pillarUid">The pillar uid.</param>
        /// <returns></returns>
        public async Task<LogisticAccommodation> GetByUid(Guid uid)
        {
            var entity = await this.repository.GetAsync(uid);
            if (entity == null || entity.IsDeleted)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Logistics, Labels.FoundF), new string[] { "Airfare" }));
            }

            return entity;
        }
    }
}
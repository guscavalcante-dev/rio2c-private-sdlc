// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-04-2020
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
    public class PillarBaseCommandHandler : BaseCommandHandler
    {
        protected readonly IPillarRepository PillarRepo;

        /// <summary>Initializes a new instance of the <see cref="PillarBaseCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="pillarRepository">The pillar repository.</param>
        public PillarBaseCommandHandler(IMediator eventBus, IUnitOfWork uow, IPillarRepository pillarRepository)
            : base(eventBus, uow)
        {
            this.PillarRepo = pillarRepository;
        }

        /// <summary>Gets the pillar by uid.</summary>
        /// <param name="pillarUid">The pillar uid.</param>
        /// <returns></returns>
        public async Task<Pillar> GetPillarByUid(Guid pillarUid)
        {
            var pillar = await this.PillarRepo.GetAsync(pillarUid);
            if (pillar == null || pillar.IsDeleted)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Pillar, Labels.FoundF), new string[] { "ToastrError" }));
            }

            return pillar;
        }
    }
}
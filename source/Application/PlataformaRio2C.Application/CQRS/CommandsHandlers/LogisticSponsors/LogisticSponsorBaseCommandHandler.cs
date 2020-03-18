// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Arthur Souza
// Created          : 01-28-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-17-2020
// ***********************************************************************
// <copyright file="TrackBaseCommandHandler.cs" company="Softo">
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
    /// <summary>LogisticSponsorBaseCommandHandler</summary>
    public class LogisticSponsorBaseCommandHandler : BaseCommandHandler
    {
        protected readonly ILogisticSponsorRepository logisticSponsorRepo;

        /// <summary>Initializes a new instance of the <see cref="LogisticSponsorBaseCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="logisticSponsorRepository">The logistic sponsor repository.</param>
        public LogisticSponsorBaseCommandHandler(IMediator eventBus, IUnitOfWork uow, ILogisticSponsorRepository logisticSponsorRepository)
            : base(eventBus, uow)
        {
            this.logisticSponsorRepo = logisticSponsorRepository;
        }

        /// <summary>Gets the logistic sponsor by uid.</summary>
        /// <param name="sponsorUid">The sponsor uid.</param>
        /// <returns></returns>
        public async Task<LogisticSponsor> GetLogisticSponsorByUid(Guid sponsorUid)
        {
            var logisticSponsor = await this.logisticSponsorRepo.GetAsync(sponsorUid);
            if (logisticSponsor == null || logisticSponsor.IsDeleted)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Sponsor, Labels.FoundM), new string[] { "ToastrError" }));
            }

            return logisticSponsor;
        }
    }
}
﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 03-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-07-2020
// ***********************************************************************
// <copyright file="NegotiationBaseCommandHandler.cs" company="Softo">
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
    /// <summary>NegotiationBaseCommandHandler</summary>
    public class NegotiationBaseCommandHandler : BaseCommandHandler
    {
        protected readonly INegotiationRepository NegotiationRepo;

        /// <summary>Initializes a new instance of the <see cref="NegotiationBaseCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="negotiationRepository">The negotiation repository.</param>
        public NegotiationBaseCommandHandler(IMediator eventBus, IUnitOfWork uow, INegotiationRepository negotiationRepository)
            : base(eventBus, uow)
        {
            this.NegotiationRepo = negotiationRepository;
        }

        /// <summary>Gets the negotiation by uid.</summary>
        /// <param name="negotiationUid">The negotiation uid.</param>
        /// <returns></returns>
        public async Task<Negotiation> GetNegotiationByUid(Guid negotiationUid)
        {
            var negotiation = await this.NegotiationRepo.GetAsync(negotiationUid);
            if (negotiation == null || negotiation.IsDeleted)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.OneToOneMeetings, Labels.FoundM), new string[] { "ToastrError" }));
            }

            return negotiation;
        }
    }
}
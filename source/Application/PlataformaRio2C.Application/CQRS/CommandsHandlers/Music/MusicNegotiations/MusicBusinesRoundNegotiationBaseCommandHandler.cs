﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 03-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-07-2020
// ***********************************************************************
// <copyright file="MusicBusinesRoundNegotiationBaseCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces.Repositories;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>MusicBusinesRoundNegotiationBaseCommandHandler</summary>
    public class MusicBusinesRoundNegotiationBaseCommandHandler : BaseCommandHandler
    {
        protected readonly IMusicBusinessRoundNegotiationRepository musicBusinessRoundNegotiationRepo;


        /// <summary>Initializes a new instance of the <see cref="MusicBusinesRoundNegotiationBaseCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        public MusicBusinesRoundNegotiationBaseCommandHandler(IMediator eventBus, IUnitOfWork uow, IMusicBusinessRoundNegotiationRepository negotiationRepository)
            : base(eventBus, uow)
        {
            this.musicBusinessRoundNegotiationRepo = negotiationRepository;
        }

        /// <summary>Gets the negotiation by uid.</summary>
        /// <param name="negotiationUid">The negotiation uid.</param>
        /// <returns></returns>
        public async Task<MusicBusinessRoundNegotiation> GetNegotiationByUid(Guid negotiationUid)
        {
            var negotiation = await this.musicBusinessRoundNegotiationRepo.GetAsync(negotiationUid);
            if (negotiation == null || negotiation.IsDeleted)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.OneToOneMeetings, Labels.FoundM), new string[] { "ToastrError" }));
            }

            return negotiation;
        }
    }
}
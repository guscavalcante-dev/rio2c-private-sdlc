// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           :Daniel Giese
// Created          : 05/03/2025
//
// Last Modified By : Daniel Giese
// Last Modified On : 05/03/2025
// ***********************************************************************
// <copyright file="MusicBusinessRoundNegotiationBaseCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using MediatR;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Interfaces.Repositories;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    public class MusicBusinessRoundNegotiationBaseCommandHandler : BaseCommandHandler
    {
        protected readonly IMusicBusinessRoundNegotiationRepository MusicBusinessRoundNegotiationRepo;

        /// <summary>Initializes a new instance of the <see cref="MusicBusinessRoundNegotiationBaseCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="negotiationRepository">The negotiation repository.</param>
        public MusicBusinessRoundNegotiationBaseCommandHandler(IMediator eventBus, IUnitOfWork uow, IMusicBusinessRoundNegotiationRepository negotiationRepository)
            : base(eventBus, uow)
        {
            this.MusicBusinessRoundNegotiationRepo = negotiationRepository;
        }

        /// <summary>Gets the negotiation by uid.</summary>
        /// <param name="negotiationUid">The negotiation uid.</param>
        /// <returns></returns>
        public async Task<MusicBusinessRoundNegotiation> GetMusicBusinessRoundNegotiationByUid(Guid negotiationUid)
        {
            var negotiation = await this.MusicBusinessRoundNegotiationRepo.GetAsync(negotiationUid);
            if (negotiation == null || negotiation.IsDeleted)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.OneToOneMeetings, Labels.FoundM), new string[] { "ToastrError" }));
            }

            return negotiation;
        }
    }
}

// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Ribeiro 
// Created          : 05-03-2025
//
// Last Modified By : Rafael Ribeiro 
// Last Modified On : 05-03-2025
// ***********************************************************************
// <copyright file="NegotiationBaseCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces.Repositories;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>NegotiationBaseCommandHandler</summary>
    /// 
    public class MusicBusinessRoundNegotiationBaseCommandHandler : BaseCommandHandler
    {
        private IMusicBusinessRoundNegotiationRepository _musicbusinessRoundnegotiationRepo;

        /// <summary>Initializes a new instance of the <see cref="MusicBusinessRoundNegotiationBaseCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="negotiationRepository">The negotiation repository.</param>
        public MusicBusinessRoundNegotiationBaseCommandHandler(IMediator eventBus, IUnitOfWork uow, IMusicBusinessRoundNegotiationRepository musicBusinessRoundNegotiationRepository)
            : base(eventBus, uow)
        {
            _musicbusinessRoundnegotiationRepo = musicBusinessRoundNegotiationRepository;
        }

        /// <summary>Gets the negotiation by uid.</summary>
        /// <param name="negotiationUid">The negotiation uid.</param>
        /// <returns></returns>
        public async Task<MusicBusinessRoundNegotiation> GetNegotiationByUid(Guid negotiationUid)
        {
            var negotiation = await this._musicbusinessRoundnegotiationRepo.GetAsync(negotiationUid);
            if (negotiation == null || negotiation.IsDeleted)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.OneToOneMeetings, Labels.FoundM), new string[] { "ToastrError" }));
            }

            return negotiation;
        }
    }

}
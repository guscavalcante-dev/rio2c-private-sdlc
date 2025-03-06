// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 05-15-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 05-15-2021
// ***********************************************************************
// <copyright file="UpdateNegotiation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Linq;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateNegotiation</summary>
    public class UpdateMusicBusinessRoundNegotiation : MusicbusinessRoundnegotiationBaseCommand
    {
        public Guid MusicRoundNegotiationUid { get; set; }
        public MusicBusinessRoundNegotiationDto MusicBusinesNegotiationDto { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateMusicBusinessRoundNegotiation"/> class.
        /// </summary>
        /// <param name="negotiationDto">The negotiation dto.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        /// <exception cref="DomainException"></exception>
        public UpdateMusicBusinessRoundNegotiation(
            MusicBusinessRoundNegotiationDto MusicBusinessRoundNegotiationDto, 
            string userInterfaceLanguage)
        {
            if (MusicBusinessRoundNegotiationDto == null || MusicBusinessRoundNegotiationDto.Negotiation == null)
            {
                throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Negotiation, Labels.FoundM));
            }

            this.MusicRoundNegotiationUid = MusicBusinessRoundNegotiationDto.Negotiation.Uid;
            this.MusicBusinesNegotiationDto = MusicBusinessRoundNegotiationDto;
            this.UpdateBaseProperties(ProjectBuyerEvaluationDto, userInterfaceLanguage);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateMusicBusinessRoundNegotiation" /> class.
        /// </summary>
        public UpdateMusicBusinessRoundNegotiation()
        {
        }
    }
}
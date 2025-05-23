﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 05-15-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 05-15-2021
// ***********************************************************************
// <copyright file="UpdateMusicBusinessRoundNegotiation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateMusicBusinessRoundNegotiation</summary>
    public class UpdateMusicBusinessRoundNegotiation : MusicBusinessRoundNegotiationBaseCommand
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
            MusicBusinessRoundNegotiationDto musicBusinessRoundNegotiationDto,
            string userInterfaceLanguage)
        {
            if (musicBusinessRoundNegotiationDto == null || musicBusinessRoundNegotiationDto.Negotiation == null)
            {
                throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Negotiation, Labels.FoundM));
            }

            this.MusicRoundNegotiationUid = musicBusinessRoundNegotiationDto.Negotiation.Uid;
            this.MusicBusinesNegotiationDto = musicBusinessRoundNegotiationDto;
            this.UpdateBaseProperties(MusicBusinesNegotiationDto.ProjectBuyerEvaluationDto, userInterfaceLanguage);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateMusicBusinessRoundNegotiation" /> class.
        /// </summary>
        public UpdateMusicBusinessRoundNegotiation()
        {
        }
    }
}
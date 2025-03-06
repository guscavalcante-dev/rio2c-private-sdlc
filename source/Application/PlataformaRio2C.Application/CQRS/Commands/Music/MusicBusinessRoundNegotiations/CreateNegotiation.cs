// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Ribeiro 
// Created          : 05-03-2025
//
// Last Modified By : Rafael Ribeiro 
// Last Modified On : 05-03-2025
// ***********************************************************************
// <copyright file="CreateNegotiation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateNegotiation</summary>
    public class CreateMusicBusinessNegotiation : MusicbusinessRoundnegotiationBaseCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMusicBusinessNegotiation"/> class.
        /// </summary>
        /// <param name="MusicBusinessRoundProjectDto">The project buyer evaluation dto.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        /// <exception cref="DomainException"></exception>
        public CreateMusicBusinessNegotiation(
            MusicBusinessRoundProjectBuyerEvaluationDto MusicBusinessRoundProjectDto,
            string userInterfaceLanguage)
        {
            this.UpdateBaseProperties(MusicBusinessRoundProjectDto, userInterfaceLanguage);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMusicBusinessNegotiation"/> class.
        /// </summary>
        /// <param name="negotiationDto">The negotiation.</param>
        public CreateMusicBusinessNegotiation()
        {
        }
    }
}
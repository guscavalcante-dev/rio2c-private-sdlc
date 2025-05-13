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
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateNegotiation</summary>
    public class UpdateNegotiation : NegotiationBaseCommand
    {
        public Guid NegotiationUid { get; set; }
        public NegotiationDto NegotiationDto { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateNegotiation"/> class.
        /// </summary>
        /// <param name="negotiationDto">The negotiation dto.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        /// <exception cref="DomainException"></exception>
        public UpdateNegotiation(
            NegotiationDto negotiationDto,
            string userInterfaceLanguage)
        {
            if (negotiationDto == null || negotiationDto.Negotiation == null)
            {
                throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Negotiation, Labels.FoundM));
            }

            this.NegotiationUid = negotiationDto.Negotiation.Uid;
            this.NegotiationDto = negotiationDto;
            this.UpdateBaseProperties(negotiationDto.ProjectBuyerEvaluationDto, userInterfaceLanguage);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateNegotiation" /> class.
        /// </summary>
        public UpdateNegotiation()
        {
        }
    }
}
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
using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateNegotiation</summary>
    public class UpdateNegotiation : CreateNegotiation
    {
        public Guid NegotiationUid { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateNegotiation"/> class.
        /// </summary>
        /// <param name="negotiationDto">The negotiation.</param>
        public UpdateNegotiation(NegotiationDto negotiationDto) : base(negotiationDto)
        {
            this.NegotiationUid = negotiationDto?.Negotiation?.Uid ?? Guid.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateNegotiation" /> class.
        /// </summary>
        public UpdateNegotiation()
        {
        }
    }
}
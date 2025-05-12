// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 05-09-2025
//
// Last Modified By : Renan Valentim
// Last Modified On : 05-09-2025
// ***********************************************************************
// <copyright file="AgendaMusicNegotiationEventJsonDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AgendaMusicNegotiationEventJsonDto</summary>
    public class AgendaMusicNegotiationEventJsonDto : AgendaBaseEventJsonDto
    {
        public string Participant { get; set; }
        public string Player { get; set; }
        public string Room { get; set; }
        public int TableNumber { get; set; }
        public int RoundNumber { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AgendaMusicNegotiationEventJsonDto"/> class.</summary>
        public AgendaMusicNegotiationEventJsonDto()
        {
        }
    }
}
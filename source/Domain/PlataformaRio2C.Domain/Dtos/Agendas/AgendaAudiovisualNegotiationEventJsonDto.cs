// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 03-27-2020
//
// Last Modified By : Renan Valentim
// Last Modified On : 05-09-2025
// ***********************************************************************
// <copyright file="AgendaAudiovisualNegotiationEventJsonDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AgendaAudiovisualNegotiationEventJsonDto</summary>
    public class AgendaAudiovisualNegotiationEventJsonDto : AgendaBaseEventJsonDto
    {
        public string ProjectLogLine { get; set; }
        public string Producer { get; set; }
        public string Player { get; set; }
        public string Room { get; set; }
        public int TableNumber { get; set; }
        public int RoundNumber { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AgendaAudiovisualNegotiationEventJsonDto"/> class.</summary>
        public AgendaAudiovisualNegotiationEventJsonDto()
        {
        }
    }
}
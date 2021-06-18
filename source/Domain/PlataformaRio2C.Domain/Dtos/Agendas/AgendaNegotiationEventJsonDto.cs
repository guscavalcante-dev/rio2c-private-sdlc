// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 03-27-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-27-2020
// ***********************************************************************
// <copyright file="AgendaNegotiationEventJsonDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AgendaNegotiationEventJsonDto</summary>
    public class AgendaNegotiationEventJsonDto : AgendaBaseEventJsonDto
    {
        public string ProjectLogLine { get; set; }
        public string Producer { get; set; }
        public string Player { get; set; }
        public string Room { get; set; }
        public int TableNumber { get; set; }
        public int RoundNumber { get; set; }
        public string VirtualMeetingUrl { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AgendaNegotiationEventJsonDto"/> class.</summary>
        public AgendaNegotiationEventJsonDto()
        {
        }
    }
}
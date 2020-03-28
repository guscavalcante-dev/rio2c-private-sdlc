// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 03-01-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-27-2020
// ***********************************************************************
// <copyright file="AgendaLogisticAirfareEventJsonDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AgendaLogisticAirfareEventJsonDto</summary>
    public class AgendaLogisticAirfareEventJsonDto : AgendaBaseEventJsonDto
    {
        public string FlightType { get; set; }
        public string FromPlace { get; set; }
        public string ToPlace { get; set; }
        public string TicketNumber { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AgendaLogisticAirfareEventJsonDto"/> class.</summary>
        public AgendaLogisticAirfareEventJsonDto()
        {
        }
    }
}
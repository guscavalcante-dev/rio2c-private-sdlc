// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 03-27-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-27-2020
// ***********************************************************************
// <copyright file="AgendaLogisticAccommodationEventJsonDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AgendaLogisticAccommodationEventJsonDto</summary>
    public class AgendaLogisticAccommodationEventJsonDto : AgendaBaseEventJsonDto
    {
        public string SubType { get; set; }
        public DateTimeOffset CheckInDate { get; set; }
        public DateTimeOffset CheckOutDate { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AgendaLogisticAccommodationEventJsonDto"/> class.</summary>
        public AgendaLogisticAccommodationEventJsonDto()
        {
        }
    }
}
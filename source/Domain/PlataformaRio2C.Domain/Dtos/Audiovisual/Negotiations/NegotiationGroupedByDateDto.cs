// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 03-07-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-08-2020
// ***********************************************************************
// <copyright file="NegotiationGroupedByDateDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>NegotiationGroupedByDateDto</summary>
    public class NegotiationGroupedByDateDto
    {
        public DateTime Date { get; set; }
        public List<NegotiationGroupedByRoomDto> NegotiationGroupedByRoomDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="NegotiationGroupedByDateDto"/> class.</summary>
        /// <param name="date">The date.</param>
        /// <param name="negotiations">The negotiations.</param>
        public NegotiationGroupedByDateDto(DateTime date, List<Negotiation> negotiations)
        {
            this.Date = date;
            this.NegotiationGroupedByRoomDtos = negotiations?
                                                    .GroupBy(n => n.Room)?
                                                    .OrderBy(n => n.Key.Id)
                                                    .Select(n => new NegotiationGroupedByRoomDto(n.Key, n.ToList()))?
                                                    .ToList();
        }

        /// <summary>Initializes a new instance of the <see cref="NegotiationGroupedByDateDto"/> class.</summary>
        public NegotiationGroupedByDateDto()
        {
        }
    }
}
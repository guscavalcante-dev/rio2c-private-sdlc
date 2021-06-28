// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 03-07-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-26-2021
// ***********************************************************************
// <copyright file="NegotiationGroupedByRoundDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>NegotiationGroupedByRoundDto</summary>
    public class NegotiationGroupedByRoundDto
    {
        public int RoundNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<Negotiation> Negotiations { get; set; }

        /// <summary>Initializes a new instance of the <see cref="NegotiationGroupedByRoundDto"/> class.</summary>
        /// <param name="roundNumber">The round number.</param>
        /// <param name="negotiations">The negotiations.</param>
        public NegotiationGroupedByRoundDto(int roundNumber, List<Negotiation> negotiations)
        {
            this.RoundNumber = roundNumber;
            this.StartDate = negotiations?.FirstOrDefault()?.StartDate.ToBrazilTimeZone();
            this.EndDate = negotiations?.FirstOrDefault()?.EndDate.ToBrazilTimeZone();
            this.Negotiations = negotiations?.OrderBy(n => n.TableNumber)?.ToList();
        }

        /// <summary>Initializes a new instance of the <see cref="NegotiationGroupedByRoundDto"/> class.</summary>
        public NegotiationGroupedByRoundDto()
        {
        }
    }
}
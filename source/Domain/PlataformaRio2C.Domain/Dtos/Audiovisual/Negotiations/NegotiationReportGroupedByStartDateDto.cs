// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 03-30-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-26-2021
// ***********************************************************************
// <copyright file="NegotiationReportGroupedByStartDateDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>NegotiationReportGroupedByStartDateDto</summary>
    public class NegotiationReportGroupedByStartDateDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        //public int? TableNumber { get; set; }
        public List<Negotiation> Negotiations { get; set; }

        /// <summary>Initializes a new instance of the <see cref="NegotiationReportGroupedByStartDateDto"/> class.</summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="negotiations">The negotiations.</param>
        public NegotiationReportGroupedByStartDateDto(DateTimeOffset startDate, DateTimeOffset endDate, List<Negotiation> negotiations)
        {
            this.StartDate = startDate.ToBrazilTimeZone();
            this.EndDate = endDate.ToBrazilTimeZone();
            this.Negotiations = negotiations?.OrderBy(n => n.TableNumber)?.ToList();
        }

        /// <summary>Initializes a new instance of the <see cref="NegotiationReportGroupedByStartDateDto"/> class.</summary>
        public NegotiationReportGroupedByStartDateDto()
        {
        }
    }
}
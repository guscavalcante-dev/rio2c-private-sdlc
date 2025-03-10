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
using System;
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>NegotiationReportGroupedByStartDateDto</summary>
    public class MusicBusinessRoundNegotiationReportGroupedByStartDateDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        //public int? TableNumber { get; set; }
        public List<MusicBusinessRoundNegotiation> MusicBusinessRoundNegotiations { get; set; }

        /// <summary>Initializes a new instance of the <see cref="MusicBusinessRoundNegotiationReportGroupedByStartDateDto"/> class.</summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="negotiations">The negotiations.</param>
        public MusicBusinessRoundNegotiationReportGroupedByStartDateDto(DateTimeOffset startDate, DateTimeOffset endDate, List<MusicBusinessRoundNegotiation> negotiations)
        {
            this.StartDate = startDate.ToBrazilTimeZone();
            this.EndDate = endDate.ToBrazilTimeZone();
            this.MusicBusinessRoundNegotiations = negotiations?.OrderBy(n => n.TableNumber)?.ToList();
        }

        /// <summary>Initializes a new instance of the <see cref="MusicBusinessRoundNegotiationReportGroupedByStartDateDto"/> class.</summary>
        public MusicBusinessRoundNegotiationReportGroupedByStartDateDto()
        {
        }
    }
}
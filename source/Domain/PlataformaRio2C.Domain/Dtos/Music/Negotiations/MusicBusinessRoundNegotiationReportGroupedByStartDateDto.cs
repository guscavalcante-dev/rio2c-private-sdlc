// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Ribeiro
// Created          : 03-10-2025
//
// Last Modified By : Rafael Ribeiro
// Last Modified On : 06-10-2025
// ***********************************************************************
// <copyright file="MusicBusinessRoundNegotiationReportGroupedByStartDateDto.cs" company="Softo">
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
    /// <summary>MusicBusinessRoundNegotiationReportGroupedByStartDateDto</summary>
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
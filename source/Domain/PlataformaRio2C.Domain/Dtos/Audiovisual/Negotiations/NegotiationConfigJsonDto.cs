// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 03-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-04-2020
// ***********************************************************************
// <copyright file="NegotiationConfigJsonDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>NegotiationConfigJsonDto</summary>
    public class NegotiationConfigJsonDto
    {
        public int NegotiationConfigId { get; set; }
        public Guid NegotiationConfigUid { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public int RoundFirstTurn { get; set; }
        public int RoundSecondTurn { get; set; }
        public TimeSpan TimeIntervalBetweenTurn { get; set; }
        public TimeSpan TimeOfEachRound { get; set; }
        public TimeSpan TimeIntervalBetweenRound { get; set; }


        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }

        /// <summary>Initializes a new instance of the <see cref="NegotiationConfigJsonDto"/> class.</summary>
        public NegotiationConfigJsonDto()
        {
        }
    }
}
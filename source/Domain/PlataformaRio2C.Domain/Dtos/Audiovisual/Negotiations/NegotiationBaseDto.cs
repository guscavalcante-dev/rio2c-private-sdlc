// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 06-24-2021
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-24-2021
// ***********************************************************************
// <copyright file="NegotiationBaseDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>NegotiationBaseDto</summary>
    public class NegotiationBaseDto
    {
        public int Id { get; set; }
        public Guid Uid { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public int TableNumber { get; set; }
        public int RoundNumber { get; set; }
        public bool IsAutomatic { get; set; }

        public ProjectBuyerEvaluationBaseDto ProjectBuyerEvaluationBaseDto { get; set; }
        public RoomJsonDto RoomJsonDto { get; set; }

        /// <summary>Initializes a new instance of the <see cref="NegotiationBaseDto"/> class.</summary>
        public NegotiationBaseDto()
        {
        }
    }
}
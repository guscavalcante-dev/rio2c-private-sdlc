// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Daniel Giese Rodrigues
// Created          : 03-14-2025
//
// Last Modified By : Daniel Giese Rodrigues
// Last Modified On : 03-14-2025
// ***********************************************************************
// <copyright file="MusicBusinessRoundNegotiationBaseDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>MusicBusinessRoundNegotiationBaseDto</summary>
    public class MusicBusinessRoundNegotiationBaseDto
    {
        public int Id { get; set; }
        public Guid Uid { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public int TableNumber { get; set; }
        public int RoundNumber { get; set; }
        public bool IsAutomatic { get; set; }
        public MusicBusinessRoundProjectBuyerEvaluationBaseDto MusicBusinessProjectProjectBuyerEvaluationBaseDto { get; set; }
        public RoomJsonDto RoomJsonDto { get; set; }

        /// <summary>Initializes a new instance of the <see cref="MusicBusinessRoundNegotiationBaseDto"/> class.</summary>
        public MusicBusinessRoundNegotiationBaseDto()
        {
        }
    }
}
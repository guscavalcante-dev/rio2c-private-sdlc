// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 10-01-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-01-2024
// ***********************************************************************
// <copyright file="GetAudiovisualMaximumAvailableSlotsByEditionId.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Application.CQRS.Dtos;

namespace PlataformaRio2C.Application.CQRS.Queries
{
    /// <summary>GetAudiovisualMaximumAvailableSlotsByEditionId</summary>
    public class GetMusicMaximumAvailableSlotsByEditionId : BaseQuery<GetMusicMaximumAvailableSlotsByEditionIdResponseDto>
    {
        public int EditionId { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetMusicMaximumAvailableSlotsByEditionId" /> class.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        public GetMusicMaximumAvailableSlotsByEditionId(int editionId)
        {
            this.EditionId = editionId;
        }
    }
}
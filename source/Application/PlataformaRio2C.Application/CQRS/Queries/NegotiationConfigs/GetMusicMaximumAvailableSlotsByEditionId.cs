// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Ribeiro 
// Created          : 10-02-2025
//
// Last Modified By : Rafael Ribeiro 
// Last Modified On : 10-01-2025
// ***********************************************************************
// <copyright file="GetMusicMaximumAvailableSlotsByEditionId.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Application.CQRS.Dtos;

namespace PlataformaRio2C.Application.CQRS.Queries
{
    /// <summary>GetMusicMaximumAvailableSlotsByEditionId</summary>
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
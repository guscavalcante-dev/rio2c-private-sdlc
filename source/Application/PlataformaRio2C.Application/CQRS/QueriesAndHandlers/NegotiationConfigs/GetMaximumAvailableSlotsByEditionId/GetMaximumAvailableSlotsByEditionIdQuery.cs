// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 10-01-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-01-2024
// ***********************************************************************
// <copyright file="GetMaximumAvailableSlotsByEditionIdQuery.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace PlataformaRio2C.Application.CQRS.Queries
{
    /// <summary>GetMaximumAvailableSlotsByEditionIdQuery</summary>
    public class GetMaximumAvailableSlotsByEditionIdQuery : BaseQuery<GetMaximumAvailableSlotsByEditionIdDto>
    {
        public int EditionId { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetMaximumAvailableSlotsByEditionIdQuery" /> class.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        public GetMaximumAvailableSlotsByEditionIdQuery(int editionId)
        {
            this.EditionId = editionId;
        }
    }
}
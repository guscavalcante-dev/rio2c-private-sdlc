// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 10-24-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-24-2024
// ***********************************************************************
// <copyright file="MusicPitchingStatusWidgetDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Dtos
{
    public class MusicPitchingStatusWidgetDto
    {
        public int DistributedMembersCount { get; private set; }

        public bool HasDistributedMembers => this.DistributedMembersCount > 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicPitchingStatusWidgetDto"/> class.
        /// </summary>
        /// <param name="distributedMembersCount">The negotiation configs with presential room configured count.</param>
        public MusicPitchingStatusWidgetDto(
            int distributedMembersCount
        )
        {
            this.DistributedMembersCount = distributedMembersCount;
        }
    }
}

// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 03-25-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-26-2024
// ***********************************************************************
// <copyright file="TargetAudienceApiDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using PlataformaRio2C.Domain.Entities;
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>TargetAudienceApiDto</summary>
    public class TargetAudienceApiDto
    {
        [JsonRequired]
        [JsonProperty("uid")]
        public Guid Uid { get; set; }

        [JsonIgnore]
        public TargetAudience TargetAudience { get; set; }

        /// <summary>
        /// Converts to musicbandtargetaudience.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public MusicBandTargetAudience ToMusicBandTargetAudience(
            int userId)
        {
            return new MusicBandTargetAudience(
                this.TargetAudience,
                userId);
        }

        /// <summary>Initializes a new instance of the <see cref="TargetAudienceApiDto"/> class.</summary>
        public TargetAudienceApiDto()
        {
        }
    }
}
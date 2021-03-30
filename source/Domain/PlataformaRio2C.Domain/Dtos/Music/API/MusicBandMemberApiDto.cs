// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 03-24-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-24-2021
// ***********************************************************************
// <copyright file="MusicBandMemberApiDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>MusicBandMemberApiDto</summary>
    public class MusicBandMemberApiDto
    {
        [JsonRequired]
        [JsonProperty(PropertyName = "name", Order = 100)]
        public string Name { get; set; }

        [JsonRequired]
        [JsonProperty(PropertyName = "musicInstrumentName", Order = 200)]
        public string MusicInstrumentName { get; set; }

        /// <summary>Initializes a new instance of the <see cref="MusicBandMemberApiDto"/> class.</summary>
        public MusicBandMemberApiDto()
        {
        }
    }
}
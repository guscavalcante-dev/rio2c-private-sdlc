// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 3-24-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-26-2024
// ***********************************************************************
// <copyright file="MusicBandMemberApiDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using PlataformaRio2C.Domain.Entities;

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

        /// <summary>
        /// Converts to musicbandmember.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public MusicBandMember ToMusicBandMember(int userId)
        {
            return new MusicBandMember(
                this.Name, 
                this.MusicInstrumentName, 
                userId);
        }

        /// <summary>Initializes a new instance of the <see cref="MusicBandMemberApiDto"/> class.</summary>
        public MusicBandMemberApiDto()
        {
        }
    }
}
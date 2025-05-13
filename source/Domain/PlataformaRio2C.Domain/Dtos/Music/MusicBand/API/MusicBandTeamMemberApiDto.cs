// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 03-24-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-26-2024
// ***********************************************************************
// <copyright file="MusicBandTeamMemberApiDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>MusicBandTeamMemberApiDto</summary>
    public class MusicBandTeamMemberApiDto
    {
        [JsonRequired]
        [JsonProperty(PropertyName = "name", Order = 100)]
        public string Name { get; set; }

        [JsonRequired]
        [JsonProperty(PropertyName = "role", Order = 200)]
        public string Role { get; set; }

        /// <summary>
        /// Converts to musicbandteammember.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public MusicBandTeamMember ToMusicBandTeamMember(int userId)
        {
            return new MusicBandTeamMember(
                this.Name,
                this.Role,
                userId);
        }

        /// <summary>Initializes a new instance of the <see cref="MusicBandTeamMemberApiDto"/> class.</summary>
        public MusicBandTeamMemberApiDto()
        {
        }
    }
}
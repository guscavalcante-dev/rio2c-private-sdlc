// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 03-24-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-26-2024
// ***********************************************************************
// <copyright file="ReleasedMusicProjectApiDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>ReleasedMusicProjectApiDto</summary>
    public class ReleasedMusicProjectApiDto
    {
        [JsonRequired]
        [JsonProperty(PropertyName = "name", Order = 100)]
        public string Name { get; set; }

        [JsonRequired]
        [JsonProperty(PropertyName = "year", Order = 200)]
        public string Year { get; set; }

        /// <summary>
        /// Converts to releasedmusicproject.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public ReleasedMusicProject ToReleasedMusicProject(int userId)
        {
            return new ReleasedMusicProject(
                this.Name,
                this.Year,
                userId);
        }

        /// <summary>Initializes a new instance of the <see cref="ReleasedMusicProjectApiDto"/> class.</summary>
        public ReleasedMusicProjectApiDto()
        {
        }
    }
}
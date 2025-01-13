// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 26-03-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-26-2024
// ***********************************************************************
// <copyright file="MusicProjectApiDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>MusicProjectApiDto</summary>
    public class MusicProjectApiDto
    {
        [JsonProperty(PropertyName = "videoUrl", Order = 100)]
        public string VideoUrl { get; set; }

        [JsonProperty(PropertyName = "videoUrlPassword", Order = 101)]
        public string VideoUrlPassword { get; set; }

        [JsonProperty(PropertyName = "music1Url", Order = 200)]
        public string Music1Url { get; set; }

        [JsonProperty(PropertyName = "music2Url", Order = 300)]
        public string Music2Url { get; set; }

        [JsonProperty(PropertyName = "clipping", Order = 400)]
        public string Clipping { get; set; }

        [JsonProperty(PropertyName = "release", Order = 700)]
        public string Release { get; set; }

        /// <summary>Initializes a new instance of the <see cref="MusicProjectApiDto"/> class.</summary>
        public MusicProjectApiDto()
        {
        }

        /// <summary>
        /// Converts to musicproject.
        /// </summary>
        /// <returns></returns>
        public MusicProject ToMusicProject(int userId)
        {
            return new MusicProject(
                this.VideoUrl,
                this.VideoUrlPassword,
                this.Music1Url,
                this.Music2Url,
                this.Clipping,
                null,
                null,
                this.Release,
                userId);
        }
    }
}
// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 26-03-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 26-03-2021
// ***********************************************************************
// <copyright file="MusicProjectApiDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>MusicProjectApiDto</summary>
    public class MusicProjectApiDto
    {
        #region MusicProject properties

        [JsonIgnore]
        public int? AttendeeMusicBandId { get; set; }

        [JsonProperty(PropertyName = "videoUrl", Order = 100)]
        public string VideoUrl { get; set; }

        [JsonProperty(PropertyName = "music1Url", Order = 200)]
        public string Music1Url { get; set; }

        [JsonProperty(PropertyName = "music2Url", Order = 300)]
        public string Music2Url { get; set; }

        [JsonProperty(PropertyName = "clipping1", Order = 400)]
        public string Clipping1 { get; set; }

        [JsonProperty(PropertyName = "clipping2", Order = 500)]
        public string Clipping2 { get; set; }

        [JsonProperty(PropertyName = "clipping3", Order = 600)]
        public string Clipping3 { get; set; }

        [JsonProperty(PropertyName = "release", Order = 700)]
        public string Release { get; set; }

        #endregion

        /// <summary>Initializes a new instance of the <see cref="MusicBandApiDto"/> class.</summary>
        public MusicProjectApiDto()
        {
        }
    }
}
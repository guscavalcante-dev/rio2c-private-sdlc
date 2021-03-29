// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 03-24-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-24-2021
// ***********************************************************************
// <copyright file="ReleasedMusicProjectApiDto.cs" company="Softo">
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
    /// <summary>ReleasedMusicProjectApiDto</summary>
    public class ReleasedMusicProjectApiDto
    {
        [JsonIgnore]
        public int MusicBandId { get; set; }

        [JsonProperty(PropertyName = "name", Order = 100)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "year", Order = 200)]
        public string Year { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ReleasedMusicProjectApiDto"/> class.</summary>
        public ReleasedMusicProjectApiDto()
        {
        }
    }
}
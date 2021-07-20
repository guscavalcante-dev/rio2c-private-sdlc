// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 03-25-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-25-2021
// ***********************************************************************
// <copyright file="MusicGenreApiDto.cs" company="Softo">
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
    /// <summary>MusicGenreApiDto</summary>
    public class MusicGenreApiDto
    {
        [JsonRequired]
        [JsonProperty("uid")]
        public Guid Uid { get; set; }

        [JsonIgnore]
        public MusicGenre MusicGenre { get; set; }

        /// <summary>Initializes a new instance of the <see cref="MusicGenreApiDto"/> class.</summary>
        public MusicGenreApiDto()
        {
        }
    }
}
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
        public string VideoUrl { get; set; }
        public string Music1Url { get; set; }
        public string Music2Url { get; set; }
        public string Clipping1 { get; set; }
        public string Clipping2 { get; set; }
        public string Clipping3 { get; set; }
        public string Release { get; set; }

        #endregion

        /// <summary>Initializes a new instance of the <see cref="MusicBandApiDto"/> class.</summary>
        public MusicProjectApiDto()
        {
        }
    }
}
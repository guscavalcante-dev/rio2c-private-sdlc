// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 03-25-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-25-2021
// ***********************************************************************
// <copyright file="TargetAudienceApiDto.cs" company="Softo">
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
    /// <summary>TargetAudienceApiDto</summary>
    public class TargetAudienceApiDto
    {
        [JsonRequired]
        [JsonProperty("uid")]
        public Guid Uid { get; set; }

        [JsonIgnore]
        public TargetAudience TargetAudience { get; set; }

        /// <summary>Initializes a new instance of the <see cref="TargetAudienceApiDto"/> class.</summary>
        public TargetAudienceApiDto()
        {
        }
    }
}
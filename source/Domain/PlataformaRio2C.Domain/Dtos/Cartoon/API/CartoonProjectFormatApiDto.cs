// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 01-25-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-25-2022
// ***********************************************************************
// <copyright file="CartoonProjectFormatApiDto.cs" company="Softo">
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
    /// <summary>CartoonProjectFormatApiDto</summary>
    public class CartoonProjectFormatApiDto
    {
        [JsonRequired]
        [JsonProperty("uid")]
        public Guid Uid { get; set; }

        [JsonProperty("additionalInfo")]
        public string AdditionalInfo { get; set; }

        [JsonIgnore]
        public CartoonProjectFormat CartoonProjectFormat { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CartoonProjectFormatApiDto"/> class.</summary>
        public CartoonProjectFormatApiDto()
        {
        }
    }
}
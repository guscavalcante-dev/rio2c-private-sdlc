// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-01-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-01-2021
// ***********************************************************************
// <copyright file="WorkDedicationApiDto.cs" company="Softo">
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
    /// <summary>WorkDedicationApiDto</summary>
    public class WorkDedicationApiDto
    {
        [JsonRequired]
        [JsonProperty("Uid")]
        public Guid Uid { get; set; }

        [JsonIgnore]
        public WorkDedication WorkDedication { get; set; }

        /// <summary>Initializes a new instance of the <see cref="WorkDedicationApiDto"/> class.</summary>
        public WorkDedicationApiDto()
        {
        }
    }
}
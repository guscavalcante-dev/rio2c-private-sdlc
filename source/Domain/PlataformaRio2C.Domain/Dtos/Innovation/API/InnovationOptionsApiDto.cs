// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 06-29-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 06-29-2021
// ***********************************************************************
// <copyright file="InnovationOptionsApiDto.cs" company="Softo">
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
    /// <summary>InnovationOptionsApiDto</summary>
    public class InnovationOptionsApiDto
    {
        [JsonRequired]
        public int Id { get; set; }

        [JsonIgnore]
        public InnovationOption InnovationOption { get; set; }

        /// <summary>Initializes a new instance of the <see cref="InnovationOptionsApiDto"/> class.</summary>
        public InnovationOptionsApiDto()
        {
        }
    }
}
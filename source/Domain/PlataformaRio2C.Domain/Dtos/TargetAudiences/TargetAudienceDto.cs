// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 01-26-2025
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-26-2025
// ***********************************************************************
// <copyright file="TargetAudienceDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>TargetAudienceDto</summary>
    public class TargetAudienceDto
    {
        public Guid Uid { get; set; }
        public string Name { get; set; }

        /// <summary>Initializes a new instance of the <see cref="TargetAudienceDto"/> class.</summary>
        public TargetAudienceDto()
        {
        }
    }
}
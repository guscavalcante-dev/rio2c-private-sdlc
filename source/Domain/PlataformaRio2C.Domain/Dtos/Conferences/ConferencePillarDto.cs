// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="ConferencePillarDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>ConferencePillarDto</summary>
    public class ConferencePillarDto
    {
        public ConferencePillar ConferencePillar { get; set; }
        public Pillar Pillar { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ConferencePillarDto"/> class.</summary>
        public ConferencePillarDto()
        {
        }
    }
}
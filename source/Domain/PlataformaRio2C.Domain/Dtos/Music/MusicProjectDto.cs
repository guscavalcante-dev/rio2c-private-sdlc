// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 02-26-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-28-2020
// ***********************************************************************
// <copyright file="MusicProjectDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>MusicProjectDto</summary>
    public class MusicProjectDto
    {
        public MusicProject MusicProject { get; set; }
        public AttendeeMusicBandDto AttendeeMusicBandDto { get; set; }
        public MusicProjectEvaluationDto MusicProjectEvaluationDto { get; set; }

        /// <summary>Initializes a new instance of the <see cref="MusicProjectDto"/> class.</summary>
        public MusicProjectDto()
        {
        }
    }
}
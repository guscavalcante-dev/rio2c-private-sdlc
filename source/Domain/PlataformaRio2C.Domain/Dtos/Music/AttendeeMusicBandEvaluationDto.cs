// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 04-02-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 04-02-2021
// ***********************************************************************
// <copyright file="AttendeeMusicBandEvaluationDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeMusicBandEvaluationDto</summary>
    public class AttendeeMusicBandEvaluationDto
    {
        public AttendeeMusicBandEvaluation AttendeeMusicBandEvaluation { get; set; }
        public User EvaluatorUser { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeMusicBandEvaluationDto"/> class.</summary>
        public AttendeeMusicBandEvaluationDto()
        {
        }
    }
}
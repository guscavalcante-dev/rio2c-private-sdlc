// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 08-06-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-06-2021
// ***********************************************************************
// <copyright file="MusicProjectEditionCountGaugeWidgetJsonDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>MusicProjectEditionCountGaugeWidgetJsonDto</summary>
    public class MusicProjectEditionCountGaugeWidgetJsonDto
    {
        public string MusicBandGenreName { get; set; }
        public int MusicBandGenreCount { get; set; }

        /// <summary>Initializes a new instance of the <see cref="MusicProjectEditionCountGaugeWidgetJsonDto"/> class.</summary>
        public MusicProjectEditionCountGaugeWidgetJsonDto()
        {
        }
    }
}
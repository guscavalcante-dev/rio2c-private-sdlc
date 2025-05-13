// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-16-2020
// ***********************************************************************
// <copyright file="TrackDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>TrackDto</summary>
    public class TrackDto
    {
        public Track Track { get; set; }
        public IEnumerable<ConferenceDto> ConferenceDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="TrackDto"/> class.</summary>
        public TrackDto()
        {
        }
    }
}
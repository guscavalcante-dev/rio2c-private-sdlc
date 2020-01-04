// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-04-2020
// ***********************************************************************
// <copyright file="ConferenceHorizontalTrackDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>ConferenceHorizontalTrackDto</summary>
    public class ConferenceHorizontalTrackDto
    {
        public ConferenceHorizontalTrack ConferenceHorizontalTrack { get; set; }
        public HorizontalTrack HorizontalTrack { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ConferenceHorizontalTrackDto"/> class.</summary>
        public ConferenceHorizontalTrackDto()
        {
        }
    }
}
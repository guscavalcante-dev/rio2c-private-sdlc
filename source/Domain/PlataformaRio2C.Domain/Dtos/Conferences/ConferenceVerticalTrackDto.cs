// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-04-2020
// ***********************************************************************
// <copyright file="ConferenceVerticalTrackDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>ConferenceVerticalTrackDto</summary>
    public class ConferenceVerticalTrackDto
    {
        public ConferenceVerticalTrack ConferenceVerticalTrack { get; set; }
        public VerticalTrack VerticalTrack { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ConferenceVerticalTrackDto"/> class.</summary>
        public ConferenceVerticalTrackDto()
        {
        }
    }
}
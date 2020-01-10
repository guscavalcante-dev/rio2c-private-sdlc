// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="ConferenceTrackDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>ConferenceTrackDto</summary>
    public class ConferenceTrackDto
    {
        public ConferenceTrack ConferenceTrack { get; set; }
        public Track Track { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ConferenceTrackDto"/> class.</summary>
        public ConferenceTrackDto()
        {
        }
    }
}
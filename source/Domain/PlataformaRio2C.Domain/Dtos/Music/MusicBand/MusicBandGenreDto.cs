// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 02-28-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-28-2020
// ***********************************************************************
// <copyright file="MusicBandGenreDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>MusicBandGenreDto</summary>
    public class MusicBandGenreDto
    {
        public MusicBandGenre MusicBandGenre { get; set; }
        public MusicGenre MusicGenre { get; set; }

        /// <summary>Initializes a new instance of the <see cref="MusicBandGenreDto"/> class.</summary>
        public MusicBandGenreDto()
        {
        }
    }
}
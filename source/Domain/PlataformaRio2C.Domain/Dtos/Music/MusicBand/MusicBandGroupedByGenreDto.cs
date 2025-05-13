// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 08-06-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-06-2021
// ***********************************************************************
// <copyright file="MusicBandGroupedByGenreDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>MusicBandGroupedByGenreDto</summary>
    public class MusicBandGroupedByGenreDto
    {
        public string MusicGenreName { get; set; }

        /// <summary>
        /// Total of Music Bands with this Music Genre in current Edition
        /// </summary>
        public int MusicBandsTotalCount { get; set; }

        /// <summary>Initializes a new instance of the <see cref="MusicBandGroupedByGenreDto"/> class.</summary>
        public MusicBandGroupedByGenreDto()
        {
        }
    }
}
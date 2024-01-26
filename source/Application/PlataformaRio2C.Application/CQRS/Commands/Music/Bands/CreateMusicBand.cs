// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 23-03-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-25-2024
// ***********************************************************************
// <copyright file="CreateMusicBand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using System.Collections.Generic;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateMusicBand</summary>
    public class CreateMusicBand : BaseCommand
    {
        public MusicBandResponsibleApiDto MusicBandResponsibleApiDto { get; set; }
        public List<MusicBandDataApiDto> MusicBandDataApiDtos { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMusicBand"/> class.
        /// </summary>
        /// <param name="musicBandResponsibleApiDto">The music band responsible API dto.</param>
        /// <param name="musicBandDataApiDtos">The music band data API dtos.</param>
        public CreateMusicBand(
            MusicBandResponsibleApiDto musicBandResponsibleApiDto,
            List<MusicBandDataApiDto> musicBandDataApiDtos)
        {
            this.MusicBandResponsibleApiDto = musicBandResponsibleApiDto;
            this.MusicBandDataApiDtos = musicBandDataApiDtos;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMusicBand"/> class.
        /// </summary>
        public CreateMusicBand()
        {

        }
    }
}
// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 23-03-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 23-03-2021
// ***********************************************************************
// <copyright file="CreateMusicBand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateMusicBand</summary>
    public class CreateMusicBand : BaseCommand//MusicBandBaseCommand
    {
        public MusicBandApiDto MusicBandApiDto { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CreateMusicBand"/> class.</summary>
        public CreateMusicBand(MusicBandApiDto musicBandApiDto)
        {
            this.MusicBandApiDto = musicBandApiDto;
            //this.UpdateBaseProperties(musicBandApiDto);
        }
    }
}
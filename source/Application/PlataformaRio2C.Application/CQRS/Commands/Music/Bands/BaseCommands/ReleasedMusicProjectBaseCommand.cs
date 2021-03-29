// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 03-23-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-23-2021
// ***********************************************************************
// <copyright file="ReleasedMusicProjectBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Foolproof;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>ReleasedMusicProjectBaseCommand</summary>
    public class ReleasedMusicProjectBaseCommand : BaseCommand
    {
        public int MusicBandId { get; set; }
        public string Name { get; set; }
        public string Year { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ReleasedMusicProjectBaseCommand"/> class.</summary>
        public ReleasedMusicProjectBaseCommand()
        {
        }

        /// <summary>
        /// Updates the base properties.
        /// </summary>
        /// <param name="releasedMusicProjectApiDto">The music band team member dto.</param>
        public void UpdateBaseProperties(ReleasedMusicProjectApiDto releasedMusicProjectApiDto)
        {
            this.MusicBandId = releasedMusicProjectApiDto.MusicBandId;
            this.Name = releasedMusicProjectApiDto.Name;
            this.Year = releasedMusicProjectApiDto.Year;
        }
    }
}
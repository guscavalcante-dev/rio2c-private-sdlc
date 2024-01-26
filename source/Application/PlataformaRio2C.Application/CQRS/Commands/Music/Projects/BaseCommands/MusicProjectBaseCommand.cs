// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 03-24-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-26-2024
// ***********************************************************************
// <copyright file="MusicProjectBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>MusicProjectBaseCommand</summary>
    public class MusicProjectBaseCommand : BaseCommand
    {
        public string VideoUrl { get; set; }
        public string Music1Url { get; set; }
        public string Music2Url { get; set; }
        public string Release { get; set; }
        public string Clipping1 { get; set; }
        public string Clipping2 { get; set; }
        public string Clipping3 { get; set; }

        /// <summary>Initializes a new instance of the <see cref="MusicProjectBaseCommand"/> class.</summary>
        public MusicProjectBaseCommand()
        {
        }

        /// <summary>
        /// Updates the base properties.
        /// </summary>
        /// <param name="musicProjectDto">The music project dto.</param>
        public void UpdateBaseProperties(MusicProjectDto musicProjectDto)
        {
            if(musicProjectDto?.MusicProject == null)
            {
                return;
            }

            this.VideoUrl = musicProjectDto.MusicProject.VideoUrl;
            this.Music1Url = musicProjectDto.MusicProject.Music1Url;
            this.Music2Url = musicProjectDto.MusicProject.Music2Url;
            this.Release = musicProjectDto.MusicProject.Release;
            this.Clipping1 = musicProjectDto.MusicProject.Clipping1;
            this.Clipping2 = musicProjectDto.MusicProject.Clipping2;
            this.Clipping3 = musicProjectDto.MusicProject.Clipping3;
        }
    }
}
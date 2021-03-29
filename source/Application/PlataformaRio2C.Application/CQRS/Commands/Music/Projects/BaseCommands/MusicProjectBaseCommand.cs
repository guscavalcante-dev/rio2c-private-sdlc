// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 03-24-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-24-2021
// ***********************************************************************
// <copyright file="MusicProjectBaseCommand.cs" company="Softo">
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
    /// <summary>MusicProjectBaseCommand</summary>
    public class MusicProjectBaseCommand : BaseCommand
    {
        public int AttendeeMusicBandId { get; set; }
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

        public void UpdateBaseProperties(MusicProjectDto musicProjectDto)
        {
            if(musicProjectDto?.MusicProject == null)
            {
                return;
            }

            this.AttendeeMusicBandId = musicProjectDto.MusicProject.AttendeeMusicBandId;
            this.VideoUrl = musicProjectDto.MusicProject.VideoUrl;
            this.Music1Url = musicProjectDto.MusicProject.Music1Url;
            this.Music2Url = musicProjectDto.MusicProject.Music2Url;
            this.Release = musicProjectDto.MusicProject.Release;
            this.Clipping1 = musicProjectDto.MusicProject.Clipping1;
            this.Clipping2 = musicProjectDto.MusicProject.Clipping2;
            this.Clipping3 = musicProjectDto.MusicProject.Clipping3;
        }

        public void UpdateBaseProperties(MusicBandApiDto musicBandApiDto)
        {
            if (musicBandApiDto == null || musicBandApiDto?.MusicProjectApiDto == null)
            {
                return;
            }

            this.AttendeeMusicBandId = musicBandApiDto.MusicProjectApiDto.AttendeeMusicBandId.Value;
            this.VideoUrl = musicBandApiDto.MusicProjectApiDto.VideoUrl;
            this.Music1Url = musicBandApiDto.MusicProjectApiDto.Music1Url;
            this.Music2Url = musicBandApiDto.MusicProjectApiDto.Music2Url;
            this.Release = musicBandApiDto.MusicProjectApiDto.Release;
            this.Clipping1 = musicBandApiDto.MusicProjectApiDto.Clipping1;
            this.Clipping2 = musicBandApiDto.MusicProjectApiDto.Clipping2;
            this.Clipping3 = musicBandApiDto.MusicProjectApiDto.Clipping3;
        }
    }
}
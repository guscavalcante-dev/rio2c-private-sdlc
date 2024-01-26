// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 03-23-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-23-2021
// ***********************************************************************
// <copyright file="CreateMusicProject.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateMusicProject</summary>
    public class CreateMusicProject : MusicProjectBaseCommand
    {
        public Guid MusicProjectUid { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMusicProject"/> class.
        /// </summary>
        /// <param name="musicProjectDto">The music project dto.</param>
        public CreateMusicProject(MusicProjectDto musicProjectDto)
        {
            this.UpdateBaseProperties(musicProjectDto);
        }

        /// <summary>Initializes a new instance of the <see cref="CreateMusicProject"/> class.</summary>
        public CreateMusicProject()
        {
            
        }
    }
}
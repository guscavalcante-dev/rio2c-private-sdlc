// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 03-23-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-23-2021
// ***********************************************************************
// <copyright file="MusicBandTeamMemberBaseCommand.cs" company="Softo">
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
    /// <summary>MusicBandTeamMemberBaseCommand</summary>
    public class MusicBandTeamMemberBaseCommand : BaseCommand
    {
        public int MusicBandId { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }

        /// <summary>Initializes a new instance of the <see cref="MusicBandTeamMemberBaseCommand"/> class.</summary>
        public MusicBandTeamMemberBaseCommand()
        {
        }

        /// <summary>
        /// Updates the base properties.
        /// </summary>
        /// <param name="musicBandTeamMemberDto">The music band team member dto.</param>
        public void UpdateBaseProperties(MusicBandTeamMemberApiDto musicBandTeamMemberDto)
        {
            this.MusicBandId = musicBandTeamMemberDto.MusicBandId;
            this.Name = musicBandTeamMemberDto.Name;
            this.Role = musicBandTeamMemberDto.Role;
        }
    }
}
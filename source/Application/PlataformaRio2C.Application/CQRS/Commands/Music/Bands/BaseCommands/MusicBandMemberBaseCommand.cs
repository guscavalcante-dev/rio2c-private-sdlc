// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 03-23-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-23-2021
// ***********************************************************************
// <copyright file="MusicBandMemberBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>MusicBandMemberBaseCommand</summary>
    public class MusicBandMemberBaseCommand : BaseCommand
    {
        public int MusicBandId { get; set; }
        public string Name { get; set; }
        public string MusicInstrumentName { get; set; }

        /// <summary>Initializes a new instance of the <see cref="MusicBandMemberBaseCommand"/> class.</summary>
        public MusicBandMemberBaseCommand()
        {
        }

        /// <summary>
        /// Updates the base properties.
        /// </summary>
        /// <param name="musicBandMemberDto">The music band member dto.</param>
        public void UpdateBaseProperties(MusicBandMemberApiDto musicBandMemberDto)
        {
            //this.MusicBandId = musicBandMemberDto.MusicBandId;
            this.Name = musicBandMemberDto.Name;
            this.MusicInstrumentName = musicBandMemberDto.MusicInstrumentName;
        }
    }
}
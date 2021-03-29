// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 03-24-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-24-2021
// ***********************************************************************
// <copyright file="CreateMusicBandMember.cs" company="Softo">
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
    /// <summary>CreateMusicBandMember</summary>
    public class CreateMusicBandMember : MusicBandMemberBaseCommand
    {
        /// <summary>Initializes a new instance of the <see cref="CreateMusicBandMember"/> class.</summary>
        public CreateMusicBandMember(MusicBandMemberApiDto musicBandMemberDto)
        {
            this.UpdateBaseProperties(musicBandMemberDto);
        }
    }
}
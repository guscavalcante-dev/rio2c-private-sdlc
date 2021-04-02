// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 02-26-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-28-2020
// ***********************************************************************
// <copyright file="AttendeeMusicBandDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeMusicBandDto</summary>
    public class AttendeeMusicBandDto
    {
        public AttendeeMusicBand AttendeeMusicBand { get; set; }
        public MusicBand MusicBand { get; set; }
        public MusicBandType MusicBandType { get; set; }
        public AttendeeMusicBandCollaboratorDto AttendeeMusicBandCollaboratorDto { get; set; }

        public IEnumerable<MusicBandGenreDto> MusicBandGenreDtos { get; set; }
        public IEnumerable<MusicBandTargetAudienceDto> MusicBandTargetAudienceDtos { get; set; }
        public IEnumerable<MusicBandMember> MusicBandMembers { get; set; }
        public IEnumerable<MusicBandTeamMember> MusicBandTeamMembers { get; set; }
        public IEnumerable<ReleasedMusicProject> ReleasedMusicProjects { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeMusicBandDto"/> class.</summary>
        public AttendeeMusicBandDto()
        {
        }
    }
}
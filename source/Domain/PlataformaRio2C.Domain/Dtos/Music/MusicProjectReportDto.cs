// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 07-20-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-20-2023
// ***********************************************************************
// <copyright file="MusicProjectReportDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>MusicProjectReportDto</summary>
    public class MusicProjectReportDto
    {
        public Guid MusicBandUid { get; set; }
        public int AttendeeMusicBandId { get; set; }
        public int MusicBandId { get; set; }
        public string MusicBandName { get; set; }
        public string FormationDate { get; set; }
        public string MainMusicInfluences { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Twitter { get; set; }
        public string YouTube { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public DateTimeOffset? ImageUploadDate { get; set; }
        public bool WouldYouLikeParticipateBusinessRound { get; set; }
        public bool WouldYouLikeParticipatePitching { get; set; }

        public MusicBandType MusicBandType { get; set; }
        public MusicProjectApiDto MusicProjectApiDto { get; set; }
        public MusicBandResponsibleApiDto MusicBandResponsibleApiDto { get; set; }
        public IEnumerable<MusicBandMemberApiDto> MusicBandMembersApiDtos { get; set; }
        public IEnumerable<MusicBandTeamMemberApiDto> MusicBandTeamMembersApiDtos { get; set; }
        public IEnumerable<ReleasedMusicProjectApiDto> ReleasedMusicProjectsApiDtos { get; set; }
        public IEnumerable<MusicGenreApiDto> MusicGenresApiDtos { get; set; }
        public IEnumerable<TargetAudienceApiDto> TargetAudiencesApiDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="MusicProjectReportDto"/> class.</summary>
        public MusicProjectReportDto()
        {
        }
    }
}
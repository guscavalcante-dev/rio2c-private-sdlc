// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 23-03-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-14-2023
// ***********************************************************************
// <copyright file="CreateMusicBand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateMusicBand</summary>
    public class CreateMusicBand : BaseCommand
    {
        public Guid MusicBandTypeUid { get; private set; }
        public string Name { get; private set; }
        public string ImageFile { get; private set; }
        public string FormationDate { get; private set; }
        public string MainMusicInfluences { get; private set; }
        public string Facebook { get; private set; }
        public string Instagram { get; private set; }
        public string Twitter { get; private set; }
        public string Youtube { get; private set; }
        public bool WouldYouLikeParticipateBusinessRound { get; private set; }
        public bool WouldYouLikeParticipatePitching { get; private set; }

        public MusicProjectApiDto MusicProjectApiDto { get; set; }
        public MusicBandResponsibleApiDto MusicBandResponsibleApiDto { get; set; }
        public List<MusicBandMemberApiDto> MusicBandMembersApiDtos { get; set; }
        public List<MusicBandTeamMemberApiDto> MusicBandTeamMembersApiDtos { get; set; }
        public List<ReleasedMusicProjectApiDto> ReleasedMusicProjectsApiDtos { get; set; }
        public List<MusicGenreApiDto> MusicGenresApiDtos { get; set; }
        public List<TargetAudienceApiDto> TargetAudiencesApiDtos { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMusicBand" /> class.
        /// </summary>
        /// <param name="musicBandTypeUid">The music band type uid.</param>
        /// <param name="name">The name.</param>
        /// <param name="imageFile">The image file.</param>
        /// <param name="formationDate">The formation date.</param>
        /// <param name="mainMusicInfluences">The main music influences.</param>
        /// <param name="facebook">The facebook.</param>
        /// <param name="instagram">The instagram.</param>
        /// <param name="twitter">The twitter.</param>
        /// <param name="youtube">The youtube.</param>
        /// <param name="wouldYouLikeParticipateBusinessRound">if set to <c>true</c> [would you like participate business round].</param>
        /// <param name="wouldYouLikeParticipatePitching">if set to <c>true</c> [would you like participate pitching].</param>
        /// <param name="musicProjectApiDto">The music project API dto.</param>
        /// <param name="musicBandResponsibleApiDto">The music band responsible API dto.</param>
        /// <param name="musicBandMembersApiDtos">The music band members API dtos.</param>
        /// <param name="musicBandTeamMembersApiDtos">The music band team members API dtos.</param>
        /// <param name="releasedMusicProjectsApiDtos">The released music projects API dtos.</param>
        /// <param name="musicGenresApiDtos">The music genres API dtos.</param>
        /// <param name="targetAudiencesApiDtos">The target audiences API dtos.</param>
        public CreateMusicBand(
            Guid musicBandTypeUid,
            string name,
            string imageFile,
            string formationDate,
            string mainMusicInfluences,
            string facebook,
            string instagram,
            string twitter,
            string youtube,
            bool wouldYouLikeParticipateBusinessRound,
            bool wouldYouLikeParticipatePitching,
            MusicProjectApiDto musicProjectApiDto,
            MusicBandResponsibleApiDto musicBandResponsibleApiDto,
            List<MusicBandMemberApiDto> musicBandMembersApiDtos,
            List<MusicBandTeamMemberApiDto> musicBandTeamMembersApiDtos,
            List<ReleasedMusicProjectApiDto> releasedMusicProjectsApiDtos,
            List<MusicGenreApiDto> musicGenresApiDtos,
            List<TargetAudienceApiDto> targetAudiencesApiDtos)
        {
            this.MusicBandTypeUid = musicBandTypeUid;
            this.Name = name;
            this.ImageFile = imageFile;
            this.FormationDate = formationDate;
            this.MainMusicInfluences = mainMusicInfluences;
            this.Facebook = facebook;
            this.Instagram = instagram;
            this.Twitter = twitter;
            this.Youtube = youtube;
            this.WouldYouLikeParticipateBusinessRound = wouldYouLikeParticipateBusinessRound;
            this.WouldYouLikeParticipatePitching = wouldYouLikeParticipatePitching;
            this.MusicProjectApiDto = musicProjectApiDto;
            this.MusicBandResponsibleApiDto = musicBandResponsibleApiDto;
            this.MusicBandMembersApiDtos = musicBandMembersApiDtos;
            this.MusicBandTeamMembersApiDtos = musicBandTeamMembersApiDtos;
            this.ReleasedMusicProjectsApiDtos = releasedMusicProjectsApiDtos;
            this.MusicGenresApiDtos = musicGenresApiDtos;
            this.TargetAudiencesApiDtos = targetAudiencesApiDtos;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMusicBand"/> class.
        /// </summary>
        public CreateMusicBand()
        {

        }
    }
}
// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 23-03-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 23-03-2021
// ***********************************************************************
// <copyright file="CreateMusicBand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using System.Collections.Generic;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateMusicBand</summary>
    public class CreateMusicBand : BaseCommand
    {
        public int MusicBandTypeId { get; private set; }
        public string Name { get; private set; }
        public string ImageUrl { get; private set; }
        public string FormationDate { get; private set; }
        public string MainMusicInfluences { get; private set; }
        public string Facebook { get; private set; }
        public string Instagram { get; private set; }
        public string Twitter { get; private set; }
        public string Youtube { get; private set; }
        public MusicProjectApiDto MusicProjectApiDto { get; set; }
        public MusicBandResponsibleApiDto MusicBandResponsibleApiDto { get; set; }
        public List<MusicBandMemberApiDto> MusicBandMembersApiDtos { get; set; }
        public List<MusicBandTeamMemberApiDto> MusicBandTeamMembersApiDtos { get; set; }
        public List<ReleasedMusicProjectApiDto> ReleasedMusicProjectsApiDtos { get; set; }
        public List<MusicGenreApiDto> MusicGenresApiDtos { get; set; }
        public List<TargetAudienceApiDto> TargetAudiencesApiDtos { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMusicBand"/> class.
        /// </summary>
        /// <param name="musicBandTypeId">The music band type identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="imageUrl">The image URL.</param>
        /// <param name="formationDate">The formation date.</param>
        /// <param name="mainMusicInfluences">The main music influences.</param>
        /// <param name="facebook">The facebook.</param>
        /// <param name="instagram">The instagram.</param>
        /// <param name="twitter">The twitter.</param>
        /// <param name="youtube">The youtube.</param>
        /// <param name="musicProjectApiDto">The music project API dto.</param>
        /// <param name="musicBandResponsibleApiDto">The music band responsible API dto.</param>
        /// <param name="musicBandMembersApiDtos">The music band members API dtos.</param>
        /// <param name="musicBandTeamMembersApiDtos">The music band team members API dtos.</param>
        /// <param name="releasedMusicProjectsApiDtos">The released music projects API dtos.</param>
        /// <param name="musicGenresApiDtos">The music genres API dtos.</param>
        /// <param name="targetAudiencesApiDtos">The target audiences API dtos.</param>
        public CreateMusicBand(
            int musicBandTypeId,
            string name,
            string imageUrl,
            string formationDate,
            string mainMusicInfluences,
            string facebook,
            string instagram,
            string twitter,
            string youtube,
            MusicProjectApiDto musicProjectApiDto,
            MusicBandResponsibleApiDto musicBandResponsibleApiDto,
            List<MusicBandMemberApiDto> musicBandMembersApiDtos,
            List<MusicBandTeamMemberApiDto> musicBandTeamMembersApiDtos,
            List<ReleasedMusicProjectApiDto> releasedMusicProjectsApiDtos,
            List<MusicGenreApiDto> musicGenresApiDtos,
            List<TargetAudienceApiDto> targetAudiencesApiDtos)
        {
            this.MusicBandTypeId = musicBandTypeId;
            this.Name = name;
            this.ImageUrl = imageUrl;
            this.FormationDate = formationDate;
            this.MainMusicInfluences = mainMusicInfluences;
            this.Facebook = facebook;
            this.Instagram = instagram;
            this.Twitter = twitter;
            this.Youtube = youtube;
            this.MusicProjectApiDto = musicProjectApiDto;
            this.MusicBandResponsibleApiDto = musicBandResponsibleApiDto;
            this.MusicBandMembersApiDtos = musicBandMembersApiDtos;
            this.MusicBandTeamMembersApiDtos = musicBandTeamMembersApiDtos;
            this.ReleasedMusicProjectsApiDtos = releasedMusicProjectsApiDtos;
            this.MusicGenresApiDtos = musicGenresApiDtos;
            this.TargetAudiencesApiDtos = targetAudiencesApiDtos;
        }

        /// <summary>Initializes a new instance of the <see cref="CreateMusicBand"/> class.</summary>
        public CreateMusicBand(MusicBandApiDto musicBandApiDto)
        {
            this.MusicBandTypeId = musicBandApiDto.MusicBandTypeId;
            this.Name = musicBandApiDto.Name;
            this.ImageUrl = musicBandApiDto.ImageUrl;
            this.FormationDate = musicBandApiDto.FormationDate;
            this.MainMusicInfluences = musicBandApiDto.MainMusicInfluences;
            this.Facebook = musicBandApiDto.Facebook;
            this.Instagram = musicBandApiDto.Instagram;
            this.Twitter = musicBandApiDto.Twitter;
            this.Youtube = musicBandApiDto.Youtube;
            this.MusicProjectApiDto = musicBandApiDto.MusicProjectApiDto;
            this.MusicBandResponsibleApiDto = musicBandApiDto.MusicBandResponsibleApiDto;
            this.MusicBandMembersApiDtos = musicBandApiDto.MusicBandMembersApiDtos;
            this.MusicBandTeamMembersApiDtos = musicBandApiDto.MusicBandTeamMembersApiDtos;
            this.ReleasedMusicProjectsApiDtos = musicBandApiDto.ReleasedMusicProjectsApiDtos;
            this.MusicGenresApiDtos = musicBandApiDto.MusicGenresApiDtos;
            this.TargetAudiencesApiDtos = musicBandApiDto.TargetAudiencesApiDtos;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMusicBand"/> class.
        /// </summary>
        public CreateMusicBand()
        {

        }
    }
}
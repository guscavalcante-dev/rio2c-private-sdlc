// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 23-03-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 23-03-2021
// ***********************************************************************
// <copyright file="MusicBandApiDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>MusicBandApiDto</summary>
    public class MusicBandApiDto
    {
        [JsonProperty(PropertyName = "musicBandTypeId", Order = 100)]
        public int MusicBandTypeId { get; set; } //1-Banda/Grupo Musical | 2-Artista Solo

        [JsonProperty(PropertyName = "name", Order = 200)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "imageUrl", Order = 300)]
        public string ImageUrl { get; set; }

        [JsonProperty(PropertyName = "formationDate", Order = 400)]
        public string FormationDate { get; set; }

        [JsonProperty(PropertyName = "mainMusicInfluences", Order = 500)]
        public string MainMusicInfluences { get; set; }

        [JsonProperty(PropertyName = "facebook", Order = 600)]
        public string Facebook { get; set; }

        [JsonProperty(PropertyName = "instagram", Order = 700)]
        public string Instagram { get; set; }

        [JsonProperty(PropertyName = "twitter", Order = 800)]
        public string Twitter { get; set; }

        [JsonProperty(PropertyName = "youtube", Order = 900)]
        public string Youtube { get; set; }


        [JsonProperty(PropertyName = "musicProject", Order = 1000)]
        public MusicProjectApiDto MusicProjectApiDto { get; set; }

        [JsonProperty(PropertyName = "musicBandMembers", Order = 1100)]
        public List<MusicBandMemberApiDto> MusicBandMembersApiDtos { get; set; }

        [JsonProperty(PropertyName = "musicBandTeamMembers", Order = 1200)]
        public List<MusicBandTeamMemberApiDto> MusicBandTeamMembersApiDtos { get; set; }

        [JsonProperty(PropertyName = "releasedMusicProjects", Order = 1300)]
        public List<ReleasedMusicProjectApiDto> ReleasedMusicProjectsApiDtos { get; set; }

        [JsonProperty(PropertyName = "musicGenres", Order = 1400)]
        public List<MusicGenreApiDto> MusicGenresApiDtos { get; set; }

        [JsonProperty(PropertyName = "targetAudiences", Order = 1500)]
        public List<TargetAudienceApiDto> TargetAudiencesApiDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="MusicBandApiDto"/> class.</summary>
        public MusicBandApiDto()
        {
        }

        /// <summary>
        /// Generates the test json.
        /// </summary>
        /// <returns></returns>
        public string GenerateTestJson()
        {
            this.MusicBandTypeId = 2;
            this.Name = "My definitive band";
            this.ImageUrl = "https://png.pngtree.com/element_our/png/20180804/rock-group-music-band-png_52911.jpg";
            this.FormationDate = "2021";
            this.MainMusicInfluences = "Rock;Metal;Heavy Metal";
            this.Facebook = "facebook.com";
            this.Instagram = "instagram.com";
            this.Twitter = "twitter.com";
            this.Youtube = "youtube.com";

            if (this.MusicProjectApiDto == null)
                this.MusicProjectApiDto = new MusicProjectApiDto();

            this.MusicProjectApiDto.AttendeeMusicBandId = null;
            this.MusicProjectApiDto.VideoUrl = "youtube.com";
            this.MusicProjectApiDto.Music1Url = "music1Url.com";
            this.MusicProjectApiDto.Music2Url = "music2Url.com";
            this.MusicProjectApiDto.Clipping1 = "clipping1.com";
            this.MusicProjectApiDto.Clipping2 = "clipping2.com";
            this.MusicProjectApiDto.Clipping3 = "clipping3.com";
            this.MusicProjectApiDto.Release = "My definitive band has been formed at 2021.";

            this.MusicBandMembersApiDtos = new List<MusicBandMemberApiDto>()
            {
                new MusicBandMemberApiDto()
                {
                    IsProjectResponsible = true,
                    Name = "Glenn Danzig",
                    MusicInstrumentName = "Vocal",
                    Email = "email@gmail.com"
                },
                new MusicBandMemberApiDto()
                {
                    Name = "Jimmy Hendrix",
                    MusicInstrumentName = "Guitarra"
                },
                new MusicBandMemberApiDto()
                {
                    Name = "Joey Jordison",
                    MusicInstrumentName = "Bateria"
                }
            };

            this.MusicBandTeamMembersApiDtos = new List<MusicBandTeamMemberApiDto>()
            {
                new MusicBandTeamMemberApiDto()
                {
                    Name = "Calango Tour",
                    Role = "Motorista"
                },
                new MusicBandTeamMemberApiDto()
                {
                    Name = "Fakir Pawlovsky",
                    Role = "Intervenção Artística"
                }
            };

            this.ReleasedMusicProjectsApiDtos = new List<ReleasedMusicProjectApiDto>()
            {
                new ReleasedMusicProjectApiDto()
                {
                    Name = "Só modão vol. 1",
                    Year = "2021"
                },
                new ReleasedMusicProjectApiDto()
                {
                    Name = "Só modão do heavy metal vol. 666",
                    Year = "2021"
                }
            };

            this.MusicGenresApiDtos = new List<MusicGenreApiDto>()
            {
                new MusicGenreApiDto() { Id = 19 },
                new MusicGenreApiDto() { Id = 20 }
            };

            this.TargetAudiencesApiDtos = new List<TargetAudienceApiDto>()
            {
                new TargetAudienceApiDto() { Id = 6 },
                new TargetAudienceApiDto() { Id = 7 }
            };

            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}
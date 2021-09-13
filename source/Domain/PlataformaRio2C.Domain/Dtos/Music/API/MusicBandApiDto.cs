// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 23-03-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 09-10-2021
// ***********************************************************************
// <copyright file="MusicBandApiDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using PlataformaRio2C.Domain.Validation;
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>MusicBandApiDto</summary>
    public class MusicBandApiDto
    {
        [JsonIgnore]
        public ValidationResult ValidationResult { get; set; }

        #region Required

        [JsonRequired]
        [JsonProperty("musicBandTypeUid")]
        public Guid? MusicBandTypeUid { get; set; }

        [JsonRequired]
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonRequired]
        [JsonProperty("formationDate")]
        public string FormationDate { get; set; }

        [JsonRequired]
        [JsonProperty("mainMusicInfluences")]
        public string MainMusicInfluences { get; set; }

        /// <summary>
        /// Don't remove order. This is for JSON beauty design. 
        /// Should be aligned at end of JSON.
        /// </summary>
        [JsonRequired]
        [JsonProperty("imageFile", Order = 98)]
        public string ImageFile { get; set; }

        [JsonRequired]
        [JsonProperty("musicProject")]
        public MusicProjectApiDto MusicProjectApiDto { get; set; }

        [JsonRequired]
        [JsonProperty("musicBandResponsible")]
        public MusicBandResponsibleApiDto MusicBandResponsibleApiDto { get; set; }

        [JsonRequired]
        [JsonProperty("musicBandMembers")]
        public List<MusicBandMemberApiDto> MusicBandMembersApiDtos { get; set; }

        [JsonRequired]
        [JsonProperty("musicGenres")]
        public List<MusicGenreApiDto> MusicGenresApiDtos { get; set; }

        [JsonRequired]
        [JsonProperty("targetAudiences")]
        public List<TargetAudienceApiDto> TargetAudiencesApiDtos { get; set; }

        #endregion

        #region Not required

        [JsonProperty("facebook")]
        public string Facebook { get; set; }

        [JsonProperty("instagram")]
        public string Instagram { get; set; }

        [JsonProperty("twitter")]
        public string Twitter { get; set; }

        [JsonProperty("youtube")]
        public string Youtube { get; set; }

        [JsonProperty("musicBandTeamMembers")]
        public List<MusicBandTeamMemberApiDto> MusicBandTeamMembersApiDtos { get; set; }

        [JsonProperty("releasedMusicProjects")]
        public List<ReleasedMusicProjectApiDto> ReleasedMusicProjectsApiDtos { get; set; }

        #endregion

        /// <summary>Initializes a new instance of the <see cref="MusicBandApiDto"/> class.</summary>
        public MusicBandApiDto()
        {
        }

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            return this.ValidationResult.IsValid;
        }

        #endregion

        /// <summary>
        /// Generates the test json.
        /// </summary>
        /// <returns></returns>
        public static string GenerateTestJson()
        {
            MusicBandApiDto i = new MusicBandApiDto();

            i.MusicBandTypeUid = new Guid("DD8D2040-52D2-427B-962B-026B7B1C4604");
            i.Name = "My definitive band";
            i.ImageFile = "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";
            i.FormationDate = "2021";
            i.MainMusicInfluences = "Rock;Metal;Heavy Metal";
            i.Facebook = "facebook.com";
            i.Instagram = "instagram.com";
            i.Twitter = "twitter.com";
            i.Youtube = "youtube.com";
            
            i.MusicProjectApiDto = new MusicProjectApiDto()
            {
                VideoUrl = "youtube.com",
                Music1Url = "music1Url.com",
                Music2Url = "music2Url.com",
                Clipping1 = "clipping1.com",
                Clipping2 = "clipping2.com",
                Clipping3 = "clipping3.com",
                Release = "My definitive band has been formed at 2021."
            };

            i.MusicBandResponsibleApiDto = new MusicBandResponsibleApiDto()
            {
                Name = "Ozzy Osbourne",
                Document = "56.998.566/0001-09",
                Email = "email@email.com",
                PhoneNumber = "+55 14 99999-9999",
                CellPhone = "+55 11 88888-8888"
            };

            i.MusicBandMembersApiDtos = new List<MusicBandMemberApiDto>()
            {
                new MusicBandMemberApiDto()
                {
                    Name = "Glenn Danzig",
                    MusicInstrumentName = "Vocal"
                },
                new MusicBandMemberApiDto()
                {
                    Name = "Jimmy Hendrix",
                    MusicInstrumentName = "Guitar"
                },
                new MusicBandMemberApiDto()
                {
                    Name = "Joey Jordison",
                    MusicInstrumentName = "Drums"
                }
            };

            i.MusicBandTeamMembersApiDtos = new List<MusicBandTeamMemberApiDto>()
            {
                new MusicBandTeamMemberApiDto()
                {
                    Name = "Herbert Richers",
                    Role = "Executive Producer"
                },
                new MusicBandTeamMemberApiDto()
                {
                    Name = "Fakir Pawlovsky",
                    Role = "Artistic Intervention"
                }
            };

            i.ReleasedMusicProjectsApiDtos = new List<ReleasedMusicProjectApiDto>()
            {
                new ReleasedMusicProjectApiDto()
                {
                    Name = "My First Band Album",
                    Year = "2020"
                },
                new ReleasedMusicProjectApiDto()
                {
                    Name = "My Second Band Album",
                    Year = "2021"
                }
            };

            i.MusicGenresApiDtos = new List<MusicGenreApiDto>()
            {
                new MusicGenreApiDto() { Uid = new Guid("CAFC1D28-7477-4AB3-99F8-FDBD4281CB41") },
                new MusicGenreApiDto() { Uid = new Guid("58569CEF-2FB6-49F7-8A74-FC5BEAB28527") }
            };

            i.TargetAudiencesApiDtos = new List<TargetAudienceApiDto>()
            {
                new TargetAudienceApiDto() { Uid = new Guid("F69DF4BE-3994-451B-BDAC-E019E17F1668") },
                new TargetAudienceApiDto() { Uid = new Guid("D7DD48CD-4781-44AF-BEF8-E0C67DEB11CF") }
            };

            return Newtonsoft.Json.JsonConvert.SerializeObject(i);
        }
    }
}
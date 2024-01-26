// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 01-26-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-26-2024
// ***********************************************************************
// <copyright file="MusicBandDataApiDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace PlataformaRio2C.Domain.Dtos
{
    public class MusicBandDataApiDto
    {
        [JsonIgnore]
        public ValidationResult ValidationResult { get; set; }

        #region Required
        
        [JsonRequired]
        [JsonProperty("musicBandTypeUid")]
        public Guid MusicBandTypeUid { get; set; }

        [JsonRequired]
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonRequired]
        [JsonProperty("formationDate")]
        public string FormationDate { get; set; }

        [JsonRequired]
        [JsonProperty("mainMusicInfluences")]
        public string MainMusicInfluences { get; set; }

        [JsonRequired]
        [JsonProperty("wouldYouLikeParticipateBusinessRound")]
        public bool WouldYouLikeParticipateBusinessRound { get; set; }

        [JsonRequired]
        [JsonProperty("wouldYouLikeParticipatePitching")]
        public bool WouldYouLikeParticipatePitching { get; set; }

        [JsonRequired]
        [JsonProperty("musicProject")]
        public MusicProjectApiDto MusicProjectApiDto { get; set; }

        [JsonRequired]
        [JsonProperty("musicBandMembers")]
        public List<MusicBandMemberApiDto> MusicBandMembersApiDtos { get; set; }

        [JsonRequired]
        [JsonProperty("musicGenres")]
        public List<MusicGenreApiDto> MusicGenresApiDtos { get; set; }

        [JsonRequired]
        [JsonProperty("targetAudiences")]
        public List<TargetAudienceApiDto> TargetAudiencesApiDtos { get; set; }

        /// <summary>
        /// Don't remove order. This is for JSON beauty design. 
        /// Should be aligned at end of JSON.
        /// </summary>
        [JsonRequired]
        [JsonProperty("imageFile", Order = 98)]
        public string ImageFile { get; set; }

        #endregion

        #region NotRequired

        [JsonProperty("facebook")]
        public string Facebook { get; set; }

        [JsonProperty("instagram")]
        public string Instagram { get; set; }

        [JsonProperty("tiktok")]
        public string Tiktok { get; set; }

        [JsonProperty("x")]
        public string Twitter { get; set; }

        [JsonProperty("youtube")]
        public string Youtube { get; set; }

        [JsonProperty("musicBandTeamMembers")]
        public List<MusicBandTeamMemberApiDto> MusicBandTeamMembersApiDtos { get; set; }

        [JsonProperty("releasedMusicProjects")]
        public List<ReleasedMusicProjectApiDto> ReleasedMusicProjectsApiDtos { get; set; }
        
        #endregion

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            if (this.WouldYouLikeParticipateBusinessRound == false &&
                this.WouldYouLikeParticipatePitching == false)
            {
                this.ValidationResult.Add(new ValidationError($"{Messages.SelectAtLeastOneOfTheOptions}: {Messages.WouldYouLikeParticipateBusinessRound} | {Messages.WouldYouLikeParticipatePitching}"));
            }

            return this.ValidationResult.IsValid;
        }

        /// <summary>
        /// Gets the name of the json property attribute.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        public static string GetJsonPropertyAttributeName(string propertyName)
        {
            return typeof(MusicBandDataApiDto)
                    .GetProperty(propertyName)?
                    .GetCustomAttribute<JsonPropertyAttribute>()?.PropertyName;
        }

        #endregion
    }
}
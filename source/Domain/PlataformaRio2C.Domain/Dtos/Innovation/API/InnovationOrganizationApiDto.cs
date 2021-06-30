// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 28-06-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 28-06-2021
// ***********************************************************************
// <copyright file="InnovationOrganizationApiDto.cs" company="Softo">
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
    /// <summary>InnovationOrganizationApiDto</summary>
    public class InnovationOrganizationApiDto
    {
        [JsonIgnore]
        public ValidationResult ValidationResult { get; set; }

        [JsonRequired]
        [JsonProperty(PropertyName = "name", Order = 200)]
        public string Name { get; set; }

        [JsonRequired]
        [JsonProperty(PropertyName = "document", Order = 300)]
        public string Document { get; set; }

        [JsonRequired]
        [JsonProperty(PropertyName = "serviceName", Order = 400)]
        public string ServiceName { get; set; }

        [JsonRequired]
        [JsonProperty(PropertyName = "foundersNames", Order = 500)]
        public string FoundersNames { get; set; }

        [JsonRequired]
        [JsonProperty(PropertyName = "agentName", Order = 500)]
        public string ResponsibleName { get; set; } //TODO: Nome do Representante?

        [JsonRequired]
        [JsonProperty(PropertyName = "email", Order = 500)]
        public string Email { get; set; }

        [JsonRequired]
        [JsonProperty(PropertyName = "phoneNumber", Order = 500)]
        public string PhoneNumber { get; set; }

        [JsonRequired]
        [JsonProperty(PropertyName = "cellPhone", Order = 500)]
        public string CellPhone { get; set; }

        [JsonRequired]
        [JsonProperty(PropertyName = "foundationDate", Order = 600)]
        public DateTime FoundationDate { get; set; }

        [JsonRequired]
        [JsonProperty(PropertyName = "accumulatedRevenue", Order = 700)]
        public decimal AccumulatedRevenue { get; set; }

        [JsonRequired]
        [JsonProperty(PropertyName = "description", Order = 800)]
        public string Description { get; set; }

        [JsonRequired]
        [JsonProperty(PropertyName = "curriculum", Order = 900)]
        public string Curriculum { get; set; }

        [JsonRequired]
        [JsonProperty(PropertyName = "businessDefinition", Order = 900)]
        public string BusinessDefinition { get; set; }

        [JsonRequired]
        [JsonProperty(PropertyName = "businessFocus", Order = 900)]
        public string BusinessFocus { get; set; }

        [JsonRequired]
        [JsonProperty(PropertyName = "businessDifferentials", Order = 900)]
        public string BusinessDifferentials { get; set; }

        [JsonRequired]
        [JsonProperty(PropertyName = "competingCompanies", Order = 900)]
        public string CompetingCompanies { get; set; }

        [JsonRequired]
        [JsonProperty(PropertyName = "businessStage", Order = 900)]
        public string BusinessStage { get; set; }



        [JsonProperty(PropertyName = "website", Order = 900)]
        public string Website { get; set; }

        [JsonProperty(PropertyName = "marketSize", Order = 900)]
        public string MarketSize { get; set; }

        [JsonProperty(PropertyName = "businessEconomicModel", Order = 900)]
        public string BusinessEconomicModel { get; set; }

        [JsonProperty(PropertyName = "businessOperationalModel", Order = 900)]
        public string BusinessOperationalModel { get; set; }

        [JsonProperty(PropertyName = "presentationUploadDate", Order = 900)]
        public DateTime PresentationUploadDate { get; set; }



        [JsonRequired]
        public List<InnovationOptionsApiDto> CompanyExperiences { get; set; }
        [JsonRequired]
        public List<InnovationOptionsApiDto> ProductsOrServices { get; set; }
        [JsonRequired]
        public List<InnovationOptionsApiDto> TechnologyExperiences { get; set; }
        [JsonRequired]
        public List<InnovationOptionsApiDto> CompanyObjectives { get; set; }

        /// <summary>Initializes a new instance of the <see cref="InnovationOrganizationApiDto"/> class.</summary>
        public InnovationOrganizationApiDto()
        {
        }

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidadeResponsible();

            return this.ValidationResult.IsValid;
        }

        /// <summary>
        /// Validades the responsible.
        /// </summary>
        /// <returns></returns>
        private void ValidadeResponsible()
        {
        }

        #endregion

        /// <summary>
        /// Generates the test json.
        /// </summary>
        /// <returns></returns>
        public string GenerateTestJson()
        {
            //this.MusicBandTypeId = 2;
            //this.Name = "My definitive band";
            //this.ImageUrl = "https://png.pngtree.com/element_our/png/20180804/rock-group-music-band-png_52911.jpg";
            //this.FormationDate = "2021";
            //this.MainMusicInfluences = "Rock;Metal;Heavy Metal";
            //this.Facebook = "facebook.com";
            //this.Instagram = "instagram.com";
            //this.Twitter = "twitter.com";
            //this.Youtube = "youtube.com";

            //this.MusicProjectApiDto = new MusicProjectApiDto()
            //{
            //    VideoUrl = "youtube.com",
            //    Music1Url = "music1Url.com",
            //    Music2Url = "music2Url.com",
            //    Clipping1 = "clipping1.com",
            //    Clipping2 = "clipping2.com",
            //    Clipping3 = "clipping3.com",
            //    Release = "My definitive band has been formed at 2021."
            //};

            //this.MusicBandResponsibleApiDto = new MusicBandResponsibleApiDto()
            //{
            //    Name = "Ozzy Osbourne",
            //    Document = "56.998.566/0001-09",
            //    Email = "email@email.com",
            //    PhoneNumber = "+55 14 99999-9999",
            //    CellPhone = "+55 11 88888-8888"
            //};

            //this.MusicBandMembersApiDtos = new List<MusicBandMemberApiDto>()
            //{
            //    new MusicBandMemberApiDto()
            //    {
            //        Name = "Glenn Danzig",
            //        MusicInstrumentName = "Vocal"
            //    },
            //    new MusicBandMemberApiDto()
            //    {
            //        Name = "Jimmy Hendrix",
            //        MusicInstrumentName = "Guitarra"
            //    },
            //    new MusicBandMemberApiDto()
            //    {
            //        Name = "Joey Jordison",
            //        MusicInstrumentName = "Bateria"
            //    }
            //};

            //this.MusicBandTeamMembersApiDtos = new List<MusicBandTeamMemberApiDto>()
            //{
            //    new MusicBandTeamMemberApiDto()
            //    {
            //        Name = "Calango Tour",
            //        Role = "Motorista"
            //    },
            //    new MusicBandTeamMemberApiDto()
            //    {
            //        Name = "Fakir Pawlovsky",
            //        Role = "Intervenção Artística"
            //    }
            //};

            //this.ReleasedMusicProjectsApiDtos = new List<ReleasedMusicProjectApiDto>()
            //{
            //    new ReleasedMusicProjectApiDto()
            //    {
            //        Name = "Só modão vol. 1",
            //        Year = "2021"
            //    },
            //    new ReleasedMusicProjectApiDto()
            //    {
            //        Name = "Só modão do heavy metal vol. 666",
            //        Year = "2021"
            //    }
            //};

            //this.MusicGenresApiDtos = new List<MusicGenreApiDto>()
            //{
            //    new MusicGenreApiDto() { Id = 19 },
            //    new MusicGenreApiDto() { Id = 20 }
            //};

            //this.TargetAudiencesApiDtos = new List<TargetAudienceApiDto>()
            //{
            //    new TargetAudienceApiDto() { Id = 6 },
            //    new TargetAudienceApiDto() { Id = 7 }
            //};

            //return Newtonsoft.Json.JsonConvert.SerializeObject(this);
            return "";
        }
    }
}
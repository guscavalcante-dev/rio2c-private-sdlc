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
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Statics;
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>InnovationOrganizationApiDto</summary>
    public class InnovationOrganizationApiDto
    {
        [JsonIgnore]
        public ValidationResult ValidationResult { get; set; }

        [JsonIgnore]
        private int CurriculumMaxLenght = 710;
        [JsonIgnore]
        private int DescriptionMaxLenght = 600;
        [JsonIgnore]
        private int CompetingCompaniesMaxCount = 3;

        #region Required

        [JsonRequired]
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonRequired]
        [JsonProperty("document")]
        public string Document { get; set; }

        [JsonRequired]
        [JsonProperty("serviceName")]
        public string ServiceName { get; set; }

        [JsonRequired]
        [JsonProperty("agentName")]
        public string ResponsibleName { get; set; }

        [JsonRequired]
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonRequired]
        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonRequired]
        [JsonProperty("cellPhone")]
        public string CellPhone { get; set; }

        [JsonRequired]
        [JsonProperty("foundationDate")]
        public DateTime FoundationDate { get; set; }

        [JsonRequired]
        [JsonProperty("accumulatedRevenue")]
        public decimal AccumulatedRevenue { get; set; }

        [JsonRequired]
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonRequired]
        [JsonProperty("curriculum")]
        public string Curriculum { get; set; }

        [JsonRequired]
        [JsonProperty("businessDefinition")]
        public string BusinessDefinition { get; set; }

        [JsonRequired]
        [JsonProperty("businessFocus")]
        public string BusinessFocus { get; set; }

        [JsonRequired]
        [JsonProperty("businessDifferentials")]
        public string BusinessDifferentials { get; set; }

        [JsonRequired]
        [JsonProperty("workDedicationUid")]
        public Guid WorkDedicationUid { get; set; }

        [JsonRequired]
        [JsonProperty("businessStage")]
        public string BusinessStage { get; set; }

        [JsonRequired]
        [JsonProperty("presentationFile")]
        public string PresentationFile { get; set; }

        [JsonRequired]
        [JsonProperty("presentationFileName")]
        public string PresentationFileName { get; set; }

        #endregion

        #region Not required

        [JsonProperty("website")]
        public string Website { get; set; }

        [JsonProperty("marketSize")]
        public string MarketSize { get; set; }

        [JsonProperty("businessEconomicModel")]
        public string BusinessEconomicModel { get; set; }

        [JsonProperty("businessOperationalModel")]
        public string BusinessOperationalModel { get; set; }

        #endregion

        #region Lists

        [JsonRequired]
        [JsonProperty("foundersNames")]
        public List<string> FoundersNames { get; set; }

        [JsonRequired]
        [JsonProperty("competingCompanies")]
        public List<string> CompetingCompanies { get; set; }

        [JsonRequired]
        [JsonProperty("companyExperiences")]
        public List<InnovationOptionApiDto> CompanyExperiences { get; set; }

        [JsonRequired]
        [JsonProperty("productsOrServices")]
        public List<InnovationOptionApiDto> ProductsOrServices { get; set; }

        [JsonRequired]
        [JsonProperty("technologyExperiences")]
        public List<InnovationOptionApiDto> TechnologyExperiences { get; set; }

        [JsonRequired]
        [JsonProperty("companyObjectives")]
        public List<InnovationOptionApiDto> CompanyObjectives { get; set; }

        #endregion

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

            this.ValidateCompetingCompanies();
            this.ValidateCurriculum();
            this.ValidateDescription();
            this.ValidatePresentationFile();

            return this.ValidationResult.IsValid;
        }

        /// <summary>
        /// Validates the competing companies.
        /// </summary>
        private void ValidateCompetingCompanies()
        {
            if(this.CompetingCompanies.Count > this.CompetingCompaniesMaxCount)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityMustContainsMaxItemsCount, nameof(CompetingCompanies), this.CompetingCompaniesMaxCount), new string[] { nameof(CompetingCompanies) }));
            }
        }

        /// <summary>
        /// Validates the curriculum.
        /// </summary>
        private void ValidateCurriculum()
        {
            if (this.Curriculum.Length > this.CurriculumMaxLenght)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, nameof(Curriculum), this.CurriculumMaxLenght, 1), new string[] { nameof(Curriculum) }));
            }
        }

        /// <summary>
        /// Validates the description.
        /// </summary>
        private void ValidateDescription()
        {
            if (this.Description.Length > this.DescriptionMaxLenght)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, nameof(Curriculum), this.DescriptionMaxLenght, 1), new string[] { nameof(Curriculum) }));
            }
        }

        /// <summary>
        /// Validates the presentation file.
        /// </summary>
        private void ValidatePresentationFile()
        {
            if (!this.PresentationFile.IsBase64String())
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityMustBeBase64, nameof(PresentationFile)), new string[] { nameof(PresentationFile) }));
            }

            if (this.PresentationFile.GetBase64FileExtension() != FileType.Pdf)
            {
                this.ValidationResult.Add(new ValidationError(Messages.FileMustBePdf, new string[] { nameof(PresentationFile) }));
            }
        }

        #endregion

        #region Payload test

        /// <summary>
        /// Generates the test json.
        /// </summary>
        /// <returns></returns>
        public string GenerateTestJson()
        {
            this.Name = "ACME Toasters LTDA";
            this.Document = "49.587.570/0001-19";
            this.ServiceName = "ACME toaster machine 3000";

            this.ResponsibleName = "Sylvester Stallone";
            this.Email = "acmetoasters3000@gmail.com";
            this.PhoneNumber = "1437319489";
            this.CellPhone = "14998269754";
            this.FoundationDate = DateTime.Today;
            this.AccumulatedRevenue = 7000;
            this.Description = "My Employee (max 600 chars)";
            this.Curriculum = "My Curriculum (max 710 chars)";
            this.BusinessDefinition = "My Business definition";
            this.BusinessFocus = "My Business focus";
            this.BusinessDifferentials = "My Business differentials";
            
            this.BusinessStage = "Business stage";
            this.Website = "www.site.com.br";
            this.MarketSize = "Market size";
            this.BusinessEconomicModel = "My Business economic model";
            this.BusinessOperationalModel = "My Business operational model";

            this.PresentationFile = "PresentationFileBase64";
            this.PresentationFileName = "PresentationFileName.pdf";
            this.WorkDedicationUid = new Guid("ADA0C122-45EF-41E4-9002-EDB9E9FBDB51"); //Integral

            this.CompetingCompanies = new List<string>()
            {
                "Skynet",
                "SpaceX",
                "Cambridge Analytica"
            };
            this.FoundersNames = new List<string>()
            {
                "George Foreman",
                "Erick Jacquin"
            };
            this.CompanyExperiences = new List<InnovationOptionApiDto>()
            {
                new InnovationOptionApiDto(){ Uid = new Guid("82167C1D-7CA6-447F-80C7-AE9188ADD436") },
                new InnovationOptionApiDto(){ Uid = new Guid("29B2CC2F-374D-4F2F-AC00-3513D02EC9C3") },
                new InnovationOptionApiDto(){ Uid = new Guid("2FD9F6BA-8852-4DD5-A402-DCD2C14923CB") },
                new InnovationOptionApiDto(){ Uid = new Guid("60079B3B-A5D9-4E59-A964-725339AFBE7F") },
                new InnovationOptionApiDto(){ Uid = new Guid("4F440536-BAB7-4E43-A3A4-F977ABAFBDA8") },

            };
            this.ProductsOrServices = new List<InnovationOptionApiDto>()
            {
                new InnovationOptionApiDto(){ Uid = new Guid("702A9B2E-DBCB-4BB0-8405-C5BD72EAF627") },
                new InnovationOptionApiDto(){ Uid = new Guid("0B1BD552-AD36-4A4D-A55D-D7B07EB6E4E0") },
                new InnovationOptionApiDto(){ Uid = new Guid("FB997A1A-CBF0-446B-82FF-FF7B5EAA21BF") },
                new InnovationOptionApiDto(){ Uid = new Guid("1646A4D0-F43A-4633-9730-3F8893A627CE") },
                new InnovationOptionApiDto(){ Uid = new Guid("A7EEDEF9-88B9-4EF4-A6EA-32EA5DB28598") },
                new InnovationOptionApiDto(){ Uid = new Guid("B05FDB02-F880-49D0-818D-65BA716EC0B8") },
                new InnovationOptionApiDto(){ Uid = new Guid("DE7F9C0A-1AA4-4F7D-985D-2D56D229B3B5") },
            };
            this.TechnologyExperiences = new List<InnovationOptionApiDto>()
            {
                new InnovationOptionApiDto(){ Uid = new Guid("0EE805CD-7E63-47DE-8034-C405DC5E1DA3") },
                new InnovationOptionApiDto(){ Uid = new Guid("6932AC40-E16D-4858-B550-1B4CD2F9461D") },
                new InnovationOptionApiDto(){ Uid = new Guid("516E3187-CE60-4541-9F46-AC41F29EA0EB") },
                new InnovationOptionApiDto(){ Uid = new Guid("9B3EFFFC-B4F9-4E65-B679-69EEE581DCC2") },
                new InnovationOptionApiDto(){ Uid = new Guid("3663D8A3-DF1D-4A41-A3EF-13CF2602FA9D") },
                new InnovationOptionApiDto(){ Uid = new Guid("F5B4623D-F4F9-4440-B575-140C273C41D2") },
            };
            this.CompanyObjectives = new List<InnovationOptionApiDto>()
            {
                new InnovationOptionApiDto(){ Uid = new Guid("9ECEFE6D-EA9A-4DCE-88A0-5B720E02EAE0") },
                new InnovationOptionApiDto(){ Uid = new Guid("0A0EA283-D9CB-4BCE-87B9-5582C57B6E42") },
                new InnovationOptionApiDto(){ Uid = new Guid("0D2684E1-4A8A-4088-8547-6AC274EB1EE4") },
                new InnovationOptionApiDto(){ Uid = new Guid("064371D0-864F-4B79-B709-221F73B0D35D") },
                new InnovationOptionApiDto(){ Uid = new Guid("38A07682-8BD4-4EDE-8BE2-1593BAD8E0B7") },
                new InnovationOptionApiDto(){ Uid = new Guid("5F872B5B-DA41-43E6-ABE7-CEF7E6BC0CE6") },
                new InnovationOptionApiDto(){ Uid = new Guid("1A144DB0-DBF2-4ECC-A4C0-267CEA374FAA") },
                new InnovationOptionApiDto(){ Uid = new Guid("5F62C762-01C0-4F55-A89D-D1E5690817F6") },
            };

            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        #endregion
    }
}
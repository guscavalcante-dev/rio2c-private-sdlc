// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 28-06-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-09-2023
// ***********************************************************************
// <copyright file="InnovationOrganizationApiDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Statics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>InnovationOrganizationApiDto</summary>
    public class InnovationOrganizationApiDto
    {
        [JsonIgnore]
        public ValidationResult ValidationResult { get; set; }

        [JsonIgnore]
        private readonly string[] EnabledPresentationFileTypes = new string[] { FileType.Pdf };

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
        [JsonProperty("responsibleName")]
        public string ResponsibleName { get; set; }

        [JsonRequired]
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonRequired]
        [JsonProperty("cellPhone")]
        public string CellPhone { get; set; }

        [JsonRequired]
        [JsonProperty("accumulatedRevenueThreeMonths")]
        public decimal AccumulatedRevenue { get; set; }

        [JsonRequired]
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonRequired]
        [JsonProperty("businessFocus")]
        public string BusinessFocus { get; set; }

        [JsonRequired]
        [JsonProperty("businessDifferentials")]
        public string BusinessDifferentials { get; set; }

        [JsonRequired]
        [JsonProperty("businessStage")]
        public string BusinessStage { get; set; }

        [JsonRequired]
        [JsonProperty("imageFile", Order = 98)]
        public string ImageFile { get; set; }

        [JsonRequired]
        [JsonProperty("presentationFile", Order = 99)]
        public string PresentationFile { get; set; }

        [JsonRequired]
        [JsonProperty("wouldYouLikeParticipateBusinessRound", Order = 100)]
        public bool WouldYouLikeParticipateBusinessRound { get; set; }

        [JsonRequired]
        [JsonProperty("wouldYouLikeParticipatePitching", Order = 101)]
        public bool WouldYouLikeParticipatePitching { get; set; }

        [JsonProperty("accumulatedRevenueForLastTwelveMonths", Order = 102)]
        public decimal? AccumulatedRevenueForLastTwelveMonths { get; set; }

        [JsonProperty("businessFoundationYear", Order = 103)]
        public int? BusinessFoundationYear { get; set; }

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

        #region Json Ignore

        [JsonIgnore]
        public string PresentationFileName { get; set; }

        #endregion

        #region Lists

        [JsonRequired]
        [JsonProperty("founders")]
        public List<AttendeeInnovationOrganizationFounderApiDto> AttendeeInnovationOrganizationFounderApiDtos { get; set; }

        [JsonRequired]
        [JsonProperty("competingCompanies")]
        public List<AttendeeInnovationOrganizationCompetitorApiDto> AttendeeInnovationOrganizationCompetitorApiDtos { get; set; }

        [JsonProperty("companyExperiences")]
        public List<InnovationOrganizationExperienceOptionApiDto> InnovationOrganizationExperienceOptionApiDtos { get; set; }

        [JsonRequired]
        [JsonProperty("organizationVerticalsAndCreativeEconomyThemes")] // "OrganizationTracks" was changed to "OrganizationVerticalsAndCreativeEconomyThemes" by customer request.
        public List<InnovationOrganizationTrackOptionApiDto> InnovationOrganizationTrackOptionApiDtos { get; set; }

        [JsonProperty("technologyExperiences")]
        public List<InnovationOrganizationTechnologyOptionApiDto> InnovationOrganizationTechnologyOptionApiDtos { get; set; }

        [JsonRequired]
        [JsonProperty("companyObjectives")]
        public List<InnovationOrganizationObjectivesOptionApiDto> InnovationOrganizationObjectivesOptionApiDtos { get; set; }

        [JsonProperty("sustainableDevelopmentObjectives")]
        public List<InnovationOrganizationSustainableDevelopmentObjectivesOptionApiDto> InnovationOrganizationSustainableDevelopmentObjectivesOptionApiDtos { get; set; }

        #endregion

        /// <summary>Initializes a new instance of the <see cref="InnovationOrganizationApiDto"/> class.</summary>
        public InnovationOrganizationApiDto()
        {
            this.WouldYouLikeParticipateBusinessRound = false;
        }

        #region Validations

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        public bool IsValid()
        {
            this.ValidationResult = new ValidationResult();
            this.ValidateFieldsRequired();
            this.ValidatePresentationFile();
            this.ValidateWhenWouldYouLikeParticipateBusinessRound();
            return this.ValidationResult.IsValid;
        }

        /// <summary>
        /// Validates the fields required.
        /// </summary>
        private void ValidateFieldsRequired()
        {
            #region Validate Fields

            if (this.Name == null || this.Name?.Length == 0)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, nameof(Name)), new string[] { nameof(Name) }));
            }

            if (this.Document == null || this.Document?.Length == 0)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, nameof(Document)), new string[] { nameof(Document) }));
            }

            if (this.ServiceName == null || this.ServiceName?.Length == 0)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, nameof(ServiceName)), new string[] { nameof(ServiceName) }));
            }

            if (this.ResponsibleName == null || this.ResponsibleName?.Length == 0)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, nameof(ResponsibleName)), new string[] { nameof(ResponsibleName) }));
            }

            if (this.ResponsibleName == null || this.ResponsibleName?.Length == 0)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, nameof(ResponsibleName)), new string[] { nameof(ResponsibleName) }));
            }

            if (this.Email == null || this.Email?.Length == 0)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, nameof(Email)), new string[] { nameof(Email) }));
            }

            if (this.CellPhone == null || this.CellPhone?.Length == 0)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, nameof(CellPhone)), new string[] { nameof(CellPhone) }));
            }

            if (this.AccumulatedRevenue <= 0)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, nameof(AccumulatedRevenue)), new string[] { nameof(AccumulatedRevenue) }));
            }

            if (this.Description == null || this.Description?.Length == 0)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, nameof(Description)), new string[] { nameof(Description) }));
            }

            if (this.ImageFile == null || this.ImageFile?.Length == 0)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, nameof(ImageFile)), new string[] { nameof(ImageFile) }));
            }

            if (this.BusinessFocus == null || this.BusinessFocus?.Length == 0)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, nameof(BusinessFocus)), new string[] { nameof(BusinessFocus) }));
            }

            if (this.MarketSize == null || this.MarketSize?.Length == 0)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, nameof(MarketSize)), new string[] { nameof(MarketSize) }));
            }

            if (this.BusinessDifferentials == null || this.BusinessDifferentials?.Length == 0)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, nameof(BusinessDifferentials)), new string[] { nameof(BusinessDifferentials) }));
            }

            if (this.BusinessStage == null || this.BusinessStage?.Length == 0)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, nameof(BusinessStage)), new string[] { nameof(BusinessStage) }));
            }

            if (this.PresentationFile == null || this.PresentationFile?.Length == 0)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, nameof(PresentationFile)), new string[] { nameof(PresentationFile) }));
            }

            #endregion

            #region Validate Lists

            if (this.AttendeeInnovationOrganizationFounderApiDtos == null ||
                !this.AttendeeInnovationOrganizationFounderApiDtos.Any())
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, "founders"), new string[] { "founders" }));
            }

            if (this.AttendeeInnovationOrganizationCompetitorApiDtos == null ||
                !this.AttendeeInnovationOrganizationCompetitorApiDtos.Any())
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, "competingCompanies"), new string[] { "competingCompanies" }));
            }

            if (this.InnovationOrganizationExperienceOptionApiDtos == null ||
                !this.InnovationOrganizationExperienceOptionApiDtos.Any())
            {
                //Actually isn't required
                //this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, "companyExperiences"), new string[] { "companyExperiences" }));
            }

            if (this.InnovationOrganizationTrackOptionApiDtos == null ||
                !this.InnovationOrganizationTrackOptionApiDtos.Any())
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, "organizationVerticalsAndCreativeEconomyThemes"), new string[] { "organizationVerticalsAndCreativeEconomyThemes" }));
            }

            if (this.InnovationOrganizationTechnologyOptionApiDtos == null ||
                !this.InnovationOrganizationTechnologyOptionApiDtos.Any())
            {
                //Actually isn't required
                //this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, "technologyExperiences"), new string[] { "technologyExperiences" }));
            }

            if (this.InnovationOrganizationObjectivesOptionApiDtos == null ||
                !this.InnovationOrganizationObjectivesOptionApiDtos.Any())
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, "companyObjectives"), new string[] { "companyObjectives" }));
            }

            if (this.InnovationOrganizationSustainableDevelopmentObjectivesOptionApiDtos == null ||
                !this.InnovationOrganizationSustainableDevelopmentObjectivesOptionApiDtos.Any())
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, "sustainableDevelopmentObjectives"), new string[] { "sustainableDevelopmentObjectives" }));
            }

            #endregion

            #region Validate Childs

            if (this.AttendeeInnovationOrganizationFounderApiDtos?.Any() == true)
            {
                foreach (var attendeeInnovationOrganizationFounderApiDto in this.AttendeeInnovationOrganizationFounderApiDtos.Where(i => !i.IsValid()))
                {
                    attendeeInnovationOrganizationFounderApiDto.ValidationResult.AddErrorsPrefixMessage(
                        string.Format("{0}[{1}]", GetJsonPropertyAttributeName(nameof(AttendeeInnovationOrganizationFounderApiDtos)),
                                                   this.AttendeeInnovationOrganizationFounderApiDtos.IndexOf(attendeeInnovationOrganizationFounderApiDto)));

                    this.ValidationResult.Add(attendeeInnovationOrganizationFounderApiDto.ValidationResult);
                }
            }

            if (this.InnovationOrganizationTrackOptionApiDtos?.Any() == true)
            {
                foreach (var innovationOrganizationTrackOptionApiDto in this.InnovationOrganizationTrackOptionApiDtos.Where(i => !i.IsValid()))
                {
                    innovationOrganizationTrackOptionApiDto.ValidationResult.AddErrorsPrefixMessage(
                        string.Format("{0}[{1}]", GetJsonPropertyAttributeName(nameof(InnovationOrganizationTrackOptionApiDtos)),
                                                   this.InnovationOrganizationTrackOptionApiDtos.IndexOf(innovationOrganizationTrackOptionApiDto)));

                    this.ValidationResult.Add(innovationOrganizationTrackOptionApiDto.ValidationResult);
                }
            }

            #endregion
        }

        /// <summary>
        /// Validates the presentation file.
        /// </summary>
        private void ValidatePresentationFile()
        {
            if (string.IsNullOrEmpty(this.PresentationFile))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, nameof(PresentationFile)), new string[] { nameof(PresentationFile) }));
            }

            if (!this.PresentationFile.IsBase64String())
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityMustBeBase64, nameof(PresentationFile)), new string[] { nameof(PresentationFile) }));
            }

            if (!this.EnabledPresentationFileTypes.Contains(this.PresentationFile.GetBase64FileExtension()))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.FileTypeMustBe, this.EnabledPresentationFileTypes.ToString(",")), new string[] { nameof(PresentationFile) }));
            }
        }

        /// <summary>
        /// Validates the when would you like participate business round.
        /// </summary>
        private void ValidateWhenWouldYouLikeParticipateBusinessRound()
        {
            if (this.WouldYouLikeParticipateBusinessRound)
            {
                if (!this.AccumulatedRevenueForLastTwelveMonths.HasValue)
                {
                    this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, nameof(AccumulatedRevenueForLastTwelveMonths)), new string[] { nameof(AccumulatedRevenueForLastTwelveMonths) }));
                }

                if (!this.BusinessFoundationYear.HasValue)
                {
                    this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, nameof(BusinessFoundationYear)), new string[] { nameof(BusinessFoundationYear) }));
                }
            }
        }

        /// <summary>
        /// Gets the name of the json property attribute.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        public static string GetJsonPropertyAttributeName(string propertyName)
        {
            return typeof(InnovationOrganizationApiDto)
                    .GetProperty(propertyName)?
                    .GetCustomAttribute<JsonPropertyAttribute>()?.PropertyName;
        }

        #endregion

        #region Payload test

        /// <summary>
        /// Generates the test json.
        /// </summary>
        /// <returns></returns>
        public static string GenerateTestJson()
        {
            InnovationOrganizationApiDto i = new InnovationOrganizationApiDto();
            i.Name = "ACME Toasters LTDA";
            i.Document = "49.587.570/0001-19";
            i.ServiceName = "ACME toaster machine 3000";
            i.ResponsibleName = "Sylvester Stallone";
            i.Email = "acmetoasters3000@gmail.com";
            i.ImageFile = "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";
            i.CellPhone = "14998269754";
            i.AccumulatedRevenue = 7000;
            i.Description = "My Employee (max 600 chars)";
            i.BusinessFocus = "My Business focus";
            i.BusinessDifferentials = "My Business differentials";

            i.BusinessStage = "Business stage";
            i.Website = "www.site.com.br";
            i.MarketSize = "Market size";
            i.BusinessEconomicModel = "My Business economic model";
            i.BusinessOperationalModel = "My Business operational model";

            i.PresentationFile = "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CmVuZG9iagoKOSAwIG9iago8PC9UeXBlL0ZvbnQvU3VidHlwZS9UcnVlVHlwZS9CYXNlRm9udC9CQUFBQUErQXJpYWwtQm9sZE1UCi9GaXJzdENoYXIgMAovTGFzdENoYXIgMTEKL1dpZHRoc1s3NTAgNzIyIDYxMCA4ODkgNTU2IDI3NyA2NjYgNjEwIDMzMyAyNzcgMjc3IDU1NiBdCi9Gb250RGVzY3JpcHRvciA3IDAgUgovVG9Vbmljb2RlIDggMCBSCj4+CmVuZG9iagoKMTAgMCBvYmoKPDwKL0YxIDkgMCBSCj4+CmVuZG9iagoKMTEgMCBvYmoKPDwvRm9udCAxMCAwIFIKL1Byb2NTZXRbL1BERi9UZXh0XT4+CmVuZG9iagoKMSAwIG9iago8PC9UeXBlL1BhZ2UvUGFyZW50IDQgMCBSL1Jlc291cmNlcyAxMSAwIFIvTWVkaWFCb3hbMCAwIDU5NSA4NDJdL0dyb3VwPDwvUy9UcmFuc3BhcmVuY3kvQ1MvRGV2aWNlUkdCL0kgdHJ1ZT4+L0NvbnRlbnRzIDIgMCBSPj4KZW5kb2JqCgoxMiAwIG9iago8PC9Db3VudCAxL0ZpcnN0IDEzIDAgUi9MYXN0IDEzIDAgUgo+PgplbmRvYmoKCjEzIDAgb2JqCjw8L1RpdGxlPEZFRkYwMDQ0MDA3NTAwNkQwMDZEMDA3OTAwMjAwMDUwMDA0NDAwNDYwMDIwMDA2NjAwNjkwMDZDMDA2NT4KL0Rlc3RbMSAwIFIvWFlaIDU2LjcgNzczLjMgMF0vUGFyZW50IDEyIDAgUj4+CmVuZG9iagoKNCAwIG9iago8PC9UeXBlL1BhZ2VzCi9SZXNvdXJjZXMgMTEgMCBSCi9NZWRpYUJveFsgMCAwIDU5NSA4NDIgXQovS2lkc1sgMSAwIFIgXQovQ291bnQgMT4+CmVuZG9iagoKMTQgMCBvYmoKPDwvVHlwZS9DYXRhbG9nL1BhZ2VzIDQgMCBSCi9PdXRsaW5lcyAxMiAwIFIKPj4KZW5kb2JqCgoxNSAwIG9iago8PC9BdXRob3I8RkVGRjAwNDUwMDc2MDA2MTAwNkUwMDY3MDA2NTAwNkMwMDZGMDA3MzAwMjAwMDU2MDA2QzAwNjEwMDYzMDA2ODAwNkYwMDY3MDA2OTAwNjEwMDZFMDA2RTAwNjkwMDczPgovQ3JlYXRvcjxGRUZGMDA1NzAwNzIwMDY5MDA3NDAwNjUwMDcyPgovUHJvZHVjZXI8RkVGRjAwNEYwMDcwMDA2NTAwNkUwMDRGMDA2NjAwNjYwMDY5MDA2MzAwNjUwMDJFMDA2RjAwNzIwMDY3MDAyMDAwMzIwMDJFMDAzMT4KL0NyZWF0aW9uRGF0ZShEOjIwMDcwMjIzMTc1NjM3KzAyJzAwJyk+PgplbmRvYmoKCnhyZWYKMCAxNgowMDAwMDAwMDAwIDY1NTM1IGYgCjAwMDAwMTE5OTcgMDAwMDAgbiAKMDAwMDAwMDAxOSAwMDAwMCBuIAowMDAwMDAwMjI0IDAwMDAwIG4gCjAwMDAwMTIzMzAgMDAwMDAgbiAKMDAwMDAwMDI0NCAwMDAwMCBuIAowMDAwMDExMTU0IDAwMDAwIG4gCjAwMDAwMTExNzYgMDAwMDAgbiAKMDAwMDAxMTM2OCAwMDAwMCBuIAowMDAwMDExNzA5IDAwMDAwIG4gCjAwMDAwMTE5MTAgMDAwMDAgbiAKMDAwMDAxMTk0MyAwMDAwMCBuIAowMDAwMDEyMTQwIDAwMDAwIG4gCjAwMDAwMTIxOTYgMDAwMDAgbiAKMDAwMDAxMjQyOSAwMDAwMCBuIAowMDAwMDEyNDk0IDAwMDAwIG4gCnRyYWlsZXIKPDwvU2l6ZSAxNi9Sb290IDE0IDAgUgovSW5mbyAxNSAwIFIKL0lEIFsgPEY3RDc3QjNEMjJCOUY5MjgyOUQ0OUZGNUQ3OEI4RjI4Pgo8RjdENzdCM0QyMkI5RjkyODI5RDQ5RkY1RDc4QjhGMjg+IF0KPj4Kc3RhcnR4cmVmCjEyNzg3CiUlRU9GCg==";
            i.PresentationFileName = "PresentationFileName.pdf";

            i.AttendeeInnovationOrganizationCompetitorApiDtos = new List<AttendeeInnovationOrganizationCompetitorApiDto>()
            {
                new AttendeeInnovationOrganizationCompetitorApiDto()
                {
                    Name = "Skynet"
                },
                new AttendeeInnovationOrganizationCompetitorApiDto()
                {
                    Name = "SpaceX"
                },
                new AttendeeInnovationOrganizationCompetitorApiDto()
                {
                    Name = "Cambridge Analytica"
                }
            };

            i.AttendeeInnovationOrganizationFounderApiDtos = new List<AttendeeInnovationOrganizationFounderApiDto>()
            {
                new AttendeeInnovationOrganizationFounderApiDto()
                {
                    FullName = "George Foreman",
                    Curriculum = "George Foreman's curriculum.",
                    WorkDedicationUid = new Guid("ADA0C122-45EF-41E4-9002-EDB9E9FBDB51") //Integral
                },
                new AttendeeInnovationOrganizationFounderApiDto()
                {
                    FullName = "Erick Jacquin",
                    Curriculum = "Erick Jacquin's curriculum.",
                    WorkDedicationUid = new Guid("DCC9878D-EBC7-438C-8A0A-5952B75A8B54") //Parcial
                },
            };

            i.InnovationOrganizationExperienceOptionApiDtos = new List<InnovationOrganizationExperienceOptionApiDto>()
            {
                new InnovationOrganizationExperienceOptionApiDto() { Uid = new Guid("82167C1D-7CA6-447F-80C7-AE9188ADD436"), AdditionalInfo = "" },
                new InnovationOrganizationExperienceOptionApiDto() { Uid = new Guid("29B2CC2F-374D-4F2F-AC00-3513D02EC9C3"), AdditionalInfo = "" },
                new InnovationOrganizationExperienceOptionApiDto() { Uid = new Guid("2FD9F6BA-8852-4DD5-A402-DCD2C14923CB"), AdditionalInfo = "" },
                new InnovationOrganizationExperienceOptionApiDto() { Uid = new Guid("60079B3B-A5D9-4E59-A964-725339AFBE7F"), AdditionalInfo = "" },
                new InnovationOrganizationExperienceOptionApiDto() { Uid = new Guid("4F440536-BAB7-4E43-A3A4-F977ABAFBDA8"), AdditionalInfo = "" },
            };

            i.InnovationOrganizationTrackOptionApiDtos = new List<InnovationOrganizationTrackOptionApiDto>()
            {
                new InnovationOrganizationTrackOptionApiDto(){ Uid = new Guid("702A9B2E-DBCB-4BB0-8405-C5BD72EAF627"), AdditionalInfo = ""},
                new InnovationOrganizationTrackOptionApiDto(){ Uid = new Guid("0B1BD552-AD36-4A4D-A55D-D7B07EB6E4E0"), AdditionalInfo = ""},
                new InnovationOrganizationTrackOptionApiDto(){ Uid = new Guid("FB997A1A-CBF0-446B-82FF-FF7B5EAA21BF"), AdditionalInfo = ""},
                new InnovationOrganizationTrackOptionApiDto(){ Uid = new Guid("1646A4D0-F43A-4633-9730-3F8893A627CE"), AdditionalInfo = ""},
                new InnovationOrganizationTrackOptionApiDto(){ Uid = new Guid("A7EEDEF9-88B9-4EF4-A6EA-32EA5DB28598"), AdditionalInfo = ""},
                new InnovationOrganizationTrackOptionApiDto(){ Uid = new Guid("B05FDB02-F880-49D0-818D-65BA716EC0B8"), AdditionalInfo = ""},
            };

            i.InnovationOrganizationTechnologyOptionApiDtos = new List<InnovationOrganizationTechnologyOptionApiDto>()
            {
                new InnovationOrganizationTechnologyOptionApiDto() { Uid = new Guid("0EE805CD-7E63-47DE-8034-C405DC5E1DA3"), AdditionalInfo = ""},
                new InnovationOrganizationTechnologyOptionApiDto() { Uid = new Guid("6932AC40-E16D-4858-B550-1B4CD2F9461D"), AdditionalInfo = ""},
                new InnovationOrganizationTechnologyOptionApiDto() { Uid = new Guid("516E3187-CE60-4541-9F46-AC41F29EA0EB"), AdditionalInfo = ""},
                new InnovationOrganizationTechnologyOptionApiDto() { Uid = new Guid("9B3EFFFC-B4F9-4E65-B679-69EEE581DCC2"), AdditionalInfo = ""},
                new InnovationOrganizationTechnologyOptionApiDto() { Uid = new Guid("3663D8A3-DF1D-4A41-A3EF-13CF2602FA9D"), AdditionalInfo = ""},
                new InnovationOrganizationTechnologyOptionApiDto() { Uid = new Guid("F5B4623D-F4F9-4440-B575-140C273C41D2"), AdditionalInfo = "Microsserviços & CloudPlatform"},
            };

            i.InnovationOrganizationObjectivesOptionApiDtos = new List<InnovationOrganizationObjectivesOptionApiDto>()
            {
                new InnovationOrganizationObjectivesOptionApiDto() { Uid = new Guid("9ECEFE6D-EA9A-4DCE-88A0-5B720E02EAE0"), AdditionalInfo = ""},
                new InnovationOrganizationObjectivesOptionApiDto() { Uid = new Guid("0A0EA283-D9CB-4BCE-87B9-5582C57B6E42"), AdditionalInfo = ""},
                new InnovationOrganizationObjectivesOptionApiDto() { Uid = new Guid("0D2684E1-4A8A-4088-8547-6AC274EB1EE4"), AdditionalInfo = ""},
                new InnovationOrganizationObjectivesOptionApiDto() { Uid = new Guid("064371D0-864F-4B79-B709-221F73B0D35D"), AdditionalInfo = ""},
                new InnovationOrganizationObjectivesOptionApiDto() { Uid = new Guid("38A07682-8BD4-4EDE-8BE2-1593BAD8E0B7"), AdditionalInfo = ""},
                new InnovationOrganizationObjectivesOptionApiDto() { Uid = new Guid("5F872B5B-DA41-43E6-ABE7-CEF7E6BC0CE6"), AdditionalInfo = ""},
                new InnovationOrganizationObjectivesOptionApiDto() { Uid = new Guid("1A144DB0-DBF2-4ECC-A4C0-267CEA374FAA"), AdditionalInfo = ""},
                new InnovationOrganizationObjectivesOptionApiDto() { Uid = new Guid("5F62C762-01C0-4F55-A89D-D1E5690817F6"), AdditionalInfo = "Captação de contatos"},
            };

            return Newtonsoft.Json.JsonConvert.SerializeObject(i);
        }

        #endregion
    }
}
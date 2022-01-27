// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 01-25-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-25-2022
// ***********************************************************************
// <copyright file="CartoonProjectApiDto.cs" company="Softo">
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
using System.Linq;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>CartoonProjectApiDto</summary>
    public class CartoonProjectApiDto
    {
        [JsonRequired]
        [JsonProperty("contactDocument")]
        public string ContactDocument { get; set; }

        [JsonRequired]
        [JsonProperty("contactFistName")]
        public string ContactFistName { get; set; }

        [JsonRequired]
        [JsonProperty("contactLastName")]
        public string ContactLastName { get; set; }

        [JsonRequired]
        [JsonProperty("contactEmail")]
        public string ContactEmail { get; set; }

        [JsonRequired]
        [JsonProperty("contactCellPhone")]
        public string ContactCellPhone { get; set; }

        [JsonProperty("contactPhoneNumber")]
        public string ContactPhoneNumber { get; set; }

        [JsonRequired]
        [JsonProperty("creatorsMiniBio")]
        public string CreatorsMiniBio { get; set; }

        [JsonRequired]
        [JsonProperty("numberOfEpisodes")]
        public int NumberOfEpisodes { get; set; }

        [JsonRequired]
        [JsonProperty("eachEpisodePlayingTime")]
        public string EachEpisodePlayingTime { get; set; }

        [JsonProperty("totalValueOfProject")]
        public string TotalValueOfProject { get; set; }

        [JsonRequired]
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonRequired]
        [JsonProperty("logline")]
        public string Logline { get; set; }

        [JsonRequired]
        [JsonProperty("synopsis")]
        public string Summary { get; set; }

        [JsonRequired]
        [JsonProperty("motivation")]
        public string Motivation { get; set; }

        [JsonRequired]
        [JsonProperty("productionPlan")]
        public string ProductionPlan { get; set; }

        [JsonRequired]
        [JsonProperty("projectBibleUrl")]
        public string ProjectBibleUrl { get; set; }

        [JsonProperty("projectTeaserUrl")]
        public string ProjectTeaserUrl { get; set; }

        [JsonRequired]
        [JsonProperty("projectFormatUid")]
        public Guid ProjectFormatUid { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CartoonProjectApiDto"/> class.</summary>
        public CartoonProjectApiDto()
        {
        }

        #region Validations
        
        [JsonIgnore]
        public ValidationResult ValidationResult { get; set; }

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        public bool IsValid()
        {
            this.ValidationResult = new ValidationResult();
            this.CustomValidation();

            return this.ValidationResult.IsValid;
        }

        /// <summary>
        /// Validates the presentation file.
        /// </summary>
        private void CustomValidation()
        {
            //if (string.IsNullOrEmpty(this.PresentationFile))
            //{
            //    this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, nameof(PresentationFile)), new string[] { nameof(PresentationFile) }));
            //}

            //if (!this.PresentationFile.IsBase64String())
            //{
            //    this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityMustBeBase64, nameof(PresentationFile)), new string[] { nameof(PresentationFile) }));
            //}

            //if (!this.EnabledPresentationFileTypes.Contains(this.PresentationFile.GetBase64FileExtension()))
            //{
            //    this.ValidationResult.Add(new ValidationError(string.Format(Messages.FileTypeMustBe, this.EnabledPresentationFileTypes.ToString(",")), new string[] { nameof(PresentationFile) }));
            //}
        }

        #endregion

        #region Payload test

        /// <summary>
        /// Generates the test json.
        /// </summary>
        /// <returns></returns>
        public static string GenerateTestJson()
        {
            CartoonProjectApiDto i = new CartoonProjectApiDto()
            {
                Title = "Oggy and the Cockroaches",

                ContactFistName = "Oggy",
                ContactLastName = "Jack",
                ContactDocument = "892.108.070-81",
                ContactEmail = "renan.valentim+99@sof.to",
                ContactCellPhone = "+55 11 998369856",
                ContactPhoneNumber = "+55 11 37319856",
                CreatorsMiniBio = "My MiniBio with 3000 max lenght.",
                EachEpisodePlayingTime = "00:20:00",
                NumberOfEpisodes = 5,
                Logline = "My LogLine with 3000 max lenght.",
                Motivation = "The Creative Motivation",
                ProductionPlan = "ProductionPlan",
                ProjectTeaserUrl = "ProjectTeaserUrl",
                ProjectBibleUrl = "ProjectBibleUrl",
                Summary = "Summary",
                TotalValueOfProject = "US$ 10.000.000",
                ProjectFormatUid = new Guid("44ab63de-66ba-4032-b9ec-171539413e85")
            };

            return Newtonsoft.Json.JsonConvert.SerializeObject(i);
        }

        #endregion
    }
}
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

        [JsonRequired]
        [JsonProperty("company")]
        public CartoonProjectCompanyApiDto CartoonProjectCompanyApiDto { get; set; }

        [JsonRequired]
        [JsonProperty("creators")]
        public List<CartoonProjectCreatorApiDto> CartoonProjectCreatorApiDtos { get; set; }

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
            this.ValidateCartoonProjectCreators();

            return this.ValidationResult.IsValid;
        }

        /// <summary>
        /// Validates the cartoon project creators.
        /// </summary>
        private void ValidateCartoonProjectCreators()
        {
            if (this.CartoonProjectCreatorApiDtos.Count(i => i.IsResponsible) == 0)
            {
                this.ValidationResult.Add(new ValidationError(Messages.AtLeastOneCreatorMustBeMarkedAsResponsible));
            }

            if (this.CartoonProjectCreatorApiDtos.Count(i => i.IsResponsible) > 1)
            {
                this.ValidationResult.Add(new ValidationError(Messages.OnlyOneCreatorMustBeMarkedAsResponsible));
            }
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

            i.CartoonProjectCompanyApiDto = new CartoonProjectCompanyApiDto()
            {
                Name = "Xilam Animation",
                TradeName = "Xilam Animation's Trade Name",
                Document = "07.294.769/0001-09",
                PhoneNumber = "(14) 99999-9999",
                Address = "Rua Elziabeth Jesus de Freitas, 94, Avaré/SP",
                ZipCode = "18706280",
                ReelUrl = "www.youtube.com/my-reel-url"
            };

            i.CartoonProjectCreatorApiDtos = new List<CartoonProjectCreatorApiDto>()
            {
                new CartoonProjectCreatorApiDto()
                {
                    FirstName = "Jean-Yves",
                    LastName = "Raimbaud",
                    Document = "451756930",
                    Email = "jean@xilamtv.com",
                    CellPhone = "(14) 88888-8888",
                    PhoneNumber = "(14) 77777-7777",
                    MiniBio = "Jean-Yves Minibio's",
                    IsResponsible = true
                },
                new CartoonProjectCreatorApiDto()
                {
                    FirstName = "Olivier",
                    LastName = "Jean-Marie",
                    Document = "451756931",
                    Email = "oliver@xilamtv.com",
                    CellPhone = "(14) 66666-6666",
                    PhoneNumber = "(14) 55555-5555",
                    MiniBio = "Olivier Minibio's",
                    IsResponsible = false
                },
            };

            return Newtonsoft.Json.JsonConvert.SerializeObject(i);
        }

        #endregion
    }
}
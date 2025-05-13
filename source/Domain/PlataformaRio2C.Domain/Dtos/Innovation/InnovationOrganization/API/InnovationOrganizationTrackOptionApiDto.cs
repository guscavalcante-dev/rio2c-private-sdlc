// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-14-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-09-2023
// ***********************************************************************
// <copyright file="InnovationOrganizationTrackOptionApiDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>InnovationOrganizationTrackOptionApiDto</summary>
    public class InnovationOrganizationTrackOptionApiDto
    {
        [JsonIgnore]
        public ValidationResult ValidationResult { get; set; }

        [JsonRequired]
        [JsonProperty("uid")]
        public Guid? Uid { get; set; }

        [JsonProperty("additionalInfo")]
        public string AdditionalInfo { get; set; }

        [JsonIgnore]
        public InnovationOrganizationTrackOption InnovationOrganizationTrackOption { get; set; }

        /// <summary>Initializes a new instance of the <see cref="InnovationOrganizationTrackOptionApiDto"/> class.</summary>
        public InnovationOrganizationTrackOptionApiDto()
        {
        }

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        public bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            if (!this.Uid.HasValue)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, nameof(Uid)), new string[] { nameof(Uid) }));
            }

            return this.ValidationResult.IsValid;
        }
    }
}
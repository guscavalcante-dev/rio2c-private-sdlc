// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 03-30-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-30-2021
// ***********************************************************************
// <copyright file="MusicBandResponsibleApiDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using PlataformaRio2C.Domain.Validation;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>MusicBandResponsibleApiDto</summary>
    public class MusicBandResponsibleApiDto
    {
        [JsonRequired]
        [JsonProperty(PropertyName = "name", Order = 100)]
        public string Name { get; set; }

        [JsonRequired]
        [JsonProperty(PropertyName = "email", Order = 200)]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "phoneNumber", Order = 300)]
        public string PhoneNumber { get; set; }

        [JsonProperty(PropertyName = "cellPhone", Order = 400)]
        public string CellPhone { get; set; }

        [JsonProperty(PropertyName = "document", Order = 500)]
        public string Document { get; set; }

        /// <summary>Initializes a new instance of the <see cref="MusicBandResponsibleApiDto"/> class.</summary>
        public MusicBandResponsibleApiDto()
        {
        }

        #region Validations

        private ValidationResult ValidationResult { get; set; }

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        public bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            if (string.IsNullOrEmpty(this.Email))
            {
                this.ValidationResult.Add(new ValidationError("The responsible must have a email!", new string[] { "Email" }));
            }

            return this.ValidationResult.IsValid;
        }

        #endregion
    }
}
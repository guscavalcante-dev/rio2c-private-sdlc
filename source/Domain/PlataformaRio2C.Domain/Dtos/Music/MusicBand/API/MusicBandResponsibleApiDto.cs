// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 03-30-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-26-2024
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

        [JsonProperty(PropertyName = "stagename", Order = 150)]
        public string StageName { get; set; }

        [JsonRequired]
        [JsonProperty(PropertyName = "email", Order = 200)]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "phoneNumber", Order = 300)]
        public string PhoneNumber { get; set; }

        [JsonRequired]
        [JsonProperty(PropertyName = "document", Order = 500)]
        public string Document { get; set; }

        public bool IsCompany { get; set; }

        [JsonRequired]
        [JsonProperty(PropertyName = "address", Order = 600)]
        public string Address { get; set; }

        [JsonRequired]
        [JsonProperty(PropertyName = "country", Order = 700)]
        public string Country { get; set; }

        [JsonRequired]
        [JsonProperty(PropertyName = "state", Order = 800)]
        public string State { get; set; }

        [JsonRequired]
        [JsonProperty(PropertyName = "city", Order = 900)]
        public string City { get; set; }

        [JsonRequired]
        [JsonProperty(PropertyName = "zipCode", Order = 1000)]
        public string ZipCode { get; set; }

        /// <summary>Initializes a new instance of the <see cref="MusicBandResponsibleApiDto"/> class.</summary>
        public MusicBandResponsibleApiDto()
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

            if (string.IsNullOrEmpty(this.Name))
            {
                this.ValidationResult.Add(new ValidationError("The Responsible must have a Name!", new string[] { "Name" }));
            }

            if (string.IsNullOrEmpty(this.Email))
            {
                this.ValidationResult.Add(new ValidationError("The Responsible must have an Email!", new string[] { "Email" }));
            }

            if (string.IsNullOrEmpty(this.Document))
            {
                this.ValidationResult.Add(new ValidationError("The Responsible must have a Document!", new string[] { "Document" }));
            }

            if (string.IsNullOrEmpty(this.Address))
            {
                this.ValidationResult.Add(new ValidationError("The Responsible must have an Address!", new string[] { "Address" }));
            }

            if (string.IsNullOrEmpty(this.Country))
            {
                this.ValidationResult.Add(new ValidationError("The Responsible must have a Country!", new string[] { "Country" }));
            }

            if (string.IsNullOrEmpty(this.State))
            {
                this.ValidationResult.Add(new ValidationError("The Responsible must have a State!", new string[] { "State" }));
            }

            if (string.IsNullOrEmpty(this.City))
            {
                this.ValidationResult.Add(new ValidationError("The Responsible must have a City!", new string[] { "City" }));
            }

            if (string.IsNullOrEmpty(this.ZipCode))
            {
                this.ValidationResult.Add(new ValidationError("The Responsible must have a Zip Code!", new string[] { "ZipCode" }));
            }

            return this.ValidationResult.IsValid;
        }

        #endregion
    }
}
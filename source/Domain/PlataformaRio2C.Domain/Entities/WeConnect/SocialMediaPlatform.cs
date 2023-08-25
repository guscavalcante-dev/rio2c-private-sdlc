// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 08-16-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-24-2023
// ***********************************************************************
// <copyright file="SocialMediaPlatform.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>
    /// Class SocialMediaPlatforms.
    /// Implements the <see cref="PlataformaRio2C.Domain.Entities.AggregateRoot" />
    /// </summary>
    /// <seealso cref="PlataformaRio2C.Domain.Entities.AggregateRoot" />
    public class SocialMediaPlatform : Entity
    {
        public static readonly int NameMaxLenght = 50;
        public static readonly int ApiKeyMaxLenght = 200;
        public static readonly int EndpointUrlMaxLenght = 1000;
        public static readonly int PublicationsRootUrlMaxLenght = 500;

        public string Name { get; private set; }
        public string ApiKey { get; private set; }
        public string EndpointUrl { get; private set; }
        public string PublicationsRootUrl { get; private set; }
        public bool IsSyncActive { get; private set; }

        public virtual List<WeConnectPublication> WeConnectPublications { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SocialMediaPlatform"/> class.
        /// </summary>
        public SocialMediaPlatform()
        {
        }

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateName();
            this.ValidateApiKey();
            this.ValidateEndpointUrl();
            this.ValidatePublicationsRootUrl();

            return this.ValidationResult.IsValid;
        }

        /// <summary>
        /// Validates the name.
        /// </summary>
        private void ValidateName()
        {
            if (this.Name.Length > NameMaxLenght)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, nameof(Name), NameMaxLenght, 1), new string[] { nameof(Name) }));
            }
        }

        /// <summary>
        /// Validates the API key.
        /// </summary>
        private void ValidateApiKey()
        {
            if (this.ApiKey.Length > ApiKeyMaxLenght)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, nameof(ApiKey), ApiKeyMaxLenght, 1), new string[] { nameof(ApiKey) }));
            }
        }

        /// <summary>
        /// Validates the endpoint URL.
        /// </summary>
        private void ValidateEndpointUrl()
        {
            if (this.EndpointUrl.Length > EndpointUrlMaxLenght)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, nameof(EndpointUrl), EndpointUrlMaxLenght, 1), new string[] { nameof(EndpointUrl) }));
            }
        }

        /// <summary>
        /// Validates the publications root URL.
        /// </summary>
        private void ValidatePublicationsRootUrl()
        {
            if (this.PublicationsRootUrl.Length > PublicationsRootUrlMaxLenght)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, nameof(PublicationsRootUrl), PublicationsRootUrlMaxLenght, 1), new string[] { nameof(PublicationsRootUrl) }));
            }
        }
        #endregion
    }
}

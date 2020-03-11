// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Arthur Souza
// Created          : 01-24-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-11-2020
// ***********************************************************************
// <copyright file="Place.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>Place</summary>
    public class Place : Entity
    {
        public static readonly int NameMaxLength = 100;
        public static readonly int WebsiteMaxLength = 300;
        public static readonly int AdditionalInfoMaxLength = 1000;

        public string Name { get; private set; }
        public bool IsHotel { get; private set; }     
        public bool IsAirport { get; private set; }
        public int? AddressId { get; private set; }
        public string Website { get; private set; }
        public string AdditionalInfo { get; private set; }
        
        public virtual Address Address { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="Place"/> class.</summary>
        protected Place()
        {
        }

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            if (this.ValidationResult == null)
            {
                this.ValidationResult = new ValidationResult();
            }

            this.ValidateName();
            this.ValidateWebsite();
            this.ValidateAdditionalInfo();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the name.</summary>
        public void ValidateName()
        {
            if (string.IsNullOrEmpty(this.Name?.Trim()))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Name), new string[] { "Name" }));
            }

            if (this.Name?.Trim().Length > NameMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Name, NameMaxLength, 1), new string[] { "Name" }));
            }
        }

        /// <summary>Validates the website.</summary>
        public void ValidateWebsite()
        {
            if (!string.IsNullOrEmpty(this.Website?.Trim()) && this.Website?.Trim().Length > WebsiteMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Website, WebsiteMaxLength, 1), new string[] { "Website" }));
            }
        }

        /// <summary>Validates the additional information.</summary>
        public void ValidateAdditionalInfo()
        {
            if (!string.IsNullOrEmpty(this.AdditionalInfo?.Trim()) && this.AdditionalInfo?.Trim().Length > AdditionalInfoMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.AdditionalInfo, AdditionalInfoMaxLength, 1), new string[] { "AdditionalInfo" }));
            }
        }

        #endregion
    }
}
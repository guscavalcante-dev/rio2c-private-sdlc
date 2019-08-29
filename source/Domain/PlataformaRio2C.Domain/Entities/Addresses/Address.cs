// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-22-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-26-2019
// ***********************************************************************
// <copyright file="Address.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>Address</summary>
    public class Address : Entity
    {
        public static readonly int Address1MinLength = 1;
        public static readonly int Address1MaxLength = 200;
        public static readonly int Address2MinLength = 1;
        public static readonly int Address2MaxLength = 200;
        public static readonly int ZipCodeMinLength = 1;
        public static readonly int ZipCodeMaxLength = 10;

        public int CityId { get; private set; }
        public string Address1 { get; private set; }
        public string Address2 { get; private set; }
        public string ZipCode { get; private set; }
        public bool IsManual { get; private set; }
        public decimal? Latitude { get; private set; }
        public decimal? Longitude { get; private set; }
        public bool IsGeoLocationUpdated { get; private set; }

        public virtual City City { get; private set; }

        public virtual ICollection<Organization> Organizations { get; private set; }
        public virtual ICollection<Collaborator> Collaborators { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="Address"/> class.</summary>
        /// <param name="country">The country.</param>
        /// <param name="stateUid">The state uid.</param>
        /// <param name="stateName">Name of the state.</param>
        /// <param name="cityUid">The city uid.</param>
        /// <param name="cityName">Name of the city.</param>
        /// <param name="address1">The address1.</param>
        /// <param name="address2">The address2.</param>
        /// <param name="addressZipCode">The address zip code.</param>
        /// <param name="isManual">if set to <c>true</c> [is manual].</param>
        /// <param name="userId">The user identifier.</param>
        public Address(
            Country country, 
            Guid? stateUid, 
            string stateName, 
            Guid? cityUid, 
            string cityName, 
            string address1,
            string address2,
            string addressZipCode,
            bool isManual, 
            int userId)
        {
            this.Address1 = address1?.Trim();
            this.Address2 = address2?.Trim();
            this.ZipCode = addressZipCode?.Trim();
            this.IsGeoLocationUpdated = false;
            this.IsManual = IsManual;
            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.Now;
            this.CreateUserId = this.UpdateUserId = userId;
            this.UpdateCity(country, stateUid, stateName, cityUid, cityName, isManual, userId);
        }

        /// <summary>Initializes a new instance of the <see cref="Address"/> class.</summary>
        protected Address()
        {
        }

        /// <summary>Updates the specified country.</summary>
        /// <param name="country">The country.</param>
        /// <param name="stateUid">The state uid.</param>
        /// <param name="stateName">Name of the state.</param>
        /// <param name="cityUid">The city uid.</param>
        /// <param name="cityName">Name of the city.</param>
        /// <param name="address1">The address1.</param>
        /// <param name="address2">The address2.</param>
        /// <param name="addressZipCode">The address zip code.</param>
        /// <param name="isManual">if set to <c>true</c> [is manual].</param>
        /// <param name="userId">The user identifier.</param>
        public void Update(
            Country country, 
            Guid? stateUid, 
            string stateName, 
            Guid? cityUid, 
            string cityName, 
            string address1,
            string address2,
            string addressZipCode, 
            bool isManual,
            int userId)
        {
            this.Address1 = address1?.Trim();
            this.Address2 = address2?.Trim();
            this.ZipCode = addressZipCode?.Trim();
            this.IsGeoLocationUpdated = false;
            this.IsManual = IsManual;
            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.Now;
            this.CreateUserId = this.UpdateUserId = userId;
            this.UpdateCity(country, stateUid, stateName, cityUid, cityName, isManual, userId);
        }

        /// <summary>Deletes the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Delete(int userId)
        {
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
            this.IsDeleted = true;
        }

        #region City

        /// <summary>Updates the city.</summary>
        /// <param name="country">The country.</param>
        /// <param name="stateUid">The state uid.</param>
        /// <param name="stateName">Name of the state.</param>
        /// <param name="cityUid">The city uid.</param>
        /// <param name="cityName">Name of the city.</param>
        /// <param name="isManual">if set to <c>true</c> [is manual].</param>
        /// <param name="userId">The user identifier.</param>
        private void UpdateCity(
            Country country, 
            Guid? stateUid, 
            string stateName, 
            Guid? cityUid, 
            string cityName,
            bool isManual,
            int userId)
        {
            this.City = country?.FindCity(stateUid, stateName, cityUid, cityName, isManual, userId);
        }

        #endregion

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateAddress1();
            this.ValidateAddress2();
            this.ValidateZipCode();
            this.ValidateCity();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the address1.</summary>
        public void ValidateAddress1()
        {
            if (!string.IsNullOrEmpty(this.Address1?.Trim()) && (this.Address1?.Trim().Length < Address1MinLength || this.Address1?.Trim().Length > Address1MaxLength))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Address1, Address1MaxLength, Address1MinLength), new string[] { "Address1" }));
            }
        }

        /// <summary>Validates the address2.</summary>
        public void ValidateAddress2()
        {
            if (!string.IsNullOrEmpty(this.Address2?.Trim()) && (this.Address2?.Trim().Length < Address2MinLength || this.Address2?.Trim().Length > Address2MaxLength))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Address2, Address2MaxLength, Address2MinLength), new string[] { "Address2" }));
            }
        }

        /// <summary>Validates the zip code.</summary>
        public void ValidateZipCode()
        {
            if (!string.IsNullOrEmpty(this.ZipCode?.Trim()) && (this.ZipCode?.Trim().Length < ZipCodeMinLength || this.ZipCode?.Trim().Length > ZipCodeMaxLength))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.ZipCode, ZipCodeMaxLength, ZipCodeMinLength), new string[] { "ZipCode" }));
            }
        }

        /// <summary>Validates the city.</summary>
        public void ValidateCity()
        {
            if (this.City != null && !this.City.IsValid() == false)
            {
                this.ValidationResult.Add(this.City.ValidationResult);
            }
        }

        #endregion
    }
}
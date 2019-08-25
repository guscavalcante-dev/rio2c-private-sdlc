// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-22-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-25-2019
// ***********************************************************************
// <copyright file="Address.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>Address</summary>
    public class Address : Entity
    {
        public static readonly int NumberMinLength = 1;
        public static readonly int NumberMaxLength = 16;
        public static readonly int ComplementMinLength = 1;
        public static readonly int ComplementMaxLength = 40;

        public int StreetId { get; private set; }
        public string Number { get; private set; }
        public string Complement { get; private set; }
        public bool IsManual { get; private set; }
        public decimal? Latitude { get; private set; }
        public decimal? Longitude { get; private set; }
        public bool IsGeoLocationUpdated { get; private set; }

        public virtual Street Street { get; private set; }

        public virtual ICollection<Organization> Organizations { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="Address"/> class.</summary>
        /// <param name="country">The country.</param>
        /// <param name="stateUid">The state uid.</param>
        /// <param name="stateName">Name of the state.</param>
        /// <param name="cityUid">The city uid.</param>
        /// <param name="cityName">Name of the city.</param>
        /// <param name="neighborhoodUid">The neighborhood uid.</param>
        /// <param name="neighborhoodName">Name of the neighborhood.</param>
        /// <param name="streetUid">The street uid.</param>
        /// <param name="streetName">Name of the street.</param>
        /// <param name="streetZipCode">The street zip code.</param>
        /// <param name="addressNumber">The address number.</param>
        /// <param name="addressComplement">The address complement.</param>
        /// <param name="isManual">if set to <c>true</c> [is manual].</param>
        /// <param name="userId">The user identifier.</param>
        public Address(
            Country country, 
            Guid? stateUid, 
            string stateName, 
            Guid? cityUid, 
            string cityName, 
            Guid? neighborhoodUid, 
            string neighborhoodName, 
            Guid? streetUid, 
            string streetName, 
            string streetZipCode, 
            string addressNumber, 
            string addressComplement, 
            bool isManual, 
            int userId)
        {
            this.Number = addressNumber?.Trim();
            this.Complement = addressComplement?.Trim();
            this.IsGeoLocationUpdated = false;
            this.IsManual = IsManual;
            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.Now;
            this.CreateUserId = this.UpdateUserId = userId;
            this.UpdateStreet(country, stateUid, stateName, cityUid, cityName, neighborhoodUid, neighborhoodName, streetUid, streetName, streetZipCode, isManual, userId);
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
        /// <param name="neighborhoodUid">The neighborhood uid.</param>
        /// <param name="neighborhoodName">Name of the neighborhood.</param>
        /// <param name="streetUid">The street uid.</param>
        /// <param name="streetName">Name of the street.</param>
        /// <param name="streetZipCode">The street zip code.</param>
        /// <param name="addressNumber">The address number.</param>
        /// <param name="addressComplement">The address complement.</param>
        /// <param name="isManual">if set to <c>true</c> [is manual].</param>
        /// <param name="userId">The user identifier.</param>
        public void Update(
            Country country, 
            Guid? stateUid, 
            string stateName, 
            Guid? cityUid, 
            string cityName, 
            Guid? neighborhoodUid, 
            string neighborhoodName, 
            Guid? streetUid, 
            string streetName, 
            string streetZipCode, 
            string addressNumber, 
            string addressComplement, 
            bool isManual,
            int userId)
        {
            this.Number = addressNumber?.Trim();
            this.Complement = addressComplement?.Trim();
            this.IsGeoLocationUpdated = false;
            this.IsManual = IsManual;
            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.Now;
            this.CreateUserId = this.UpdateUserId = userId;
            this.UpdateStreet(country, stateUid, stateName, cityUid, cityName, neighborhoodUid, neighborhoodName, streetUid, streetName, streetZipCode, isManual, userId);
        }

        /// <summary>Deletes the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Delete(int userId)
        {
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
            this.IsDeleted = true;
        }

        #region Street

        /// <summary>Updates the street.</summary>
        /// <param name="country">The country.</param>
        /// <param name="stateUid">The state uid.</param>
        /// <param name="stateName">Name of the state.</param>
        /// <param name="cityUid">The city uid.</param>
        /// <param name="cityName">Name of the city.</param>
        /// <param name="neighborhoodUid">The neighborhood uid.</param>
        /// <param name="neighborhoodName">Name of the neighborhood.</param>
        /// <param name="streetUid">The street uid.</param>
        /// <param name="streetName">Name of the street.</param>
        /// <param name="streetZipCode">The street zip code.</param>
        /// <param name="isManual">if set to <c>true</c> [is manual].</param>
        /// <param name="userId">The user identifier.</param>
        private void UpdateStreet(
            Country country, 
            Guid? stateUid, 
            string stateName, 
            Guid? cityUid, 
            string cityName,
            Guid? neighborhoodUid, 
            string neighborhoodName, 
            Guid? streetUid, 
            string streetName, 
            string streetZipCode, 
            bool isManual,
            int userId)
        {
            this.Street = country?.FindStreet(stateUid, stateName, cityUid, cityName, neighborhoodUid, neighborhoodName, streetUid, streetName, streetZipCode, isManual, userId);
        }

        #endregion

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateNumber();
            this.ValidateComplement();
            //this.ValidateStreets();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the number.</summary>
        public void ValidateNumber()
        {
            if (!string.IsNullOrEmpty(this.Number?.Trim()) && (this.Number?.Trim().Length < NumberMinLength || this.Number?.Trim().Length > NumberMaxLength))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Number, NumberMaxLength, NumberMinLength), new string[] { "Number" }));
            }
        }

        /// <summary>Validates the complement.</summary>
        public void ValidateComplement()
        {
            if (!string.IsNullOrEmpty(this.Complement?.Trim()) && (this.Complement?.Trim().Length < ComplementMinLength || this.Complement?.Trim().Length > ComplementMaxLength))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.AddressComplement, ComplementMaxLength, ComplementMinLength), new string[] { "Complement" }));
            }
        }

        ///// <summary>Validates the streets.</summary>
        //public void ValidateStreets()
        //{
        //    foreach (var street in this.Streets?.Where(d => !d.IsValid())?.ToList())
        //    {
        //        this.ValidationResult.Add(street.ValidationResult);
        //    }
        //}

        #endregion
    }
}
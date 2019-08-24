// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-22-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-23-2019
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

        /// <summary>Initializes a new instance of the <see cref="Address"/> class.</summary>
        /// <param name="street">The street.</param>
        /// <param name="number">The number.</param>
        /// <param name="complement">The complement.</param>
        /// <param name="isManual">if set to <c>true</c> [is manual].</param>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <param name="isGeoLocationUpdated">if set to <c>true</c> [is geo location updated].</param>
        /// <param name="userId">The user identifier.</param>
        public Address(Street street, string number, string complement, bool isManual, decimal? latitude, decimal? longitude, bool isGeoLocationUpdated, int userId)
        {
            this.Street = street;
            this.Number = number?.Trim();
            this.Complement = complement?.Trim();
            this.IsManual = isManual;
            this.Latitude = latitude;
            this.Longitude = longitude;
            this.IsGeoLocationUpdated = isGeoLocationUpdated;
            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.Now;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="Address"/> class.</summary>
        protected Address()
        {
        }

        /// <summary>Updates the specified number.</summary>
        /// <param name="number">The number.</param>
        /// <param name="complement">The complement.</param>
        /// <param name="isManual">if set to <c>true</c> [is manual].</param>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <param name="isGeoLocationUpdated">if set to <c>true</c> [is geo location updated].</param>
        /// <param name="userId">The user identifier.</param>
        public void Update(string number, string complement, bool isManual, decimal? latitude, decimal? longitude, bool isGeoLocationUpdated, int userId)
        {
            this.Number = number?.Trim();
            this.Complement = complement?.Trim();
            this.IsManual = isManual;
            this.Latitude = latitude;
            this.Longitude = longitude;
            this.IsGeoLocationUpdated = isGeoLocationUpdated;
            this.IsManual = isManual;
            this.IsDeleted = false;
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
        }

        /// <summary>Deletes the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Delete(int userId)
        {
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
            this.IsDeleted = true;
            //this.DeleteStreets(userId);

            //if (this.FindAllStreetsNotDeleted()?.Any() == false)
            //{
            //    this.IsDeleted = true;
            //}
        }

        //#region Streets

        ///// <summary>Deletes the streets.</summary>
        ///// <param name="userId">The user identifier.</param>
        //private void DeleteStreets(int userId)
        //{
        //    foreach (var street in this.FindAllStreetsNotDeleted())
        //    {
        //        street?.Delete(userId);
        //    }
        //}

        ///// <summary>Finds all streets not deleted.</summary>
        ///// <returns></returns>
        //private List<Street> FindAllStreetsNotDeleted()
        //{
        //    return this.Streets?.Where(c => !c.IsDeleted)?.ToList();
        //}

        //#endregion

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
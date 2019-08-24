// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-22-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-23-2019
// ***********************************************************************
// <copyright file="Street.cs" company="Softo">
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
    /// <summary>Street</summary>
    public class Street : Entity
    {
        public static readonly int NameMinLength = 3;
        public static readonly int NameMaxLength = 100;
        public static readonly int ZipCodeMinLength = 3;
        public static readonly int ZipCodeMaxLength = 100;

        public int NeighborhoodId { get; private set; }
        public string Name { get; private set; }
        public string ZipCode { get; private set; }
        public bool IsManual { get; private set; }

        public virtual Neighborhood Neighborhood { get; private set; }
        public virtual ICollection<Address> Addresses { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="Street"/> class.</summary>
        /// <param name="neighborhood">The neighborhood.</param>
        /// <param name="name">The name.</param>
        /// <param name="zipCode">The zip code.</param>
        /// <param name="isManual">if set to <c>true</c> [is manual].</param>
        /// <param name="userId">The user identifier.</param>
        public Street(Neighborhood neighborhood, string name, string zipCode, bool isManual, int userId)
        {
            this.Neighborhood = neighborhood;
            this.Name = name?.Trim();
            this.ZipCode = zipCode?.Trim();
            this.IsManual = isManual;
            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.Now;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="Street"/> class.</summary>
        protected Street()
        {
        }

        /// <summary>Updates the specified name.</summary>
        /// <param name="name">The name.</param>
        /// <param name="zipCode">The zip code.</param>
        /// <param name="isManual">if set to <c>true</c> [is manual].</param>
        /// <param name="userId">The user identifier.</param>
        public void Update(string name, string zipCode, bool isManual, int userId)
        {
            this.Name = name?.Trim();
            this.ZipCode = zipCode?.Trim();
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
            this.DeleteAddresses(userId);

            if (this.FindAllAddressesNotDeleted()?.Any() == false)
            {
                this.IsDeleted = true;
            }
        }

        #region Addresses

        /// <summary>Deletes the addresses.</summary>
        /// <param name="userId">The user identifier.</param>
        private void DeleteAddresses(int userId)
        {
            foreach (var address in this.FindAllAddressesNotDeleted())
            {
                address?.Delete(userId);
            }
        }

        /// <summary>Finds all addresses not deleted.</summary>
        /// <returns></returns>
        private List<Address> FindAllAddressesNotDeleted()
        {
            return this.Addresses?.Where(c => !c.IsDeleted)?.ToList();
        }

        #endregion

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateName();
            this.ValidateZipCode();
            this.ValidadeAddresses();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the name.</summary>
        public void ValidateName()
        {
            if (string.IsNullOrEmpty(this.Name?.Trim()))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Name), new string[] { "Name" }));
            }

            if (this.Name?.Trim().Length < NameMinLength || this.Name?.Trim().Length > NameMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Name, NameMaxLength, NameMinLength), new string[] { "Name" }));
            }
        }

        /// <summary>Validates the zip code.</summary>
        public void ValidateZipCode()
        {
            if (string.IsNullOrEmpty(this.ZipCode?.Trim()))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.ZipCode), new string[] { "ZipCode" }));
            }

            if (this.ZipCode?.Trim().Length < ZipCodeMinLength || this.ZipCode?.Trim().Length > ZipCodeMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.ZipCode, ZipCodeMaxLength, ZipCodeMinLength), new string[] { "ZipCode" }));
            }
        }

        /// <summary>Validades the addresses.</summary>
        public void ValidadeAddresses()
        {
            foreach (var address in this.Addresses?.Where(d => !d.IsValid())?.ToList())
            {
                this.ValidationResult.Add(address.ValidationResult);
            }
        }

        #endregion
    }
}
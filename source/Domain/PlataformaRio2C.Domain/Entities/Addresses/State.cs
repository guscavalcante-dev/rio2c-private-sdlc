// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-23-2019
// ***********************************************************************
// <copyright file="State.cs" company="Softo">
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
    /// <summary>State</summary>
    public class State : Entity
    {
        public static readonly int NameMinLength = 2;
        public static readonly int NameMaxLength = 100;
        public static readonly int CodeMinLength = 1;
        public static readonly int CodeMaxLength = 2;

        public int CountryId { get; private set; }
        public string Name { get; private set; }
        public string Code { get; private set; }
        public bool IsManual { get; private set; }

        public virtual Country Country { get; private set; }
        public virtual ICollection<City> Cities { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="State"/> class.</summary>
        /// <param name="country">The country.</param>
        /// <param name="name">The name.</param>
        /// <param name="code">The code.</param>
        /// <param name="isManual">if set to <c>true</c> [is manual].</param>
        /// <param name="userId">The user identifier.</param>
        public State(Country country, string name, string code, bool isManual, int userId)
        {
            this.Country = country;
            this.Name = name?.Trim();
            this.Code = code?.Trim();
            this.IsManual = isManual;
            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.Now;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="State"/> class.</summary>
        protected State()
        {
        }

        /// <summary>Updates the specified name.</summary>
        /// <param name="name">The name.</param>
        /// <param name="code">The code.</param>
        /// <param name="isManual">if set to <c>true</c> [is manual].</param>
        /// <param name="userId">The user identifier.</param>
        public void Update(string name, string code, bool isManual, int userId)
        {
            this.Name = name?.Trim();
            this.Code = code?.Trim();
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
            this.DeleteCities(userId);

            if (this.FindAllCitiesNotDeleted()?.Any() == false)
            {
                this.IsDeleted = true;
            }
        }

        #region Cities

        /// <summary>Deletes the cities.</summary>
        /// <param name="userId">The user identifier.</param>
        private void DeleteCities(int userId)
        {
            foreach (var city in this.FindAllCitiesNotDeleted())
            {
                city?.Delete(userId);
            }
        }

        /// <summary>Finds all cities not deleted.</summary>
        /// <returns></returns>
        private List<City> FindAllCitiesNotDeleted()
        {
            return this.Cities?.Where(c => !c.IsDeleted)?.ToList();
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
            this.ValidateCode();
            this.ValidateCities();

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

        /// <summary>Validates the code.</summary>
        public void ValidateCode()
        {
            if (string.IsNullOrEmpty(this.Code?.Trim()))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Code), new string[] { "Code" }));
            }

            if (this.Code?.Trim().Length < CodeMinLength || this.Code?.Trim().Length > CodeMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Code, CodeMaxLength, CodeMinLength), new string[] { "Code" }));
            }
        }

        /// <summary>Validates the cities.</summary>
        public void ValidateCities()
        {
            foreach (var city in this.Cities?.Where(d => !d.IsValid())?.ToList())
            {
                this.ValidationResult.Add(city.ValidationResult);
            }
        }

        #endregion
    }
}
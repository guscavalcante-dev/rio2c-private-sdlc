﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-08-2020
// ***********************************************************************
// <copyright file="State.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public virtual ICollection<Address> Addresses { get; private set; }

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
            this.Code = code?.Trim() ??
                        name?.Trim().GetTwoLetterCode();
            this.IsManual = isManual;
            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
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
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>Deletes the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Delete(int userId)
        {
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
            this.DeleteCities(userId);
            this.DeleteAddresses(userId);

            if (this.FindAllCitiesNotDeleted()?.Any() == false && this.FindAllAddressesNotDeleted()?.Any() == false)
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

        /// <summary>Finds the city not deleted by uid.</summary>
        /// <param name="cityUid">The city uid.</param>
        /// <returns></returns>
        private City FindCityNotDeletedByUid(Guid cityUid)
        {
            return this.Cities?.FirstOrDefault(c => c.Uid == cityUid && !c.IsDeleted);
        }

        /// <summary>Finds the name of the city not deleted by.</summary>
        /// <param name="cityName">Name of the city.</param>
        /// <returns></returns>
        private City FindCityNotDeletedByName(string cityName)
        {
            return this.Cities?.FirstOrDefault(s => s.Name.Trim().ToLowerInvariant() == cityName?.Trim()?.ToLowerInvariant() && !s.IsDeleted);
        }

        /// <summary>Finds the or create city.</summary>
        /// <param name="cityUid">The city uid.</param>
        /// <param name="cityName">Name of the city.</param>
        /// <param name="isManual">if set to <c>true</c> [is manual].</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        private City FindOrCreateCity(Guid? cityUid, string cityName, bool isManual, int userId)
        {
            if (this.Cities == null)
            {
                this.Cities = new List<City>();
            }

            City city = null;
            if (cityUid.HasValue)
            {
                city = this.FindCityNotDeletedByUid(cityUid.Value);
            }
            else if (!string.IsNullOrEmpty(cityName?.Trim()))
            {
                city = this.FindCityNotDeletedByName(cityName) ??
                       new City(this, cityName, isManual, userId);
            }

            if (city == null)
            {
                throw new DomainException(string.Format(Messages.CouldNotCreate, Labels.TheF.ToLowerInvariant(), Labels.City.ToLowerInvariant()));
            }

            return city;
        }

        /// <summary>Finds the city.</summary>
        /// <param name="cityUid">The city uid.</param>
        /// <param name="cityName">Name of the city.</param>
        /// <param name="isManual">if set to <c>true</c> [is manual].</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public City FindCity(Guid? cityUid, string cityName, bool isManual, int userId)
        {
            return this.FindOrCreateCity(cityUid, cityName, isManual, userId);
        }

        #endregion

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
            return this.Addresses?.Where(n => !n.IsDeleted)?.ToList();
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
            this.ValidateCountry();

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

        /// <summary>Validates the country.</summary>
        public void ValidateCountry()
        {
            if (this.Country != null && !this.Country.IsValid() == false)
            {
                this.ValidationResult.Add(this.Country.ValidationResult);
            }
        }

        #endregion
    }
}
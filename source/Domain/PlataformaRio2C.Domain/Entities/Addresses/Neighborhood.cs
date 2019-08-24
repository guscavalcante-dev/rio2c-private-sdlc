// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-22-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-23-2019
// ***********************************************************************
// <copyright file="Neighborhood.cs" company="Softo">
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
    /// <summary>Neighborhood</summary>
    public class Neighborhood : Entity
    {
        public static readonly int NameMinLength = 3;
        public static readonly int NameMaxLength = 100;

        public int CityId { get; private set; }
        public string Name { get; private set; }
        public bool IsManual { get; private set; }

        public virtual City City { get; private set; }
        public virtual ICollection<Street> Streets { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="Neighborhood"/> class.</summary>
        /// <param name="city">The city.</param>
        /// <param name="name">The name.</param>
        /// <param name="isManual">if set to <c>true</c> [is manual].</param>
        /// <param name="userId">The user identifier.</param>
        public Neighborhood(City city, string name, bool isManual, int userId)
        {
            this.City = city;
            this.Name = name?.Trim();
            this.IsManual = isManual;
            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.Now;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="Neighborhood"/> class.</summary>
        protected Neighborhood()
        {
        }

        /// <summary>Updates the specified name.</summary>
        /// <param name="name">The name.</param>
        /// <param name="isManual">if set to <c>true</c> [is manual].</param>
        /// <param name="userId">The user identifier.</param>
        public void Update(string name, bool isManual, int userId)
        {
            this.Name = name?.Trim();
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
            this.DeleteStreets(userId);

            if (this.FindAllStreetsNotDeleted()?.Any() == false)
            {
                this.IsDeleted = true;
            }
        }

        #region Streets

        /// <summary>Deletes the streets.</summary>
        /// <param name="userId">The user identifier.</param>
        private void DeleteStreets(int userId)
        {
            foreach (var street in this.FindAllStreetsNotDeleted())
            {
                street?.Delete(userId);
            }
        }

        /// <summary>Finds all streets not deleted.</summary>
        /// <returns></returns>
        private List<Street> FindAllStreetsNotDeleted()
        {
            return this.Streets?.Where(c => !c.IsDeleted)?.ToList();
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
            this.ValidateStreets();

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

        /// <summary>Validates the streets.</summary>
        public void ValidateStreets()
        {
            foreach (var street in this.Streets?.Where(d => !d.IsValid())?.ToList())
            {
                this.ValidationResult.Add(street.ValidationResult);
            }
        }

        #endregion
    }
}
// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-23-2019
// ***********************************************************************
// <copyright file="City.cs" company="Softo">
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
    /// <summary>City</summary>
    public class City : Entity
    {
        public static readonly int NameMinLength = 3;
        public static readonly int NameMaxLength = 100;

        public int StateId { get; private set; }
        public string Name { get; private set; }
        public bool IsManual { get; private set; }

        public virtual State State { get; private set; }
        public virtual ICollection<Neighborhood> Neighborhoods { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="City"/> class.</summary>
        /// <param name="state">The state.</param>
        /// <param name="name">The name.</param>
        /// <param name="isManual">if set to <c>true</c> [is manual].</param>
        /// <param name="userId">The user identifier.</param>
        public City(State state, string name, bool isManual, int userId)
        {
            this.State = state;
            this.Name = name?.Trim();
            this.IsManual = isManual;
            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.Now;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="City"/> class.</summary>
        protected City()
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
            this.DeleteNeighborhoods(userId);

            if (this.FindAllNeighborhoodsNotDeleted()?.Any() == false)
            {
                this.IsDeleted = true;
            }
        }

        #region Neighborhoods

        /// <summary>Deletes the neighborhoods.</summary>
        /// <param name="userId">The user identifier.</param>
        private void DeleteNeighborhoods(int userId)
        {
            foreach (var neighborhoods in this.FindAllNeighborhoodsNotDeleted())
            {
                neighborhoods?.Delete(userId);
            }
        }

        /// <summary>Finds all neighborhoods not deleted.</summary>
        /// <returns></returns>
        private List<Neighborhood> FindAllNeighborhoodsNotDeleted()
        {
            return this.Neighborhoods?.Where(c => !c.IsDeleted)?.ToList();
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
            this.ValidateNeighborhoods();

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

        /// <summary>Validates the neighborhoods.</summary>
        public void ValidateNeighborhoods()
        {
            foreach (var neighborhood in this.Neighborhoods?.Where(d => !d.IsValid())?.ToList())
            {
                this.ValidationResult.Add(neighborhood.ValidationResult);
            }
        }

        #endregion
    }
}
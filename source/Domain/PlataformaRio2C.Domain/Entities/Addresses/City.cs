// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-24-2019
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
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;

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
            return this.Neighborhoods?.Where(n => !n.IsDeleted)?.ToList();
        }

        /// <summary>Finds the neighborhood not deleted by uid.</summary>
        /// <param name="neighborhoodUid">The neighborhood uid.</param>
        /// <returns></returns>
        private Neighborhood FindNeighborhoodNotDeletedByUid(Guid neighborhoodUid)
        {
            return this.Neighborhoods?.FirstOrDefault(n => n.Uid == neighborhoodUid && !n.IsDeleted);
        }

        /// <summary>Finds the name of the neighborhood not deleted by.</summary>
        /// <param name="neighborhoodName">Name of the neighborhood.</param>
        /// <returns></returns>
        private Neighborhood FindNeighborhoodNotDeletedByName(string neighborhoodName)
        {
            return this.Neighborhoods?.FirstOrDefault(n => n.Name.Trim().ToLowerInvariant() == neighborhoodName?.Trim()?.ToLowerInvariant() && !n.IsDeleted);
        }

        /// <summary>Finds the or create neighborhood.</summary>
        /// <param name="neighborhoodUid">The neighborhood uid.</param>
        /// <param name="neighborhoodName">Name of the neighborhood.</param>
        /// <param name="isManual">if set to <c>true</c> [is manual].</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        private Neighborhood FindOrCreateNeighborhood(Guid? neighborhoodUid, string neighborhoodName, bool isManual, int userId)
        {
            if (this.Neighborhoods == null)
            {
                this.Neighborhoods = new List<Neighborhood>();
            }

            Neighborhood neighborhood = null;
            if (neighborhoodUid.HasValue)
            {
                neighborhood = this.FindNeighborhoodNotDeletedByUid(neighborhoodUid.Value);
            }
            else if (!string.IsNullOrEmpty(neighborhoodName?.Trim()))
            {
                neighborhood = this.FindNeighborhoodNotDeletedByName(neighborhoodName) ??
                               new Neighborhood(this, neighborhoodName, isManual, userId);
            }

            if (neighborhood == null)
            {
                throw new DomainException("Could not create the neighborhood."); //TODO: Translate city error
            }

            return neighborhood;
        }

        #endregion

        #region Streets

        /// <summary>Finds the street.</summary>
        /// <param name="neighborhoodUid">The neighborhood uid.</param>
        /// <param name="neighborhoodName">Name of the neighborhood.</param>
        /// <param name="streetUid">The street uid.</param>
        /// <param name="streetName">Name of the street.</param>
        /// <param name="streetZipCode">The street zip code.</param>
        /// <param name="isManual">if set to <c>true</c> [is manual].</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public Street FindStreet(Guid? neighborhoodUid, string neighborhoodName, Guid? streetUid, string streetName, string streetZipCode, bool isManual, int userId)
        {
            var neighborhood = this.FindOrCreateNeighborhood(neighborhoodUid, neighborhoodName, isManual, userId);
            return neighborhood?.FindStreet(streetUid, streetName, streetZipCode, isManual, userId);
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